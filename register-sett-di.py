#!/usr/bin/env python3
"""Append missing DI registrations to Newtouch.Sett.Web/Program.cs.
- Scans all interfaces in Newtouch.Sett.Infrastructure (any namespace).
- For each interface not yet registered, finds its impl class (a class whose
  base list includes the interface) and appends a FULLY QUALIFIED
  AddScoped<IFaceNs.IFace, ImplNs.Impl>(); line before AddControllersWithViews.
- Idempotent: re-running adds nothing.
- Prints added lines + interfaces with no impl found (caller fixes those).
"""
import re, glob, sys

WEB = "/home/hatch/workspace/openhis-core/Newtouch.Sett.Web"
INF = "/home/hatch/workspace/openhis-core/Newtouch.Sett.Infrastructure"
PROG = f"{WEB}/Program.cs"

ns_of_file = {}
iface_ns = {}   # iface -> namespace
impl_map = {}   # iface -> (impl, impl_ns)
for f in glob.glob(INF + "/**/*.cs", recursive=True):
    if "/obj/" in f or "/bin/" in f: continue
    txt = open(f, encoding="utf-8", errors="ignore").read()
    m = re.search(r"namespace\s+([\w\.]+)", txt)
    ns = m.group(1) if m else ""
    for im in re.finditer(r"public interface (\w+)", txt):
        iface_ns[im.group(1)] = ns
    for cm in re.finditer(r"public class (\w+)\s*:\s*([^{\n]+)", txt):
        for base in cm.group(2).split(","):
            base = base.strip()
            if base.startswith("I"):
                impl_map[base] = (cm.group(1), ns)

prog = open(PROG, encoding="utf-8").read()
registered = set()
for m in re.finditer(r"AddScoped<([\w\.]+)\s*,", prog):
    registered.add(m.group(1).split(".")[-1])

added, missing = [], []
for iface in sorted(iface_ns):
    if iface in registered:
        continue
    if iface not in impl_map:
        missing.append(iface)
        continue
    impl, impl_ns = impl_map[iface]
    line = f"builder.Services.AddScoped<{iface_ns[iface]}.{iface}, {impl_ns}.{impl}>();"
    added.append(line)

if added:
    anchor = "builder.Services.AddControllersWithViews()"
    assert anchor in prog
    prog = prog.replace(anchor, "\n".join(added) + "\n" + anchor, 1)
    open(PROG, "w", encoding="utf-8").write(prog)
print(f"added {len(added)} registrations")
for a in added: print("  ", a)
print("interfaces with NO impl found:", missing)
