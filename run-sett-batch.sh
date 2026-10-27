#!/bin/bash
# Run the full port pipeline for each Area in order; stop on first failure.
# Usage: ./run-sett-batch.sh Area1 Area2 ...
cd /home/hatch/workspace/openhis-core || exit 1
for AREA in "$@"; do
  echo "===== $AREA ====="
  python3 port-sett-area.py "$AREA" || exit 1
  python3 gen-sett-services.py "$AREA" || exit 1
  python3 gen-sett-types.py "$AREA" || exit 1
  python3 port-sett-enums.py "$AREA" || exit 1
  python3 translate-sett-area.py "$AREA" || exit 1
done
python3 register-sett-di.py || exit 1
echo "BATCH DONE"
