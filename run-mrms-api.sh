#!/bin/bash
# MRMS.API host (Scope B) on 127.0.0.1:5332.
# Reuses MRMS Web appsettings.json for BusinessDB/RedisConfig (same deployment,
# same trust boundary) so no credential is duplicated into a new file.
# Proxy env UNSET + DOTNET_SYSTEM_NET_DISABLEIPV6=true are MANDATORY:
# without them the app's loopback calls (BaseAPI/Redis) fail (proven with OR Web).
# ASPNETCORE_URLS is explicit because DLL-direct launches ignore launchSettings.
cd "$(dirname "$0")/Newtouch.HIS.MRMS/Newtouch.MRMS.API" || exit 1
# SA password comes from ~/.sqlserver.env (MSSQL_SA_PASSWORD) at launch time only;
# it is never written into this script or any appsettings file.
source ~/workspace/.sqlserver.env
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:5332 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.MRMS.API.dll
