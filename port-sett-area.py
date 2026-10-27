#!/usr/bin/env python3
"""Port ONE Sett Area (Framework MVC5 -> ASP.NET Core 6) into the merged app.
Usage: python3 port-sett-area.py <AreaName>
- Controllers: mechanical transform + ctor injection from readonly fields.
- Views: verbatim copy of the whole Views tree (+ _ViewStart if present).
- Reports: base classes, unportable-API hits, layout/partial/view-name gaps.
Does NOT translate (translate-sett-area.py does that) and does NOT generate
services/types (gen-sett-services.py / gen-sett-types.py do that).
"""
import re, os, shutil, sys

AREA = sys.argv[1]
SRC = f"/home/hatch/workspace/lmsoft_openhis_run/Newtouch.HIS.Sett/Newtouch.Web/Areas/{AREA}"
DST = f"/home/hatch/workspace/openhis-core/Newtouch.Sett.Web/Areas/{AREA}"
WEBROOT = "/home/hatch/workspace/openhis-core/Newtouch.Sett.Web"

HEADER = f"""using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.{AREA}.Controllers
{{
"""

REPLACEMENTS = [
    ("OperatorProvider.GetCurrent().OrganizeId", "this.OrganizeId"),
    ("OperatorProvider.GetCurrent().UserCode", "this.UserIdentity.UserCode"),
    # Sett's own wrapper (source: Newtouch.Infrastructure.Operator.NewOperatorProvider)
    ("NewOperatorProvider.Provider.GetCurrent().OrganizeId", "this.OrganizeId"),
    ("NewOperatorProvider.Provider.GetCurrent().UserCode", "this.UserIdentity.UserCode"),
    ("EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt(", "SettKeyHelper.GetNewPrimaryKeyInt("),
    ('ConfigurationHelper.GetAppConfigValue("ReportServer.HOST")', '""'),
    ("System.Web.HttpUtility.UrlDecode", "System.Net.WebUtility.UrlDecode"),
    ("System.Web.HttpUtility.UrlEncode", "System.Net.WebUtility.UrlEncode"),
    ("System.Web.HttpUtility.HtmlEncode", "System.Net.WebUtility.HtmlEncode"),
    ("HttpUtility.UrlDecode", "System.Net.WebUtility.UrlDecode"),
    ("HttpUtility.UrlEncode", "System.Net.WebUtility.UrlEncode"),
    ("System.Web.HttpContext.Current", "HttpContext"),
    ("HttpContext.Current", "HttpContext"),
    ("WebHelper.GetCookie(", "Request.Cookies["),
    ("WebHelper.RemoveCookie(", "Response.Cookies.Delete("),
    ("Server.MapPath(", "SettPathHelper.MapPath("),
    ("HostingEnvironment.MapPath(", "SettPathHelper.MapPath("),
    ("Server.UrlDecode(", "System.Net.WebUtility.UrlDecode("),
    ("Server.HtmlEncode(", "System.Net.WebUtility.HtmlEncode("),
    ("Request.Files", "Request.Form.Files"),
    ("HttpPostedFileBase", "Microsoft.AspNetCore.Http.IFormFile"),
    ("[FromUri]", "[FromQuery]"),
    ("[ValidateInput(false)]", ""),
    ("[ValidateInput(true)]", ""),
    ("[AllowHtml] ", ""),
    (", JsonRequestBehavior.AllowGet", ""),
    (", JsonRequestBehavior.DenyGet", ""),
    ("JsonRequestBehavior.AllowGet", ""),
]

ATTR_DROP = re.compile(r"^\s*\[(HandlerAjaxOnly|OutputCache|ChildActionOnly|AutoResolveIgnore)[^\]]*\]\s*$", re.M)
PARAM_RE = re.compile(r'Request\.Params\["([^"]+)"\]')
REQUEST_BRACKET_RE = re.compile(r'Request\["([^"]+)"\]')


def find_method_bounds(text, start):
    brace = text.index("{", start)
    depth = 0; i = brace; in_str = None
    while i < len(text):
        c = text[i]
        if in_str:
            if c == "\\":
                i += 2; continue
            if c == in_str: in_str = None
        else:
            if c in ('"', "'"): in_str = c
            elif c == "{": depth += 1
            elif c == "}":
                depth -= 1
                if depth == 0: return brace, i + 1
        i += 1
    raise ValueError("unbalanced braces")


