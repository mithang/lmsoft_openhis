#!/bin/bash
cd ~/workspace/openhis-core/sett-stage-pay/Newtouch.Sett.Web
source ~/workspace/.sqlserver.env
env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy ASPNETCORE_ENVIRONMENT=Development BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Sett.Web.dll
