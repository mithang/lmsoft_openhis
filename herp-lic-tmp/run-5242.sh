#!/bin/bash
# Herp web app launcher (port 5242). Passwords are NOT in this file:
# SA password comes from ~/workspace/.sqlserver.env, Redis password from
# ~/workspace/.redis.env (both mode 600). Proxy env vars are unset so
# server-to-server loopback calls (BaseAPI on 7001) are not proxied.
set -e
cd ~/workspace/openhis-core/herp-lic-tmp/Newtouch.Herp.Web
source ~/workspace/.sqlserver.env
source ~/workspace/.redis.env
export ASPNETCORE_ENVIRONMENT=Development
export ASPNETCORE_URLS=http://127.0.0.1:5242
export BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD"
export RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD"
export ConnectionStrings__RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD"
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Herp.Web.dll < /dev/null > ~/workspace/openhis-core/herp-lic-tmp/herp-lic.log 2>&1