def port_controller(name):
    with open(os.path.join(SRC, "Controllers", name), encoding="utf-8-sig") as f:
        text = f.read()
    m = re.search(r"namespace\s+[\w\.]+\s*\{", text)
    assert m, f"no namespace in {name}"
    body = text[m.end():].rstrip()
    assert body.endswith("}"), name
    body = body[:-1].rstrip() + "\n"
    notes = []
    # base class
    bm = re.search(r"public class (\w+Controller)\s*:\s*(\w+)", body)
    cls = bm.group(1) if bm else name[:-3]
    base = bm.group(2) if bm else "?"
    if base in ("ControllerBase", "OrgControllerBase"):
        # OrgControllerBase (FrameworkBase.MultiOrg) == merged ControllerBase's
        # own base: re-basing to the merged ControllerBase keeps OrganizeId etc.
        body = re.sub(r"(public class \w+Controller)\s*:\s*(ControllerBase|OrgControllerBase)",
                      r"\1 : Newtouch.Sett.Web.Controllers.ControllerBase", body, count=1)
    else:
        notes.append(f"BASE={base}")
    body = re.sub(r"(\n\s*)public class (\w+Controller)",
                  lambda m: f'{m.group(1)}[Area("{AREA}")]\n{m.group(1)}public class {m.group(2)}',
                  body, count=1)
    # return types
    body = re.sub(r"\bActionResult\b", "IActionResult", body)
    body = body.replace("public JsonResult ", "public IActionResult ")
    body = body.replace("Task<JsonResult>", "Task<IActionResult>")
    body = ATTR_DROP.sub("", body)
    # ctor injection from fields declared before first action
    act = re.search(r"\n\s*public\s+(?:override\s+)?(?:IActionResult|string|void|Task<[^>]+>)\s+\w+\s*\(", body)
    head_region = body[:act.start()] if act else body
    fields = re.findall(r"private readonly (I\w+) (_\w+);", head_region)
    if not fields:
        # fallback: any private readonly I-field anywhere
        fields = re.findall(r"private readonly (I\w+) (_\w+);", body)
    # source already has its own ctor (e.g. `this._f = p;`) -> do NOT add another
    if fields and re.search(rf"public\s+{re.escape(cls)}\s*\(", body):
        fields = []
    if fields:
        ctor_params, assigns = [], []
        for t, f in fields:
            pname = f[1:]
            pname = pname[0].lower() + pname[1:]
            ctor_params.append(f"{t} {pname}")
            assigns.append(f"            {f} = {pname};")
        ctor = (f"\n        public {cls}({', '.join(ctor_params)})\n        {{\n"
                + "\n".join(assigns) + "\n        }\n")
        last_field = list(re.finditer(r"private readonly I\w+ _\w+;", body))[-1]
        body = body[:last_field.end()] + ctor + body[last_field.end():]
    # replacements
    for old, new in REPLACEMENTS:
        body = body.replace(old, new)
    # Framework Tools.Json.ToList<T>(json) -> Newtonsoft (phase-1 pattern);
    # strip any namespace prefix (Newtouch.Tools.Json.ToList -> Newtouch.JsonConvert bug)
    body = re.sub(r"(?:[\w]+\.)*Tools\.Json\.ToList<(\w+)>\(", r"JsonConvert.DeserializeObject<List<\1>>(", body)
    # Framework app-config helper -> merged SysConfigReader (DB-backed; missing
    # keys return null and existing default fallbacks still apply)
    body = re.sub(r"ConfigurationHelper\.GetAppConfigValue\(", "SysConfigReader.String(", body)
    # Framework JavaScriptSerializer -> Newtonsoft (no Core equivalent)
    body = re.sub(r"var\s+(\w+)\s*=\s*new JavaScriptSerializer\(\);\n", "", body)
    # nested generic first: Deserialize<List<T>> (single-type pattern fails here:
    # [^>]+ stops at the first > and the remaining > breaks the \( match)
    body = re.sub(r"(\w+)\.Deserialize<(\w+)<(\w+)>>\(", r"JsonConvert.DeserializeObject<\2<\3>>(", body)
    body = re.sub(r"(\w+)\.Deserialize<([^>]+)>\(", r"JsonConvert.DeserializeObject<\2>(", body)
    body = re.sub(r"(\w+)\.Serialize\(", r"JsonConvert.SerializeObject(", body)
    body = PARAM_RE.sub(lambda mm: f'Request.Query["{mm.group(1)}"].FirstOrDefault()', body)
    def req_bracket(mm):
        k = mm.group(1)
        return (f'(Request.Query["{k}"].FirstOrDefault() ?? '
                f'(Request.HasFormContentType ? Request.Form["{k}"].FirstOrDefault() : null))')
    body = REQUEST_BRACKET_RE.sub(req_bracket, body)
    # unportable-API hits (report only; fixed in build rounds)
    for pat in ["Interop", "WCF", "ServiceModel", "ReportViewer", "Microsoft.Reporting",
                "SignalR", "System.Drawing", "DirectoryServices", "FormsAuthentication",
                "ConfigurationManager", "WebConfigurationManager", "FileWebHelper",
                "HttpContext.Current", "System.Web.", "Response.Write", "Response.BinaryWrite",
                "Request.Url", "Request.UserHostAddress", "Request.InputStream",
                "HttpResponseMessage", "ApiController", "Membership", "Roles.",
                "Owin", "Soap", "WebService", "Office", "Excel"]:
        if re.search(pat, body):
            notes.append(f"HIT:{pat}")
    out = HEADER + body + "}\n"
    os.makedirs(os.path.join(DST, "Controllers"), exist_ok=True)
    with open(os.path.join(DST, "Controllers", name), "w", encoding="utf-8") as f:
        f.write(out)
    print(f"ported {name} fields={len(fields)} base={base} notes={notes}")
    return notes


