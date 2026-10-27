#!/usr/bin/env python3
"""Generate Sett service interfaces + stub implementations from controller call sites.
Return-type inference: special cases > LINQ/member usage > object.
Stub policy: write-like methods throw NotSupportedException (never fake success);
read methods return empty list / null / default."""
import re, os, glob, collections

CDIR = "/home/hatch/workspace/openhis-core/sett-stage-outpatient/Newtouch.Sett.Web/Areas/OutpatientManage/Controllers"
OUTDIR = "/home/hatch/workspace/openhis-core/sett-stage-outpatient/Newtouch.Sett.Infrastructure/Domain"
os.makedirs(OUTDIR, exist_ok=True)

def balanced(text, start):
    """start = index of '('; return (args_text, end_index_after_close)."""
    depth, i = 0, start
    instr, esc = None, False
    while i < len(text):
        c = text[i]
        if instr:
            if esc: esc = False
            elif c == "\\": esc = True
            elif c == instr: instr = None
        else:
            if c in "\"'": instr = c
            elif c == "(": depth += 1
            elif c == ")":
                depth -= 1
                if depth == 0:
                    return text[start + 1:i], i + 1
        i += 1
    return "", len(text)

def split_args(s):
    parts, depth, cur, instr, esc = [], 0, "", None, False
    for c in s:
        if instr:
            cur += c
            if esc: esc = False
            elif c == "\\": esc = True
            elif c == instr: instr = None
        else:
            if c in "\"'": instr, cur = c, cur + c
            elif c in "([{<": depth, cur = depth + 1, cur + c
            elif c in ")]}>": depth, cur = depth - 1, cur + c
            elif c == "," and depth == 0: parts.append(cur); cur = ""
            else: cur += c
    if cur.strip() or parts: parts.append(cur)
    return [p.strip() for p in parts if p.strip() != ""]

fields = {}   # controller -> [(type, name)]
calls = collections.defaultdict(list)  # (type, method) -> list of dicts
texts = {}
for f in sorted(glob.glob(CDIR + "/*.cs")):
    txt = open(f, encoding="utf-8").read()
    texts[f] = txt
    fl = re.findall(r"private readonly (\w+) (_\w+);", txt)
    fields[f] = fl
    fmap = dict((name, typ) for typ, name in fl)
    for m in re.finditer(r"(_\w+)\.(\w+)\s*\(", txt):
        fname, method = m.group(1), m.group(2)
        if fname not in fmap: continue
        args_text, end = balanced(txt, m.end() - 1)
        args = split_args(args_text)
        kinds, names = [], []
        for a in args:
            if a.startswith("out "): kinds.append("out"); names.append("")
            elif a.startswith("ref "): kinds.append("ref"); names.append("")
            elif re.match(r"^\w+\s*:", a): kinds.append("named"); names.append(a.split(":", 1)[0].strip())
            else: kinds.append(""); names.append("")
        tail = txt[end:end + 120]
        calls[(fmap[fname], method)].append({
            "file": f, "args": args, "kinds": kinds, "names": names,
            "call_end": m.end(), "tail": tail, "full": txt,
        })

# ---- return inference helpers ----
def var_uses(txt, call):
    """Find variable assigned from this call site (approx: 'var x = ...' spanning)."""
    return []

