#!/usr/bin/env python3
"""Generate Sett service interfaces + stub impls + DTOs for ONE Area, registry-aware.
Usage: python3 gen-sett-services.py <AreaName>
- Scans ported controllers in Newtouch.Sett.Web/Areas/<Area>/Controllers.
- Reuses interfaces/DTOs already defined anywhere in Newtouch.Sett.Infrastructure
  (merged phase-1 files + earlier remainder Areas); appends only MISSING methods
  to the existing interface/impl files, and only MISSING props to existing DTOs.
- New interfaces -> Domain/Interfaces_<Area>.cs ; new impls -> DomainServices/Stubs_<Area>.cs
- New DTOs -> Domain/DTOs_<Area>.cs
- Also creates EMPTY interfaces for field types that are never called (so the
  ctor-injected field type still resolves).
- Prints: new interfaces, merged methods, new DTOs, and interfaces that still
  need a Program.cs AddScoped line (caller appends them, deduped).
Return-type inference: SPECIAL/FIX tables > LINQ/member usage > object.
Write-like methods throw NotSupportedException (never fake success);
read methods return empty list / null / default.
"""
import re, os, glob, collections, sys

AREA = sys.argv[1]
WEB = "/home/hatch/workspace/openhis-core/Newtouch.Sett.Web"
INF = "/home/hatch/workspace/openhis-core/Newtouch.Sett.Infrastructure"
CDIR = f"{WEB}/Areas/{AREA}/Controllers"


def balanced(text, start):
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
                if depth == 0: return text[start + 1:i], i + 1
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


# ---------- registry: what already exists in Infrastructure ----------
def brace_block_end(text, open_idx):
    depth = 0; i = open_idx; in_str = None
    while i < len(text):
        c = text[i]
        if in_str:
            if c == "\\": i += 2; continue
            if c == in_str: in_str = None
        else:
            if c in ('"', "'"): in_str = c
            elif c == "{": depth += 1
            elif c == "}":
                depth -= 1
                if depth == 0: return i
        i += 1
    return len(text) - 1


iface_file = {}      # iface -> file
iface_methods = collections.defaultdict(set)   # iface -> {(method, arity, kinds, names)}
impl_file = {}       # impl class -> file
impl_iface = {}      # impl class -> iface
dto_file = {}        # dto -> file
dto_props = collections.defaultdict(set)

for f in glob.glob(INF + "/**/*.cs", recursive=True):
    if "/obj/" in f or "/bin/" in f: continue
    txt = open(f, encoding="utf-8", errors="ignore").read()
    for m in re.finditer(r"public interface (\w+)", txt):
        iface = m.group(1)
        iface_file[iface] = f
        b = txt.find("{", m.end())
        e = brace_block_end(txt, b)
        body = txt[b:e]
        for mm in re.finditer(r"^\s*([\w<>\[\],\s\?\.\|]+?)\s+(\w+)\(([^;]*)\)\s*;", body, flags=re.M):
            ret, meth, params = mm.group(1).strip(), mm.group(2), mm.group(3).strip()
            args = split_args(params) if params else []
            kinds, names = [], []
            for a in args:
                if a.startswith("out "): kinds.append("out"); names.append(a[4:].split()[-1])
                elif a.startswith("ref "): kinds.append("ref"); names.append(a[4:].split()[-1])
                elif re.match(r"^\w+\s*:", a): kinds.append("named"); names.append(a.split(":", 1)[0].strip())
                else: kinds.append(""); names.append("")
            # NB: key EXCLUDES param names: two runs inferring different names
            # for the same call (p0.. vs call-site vars) must not re-append an
            # identical overload (CS0111 duplicates seen in Batch C).
            iface_methods[iface].add((meth, len(args), tuple(kinds)))
    for m in re.finditer(r"public class (\w+)\s*:\s*([^{\n]+)", txt):
        cls = m.group(1)
        impl_file[cls] = f
        for base in m.group(2).split(","):
            base = base.strip()
            if base.startswith("I"):
                impl_iface.setdefault(cls, set()).add(base)
    for m in re.finditer(r"public class (\w+)", txt):
        cls = m.group(1)
        if cls in impl_file or cls in iface_file: continue
        dto_file[cls] = f
        b = txt.find("{", m.end())
        if b < 0: continue
        e = brace_block_end(txt, b)
        body = txt[b:e]
        for pm in re.finditer(r"public\s+[\w<>\[\],\s\?]+\s+(\w+)\s*\{\s*get;\s*set;\s*\}", body):
            dto_props[cls].add(pm.group(1))

defined_types = set(iface_file) | set(impl_file) | set(dto_file)

