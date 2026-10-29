#!/bin/bash
# EMR API — STAGING variant for verification ONLY (NOT production).
# Port 15335 (prod 5335). Binary from staging-only worktree ~/workspace/openhis-stg
# (Program.cs carries the 15335 UseUrls literal; ASPNETCORE_URLS below repeats
# it explicitly). DBConf (0 Base, 1 Sett, 2 Emr, 3 Mrms, 4 MrQc, 5 Cis, 6 Union)
# all point at *_STG clones, BaseAPI calls go to staging BaseAPI 17001, Redis
# to staging 6380, so a staging call can never touch a demo DB or session.
# Secrets come from env files at launch time only, never written here.
source ~/workspace/.sqlserver.env
REDIS_STG_PASS="$(cat ~/workspace/.redis-stg.env)"
cd ~/workspace/openhis-stg/Newtouch.HIS.EMR/Newtouch.EMR.API || exit 1
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_ENVIRONMENT=Development \
  ASPNETCORE_URLS=http://127.0.0.1:15335 \
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
  RedisConfig__Connection="127.0.0.1:6380,password=$REDIS_STG_PASS" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.EMR.API.dll
