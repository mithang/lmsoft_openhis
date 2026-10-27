#!/usr/bin/env python3
"""Apply Vietnamese translation map to HOSP web (.cs + .cshtml).

Rules (from task + session decisions):
- Replace WHOLE CJK runs only (maximal CJK sequences). A mapped key that is a
  substring of a longer run is NEVER replaced (prevents word-salad from
  Chinese modifier-head order vs Vietnamese head-modifier order).
- Skip comment lines and comment blocks (//, /* */, ///, @* *@, <!-- -->).
- Skip lines with SQL keywords (SELECT/INSERT/UPDATE/DELETE/FROM/WHERE) so
  DB-value logic and SQL text are never translated.
- Skip lines with equality comparisons (==, !=, .Equals()).
- Skip log-only lines (Log(/logger./WriteLog) and lines containing http URLs.
- Never touch wwwroot, obj, bin, or the static host.
Usage: apply-base-vi.py [--apply]   (default: dry run, prints stats only)
"""
import re, os, sys, json, collections

W = "/home/hatch/workspace/openhis-core/Newtouch.HIS.Base/Newtouch.HIS.Base.Web"
D = "/home/hatch/workspace/openhis-core/base-translate"
CJK = re.compile(r'[\u2e80-\u9fff]+')
SQL = re.compile(r'\b(SELECT|INSERT|UPDATE|DELETE|FROM|WHERE)\b', re.I)

def load_map():
    m = {}
    for fn in ["vi-map-1.json", "vi-map-2.json", "vi-map-3.json", "vi-map-4.json", "vi-map-5.json"]:
        m.update(json.load(open(os.path.join(D, fn), encoding="utf-8")))
    return m

def is_comment_line(s, is_cs):
    if is_cs:
        return s.startswith('//') or s.startswith('*') or s.startswith('/*') or s.startswith('///')
    return s.startswith('<!--') or s.startswith('@*') or s.startswith('//')

def operand_spans(line):
    """Spans of quoted strings used as comparison operands (must never translate)."""
    spans = []
    for pat in [r'==\s*"([^"]*)"', r'!=\s*"([^"]*)"', r'\.Equals\(\s*"([^"]*)"\s*\)',
                r'"([^"]*)"\s*==', r'"([^"]*)"\s*!=']:
        for m in re.finditer(pat, line):
            spans.append((m.start(1), m.end(1)))
    return spans

def translate_line(line, is_cs, mp):
    s = line.strip()
    if is_comment_line(s, is_cs): return line, 0
    if SQL.search(line): return line, 0
    if 'http' in line: return line, 0
    if re.search(r'\b(Log|logger|WriteLog)\s*\(', line): return line, 0
    spans = operand_spans(line) if ('==' in line or '!=' in line or '.Equals(' in line) else []
    n = 0
    def rep(mo):
        nonlocal n
        run = mo.group(0)
        # skip runs inside a comparison-operand string literal
        for a, b in spans:
            if mo.start() >= a and mo.end() <= b:
                return run
        if run in mp:
            n += 1
            return mp[run]
        return run
    return CJK.sub(rep, line), n

def main():
    apply = '--apply' in sys.argv
    mp = load_map()
    print(f"map keys: {len(mp)}")
    files_changed = 0; total_rep = 0; leftover = collections.Counter()
    for root, dirs, files in os.walk(W):
        dirs[:] = [d for d in dirs if d not in ('obj', 'bin', 'wwwroot')]
        for f in files:
            if not (f.endswith('.cs') or f.endswith('.cshtml')): continue
            p = os.path.join(root, f)
            is_cs = f.endswith('.cs')
            text = open(p, encoding='utf-8').read()
            lines = text.split('\n')
            out = []; changed = False; in_block = False
            for line in lines:
                st = line.strip()
                # block comment tracking (/* */, @* *@, <!-- -->)
                if in_block:
                    if ('*/' in st) or ('-->' in st) or ('*@' in st):
                        in_block = False
                    out.append(line); continue
                if (not is_cs and '@*' in line and '*@' not in line) or ('/*' in line and '*/' not in line) or ('<!--' in line and '-->' not in line):
                    in_block = True
                    out.append(line); continue
                nl, n = translate_line(line, is_cs, mp)
                if n: changed = True; total_rep += n
                out.append(nl)
            if changed:
                files_changed += 1
                if apply:
                    open(p, 'w', encoding='utf-8').write('\n'.join(out))
    # leftover scan (post-apply state if applying, else current state)
    for root, dirs, files in os.walk(W):
        dirs[:] = [d for d in dirs if d not in ('obj', 'bin', 'wwwroot')]
        for f in files:
            if not (f.endswith('.cs') or f.endswith('.cshtml')): continue
            is_cs = f.endswith('.cs')
            try: lines = open(os.path.join(root, f), encoding='utf-8').read().split('\n')
            except: continue
            in_block = False
            for line in lines:
                st = line.strip()
                if in_block:
                    if ('*/' in st) or ('-->' in st) or ('*@' in st): in_block = False
                    continue
                if (not is_cs and '@*' in line and '*@' not in line) or ('/*' in line and '*/' not in line) or ('<!--' in line and '-->' not in line):
                    in_block = True; continue
                if is_comment_line(st, is_cs) or SQL.search(line) or 'http' in line:
                    continue
                spans = operand_spans(line) if ('==' in line or '!=' in line or '.Equals(' in line) else []
                for mo in CJK.finditer(line):
                    if any(mo.start() >= a and mo.end() <= b for a, b in spans):
                        continue
                    leftover[mo.group(0)] += 1
    print(f"mode: {'APPLY' if apply else 'DRY RUN'}, files changed: {files_changed}, replacements: {total_rep}")
    print(f"leftover non-comment runs: {len(leftover)} unique, {sum(leftover.values())} occurrences")
    top = sorted(leftover.items(), key=lambda x: -x[1])[:120]
    for k, c in top: print(f"  {c:4d} | {k}")
    json.dump([{"run": k, "count": c} for k, c in top],
              open(os.path.join(D, "leftover-after-apply.json"), "w", encoding="utf-8"),
              ensure_ascii=False, indent=1)

if __name__ == '__main__':
    main()
