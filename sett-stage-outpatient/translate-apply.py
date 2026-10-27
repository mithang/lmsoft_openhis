#!/usr/bin/env python3
"""Apply translation dicts to views (visible lines) and controllers (string literals)."""
import re, glob

def load_dict(path, name):
    txt = open(path, encoding="utf-8").read()
    m = re.search(r"^" + name + r" = (\{.*?\n\})", txt, flags=re.S | re.M)
    return eval(m.group(1))

D = load_dict("/home/hatch/workspace/openhis-core/sett-stage-outpatient/translate-vi.py", "D")
D.update(load_dict("/home/hatch/workspace/openhis-core/sett-stage-outpatient/translate-vi2.py", "D2"))
D.update(load_dict("/home/hatch/workspace/openhis-core/sett-stage-outpatient/translate-vi3.py", "D3"))
KEYS = sorted(D.keys(), key=len, reverse=True)
print(f"dict entries: {len(D)}")

def translate_run(run):
    if run in D:
        return D[run]
    # greedy longest-match segmentation
    out, i = "", 0
    while i < len(run):
        hit = None
        for k in KEYS:
            if run.startswith(k, i):
                hit = k
                break
        if hit:
            out += D[hit]
            i += len(hit)
        else:
            out += run[i]
            i += 1
    return out

CJK = re.compile(r"[\u4e00-\u9fff]+")

def translate_text(s):
    return CJK.sub(lambda m: translate_run(m.group(0)), s)

def strip_comment(line, inblock):
    """Return (visible_line, new_inblock). Strips // comments outside quotes and /* */."""
    if inblock:
        if "*/" in line:
            line = line.split("*/", 1)[1]
            inblock = False
        else:
            return "", True
    while "/*" in line:
        a, b = line.split("/*", 1)
        if "*/" in b:
            line = a + b.split("*/", 1)[1]
        else:
            line = a
            inblock = True
            break
    res, i, q = "", 0, None
    while i < len(line):
        c = line[i]
        if q:
            res += c
            if c == "\\" and i + 1 < len(line):
                res += line[i + 1]
                i += 2
                continue
            if c == q:
                q = None
        else:
            if c in "\"'":
                q = c
                res += c
            elif c == "/" and i + 1 < len(line) and line[i + 1] == "/":
                break
            else:
                res += c
        i += 1
    return res, inblock

def process_view(path):
    lines = open(path, encoding="utf-8-sig", errors="ignore").read().splitlines()
    inblock = False
    out = []
    for line in lines:
        vis, inblock = strip_comment(line, inblock)
        if vis != line:
            # comment part preserved: re-append trailing comment unchanged
            # find comment start in original by length heuristic: keep original suffix after vis
            suffix = line[len(vis):] if line.startswith(vis) else ""
            out.append(translate_text(vis) + suffix)
        else:
            out.append(translate_text(line))
    open(path, "w", encoding="utf-8").write("\n".join(out) + "\n")

def process_controller(path):
    # only inside double-quoted literals on non-comment lines
    lines = open(path, encoding="utf-8", errors="ignore").read().splitlines()
    out = []
    for line in lines:
        stripped = line.strip()
        if stripped.startswith("//") or stripped.startswith("///") or stripped.startswith("*"):
            out.append(line)
            continue
        vis, _ = strip_comment(line, False)
        comment_suffix = line[len(vis):] if line.startswith(vis) else ""
        # translate CJK inside "..." segments of vis
        parts = re.split(r'("(?:[^"\\]|\\.)*")', vis)
        parts = [translate_text(p) if p.startswith('"') else p for p in parts]
        out.append("".join(parts) + comment_suffix)
    open(path, "w", encoding="utf-8").write("\n".join(out) + "\n")

for f in glob.glob("/home/hatch/workspace/openhis-core/sett-stage-outpatient/Newtouch.Sett.Web/Areas/OutpatientManage/Views/**/*", recursive=True):
    if f.endswith((".cshtml", ".html")):
        process_view(f)
for f in glob.glob("/home/hatch/workspace/openhis-core/sett-stage-outpatient/Newtouch.Sett.Web/Areas/OutpatientManage/Controllers/*.cs"):
    process_controller(f)
print("applied")
