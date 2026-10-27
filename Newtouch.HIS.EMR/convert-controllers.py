#!/usr/bin/env python3
"""Mechanical MVC5 -> ASP.NET Core conversion for EMR Area controllers (textual pass).
Build errors after this pass are fixed manually; this script never invents logic.
"""
import pathlib, re, sys

DST = pathlib.Path.home() / "workspace/openhis-core/Newtouch.HIS.EMR/Newtouch.EMR.Web"

USING_MAP = [
    ("using System.Web.Mvc;", "using Microsoft.AspNetCore.Mvc;"),
    ("using System.Web;", ""),
    ("using FrameworkBase.MultiOrg.Web;", "using NewtouchHIS.Framework.Web.Controllers;"),
    ("using FrameworkBase.MultiOrg.Domain.IRepository;", ""),
    ("using FrameworkBase.MultiOrg.DmnService;", ""),
    ("using FrameworkBase.MultiOrg.Infrastructure;", ""),
    ("using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;", ""),
    ("using FrameworkBase.MultiOrg.Domain.Entity;", ""),
    ("using FrameworkBase.MultiOrg.Application.Interface;", ""),
    ("using FrameworkBase.MultiOrg.Repository;", ""),
    ("using FrameworkBase.API;", ""),
    ("using Newtouch.Common.Operator;", "using NewtouchHIS.Lib.Framework.Operator;"),
    ("using Newtouch.Common.Web;", ""),
    ("using Newtouch.Common;", ""),
    ("using Newtouch.Core.Common.Exceptions;", "using NewtouchHIS.Lib.Base;"),
    ("using Newtouch.Core.Common.Utils;", ""),
    ("using Newtouch.Core.Common.Interface;", ""),
    ("using Newtouch.Core.Common;", ""),
    ("using Newtouch.Core.Redis.Cache;", ""),
    ("using Newtouch.Core.Redis;", ""),
    ("using Newtouch.Tools.Net;", ""),
    ("using Newtouch.Tools;", "using NewtouchHIS.Lib.Base.Model;\nusing NewtouchHIS.Lib.Base.Extension;"),
    ("using Newtouch.Infrastructure.EF;", ""),
    ("using Newtouch.Infrastructure;", ""),
    ("using DCSoft.Writer.Dom;", ""),
    ("using DCSoft.Writer.Controls;", ""),
    ("using DCSoft.Writer;", ""),
    ("using System.Drawing;", ""),
]

def convert(path: pathlib.Path, area: str):
    t = path.read_text(encoding="utf-8-sig")
    orig = t
    for old, new in USING_MAP:
        t = t.replace(old, new)
    # collapse >2 blank lines left by removed usings
    t = re.sub(r"\n{3,}", "\n\n", t)
    # Core filter attributes live in Lib.Framework
    if "Microsoft.AspNetCore.Mvc;" in t and "Lib.Framework.Filter" not in t:
        t = t.replace("using Microsoft.AspNetCore.Mvc;",
                      "using Microsoft.AspNetCore.Mvc;\nusing NewtouchHIS.Lib.Framework.Filter;\nusing NewtouchHIS.Lib.Framework.Attributes;", 1)
    # Area attribute (MVC5 used AreaRegistration classes, Core needs the attribute)
    if area and "[Area(" not in t:
        t = re.sub(r"(?m)^(\s*public class \w+Controller)",
                   f"    [Area(\"{area}\")]\n\\1", t, count=1)
    # ActionResult -> IActionResult (return types only in practice; word replace is safe here)
    t = re.sub(r"\bActionResult\b", "IActionResult", t)
    # Request indexing
    t = t.replace('this.Request["', 'Request.ReqVal("')
    t = re.sub(r'Request\["([^"]+)"\]', r'Request.ReqVal("\1")', t)
    t = t.replace("Request.QueryString[", "Request.Query[")
    # Server.MapPath
    t = t.replace("this.Server.MapPath(", "WebPath.Map(")
    t = t.replace("Server.MapPath(", "WebPath.Map(")
    # Config / constants / helpers
    t = t.replace("ConfigurationHelper.GetAppConfigValue(", "EmrConfig.GetAppConfigValue(")
    t = t.replace("Constants.AppId", '"EMR"')
    t = t.replace("Constants.TopOrganizeId", "EmrConfig.TopOrganizeId")
    t = t.replace("EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue(", "FieldValueHelper.GetNewFieldUniqueValue(")
    t = t.replace("HttpContext.Current", "HttpContext")
    if t != orig:
        path.write_text(t, encoding="utf-8")
        return True
    return False

changed = 0
for ctrl in sorted((DST / "Areas").rglob("Controllers/*.cs")):
    area = ctrl.parts[ctrl.parts.index("Areas") + 1]
    if convert(ctrl, area):
        changed += 1
        print("converted", ctrl.relative_to(DST))
print("total changed:", changed)
