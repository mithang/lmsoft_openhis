# -*- coding: utf-8 -*-
"""CIS Web translation sweep, tier 6 (polish):
- repair partial-translation artifacts (short key applied inside a longer word)
- spacing repairs for glued Vietnamese words
- full-width punctuation -> ASCII punctuation on display lines
Same protections: comments split, HARD_SKIP lines untouched, placeholder-aware
comparison test (literal mode not needed here: repairs are whole-phrase)."""
import os, re

W = "/home/hatch/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web"
COMMENT = re.compile(r"(@\*.*?\*@|<!--.*?-->|(?<!:)//[^\n]*|/\*.*?\*/)", re.DOTALL)
HARD_SKIP = re.compile(r"(SELECT\s|FROM\s|WHERE\s|INSERT\s+INTO|UPDATE\s+\w+\s+SET|DELETE\s+FROM|\bEXEC\b|Log\.|logger\.|Console\.Write|https?://|^\s*case\s)")
PLACEHOLDER = re.compile(r"==[^=\n]*==")
CMP = re.compile(r"(==|!=|\.Equals\()")

REPAIRS = {
"离休Cán bộ": "Cán bộ nghỉ hưu",
"cómột": "có một", "mộthoặc": "một hoặc",
}
PUNCT = {
"，": ",", "。": ".", "！": "!", "？": "?",
"：": ":", "；": ";", "（": "(", "）": ")",
"": "]", "、": ",",
}
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
                test = PLACEHOLDER.sub('', ln)
                if CMP.search(test):
                    # comparison line: only apply phrase repairs inside literals,
                    # never punctuation (could touch operands)
                    for k, v in REPAIRS.items():
                        if k in ln:
                            ln = ln.replace(k, v); n += 1
                else:
                    for k, v in REPAIRS.items():
                        if k in ln:
                            ln = ln.replace(k, v); n += 1
                    for k, v in PUNCT.items():
                        if k in ln:
                            ln = ln.replace(k, v); n += 1
                nlines.append(ln)
            out.append('\n'.join(nlines))
        if n:
            open(p, 'w', encoding='utf-8').write(''.join(out))
            changed += 1; replaced += n
print("tier6 files changed:", changed, "| replacements:", replaced)
