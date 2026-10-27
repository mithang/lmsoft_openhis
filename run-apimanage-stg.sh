#!/bin/bash
# WebAPI.Manage (8016) — STAGING variant for Stage-6 verification ONLY (NOT production).
# Port 18016 (prod 8016). No rebuild needed (no hardcoded UseUrls; ASPNETCORE_URLS
# applies). DBConf (0 Base, 1 Sett, 2 Emr, 3 Mrms, 4 MrQc, 5 Cis, 6 Interface,
# 7 Pds) all point at *_STG clones; SiteSettAPIHost points at Sett-API staging
# 17002 (never prod 7002, which writes demo); Redis to staging 6380.
source ~/workspace/.sqlserver.env
REDIS_STG_PASS="$(cat ~/workspace/.redis-stg.env)"
cd "$(dirname "$0")/Newtouch.HIS.Center/NewtouchHIS.WebAPI.Manage" || exit 1
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:18016 \
  BusinessConfig__AppAPIHost__SiteSettAPIHost=http://127.0.0.1:17002/ \
  BusinessConfig__AppAPIHost__SiteBaseAPIHost=http://127.0.0.1:17001/ \
  BusinessConfig__AppAPIHost__SiteAuthCenterHost=http://127.0.0.1:18015/ \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  BusinessDB__DBConf__0__DBName=NewtouchHIS_Base_STG \
  BusinessDB__DBConf__1__DBName=NewtouchHIS_Sett_STG \
  BusinessDB__DBConf__2__DBName=Newtouch_EMR_STG \
  BusinessDB__DBConf__3__DBName=Newtouch_MRMS_STG \
  BusinessDB__DBConf__4__DBName=Newtouch_MRQC_STG \
  BusinessDB__DBConf__5__DBName=Newtouch_CIS_STG \
  BusinessDB__DBConf__6__DBName=Newtouch_Interface_STG \
  BusinessDB__DBConf__7__DBName=NewtouchHIS_PDS_STG \
  RedisConfig__Connection="127.0.0.1:6380,password=$REDIS_STG_PASS" \
  ~/.dotnet/dotnet bin/Release/net6.0/NewtouchHIS.WebAPI.Manage.dll