def port_views():
    src_views = os.path.join(SRC, "Views")
    dst_views = os.path.join(DST, "Views")
    if not os.path.isdir(src_views):
        print("NO VIEWS DIR")
        return []
    copied = []
    for root, _dirs, files in os.walk(src_views):
        for fn in files:
            if not fn.endswith(".cshtml"):
                continue
            sp = os.path.join(root, fn)
            rel = os.path.relpath(sp, src_views)
            dp = os.path.join(dst_views, rel)
            os.makedirs(os.path.dirname(dp), exist_ok=True)
            txt = open(sp, encoding="utf-8-sig", errors="ignore").read()
            # drop old-framework @usings; keep NewtouchHIS.* / Newtouch.Sett.*
            lines = []
            for ln in txt.split("\n"):
                if re.match(r"\s*@using Newtouch\.Common\.Operator\b", ln):
                    # binary OperatorProvider -> Core Lib.Framework equivalent (phase-1 pattern)
                    lines.append("@using NewtouchHIS.Lib.Framework.Operator;")
                    continue
                if re.match(r"\s*@using Newtouch\.(HIS|Infrastructure|Tools|Core|Application|Toos|Common)\b", ln):
                    continue
                lines.append(ln)
            txt = "\n".join(lines)
            # fully-qualified old types -> short name (resolves via _ViewImports Sett.Domain)
            txt = re.sub(r"Newtouch\.[\w\.]+\.(\w+)\.", r"\1.", txt)
            # the strip above turns Web.Core Models.X into Models.X; phase-1
            # views use the fully-qualified merged Models type instead
            txt = txt.replace("Models.BottomButtonViewModel",
                              "Newtouch.Sett.Web.Models.BottomButtonViewModel")
            # Framework helpers mangled by the strip into bare names:
            # Newtouch.Tools.Ext.ToDateString() -> Ext.ToDateString() (demo
            # date default = today); Newtouch...NewOperatorProvider.Provider
            # -> Provider (current user in views, phase-1 pattern)
            txt = txt.replace("@Ext.ToDateString()",
                              '@DateTime.Now.ToString("yyyy-MM-dd")')
            # BARE Provider only: never rewrite inside OperatorProvider
            # (a plain replace here once produced OperatorOperatorProvider)
            txt = re.sub(r"(?<![\w])Provider\.GetCurrent\(\)",
                         "OperatorProvider.GetCurrent()", txt)
            if ("OperatorProvider.GetCurrent()" in txt
                    and "Lib.Framework.Operator" not in txt):
                txt = "@using NewtouchHIS.Lib.Framework.Operator;\n" + txt
            # Framework SiteUrl helper -> Core MvcHelper (same static-resource API)
            txt = txt.replace("SiteUrl.GetStaticResourceScriptUrl", "MvcHelper.GetStaticResourceScriptUrl")
            # Chinese grid locales -> Vietnamese ones (never ship cn/zh locales)
            txt = txt.replace("grid.locale-cn.js", "grid.locale-vi.js")
            txt = txt.replace("bootstrap-table-zh-CN.min.js", "bootstrap-table-vi-VN.js")
            # Framework report-server config -> empty stub (SSRS host absent)
            txt = re.sub(r"ConfigurationHelper\.GetAppConfigValue\(\"[^\"]*\"\)", '""', txt)
            # Binary-only Newtouch.Tools.Comm.CreateNo (backup-name default) ->
            # timestamp default (field is user-editable; value is a suggestion)
            txt = txt.replace("Comm.CreateNo()", 'DateTime.Now.ToString("yyyyMMddHHmmssfff")')
            # Audit-status enum mangled to bare DS by the strip above; restore
            # qualified form (enum copied to Infra with VI descriptions)
            txt = txt.replace("DS.ToDescSelectList()",
                              "Newtouch.Sett.Domain.EnumOrgshzt.DS.ToDescSelectList()")
            # Binary FrameworkBase org entity -> merged source-true copy
            txt = txt.replace("FrameworkBase.MultiOrg.Domain.Entity.SysOrganizeVEntity",
                              "Newtouch.Sett.Domain.SysOrganizeVEntity")
            # bundling helpers do not exist in Core
            if "@Scripts.Render" in txt or "@Styles.Render" in txt:
                print(f"  BUNDLE-HIT: {rel}")
            if "@Html.Action(" in txt or "@Html.RenderAction(" in txt:
                print(f"  CHILDACTION-HIT: {rel}")
            open(dp, "w", encoding="utf-8").write(txt)
            copied.append(rel)
    print(f"views copied: {len(copied)}")
    return copied


