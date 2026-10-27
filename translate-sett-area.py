#!/usr/bin/env python3
"""Translate ONE ported Sett Area (views + controllers) CJK -> Vietnamese.
Usage: python3 translate-sett-area.py <AreaName> [extra files...]
- Dictionary: sett-dict.json + sett-rem-dict-add.json (additions win).
- Protection: CJK runs used in comparisons / SQL / report URLs in the SOURCE
  file are never replaced (changing them would break logic or report links).
- Skips comments: // and /* */ in .cs; @* *@ and <!-- --> in .cshtml
  (plus // comments in .cshtml code, tracked string-aware).
- Replacement: longest dict key first, applied to non-comment segments only.
- Rescan at the end: remaining CJK runs per file (excluding comments and
  protected runs) + unique leftover runs for the operator to translate next.
"""
import re, os, glob, json, sys

AREA = sys.argv[1]
EXTRA = sys.argv[2:]
SRC_AREA = f"/home/hatch/workspace/lmsoft_openhis_run/Newtouch.HIS.Sett/Newtouch.Web/Areas/{AREA}"
DST_AREA = f"/home/hatch/workspace/openhis-core/Newtouch.Sett.Web/Areas/{AREA}"
CJK_RUN = re.compile(r"[\u4e00-\u9fff]+")
CJK = re.compile(r"[\u4e00-\u9fff]")

d = json.load(open("/home/hatch/workspace/openhis-core/sett-dict.json", encoding="utf-8"))
add_path = "/home/hatch/workspace/openhis-core/sett-rem-dict-add.json"
if os.path.exists(add_path):
    d.update(json.load(open(add_path, encoding="utf-8")))
KEYS = sorted(d, key=len, reverse=True)


def protected_spans(text, is_view):
    """Spans of quoted CJK strings that must NOT be translated (positional):
    - comparison operands: ==, !=, Contains(, IndexOf(, StartsWith(, EndsWith(, case
    - SQL content, report-server URLs, absolute URL-ish content
    Everything else (display strings, formatter returns, option text) translates.
    A protected run stays Chinese ONLY at that occurrence; the same text used
    as display elsewhere still translates (positional masking, not per-run)."""
    spans = []
    for m in re.finditer(r'"([^"\n]{0,500})"', text):
        s = m.group(1)
        if not CJK.search(s):
            continue
        before = text[max(0, m.start() - 40):m.start()]
        after = text[m.end():m.end() + 10]
        if (re.search(r"(==|!=|Contains\(|IndexOf\(|StartsWith\(|EndsWith\(|case)\s*$", before)
                or re.match(r"\s*(==|!=)", after)
                or re.search(r"(?i)\b(select|insert\s+into|update\s|delete\s+from)\b", s)
                or "ReportServer" in s or "%2f" in s.lower()):
            spans.append((m.start(), m.end()))
        elif "://" in s:
            if is_view:
                # Narrow: mask only the URL itself, so display text around it
                # (e.g. <a href="http://...">中文</a>) still translates.
                for um in re.finditer(r"https?://[^\s'\"<>]+", s):
                    spans.append((m.start() + 1 + um.start(), m.start() + 1 + um.end()))
            else:
                spans.append((m.start(), m.end()))
    if is_view:
        for m in re.finditer(r"'([^'\n]{0,500})'", text):
            s = m.group(1)
            if not CJK.search(s):
                continue
            before = text[max(0, m.start() - 40):m.start()]
            after = text[m.end():m.end() + 10]
            if (re.search(r"(==|!=|Contains\(|IndexOf\(|case)\s*$", before)
                    or re.match(r"\s*(==|!=)", after)
                    or "ReportServer" in s or "%2f" in s.lower()):
                spans.append((m.start(), m.end()))
            elif "://" in s:
                # Same narrowing as above (demo link with display text).
                for um in re.finditer(r"https?://[^\s'\"<>]+", s):
                    spans.append((m.start() + 1 + um.start(), m.start() + 1 + um.end()))
    if is_view:
        # href/src attribute VALUES are paths, never display text: mask them
        # so a Chinese filename (e.g. ~/File/医保中间件下载.zip) survives while
        # the link text itself still translates.
        for m in re.finditer(r'''(?:href|src)\s*=\s*"([^"\n]{0,500})"''', text):
            if CJK.search(m.group(1)):
                spans.append((m.start(1), m.end(1)))
        for m in re.finditer(r"""(?:href|src)\s*=\s*'([^'\n]{0,500})'""", text):
            if CJK.search(m.group(1)):
                spans.append((m.start(1), m.end(1)))
    return spans


