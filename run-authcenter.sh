#!/bin/bash
# AuthCenter host (already .NET 6) on 127.0.0.1:8015.
# Proxy env UNSET + DOTNET_SYSTEM_NET_DISABLEIPV6=true are MANDATORY:
# without them the app's loopback calls (BaseAPI/Redis) fail (proven with OR Web).
# ASPNETCORE_URLS is explicit because DLL-direct launches ignore launchSettings.
cd "$(dirname "$0")/Newtouch.HIS.Center/NewtouchHIS.AuthenticationCenter" || exit 1
# SA password comes from ~/.sqlserver.env (MSSQL_SA_PASSWORD) at launch time only;
# it is never written into this script or any appsettings file.
source ~/workspace/.sqlserver.env
source ~/workspace/.redis.env
source ~/workspace/.appsecrets.env
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:8015 \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD" \
  JWT__Key="$AUTHCENTER_JWT_KEY" \
  JWT__SSOAuthApp__0__AppSecret="$AUTHCENTER_APPSECRET_0" \
  JWT__SSOAuthApp__1__AppSecret="$AUTHCENTER_APPSECRET_1" \
  JWT__SSOAuthApp__2__AppSecret="$AUTHCENTER_APPSECRET_2" \
  JWT__SSOAuthApp__3__AppSecret="$AUTHCENTER_APPSECRET_3" \
  JWT__SSOAuthApp__4__AppSecret="$AUTHCENTER_APPSECRET_4" \
  JWT__SSOAuthApp__5__AppSecret="$AUTHCENTER_APPSECRET_5" \
  JWT__SSOAuthApp__6__AppSecret="$AUTHCENTER_APPSECRET_6" \
  JWT__SSOAuthApp__7__AppSecret="$AUTHCENTER_APPSECRET_7" \
  ~/.dotnet/dotnet bin/Release/net6.0/NewtouchHIS.AuthenticationCenter.dll