# ---------- scan controllers: fields + calls ----------
texts = {}
fields_all = collections.defaultdict(list)   # file -> [(type, field)]
calls = collections.defaultdict(list)        # (type, method) -> [call dicts]
for f in sorted(glob.glob(CDIR + "/*.cs")):
    txt = open(f, encoding="utf-8").read()
    texts[f] = txt
    act = re.search(r"\n\s*public\s+(?:override\s+)?(?:IActionResult|string|void|Task<[^>]+>)\s+\w+\s*\(", txt)
    head = txt[:act.start()] if act else txt
    # NB: readonly is OPTIONAL: some source controllers declare plain
    # `private IFoo _foo;` (PatientListPrint missed in Batch C because of this).
    fl = re.findall(r"private (?:readonly )?(I\w+) (_\w+);", head)
    if not fl:
        fl = re.findall(r"private (?:readonly )?(I\w+) (_\w+);", txt)
    fields_all[f] = fl
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
        calls[(fmap[fname], method)].append({
            "file": f, "args": args, "kinds": kinds, "names": names,
            "tail": txt[end:end + 120], "full": txt,
        })

SPECIAL = {}
WRITE_PREFIX = ("Save", "Submit", "Update", "Delete", "Insert", "Remove", "Cancel", "Sett", "Refund",
                "Book", "Signin", "Trade", "Exec", "Push", "Commit", "Generate", "Create", "Add",
                "Record", "BackNum", "Return", "Cancal", "Do", "Handle", "CheckInvoice",
                "CheckBrxz", "UpdateMzGh", "update", "UpdateCharge", "UpdateSetted", "UpdateYP",
                "UpdateArrearage", "UpdatePat", "UpdateForm", "UpdateDate", "updateZt", "UpdateArrange",
                "InsertArrange", "InsertghpbTime", "SaveData", "SubmitForm", "SubmitArrange", "Submitghpb",
                "SubmitInfo", "SubmitPhysicalexam", "SubmitArrearage", "SaveOptima", "Savepatient",
                "SaveOutpatient", "SaveAcount", "SaveCqyb", "SaveChongQing", "UpDate", "DetailsUpload",
                "XnhTuiFei", "XnhSett", "GuiAnXnh", "SimulationSett", "SaveCancel", "UnSetted",
                "PatBookGh", "DeleteArrange", "DeleteForm", "SaveDatapb", "SaveDatatzcz",
                "Set", "Write", "Upload", "Import", "Export", "Send", "Print", "DownLoad",
                "Backup", "Restore", "Lock", "Unlock", "Audit", "Approve", "Reject", "Confirm")


def dto_name(svc, method):
    return re.sub(r"\W", "", svc + "_" + method + "Dto")


dto_props_new = collections.defaultdict(set)
inferred = {}
for (svc, method), lst in calls.items():
    if (svc, method) in SPECIAL:
        inferred[(svc, method)] = SPECIAL[(svc, method)]
        continue
    is_list, is_dto = False, False
    dn = dto_name(svc, method)
    for c in lst:
        tail = c["tail"]
        if re.match(r"\s*\.\s*(Where|OrderBy|OrderByDescending|ThenBy)\s*\(", tail):
            is_list = True
            for p in re.findall(r"=>[^;]{0,200}?\.(\w+)", tail[:300]):
                dto_props_new[dn].add(p)
        if re.match(r"\s*\.\s*ToList\s*\(", tail):
            is_list = True
    for f, txt in texts.items():
        for m in re.finditer(r"(\w+)\s*=\s*[^;]{0,400}?\b" + re.escape(method) + r"\s*\(", txt):
            var = m.group(1)
            if var in ("return", "var"): continue
            seg = txt[m.start():m.start() + 1200]
            for p in re.findall(r"\b" + re.escape(var) + r"\.(\w+)", seg):
                if p in ("ToJson", "ToString", "Count"): continue
                is_dto = True
                dto_props_new[dn].add(p)
            if re.search(r"\b" + re.escape(var) + r"\s*\.\s*(Where|OrderBy|OrderByDescending)", seg):
                is_list = True
                for p in re.findall(r"=>[^;]{0,200}?\.(\w+)", seg):
                    dto_props_new[dn].add(p)
            if re.search(r"foreach\s*\([^)]*\b" + re.escape(var) + r"\b", seg):
                is_list = True
    if is_list:
        inferred[(svc, method)] = "List<" + dn + ">"
    elif is_dto:
        inferred[(svc, method)] = dn
    else:
        inferred[(svc, method)] = "object"


def is_write(svc, method):
    # "Do" prefix is for DoGenerate-style writes; Doctor* are doctor reports
    # (reads) misclassified by the bare "Do" prefix — exclude them.
    return method.startswith(WRITE_PREFIX) and not method.startswith("Doctor")


