#!/bin/bash
# Base HOSP web on 5238 (proxy env MUST be unset for server-side loopback calls;
# DOTNET_SYSTEM_NET_DISABLEIPV6=true keeps dotnet loopback off the IPv6-mapped
# path that the sandbox routes to its denial proxy -- proven by the MRMS agent.
# SA password via env override (never printed, never written to appsettings).)
source ~/workspace/.sqlserver.env
source ~/workspace/.redis.env
cd ~/workspace/openhis-core/Newtouch.HIS.Base/Newtouch.HIS.Base.Web
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS=http://127.0.0.1:5238 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD" \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.HIS.Base.Web.dll >> ~/workspace/openhis-core/base-web.log 2>&1
