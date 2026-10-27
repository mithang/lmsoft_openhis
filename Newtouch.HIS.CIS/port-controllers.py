"""Port CIS.Web controllers (MVC5 -> ASP.NET Core 6) + generate DI ctors."""
import os, re, sys

SRC = "/home/hatch/workspace/lmsoft_openhis_run/Newtouch.HIS.CIS/Newtouch.CIS.Web"
DST = "/home/hatch/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web"
SKIP = {"LoginController.cs", "ErrorController.cs", "HomeController.cs"}

USING_MAP = [
    ("using System.Web.Mvc;", "using Microsoft.AspNetCore.Mvc;"),
    ("using System.Web;", ""),
    ("using System.Web.Services;", ""),
    ("using System.Configuration;", ""),
    ("using FrameworkBase.MultiOrg.Web;", "using NewtouchHIS.Framework.Web.Controllers;"),
    ("using FrameworkBase.MultiOrg.Domain.Entity;", ""),
    ("using FrameworkBase.MultiOrg.Domain.IRepository;", "using Newtouch.Domain.IRepository;"),
    ("using FrameworkBase.MultiOrg.Domain.IDomainServices;", "using Newtouch.Domain.IDomainServices;"),
    ("using FrameworkBase.MultiOrg.Infrastructure;", ""),
    ("using FrameworkBase.MultiOrg.Repository;", "using Newtouch.CIS.Repository;"),
    ("using FrameworkBase.MultiOrg.Application;", ""),
    ("using Newtouch.Common;", ""),
    ("using Newtouch.Common.Operator;", "using NewtouchHIS.Lib.Framework.Operator;"),
    ("using Newtouch.Common.Model;", "using NewtouchHIS.Lib.Base.Model;"),
    ("using Newtouch.Core.Common;", ""),
    ("using Newtouch.Core.Common.Exceptions;", "using Newtouch.CIS.Compat;"),
    ("using Newtouch.Core.Common.Utils;", "using Newtouch.CIS.Compat;"),
    ("using Newtouch.Tools;", "using Newtouch.CIS.Compat;"),
    ("using Newtouch.HIS.Web.Core.Attributes;", "using NewtouchHIS.Lib.Framework.Attributes;"),
    ("using Newtouch.HIS.Web.Core;", ""),
    ("using System.Drawing;", ""),
    ("using System.Drawing.Imaging;", ""),
]
STANDARD_USINGS = [
    "using NewtouchHIS.Framework.Web.Controllers;",
    "using NewtouchHIS.Lib.Framework.Operator;",
    "using NewtouchHIS.Lib.Base.Model;",
    "using NewtouchHIS.Lib.Base.Extension;",
    "using Newtouch.CIS.Compat;",
    "using Newtouch.Application.Interface;",
    "using Newtouch.Domain.Entity;",
    "using Newtouch.Domain.IDomainServices;",
    "using Newtouch.Domain.IRepository;",
    "using Newtouch.CIS.Repository;",
]
QUAL_MAP = [
    ("FrameworkBase.MultiOrg.Domain.IDomainServices.IBaseDataDmnService", "IBaseDataDmnService"),
    ("FrameworkBase.MultiOrg.Domain.IRepository.ISysConfigRepo", "ISysConfigRepo"),
    ("FrameworkBase.MultiOrg.Domain.IRepository.ISysStaffRepo", "ISysStaffRepo"),
    ("FrameworkBase.MultiOrg.Web.Controllers.ClientsDataController", "ClientsDataBaseController"),
    ("Newtouch.Core.Common.Utils.ConfigurationHelper.GetAppConfigValue", "Newtouch.CIS.Compat.AppConfig.Get"),
    ("Newtouch.Core.Common.Exceptions.FailedException", "Newtouch.CIS.Compat.FailedException"),
]

def simple_name(t):
    return t.strip().split(".")[-1]

def is_service_type(t):
    s = simple_name(t)
    return bool(re.match(r"^I[A-Z]", s))

