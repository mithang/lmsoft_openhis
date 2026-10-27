#!/bin/bash
# MRQC pilot launcher: proxy env unset (loopback calls must not be proxied),
# SA password via env override (never printed, never written to appsettings).
source ~/workspace/.sqlserver.env
cd ~/workspace/openhis-core/Newtouch.MRQC.Web
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  ~/.dotnet/dotnet bin/Debug/net6.0/Newtouch.MRQC.Web.dll
