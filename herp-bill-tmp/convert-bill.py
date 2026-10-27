#!/usr/bin/env python3
"""Mechanical Framework->Core6 conversion for Herp BillManage/DeliveryManage infra files.
Reads from the read-only source tree, writes into herp-bill-tmp. Hand-fixes follow
for FailedException / process classes / UpdateYfsl (see notes in converted files).
"""
import re, os, sys

SRC = os.path.expanduser("~/workspace/lmsoft_openhis_run/Newtouch.HIS.Herp")
DST = os.path.expanduser("~/workspace/openhis-core/herp-bill-tmp/Newtouch.Herp.Infrastructure")

JOBS = [
    # (source rel, dest rel)
    ("Newtouch.Herp.DomainServices/Bill/KfApplyOrderDmnService.cs", "DomainServices/Bill/KfApplyOrderDmnService.cs"),
    ("Newtouch.Herp.DomainServices/Bill/KfCrkdjDmnService.cs", "DomainServices/Bill/KfCrkdjDmnService.cs"),
    ("Newtouch.Herp.DomainServices/Purchase/CgOrderDmnService.cs", "DomainServices/Purchase/CgOrderDmnService.cs"),
    ("Newtouch.Herp.DomainServices/Purchase/CgPurchaseOrderDmnService.cs", "DomainServices/Purchase/CgPurchaseOrderDmnService.cs"),
    ("Newtouch.Herp.Repository/Bill/KfApplyOrderRepo.cs", "Repository/Bill/KfApplyOrderRepo.cs"),
    ("Newtouch.Herp.Repository/Bill/KfApplyOrderDetailRepo.cs", "Repository/Bill/KfApplyOrderDetailRepo.cs"),
    ("Newtouch.Herp.Repository/Bill/KfCrkdjRepo.cs", "Repository/Bill/KfCrkdjRepo.cs"),
    ("Newtouch.Herp.Repository/Bill/KfCrkmxRepo.cs", "Repository/Bill/KfCrkmxRepo.cs"),
    ("Newtouch.Herp.Repository/Purchase/CgOrderRepo.cs", "Repository/Purchase/CgOrderRepo.cs"),
    ("Newtouch.Herp.Repository/Purchase/CgOrderDetailRepo.cs", "Repository/Purchase/CgOrderDetailRepo.cs"),
    ("Newtouch.Herp.Repository/Purchase/CgPurchaseOrderRepo.cs", "Repository/Purchase/CgPurchaseOrderRepo.cs"),
    ("Newtouch.Herp.Repository/Purchase/CgPurchaseOrderDetailRepo.cs", "Repository/Purchase/CgPurchaseOrderDetailRepo.cs"),
    ("Newtouch.Herp.Application/Implementation/Purchase/PurchasingPlanApp.cs", "Application/Implementation/PurchasingPlanApp.cs"),
    ("Newtouch.Herp.Infrastructure/Common/ReceiptNoManage.cs", "Common/ReceiptNoManage.cs"),
]

DROP_USING_PREFIXES = [
    "System.Data.Common", "System.Data.SqlClient",
    "FrameworkBase.", "Newtouch.Core.Common", "Newtouch.Tools",
    "Newtouch.Common.Operator", "Newtouch.Infrastructure.EF",
    "Newtouch.Herp.Infrastructure.ExException",
]
# NOTE: Newtouch.Infrastructure.EF is re-added below where EFDbTransaction is used.

def convert(text, kind):
    text = text.lstrip("\ufeff")
    # usings
    lines = text.split("\n")
    out = []
    for ln in lines:
        s = ln.strip()
        if s.startswith("using "):
            m = re.match(r"using\s+([\w\.]+)\s*;", s)
            ns = m.group(1) if m else ""
            if any(ns == p or ns.startswith(p) for p in DROP_USING_PREFIXES):
                continue
        out.append(ln)
    text = "\n".join(out)
    # namespace-scoped fixups
    text = text.replace("using Newtouch.Herp.Infrastructure;\n",
                        "using Newtouch.Herp.Infrastructure;\nusing NewtouchHIS.Lib.Base.Model;\nusing SqlSugar;\n")
    # type replacements
    text = text.replace("DbParameter[]", "SugarParameter[]")
    text = text.replace("List<DbParameter>", "List<SugarParameter>")
    text = text.replace("new SqlParameter(", "new SugarParameter(")
    text = text.replace("new DbParameter[]", "new SugarParameter[]")
    # ctors with database factory
    text = re.sub(r"public (\w+)\(IDefaultDatabaseFactory databaseFactory\)\s*:\s*base\(databaseFactory\)\s*\{\s*\}",
                  "", text)
    text = re.sub(r"public (\w+)\(IDefaultDatabaseFactory databaseFactory\)\s*:\s*base\(databaseFactory\)",
                  "public \\1()", text)
    text = text.replace("new EFDbTransaction(_databaseFactory)", "new EFDbTransaction(Db)")
    text = text.replace("new EFDbTransaction(new DefaultDatabaseFactory())",
                        "new EFDbTransaction(HerpDb.GetConnection())")
    text = text.replace("db.ExecuteSqlCommandNoLog(", "db.Db.Ado.ExecuteCommand(")
    text = text.replace("db.ExecuteSqlCommand(", "db.Db.Ado.ExecuteCommand(")
    # EFDbTransaction needs its namespace wherever used
    if "EFDbTransaction" in text and "Newtouch.Infrastructure.EF" not in text:
        text = text.replace("using Newtouch.Herp.Infrastructure;\n",
                            "using Newtouch.Herp.Infrastructure;\nusing Newtouch.Infrastructure.EF;\n", 1)
    # LogCore 3-arg string form -> 2-arg form (copy's LogCore takes a dictionary)
    text = re.sub(r"LogCore\.Error\((\"[^\"]*\"),\s*e,\s*string\.Format\(([^)]*)\)\)",
                  r"LogCore.Error(\1, e)", text)
    # ReceiptNoManage: GBK via CodePages (Linux .NET Default encoding is UTF-8,
    # which would break the GBK-range pinyin-initial mapping)
    if "GetCharSpellCode" in text:
        text = text.replace("var zw = Encoding.Default.GetBytes(cnChar);",
                            "var zw = GbEncoding.GetBytes(cnChar);")
        text = text.replace("public static Random random = new Random();",
                            "public static Random random = new Random();\n\n        private static readonly System.Text.Encoding GbEncoding;\n\n        static ReceiptNoManage()\n        {\n            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);\n            GbEncoding = System.Text.Encoding.GetEncoding(\"GBK\");\n        }")
    # .Create() on long-Id entities -> .Create(false) (stamps audit fields only;
    # identity Id is assigned by the DB). String-Id Create(true) untouched.
    text = re.sub(r"\.Create\(\)", ".Create(false)", text)
    return text

def main():
    for src_rel, dst_rel in JOBS:
        src = os.path.join(SRC, src_rel)
        dst = os.path.join(DST, dst_rel)
        os.makedirs(os.path.dirname(dst), exist_ok=True)
        with open(src, encoding="utf-8-sig") as f:
            text = f.read()
        text = convert(text, dst_rel)
        with open(dst, "w", encoding="utf-8") as f:
            f.write(text)
        print("wrote", dst_rel)

if __name__ == "__main__":
    main()
