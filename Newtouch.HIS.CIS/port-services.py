#!/usr/bin/env python3
"""Port CIS DomainServices + Repository + Application layers.
Skips: DmnServiceBase.cs (hand-written stub), Web References (generated
SOAP reference), Properties/AssemblyInfo.
Transforms: FrameworkBase usings -> Core equivalents; ctor factory params
dropped; every file gains `using Newtouch.CIS.Compat;` (ICache,
EntityExtensions, SqlParam helpers)."""
import io
import os
import re

SRC = os.path.expanduser("~/workspace/lmsoft_openhis_run/Newtouch.HIS.CIS")
DST = os.path.expanduser("~/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure")

USING_MAP = [
    (r"using FrameworkBase\.MultiOrg\.Repository;", "using Newtouch.CIS.Repository;"),
    (r"using FrameworkBase\.MultiOrg\.Domain\.Entity;", "using Newtouch.Domain.Entity;"),
    (r"using FrameworkBase\.MultiOrg\.Domain\.IRepository;", "using Newtouch.CIS.Repository;"),
    (r"using FrameworkBase\.MultiOrg\.Infrastructure;", ""),
    (r"using FrameworkBase\.MultiOrg\.Application;", ""),
    (r"using FrameworkBase[^;]*;", ""),
    (r"using Newtouch\.Common\.Operator;", "using NewtouchHIS.Lib.Framework.Operator;\nusing NewtouchHIS.Lib.Base.Model;"),
    (r"using Newtouch\.Core\.Common\.Utils;", ""),
    (r"using Newtouch\.Core\.Common\.Exceptions;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Core\.Common;", "using NewtouchHIS.Lib.Base.Model;"),
    (r"using Newtouch\.Core\.Redis;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Tools\.Net;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Tools;", "using NewtouchHIS.Lib.Base.Extension;"),
    (r"using Newtouch\.Infrastructure\.Log;", "using Newtouch.CIS.Compat;"),
    (r"using Newtouch\.Common\.Web;", "using Newtouch.CIS.Compat;\nusing Newtouch.Common.Web;"),
    (r"using Newtouch\.Infrastructure\.EF;", "using Newtouch.CIS.Repository;"),
    (r"using Newtouch\.Core\.Common\.Interface;", ""),
    (r"using Newtouch\.Common\.Model;", ""),
    (r"using Newtouch\.Infrastructure\.TSql;", ""),
    (r"using Newtouch\.HIS\.Sett\.Request;", ""),
]

CTOR_FACTORY = re.compile(r"IDefaultDatabaseFactory\s+databaseFactory\s*,?\s*|IDatabaseFactory\s+databaseFactory\s*,?\s*")


def transform(text):
    for pat, rep in USING_MAP:
        text = re.sub(pat, rep, text)
    # drop the factory ctor parameter (and the comma after it, if any)
    text = CTOR_FACTORY.sub("", text)
    # base(databaseFactory) -> base()
    text = re.sub(r":\s*base\(databaseFactory\)", ": base()", text)
    # base(databaseFactory, ...) kept? (should not occur) -> strip first arg
    text = re.sub(r":\s*base\(databaseFactory\s*,", ": base(", text)
    # every file imports Compat (ICache / EntityExtensions / SqlParam)
    if "using Newtouch.CIS.Compat;" not in text:
        text = re.sub(r"(using System[^\n]*\n)", r"\1using Newtouch.CIS.Compat;\n", text, count=1)
        if "using Newtouch.CIS.Compat;" not in text:
            text = "using Newtouch.CIS.Compat;\n" + text
    text = re.sub(r"\n{3,}", "\n\n", text)
    return text


def port_tree(sub_src, sub_dst, skip_files=()):
    count = 0
    for root, dirs, files in os.walk(sub_src):
        dirs[:] = [d for d in dirs if d not in ("Properties", "Web References", "obj", "bin")]
        for fn in files:
            if not fn.endswith(".cs") or fn in skip_files:
                continue
            src_path = os.path.join(root, fn)
            rel = os.path.relpath(src_path, sub_src)
            dst_path = os.path.join(sub_dst, rel)
            os.makedirs(os.path.dirname(dst_path), exist_ok=True)
            with io.open(src_path, encoding="utf-8-sig") as fh:
                text = fh.read()
            with io.open(dst_path, "w", encoding="utf-8", newline="\n") as fh:
                fh.write(transform(text))
            count += 1
    return count


n1 = port_tree(os.path.join(SRC, "Newtouch.DomainServices"),
               os.path.join(DST, "DomainServices"),
               skip_files=("DmnServiceBase.cs",))
n2 = port_tree(os.path.join(SRC, "Newtouch.Repository"),
               os.path.join(DST, "Repository"))
n3 = port_tree(os.path.join(SRC, "Newtouch.Application"),
               os.path.join(DST, "Application"))
print(f"dmnservices: {n1}, repos: {n2}, apps: {n3}")
