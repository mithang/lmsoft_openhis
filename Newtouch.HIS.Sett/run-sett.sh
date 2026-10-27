#!/bin/bash
# Merged Sett Phase 1 app (OutpatientManage + HospitalizationManage + PayManage) on 127.0.0.1:5221.
# Pattern: cd into Web dir first (content root), proxy env UNSET, SA password via env override only.
cd "$(dirname "$0")/../Newtouch.Sett.Web" || exit 1
source ~/workspace/.sqlserver.env
source ~/workspace/.redis.env
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS=http://127.0.0.1:5221 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Sett.Web.dll
