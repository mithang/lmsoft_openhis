#!/usr/bin/env python3
"""Bulk-convert Herp Framework source files to the net6.0 infra project.
Copies: Domain/Entity, Domain/DTO, Domain/ValueObjects, Domain/IRepository,
Domain/IDomainServices, Application/Interface.
Rules: drop binary-only usings, map Newtouch.Tools/Operator/Exceptions to the
net6.0 Lib equivalents, convert EF [Table]/[Key] to SqlSugar attributes.
Does NOT touch Repository impls, DmnServices, Application impls, Web (ported per-area).
"""
import os, re, shutil, sys

SRC = '/home/hatch/workspace/lmsoft_openhis_run/Newtouch.HIS.Herp'
DST = '/home/hatch/workspace/openhis-core/Newtouch.HIS.Herp/Newtouch.Herp.Infrastructure'

COPY_DIRS = [
    'Newtouch.Herp.Domain/Entity',
    'Newtouch.Herp.Domain/DTO',
    'Newtouch.Herp.Domain/ValueObjects',
    'Newtouch.Herp.Domain/IRepository',
    'Newtouch.Herp.Domain/IDomainServices',
    'Newtouch.Herp.Application/Interface',
]

USING_MAP = [
    (re.compile(r'^using FrameworkBase[^;]*;\s*$', re.M), ''),
    (re.compile(r'^using Newtouch\.HIS\.Web\.Core[^;]*;\s*$', re.M), ''),
    (re.compile(r'^using Newtouch\.Infrastructure[^;]*;\s*$', re.M), ''),
    (re.compile(r'^using Newtouch\.Tools;\s*$', re.M),
     'using NewtouchHIS.Lib.Base.Model;\nusing NewtouchHIS.Lib.Base.Extension;'),
    (re.compile(r'^using Newtouch\.Common\.Operator;\s*$', re.M),
     'using NewtouchHIS.Lib.Framework.Operator;'),
    (re.compile(r'^using Newtouch\.Core\.Common\.Exceptions;\s*$', re.M),
     'using NewtouchHIS.Lib.Base.Exceptions;'),
    (re.compile(r'^using Newtouch\.Core\.Common\.Interface;\s*$', re.M), ''),
    (re.compile(r'^using Newtouch\.Core\.Common\.Utils;\s*$', re.M), ''),
    (re.compile(r'^using Newtouch\.Core\.Common;\s*$', re.M),
     'using NewtouchHIS.Lib.Base.Model;'),
    (re.compile(r'^using Newtouch\.Core\.Redis[^;]*;\s*$', re.M), ''),
    (re.compile(r'^using Newtouch\.Common;\s*$', re.M), ''),
    (re.compile(r'^using Newtouch\.HIS\.API\.Common[^;]*;\s*$', re.M), ''),
    (re.compile(r'^using System\.Data\.Entity[^;]*;\s*$', re.M), ''),
]

def convert(text, is_entity):
    for pat, rep in USING_MAP:
        text = pat.sub(rep, text)
    if is_entity:
        # EF -> SqlSugar mapping attributes
        def table_repl(m):
            return '[SugarTable("%s")]' % m.group(1)
        text, n_table = re.subn(r'\[Table\("([^"]+)"\)\]', table_repl, text)
        text = re.sub(r'^\s*\[Key\]\s*$', '        [SugarColumn(IsPrimaryKey = true)]', text, flags=re.M)
        if n_table or 'SugarColumn' in text:
            if 'using SqlSugar;' not in text:
                # insert after first using line
                text = re.sub(r'(^using [^;]+;\s*$)', r'\1\nusing SqlSugar;', text, count=1, flags=re.M)
    # collapse 3+ blank lines
    text = re.sub(r'\n{3,}', '\n\n', text)
    return text

def main():
    copied, skipped = 0, 0
    for d in COPY_DIRS:
        srcdir = os.path.join(SRC, d)
        for root, _dirs, files in os.walk(srcdir):
            for fn in files:
                if not fn.endswith('.cs'):
                    continue
                src = os.path.join(root, fn)
                rel = os.path.relpath(src, SRC)
                # keep original relative layout, but under Infra root:
                # Newtouch.Herp.Domain/Entity/... -> Domain/Entity/...
                # Newtouch.Herp.Application/Interface/... -> Application/Interface/...
                parts = rel.split(os.sep)
                if parts[0] == 'Newtouch.Herp.Domain':
                    outrel = os.path.join('Domain', *parts[1:])
                elif parts[0] == 'Newtouch.Herp.Application':
                    outrel = os.path.join('Application', *parts[1:])
                else:
                    outrel = rel
                dst = os.path.join(DST, outrel)
                os.makedirs(os.path.dirname(dst), exist_ok=True)
                with open(src, 'rb') as f:
                    raw = f.read()
                try:
                    text = raw.decode('utf-8-sig')
                except UnicodeDecodeError:
                    text = raw.decode('cp936')  # GBK-encoded legacy file
                is_entity = '/Entity/' in rel
                text = convert(text, is_entity)
                with open(dst, 'w', encoding='utf-8') as f:
                    f.write(text)
                copied += 1
    print(f'copied {copied} files')

if __name__ == '__main__':
    main()
