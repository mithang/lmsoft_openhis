#!/bin/bash
# Read-only-or-staging SQL helper. Usage: sql.sh <db> "<query>"
# Never run a write query against a non-*_STG database with this helper
# unless the query is an explicit demo COUNT (read-only).
source ~/workspace/.sqlserver.env
export LD_LIBRARY_PATH=~/workspace/mssql-tools/usr/lib/x86_64-linux-gnu:~/workspace/mssql-tools/opt/microsoft/msodbcsql18/lib64
export ODBCSYSINI=~/workspace/odbc
exec ~/workspace/mssql-tools/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -b -h -1 -W -d "$1" -Q "$2"