def sig_params(arity, kinds, names):
    params = []
    for i in range(arity):
        pname = names[i] if kinds[i] == "named" and names[i] else f"p{i}"
        prefix = "out " if kinds[i] == "out" else ("ref " if kinds[i] == "ref" else "")
        params.append(f"{prefix}object {pname}")
    return ", ".join(params)


def stub_body(ret, arity, kinds, names):
    body = ""
    for i in range(arity):
        if kinds[i] == "out":
            pname = names[i] if names[i] else f"p{i}"
            body += f"            {pname} = default;\n"
    return body


def stub_return(ret):
    if ret.startswith("List<") or ret.startswith("IList<") or ret.startswith("IEnumerable<"):
        inner = re.sub(r"^(?:List|IList|IEnumerable)<(.+)>$", r"\1", ret)
        return f"            return new List<{inner}>();\n"
    if ret.endswith("[]"):
        return f"            return new {ret[:-2]}[0];\n"
    if ret == "string": return "            return null;\n"
    if ret == "bool": return "            return false;\n"
    if ret == "bool?": return "            return null;\n"
    if ret in ("int", "long", "short", "decimal", "double", "float"): return f"            return default;\n"
    if ret == "DateTime": return "            return default;\n"
    if ret == "object": return "            return null;\n"
    if ret == "void": return ""
    return f"            return new {ret}();\n"


def insert_into_block(path, decl_re, new_text):
    txt = open(path, encoding="utf-8").read()
    m = re.search(decl_re, txt)
    if not m: return False
    b = txt.find("{", m.end())
    e = brace_block_end(txt, b)
    txt = txt[:e] + new_text + txt[e:]
    open(path, "w", encoding="utf-8").write(txt)
    return True


# ---------- emit ----------
new_ifaces, merged_methods, new_dtos, merged_dto_props = [], [], [], []
need_registration = []
services = collections.defaultdict(set)
for (svc, method), lst in calls.items():
    for c in lst:
        services[svc].add((method, len(c["args"]), tuple(c["kinds"]), tuple(c["names"])))
# field types never called -> empty interface
for f, fl in fields_all.items():
    for typ, _name in fl:
        if typ not in services:
            services[typ] = set()

iface_new_path = f"{INF}/Domain/Interfaces_{AREA}.cs"
impl_new_path = f"{INF}/DomainServices/Stubs_{AREA}.cs"
dto_new_path = f"{INF}/Domain/DTOs_{AREA}.cs"
iface_buf, impl_buf, dto_buf = [], [], []

def find_impl(svc):
    """Impl class name for an interface: registry (any base) else strip-I guess."""
    for cls, ifaces in impl_iface.items():
        if svc in ifaces:
            return cls
    return svc[1:] if svc.startswith("I") else svc + "Impl"


