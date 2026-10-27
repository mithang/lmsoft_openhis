#!/usr/bin/env python3
"""Mechanical port of HospitalizationManage controllers Framework MVC5 -> ASP.NET Core 6.
Stubs (explicit Error, never fake success): Excel exports x2, GuiAn insurance actions x4,
SaveSettle (S14 proxy), TransfertoCYsettle (CIS), Optima check, GuiAn private helpers.
"""
import re, sys, os

SRC = "/home/hatch/workspace/lmsoft_openhis_run/Newtouch.HIS.Sett/Newtouch.Web/Areas/HospitalizationManage/Controllers"
DST = "/home/hatch/workspace/openhis-core/sett-stage-hospitalization/Newtouch.Sett.Web/Areas/HospitalizationManage/Controllers"

STUB_ACTIONS = {
    # BookkeepInHos
    "AccountingExecutionExportExcel",
    "AccountingPlanExportExcel",
    # DischargeSettle (GuiAn new-rural-cooperative insurance proxy + CIS)
    "GuiAnXnhMnjs", "GuiAnXnhSettlement", "GuiAnXnhSettCancel", "GuiAnXnhCyblCancel",
    "SaveSettle", "TransfertoCYsettle",
}
STUB_PRIVATE = {"checkOptimaZyhSettleable", "GuiAnXnhDelFeeDetails"}

HEADER = """using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.HospitalizationManage.Controllers
{
"""

def find_method_bounds(text, start):
    """start = index of method signature start; return (body_open_brace_idx, end_idx past closing brace)."""
    brace = text.index("{", start)
    depth = 0
    i = brace
    in_str = None
    while i < len(text):
        c = text[i]
        if in_str:
            if c == "\\":
                i += 2
                continue
            if c == in_str:
                in_str = None
        else:
            if c in ('"', "'"):
                in_str = c
            elif c == "{":
                depth += 1
            elif c == "}":
                depth -= 1
                if depth == 0:
                    return brace, i + 1
        i += 1
    raise ValueError("unbalanced braces")

def stub_body(sig_text, is_private=False):
    if is_private:
        return "throw new NotSupportedException(\"Tích hợp Optima/BHYT GuiAn chưa được chuyển đổi sang .NET Core trong giai đoạn này.\");"
    return "return Error(\"Chức năng này chưa được chuyển đổi sang .NET Core trong giai đoạn này (tích hợp BHYT GuiAn / xuất Excel / CIS).\");"

def port_file(name):
    with open(os.path.join(SRC, name), encoding="utf-8-sig") as f:
        text = f.read()
    # strip everything before 'namespace' (usings + BOM handled), keep from namespace's '{'
    m = re.search(r"namespace\s+[\w\.]+\s*\{", text)
    body = text[m.end():]
    # body ends with final '}' of namespace; strip last closing brace
    body = body.rstrip()
    assert body.endswith("}"), name
    body = body[:-1].rstrip() + "\n"
    # class-level: remove summary comments are kept; find class decl
    # Add [Area] attribute before 'public class XController'
    body = re.sub(r"(public class \w+Controller)",
                  r"[Area(\"HospitalizationManage\")]\n    \1", body, count=1)
    # ActionResult -> IActionResult
    body = re.sub(r"\bActionResult\b", "IActionResult", body)
    # field declarations -> collect for ctor
    fields = re.findall(r"private readonly (I\w+) (_\w+);", body)
    # build ctor text, insert before first method/region after fields block
    if fields:
        params = ", ".join(f"{t} {f[1:].lstrip('_') if False else f[1:]}" for t, f in [])
        ctor_params = []
        assigns = []
        for t, f in fields:
            pname = f[1:]
            pname = pname[0].lower() + pname[1:]
            ctor_params.append(f"{t} {pname}")
            assigns.append(f"            {f} = {pname};")
        cls = re.search(r"public class (\w+Controller)", body).group(1)
        ctor = (f"\n        public {cls}({', '.join(ctor_params)})\n        {{\n" + "\n".join(assigns) + "\n        }\n")
        # insert after last field declaration line
        last_field = list(re.finditer(r"private readonly I\w+ _\w+;", body))[-1]
        body = body[:last_field.end()] + ctor + body[last_field.end():]
    # textual replacements
    body = body.replace("OperatorProvider.GetCurrent().OrganizeId", "this.OrganizeId")
    body = body.replace("OperatorProvider.GetCurrent().UserCode", "this.UserIdentity.UserCode")
    body = re.sub(r"Tools\.Json\.ToList<(\w+)>\(([^)]+)\)", r"JsonConvert.DeserializeObject<List<\1>>(\2)", body)
    body = re.sub(r"EFDBBaseFuncHelper\.Instance\.GetNewPrimaryKeyInt\(([^)]+)\)", r"SettKeyHelper.GetNewPrimaryKeyInt(\1)", body)
    body = body.replace('ConfigurationHelper.GetAppConfigValue("ReportServer.HOST")', '""')
    # only the two grid actions returned null for empty zyh; DataTable helpers keep return null
    body = body.replace("""if (string.IsNullOrWhiteSpace(zyh))
            {
                return null;
            }""", """if (string.IsNullOrWhiteSpace(zyh))
            {
                return Error("Thiếu mã nhập viện (zyh).");
            }""")
    body = body.replace("""if (string.IsNullOrWhiteSpace(zyh))
            {
                return null;
            }
            var list""", """if (string.IsNullOrWhiteSpace(zyh))
            {
                return Error("Thiếu mã nhập viện (zyh).");
            }
            var list""")
    # fully-qualified local base (avoids ambiguity with Microsoft.AspNetCore.Mvc.ControllerBase)
    body = re.sub(r"(public class \w+Controller) : ControllerBase",
                  r"\1 : Newtouch.Sett.Web.Controllers.ControllerBase", body)
    # stub whole methods (actions + private helpers)
    for meth in list(STUB_ACTIONS) + list(STUB_PRIVATE):
        pat = re.compile(r"(public|private)[^\{;]*?\b" + re.escape(meth) + r"\s*\([^;]*?\)\s*(?:\n\s*)?\{", re.S)
        mm = pat.search(body)
        if mm:
            open_b, end = find_method_bounds(body, mm.start())
            sig = body[mm.start():open_b + 1]
            is_priv = mm.group(1) == "private"
            # for private bool checkOptima -> keep signature, stub body returns throw (bool return unreachable ok? throw satisfies)
            repl = sig + "\n            " + stub_body(sig, is_priv) + "\n        }"
            body = body[:mm.start()] + repl + body[end:]
        else:
            print(f"  WARN: method not found for stubbing: {meth} in {name}")
    out = HEADER + body + "}\n"
    with open(os.path.join(DST, name), "w", encoding="utf-8") as f:
        f.write(out)
    print(f"ported {name} fields={len(fields)}")

os.makedirs(DST, exist_ok=True)
for fn in ["BookkeepInHosController.cs", "DischargeSettleController.cs", "HospitalRefundController.cs",
           "InpatientRefundController.cs", "SettlementController.cs"]:
    port_file(fn)
