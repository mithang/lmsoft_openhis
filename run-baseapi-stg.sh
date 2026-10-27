#!/bin/bash
# HIS.BaseAPI — STAGING variant for Stage-6 verification ONLY (NOT production).
# Port 17001. Every DBConf entry is overridden to its *_STG clone and Redis to
# the staging instance on 6380, so no staging request can touch a demo DB or
# a demo session. Secrets come from env files at launch time only and are
# NEVER written into this script or appsettings.json.
# DBConf index order (BaseAPI appsettings): 0 BaseDb, 1 SettDb, 2 EmrDb,
# 3 MrmsDb, 4 MrQcDb, 5 CisDb, 6 UnionDb.
source ~/workspace/.sqlserver.env
source ~/workspace/.redis.env
source ~/workspace/.appsecrets.env
REDIS_STG_PASS="$(cat ~/workspace/.redis-stg.env)"
cd "$(dirname "$0")/Newtouch.HIS.SSO/HIS.BaseAPI" || exit 1
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:17001 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  BusinessDB__DBConf__0__DBName=NewtouchHIS_Base_STG \
  BusinessDB__DBConf__1__DBName=NewtouchHIS_Sett_STG \
  BusinessDB__DBConf__2__DBName=Newtouch_EMR_STG \
  BusinessDB__DBConf__3__DBName=Newtouch_MRMS_STG \
  BusinessDB__DBConf__4__DBName=Newtouch_MRQC_STG \
  BusinessDB__DBConf__5__DBName=Newtouch_CIS_STG \
  BusinessDB__DBConf__6__DBName=Newtouch_Union_STG \
  RedisConfig__Connection="127.0.0.1:6380,password=$REDIS_STG_PASS" \
  SSOAuthIdentity__0__AppSecret="$BASEAPI_APPSECRET" \
  ~/.dotnet/dotnet bin/Release/net6.0/HIS.BaseAPI.dll
