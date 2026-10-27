#!/bin/bash
# PDS.API host (.NET 6 port) on 127.0.0.1:5337.
# Reuses PDS Web appsettings.json for BusinessDB/RedisConfig (same deployment,
# same trust boundary) so no credential is duplicated into a new file.
# Proxy env UNSET + DOTNET_SYSTEM_NET_DISABLEIPV6=true are MANDATORY:
# without them the app's loopback calls (BaseAPI/Redis) fail (proven with OR Web).
# ASPNETCORE_URLS is explicit because DLL-direct launches ignore launchSettings.
cd "$(dirname "$0")/Newtouch.HIS.PDS/Newtouch.PDS.API" || exit 1
# SA password comes from ~/.sqlserver.env (MSSQL_SA_PASSWORD) at launch time only;
# it is never written into this script or any appsettings file.
source ~/workspace/.sqlserver.env
source ~/workspace/.redis.env
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:5337 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.PDS.API.dll
