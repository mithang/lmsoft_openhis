#!/bin/bash
# Herp API host launcher (TEMP port 5336; final port 5336 after parent
# integration — change ASPNETCORE_URLS below and UseUrls in
# Newtouch.Herp.API/Program.cs to http://127.0.0.1:5336 then).
# Passwords are NOT in this file: SA password comes from
# ~/workspace/.sqlserver.env, Redis password from ~/workspace/.redis.env
# (both mode 600). Proxy env vars are unset so server-to-server loopback
# calls are not proxied.
set -e
cd ~/workspace/openhis-core/Newtouch.HIS.Herp/Newtouch.Herp.API
source ~/workspace/.sqlserver.env
source ~/workspace/.redis.env
export ASPNETCORE_ENVIRONMENT=Development
export DOTNET_SYSTEM_NET_DISABLEIPV6=true
export ASPNETCORE_URLS=http://127.0.0.1:5336
export BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD"
export RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD"
export ConnectionStrings__RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD"
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Herp.API.dll < /dev/null > ~/workspace/openhis-core/Newtouch.HIS.Herp/herp-api.log 2>&1
