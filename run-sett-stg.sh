#!/bin/bash
# Sett merged web — STAGING variant for Stage-6 verification ONLY (NOT production).
# Port 15221 (prod is 5221). All DBConf entries point at *_STG clones, BaseAPI
# calls go to the staging BaseAPI on 17001, Redis to staging 6380.
# SiteApiManageHost/SiteAuthCenterHost point at staging ports 18016/18015 so a
# staging call can never proxy into a prod host that writes demo data; those
# staging hosts are started only for the cross-app tests that need them.
# Secrets come from env files at launch time only, never written here.
source ~/workspace/.sqlserver.env
REDIS_STG_PASS="$(cat ~/workspace/.redis-stg.env)"
# Staging binary comes from the staging-only worktree ~/workspace/openhis-stg
# (same commit 0a730b1b, only the UseUrls port literal changed 5221 -> 15221,
# because the prod binary hardcodes 5221 and UseUrls beats env/CLI overrides).
# Main checkout code and prod binaries are untouched.
cd ~/workspace/openhis-stg/Newtouch.Sett.Web || exit 1
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS=http://127.0.0.1:15221 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  BusinessDB__DBConf__0__DBName=NewtouchHIS_Base_STG \
  BusinessDB__DBConf__1__DBName=NewtouchHIS_Sett_STG \
  BusinessDB__DBConf__2__DBName=Newtouch_EMR_STG \
  BusinessDB__DBConf__3__DBName=Newtouch_MRMS_STG \
  BusinessDB__DBConf__4__DBName=Newtouch_MRQC_STG \
  BusinessDB__DBConf__5__DBName=Newtouch_CIS_STG \
  BusinessDB__DBConf__6__DBName=Newtouch_Union_STG \
  BusinessConfig__AppAPIHost__HisAppBaseAPIHost=http://127.0.0.1:17001/ \
  BusinessConfig__AppAPIHost__SiteBaseAPIHost=http://127.0.0.1:17001/ \
  BusinessConfig__AppAPIHost__SiteApiManageHost=http://127.0.0.1:18016/ \
  BusinessConfig__AppAPIHost__SiteAuthCenterHost=http://127.0.0.1:18015/ \
  RedisConfig__Connection="127.0.0.1:6380,password=$REDIS_STG_PASS" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Sett.Web.dll
