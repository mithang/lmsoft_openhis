#!/usr/bin/env python3
"""Port OutpatientManage controllers Framework -> Core 6 (mechanical pass).
Keeps bodies close to original; service interfaces are generated separately.
"""
import re, os, shutil, sys

SRC = "/home/hatch/workspace/lmsoft_openhis_run/Newtouch.HIS.Sett/Newtouch.Web/Areas/OutpatientManage/Controllers"
DST = "/home/hatch/workspace/openhis-core/sett-stage-outpatient/Newtouch.Sett.Web/Areas/OutpatientManage/Controllers"
os.makedirs(DST, exist_ok=True)

HEADER = """using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;

"""

for fn in sorted(os.listdir(SRC)):
    if not fn.endswith(".cs"):
        continue
    txt = open(os.path.join(SRC, fn), encoding="utf-8-sig", errors="ignore").read()
    # strip using lines
    txt = re.sub(r"^\s*using [^;]+;\s*$", "", txt, flags=re.M)
    # namespace
    txt = re.sub(r"namespace\s+[\w\.]+", "namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers", txt, count=1)
    # class: add Area attribute
    txt = re.sub(r"(\n\s*)public class (\w+Controller) : ControllerBase",
                 r"\1[Area(\"OutpatientManage\")]\n\1public class \2 : ControllerBase", txt)
    # return types
    txt = txt.replace("public ActionResult ", "public IActionResult ")
    txt = txt.replace("public JsonResult ", "public IActionResult ")
    txt = txt.replace("Task<ActionResult>", "Task<IActionResult>")
    # drop HandlerAjaxOnly (no Core equivalent in Lib.Framework used by MRQC)
    txt = re.sub(r"\[HandlerAjaxOnly\]\s*\n", "", txt)
    # Framework helpers -> Core equivalents
    txt = txt.replace('ConfigurationHelper.GetAppConfigValue("ReportServer.HOST")', '""')
    txt = txt.replace("System.Web.HttpUtility.UrlDecode", "System.Net.WebUtility.UrlDecode")
    txt = re.sub(r'Request\.Params\["([^"]+)"\]', r'Request.Query["\1"].FirstOrDefault()', txt)
    txt = txt.replace('WebHelper.GetCookie("ExportExcelCols")', 'Request.Cookies["ExportExcelCols"]')
    txt = txt.replace('WebHelper.RemoveCookie("ExportExcelCols")', 'Response.Cookies.Delete("ExportExcelCols")')
    # System.Web.HttpContext.Current -> HttpContext (handled manually if it breaks)
    txt = txt.replace("System.Web.HttpContext.Current", "HttpContext")
    txt = HEADER + txt.lstrip("\ufeff")
    open(os.path.join(DST, fn), "w", encoding="utf-8").write(txt)
    print("ported", fn)
print("done")
