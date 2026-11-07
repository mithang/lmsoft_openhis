#!/usr/bin/env bash
# OpenHIS — one-command local setup with Docker.
#
#   ./setup.sh            Start the 6 core services (SQL Server, Redis,
#                         static host, BaseAPI, Sett-API, SSO) and run a
#                         demo-login smoke test.
#   ./setup.sh --all      Same, plus the other 17 apps (profile "all",
#                         23 apps total).
#   ./setup.sh --skip-restore
#                         Skip the database restore step (databases are
#                         already in the sqldata volume).
#
# What you need first: Docker with Compose v2 (Docker Desktop on
# Windows/macOS, or Docker Engine on Linux), python3, curl, openssl,
# and the database backups — either ./sqlbackup/*.bak on disk, or set
# OPENHIS_BACKUP_URL to a .zip/.tar.gz of .bak files. See DOCKER-SETUP.md
# for the current download link and requirements (RAM: 4 GB core,
# 8 GB full; Windows needs WSL2 or Git Bash).
#
# Safety notes:
# * No sudo / systemctl is used. Nothing is killed: if a required port
#   is busy the script reports which port (and which process, when it
#   can tell) and stops.
# * .env is generated locally with fresh random secrets and is never
#   committed (gitignored). The VM's real secrets are never read.
# * The demo login smoke test runs EXACTLY ONCE. A failed login is
#   reported with the app logs and the script stops — it never retries
#   (3 consecutive failures lock the demo account for 60 minutes).
# * Re-running setup.sh is safe: an existing .env is kept, databases
#   that already exist are skipped by the restore step.

set -euo pipefail
cd "$(dirname "$0")"

ALL=0
SKIP_RESTORE=0
for arg in "$@"; do
  case "$arg" in
    --all) ALL=1 ;;
    --skip-restore) SKIP_RESTORE=1 ;;
    -h|--help)
      sed -n '2,30p' "$0"
      exit 0
      ;;
    *) echo "Unknown option: $arg (see --help)" >&2; exit 2 ;;
  esac
done

# Loopback calls must bypass any proxy on the host, or curl talks to
# the proxy instead of the containers. Use the function below (not a
# plain variable) so the '*' in --noproxy '*' is never glob-expanded.
export no_proxy="${no_proxy:+$no_proxy,}127.0.0.1,localhost"
curl_loopback() { curl --noproxy '*' -s "$@"; }

log()  { printf '[setup] %s\n' "$*"; }
fail() { printf '[setup] ERROR: %s\n' "$*" >&2; exit 1; }

# ------------------------------------------------------------------ step 1
log "Checking tools..."
command -v docker >/dev/null 2>&1 || fail "docker is not installed — install Docker Desktop (Windows/macOS) or Docker Engine (Linux), then re-run."
docker info >/dev/null 2>&1 || fail "Docker daemon is not running — start Docker Desktop / the docker service, then re-run."
if docker compose version >/dev/null 2>&1; then
  COMPOSE="docker compose"
elif command -v docker-compose >/dev/null 2>&1; then
  COMPOSE="docker-compose"
else
  fail "docker compose v2 (or docker-compose) is not available."
fi
command -v python3 >/dev/null 2>&1 || fail "python3 is not installed — it is needed for the database restore step."
command -v curl >/dev/null 2>&1 || fail "curl is not installed."
log "Using: $COMPOSE"

# ------------------------------------------------------------------ step 2
# Ports the core setup needs; --all needs every app port as well.
CORE_PORTS="1433 6379 41142 5208 7001 7002"
ALL_PORTS="1433 6379 41142 5208 5220 5221 5231 5232 5233 5234 5235 5236 5237 5238 5320 5331 5332 5333 5334 5335 5336 5337 7001 7002 8015 8016"
if [ "$ALL" -eq 1 ]; then PORTS="$ALL_PORTS"; else PORTS="$CORE_PORTS"; fi

port_busy() { # $1 = port -> 0 if busy
  local port="$1"
  if command -v ss >/dev/null 2>&1; then
    ss -ltn 2>/dev/null | grep -q ":${port} "
  elif command -v lsof >/dev/null 2>&1; then
    lsof -i ":${port}" -sTCP:LISTEN >/dev/null 2>&1
  else
    (echo > "/dev/tcp/127.0.0.1/${port}") >/dev/null 2>&1
  fi
}