def mask_spans(text, spans):
    """Replace spans (last to first) with placeholders; return masked, originals."""
    originals = []
    for i, (a, b) in enumerate(sorted(spans, reverse=True)):
        originals.append((f"__SETTPROT{i}__", text[a:b]))
        text = text[:a] + f"__SETTPROT{i}__" + text[b:]
    return text, originals


def unmask(text, originals):
    for ph, orig in originals:
        text = text.replace(ph, orig)
    return text


def split_cs_comments(text):
    """Yield (is_comment, segment) for .cs text, string-aware for // comments."""
    out = []
    i, n = 0, len(text)
    buf = []
    in_str = None
    in_block = False
    while i < n:
        c = text[i]
        if in_block:
            buf.append(c)
            if c == "*" and i + 1 < n and text[i + 1] == "/":
                buf.append("/"); i += 2
                out.append((True, "".join(buf))); buf = []
                in_block = False
            else:
                i += 1
            continue
        if in_str:
            buf.append(c)
            if c == "\\" and i + 1 < n:
                buf.append(text[i + 1]); i += 2; continue
            if c == in_str: in_str = None
            i += 1
            continue
        if c in ('"', "'") and (c == '"' or True):
            # treat ' as string only when it looks like a char literal start
            in_str = c; buf.append(c); i += 1; continue
        if c == "/" and i + 1 < n and text[i + 1] == "/":
            out.append((False, "".join(buf))); buf = []
            j = text.find("\n", i)
            j = n if j < 0 else j
            out.append((True, text[i:j])); i = j
            continue
        if c == "/" and i + 1 < n and text[i + 1] == "*":
            out.append((False, "".join(buf))); buf = []
            in_block = True; buf.append("/*"); i += 2
            continue
        buf.append(c); i += 1
    if buf: out.append((in_block, "".join(buf)))
    return out


BRANDS = ["WeChat"]


def _space_token(tok):
    """Insert a space at lowercase->Uppercase boundaries (two CJK runs that
    were translated abutting, e.g. 'ChọnNhân'). Tokens with / or . are
    URLs/paths/dotted names -- left alone. Pure-ASCII tokens are code
    identifiers ([Area("BusinessManage")], jQuery selectors, JSON keys) --
    NEVER spaced: the first version of this pass corrupted them."""
    if "/" in tok or "." in tok:
        return tok
    if tok.isascii():
        return tok
    out = []
    for i, ch in enumerate(tok):
        out.append(ch)
        if i + 1 < len(tok) and ch.islower() and tok[i + 1].isupper():
            out.append(" ")
    return "".join(out)


def _space_boundaries(seg):
    """Apply _space_token ONLY inside string literals: code identifiers
    (bindSelect, getData) must never be touched."""
    brands = {}
    for i, b in enumerate(BRANDS):
        if b in seg:
            ph = f"__SETTBR{i}__"
            seg = seg.replace(b, ph)
            brands[ph] = b

    def fix_span(m):
        q, inner = m.group(0)[0], m.group(1)
        fixed = " ".join(_space_token(t) for t in inner.split(" "))
        return q + fixed + q

    seg = re.sub(r'"((?:[^"\\]|\\.)*)"', fix_span, seg)
    seg = re.sub(r"'((?:[^'\\]|\\.){2,})'", fix_span, seg)
    for ph, b in brands.items():
        seg = seg.replace(ph, b)
    return seg


def translate_segment(seg, protected):
    for k in KEYS:
        if k in protected: continue
        if k not in seg:
            continue
        # Boundary-space rule: two CJK runs abut in source with no separator,
        # but their Vietnamese translations are phrases that MUST be spaced
        # ("tải lên"+"trường hợp" -> "tải lên trường hợp", not
        # "tải lêntrường hợp"). When a replacement directly follows a letter
        # in the output so far and starts with a letter, prepend one space.
        # (The old post-hoc pass only caught lower->Upper joins inside string
        # literals and missed HTML text nodes entirely.)
        rep = d[k]
        if not rep or not rep[0].isalpha():
            seg = seg.replace(k, rep)
            continue
        out, i = [], 0
        while True:
            j = seg.find(k, i)
            if j < 0:
                out.append(seg[i:])
                break
            out.append(seg[i:j])
            if j > 0 or out:
                prev = "".join(out)
                if prev and prev[-1].isalpha():
                    out.append(" ")
            out.append(rep)
            i = j + len(k)
        seg = "".join(out)
    return _space_boundaries(seg)


IDENTITY = sorted([k for k, v in d.items() if k == v], key=len, reverse=True)


