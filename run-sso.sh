#!/bin/bash
# SSO portal (ported .NET 6, openhis-core copy) on *:5208.
# Proxy env UNSET + DOTNET_SYSTEM_NET_DISABLEIPV6=true are MANDATORY:
# without them the app's loopback calls (BaseAPI/Redis) fail (proven with OR Web).
# ASPNETCORE_URLS is explicit because DLL-direct launches ignore launchSettings.
cd "$(dirname "$0")/Newtouch.HIS.SSO/HIS.SSO" || exit 1
# SA password comes from ~/.sqlserver.env (MSSQL_SA_PASSWORD) at launch time only;
# it is never written into this script or any appsettings file.
source ~/workspace/.sqlserver.env
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://*:5208 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  ~/.dotnet/dotnet bin/Release/net6.0/HIS.SSO.dll