def port_file(src_path, dst_path, area):
    with open(src_path, encoding="utf-8-sig") as f:
        s = f.read()
    # --- using replacements ---
    for old, new in USING_MAP:
        s = s.replace(old, new)
    # drop empty using lines left behind
    s = re.sub(r"(?m)^using\s*;\s*$", "", s)
    # --- qualified names ---
    for old, new in QUAL_MAP:
        s = s.replace(old, new)
    # --- member replacements ---
    s = re.sub(r"\bActionResult\b", "IActionResult", s)
    s = s.replace(", JsonRequestBehavior.AllowGet)", ")")
    s = s.replace(", JsonRequestBehavior.DenyGet)", ")")
    s = s.replace("HttpNotFound()", "NotFound()")
    s = s.replace("Request.Files", "Request.Form.Files")
    s = s.replace('Request["', 'Request.Query["')
    s = s.replace("HttpPostedFileBase", "Microsoft.AspNetCore.Http.IFormFile")
    # base class: plain MVC Controller -> local ControllerBase
    s = re.sub(r"(:\s*)Controller(\s*\{)", r"\1ControllerBase\2", s)
    # --- area attribute ---
    if area:
        s = re.sub(r"(?m)^(\s*)public class (\w+Controller)",
                   r'\1[Area("' + area + r'")]\n\1public class \2', s, count=1)
    # --- standard usings (dedup) ---
    existing = set(re.findall(r"(?m)^using [^;]+;", s))
    add = [u for u in STANDARD_USINGS if u not in existing]
    if add:
        m = re.search(r"(?m)^using [^;]+;\s*$", s)
        if m:
            s = s[:m.end()] + "\n" + "\n".join(add) + s[m.end():]
        else:
            s = "\n".join(add) + "\n" + s
    # --- DI ctor generation ---
    s = gen_ctor(s)
    os.makedirs(os.path.dirname(dst_path), exist_ok=True)
    with open(dst_path, "w", encoding="utf-8") as f:
        f.write(s)
    return s

def gen_ctor(s):
    # find class declaration
    cm = re.search(r"public class (\w+Controller)\b[^{]*\{", s)
    if not cm:
        return s
    cls = cm.group(1)
    body_start = cm.end()
    # collect readonly service fields declared before first method (scan whole class for simplicity)
    fields = []  # (type_simple, field_name)
    for fm in re.finditer(r"private\s+readonly\s+([\w\.]+(?:<[^>]+>)?)\s+(_\w+)\s*;", s):
        t, fn = fm.group(1), fm.group(2)
        if is_service_type(t):
            fields.append((simple_name(t), fn))
    if not fields:
        return s
    # existing ctor?
    ctor_re = re.compile(r"public\s+" + re.escape(cls) + r"\s*\(([^)]*)\)\s*\{")
    ctm = ctor_re.search(s)
    if ctm:
        params = ctm.group(1)
        # fields already assigned in ctor body: find matching closing brace roughly (next 2000 chars)
        seg = s[ctm.end():ctm.end() + 3000]
        assigned = set(re.findall(r"(?:this\.)?(_\w+)\s*=", seg))
        missing = [(t, fn) for (t, fn) in fields if fn not in assigned]
        if not missing:
            return s
        new_params = []
        for (t, fn) in missing:
            pname = fn.lstrip("_")
            pname = pname[0].lower() + pname[1:] if pname else pname
            new_params.append(f"{t} {pname}")
        merged = (params.strip() + ", " + ", ".join(new_params)) if params.strip() else ", ".join(new_params)
        assigns = "".join(f"\n            this.{fn} = {fn.lstrip('_')[0].lower() + fn.lstrip('_')[1:]};" for (_, fn) in missing)
        s = s[:ctm.start(1)] + merged + s[ctm.end(1):]
        # insert assignments right after the ctor opening brace
        ctm2 = ctor_re.search(s)
        s = s[:ctm2.end()] + assigns + s[ctm2.end():]
        return s
    # no ctor: generate one after the last field declaration
    last_field = None
    for fm in re.finditer(r"private\s+readonly\s+[\w\.]+(?:<[^>]+>)?\s+_\w+\s*;", s):
        last_field = fm
    if not last_field:
        return s
    params = []
    assigns = []
    for (t, fn) in fields:
        pname = fn.lstrip("_")
        pname = pname[0].lower() + pname[1:] if pname else pname
        params.append(f"{t} {pname}")
        assigns.append(f"            this.{fn} = {pname};")
    ctor = ("\n\n        public " + cls + "(" + ", ".join(params) + ")\n        {\n"
            + "\n".join(assigns) + "\n        }")
    s = s[:last_field.end()] + ctor + s[last_field.end():]
    return s

def main():
    jobs = []
    for fn in sorted(os.listdir(os.path.join(SRC, "Controllers"))):
        if fn.endswith(".cs") and fn not in SKIP:
            jobs.append((os.path.join(SRC, "Controllers", fn),
                         os.path.join(DST, "Controllers", fn), None))
    areas_dir = os.path.join(SRC, "Areas")
    for area in sorted(os.listdir(areas_dir)):
        cdir = os.path.join(areas_dir, area, "Controllers")
        if not os.path.isdir(cdir):
            continue
        for fn in sorted(os.listdir(cdir)):
            if fn.endswith(".cs"):
                jobs.append((os.path.join(cdir, fn),
                             os.path.join(DST, "Areas", area, "Controllers", fn), area))
    for src, dst, area in jobs:
        port_file(src, dst, area)
        print(("AREA " + area + " " if area else "ROOT ") + os.path.basename(src))
    print("TOTAL:", len(jobs))

main()
