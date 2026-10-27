#!/bin/bash
# Clone demo DBs -> *_STG for Stage-6 write verification (Option C).
# NEVER restores over a demo DB: target name is always ${DB}_STG, and the
# script aborts if a target already exists (except Sett_STG which pre-exists
# and is skipped). No password is echoed: it comes from ~/.sqlserver.env only.
# Usage: bash clone-stg.sh [DB...]   (default: all demo DBs except Sett)
set -u
source ~/workspace/.sqlserver.env
export LD_LIBRARY_PATH=~/workspace/mssql-tools/usr/lib/x86_64-linux-gnu:~/workspace/mssql-tools/opt/microsoft/msodbcsql18/lib64
export ODBCSYSINI=~/workspace/odbc
SQLCMD=~/workspace/mssql-tools/opt/mssql-tools18/bin/sqlcmd
BAKDIR=~/workspace/sqlbackup-stg
DATADIR=~/workspace/sqldata/data
mkdir -p "$BAKDIR"
DBS="${*:-NewtouchHIS_Base Newtouch_Union Newtouch_CIS Newtouch_EMR Newtouch_MRMS Newtouch_MRQC Newtouch_OR NewtouchHIS_herp NewtouchHIS_PDS Newtouch_Interface Newtouch_His_Log NewtouchHis_Job bdg}"
q() { "$SQLCMD" -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -b "$@"; }
for DB in $DBS; do
  STG="${DB}_STG"
  EXISTS=$(q -h -1 -W -Q "SET NOCOUNT ON; SELECT COUNT(*) FROM sys.databases WHERE name='$STG'" | tr -d ' \r')
  if [ "$EXISTS" = "1" ]; then echo "SKIP $STG already exists"; continue; fi
  echo "=== BACKUP $DB"
  q -Q "BACKUP DATABASE [$DB] TO DISK='$BAKDIR/$DB.bak' WITH COMPRESSION, INIT" || { echo "BACKUP-FAILED $DB"; continue; }
  echo "=== RESTORE $DB -> $STG"
  MOVES=""
  while IFS=',' read -r LOGICAL _ TYPE _; do
    LOGICAL=$(echo "$LOGICAL" | tr -d ' \r'); TYPE=$(echo "$TYPE" | tr -d ' \r')
    [ -z "$LOGICAL" ] && continue
    if [ "$TYPE" = "D" ]; then MOVES="$MOVES, MOVE '$LOGICAL' TO '$DATADIR/${STG}_${LOGICAL}.mdf'"
    elif [ "$TYPE" = "L" ]; then MOVES="$MOVES, MOVE '$LOGICAL' TO '$DATADIR/${STG}_${LOGICAL}.ldf'"; fi
  done < <(q -s "," -W -h -1 -Q "RESTORE FILELISTONLY FROM DISK='$BAKDIR/$DB.bak'" | grep -v '^$')
  MOVES="${MOVES#, }"
  q -Q "RESTORE DATABASE [$STG] FROM DISK='$BAKDIR/$DB.bak' WITH $MOVES" || { echo "RESTORE-FAILED $DB"; continue; }
  echo "OK $STG"
done
echo "=== FINAL STG LIST"
q -h -1 -W -Q "SET NOCOUNT ON; SELECT name FROM sys.databases WHERE name LIKE '%_STG' ORDER BY name"
