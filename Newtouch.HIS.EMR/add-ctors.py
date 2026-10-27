#!/usr/bin/env python3
"""Add/complete DI constructors for EMR Area controllers.

The Framework originals declared `private readonly I*` fields with NO constructor
(Autofac-era latent bug: those fields were always null upstream). ASP.NET Core
needs a real constructor, so this script generates one per controller covering
ALL declared service/repo fields (existing ctor params kept first, in order;
missing fields appended; old non-assignment ctor body statements preserved).
Also drops CommonController's unused ISysTCMSyndromeRepo field (only a commented
call site references it).
"""
import re
import glob
import sys

sys.path.insert(0, "/home/hatch/workspace/openhis-core/Newtouch.HIS.EMR")

def find_block_end(lines, start):
    i = start
    while i < len(lines):
        if "{" in lines[i]:
            break
        i += 1
    if i >= len(lines):
        raise ValueError("no opening brace from line %d" % start)
    depth = 0
    in_str = in_char = esc = in_block = False
    j = i
    while j < len(lines):
        line = lines[j]
        k = 0
        while k < len(line):
            ch = line[k]
            nxt = line[k + 1] if k + 1 < len(line) else ""
            if in_block:
                if ch == "*" and nxt == "/":
                    in_block = False
                    k += 2
                    continue
                k += 1
                continue
            if in_str:
                if esc:
                    esc = False
                elif ch == "\\":
                    esc = True
                elif ch == '"':
                    in_str = False
                k += 1
                continue
            if in_char:
                if esc:
                    esc = False
                elif ch == "\\":
                    esc = True
                elif ch == "'":
                    in_char = False
                k += 1
                continue
            if ch == "/" and nxt == "/":
                break
            if ch == "/" and nxt == "*":
                in_block = True
                k += 2
                continue
            if ch == '"':
                in_str = True
            elif ch == "'":
                in_char = True
            elif ch == "{":
                depth += 1
            elif ch == "}":
                depth -= 1
                if depth == 0:
                    return j + 1
            k += 1
        j += 1
    raise ValueError("unbalanced block from line %d" % start)

def param_name(field):
    name = field.lstrip("_")
    return name[0].lower() + name[1:]

for path in sorted(glob.glob("/home/hatch/workspace/openhis-core/Newtouch.HIS.EMR/Newtouch.EMR.Web/Areas/*/Controllers/*.cs")):
    with open(path, encoding="utf-8") as f:
        text = f.read()
    if "CommonController.cs" in path:
        text = re.sub(r"\n\s*private readonly ISysTCMSyndromeRepo _sysTcmsyndromeRepo;", "", text)
    lines = text.split("\n")
    m_cls = re.search(r"public class (\w+Controller)", text)
    if not m_cls:
        continue
    cls = m_cls.group(1)
    fields = re.findall(r"private readonly (\S+) (_\w+);", text)
    if not fields:
        continue
    # existing ctor?
    ctor_idx = None
    for i, line in enumerate(lines):
        if re.search(r"public\s+%s\s*\(" % cls, line):
            ctor_idx = i
            break
    old_params = []  # (type, name)
    old_body_stmts = []
    if ctor_idx is not None:
        header = lines[ctor_idx]
        # params may span lines until ')'
        j = ctor_idx
        header_text = ""
        while ")" not in header_text and j < len(lines):
            header_text += lines[j] + "\n"
            j += 1
        pm = re.search(r"\(\s*(.*?)\s*\)", header_text, re.S)
        if pm and pm.group(1).strip():
            for part in pm.group(1).split(","):
                part = part.strip()
                if not part:
                    continue
                t, n = part.rsplit(None, 1)
                old_params.append((t, n))
        end = find_block_end(lines, ctor_idx)
        body_lines = lines[ctor_idx:end]
        for bl in body_lines:
            s = bl.strip()
            if not s or s in ("{", "}"):
                continue
            if re.match(r"(this\.)?_\w+\s*=", s):
                continue  # old assignment, regenerated below
            if s.startswith("public %s" % cls):
                continue
            old_body_stmts.append(bl)
    # build full param list: existing first, then missing fields
    have_types = {t for t, _ in old_params}
    params = list(old_params)
    field_param = {}
    for ftype, fname in fields:
        if ftype in have_types:
            # find existing param name for this type
            pname = next(n for t, n in old_params if t == ftype)
        else:
            pname = param_name(fname)
            params.append((ftype, pname))
        field_param[fname] = pname
    # collision check
    pnames = [n for _, n in params]
    if len(set(pnames)) != len(pnames):
        print("COLLISION in", path, pnames)
        continue
    param_str = ", ".join("%s %s" % (t, n) for t, n in params)
    ctor_lines = ["        public %s(%s)" % (cls, param_str), "        {"]
    ctor_lines.extend(old_body_stmts)
    for ftype, fname in fields:
        ctor_lines.append("            this.%s = %s;" % (fname, field_param[fname]))
    ctor_lines.append("        }")
    if ctor_idx is not None:
        end = find_block_end(lines, ctor_idx)
        lines = lines[:ctor_idx] + ctor_lines + lines[end:]
        print(path.split("/")[-1], "ctor replaced:", len(params), "params")
    else:
        # insert after last field declaration
        last_field = max(i for i, l in enumerate(lines) if re.search(r"private readonly \S+ _\w+;", l))
        lines = lines[:last_field + 1] + [""] + ctor_lines + lines[last_field + 1:]
        print(path.split("/")[-1], "ctor added:", len(params), "params")
    with open(path, "w", encoding="utf-8") as f:
        f.write("\n".join(lines))
print("done")
