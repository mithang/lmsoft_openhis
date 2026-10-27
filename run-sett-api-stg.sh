#!/bin/bash
# Sett-API host — STAGING variant for verification ONLY (NOT for production).
# Identical to run-sett-api.sh except SettApi:SettDb is overridden to
# NewtouchHIS_Sett_STG, so every write lands in the staging DB and the demo
# Sett DB is never touched. 8016 keeps its demo config (it only proxies).
# Switch to production by stopping this instance and starting run-sett-api.sh.
source ~/workspace/.sqlserver.env
cd "$(dirname "$0")/Newtouch.HIS.Sett/Newtouch.Sett.API" || exit 1
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:7002 \
  SettApi__SettDb=NewtouchHIS_Sett_STG \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Sett.API.dll