# Special return types
SPECIAL = {
    ("ISysConfigRepo", "GetBoolValueByCode"): "bool?",
    ("ISysConfigRepo", "GetValueByCode"): "string",
    ("ISysConfigRepo", "GetByCode"): "SysConfigDto",
    ("IOutpatientRegApp", "GetInvoice"): "string",
    ("ICommonDmnService", "Gzyb_GetJyyzmByJylsh"): "string",
    ("IOutBookDmnService", "getStaffName"): "string",
    ("IOutPatientSettleDmnService", "GetMzhJzxh"): "Tuple<short, string>",
    ("IOutPatientSettleDmnService", "GetCQMzhJzxh"): "Tuple<short, string>",
    ("IOutPatientSettleDmnService", "GetOrgXzqh"): "string",
    ("IOutPatientSettleDmnService", "GetRegListJson"): "List<GhJzInfo>",
    ("IOutPatientSettleDmnService", "GetCurrentDayRegList"): "List<OutPatientRegInfoVO>",
    ("IOutPatChargeApp", "submitOutpatCharge"): "bool",
    ("IPayApp", "TradeRefund"): "int",
    ("IGuiAnOutpatientXnhApp", "Sett"): "string",
    ("IOutPatChargeApp", "GetCQDetailsMzjsYbTfh"): "CqDetailsYbTfhDto",
    ("IOutpatientPrescriptionRepo", "GetValidEntityByCfnm"): "OutpatientPrescriptionEntity",
    ("IOutpatientSettlementRepo", "UpdateSettedFph"): "object",
}
# DTO element props collected from LINQ/member usage on results
dto_props = collections.defaultdict(set)   # dto name -> props
dto_prop_type = collections.defaultdict(dict)

def dto_name(svc, method):
    return re.sub(r"\W", "", svc + "_" + method + "Dto")

WRITE_PREFIX = ("Save", "Submit", "Update", "Delete", "Insert", "Remove", "Cancel", "Sett", "Refund",
                "Book", "Signin", "Trade", "Exec", "Push", "Commit", "Generate", "Create", "Add",
                "Record", "BackNum", "Return", "FZsjTB", "Cancal", "Do", "Handle", "CheckInvoice",
                "CheckBrxz", "UpdateMzGh", "update", "UpdateCharge", "UpdateSetted", "UpdateYP",
                "UpdateArrearage", "UpdatePat", "UpdateForm", "UpdateDate", "updateZt", "UpdateArrange",
                "InsertArrange", "InsertghpbTime", "SaveData", "SubmitForm", "SubmitArrange", "Submitghpb",
                "SubmitInfo", "SubmitPhysicalexam", "SubmitArrearage", "SaveOptima", "Savepatient",
                "SaveOutpatient", "SaveAcount", "SaveCqyb", "SaveChongQing", "UpDate", "DetailsUpload",
                "XnhTuiFei", "XnhSett", "GuiAnXnh", "SimulationSett", "SaveCancel", "UnSetted",
                "PatBookGh", "DeleteArrange", "DeleteForm", "SaveDatapb", "SaveDatatzcz")

inferred = {}
for (svc, method), lst in calls.items():
    if (svc, method) in SPECIAL:
        inferred[(svc, method)] = SPECIAL[(svc, method)]
        continue
    # gather usages: look at text after each call for chained LINQ / member access
    is_list, is_dto = False, False
    dn = dto_name(svc, method)
    for c in lst:
        tail = c["tail"]
        # chained: .Where(/.OrderBy(/.FirstOrDefault(/.ToList()/.Count directly on result
        if re.match(r"\s*\.\s*(Where|OrderBy|OrderByDescending|ThenBy)\s*\(", tail):
            is_list = True
            mm = re.search(r"\(\s*\w+\s*=>\s*[^)]*?\.(\w+)", tail)
            # collect all lambda props in first 300 chars
            for p in re.findall(r"=>[^;]{0,200}?\.(\w+)", tail[:300]):
                dto_props[dn].add(p)
        if re.match(r"\s*\.\s*ToList\s*\(", tail):
            is_list = True
        # assigned var: find 'var x =' or 'Type x =' preceding? approximate: search full text for
        # occurrences of the call and check following lines for 'x.Prop' -- handled below globally
    # global var-usage scan per file
    for f, txt in texts.items():
        for m in re.finditer(r"(\w+)\s*=\s*[^;]{0,400}?\b" + re.escape(method) + r"\s*\(", txt):
            var = m.group(1)
            if var in ("return", "var"): continue
            seg = txt[m.start():m.start() + 1200]
            # member accesses on var
            for p in re.findall(r"\b" + re.escape(var) + r"\.(\w+)", seg):
                if p in ("ToJson", "ToString", "Count"): continue
                is_dto = True
                dto_props[dn].add(p)
            if re.search(r"\b" + re.escape(var) + r"\s*\.\s*(Where|OrderBy|OrderByDescending)", seg):
                is_list = True
                for p in re.findall(r"=>[^;]{0,200}?\.(\w+)", seg):
                    dto_props[dn].add(p)
            if re.search(r"foreach\s*\([^)]*\b" + re.escape(var) + r"\b", seg):
                is_list = True
    if is_list:
        inferred[(svc, method)] = "List<" + dn + ">"
    elif is_dto:
        inferred[(svc, method)] = dn
    else:
        inferred[(svc, method)] = "object"

