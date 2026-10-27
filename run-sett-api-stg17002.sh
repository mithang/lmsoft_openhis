#!/bin/bash
# Sett-API host — STAGING variant for Stage-6 verification ONLY (NOT production).
# Port 17002 (prod is 7002; the pre-existing run-sett-api-stg.sh binds 7002 and
# must NOT run while prod holds it). Sett-API reads its port from SETT_API_URLS
# (Program.cs), so no staging rebuild is needed for this host.
# All three DBs point at *_STG clones (Sett/Base/Interface); secrets come from
# env files at launch time only, never written here.
source ~/workspace/.sqlserver.env
cd "$(dirname "$0")/Newtouch.HIS.Sett/Newtouch.Sett.API" || exit 1
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  SETT_API_URLS=http://127.0.0.1:17002 \
  SettApi__SettDb=NewtouchHIS_Sett_STG \
  SettApi__BaseDb=NewtouchHIS_Base_STG \
  SettApi__InterfaceDb=Newtouch_Interface_STG \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Sett.API.dll