port_holder() { # $1 = port -> best-effort "pid/command" or "unknown"
  local port="$1" holder=""
  if command -v ss >/dev/null 2>&1; then
    holder="$(ss -ltnp 2>/dev/null | grep ":${port} " | grep -oE 'users:\(\("[^"]+"[^)]*\)\)' | head -1)"
  fi
  if [ -z "$holder" ] && command -v lsof >/dev/null 2>&1; then
    holder="$(lsof -i ":${port}" -sTCP:LISTEN 2>/dev/null | tail -1)"
  fi
  printf '%s' "${holder:-unknown}"
}

log "Checking required ports are free..."
# Ports published by this compose project's own containers are not conflicts:
# re-running setup.sh while the stack is up must keep working.
PROJECT_NAME="$(basename "$PWD" | tr '[:upper:]' '[:lower:]' | tr -cd 'a-z0-9_-')"
own_ports() {
  docker ps --filter "label=com.docker.compose.project=${PROJECT_NAME}" \
    --format '{{.Ports}}' 2>/dev/null \
    | grep -oE ':[0-9]+->' | tr -d ':->' | sort -u || true
}
OWN_PORTS=" $(own_ports | tr '\n' ' ') "
BUSY=""
for p in $PORTS; do
  case "$OWN_PORTS" in *" $p "*) continue ;; esac
  if port_busy "$p"; then
    BUSY="$BUSY $p($(port_holder "$p"))"
  fi
done
if [ -n "$BUSY" ]; then
  fail "these ports are already in use:${BUSY} — stop whatever holds them (or run the VM stack down first) and re-run. Nothing was started or killed."
fi
log "All required ports are free."

# ------------------------------------------------------------------ step 3
rand_pw() { # SQL/Redis password: base64 without /+=, plus aB1! so the
            # SQL Server complexity rule (upper+lower+digit+symbol) holds.
  local r=""
  if command -v openssl >/dev/null 2>&1; then
    r="$(openssl rand -base64 18 2>/dev/null | tr -d '/+=\n' | head -c 20)"
  fi
  if [ -z "$r" ]; then
    r="$(head -c 24 /dev/urandom | od -An -tx1 | tr -d ' \n' | head -c 20)"
  fi
  printf '%s' "${r}aB1!"
}
rand_hex() { # 64 hex chars for APP_SECRET / JWT key
  if command -v openssl >/dev/null 2>&1; then
    openssl rand -hex 32 2>/dev/null
  else
    head -c 32 /dev/urandom | od -An -tx1 | tr -d ' \n'
  fi
}

if [ ! -f .env ]; then
  log "Generating .env with fresh random secrets..."
  {
    echo "# Generated by setup.sh on $(date -u +%Y-%m-%dT%H:%M:%SZ) — local only, never commit."
    echo "SA_PASSWORD=$(rand_pw)"
    echo "REDIS_PASSWORD=$(rand_pw)"
    echo "APP_SECRET=$(rand_hex)"
    echo "AUTHCENTER_JWT_KEY=$(rand_hex)"
  } > .env
  chmod 600 .env
  log ".env created (values are NOT printed). Keep it — re-running setup.sh reuses it."
else
  log ".env already exists — reusing it (delete it to regenerate secrets)."
fi
# shellcheck disable=SC1091
set -a; . ./.env; set +a
: "${SA_PASSWORD:?SA_PASSWORD is empty in .env}"
: "${REDIS_PASSWORD:?REDIS_PASSWORD is empty in .env}"
: "${APP_SECRET:?APP_SECRET is empty in .env}"
: "${AUTHCENTER_JWT_KEY:?AUTHCENTER_JWT_KEY is empty in .env}"
export OPENHIS_COMPOSE_CMD="$COMPOSE"

