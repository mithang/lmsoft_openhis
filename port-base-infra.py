#!/usr/bin/env python3
"""Mechanical port: Base module Framework (EF6) -> net6.0 SqlSugar (Infra layer only).
Copies Domain/Repository/DomainServices/Application/Infrastructure sources from the
run copy into the new Infra classlib, applying namespace/attribute/ctor transforms.
Run copy is only READ, never written.
"""
import os
import re
import sys

SRC = os.path.expanduser("~/workspace/lmsoft_openhis_run/Newtouch.HIS.Base")
DST = os.path.expanduser("~/workspace/openhis-core/Newtouch.HIS.Base/Newtouch.HIS.Base.Infrastructure")

SKIP_FILES = {
    "IEntity.cs", "IRepositoryBase.T.cs", "RepositoryBase.T.cs", "DmnServiceBase.cs",
    "Constants.cs", "EFDbTransaction.cs", "IBaseDatabaseFactory.cs",
    "ICISApiDmnService.cs", "CISApiDmnService.cs",  # cross-module CIS/PDS types; unused by HOSP/Base webs
    "AssemblyInfo.cs",  # SDK-style project auto-generates assembly attributes
}
SKIP_DIRS = {"Properties", "DBContext.Infrastructure", "obj", "bin"}


def read_text(path):
    raw = open(path, "rb").read()
    for enc in ("utf-8-sig", "utf-8", "gbk"):
        try:
            return raw.decode(enc).replace("\r\n", "\n").replace("\r", "\n")
        except UnicodeDecodeError:
            continue
    return raw.decode("utf-8", errors="replace").replace("\r\n", "\n").replace("\r", "\n")


def csproj_compile_set(csproj_path):
    """Old-style csproj lists exactly the files in the build; files on disk but not
    listed (e.g. a duplicate ISysReportDmnService under IDomainServices/) must be
    excluded, or the SDK-style glob build sees duplicate types."""
    if not csproj_path or not os.path.exists(csproj_path):
        return None
    txt = read_text(csproj_path)
    inc = set()
    for m in re.finditer(r'<Compile\s+Include="([^"]+)"', txt):
        inc.add(m.group(1).replace("\\", "/"))
    if not inc:
        return None
    return inc


def transform_usings(lines):
    out = []
    for ln in lines:
        s = ln.strip()
        if re.match(r"using\s+System\.Web(\.|;)", s):
            continue
        if re.match(r"using\s+System\.Data\.Entity(\.|;)", s):
            continue
        if re.match(r"using\s+FrameworkBase", s):
            continue
        if re.match(r"using\s+Newtouch\.Infrastructure\.EF", s):
            continue
        if re.match(r"using\s+Newtouch\.HIS\.Domain\.DBContext\.Infrastructure", s):
            continue
        if re.match(r"using\s+Newtouch\.HIS\.Web\.Core", s):
            continue
        if s == "using Newtouch.Common.Operator;":
            out.append("using NewtouchHIS.Lib.Framework.Operator;")
            continue
        if s == "using Newtouch.Common.Web;":
            out.append("using NewtouchHIS.Lib.Base.Utilities;")
            continue
        if s == "using Newtouch.Common;":
            continue
        if s == "using Newtouch.Common.Model;":
            continue  # types from here are re-declared locally (FirstSecond) or unused
        if s == "using Newtouch.Common.Exceptions;":
            out.append("using NewtouchHIS.Lib.Base.Exceptions;")
            continue
        if s == "using Newtouch.Core.Common.Exceptions;":
            out.append("using NewtouchHIS.Lib.Base.Exceptions;")
            continue
        if re.match(r"using\s+Newtouch\.Core\.Common\.(Security|Utils|Interface)", s):
            continue
        if s == "using Newtouch.Core.Common;":
            continue
        if s == "using Newtouch.Core.Redis;":
            continue
        if s == "using Newtouch.Core.NLogger;":
            continue
        if s == "using Newtouch.Tools;":
            out.append("using NewtouchHIS.Lib.Base.Model;")
            out.append("using NewtouchHIS.Lib.Base.Extension;")
            out.append("using Newtouch.Tools;")
            continue
        out.append(ln)
    return out


