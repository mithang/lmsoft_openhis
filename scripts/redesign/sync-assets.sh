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
# U7-002: portal.css cũng sync sang CIS — /Home/Index của CIS là trang
# Layout = null, nạp portal.css trực tiếp (mục body.cis-home trong file
# này chỉ nhắm trang đó nên không ảnh hưởng portal khác). Các portal
# còn lại (U7-003+) thêm vào danh sách này khi tới lượt.
# U7-003: portal.css cũng sync sang EMR — /Home/Index của EMR là shell
# AdminLTE cùng markup với SSO (2 div #nav-col, .login-foot, #sidebar-nav
# do $.LoadNavTree() dựng) nên 2 rule chung U7-001 (sidebar padding 6px /
# min-height 40px + footer height:auto) áp dụng luôn cho EMR; mục
# body[data-appid="EMR"] trong file này chỉ chứa rule riêng EMR.
# U7-004: portal.css cũng sync sang Herp — /Home/Index của Herp là trang
# Layout = null độc lập, nạp portal.css trực tiếp (mục
# body[data-appid="Herp"] trong file này chỉ nhắm trang đó nên không ảnh
# hưởng portal khác).
# U7-005: portal.css cũng sync sang MRMS — /Home/Index của MRMS là shell
# AdminLTE cùng markup với SSO/EMR (#page-wrapper, #sidebar-nav do
# $.LoadNavTree() dựng) nên 2 rule chung U7-001 (sidebar padding 6px /
# min-height 40px + footer height:auto) áp dụng luôn cho MRMS; mục
# body[data-appid="MR"] trong file này chỉ chứa rule riêng MRMS
# (AppId là "MR", không phải "MRMS"). Dashboard trong iframe /Home/Default
# KHÔNG dùng file này (xem Default.cshtml).
# U7-006: portal.css cũng sync sang OR — /Home/Index của OR là trang
# Layout = null độc lập, nạp portal.css trực tiếp (mục
# body[data-appid="OR"] trong file này chỉ nhắm trang đó nên không ảnh
# hưởng portal khác).
# U7-007: portal.css cũng sync sang PDS — /Home/Index của PDS là shell
# AdminLTE cùng markup với SSO/EMR/MRMS (#page-wrapper, #sidebar-nav do
# $.LoadNavTree() dựng, .login-foot) nên 2 rule chung U7-001 (sidebar
# padding 6px / min-height 40px + footer height:auto) áp dụng luôn cho
# PDS; mục body[data-appid="PDS"] trong file này chỉ chứa rule riêng PDS
# (sidebar span 2 dòng, username 110px). Dashboard trong iframe
# /Home/Default KHÔNG dùng file này (xem Default.cshtml).
# U7-008: portal.css cũng sync sang MRQC — /Home/Index của MRQC là trang
# Layout = null độc lập, nạp portal.css trực tiếp (mục
# body[data-appid="MRQC"] trong file này chỉ nhắm trang đó nên không ảnh
# hưởng portal khác).
# U7-009: portal.css cũng sync sang Sett — /Home/Index của Sett là trang
# Layout = null độc lập, nạp portal.css trực tiếp (mục
# body[data-appid="Sett"] trong file này chỉ nhắm trang đó nên không ảnh
# hưởng portal khác).
# U7-010: portal.css cũng sync sang Base Web — /Home/Index của Base Web là
# shell AdminLTE cùng markup với SSO/EMR/MRMS/PDS (#page-wrapper,
# #sidebar-nav) nên 2 rule chung U7-001 (sidebar padding 6px /
# min-height 40px + .login-foot height:auto) áp dụng luôn cho Base Web;
# mục body[data-appid="BASE.BASE"] trong file này chỉ chứa rule riêng
# Base Web (watermark logo-03.png, username). Dashboard trong iframe
# /Home/Default KHÔNG dùng file này (xem Default.cshtml).
PORTAL_CSS_APPS=(
  "Newtouch.HIS.CIS/Newtouch.CIS.Web"
  "Newtouch.HIS.EMR/Newtouch.EMR.Web"
  "Newtouch.HIS.Herp/Newtouch.Herp.Web"
  "Newtouch.HIS.MRMS/Newtouch.MRMS.Web"
  "Newtouch.HIS.OR/Newtouch.OR.Web"
  "Newtouch.HIS.PDS/Newtouch.PDS.Web"
  "Newtouch.MRQC.Web"
  "Newtouch.Sett.Web"
  "Newtouch.HIS.Base/Newtouch.HIS.Base.Web"
)
for app in "${PORTAL_CSS_APPS[@]}"; do
  dest="$ROOT/$app/wwwroot"
  [ -d "$ROOT/$app" ] || { echo "SKIP (không thấy project): $app"; continue; }
  copy_one "$SRC/redesign-portal.css" "$dest/css/redesign-portal.css"
done
echo "DONE. Nguồn chuẩn: $SRC (sửa ở đó, đừng sửa wwwroot tay)."
