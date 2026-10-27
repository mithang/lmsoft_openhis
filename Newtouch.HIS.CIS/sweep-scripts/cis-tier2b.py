# -*- coding: utf-8 -*-
"""Tier 2b: translate user-visible message literals on comparison lines.
Skipped lines (==/!=/Equals) are revisited: a quoted literal is translated
UNLESS it sits directly adjacent to ==/!= (i.e. it is a comparison operand).
Log/SQL/URL lines stay untouched."""
import os, re

W = "/home/hatch/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web"
COMMENT = re.compile(r"(@\*.*?\*@|<!--.*?-->|(?<!:)//[^\n]*|/\*.*?\*/)", re.DOTALL)
CMP_LINE = re.compile(r"(==|!=|\.Equals\()")
HARD_SKIP = re.compile(r"(SELECT\s|FROM\s|WHERE\s|INSERT\s+INTO|UPDATE\s+\w+\s+SET|DELETE\s+FROM|\bEXEC\b|Log\.|logger\.|Console\.Write|https?://)")
STR = re.compile(r'"([^"\n]{0,300})"|\'([^\'\n]{0,300})\'')

_src = open('/tmp/cis-tier1.py', encoding='utf-8').read()
_dict_src = "D = " + _src.split('D = ', 1)[1]
_dict_src = _dict_src[:_dict_src.index('\n}\n') + 3]
_ns = {}
exec(_dict_src, _ns)
D = dict(_ns['D'])
# tier2 dict
_src2 = open('/tmp/cis-tier2.py', encoding='utf-8').read()
_dict_src2 = "NEW = " + _src2.split('NEW = ', 1)[1]
_dict_src2 = _dict_src2[:_dict_src2.index('\n}\n') + 3]
_ns2 = {}
exec(_dict_src2, _ns2)
for k, v in _ns2['NEW'].items():
    D.setdefault(k, v)
D["接口调用"] = "Gọi giao diện"
D["签到"] = "Check-in"
D = {k: v for k, v in D.items() if len(k) >= 2}
keys = sorted(D, key=len, reverse=True)


def translate_literal(lit):
    out = lit
    for k in keys:
        if k in out:
            out = out.replace(k, D[k])
    return out


changed = 0; replaced = 0; left = []
for root, dirs, files in os.walk(W):
    dirs[:] = [d for d in dirs if d not in ('obj', 'bin')]
    for fn in files:
        if not (fn.endswith('.cshtml') or fn.endswith('.cs') or fn.endswith('.js')):
            continue
        p = os.path.join(root, fn)
        s = open(p, encoding='utf-8-sig', errors='ignore').read()
        parts = COMMENT.split(s)
        out = []; box = [0]
        for i, part in enumerate(parts):
            if i % 2 == 1:
                out.append(part); continue
            plines = part.split('\n')
            nlines = []
            for ln in plines:
                if CMP_LINE.search(ln) and not HARD_SKIP.search(ln):
                    # translate non-operand literals on this line
                    def _sub(m):
                        lit = m.group(1) if m.group(1) is not None else m.group(2)
                        q = '"' if m.group(1) is not None else "'"
                        if not lit or not re.search(r'[\u4e00-\u9fff]', lit):
                            return m.group(0)
                        # operand adjacency check
                        st, en = m.span()
                        before = ln[max(0, st - 6):st]
                        after = ln[en:en + 6]
                        if re.search(r'(==|!=)\s*$', before) or re.search(r'^\s*(==|!=)', after):
                            return m.group(0)
                        nl = translate_literal(lit)
                        if nl != lit:
                            box[0] += 1
                            return q + nl + q
                        return m.group(0)
                    ln = STR.sub(_sub, ln)
                nlines.append(ln)
            out.append('\n'.join(nlines))
        if box[0]:
            open(p, 'w', encoding='utf-8').write(''.join(out))
            changed += 1; replaced += box[0]
print("tier2b files changed:", changed, "| literals translated:", replaced)
