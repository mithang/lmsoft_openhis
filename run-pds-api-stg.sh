#!/bin/bash
# PDS API — STAGING variant for verification ONLY (NOT production).
# Port 15337 (prod 5337). Binary from staging-only worktree ~/workspace/openhis-stg
# (same pattern as the other run-*-stg.sh launchers; the worktree PDS.API
# Program.cs carries the 15337 UseUrls literal, and ASPNETCORE_URLS below
# repeats it explicitly). Loads PDS Web's appsettings.json (same deployment
# pattern as CIS API). DBConf order for PDS Web: 0 BaseDb, 1 UnionDb, 2 PdsDb,
# 3 CisDb — all pointed at *_STG clones. BaseAPI calls go to staging
# BaseAPI 17001, Redis to staging 6380, so a staging call can never proxy
# into a prod host that writes demo data. Binary is bin/Release (see the
# staging-build rule: always build Release for staging launchers).
source ~/workspace/.sqlserver.env
REDIS_STG_PASS="$(cat ~/workspace/.redis-stg.env)"
cd ~/workspace/openhis-stg/Newtouch.HIS.PDS/Newtouch.PDS.API || exit 1
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_ENVIRONMENT=Development \
  ASPNETCORE_URLS=http://127.0.0.1:15337 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  BusinessDB__DBConf__0__DBName=NewtouchHIS_Base_STG \
  BusinessDB__DBConf__1__DBName=Newtouch_Union_STG \
  BusinessDB__DBConf__2__DBName=NewtouchHIS_PDS_STG \
  BusinessDB__DBConf__3__DBName=Newtouch_CIS_STG \
  BusinessConfig__AppAPIHost__HisAppBaseAPIHost=http://127.0.0.1:17001/ \
  BusinessConfig__AppAPIHost__SiteBaseAPIHost=http://127.0.0.1:17001/ \
  RedisConfig__Connection="127.0.0.1:6380,password=$REDIS_STG_PASS" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.PDS.API.dll
