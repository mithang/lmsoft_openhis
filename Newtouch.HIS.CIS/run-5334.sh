#!/bin/bash
# CIS.API .NET 6 launcher: proxy env unset (loopback calls must not be proxied).
# SA password comes from ~/workspace/.sqlserver.env via $MSSQL_SA_PASSWORD
# (never written literally into this file or appsettings).
source ~/workspace/.sqlserver.env
source ~/workspace/.redis.env
cd ~/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.API
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  ASPNETCORE_URLS="http://127.0.0.1:5334" \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD" \
  ~/.dotnet/dotnet bin/Debug/net6.0/Newtouch.CIS.API.dll
