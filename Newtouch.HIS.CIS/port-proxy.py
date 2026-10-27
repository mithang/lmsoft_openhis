#!/usr/bin/env python3
"""Port CIS Proxy project (CMM telemedicine platform integration).
Copies: CMMPlatform DTO tree, Dapper builders, CMMProxyCenter, proxy base,
Tools, Extend. WCF generated-client call sites are stubbed afterwards by
hand (see port notes); HTTP (WebClient) call sites are converted to
HttpMethods.HttpGet. Skips: Connected Services (WCF generated code),
Properties."""
import io
import os
import re

SRC = os.path.expanduser("~/workspace/lmsoft_openhis_run/Newtouch.HIS.CIS/Newtouch.CIS.Proxy")
DST = os.path.expanduser("~/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/Proxy")

USING_MAP = [
    (r"using FrameworkBase[^;]*;", ""),
    (r"using Newtouch\.Common\.Operator;", "using NewtouchHIS.Lib.Framework.Operator;\nusing NewtouchHIS.Lib.Base.Model;"),
    (r"using Newtouch\.Core\.Common\.Utils;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Core\.Common\.Exceptions;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Core\.Common;", "using NewtouchHIS.Lib.Base.Model;"),
    (r"using Newtouch\.Core\.Redis;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Tools\.Net;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Tools;", "using NewtouchHIS.Lib.Base.Extension;"),
    (r"using Newtouch\.Infrastructure\.Log;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Common\.Web;", "using Newtouch.CIS.Compat;\nusing Newtouch.Common.Web;"),
    (r"using Newtouch\.Infrastructure\.EF;", "using Newtouch.CIS.Repository;"),
]


def transform(text):
    for pat, rep in USING_MAP:
        text = re.sub(pat, rep, text)
    text = text.replace(
        "Core.Common.Utils.ConfigurationHelper.GetAppConfigValue(",
        "Newtouch.CIS.Compat.AppConfig.Get(")
    if "using Newtouch.CIS.Compat;" not in text:
        text = re.sub(r"(using System[^\n]*\n)", r"\1using Newtouch.CIS.Compat;\n", text, count=1)
        if "using Newtouch.CIS.Compat;" not in text:
            text = "using Newtouch.CIS.Compat;\n" + text
    text = re.sub(r"\n{3,}", "\n\n", text)
    return text


count = 0
for root, dirs, files in os.walk(SRC):
    dirs[:] = [d for d in dirs if d not in ("Properties", "Connected Services", "obj", "bin")]
    for fn in files:
        if not fn.endswith(".cs"):
            continue
        src_path = os.path.join(root, fn)
        rel = os.path.relpath(src_path, SRC)
        dst_path = os.path.join(DST, rel)
        os.makedirs(os.path.dirname(dst_path), exist_ok=True)
        with io.open(src_path, encoding="utf-8-sig") as fh:
            text = fh.read()
        with io.open(dst_path, "w", encoding="utf-8", newline="\n") as fh:
            fh.write(transform(text))
        count += 1
print("proxy files copied:", count)
