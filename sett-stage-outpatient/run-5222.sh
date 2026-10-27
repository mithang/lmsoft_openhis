#!/bin/bash
# Staging launcher for Sett OutpatientManage (port 5222). Run in background:
#   ./run-5222.sh & echo $! > sett-outpatient.pid   ($! IS the dotnet PID: exec replaces this shell)
cd "$(dirname "$0")/Newtouch.Sett.Web" || exit 1
source ~/workspace/.sqlserver.env
unset http_proxy https_proxy HTTP_PROXY HTTPS_PROXY ALL_PROXY all_proxy
export ASPNETCORE_ENVIRONMENT=Development
export BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD"
exec ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Sett.Web.dll