def mask_identity(text):
    """Mask demo proper nouns mapped to themselves: longest-first replacement
    with placeholders, so shorter fragment keys can never match inside them."""
    originals = []
    for i, k in enumerate(IDENTITY):
        if k in text:
            ph = f"__SETTID{i}__"
            text = text.replace(k, ph)
            originals.append((ph, k))
    return text, originals


def translate_cs(path):
    txt = open(path, encoding="utf-8").read()
    txt, id_originals = mask_identity(txt)
    masked, originals = mask_spans(txt, protected_spans(txt, False))
    parts = split_cs_comments(masked)
    out = "".join(translate_segment(s, set()) if not is_c else s for is_c, s in parts)
    open(path, "w", encoding="utf-8").write(unmask(unmask(out, originals), id_originals))


def translate_cshtml(path):
    txt = open(path, encoding="utf-8").read()
    txt, id_originals = mask_identity(txt)
    masked, originals = mask_spans(txt, protected_spans(txt, True))
    # mask @* *@ and <!-- --> comments
    segs = []
    i, n = 0, len(masked)
    while i < n:
        if masked.startswith("@*", i):
            j = masked.find("*@", i + 2)
            j = n if j < 0 else j + 2
            segs.append((True, masked[i:j])); i = j
        elif masked.startswith("<!--", i):
            j = masked.find("-->", i + 4)
            j = n if j < 0 else j + 3
            segs.append((True, masked[i:j])); i = j
        else:
            j = len(masked)
            for pat in ("@*", "<!--"):
                k = masked.find(pat, i)
                if k >= 0: j = min(j, k)
            segs.append((False, masked[i:j])); i = j
    out_parts = []
    for is_c, seg in segs:
        if is_c:
            out_parts.append(seg)
        else:
            sub = split_cs_comments(seg)
            out_parts.append("".join(translate_segment(s, set()) if not c else s for c, s in sub))
    open(path, "w", encoding="utf-8").write(unmask(unmask("".join(out_parts), originals), id_originals))


def rescan(path):
    txt = open(path, encoding="utf-8", errors="ignore").read()
    if path.endswith(".cs"):
        # #region names are compiler directives, never rendered (like comments)
        txt = "\n".join(ln for ln in txt.split("\n")
                        if not re.match(r"\s*#(?:region|endregion)\b", ln))
        segs = [(s, c) for c, s in split_cs_comments(txt)]
        body = "".join(s for s, c in segs if not c)
    else:
        body = re.sub(r"@\*.*?\*@", "", txt, flags=re.S)
        body = re.sub(r"<!--.*?-->", "", body, flags=re.S)
        # JS /* */ block comments are never rendered either
        body = re.sub(r"/\*.*?\*/", "", body, flags=re.S)
        body = "".join(seg for is_c, seg in split_cs_comments(body) if not is_c)
        # href/src values are paths (masked during translation too)
        body = re.sub(r'''(?:href|src)\s*=\s*"[^"]*"''', "", body)
        body = re.sub(r"""(?:href|src)\s*=\s*'[^']*'""", "", body)
    runs = CJK_RUN.findall(body)
    return runs


if __name__ == "__main__":
    targets = []
    for root, _dd, files in os.walk(os.path.join(DST_AREA, "Controllers")):
        for fn in files:
            if fn.endswith(".cs"):
                targets.append(("cs", os.path.join(root, fn),
                                os.path.join(SRC_AREA, "Controllers", fn)))
    for root, _dd, files in os.walk(os.path.join(DST_AREA, "Views")):
        for fn in files:
            if fn.endswith(".cshtml"):
                rel = os.path.relpath(os.path.join(root, fn), os.path.join(DST_AREA, "Views"))
                targets.append(("cshtml", os.path.join(root, fn),
                                os.path.join(SRC_AREA, "Views", rel)))
    for extra in EXTRA:
        kind = "cs" if extra.endswith(".cs") else "cshtml"
        targets.append((kind, extra, extra))
    leftover = {}
    for kind, path, src in targets:
        if kind == "cs":
            translate_cs(path)
        else:
            translate_cshtml(path)
        runs = rescan(path)
        if runs:
            rel = os.path.relpath(path, "/home/hatch/workspace/openhis-core")
            leftover[rel] = runs
            from collections import Counter
            print(f"LEFTOVER {rel}: {sum(Counter(runs).values())} chars, unique={len(set(runs))}")
    uniq = set()
    for runs in leftover.values():
        uniq |= set(runs)
    print(f"TOTAL leftover unique runs: {len(uniq)}")
    for r in sorted(uniq):
        print("  RUN:", r)
    json.dump(sorted(uniq), open(f"/home/hatch/workspace/openhis-core/leftover-{AREA}.json", "w", encoding="utf-8"), ensure_ascii=False)