# Fix known bad inferences manually
FIX = {
    ("IOutPatientDmnService", "GetOutpatientRegistScheduleList"): "List<OutpatientRegistScheduleDto>",
    ("ISyncTreatmentServiceRecordRepo", "GetUnitList"): "List<SyncTreatmentUnitDto>",
    ("IOutPatientChargeQueryDmnService", "GetSettZffsResultList"): "List<SettZffsDto>",
    ("IOutPatientUniversalDmnService", "RefundZtDetail"): "List<RefundZtDto>",
    ("IOutPatChargeDmnService", "GetOutAccountInfo"): "List<OutAccountInfoDto>",
    ("IOutPatChargeDmnService", "GetintAccountInfo"): "List<OutAccountInfoDto>",
    ("IOutPatientDmnService", "GetRelatedYpDetailListByMzh"): "List<RelatedYpDetailDto>",
    ("IRefundDmnService", "GetGridViewMx"): "List<RefundGridMxDto>",
    ("IOutPatienChargeQueryApp", "LoadMzjsRecords"): "List<MzjsRecordDto>",
    ("IOutPatienChargeQueryApp", "LoadMzjsMXRecords"): "List<MzjsMxDto>",
    ("IOutPatChargeApp", "GetPatientGridJson"): "List<PatientGridDto>",
    ("IOutPatChargeApp", "GetChargeTemplateAll"): "List<ChargeTemplateDto>",
    ("IOutPatientDmnService", "CommitAccounting"): "object",
    ("IOutPatientDmnService", "SaveOutpatientAccountInfo"): "object",
    ("IOutPatientDmnService", "SavepatientAccountInfo"): "object",
}
for k, v in FIX.items():
    if k in inferred: inferred[k] = v
# props for fixed DTOs (from controller member usage)
FIXPROPS = {
    "OutpatientRegistScheduleDto": {"Id", "ghpbId"},
    "SyncTreatmentUnitDto": {"hidden"},
    "SettZffsDto": {"jsnm", "xjzffsmc"},
    "RefundZtDto": {"ztId", "jsmxnm", "sl"},
    "OutAccountInfoDto": {"jzsj"},
    "RelatedYpDetailDto": set(),
    "RefundGridMxDto": set(),
    "MzjsRecordDto": {"jsnm", "zffsmcstr"},
    "MzjsMxDto": {"jsnm"},
    "PatientGridDto": {"mzh", "blh", "ghrq"},
    "ChargeTemplateDto": set(),
}
for k, v in FIXPROPS.items(): dto_props[k] |= v
# MzjsRecordDto zffsmcstr is assigned (js.zffsmcstr = ...) -> must be settable string
# OutAccountInfoDto jzsj used in OrderBy -> any comparable type; keep string? OrderBy works on string too.

# ---- emit interfaces ----
services = collections.defaultdict(set)  # svc -> set of method keys
for (svc, method), lst in calls.items():
    for c in lst:
        key = (method, len(c["args"]), tuple(c["kinds"]), tuple(c["names"]))
        services[svc].add(key)