def transform_text(text, is_entity):
    # --- attributes (entities) ---
    if is_entity:
        text = re.sub(r'\[Table\("([^"]+)"\)\]', r'[SugarTable("\1")]', text)
        text = re.sub(r"\[Key\]", "[SugarColumn(IsPrimaryKey = true)]", text)
        text = re.sub(
            r"\[DatabaseGenerated\(DatabaseGeneratedOption\.Identity\)\]",
            "[SugarColumn(IsIdentity = true)]", text)
        text = re.sub(r"\[DatabaseGenerated\([^\]]+\)\]", "", text)
        text = re.sub(r".*\[DecimalPrecision\([^\]]+\)\].*\n", "", text)
        text = re.sub(r":\s*IEntity<[A-Za-z0-9_, <>\.]+>", ": EntityBase", text)
        if ("SugarTable(" in text or "SugarColumn(" in text) and "using SqlSugar;" not in text:
            text = re.sub(r"(using [^;]+;\n)", r"\1using SqlSugar;\n", text, count=1)
    # --- ctors: drop database factory ---
    text = text.replace("new EFDbTransaction(this._databaseFactory).BeginTrans()", "new DbTrans()")
    text = text.replace("new Infrastructure.EFDbTransaction(this._databaseFactory).BeginTrans()", "new DbTrans()")
    text = re.sub(r":\s*base\(databaseFactory\)", "", text)
    text = re.sub(r",\s*IBaseDatabaseFactory databaseFactory", "", text)
    text = re.sub(r"IBaseDatabaseFactory databaseFactory\s*,", "", text)
    text = re.sub(r"IBaseDatabaseFactory databaseFactory", "", text)
    # --- EF context calls ---
    text = text.replace("_dataContext.Database.ExecuteSqlCommand(", "Sql.ExecuteCommand(")
    text = text.replace("_databaseFactory.Get().Database.ExecuteSqlCommand(", "Sql.ExecuteCommand(")
    # --- using injection for Lib.Base.Model types (Pagination/OperatorModel/AjaxResult) ---
    if re.search(r"\b(Pagination|OperatorModel|AjaxResult|ResultType)\b", text) \
            and "using NewtouchHIS.Lib.Base.Model;" not in text:
        text = re.sub(r"(using [^;]+;\n)", r"\1using NewtouchHIS.Lib.Base.Model;\n", text, count=1)
        if "using " not in text:
            text = "using NewtouchHIS.Lib.Base.Model;\n" + text
    if "OperatorProvider" in text and "using NewtouchHIS.Lib.Framework.Operator;" not in text:
        text = re.sub(r"(using [^;]+;\n)", r"\1using NewtouchHIS.Lib.Framework.Operator;\n", text, count=1)
    # --- WebHelper -> Lib.Base HttpWebHelper ---
    text = text.replace("WebHelper.", "HttpWebHelper.")
    if "HttpWebHelper." in text and "using NewtouchHIS.Lib.Base.Utilities;" not in text:
        text = re.sub(r"(using [^;]+;\n)", r"\1using NewtouchHIS.Lib.Base.Utilities;\n", text, count=1)
    return text


def port_tree(src_sub, dst_sub, is_entity=False, csproj=None):
    src_root = os.path.join(SRC, src_sub)
    dst_root = os.path.join(DST, dst_sub)
    proj_root = os.path.join(SRC, csproj.rsplit("/", 1)[0]) if csproj else None
    allowed = csproj_compile_set(os.path.join(SRC, csproj)) if csproj else None
    n = 0
    skipped = 0
    for root, dirs, files in os.walk(src_root):
        dirs[:] = [d for d in dirs if d not in SKIP_DIRS]
        for f in files:
            if not f.endswith(".cs") or f in SKIP_FILES:
                continue
            if f.endswith(".Designer.cs"):
                continue
            src = os.path.join(root, f)
            if allowed is not None:
                rel_proj = os.path.relpath(src, proj_root).replace(os.sep, "/")
                if rel_proj not in allowed:
                    skipped += 1
                    continue
            rel = os.path.relpath(src, src_root)
            dst = os.path.join(dst_root, rel)
            os.makedirs(os.path.dirname(dst), exist_ok=True)
            text = read_text(src)
            lines = transform_usings(text.split("\n"))
            text = transform_text("\n".join(lines), is_entity)
            open(dst, "w", encoding="utf-8").write(text)
            n += 1
    print(f"{src_sub} -> {dst_sub}: {n} files" + (f" ({skipped} not in csproj, skipped)" if skipped else ""))
    return n


def main():
    total = 0
    dom = "Newtouch.HIS.Domain/Newtouch.HIS.Domain.csproj"
    total += port_tree("Newtouch.HIS.Domain/Entity", "Domain/Entity", is_entity=True, csproj=dom)
    total += port_tree("Newtouch.HIS.Domain/IRepository", "Domain/IRepository", csproj=dom)
    total += port_tree("Newtouch.HIS.Domain/IDomainServices", "Domain/IDomainServices", csproj=dom)
    total += port_tree("Newtouch.HIS.Domain/BusinessObjects", "Domain/BusinessObjects",
                       is_entity=True, csproj=dom)
    for sub in os.listdir(os.path.join(SRC, "Newtouch.HIS.Domain")):
        p = os.path.join(SRC, "Newtouch.HIS.Domain", sub)
        if os.path.isdir(p) and sub not in ("Entity", "IRepository", "IDomainServices",
                                            "BusinessObjects", "DBContext.Infrastructure"):
            total += port_tree(f"Newtouch.HIS.Domain/{sub}", f"Domain/{sub}",
                               is_entity=sub in ("ValueObjects", "ReportManagement"), csproj=dom)
    total += port_tree("Newtouch.HIS.Repository", "Repository",
                       csproj="Newtouch.HIS.Repository/Newtouch.HIS.Repository.csproj")
    total += port_tree("Newtouch.HIS.DomainServices", "DomainServices",
                       csproj="Newtouch.HIS.DomainServices/Newtouch.HIS.DomainServices.csproj")
    app = "Newtouch.HIS.Application/Newtouch.HIS.Application.csproj"
    total += port_tree("Newtouch.HIS.Application/Interface", "Application/Interface", csproj=app)
    total += port_tree("Newtouch.HIS.Application/Implementation", "Application/Implementation", csproj=app)
    for f in ("CacheKey.cs", "CommmHelper.cs", "EnumDB.cs", "SitePdsAPIHelper.cs"):
        src = os.path.join(SRC, "Newtouch.Infrastructure", f)
        if os.path.exists(src):
            text = transform_text("\n".join(transform_usings(read_text(src).split("\n"))), False)
            dst = os.path.join(DST, "Infrastructure", f)
            os.makedirs(os.path.dirname(dst), exist_ok=True)
            open(dst, "w", encoding="utf-8").write(text)
            print(f"Infra helper: {f}")
            total += 1
    print(f"TOTAL: {total} files")


if __name__ == "__main__":
    main()
