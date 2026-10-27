#!/usr/bin/env python3
"""Port MRMS Web (MVC5) -> ASP.NET Core 6.0, following the MRQC pilot pattern.
Skips Login/Home/ClientsData controllers (hand-written) and SystemManage Sys* views
(served by SSO in production). Safe to re-run: output dir is rewritten each time.
"""
import re, shutil, sys
from pathlib import Path

WS = Path.home() / "workspace"
SRC = WS / "lmsoft_openhis_run/Newtouch.HIS.MRMS/Newtouch.MR.ManageSystem.Web"
MRQC = WS / "openhis-core/Newtouch.MRQC.Web"
DST = WS / "openhis-core/Newtouch.HIS.MRMS/Newtouch.MRMS.Web"

DROP_USING_PREFIXES = (
    "using System.Web", "using Newtouch.Common", "using Newtouch.Core",
    "using Newtouch.HIS.Web.Core", "using FrameworkBase", "using Newtouch.Infrastructure",
    "using Newtouch.Tools", "using System.Data.Entity", "using DCSoft",
    "using System.Drawing", "using Newtouch.MR.ManageSystem.APIRequest",
)
HAND_CONTROLLERS = {"LoginController.cs", "HomeController.cs", "ClientsDataController.cs"}
# SystemManage Sys* views served by SSO's shared sys-manage controllers in production.
SKIP_VIEWS = {
    "Areas/SystemManage/Views/SysConfig/Form.cshtml",
    "Areas/SystemManage/Views/SysConfig/Index.cshtml",
    "Areas/SystemManage/Views/SysConfig/TemplateForm.cshtml",
    "Areas/SystemManage/Views/SysConfig/TemplateIndex.cshtml",
    "Areas/SystemManage/Views/SysLog/Index.cshtml",
    "Areas/SystemManage/Views/SysModule/Form.cshtml",
    "Areas/SystemManage/Views/SysModule/Index.cshtml",
    "Areas/SystemManage/Views/SysRole/Form.cshtml",
    "Areas/SystemManage/Views/SysRole/Index.cshtml",
    "Areas/SystemManage/Views/SysStaff/Selector.cshtml",
    "Areas/SystemManage/Views/SysUser/Index.cshtml",
    "Areas/SystemManage/Views/SysUser/PersonalInfo.cshtml",
    "Areas/SystemManage/Views/SysUser/Roles.cshtml",
    "Areas/SystemManage/Views/SysUser/Selector.cshtml",
}
CORE_USINGS = """using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Init;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.MRMS.Web.Models;
"""

def read(p):
    return p.read_text(encoding="utf-8-sig")

def write(p, txt):
    p.parent.mkdir(parents=True, exist_ok=True)
    p.write_text(txt, encoding="utf-8")

