#!/usr/bin/env python3
"""Mechanical port: Base MVC5 web -> ASP.NET Core 6.0 web (Base web, port 5238).
Adapted from port-base-web.py with layout-insert fixes (jQuery FIRST, vi tags
appended AFTER the closing tag, not before it -- the HOSP script's inserts broke
tags and wiped jQuery plugins; see HOSP fix notes).
Copies controllers + views from the run copy (read-only) into the new web project,
applying namespace/using/API transforms. LoginController is NOT ported (replaced by
the MRQC-pattern LoginBaseController version, written by hand).
"""
import os
import re
import shutil

SRC = os.path.expanduser("~/workspace/lmsoft_openhis_run/Newtouch.HIS.Base/Newtouch.HIS.Base")
DST = os.path.expanduser("~/workspace/openhis-core/Newtouch.HIS.Base/Newtouch.HIS.Base.Web")
MRQC = os.path.expanduser("~/workspace/openhis-core/Newtouch.MRQC.Web")

SKIP_CTRL_FILES = {"LoginController.cs"}
SKIP_DIRS = {"obj", "bin", "Properties"}


def read_text(path):
    raw = open(path, "rb").read()
    for enc in ("utf-8-sig", "utf-8", "gbk"):
        try:
            return raw.decode(enc).replace("\r\n", "\n").replace("\r", "\n")
        except UnicodeDecodeError:
            continue
    return raw.decode("utf-8", errors="replace").replace("\r\n", "\n")


def transform_usings(lines):
    out = []
    for ln in lines:
        s = ln.strip()
        if re.match(r"using\s+System\.Web(\.|;)", s):
            if s == "using System.Web.Mvc;":
                out.append("using Microsoft.AspNetCore.Mvc;")
            continue
        if re.match(r"using\s+System\.Data\.Entity(\.|;)", s):
            continue
        if re.match(r"using\s+FrameworkBase", s):
            continue
        if re.match(r"using\s+Newtouch\.HIS\.Web\.Core", s):
            continue
        if s == "using Newtouch.Common.Operator;":
            out.append("using NewtouchHIS.Lib.Framework.Operator;")
            continue
        if s == "using Newtouch.Common.Exceptions;":
            out.append("using NewtouchHIS.Lib.Base.Exceptions;")
            continue
        if s == "using Newtouch.Common.Model;":
            # Tree models now live in Newtouch.Tools (Shims/TreeShims.cs)
            out.append("using Newtouch.Tools;")
            continue
        if s == "using Newtouch.Common.Web;":
            out.append("using NewtouchHIS.Lib.Base.Utilities;")
            continue
        if s == "using Newtouch.Common;":
            continue
        if s == "using Newtouch.Core.Common.Exceptions;":
            out.append("using NewtouchHIS.Lib.Base.Exceptions;")
            continue
        if s == "using Newtouch.Core.Common.Security;":
            out.append("using NewtouchHIS.Lib.Base.Security;")
            continue
        if re.match(r"using\s+Newtouch\.Core\.Common\.(Utils|Interface)", s):
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


def transform_controller(text, area):
    text = re.sub(r"namespace Newtouch\.HIS\.Base\b",
                  "namespace Newtouch.HIS.Base.Web", text)
    # local ControllerBase lives in Newtouch.HIS.Base.Web (Controllers/ControllerBase.cs).
    # Framework BaseController -> local ControllerBase (keeps OrganizeId + HandlerLogin).
    text = re.sub(r":\s*BaseController\b", ": ControllerBase", text)
    text = re.sub(r"\bActionResult\b", "IActionResult", text)
    text = re.sub(r",\s*JsonRequestBehavior\.(AllowGet|DenyGet)", "", text)
    # Handler attributes -> Lib.Framework namespaces
    if re.search(r"\[Handler(AjaxOnly|Authorize|Login)", text):
        if "using NewtouchHIS.Lib.Framework.Filter;" not in text:
            text = re.sub(r"(using [^;]+;\n)", r"\1using NewtouchHIS.Lib.Framework.Filter;\n", text, count=1)
        if "using NewtouchHIS.Lib.Framework.Attributes;" not in text:
            text = re.sub(r"(using [^;]+;\n)", r"\1using NewtouchHIS.Lib.Framework.Attributes;\n", text, count=1)
    # Lib.Base.Model types used without the Tools using
    if re.search(r"\b(Pagination|OperatorModel|AjaxResult|ResultType)\b", text) \
            and "using NewtouchHIS.Lib.Base.Model;" not in text:
        text = re.sub(r"(using [^;]+;\n)", r"\1using NewtouchHIS.Lib.Base.Model;\n", text, count=1)
    if "WebHelper." in text and "using NewtouchHIS.Lib.Base.Utilities;" not in text:
        text = re.sub(r"(using [^;]+;\n)", r"\1using NewtouchHIS.Lib.Base.Utilities;\n", text, count=1)
        text = text.replace("WebHelper.", "HttpWebHelper.")
    elif "WebHelper." in text:
        text = text.replace("WebHelper.", "HttpWebHelper.")
    if re.search(r"\bFirstSecond\b", text) \
            and "using Newtouch.HIS.Domain;" not in text:
        text = re.sub(r"(using [^;]+;\n)", r"\1using Newtouch.HIS.Domain;\n", text, count=1)
    if area:
        # add [Area("X")] above the class declaration if missing
        if '[Area("' not in text:
            text = re.sub(r"(public\s+class\s+\w+Controller)",
                          '[Area("%s")]\n    \\1' % area, text, count=1)
    return text


