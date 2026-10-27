#!/bin/bash
# Sett-API host (ported .NET 6, openhis-core) on 127.0.0.1:7002.
# Serves ONLY the 3 route groups that WebAPI.Manage (8016) proxies to
# SiteSettAPIHost=http://localhost:7002/: api/OutpBook/BookingESB,
# api/PatientAnon/*, api/OrderCenter/*.
# Proxy env UNSET + DOTNET_SYSTEM_NET_DISABLEIPV6=true are MANDATORY for
# loopback calls; ASPNETCORE_URLS is explicit because DLL-direct launches
# ignore launchSettings.
# appsettings.json carries a PLACEHOLDER password only. The live connection
# string comes from ~/.sqlserver.env at launch time (same pattern as
# run-sso.sh / run-his-baseapi.sh) and is NEVER written into appsettings.json
# or this script. {0} is the Initial Catalog slot filled from
# SettApi:SettDb in appsettings.json (NewtouchHIS_Sett for prod).
source ~/workspace/.sqlserver.env
cd "$(dirname "$0")/Newtouch.HIS.Sett/Newtouch.Sett.API" || exit 1
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:7002 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Sett.API.dll
