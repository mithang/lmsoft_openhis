#!/usr/bin/env python3
"""Mechanical .NET Framework -> .NET Core 6 port transform for the OR module copy.
Applies using/type replacements + [Area] attributes + ctor generation for
classes whose readonly fields are never assigned (the original relied on
FrameworkBase property injection, which has no Core equivalent).
Safe to re-run (idempotent for the replacements it makes).
"""
import re, sys
from pathlib import Path

USING_MAP = [
    ("using System.Web.Mvc;", "using Microsoft.AspNetCore.Mvc;"),
    ("using System.Web;", ""),
    ("using System.Web.Routing;", ""),
    ("using System.Web.Http;", "using Microsoft.AspNetCore.Mvc;"),
    ("using FrameworkBase.MultiOrg.Web;", "using NewtouchHIS.Framework.Web.Controllers;"),
    ("using FrameworkBase.MultiOrg.Domain.Entity;", ""),
    ("using FrameworkBase.MultiOrg.Domain.IRepository;", ""),
    ("using FrameworkBase.MultiOrg.Infrastructure;", ""),
    ("using FrameworkBase.MultiOrg.Repository;", ""),
    ("using FrameworkBase.MultiOrg.DmnService;", ""),
    ("using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;", ""),
    ("using Newtouch.Infrastructure.EF;", ""),
    ("using Newtouch.Core.Common;", "using NewtouchHIS.Lib.Base.Model;"),
    ("using Newtouch.Core.Common.Exceptions;", "using NewtouchHIS.Lib.Base.Exceptions;"),
    ("using Newtouch.Common.Operator;", "using NewtouchHIS.Lib.Framework.Operator;"),
    ("using Newtouch.Tools;", "using NewtouchHIS.Lib.Base.Model;\nusing NewtouchHIS.Lib.Base.Extension;"),
    ("using System.Data.SqlClient;", "using SqlSugar;"),
    ("using Autofac;", ""),
    ("using Autofac.Integration.Mvc;", ""),
    ("using Newtouch.Core.Redis;", ""),
    ("using Newtouch.Core.Redis.Cache;", ""),
    ("using Newtouch.Core.NLogger;", ""),
    ("using Newtouch.Core.Common.Interface;", ""),
    ("using Newtouch.Common;", ""),
    ("using Newtouch.Common.Web;", ""),
    ("using Newtouch.Core.Common.Utils;", ""),
    ("using System.Data.Entity.Core.Common.CommandTrees;", ""),
]

def transform_usings(text):
    lines = text.split("\n")
    out = []
    for ln in lines:
        s = ln.strip()
        replaced = False
        for old, new in USING_MAP:
            if s == old:
                if new:
                    out.append(new)
                replaced = True
                break
        if not replaced:
            out.append(ln)
    return "\n".join(out)

FIELD_RE = re.compile(r"^(\s*)private\s+readonly\s+([\w\.<>,\s\?\[\]]+?)\s+(_\w+)\s*;\s*$")
CLASS_RE = re.compile(r"^\s*(public\s+)?(abstract\s+|sealed\s+)?(class|struct)\s+(\w+)")

def add_ctors(text, path):
    """If a class declares readonly fields but no ctor, insert one assigning them."""
    lines = text.split("\n")
    # find first class declaration (each ported file has exactly one main class)
    cls = None
    for ln in lines:
        m = CLASS_RE.match(ln)
        if m:
            cls = m.group(4)
            break
    if cls is None:
        return text
    # already has a ctor?
    if re.search(r"\bpublic\s+" + re.escape(cls) + r"\s*\(", text):
        return text
    fields = []
    for ln in lines:
        m = FIELD_RE.match(ln)
        if m and not ln.strip().startswith("//"):
            ftype, fname = m.group(2).strip(), m.group(3)
            fields.append((ftype, fname, m.group(1)))
    if not fields:
        return text
    # insert ctor after the last field line
    last_idx = None
    for i, ln in enumerate(lines):
        if FIELD_RE.match(ln) and not ln.strip().startswith("//"):
            last_idx = i
    def pname(f):
        n = f.lstrip("_")
        return n[0].lower() + n[1:] if n else f
    params = ", ".join(f"{t} {pname(f)}" for t, f, _ in fields)
    indent = fields[0][2]
    ctor = [f"{indent}public {cls}({params})", f"{indent}{{"] + \
           [f"{indent}    {f} = {pname(f)};" for _, f, _ in fields] + [f"{indent}}}"]
    lines[last_idx + 1:last_idx + 1] = ctor
    return "\n".join(lines)

def transform_file(p: Path, is_controller: bool, area: str | None):
    raw = p.read_bytes()
    text = raw.decode("utf-8-sig")
    orig = text
    text = transform_usings(text)
    # type/attribute replacements
    text = re.sub(r"\bSqlParameter\b", "SugarParameter", text)
    text = re.sub(r"\[Table\(", "[SugarTable(", text)
    text = text.replace("[Key]", "[SugarColumn(IsPrimaryKey = true)]")
    text = re.sub(r":\s*IEntity<(\w+)>", ": IEntityBase", text)
    if is_controller:
        text = re.sub(r"\bActionResult\b", "IActionResult", text)
    if area:
        # add [Area("X")] before the controller class declaration
        text = re.sub(r"(?m)^(\s*public\s+class\s+\w+Controller\b)",
                      f'    [Area("{area}")]\\n\\1', text, count=1)
    text = add_ctors(text, p)
    if text != orig:
        p.write_text(text, encoding="utf-8")
        return True
    return False

def main():
    base = Path.home() / "workspace/openhis-core/Newtouch.HIS.OR"
    changed = 0
    total = 0
    for p in sorted((base / "Newtouch.OR.Infrastructure").rglob("*.cs")):
        total += 1
        changed += transform_file(p, False, None)
    web = base / "Newtouch.OR.Web"
    for p in sorted(web.rglob("*.cs")):
        # area from path Areas/<Area>/Controllers/
        m = re.search(r"Areas/([^/]+)/Controllers/", str(p))
        area = m.group(1) if m else None
        total += 1
        changed += transform_file(p, True, area)
    print(f"transformed {changed}/{total} files")

if __name__ == "__main__":
    main()
