#!/usr/bin/env python3
"""Restore OpenHIS .bak backups into the Docker SQL Server container.

Adapted from ~/workspace/restore_dbs.py (proven on the VM against the
local SQL Server). Differences from the VM version:

* sqlcmd is NOT run on the host. It runs inside the sqlserver container
  via `docker compose exec -T sqlserver /opt/mssql-tools18/bin/sqlcmd`,
  so the host needs no sqlcmd install. The password is passed with
  SQLCMDPASSWORD (sqlcmd reads it from the environment) instead of -P,
  so it never appears on a command line.
* Backup dir is the HOST ./sqlbackup, mounted read-only into the
  container at /sqlbackup (see docker-compose.yml). sqlcmd sees the
  in-container path /sqlbackup/<file>.bak.
* DB name = backup filename minus .bak, minus a trailing _YYYYMMDD date
  suffix if present. That handles BOTH naming styles:
  sqlbackup-current (NewtouchHIS_Base.bak -> NewtouchHIS_Base) and the
  V1.8 set (NewtouchHIS_Base_20241108.bak -> NewtouchHIS_Base).
  The VM script stripped everything after the LAST underscore, which
  would wrongly turn NewtouchHIS_Base.bak into NewtouchHIS.
* Idempotent: a database that already exists in sys.databases is
  SKIPPED (reported, not an error), so setup.sh can be re-run safely.

Usage (run from the repo root, after `docker compose up -d sqlserver`):
    MSSQL_SA_PASSWORD=<from .env> python3 docker/db/restore_dbs.py
    MSSQL_SA_PASSWORD=<from .env> python3 docker/db/restore_dbs.py \
        --backup-dir ./sqlbackup --service sqlserver
"""
import argparse
import os
import re
import subprocess
import sys

SQLCMD_IN_CONTAINER = "/opt/mssql-tools18/bin/sqlcmd"
CONTAINER_BACKUP_DIR = "/sqlbackup"
CONTAINER_DATA_DIR = "/var/opt/mssql/data"


def build_cmd(compose_cmd, service, extra_env):
    # -T disables TTY allocation (needed for piped input/output).
    cmd = compose_cmd + ["exec", "-T"]
    for k, v in extra_env.items():
        cmd += ["-e", f"{k}={v}"]
    cmd += [service, SQLCMD_IN_CONTAINER, "-S", "localhost", "-U", "sa",
            "-C", "-b"]
    return cmd


def run_sqlcmd(compose_cmd, service, pw, query, extra=None):
    cmd = build_cmd(compose_cmd, service, {"SQLCMDPASSWORD": pw})
    if extra:
        cmd += extra
    cmd += ["-Q", query]
    return subprocess.run(cmd, capture_output=True, text=True)


def existing_dbs(compose_cmd, service, pw):
    r = run_sqlcmd(compose_cmd, service, pw,
                   "SELECT name FROM sys.databases WHERE database_id > 4",
                   extra=["-W", "-h", "-1"])
    if r.returncode != 0:
        return None, (r.stdout + r.stderr).strip()[-500:]
    return {line.strip() for line in (r.stdout or "").splitlines()
            if line.strip()}, ""


def db_name_from_file(filename):
    stem = filename[:-4] if filename.lower().endswith(".bak") else filename
    # Strip a trailing _YYYYMMDD date suffix only when it is there.
    m = re.match(r"^(.*)_\d{8}$", stem)
    return m.group(1) if m else stem


def file_list(compose_cmd, service, pw, bak_in_container):
    r = run_sqlcmd(
        compose_cmd, service, pw,
        f"RESTORE FILELISTONLY FROM DISK = N'{bak_in_container}'",
        extra=["-s", ",", "-W", "-h", "-1"],
    )
    if r.returncode != 0:
        return None, (r.stdout + r.stderr).strip()[-500:]
    files = []
    for line in (r.stdout or "").splitlines():
        parts = [p.strip() for p in line.split(",")]
        if len(parts) >= 3 and parts[2] in ("D", "L"):
            files.append((parts[0], parts[2]))
    return files, ""


def main():
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument("--backup-dir", default="./sqlbackup",
                    help="host dir with *.bak files (mounted at /sqlbackup)")
    ap.add_argument("--service", default="sqlserver",
                    help="compose service name of SQL Server")
    ap.add_argument("--data-dir", default=CONTAINER_DATA_DIR,
                    help="in-container data dir for MOVE targets")
    ap.add_argument("--compose-cmd", default=None,
                    help="compose command (default: $OPENHIS_COMPOSE_CMD "
                         "or 'docker compose')")
    args = ap.parse_args()

    pw = os.environ.get("MSSQL_SA_PASSWORD", "")
    if not pw:
        sys.exit("MSSQL_SA_PASSWORD is not set in the environment")
    compose_cmd = (args.compose_cmd or os.environ.get(
        "OPENHIS_COMPOSE_CMD") or "docker compose").split()
    if not os.path.isdir(args.backup_dir):
        sys.exit(f"backup dir not found: {args.backup_dir}")

    baks = sorted(f for f in os.listdir(args.backup_dir)
                  if f.lower().endswith(".bak"))
    if not baks:
        sys.exit(f"no *.bak files in {args.backup_dir}")

    have, err = existing_dbs(compose_cmd, args.service, pw)
    if have is None:
        sys.exit(f"cannot list databases: {err}")
    print(f"Databases already on server: {len(have)}")

    results = []
    for fn in baks:
        db = db_name_from_file(fn)
        if db in have:
            results.append((db, "SKIPPED (already exists)"))
            continue
        bak = f"{CONTAINER_BACKUP_DIR}/{fn}"
        files, err = file_list(compose_cmd, args.service, pw, bak)
        if files is None:
            results.append((db, f"FILELISTONLY FAILED: {err}"))
            continue
        moves = []
        for logical, typ in files:
            safe = re.sub(r"[^A-Za-z0-9_]", "_", logical)
            ext = "mdf" if typ == "D" else "ldf"
            moves.append(
                f"MOVE N'{logical}' TO N'{args.data_dir}/{db}_{safe}.{ext}'"
            )
        q = (f"RESTORE DATABASE [{db}] FROM DISK = N'{bak}' "
             f"WITH {', '.join(moves)}, REPLACE, STATS = 5")
        r = run_sqlcmd(compose_cmd, args.service, pw, q)
        ok = r.returncode == 0
        tail = (r.stdout + r.stderr).strip().splitlines()
        results.append((db, "OK" if ok else "FAILED: " + " | ".join(tail[-3:])))

    print("\n==== RESTORE SUMMARY ====")
    for db, status in results:
        print(f"{db}: {status}")
    failed = [d for d, s in results if s.startswith("FAILED")]
    if failed:
        print(f"\nFAILED DBs: {', '.join(failed)}")
        sys.exit(1)
    print("\nAll restores succeeded or were already present.")


if __name__ == "__main__":
    main()
