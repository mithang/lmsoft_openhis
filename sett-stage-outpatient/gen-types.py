#!/usr/bin/env python3
"""Generate Newtouch.Sett.Domain Types.cs: every Capitalized type referenced by
OutpatientManage controllers that is not already defined / framework / Lib.
Props are inferred from `var.Prop`, `new T { Prop = }`, and param declarations."""
import re, glob, collections, os

CDIR = "/home/hatch/workspace/openhis-core/sett-stage-outpatient/Newtouch.Sett.Web/Areas/OutpatientManage/Controllers"
INFRA = "/home/hatch/workspace/openhis-core/sett-stage-outpatient/Newtouch.Sett.Infrastructure/Domain"

defined = set()
for f in glob.glob(INFRA + "/*.cs"):
    txt = open(f, encoding="utf-8").read()
    defined |= set(re.findall(r"(?:class|interface|enum|struct)\s+(\w+)", txt))
defined |= {"Pagination", "AjaxResult", "ControllerBase", "OrgControllerBase", "OperatorModel",
            "HttpContext", "IActionResult", "JsonResult", "ActionResult", "FileResult",
            "string", "int", "decimal", "bool", "DateTime", "object", "List", "Dictionary",
            "IList", "IEnumerable", "IDictionary", "String", "Int32", "Decimal", "Boolean",
            "Guid", "TimeSpan", "Exception", "FailedException", "FailedCodeException"}
# Lib / framework types used by controllers (resolved via usings / base class)
defined |= {"OperatorProvider", "UserIdentity"}

texts = {}
for f in sorted(glob.glob(CDIR + "/*.cs")):
    texts[f] = open(f, encoding="utf-8").read()

# candidate types from signatures / new / casts / generics
cands = set()
for txt in texts.values():
    for m in re.finditer(r"public\s+(?:IActionResult|string|void)\s+\w+\(([^)]*)\)", txt):
        for p in m.group(1).split(","):
            p = p.strip()
            p = re.sub(r"=[^,]+$", "", p).strip()
            parts = p.split()
            if len(parts) >= 2:
                t = parts[-2]
                t = re.sub(r"^(List|IList|IEnumerable)<(.+)>$", r"\2", t)
                t = t.rstrip("[]?")
                if re.match(r"^[A-Z]\w*$", t): cands.add(t)
    for m in re.finditer(r"new\s+([A-Z]\w*)\s*[\(\{]", txt): cands.add(m.group(1))
    for m in re.finditer(r"\(([A-Z]\w*)\)\s*\w", txt): cands.add(m.group(1))
    for m in re.finditer(r"<([A-Z]\w*)>", txt): cands.add(m.group(1))
    for m in re.finditer(r"^\s*(?:private|public|var)?\s*([A-Z]\w*(?:<[^>]+>)?)\s+\w+\s*=", txt, flags=re.M):
        pass
missing = sorted(c for c in cands if c not in defined)
print("candidate missing types:", missing)

# var -> type mapping per file (params + locals declared as Type name)
props = collections.defaultdict(set)   # type -> props
prop_kind = collections.defaultdict(dict)  # (type, prop) -> kind: string/decimal/bool/list
for f, txt in texts.items():
    var_type = {}
    # action params
    for m in re.finditer(r"public\s+(?:IActionResult|string|void)\s+\w+\(([^)]*)\)", txt):
        for p in m.group(1).split(","):
            p = p.strip(); p = re.sub(r"=[^,]+$", "", p).strip()
            parts = p.split()
            if len(parts) >= 2:
                t = parts[-2].rstrip("[]?")
                t = re.sub(r"^(?:List|IList|IEnumerable)<(.+)>$", r"\1", t)
                var_type[parts[-1]] = t
    # local declarations: Type name = / Type name;
    for m in re.finditer(r"\b([A-Z]\w*)\s+(\w+)\s*(?:=|;)", txt):
        if m.group(1) not in ("return",):
            var_type.setdefault(m.group(2), m.group(1))
    # out declarations: out Type name
    for m in re.finditer(r"\bout\s+([A-Z]\w*)\s+(\w+)", txt):
        var_type[m.group(2)] = m.group(1)
    # new T { Prop = ... }
    for m in re.finditer(r"new\s+([A-Z]\w*)\s*\{([^}]*)\}", txt):
        for pm in re.finditer(r"(\w+)\s*=", m.group(2)):
            props[m.group(1)].add(pm.group(1))
    # var.Prop usages
    for var, t in var_type.items():
        for m in re.finditer(r"\b" + re.escape(var) + r"\.(\w+)", txt):
            props[t].add(m.group(1))
    # lambda element props for List<T> vars: var is List<T> handled via param types above (var_type maps to element type for List<> params? no: param type List<T> -> var_type[var]=T after regex strip) -- ok