def port_controller(src_path, area):
    txt = read(src_path)
    lines = txt.split("\n")
    kept = []
    for ln in lines:
        s = ln.strip()
        if s.startswith("using ") and any(s.startswith(p) for p in DROP_USING_PREFIXES):
            continue
        kept.append(ln)
    txt = "\n".join(kept)
    txt = txt.replace("Newtouch.MR.ManageSystem.Web", "Newtouch.MRMS.Web")
    # header usings (dedupe against existing)
    existing = set(re.findall(r"using ([\w\.]+);", txt))
    add = [u for u in CORE_USINGS.strip().split("\n") if re.search(r"using ([\w\.]+);", u).group(1) not in existing]
    if add:
        # insert after first using line, else at top
        ls = txt.split("\n")
        idx = next((i for i, l in enumerate(ls) if l.strip().startswith("using ")), -1)
        for j, u in enumerate(add):
            ls.insert(idx + 1 + j, u)
        txt = "\n".join(ls)
    # area attribute
    if area:
        txt = re.sub(r"(?m)^(\s*)public class (\w+Controller)\b",
                     lambda m: m.group(1) + '[Area("' + area + '")]\n' + m.group(1) + "public class " + m.group(2),
                     txt, count=1)
    # action result types (override first, then blanket with lookbehind so
    # already-correct IActionResult is not doubled to IIActionResult)
    txt = re.sub(r"public ActionResult (Index|Form)\(\)", r"public override IActionResult \1()", txt)
    txt = re.sub(r"(?<!I)ActionResult", "IActionResult", txt)
    txt = re.sub(r"public JsonResult ", r"public IActionResult ", txt)
    txt = re.sub(r"Json\(([^,()]+(?:\([^()]*\))?[^,]*?),\s*JsonRequestBehavior\.AllowGet\)", r"Json(\1)", txt)
    txt = txt.replace("ConfigurationHelper.GetAppConfigValue(", "AppSettings.GetConfig(")
    txt = txt.replace("Newtouch.Core.Common.Utils.ConfigurationHelper", "AppSettings")
    # MrFeeItemController: binary Newtouch.Common.TreeViewModel -> own Domain.Entity one
    txt = txt.replace("Common.TreeViewModel", "TreeViewModel")
    # constructor generation for readonly service fields without a ctor
    m = re.search(r"public class (\w+Controller)\b", txt)
    if m:
        cls = m.group(1)
        if not re.search(r"public " + cls + r"\s*\(", txt):
            fields = re.findall(r"private readonly (I[A-Za-z0-9_]+) (_[A-Za-z0-9_]+);", txt)
            if fields:
                params = ", ".join(f"{t} {f[1:]}" for t, f in fields)
                body = "\n".join(f"            this.{f} = {f[1:]};" for _, f in fields)
                ctor = f"\n        public {cls}({params})\n        {{\n{body}\n        }}\n"
                # insert right after the class opening brace line
                cm = re.search(r"(public class " + cls + r"[^\n]*\n\s*\{)", txt)
                if cm:
                    txt = txt[:cm.end()] + ctor + txt[cm.end():]
                print(f"  GEN-CTOR {cls}: {len(fields)} params")
    return txt

def port_view(txt):
    txt = txt.replace("SiteUrl.", "MvcHelper.")
    txt = txt.replace("@using Newtouch.Common.Operator", "@using NewtouchHIS.Lib.Framework.Operator")
    txt = txt.replace("@using Newtouch.Core.Common.Utils", "@using NewtouchHIS.Lib.Base.Utilities")
    txt = txt.replace("Newtouch.HIS.Web.Core.Models.BottomButtonViewModel", "Newtouch.MRMS.Web.Models.BottomButtonViewModel")
    txt = txt.replace("Newtouch.Core.Common.Utils.", "")
    txt = txt.replace("ConfigurationHelper.GetAppConfigValue(", "AppSettings.GetConfig(")
    txt = txt.replace("FrameworkBase.MultiOrg.Infrastructure.ConstantsBase.AppName", 'AppSettings.GetConfig("AppName")')
    txt = txt.replace("FrameworkBase.MultiOrg.Infrastructure.ConstantsBase.AppId", 'AppSettings.GetConfig("AppId")')
    txt = txt.replace("FrameworkBase.MultiOrg.Infrastructure.ConstantsBase.TopOrganizeId", 'AppSettings.GetConfig("Top_OrganizeId")')
    txt = txt.replace("FrameworkBase.MultiOrg.Domain.Entity.SysOrganizeVEntity", "Newtouch.MRMS.Web.Models.MrmsOrgItem")
    txt = txt.replace("Newtouch.MR.ManageSystem.Web", "Newtouch.MRMS.Web")
    return txt

