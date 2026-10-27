# -*- coding: utf-8 -*-
"""CIS Web translation sweep, tier 5.
Key fix: == inside placeholder options (==...==) is NOT a comparison.
For each line, strip placeholder spans for the comparison test only:
- line with a REAL comparison (==/!=/Equals outside placeholders) -> literal mode
  (translate quoted literals NOT adjacent to ==/!=, full combined dict)
- other lines -> fragment mode (full combined dict), including placeholder text
Also skips SQL/log/URL lines entirely, and case lines (consistency rule)."""
import os, re

W = "/home/hatch/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web"
COMMENT = re.compile(r"(@\*.*?\*@|<!--.*?-->|(?<!:)//[^\n]*|/\*.*?\*/)", re.DOTALL)
HARD_SKIP = re.compile(r"(SELECT\s|FROM\s|WHERE\s|INSERT\s+INTO|UPDATE\s+\w+\s+SET|DELETE\s+FROM|\bEXEC\b|Log\.|logger\.|Console\.Write|https?://|^\s*case\s)")
PLACEHOLDER = re.compile(r"==[^=\n]*==")
CMP = re.compile(r"(==|!=|\.Equals\()")
STR = re.compile(r'"([^"\n]{0,300})"|\'([^\'\n]{0,300})\'')


def load_dict(path, var):
    src = open(path, encoding='utf-8').read()
    dsrc = var + " = " + src.split(var + ' = ', 1)[1]
    dsrc = dsrc[:dsrc.index('\n}\n') + 3]
    ns = {}
    exec(dsrc, ns)
    return ns[var]


D = load_dict('/tmp/cis-tier1.py', 'D')
for pth, var in [('/tmp/cis-tier2.py', 'NEW'), ('/tmp/cis-tier3.py', 'NEW3'), ('/tmp/cis-tier4.py', 'NEW4')]:
    for k, v in load_dict(pth, var).items():
        D.setdefault(k, v)
NEW5 = {
"行的": "dòng", "中有": "có", "或多个空值": "hoặc nhiều giá trị trống",
"有空值": "có giá trị trống", "当天": "Hôm nay",
"请Kiểm tra": "vui lòng kiểm tra", "请选择": "Vui lòng chọn",
}
for k, v in NEW5.items():
    D.setdefault(k, v)
keys = sorted([k for k in D if len(k) >= 2], key=len, reverse=True)
SPECIAL = ('"第" + (j + 1) + "行的', '"Dòng thứ " + (j + 1) + " dòng "')


def translate_literal(lit):
    out = lit
    for k in keys:
        if k in out:
            out = out.replace(k, D[k])
    return out


changed = 0; replaced = 0
for root, dirs, files in os.walk(W):
    dirs[:] = [d for d in dirs if d not in ('obj', 'bin')]
    for fn in files:
        if not (fn.endswith('.cshtml') or fn.endswith('.cs') or fn.endswith('.js')):
            continue
        p = os.path.join(root, fn)
        s = open(p, encoding='utf-8-sig', errors='ignore').read()
        parts = COMMENT.split(s)
        out = []; n = 0
        for i, part in enumerate(parts):
            if i % 2 == 1:
                out.append(part); continue
            plines = part.split('\n')
            nlines = []
            for ln in plines:
                if HARD_SKIP.search(ln):
                    nlines.append(ln); continue
                if SPECIAL[0] in ln:
                    ln = ln.replace(SPECIAL[0], SPECIAL[1]); n += 1
                test = PLACEHOLDER.sub('', ln)
                if CMP.search(test):
                    def _sub(m, _ln=ln):
                        lit = m.group(1) if m.group(1) is not None else m.group(2)
                        q = '"' if m.group(1) is not None else "'"
                        if not lit or not re.search(r'[\u4e00-\u9fff]', lit):
                            return m.group(0)
                        st, en = m.span()
                        before = _ln[max(0, st - 6):st]
                        after = _ln[en:en + 6]
                        if re.search(r'(==|!=)\s*$', before) or re.search(r'^\s*(==|!=)', after):
                            return m.group(0)
                        nl = translate_literal(lit)
                        if nl != lit:
                            return q + nl + q
                        return m.group(0)
                    newln = STR.sub(_sub, ln)
                    # count CJK removed roughly
                    if newln != ln:
                        n += 1
                    ln = newln
                else:
                    for k in keys:
                        if k in ln:
                            c = ln.count(k)
                            ln = ln.replace(k, D[k]); n += c
                nlines.append(ln)
            out.append('\n'.join(nlines))
        if n:
            open(p, 'w', encoding='utf-8').write(''.join(out))
            changed += 1; replaced += n
print("tier5 files changed:", changed, "| replacements:", replaced)