def port_controllers():
    n = 0
    for sub, area in (("Controllers", None),):
        src_root = os.path.join(SRC, sub)
        for root, dirs, files in os.walk(src_root):
            dirs[:] = [d for d in dirs if d not in SKIP_DIRS]
            for f in files:
                if not f.endswith(".cs") or f in SKIP_CTRL_FILES or f.endswith(".Designer.cs"):
                    continue
                src = os.path.join(root, f)
                rel = os.path.relpath(src, src_root)
                dst = os.path.join(DST, sub, rel)
                os.makedirs(os.path.dirname(dst), exist_ok=True)
                text = read_text(src)
                text = transform_controller("\n".join(transform_usings(text.split("\n"))), area)
                open(dst, "w", encoding="utf-8").write(text)
                n += 1
    areas_root = os.path.join(SRC, "Areas")
    if not os.path.isdir(areas_root):
        print(f"controllers: {n} files")
        return n
    for area in sorted(os.listdir(areas_root)):
        src_root = os.path.join(areas_root, area, "Controllers")
        if not os.path.isdir(src_root):
            continue
        for root, dirs, files in os.walk(src_root):
            dirs[:] = [d for d in dirs if d not in SKIP_DIRS]
            for f in files:
                if not f.endswith(".cs") or f in SKIP_CTRL_FILES or f.endswith(".Designer.cs") \
                        or "AreaRegistration" in f:
                    continue
                src = os.path.join(root, f)
                rel = os.path.relpath(src, src_root)
                dst = os.path.join(DST, "Areas", area, "Controllers", rel)
                os.makedirs(os.path.dirname(dst), exist_ok=True)
                text = read_text(src)
                text = transform_controller("\n".join(transform_usings(text.split("\n"))), area)
                open(dst, "w", encoding="utf-8").write(text)
                n += 1
    print(f"controllers: {n} files")
    return n


