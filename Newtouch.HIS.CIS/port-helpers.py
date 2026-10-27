#!/usr/bin/env python3
"""Port CIS Infrastructure helpers (namespace Newtouch.Infrastructure preserved).
Skips hand-ported files: AuthenManageHelper, Constants, EFDBBaseFuncHelper."""
import io
import os
import re
import sys

SRC = os.path.expanduser("~/workspace/lmsoft_openhis_run/Newtouch.HIS.CIS/Newtouch.Infrastructure")
DST = os.path.expanduser("~/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/Infrastructure")
HAND = {"AuthenManageHelper.cs", "Constants.cs", "EFDBBaseFuncHelper.cs", "CommmHelper.cs", "QREncoder.cs"}

USING_MAP = [
    (r"using FrameworkBase\.MultiOrg\.Infrastructure;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Common\.Operator;", "using NewtouchHIS.Lib.Framework.Operator;\nusing NewtouchHIS.Lib.Base.Model;"),
    (r"using Newtouch\.Core\.Common\.Utils;", ""),
    (r"using Newtouch\.Core\.Common\.Exceptions;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Core\.Common;", "using NewtouchHIS.Lib.Base.Model;"),
    (r"using Newtouch\.Core\.Redis;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Tools\.Net;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Tools;", "using NewtouchHIS.Lib.Base.Extension;"),
    (r"using Newtouch\.Infrastructure\.Log;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Common\.Web;", "using Newtouch.CIS.Compat;\nusing Newtouch.Common.Web;"),
    (r"using Newtouch\.Infrastructure\.EF[^;]*;", ""),
    (r"using FrameworkBase[^;]*;", ""),
    (r"using System\.Configuration;", ""),
]

os.makedirs(DST, exist_ok=True)
n = 0
for fn in sorted(os.listdir(SRC)):
    if not fn.endswith(".cs") or fn in HAND:
        continue
    with io.open(os.path.join(SRC, fn), encoding="utf-8-sig") as fh:
        t = fh.read()
    for pat, rep in USING_MAP:
        t = re.sub(pat, rep, t)
    t = re.sub(r"\n{3,}", "\n\n", t)
    with io.open(os.path.join(DST, fn), "w", encoding="utf-8", newline="\n") as fh:
        fh.write(t)
    n += 1
print("helpers copied:", n)
