#!/bin/bash
# sync-assets.sh — Đồng bộ file nền tảng redesign từ nguồn chuẩn vào wwwroot.
# Nguồn chuẩn DUY NHẤT: scripts/redesign/assets/ (đi theo repo openhis-core).
# Đích: wwwroot/css|js/ của 10 app nghiệp vụ + SSO portal.
# CẢNH BÁO: file đích là bản sao đã sinh — sửa tay trong wwwroot sẽ bị
# ghi đè ở lần sync sau. Muốn đổi, sửa trong assets/ rồi chạy lại script.
# Script này KHÔNG chạm static host 127.0.0.1:41142 (xem ASSET-PIPELINE.md
# §4: static host là run-copy, repo riêng, sửa ở đó đổi cả prod ngay).
# Cách dùng: bash scripts/redesign/sync-assets.sh [--dry-run]
set -euo pipefail
ROOT="$(cd "$(dirname "$0")/../.." && pwd)"
SRC="$ROOT/scripts/redesign/assets"
DRY=0
[ "${1:-}" = "--dry-run" ] && DRY=1

# project web dir (tương đối từ root) — 10 app nghiệp vụ + SSO
APPS=(
  "Newtouch.MRQC.Web"
  "Newtouch.Sett.Web"
  "Newtouch.HIS.MRMS/Newtouch.MRMS.Web"
  "Newtouch.HIS.OR/Newtouch.OR.Web"
  "Newtouch.HIS.Herp/Newtouch.Herp.Web"
  "Newtouch.HIS.EMR/Newtouch.EMR.Web"
  "Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.Web"
  "Newtouch.HIS.Base/Newtouch.HIS.Base.Web"
  "Newtouch.HIS.CIS/Newtouch.CIS.Web"
  "Newtouch.HIS.PDS/Newtouch.PDS.Web"
)
SSO="Newtouch.HIS.SSO/HIS.SSO"

copy_one() { # $1=src $2=dst
  if [ "$DRY" = 1 ]; then echo "DRY: $1 -> $2"; return; fi
  mkdir -p "$(dirname "$2")"
  cp "$1" "$2"
  echo "OK: $2 ($(md5sum "$2" | cut -d' ' -f1))"
}

for app in "${APPS[@]}"; do
  dest="$ROOT/$app/wwwroot"
  [ -d "$ROOT/$app" ] || { echo "SKIP (không thấy project): $app"; continue; }
  copy_one "$SRC/redesign-tokens.css" "$dest/css/redesign-tokens.css"
  copy_one "$SRC/redesign-grid.js" "$dest/js/redesign-grid.js"
done
# SSO: tokens + grid cho _Index, portal css/js cho SSOIndex
dest="$ROOT/$SSO/wwwroot"
copy_one "$SRC/redesign-tokens.css" "$dest/css/redesign-tokens.css"
copy_one "$SRC/redesign-grid.js" "$dest/js/redesign-grid.js"
copy_one "$SRC/redesign-portal.css" "$dest/css/redesign-portal.css"
copy_one "$SRC/redesign-portal.js" "$dest/js/redesign-portal.js"
echo "DONE. Nguồn chuẩn: $SRC (sửa ở đó, đừng sửa wwwroot tay)."
