#!/bin/bash
# WebAPI.Manage host (already .NET 6) on 127.0.0.1:8016.
# Proxy env UNSET + DOTNET_SYSTEM_NET_DISABLEIPV6=true are MANDATORY:
# without them the app's loopback calls (BaseAPI/Redis) fail (proven with OR Web).
# ASPNETCORE_URLS is explicit because DLL-direct launches ignore launchSettings.
cd "$(dirname "$0")/Newtouch.HIS.Center/NewtouchHIS.WebAPI.Manage" || exit 1
# SA password comes from ~/.sqlserver.env (MSSQL_SA_PASSWORD) at launch time only;
# it is never written into this script or any appsettings file.
source ~/workspace/.sqlserver.env
source ~/workspace/.redis.env
source ~/workspace/.appsecrets.env
# Sett host override (giai doan 3, 2026-10-27): 8016 chay Development nen
# mac dinh SiteSettAPIHost = localhost:17361 (khong co host) — chi override
# rieng pointer nay sang host that 7002, KHONG doi ENVIRONMENT (Development
# con mang Top_OrganizeId/host khac voi Production, doi ca moi truong se
# anh huong nhieu cau hinh khac cua center).
exec env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy \
  ASPNETCORE_ENVIRONMENT=Development \
  DOTNET_SYSTEM_NET_DISABLEIPV6=true \
  ASPNETCORE_URLS=http://127.0.0.1:8016 \
  BusinessConfig__AppAPIHost__SiteSettAPIHost=http://localhost:7002/ \
  BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" \
  RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD" \
  SSOAuthIdentity__0__AppSecret="$MANAGE_APPSECRET" \
  ~/.dotnet/dotnet bin/Release/net6.0/NewtouchHIS.WebAPI.Manage.dll
