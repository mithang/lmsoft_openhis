#!/usr/bin/env python3
"""Port MRMS (.NET Framework MVC5) -> ASP.NET Core 6.0, following the MRQC pilot pattern.
Copies source files from the READ-ONLY run copy into ~/workspace/openhis-core/Newtouch.HIS.MRMS
with mechanical transforms; hand-written base files are created by part 2 of this script run.
Idempotent: re-running overwrites the copied files (hand edits inside copied files will be lost;
hand-written files are marked HAND-WRITTEN and are only created if absent, except base files
which are always rewritten from the templates below).
"""
import os, re, shutil, sys

SRC = "/home/hatch/workspace/lmsoft_openhis_run/Newtouch.HIS.MRMS"
DST = "/home/hatch/workspace/openhis-core/Newtouch.HIS.MRMS"
INFRA = os.path.join(DST, "Newtouch.MRMS.Infrastructure")
WEB = os.path.join(DST, "Newtouch.MRMS.Web")

DROP_PREFIXES = [
    r"System\.Web", r"FrameworkBase", r"Newtouch\.Core", r"Newtouch\.Tools",
    r"Newtouch\.HIS\.Web\.Core", r"System\.Data\.SqlClient", r"EntityFramework",
    r"DCSoft", r"Newtouch\.Infrastructure\.EF", r"Newtouch\.Common",
    r"Newtouch\.HIS\.API",
]
DROP_USING_RE = re.compile(r"^\s*using\s+(?:" + "|".join(DROP_PREFIXES) + r")(?:[.;]|$)")

STD_USINGS = """using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
"""

WEB_USINGS = """using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;
"""

def read(path):
    with open(path, "r", encoding="utf-8-sig", errors="replace") as f:
        return f.read()

def write(path, text):
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, "w", encoding="utf-8") as f:
        f.write(text)

def strip_drop_usings(text):
    lines = text.split("\n")
    kept = [ln for ln in lines if not DROP_USING_RE.match(ln)]
    return "\n".join(kept)

def insert_usings(text, extra):
    # insert the using block before the first namespace declaration
    m = re.search(r"^(namespace\s+[\w\.]+)", text, flags=re.M)
    block = (STD_USINGS + extra).rstrip() + "\n\n"
    if m:
        return text[:m.start()] + block + text[m.start():]
    return block + text

def base_transforms(text):
    # entity mapping attributes
    text = re.sub(r"\[Table\(\"([^\"]+)\"\)\]", r'[SugarTable("\1")]', text)
    text = text.replace("[Key]", "[SugarColumn(IsPrimaryKey = true)]")
    # entity base
    text = re.sub(r":\s*IEntity<\w+>", ": EntityBase", text)
    # sql params
    text = text.replace("SqlParameter", "SugarParameter")
    # ctor DI: drop IDefaultDatabaseFactory param + base(...) call
    text = re.sub(r"\(IDefaultDatabaseFactory databaseFactory,\s*", "(", text)
    text = re.sub(r",\s*IDefaultDatabaseFactory databaseFactory\)", ")", text)
    text = re.sub(r"\(IDefaultDatabaseFactory databaseFactory\)", "()", text)
    text = re.sub(r":\s*base\(databaseFactory\)", "", text)
    # config helper
    text = text.replace("ConfigurationHelper.GetAppConfigValue", "AppSettings.GetConfig")
    text = text.replace("Newtouch.Core.Common.Utils.ConfigurationHelper", "AppSettings")
    return text

def port_cs(src_path, dst_path, is_controller=False, area=None):
    text = read(src_path)
    text = strip_drop_usings(text)
    text = base_transforms(text)
    if is_controller:
        # action return types
        text = re.sub(r"public\s+ActionResult\s+Index\s*\(", "public override IActionResult Index(", text)
        text = re.sub(r"public\s+ActionResult\s+Form\s*\(", "public override IActionResult Form(", text)
        text = re.sub(r"public\s+ActionResult\s+", "public IActionResult ", text)
        text = re.sub(r"public\s+JsonResult\s+", "public IActionResult ", text)
        text = re.sub(r",\s*JsonRequestBehavior\.AllowGet", "", text)
        # area attribute (insert before class declaration)
        if area:
            text = re.sub(r"(public\s+class\s+\w+Controller\b)",
                          '[Area("%s")]\n    \\1' % area, text, count=1)
    text = insert_usings(text, WEB_USINGS if is_controller else "")
    write(dst_path, text)

def copy_tree(src_dir, dst_dir, skip_names=()):
    for root, dirs, files in os.walk(src_dir):
        dirs[:] = [d for d in dirs if d not in skip_names]
        for fn in files:
            if fn.endswith(".cs") and fn != "AssemblyInfo.cs":
                sp = os.path.join(root, fn)
                rel = os.path.relpath(sp, src_dir)
                port_cs(sp, os.path.join(dst_dir, rel))

def main():
    # ---------- Infrastructure ----------
    copy_tree(os.path.join(SRC, "Newtouch.MR.ManageSystem.Domain", "Entity"),
              os.path.join(INFRA, "Domain", "Entity"))
    copy_tree(os.path.join(SRC, "Newtouch.MR.ManageSystem.Domain", "DTO"),
              os.path.join(INFRA, "Domain", "DTO"))
    copy_tree(os.path.join(SRC, "Newtouch.MR.ManageSystem.Domain", "ValueObjects"),
              os.path.join(INFRA, "Domain", "ValueObjects"))
    copy_tree(os.path.join(SRC, "Newtouch.MR.ManageSystem.Domain", "IDomainServices"),
              os.path.join(INFRA, "Domain", "IDomainServices"))
    copy_tree(os.path.join(SRC, "Newtouch.MR.ManageSystem.Domain", "IRepository"),
              os.path.join(INFRA, "Domain", "IRepository"))
    copy_tree(os.path.join(SRC, "Newtouch.MR.ManageSystem.DomainServices"),
              os.path.join(INFRA, "DomainServices"))
    copy_tree(os.path.join(SRC, "Newtouch.MR.ManageSystem.Repository"),
              os.path.join(INFRA, "Repository"))
    for fn in ["Constants.cs", "EnumDB.cs", "CacheKey.cs"]:
        port_cs(os.path.join(SRC, "Newtouch.MR.ManageSystem.Infrastructure", fn),
                os.path.join(INFRA, "Infrastructure", fn))
    print("infra copied")

if __name__ == "__main__":
    main()
