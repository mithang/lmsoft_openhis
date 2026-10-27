#!/usr/bin/env python3
"""Port CIS Domain layer (entities, DTOs, VOs, ViewModels, Api, IDomainServices,
IRepository interfaces) from Framework source to the net6.0 Infra project.
Keeps original namespaces (Newtouch.Domain.*) to minimize churn in services.
Excludes: DBContext.Infrastructure (EF-only), DomainServices/DmnServiceBase.cs
(replaced by the Core stub in Infra/DomainServices/DmnServiceBase.cs).
Transforms:
  - strip BOM; drop FrameworkBase / EF / Newtouch.Infrastructure.EF usings
  - Newtouch.Core.Common -> NewtouchHIS.Lib.Base.Model (Pagination/AjaxResult)
  - Newtouch.Infrastructure.EF -> Newtouch.CIS.Repository (IRepositoryBase)
  - Entity [Table("x")] -> [SugarTable("x")] + using SqlSugar
  - Entity ': IEntity<T>' base -> dropped (marker interface only)
  - Newtouch.Core.Common.Utils / other Newtouch.Core.* usings -> dropped (fixed at build)
"""
import io, os, re, shutil, sys

SRC = os.path.expanduser("~/workspace/lmsoft_openhis_run/Newtouch.HIS.CIS/Newtouch.Domain")
DST = os.path.expanduser("~/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/Domain")
EXCLUDE_DIRS = {"DBContext.Infrastructure", "Properties"}
EXCLUDE_FILES = set()

def transform(text, path):
    # strip BOM handled by read encoding; normalize line endings
    lines = text.splitlines()
    out = []
    for ln in lines:
        s = ln.strip()
        if re.match(r"using\s+FrameworkBase\.[^;]+;", s):
            continue
        if re.match(r"using\s+Newtouch\.Infrastructure\.EF\s*;", s):
            out.append("using Newtouch.CIS.Repository;")
            continue
        if re.match(r"using\s+System\.Data\.Entity\s*;", s):
            continue
        if re.match(r"using\s+EntityFramework\s*;", s):
            continue
        if re.match(r"using\s+Newtouch\.Core\.Common\s*;", s):
            out.append("using NewtouchHIS.Lib.Base.Model;")
            continue
        if re.match(r"using\s+FrameworkBase\.MultiOrg\.Domain\.Entity\s*;", s):
            out.append("using Newtouch.Domain.Entity;")
            continue
        if re.match(r"using\s+Newtouch\.Common\.Operator\s*;", s):
            out.append("using NewtouchHIS.Lib.Framework.Operator;")
            out.append("using NewtouchHIS.Lib.Base.Model;")
            continue
        if re.match(r"using\s+Newtouch\.Infrastructure\.EF\.Attributes\s*;", s):
            out.append("using Newtouch.CIS.Compat;")
            continue
        if re.match(r"using\s+Newtouch\.Infrastructure\.EF\.Conventions\s*;", s):
            continue
        if re.match(r"using\s+Newtouch\.Core\.[^;]+;", s):
            continue  # fixed at build if a type is actually needed
        if re.match(r"using\s+Newtouch\.Common\s*;", s):
            continue  # fixed at build if needed
        out.append(ln)
    text = "\n".join(out) + "\n"
    # Entity table mapping
    if "[Table(" in text:
        text = re.sub(r"\[Table\(", "[SugarTable(", text)
        if "using SqlSugar;" not in text:
            text = re.sub(r"(using System[^\n]*\n)", r"\1using SqlSugar;\n", text, count=1)
    # Drop IEntity<T> base (marker only; entities are otherwise empty)
    text = re.sub(r"\s*:\s*IEntity<[^>]+>", "", text)
    return text

copied, skipped = 0, 0
for root, dirs, files in os.walk(SRC):
    dirs[:] = [d for d in dirs if d not in EXCLUDE_DIRS]
    for fn in files:
        if not fn.endswith(".cs"):
            continue
        sp = os.path.join(root, fn)
        rel = os.path.relpath(sp, SRC)
        if rel in EXCLUDE_FILES:
            skipped += 1
            continue
        dp = os.path.join(DST, rel)
        os.makedirs(os.path.dirname(dp), exist_ok=True)
        with io.open(sp, encoding="utf-8-sig") as f:
            text = f.read()
        with io.open(dp, "w", encoding="utf-8", newline="\n") as f:
            f.write(transform(text, rel))
        copied += 1
print(f"domain copied: {copied}, skipped: {skipped}")