# prop kind inference from textual contexts
for f, txt in texts.items():
    var_type = {}
    for m in re.finditer(r"public\s+(?:IActionResult|string|void)\s+\w+\(([^)]*)\)", txt):
        for p in m.group(1).split(","):
            p = p.strip(); p = re.sub(r"=[^,]+$", "", p).strip()
            parts = p.split()
            if len(parts) >= 2:
                t = parts[-2].rstrip("[]?")
                t = re.sub(r"^(?:List|IList|IEnumerable)<(.+)>$", r"\1", t)
                var_type[parts[-1]] = t
    for m in re.finditer(r"\b([A-Z]\w*)\s+(\w+)\s*(?:=|;)", txt):
        var_type.setdefault(m.group(2), m.group(1))
    for var, t in var_type.items():
        for m in re.finditer(r"\b" + re.escape(var) + r"\.(\w+)\s*(==|!=|>|<|>=|<=|\+|\-|\*|/)", txt):
            prop, op = m.group(1), m.group(2)
            after = txt[m.end():m.end() + 30]
            if after.lstrip().startswith('"'):
                prop_kind[(t, prop)] = "string"
            elif re.match(r"\s*\d", after):
                prop_kind[(t, prop)] = "decimal"
        for m in re.finditer(r"\b" + re.escape(var) + r"\.(\w+)\s*=\s*\"[^\"]*\"", txt):
            prop_kind[(t, m.group(1))] = "string"
        for m in re.finditer(r"if\s*\(\s*" + re.escape(var) + r"\.(\w+)\s*\)", txt):
            prop_kind[(t, m.group(1))] = "bool"

# Types that must NOT be generated (handled elsewhere / Lib / helpers)
SKIP = {"S25ResponseDTO", "S24ResponseDTO", "CqybGjbmInfoVo", "BespeakRegisterParamDTO",
        "MzBespeakRegisterProcess", "PrescriptionChargeStatusUpdateRequestDTO", "RefJson",
        "CheckApplicationfromDTO", "APIRequestHelper", "SiteCISAPIHelper", "SitePDSAPIHelper",
        "AppLogger", "LogCore", "Tools", "JavaScriptJsonSerializerHelper", "BasicInfoDto2018",
        "OutGridInfoDto2018"}
# BasicInfoDto2018/OutGridInfoDto2018 are real DTOs used heavily; generate them too (remove from SKIP)
SKIP.discard("BasicInfoDto2018"); SKIP.discard("OutGridInfoDto2018")

out = ["using System;", "using System.Collections.Generic;", "", "namespace Newtouch.Sett.Domain", "{"]
for t in missing:
    if t in SKIP: continue
    out.append(f"    public class {t}")
    out.append("    {")
    for p in sorted(props.get(t, set())):
        k = prop_kind.get((t, p), "string")
        ct = {"string": "string", "decimal": "decimal", "bool": "bool"}.get(k, "string")
        out.append(f"        public {ct} {p} {{ get; set; }}")
    out.append("    }")
    out.append("")
out.append("}")
open(os.path.join(INFRA, "Types.cs"), "w", encoding="utf-8").write("\n".join(out))
print("Types.cs written with", len([t for t in missing if t not in SKIP]), "types")
for t in missing:
    if t not in SKIP and props.get(t):
        print(" ", t, sorted(props[t]))