def port_views():
    n = 0
    for sub in ("Views",):
        src_root = os.path.join(SRC, sub)
        for root, dirs, files in os.walk(src_root):
            dirs[:] = [d for d in dirs if d not in SKIP_DIRS]
            for f in files:
                if not f.endswith(".cshtml"):
                    continue
                src = os.path.join(root, f)
                rel = os.path.relpath(src, src_root)
                dst = os.path.join(DST, sub, rel)
                os.makedirs(os.path.dirname(dst), exist_ok=True)
                text = read_text(src)
                text = text.replace("SiteUrl.GetStaticResourceScriptUrl", "MvcHelper.GetStaticResourceScriptUrl")
                text = text.replace("Newtouch.Common.Operator", "NewtouchHIS.Lib.Framework.Operator")
                text = text.replace("Newtouch.Core.Common.Utils.ConfigurationHelper", "Newtouch.Tools.ConfigurationHelper")
                open(dst, "w", encoding="utf-8").write(text)
                n += 1
    areas_root = os.path.join(SRC, "Areas")
    for area in sorted(os.listdir(areas_root)) if os.path.isdir(areas_root) else []:
        src_root = os.path.join(areas_root, area, "Views")
        if not os.path.isdir(src_root):
            continue
        for root, dirs, files in os.walk(src_root):
            dirs[:] = [d for d in dirs if d not in SKIP_DIRS]
            for f in files:
                if not f.endswith(".cshtml"):
                    continue
                src = os.path.join(root, f)
                rel = os.path.relpath(src, src_root)
                dst = os.path.join(DST, "Areas", area, "Views", rel)
                os.makedirs(os.path.dirname(dst), exist_ok=True)
                text = read_text(src)
                text = text.replace("SiteUrl.GetStaticResourceScriptUrl", "MvcHelper.GetStaticResourceScriptUrl")
                text = text.replace("Newtouch.Common.Operator", "NewtouchHIS.Lib.Framework.Operator")
                text = text.replace("Newtouch.Core.Common.Utils.ConfigurationHelper", "Newtouch.Tools.ConfigurationHelper")
                open(dst, "w", encoding="utf-8").write(text)
                n += 1
    # _crossDomain: Framework-only (HttpContext.Current) -> empty partial (MRQC pattern)
    for p in (os.path.join(DST, "Views/Shared/_crossDomain.cshtml"),):
        if os.path.exists(p):
            open(p, "w", encoding="utf-8").write("@* cross-domain document.domain shim: not needed standalone; empty by design *@\n")
    # Error view: drop MVC5 HandleErrorInfo model (MRQC pattern)
    mrqc_err = os.path.join(MRQC, "Views/Shared/Error.cshtml")
    if os.path.exists(mrqc_err):
        shutil.copyfile(mrqc_err, os.path.join(DST, "Views/Shared/Error.cshtml"))
    # layouts: jQuery core first (standalone pages have no portal frame) + vi locale + modal override
    for layout in ("Views/Shared/_Index.cshtml", "Views/Shared/_Form.cshtml",
                   "Views/Shared/_Layout.cshtml", "Views/Shared/_LayoutIndex.cshtml"):
        p = os.path.join(DST, layout)
        if not os.path.exists(p):
            continue
        t = open(p, encoding="utf-8").read()
        if "jquery-2.1.1.min.js" not in t and "jquery.choose.js" in t:
            # jQuery FIRST (before all plugins) -- end-of-head placement wipes
            # plugin registrations at $(document).ready() time (proven on HOSP).
            t = re.sub(r'(.*jquery\.choose\.js.*\n)',
                       '    <script src="@MvcHelper.GetStaticResourceScriptUrl(\\"~/js/jquery/jquery-2.1.1.min.js\\")"></script>\n\\1',
                       t, count=1)
        if "grid.locale-vi.js" not in t and "grid.locale-cn.js" in t:
            t = t.replace('grid.locale-cn.js")"></script>',
                          'grid.locale-cn.js")"></script>\n    <script src="/js/jqgrid/grid.locale-vi.js"></script>', 1)
        if "baseweb-vi.js" not in t and "framework-ui.js" in t:
            t = t.replace('framework-ui.js")"></script>',
                          'framework-ui.js")"></script>\n    <script src="/js/baseweb-vi.js"></script>', 1)
        open(p, "w", encoding="utf-8").write(t)
    # _ViewImports under Views/ and each Areas/<A>/Views/
    vi = ("@using NewtouchHIS.Lib.Base.Utilities\n"
          "@using NewtouchHIS.Lib.Base\n"
          "@using Newtouch.HIS.Web.Core.Models\n"
          "@using Newtouch.HIS.Web.Core.Extensions\n"
          "@using NewtouchHIS.Lib.Base.Extension\n"
          "@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers\n")
    open(os.path.join(DST, "Views/_ViewImports.cshtml"), "w", encoding="utf-8").write(vi)
    for area in sorted(os.listdir(os.path.join(DST, "Areas"))) if os.path.isdir(os.path.join(DST, "Areas")) else []:
        d = os.path.join(DST, "Areas", area, "Views")
        if os.path.isdir(d):
            open(os.path.join(d, "_ViewImports.cshtml"), "w", encoding="utf-8").write(vi)
    print(f"views: {n} files")
    return n


def port_static():
    # page assets referenced as ~/Content/... -> wwwroot/Content/...
    src = os.path.join(SRC, "Content")
    dst = os.path.join(DST, "wwwroot/Content")
    if os.path.isdir(src):
        if os.path.exists(dst):
            shutil.rmtree(dst)
        shutil.copytree(src, dst)
    # jqGrid vi locale + modal chrome override (MRQC approach)
    os.makedirs(os.path.join(DST, "wwwroot/js/jqgrid"), exist_ok=True)
    shutil.copyfile(os.path.join(MRQC, "wwwroot/js/jqgrid/grid.locale-vi.js"),
                    os.path.join(DST, "wwwroot/js/jqgrid/grid.locale-vi.js"))
    mrqc_vi = open(os.path.join(MRQC, "wwwroot/js/mrqc-vi.js"), encoding="utf-8").read()
    mrqc_vi = mrqc_vi.replace("MRQC pilot", "Base web port")
    open(os.path.join(DST, "wwwroot/js/baseweb-vi.js"), "w", encoding="utf-8").write(mrqc_vi)
    print("static: Content + grid.locale-vi.js + baseweb-vi.js")


if __name__ == "__main__":
    port_controllers()
    port_views()
    port_static()