iface_lines = []
iface_lines.append("using System;\nusing System.Collections.Generic;\n\nnamespace Newtouch.Sett.Domain\n{")
for svc in sorted(services):
    iface_lines.append(f"    public interface {svc}\n    {{")
    for (method, arity, kinds, names) in sorted(services[svc]):
        ret = inferred.get((svc, method), "object")
        params = []
        for i in range(arity):
            pname = names[i] if kinds[i] == "named" and names[i] else f"p{i}"
            prefix = ""
            if kinds[i] == "out": prefix = "out "
            elif kinds[i] == "ref": prefix = "ref "
            params.append(f"{prefix}object {pname}")
        iface_lines.append(f"        {ret} {method}({', '.join(params)});")
    iface_lines.append("    }\n")

# DTO classes
for dn in sorted(set([re.sub(r"List<(.+)>", r"\1", v) for v in inferred.values() if v.startswith("List<") or (v.endswith("Dto") and v not in ("SysConfigDto", "GhJzInfo", "OutPatientRegInfoVO", "CqDetailsYbTfhDto", "OutpatientPrescriptionEntity", "Tuple<short, string>") and v != "object")])):
    pass
all_dtos = set()
for v in inferred.values():
    mm = re.match(r"List<(\w+)>", v)
    if mm: all_dtos.add(mm.group(1))
    elif v.endswith("Dto") and v not in ("SysConfigDto",): all_dtos.add(v)
for dn in sorted(all_dtos):
    props = dto_props.get(dn, set())
    iface_lines.append(f"    public class {dn}\n    {{")
    for p in sorted(props):
        iface_lines.append(f"        public object {p} {{ get; set; }}")
    iface_lines.append("    }\n")
iface_lines.append("    public class SysConfigDto { public string Value { get; set; } public string Code { get; set; } }")
iface_lines.append("    public class CqDetailsYbTfhDto { public List<CqDetailsYbTfhItemDto> cflist { get; set; } }")
iface_lines.append("    public class CqDetailsYbTfhItemDto { public string cfh { get; set; } }")
iface_lines.append("}")
open(os.path.join(OUTDIR, "Interfaces.cs"), "w", encoding="utf-8").write("\n".join(iface_lines))

# ---- emit stub implementations ----
def is_write(svc, method):
    return method.startswith(WRITE_PREFIX) or method in ("btnReturn", "btnReturnInAcc", "RecordOutpId")

impl_lines = []
impl_lines.append("using System;\nusing System.Collections.Generic;\n\nnamespace Newtouch.Sett.DomainServices\n{")
for svc in sorted(services):
    impl = svc[1:] if svc.startswith("I") else svc
    impl_lines.append(f"    public class {impl} : {svc}\n    {{")
    for (method, arity, kinds, names) in sorted(services[svc]):
        ret = inferred.get((svc, method), "object")
        params = []
        for i in range(arity):
            pname = names[i] if kinds[i] == "named" and names[i] else f"p{i}"
            prefix = ""
            if kinds[i] == "out": prefix = "out "
            elif kinds[i] == "ref": prefix = "ref "
            params.append(f"{prefix}object {pname}")
        body = ""
        # out params must be assigned
        for i in range(arity):
            if kinds[i] == "out":
                pname = names[i] if names[i] else f"p{i}"
                body += f"            {pname} = null;\n"
        if is_write(svc, method):
            body += '            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");\n'
        elif ret.startswith("List<"):
            body += f"            return new {ret}();\n"
        elif ret == "string":
            body += "            return null;\n"
        elif ret == "bool":
            body += "            return false;\n"
        elif ret == "bool?":
            body += "            return null;\n"
        elif ret == "int":
            body += "            return 0;\n"
        elif ret.startswith("Tuple<"):
            body += "            return new Tuple<short, string>(0, \"\");\n"
        elif ret == "object":
            body += "            return null;\n"
        else:
            body += f"            return new {ret}();\n"
        impl_lines.append(f"        public {ret} {method}({', '.join(params)})\n        {{\n{body}        }}\n")
    impl_lines.append("    }\n")
impl_lines.append("}")
open(os.path.join(OUTDIR, "Stubs.cs"), "w", encoding="utf-8").write("\n".join(impl_lines))

print("services:", len(services))
for svc in sorted(services):
    print(svc, len(services[svc]), "methods")
print("inferred DTOs:", sorted(all_dtos))
