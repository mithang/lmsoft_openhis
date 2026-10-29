#!/bin/bash
# Sett-API host — STAGING variant for verification ONLY (NOT for production).
# Port 17002 (NEVER the prod Sett-API port). Serves the 3 route
# groups WebAPI.Manage proxies to SiteSettAPIHost: api/OutpBook/BookingESB,
# api/PatientAnon/*, api/OrderCenter/* (tokenless by design, read probes only
# unless a write test is explicitly ordered).
# All DBConf entries point at *_STG clones, SettApi DB names at *_STG,
# BaseAPI calls go to staging BaseAPI 17001, Redis to staging 6380, so a
# staging call can never touch a demo DB or a demo session.
# Secrets come from env files at launch time only, never written here.
source ~/workspace/.sqlserver.env
REDIS_STG_PASS="$(cat ~/workspace/.redis-stg.env)"
# Binary staging lấy từ worktree staging mới ~/workspace/openhis-stg2 (build từ
# fcfb6c17, xem REPORT-FIX) — KHÔNG chạy từ cây chính vì bin đó dùng chung với
# Sett-API prod; đổi cd này để restage không bao giờ chạm bin prod.
cd ~/workspace/openhis-stg2/Newtouch.HIS.Sett/Newtouch.Sett.API || exit 1
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:17002 \
  SETT_API_URLS=http://127.0.0.1:17002 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  BusinessDB__DBConf__0__DBName=NewtouchHIS_Base_STG \
  BusinessDB__DBConf__1__DBName=NewtouchHIS_Sett_STG \
  BusinessDB__DBConf__2__DBName=Newtouch_EMR_STG \
  BusinessDB__DBConf__3__DBName=Newtouch_MRMS_STG \
  BusinessDB__DBConf__4__DBName=Newtouch_MRQC_STG \
  BusinessDB__DBConf__5__DBName=Newtouch_CIS_STG \
  BusinessDB__DBConf__6__DBName=Newtouch_Union_STG \
  SettApi__SettDb=NewtouchHIS_Sett_STG \
  SettApi__BaseDb=NewtouchHIS_Base_STG \
  SettApi__InterfaceDb=Newtouch_Interface_STG \
  BusinessConfig__AppAPIHost__HisAppBaseAPIHost=http://127.0.0.1:17001/ \
  BusinessConfig__AppAPIHost__SiteBaseAPIHost=http://127.0.0.1:17001/ \
  BusinessConfig__AppAPIHost__SiteApiManageHost=http://127.0.0.1:18016/ \
  BusinessConfig__AppAPIHost__SiteAuthCenterHost=http://127.0.0.1:18015/ \
  RedisConfig__Connection="127.0.0.1:6380,password=$REDIS_STG_PASS" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Sett.API.dll
