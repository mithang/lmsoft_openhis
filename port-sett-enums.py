#!/usr/bin/env python3
"""Port source-Domain enums used by ONE Area, with translated [Description]s.
Usage: python3 port-sett-enums.py <AreaName>
- Finds enum candidates: enums stubbed in Types_<Area>.cs + types used in
  GetDescription()/ToDescSelectList<>/Enum.GetName/Enum.Parse contexts in the
  Area's ported controllers.
- Searches the run copy for `enum <Name>` (preferring Sett's Newtouch.Domain),
  copies the enum, translates [Description("...")] via sett-dict.json
  (longest-first; untranslated descriptions are reported, not dropped).
- Writes Domain/Enums_<Area>.cs; removes the stub enum block from Types_<Area>.cs
  (avoids duplicate-type errors). Enums not found in source stay as stubs.
"""
import re, os, glob, json, sys

AREA = sys.argv[1]
INF = "/home/hatch/workspace/openhis-core/Newtouch.Sett.Infrastructure"
WEB = "/home/hatch/workspace/openhis-core/Newtouch.Sett.Web"
RUN = "/home/hatch/workspace/lmsoft_openhis_run"
d = json.load(open("/home/hatch/workspace/openhis-core/sett-dict.json", encoding="utf-8"))
add_path = "/home/hatch/workspace/openhis-core/sett-rem-dict-add.json"
if os.path.exists(add_path):
    d.update(json.load(open(add_path, encoding="utf-8")))
KEYS = sorted(d, key=len, reverse=True)


def brace_end(text, open_idx):
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


# candidate enum names
cands = set()
types_path = f"{INF}/Domain/Types_{AREA}.cs"
if os.path.exists(types_path):
    txt = open(types_path, encoding="utf-8").read()
    cands |= set(re.findall(r"public enum (\w+)", txt))
for f in glob.glob(f"{WEB}/Areas/{AREA}/Controllers/*.cs"):
    txt = open(f, encoding="utf-8").read()
    for m in re.finditer(r"(\w+)\.GetDescription\(\)", txt): cands.add(m.group(1))
    for m in re.finditer(r"ToDescSelectList<(\w+)>", txt): cands.add(m.group(1))
    for m in re.finditer(r"Enum\.GetName\(typeof\((\w+)\)", txt): cands.add(m.group(1))
    for m in re.finditer(r"Enum\.Parse\(typeof\((\w+)\)", txt): cands.add(m.group(1))
    for m in re.finditer(r"Enum\.GetValues\(typeof\((\w+)\)", txt): cands.add(m.group(1))
    for m in re.finditer(r"(\w+)\.GetDescription\(", txt): cands.add(m.group(1))

# index source enums
src_enums = {}
for f in glob.glob(RUN + "/**/*.cs", recursive=True):
    if "/obj/" in f or "/bin/" in f or "/Newtouch.Web/" in f: continue
    try:
        txt = open(f, encoding="utf-8-sig", errors="ignore").read()
    except Exception:
        continue
    for m in re.finditer(r"public enum (\w+)", txt):
        name = m.group(1)
        b = txt.find("{", m.end())
        if b < 0: continue
        e = brace_end(txt, b)
        body = txt[b:e + 1]
        score = 0
        if "Newtouch.HIS.Sett" in f: score += 2
        if "Newtouch.Domain" in f: score += 1
        if name not in src_enums or score > src_enums[name][0]:
            src_enums[name] = (score, body, f)

ported, missing = [], []
for name in sorted(cands):
    if name not in src_enums:
        missing.append(name)
        continue
    _score, body, _f = src_enums[name]
    # translate descriptions
    def tr_desc(m):
        s = m.group(1)
        for k in KEYS:
            if k in s: s = s.replace(k, d[k])
        return f'[Description("{s}")]'
    body2 = re.sub(r'\[Description\("([^"]*)"\)\]', tr_desc, body)
    # strip other attributes that may reference missing types? keep Description only
    ported.append((name, body2))

if ported:
    lines = ["using System;\nusing System.ComponentModel;\n\nnamespace Newtouch.Sett.Domain\n{"]
    for name, body in ported:
        lines.append(f"    public enum {name} {body}\n")
    lines.append("}")
    out = f"{INF}/Domain/Enums_{AREA}.cs"
    open(out, "w", encoding="utf-8").write("\n".join(lines))
    print(f"ported enums -> {out}: {[n for n, _ in ported]}")
    # remove stub enums from Types file
    if os.path.exists(types_path):
        txt = open(types_path, encoding="utf-8").read()
        for name, _b in ported:
            m = re.search(rf"    public enum {re.escape(name)}\s*\n?\s*\{{", txt)
            if m:
                b = txt.find("{", m.end())
                e = brace_end(txt, b)
                txt = txt[:m.start()] + txt[e + 1:]
        open(types_path, "w", encoding="utf-8").write(txt)
        print("stub enums removed from Types file")
print("enums NOT found in source (stay as stubs):", missing)
# report untranslated descriptions
CJK = re.compile(r"[\u4e00-\u9fff]")
for name, body in ported:
    for m in re.finditer(r'\[Description\("([^"]*)"\)\]', body):
        if CJK.search(m.group(1)):
            print(f"  UNTRANSLATED-DESC {name}: {m.group(1)}")
