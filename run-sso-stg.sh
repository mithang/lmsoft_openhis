#!/bin/bash
# SSO portal — STAGING variant for Stage-6 verification ONLY (NOT production).
# Port 15208 (prod 5208). Binary from staging-only worktree ~/workspace/openhis-stg.
# SSO has no hardcoded UseUrls, so ASPNETCORE_URLS selects the staging port.
# DBConf (Base/Sett/Emr/Mrms) all point at *_STG clones, BaseAPI calls go to
# staging BaseAPI 17001, Redis to staging 6380.
source ~/workspace/.sqlserver.env
REDIS_STG_PASS="$(cat ~/workspace/.redis-stg.env)"
cd ~/workspace/openhis-stg/Newtouch.HIS.SSO/HIS.SSO || exit 1
exec env -u http_proxy -u https_proxy -u HTTPS_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:15208 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  BusinessDB__DBConf__0__DBName=NewtouchHIS_Base_STG \
  BusinessDB__DBConf__1__DBName=NewtouchHIS_Sett_STG \
  BusinessDB__DBConf__2__DBName=Newtouch_EMR_STG \
  BusinessDB__DBConf__3__DBName=Newtouch_MRMS_STG \
  BusinessConfig__AppAPIHost__HisAppBaseAPIHost=http://127.0.0.1:17001/ \
  BusinessConfig__AppAPIHost__SiteBaseAPIHost=http://127.0.0.1:17001/ \
  BusinessConfig__AppAPIHost__SiteApiManageHost=http://127.0.0.1:18016/ \
  BusinessConfig__AppAPIHost__SiteAuthCenterHost=http://127.0.0.1:18015/ \
  RedisConfig__Connection="127.0.0.1:6380,password=$REDIS_STG_PASS" \
  ~/.dotnet/dotnet bin/Release/net6.0/HIS.SSO.dll