def check_view_refs():
    """Verify Layout / Partial / View() targets exist (case-sensitive)."""
    all_views = set()
    for root, _d, files in os.walk(os.path.join(WEBROOT, "Views")):
        for fn in files:
            if fn.endswith(".cshtml"):
                all_views.add(os.path.relpath(os.path.join(root, fn), WEBROOT))
    for root, _d, files in os.walk(os.path.join(DST, "Views")):
        for fn in files:
            if fn.endswith(".cshtml"):
                all_views.add(os.path.relpath(os.path.join(root, fn), WEBROOT))
    missing = []
    for root, _d, files in os.walk(os.path.join(DST, "Views")):
        for fn in files:
            if not fn.endswith(".cshtml"):
                continue
            p = os.path.join(root, fn)
            txt = open(p, encoding="utf-8", errors="ignore").read()
            rel = os.path.relpath(p, WEBROOT)
            for mm in re.finditer(r'Layout\s*=\s*"([^"]+)"', txt):
                lay = mm.group(1).lstrip("~/")
                if lay and lay not in all_views:
                    missing.append(f"{rel}: Layout missing {lay}")
            for mm in re.finditer(r'(?:Html\.)?(?:Partial|RenderPartial)\("([^"]+)"', txt):
                part = mm.group(1)
                cands = [f"Areas/{AREA}/Views/Shared/{part}.cshtml",
                         f"Areas/{AREA}/Views/{os.path.basename(os.path.dirname(p))}/{part}.cshtml",
                         f"Views/Shared/{part}.cshtml"]
                if part.endswith(".cshtml"):
                    cands = [part.lstrip("~/")]
                if not any(c in all_views for c in cands):
                    missing.append(f"{rel}: Partial missing {part}")
    # controller View("X") checks
    ctrl_dir = os.path.join(DST, "Controllers")
    if os.path.isdir(ctrl_dir):
        for fn in os.listdir(ctrl_dir):
            if not fn.endswith(".cs"):
                continue
            txt = open(os.path.join(ctrl_dir, fn), encoding="utf-8").read()
            ctrl = fn[:-len("Controller.cs")]
            for mm in re.finditer(r'return View\("([^"]+)"', txt):
                v = mm.group(1)
                if v.startswith("~/") or "/" in v:
                    continue
                if f"Areas/{AREA}/Views/{ctrl}/{v}.cshtml" not in all_views:
                    missing.append(f"{fn}: View missing {ctrl}/{v}")
    for mm_ in missing:
        print("  GAP:", mm_)
    print(f"view-ref gaps: {len(missing)}")
    return missing


if __name__ == "__main__":
    all_notes = {}
    cdir = os.path.join(SRC, "Controllers")
    for fn in sorted(os.listdir(cdir)):
        if fn.endswith(".cs"):
            all_notes[fn] = port_controller(fn)
    port_views()
    check_view_refs()
    print("DONE", AREA)