missing_impl_methods = collections.defaultdict(list)  # (svc, impl) -> [method tuples]
for svc in sorted(services):
    impl = find_impl(svc)
    for (method, arity, kinds, names) in sorted(services[svc]):
        ret = inferred.get((svc, method), "object")
        key = (method, arity, kinds)
        sig = f"{ret} {method}({sig_params(arity, kinds, names)});"
        if svc not in iface_file:
            pass  # whole interface emitted below
        elif key not in iface_methods[svc]:
            ok = insert_into_block(iface_file[svc], rf"public interface {re.escape(svc)}\b", f"        {sig}\n")
            merged_methods.append(f"{svc}.{method} -> {iface_file[svc]} (inserted={ok})")
            # matching impl method
            if impl in impl_file:
                ib = stub_body(ret, arity, kinds, names)
                if is_write(svc, method):
                    ib += '            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");\n'
                else:
                    ib += stub_return(ret)
                ok2 = insert_into_block(impl_file[impl], rf"public class {re.escape(impl)}\b",
                                        f"        public {ret} {method}({sig_params(arity, kinds, names)})\n        {{\n{ib}        }}\n")
                if not ok2:
                    merged_methods.append(f"  WARN: impl method not inserted for {impl}.{method}")
            else:
                missing_impl_methods[(svc, impl)].append((method, arity, kinds, names, ret))
        # DTO bookkeeping
        mm = re.match(r"List<(\w+)>", ret)
        dname = mm.group(1) if mm else (ret if ret.endswith("Dto") else None)
        if dname and dname not in ("SysConfigDto",):
            props = dto_props_new.get(dname, set())
            if dname not in dto_file:
                dto_buf.append((dname, props))
                dto_file[dname] = dto_new_path
                new_dtos.append(dname)
            else:
                missing = props - dto_props.get(dname, set())
                if missing:
                    if dname in new_dtos:
                        # Buffered this run, file not written yet: merge into
                        # the buffer instead of reading a nonexistent file.
                        for i, (bn, bp) in enumerate(dto_buf):
                            if bn == dname:
                                dto_buf[i] = (bn, bp | missing)
                                break
                        merged_dto_props.append(f"{dname}: +{sorted(missing)} (buffered)")
                    else:
                        lines = "".join(f"        public object {p} {{ get; set; }}\n" for p in sorted(missing))
                        ok = insert_into_block(dto_file[dname], rf"public class {re.escape(dname)}\b", lines)
                        merged_dto_props.append(f"{dname}: +{sorted(missing)} (inserted={ok})")
                    dto_props[dname] |= missing
    if svc not in iface_file:
        # whole new interface + impl class
        methods_txt = "\n".join(f"        {inferred.get((svc, m), 'object')} {m}({sig_params(a, k, n)});"
                                 for (m, a, k, n) in sorted(services[svc])) or "        // no calls observed"
        iface_buf.append(f"    public interface {svc}\n    {{\n{methods_txt}\n    }}\n")
        new_ifaces.append(svc)
        iface_file[svc] = iface_new_path
        impl_lines = [f"    public class {impl} : {svc}\n    {{"]
        for (m, a, k, n) in sorted(services[svc]):
            ret = inferred.get((svc, m), "object")
            ib = stub_body(ret, a, k, n)
            if is_write(svc, m):
                ib += '            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");\n'
            else:
                ib += stub_return(ret)
            impl_lines.append(f"        public {ret} {m}({sig_params(a, k, n)})\n        {{\n{ib}        }}\n")
        impl_lines.append("    }\n")
        impl_buf.append("\n".join(impl_lines))
        impl_file[impl] = impl_new_path
        need_registration.append((svc, impl))
    elif svc not in [s for s, _i in need_registration]:
        # existing interface: ensure a registration exists in Program.cs
        need_registration.append((svc, impl))

# impl classes for interfaces whose impl class was missing (grouped per class)
for (svc, impl), methods in missing_impl_methods.items():
    lines = [f"    public class {impl} : {svc}\n    {{"]
    for (method, arity, kinds, names, ret) in methods:
        ib = stub_body(ret, arity, kinds, names)
        if is_write(svc, method):
            ib += '            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");\n'
        else:
            ib += stub_return(ret)
        lines.append(f"        public {ret} {method}({sig_params(arity, kinds, names)})\n        {{\n{ib}        }}\n")
    lines.append("    }\n")
    impl_buf.append("\n".join(lines))
    impl_file[impl] = impl_new_path
    need_registration.append((svc, impl))

if iface_buf:
    os.makedirs(os.path.dirname(iface_new_path), exist_ok=True)
    content = "using System;\nusing System.Collections.Generic;\n\nnamespace Newtouch.Sett.Domain\n{\n" + "\n".join(iface_buf) + "}\n"
    open(iface_new_path, "w", encoding="utf-8").write(content)
if impl_buf:
    os.makedirs(os.path.dirname(impl_new_path), exist_ok=True)
    content = "using Newtouch.Sett.Domain;\nusing System;\nusing System.Collections.Generic;\n\nnamespace Newtouch.Sett.DomainServices\n{\n" + "\n".join(impl_buf) + "}\n"
    open(impl_new_path, "w", encoding="utf-8").write(content)
if dto_buf:
    os.makedirs(os.path.dirname(dto_new_path), exist_ok=True)
    lines = ["using System;\nusing System.Collections.Generic;\n\nnamespace Newtouch.Sett.Domain\n{"]
    for dname, props in dto_buf:
        lines.append(f"    public class {dname}\n    {{")
        for p in sorted(props):
            lines.append(f"        public object {p} {{ get; set; }}")
        lines.append("    }\n")
    lines.append("}")
    open(dto_new_path, "w", encoding="utf-8").write("\n".join(lines))

prog = open(f"{WEB}/Program.cs", encoding="utf-8").read()
registered = set(re.findall(r"AddScoped<([^,>]+),", prog))
print(f"services: {len(services)}")
print("NEW interfaces:", new_ifaces)
print("MERGED methods:", len(merged_methods))
for mm_ in merged_methods: print("  ", mm_)
print("NEW DTOs:", new_dtos)
print("MERGED DTO props:", merged_dto_props)
print("NEED REGISTRATION (interface -> impl; not yet in Program.cs):")
for s, i in sorted(set(need_registration)):
    if s not in registered and s.split(".")[-1] not in registered:
        print(f"  {s} -> {i}")
