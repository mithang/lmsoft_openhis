#!/bin/bash
# HIS.BaseAPI (ported .NET 6, openhis-core copy) on 127.0.0.1:7001.
# Cut over from the run-copy instance 2026-10-27 (recovery.sh section 5 uses this).
# Proxy env UNSET + DOTNET_SYSTEM_NET_DISABLEIPV6=true are MANDATORY for
# loopback calls; ASPNETCORE_URLS is explicit because DLL-direct launches
# ignore launchSettings.
# IMPORTANT: appsettings.json BusinessDB.DefaultConn carries a STALE SA password
# (proven 2026-10-27: instance without override fails CheckPwd with "Login failed
# for user 'sa'"; with this override it returns 10000 SUCCESS). The connection
# string therefore comes from ~/.sqlserver.env at launch time only (same pattern
# as run-sso.sh) and is NEVER written into appsettings.json or this script.
# Top_OrganizeId (demo org) comes from appsettings.json and is correct.
source ~/workspace/.sqlserver.env
cd "$(dirname "$0")/Newtouch.HIS.SSO/HIS.BaseAPI" || exit 1
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:7001 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  ~/.dotnet/dotnet bin/Release/net6.0/HIS.BaseAPI.dll