# ------------------------------------------------------------------ step 4
mkdir -p sqlbackup
have_bak() { ls sqlbackup/*.bak >/dev/null 2>&1; }
if ! have_bak; then
  if [ -n "${OPENHIS_BACKUP_URL:-}" ]; then
    log "No local backups — downloading $OPENHIS_BACKUP_URL ..."
    tmpdl="$(mktemp -d)/backup-download"
    mkdir -p "$tmpdl"
    curl -L --fail -o "$tmpdl/archive" "$OPENHIS_BACKUP_URL" \
      || fail "download failed — check OPENHIS_BACKUP_URL and your network."
    case "$OPENHIS_BACKUP_URL" in
      *.zip)     command -v unzip >/dev/null 2>&1 || fail "unzip is needed to extract a .zip backup."
                unzip -j -q -o "$tmpdl/archive" '*.bak' -d sqlbackup \
                  || fail "could not extract the .zip backup." ;;
      *.tar.gz|*.tgz)
                tmpx="$(mktemp -d)"
                tar -xzf "$tmpdl/archive" -C "$tmpx" \
                  || { rm -rf "$tmpx"; fail "could not extract the .tar.gz backup."; }
                find "$tmpx" -name '*.bak' -exec mv {} sqlbackup/ \;
                rm -rf "$tmpx" ;;
      *) fail "OPENHIS_BACKUP_URL must end in .zip or .tar.gz (got: $OPENHIS_BACKUP_URL)" ;;
    esac
    rm -rf "$tmpdl"
    have_bak || fail "the archive contained no *.bak files."
  else
    fail "no database backups found. Put the 14 .bak files in ./sqlbackup/ or set OPENHIS_BACKUP_URL to a .zip/.tar.gz of them. See DOCKER-SETUP.md for the current download link. No schema is invented and no upgrade scripts are run — the backups already contain the final state."
  fi
fi
log "Backups present: $(ls sqlbackup/*.bak | wc -l) .bak files in ./sqlbackup/"

# ------------------------------------------------------------------ step 5
if [ "$ALL" -eq 1 ]; then
  log "Building all images (23 apps — first build downloads the .NET SDK + NuGet packages, allow several minutes)..."
  $COMPOSE --profile all build
else
  log "Building core images (BaseAPI, Sett-API, SSO, static — first build downloads the .NET SDK + NuGet packages, allow several minutes)..."
  $COMPOSE build baseapi settapi sso static
fi

# ------------------------------------------------------------------ step 6
log "Starting SQL Server + Redis..."
$COMPOSE up -d sqlserver redis

log "Waiting for SQL Server to answer (up to 180s)..."
for _ in $(seq 1 36); do
  if $COMPOSE exec -T -e SQLCMDPASSWORD="$SA_PASSWORD" sqlserver \
      /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -C -Q "SELECT 1" \
      >/dev/null 2>&1; then
    log "SQL Server is up."
    break
  fi
  sleep 5
done
$COMPOSE exec -T -e SQLCMDPASSWORD="$SA_PASSWORD" sqlserver \
  /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -C -Q "SELECT 1" >/dev/null 2>&1 \
  || fail "SQL Server did not answer in 180s — see: $COMPOSE logs sqlserver"

log "Waiting for Redis to answer (up to 60s)..."
for _ in $(seq 1 12); do
  if $COMPOSE exec -T redis redis-cli -a "$REDIS_PASSWORD" ping 2>/dev/null | grep -q PONG; then
    log "Redis is up."
    break
  fi
  sleep 5
done
$COMPOSE exec -T redis redis-cli -a "$REDIS_PASSWORD" ping 2>/dev/null | grep -q PONG \
  || fail "Redis did not answer in 60s — see: $COMPOSE logs redis"

# ------------------------------------------------------------------ step 7
if [ "$SKIP_RESTORE" -eq 1 ]; then
  log "--skip-restore: keeping the databases already in the sqldata volume."
else
  log "Restoring databases (existing ones are skipped)..."
  MSSQL_SA_PASSWORD="$SA_PASSWORD" python3 docker/db/restore_dbs.py \
    --backup-dir ./sqlbackup --service sqlserver \
    || fail "database restore failed — see the summary above."
fi

# ------------------------------------------------------------------ step 8
if [ "$ALL" -eq 1 ]; then
  log "Starting all 23 apps..."
  $COMPOSE --profile all up -d
else
  log "Starting core apps (BaseAPI, Sett-API, SSO, static)..."
  $COMPOSE up -d baseapi settapi sso static
fi

wait_http() { # $1 = url, $2 = seconds, $3 = label
  local url="$1" secs="$2" label="$3" i code
  for i in $(seq 1 "$((secs / 5))"); do
    code="$(curl_loopback --connect-timeout 3 --max-time 5 -o /dev/null -w '%{http_code}' "$url" 2>/dev/null)" || code=000
    case "$code" in
      2??|3??)
        log "$label is up (HTTP $code)."
        return 0
        ;;
    esac
    sleep 5
  done
  return 1
}
wait_http "http://127.0.0.1:5208/Login/Index" 300 "SSO (5208)" \
  || fail "SSO did not answer in 300s — see: $COMPOSE logs sso baseapi"
wait_http "http://127.0.0.1:7002/" 300 "Sett-API (7002)" \
  || fail "Sett-API did not answer in 300s — see: $COMPOSE logs settapi"

# ------------------------------------------------------------------ step 9
# Demo smoke login — EXACTLY ONE attempt. The password sent is the md5 of
# the plaintext "000000" (the login JS hashes it before POST, same as the
# browser). Never retry: 3 consecutive failures lock the account.
log "Smoke test: one demo login as 000000..."
if command -v md5sum >/dev/null 2>&1; then
  MD5="$(printf '000000' | md5sum | cut -d' ' -f1)"
elif command -v md5 >/dev/null 2>&1; then
  MD5="$(printf '000000' | md5 -q)"
else
  fail "neither md5sum nor md5 is available — cannot run the smoke login."
fi
RESP="$(curl_loopback -X POST --data-urlencode "username=000000" \
  --data-urlencode "password=$MD5" \
  http://127.0.0.1:5208/Login/CheckLogin 2>/dev/null || echo "")"
if printf '%s' "$RESP" | grep -q '"state":"success"'; then
  log "Demo login OK: $RESP"
else
  printf '[setup] Login response was: %s\n' "${RESP:-<empty>}" >&2
  printf '[setup] Last SSO logs:\n' >&2
  $COMPOSE logs --tail=50 sso >&2 || true
  printf '[setup] Last BaseAPI logs:\n' >&2
  $COMPOSE logs --tail=50 baseapi >&2 || true
  fail "demo login failed — see the logs above. Do NOT retry by hand: 3 consecutive failures lock account 000000 for 60 minutes."
fi

# ----------------------------------------------------------------- step 10
cat <<'EOF'

==================== OpenHIS is up ====================
SSO portal:      http://127.0.0.1:5208
Demo login:      000000 / 000000
BaseAPI:         http://127.0.0.1:7001  (root returns 404 — that is normal)
Sett-API:        http://127.0.0.1:7002
Static assets:   http://127.0.0.1:41142
SQL Server:      127.0.0.1:1433 (user sa, password is in .env)
Redis:           127.0.0.1:6379 (password is in .env)

Open http://127.0.0.1:5208 in the browser (use 127.0.0.1, not
localhost) and log in as 000000 / 000000. From the SSO home page the
portal tiles open the other apps.
EOF
if [ "$ALL" -eq 1 ]; then
cat <<'EOF'
Full profile is up (all 23 apps):
  Sett 5221, MRQC 5220, OR 5231, MRMS 5232, Base HOSP 5233,
  CIS 5234, EMR 5235, Herp 5236, PDS 5237, Base Web 5238,
  MRQC.API 5320, OR.API 5331, MRMS.API 5332, HOSP.API 5333,
  CIS.API 5334, EMR.API 5335, Herp.API 5336, PDS.API 5337,
  AuthCenter 8015, WebAPI.Manage 8016
EOF
else
cat <<'EOF'
Only the 6 core services are up. To start all 23 apps:
  ./setup.sh --all
(or: docker compose --profile all up -d)
EOF
fi
cat <<'EOF'
Stop everything (keeps data volumes):
  docker compose down
Stop everything AND delete data volumes (databases + Redis data):
  docker compose down -v
Logs for one service:
  docker compose logs -f sso
=========================================================
EOF