def main():
    # hand-maintained files the port must not wipe
    preserve = ["Newtouch.MRMS.Web.csproj", "Program.cs", "appsettings.json",
                "Controllers/LoginController.cs", "Controllers/HomeController.cs",
                "Controllers/ClientsDataController.cs", "Models/MrmsOrgItem.cs",
                "Models/TreeGridModels.cs", "Configs/Log4Net.config"]
    saved = {}
    for rel in preserve:
        p = DST / rel
        if p.exists():
            saved[rel] = p.read_bytes()
    configs_dir = DST / "Configs"
    saved_configs = None
    if configs_dir.exists():
        saved_configs = {str(f.relative_to(configs_dir)): f.read_bytes() for f in configs_dir.rglob("*") if f.is_file()}
    if DST.exists():
        shutil.rmtree(DST)
    n_ctl, n_view = 0, 0
    # controllers
    for src in sorted((SRC / "Controllers").glob("*.cs")) + sorted(SRC.glob("Areas/*/Controllers/*.cs")):
        if src.name in HAND_CONTROLLERS:
            print("SKIP (hand-written):", src.relative_to(SRC)); continue
        area = None
        parts = src.relative_to(SRC).parts
        if parts[0] == "Areas":
            area = parts[1]
        rel = Path("Controllers") / src.name if area is None else Path("Areas") / area / "Controllers" / src.name
        out = DST / rel
        write(out, port_controller(src, area))
        n_ctl += 1
    # views (all except skipped Sys* + Shared + Home handled below)
    for src in sorted(SRC.rglob("*.cshtml")):
        rel = src.relative_to(SRC)
        if str(rel).replace("\\", "/") in SKIP_VIEWS:
            print("SKIP (SSO-served):", rel); continue
        if rel.parts[0] == "Views" and len(rel.parts) > 1 and rel.parts[1] == "Shared":
            continue
        if str(rel) == "Views/_ViewStart.cshtml":
            continue
        write(DST / rel, port_view(read(src)))
        n_view += 1
    # Shared layouts from MRQC (ported), mrqc-vi -> mrms-vi, MRQC ns -> MRMS ns
    for src in sorted((MRQC / "Views/Shared").glob("*.cshtml")):
        txt = read(src).replace("mrqc-vi.js", "mrms-vi.js").replace("Newtouch.MRQC", "Newtouch.MRMS")
        write(DST / "Views/Shared" / src.name, txt)
    # _ViewStart + _ViewImports from MRQC
    write(DST / "Views/_ViewStart.cshtml", read(MRQC / "Views/_ViewStart.cshtml"))
    vi = read(MRQC / "Views/_ViewImports.cshtml")
    if "Lib.Base.Init" not in vi:
        vi = vi.replace("@using NewtouchHIS.Lib.Base\n", "@using NewtouchHIS.Lib.Base\n@using NewtouchHIS.Lib.Base.Init\n")
    write(DST / "Views/_ViewImports.cshtml", vi)
    write(DST / "Areas/_ViewImports.cshtml", vi)
    # wwwroot from MRQC, rename mrqc-vi.js -> mrms-vi.js
    shutil.copytree(MRQC / "wwwroot", DST / "wwwroot")
    (DST / "wwwroot/js/mrqc-vi.js").rename(DST / "wwwroot/js/mrms-vi.js")
    # Login view from MRQC, rebranded
    login = read(MRQC / "Views/Login/Index.cshtml")
    login = login.replace("MRQC Hệ thống kiểm soát chất lượng", "MRMS Hệ thống bệnh án")
    login = login.replace("'@(\"MRQC\")'", "'@(\"MR\")'")
    write(DST / "Views/Login/Index.cshtml", login)
    # BottomButtonViewModel (MRQC pattern, MRMS namespace)
    bbm = read(MRQC / "Models/BottomButtonViewModel.cs")
    bbm = bbm.replace("Newtouch.MRQC.Web", "Newtouch.MRMS.Web")
    write(DST / "Models/BottomButtonViewModel.cs", bbm)
    # restore hand-maintained files
    for rel, data in saved.items():
        p = DST / rel
        p.parent.mkdir(parents=True, exist_ok=True)
        p.write_bytes(data)
    if saved_configs:
        for rel, data in saved_configs.items():
            p = DST / "Configs" / rel
            p.parent.mkdir(parents=True, exist_ok=True)
            p.write_bytes(data)
    print(f"controllers={n_ctl} views={n_view}")

if __name__ == "__main__":
    main()
