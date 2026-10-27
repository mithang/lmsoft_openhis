#!/bin/bash
# EMR.API host launcher: port 5335, proxy env unset (loopback calls must not be proxied),
# SA password via env override (never printed, never written to appsettings).
source ~/workspace/.sqlserver.env
source ~/workspace/.redis.env
cd ~/workspace/openhis-core/Newtouch.HIS.EMR/Newtouch.EMR.API
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.EMR.API.dll
