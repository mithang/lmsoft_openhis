#!/usr/bin/env python3
"""Round 2: apply the same signature changes to Stubs.cs method declarations
(which end with a body brace, not ';'), then fix stub bodies."""
import re, sys

BASE = "/home/hatch/workspace/openhis-core/sett-stage-outpatient"
INF = BASE + "/Newtouch.Sett.Infrastructure/Domain"

def load(p):
    with open(p, encoding="utf-8-sig") as f:
        return f.read()

def save(p, s):
    with open(p, "w", encoding="utf-8") as f:
        f.write(s)

ok = True
# core signature text without trailing ';' — matches both interface decls and stub defs
pairs = [
    ("object GetCfh(object p0)", "string GetCfh(object p0)"),
    ("object CheckStaffIsBelongDuty(object p0, object p1)", "bool CheckStaffIsBelongDuty(object p0, object p1)"),
    ("object btnReturn(object p0, object p1, object p2, out object p3)", "bool btnReturn(object p0, object p1, object p2, out bool p3)"),
    ("object btnReturnInAcc(object p0, object p1, object p2, out object p3)", "bool btnReturnInAcc(object p0, object p1, object p2, out bool p3)"),
    ("object GetGridViewMx(object p0)", "List<IRefundApp_GetGridViewMxDto> GetGridViewMx(object p0)"),
    ("IOutPatChargeApp_GetOutPatBasicInfoInAccDto GetOutPatBasicInfoInAcc(object p0, object p1)",
     "List<OutpatAccInfoDto> GetOutPatBasicInfoInAcc(object p0, object p1)"),
    ("List<IOutPatChargeDmnService_GetOutpatChargePatInfoInAccDto> GetOutpatChargePatInfoInAcc(object p0, object p1, object p2, object p3, object p4)",
     "List<OutpatAccInfoDto> GetOutpatChargePatInfoInAcc(object p0, object p1, object p2, object p3, object p4)"),
    ("object GetPatInfoInOptima(object p0, object p1, object p2)", "OutpatAccInfoDto GetPatInfoInOptima(object p0, object p1, object p2)"),
    ("object GetInfo(object p0)", "List<OutAccountInfoDto> GetInfo(object p0)"),
    ("IOutPatChargeDmnService_GetOutpatientSettArrearageVOListDto GetOutpatientSettArrearageVOList(object p0, object p1)",
     "List<IOutPatChargeDmnService_GetOutpatientSettArrearageVOListDto> GetOutpatientSettArrearageVOList(object p0, object p1)"),
    ("List<IOutPatChargeDmnService_GetPatientGridJsonDto> GetPatientGridJson(object p0)", "string GetPatientGridJson(object p0)"),
    ("object GetSettlementList(object p0, object p1, object p2, object p3, object p4, object p5)",
     "List<OutpatientSettlementVO> GetSettlementList(object p0, object p1, object p2, object p3, object p4, object p5)"),
    ("object RegChargeQuery(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10)",
     "List<OutPatientRegChargeMVO> RegChargeQuery(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10)"),
    ("object ZFRegChargeQuery(object p0, object p1, object p2, object p3)", "List<OutPatientRegChargeMVO> ZFRegChargeQuery(object p0, object p1, object p2, object p3)"),
    ("List<MzjsMxDto> LoadMzjsMXRecords(object p0)", "OutPatientReprintOrSuppPrintSettleDetailDto LoadMzjsMXRecords(object p0)"),
    ("IOutPatChargeDmnService_GetCQjzdjInfoDto GetCQjzdjInfo(object p0, object p1)", "Input_2203A GetCQjzdjInfo(object p0, object p1)"),
    ("object GetGuiAnDetailsMzjsYbTfh(object p0, object p1, object p2)", "guianMainAllOfMzjs GetGuiAnDetailsMzjsYbTfh(object p0, object p1, object p2)"),
    ("object GetGuianMainOfMzjs(object p0)", "guianMainAllOfMzjs GetGuianMainOfMzjs(object p0)"),
    ("object GetChongQingGHMzjs(object p0, object p1, object p2, object p3, object p4)",
     "List<IOutPatChargeDmnService_GetChongQingGHMzjsDto> GetChongQingGHMzjs(object p0, object p1, object p2, object p3, object p4)"),
    ("object getDateInfosjd(object p0)", "List<string> getDateInfosjd(object p0)"),
    ("object getDateInfosjdcount(object p0, object p1)", "List<string> getDateInfosjdcount(object p0, object p1)"),
    ("object getStaffListByKs(object p0, object p1)", "List<string> getStaffListByKs(object p0, object p1)"),
    ("List<IOutBookRepo_getStaffByKsDto> getStaffByKs(object p0)", "List<string> getStaffByKs(object p0)"),
    ("List<IOutBookRepo_getStaffByKsDto> getStaffByKs(object p0, object p1)", "List<string> getStaffByKs(object p0, object p1)"),
    ("object GetPagintionList(object p0, object p1, object p2)", "List<IOutBookScheduleRepo_GetPagintionListDto> GetPagintionList(object p0, object p1, object p2)"),
    ("object GetChongQingMainOfMzjs(object p0, object p1, out object p2, out object p3)",
     "object GetChongQingMainOfMzjs(object p0, object p1, out decimal p2, out decimal p3)"),
    ("CqDetailsYbTfhDto GetCQDetailsMzjsYbTfh(object p0, object p1, object p2, out object p3, out object p4, out object p5)",
     "CqDetailsYbTfhDto GetCQDetailsMzjsYbTfh(object p0, object p1, object p2, out decimal p3, out decimal p4, out decimal p5)"),
    ("string Sett(object p0, object p1, object p2, object p3, out object p4)", "string Sett(object p0, object p1, object p2, object p3, out S25ResponseDTO p4)"),
    ("string Sett(object p0, object p1, object p2, out object p3, out object p4)", "string Sett(object p0, object p1, object p2, out S25ResponseDTO p3, out string p4)"),
    ("object XnhTuiFeiProcess(object p0, object p1, object p2, object p3, object p4, object p5, out object p6)",
     "object XnhTuiFeiProcess(object p0, object p1, object p2, object p3, object p4, object p5, out TFS21RequestDTO p6)"),
    ("object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out object p6, out object p7, object p8, object p9, object p10, object p11, object p12, object p13)",
     "object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out string p6, out decimal p7, object p8, object p9, object p10, object p11, object p12, object p13)"),
    ("object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out object p6, out object p7, object p8, object p9, object p10, object p11, object p12, object p13, object p14, object p15)",
     "object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out string p6, out decimal p7, object p8, object p9, object p10, object p11, object p12, object p13, object p14, object p15)"),
    ("int TradeRefund(object p0, object p1, object p2, object p3, out object p4)", "int TradeRefund(object p0, object p1, object p2, object p3, out string p4)"),
    ("bool submitOutpatCharge(object p0, object p1, object p2, object p3, object p4, object p5, object p6, out object p7, object p8, object p9)",
     "bool submitOutpatCharge(object p0, object p1, object p2, object p3, object p4, object p5, object p6, out IList<int> p7, object p8, object p9)"),
    ("ref object p19, ref object p20, object p21", "ref short? p19, ref string p20, object p21"),
    ("ICqybMedicalReg02Repo_FindEntityDto FindEntity(object p0)",
     "CqybMedicalReg02Entity FindEntity(System.Linq.Expressions.Expression<System.Func<CqybMedicalReg02Entity, bool>> predicate)"),
    ("ISysPatientBasicInfoRepo_FindEntityDto FindEntity(object p0)",
     "SysPatientBasicInfoEntity FindEntity(System.Linq.Expressions.Expression<System.Func<SysPatientBasicInfoEntity, bool>> predicate)"),
    ("List<IRefundDmnService_RefundableQueryDto> RefundableQuery(object p0, object p1)",
     "List<OutPatientRefundableVO> RefundableQuery(object p0, object p1)"),
]
s = load(INF + "/Stubs.cs")
for old, new in pairs:
    n = s.count(old)
    if n == 0:
        print(f"MISS stub: {old[:80]!r}"); ok = False
    s = s.replace(old, new)
save(INF + "/Stubs.cs", s)

# Interfaces.cs: RefundableQuery (was List<Dto>, not object)
s = load(INF + "/Interfaces.cs")
old = "List<IRefundDmnService_RefundableQueryDto> RefundableQuery(object p0, object p1);"
if old not in s:
    print("MISS iface RefundableQuery"); ok = False
s = s.replace(old, "List<OutPatientRefundableVO> RefundableQuery(object p0, object p1);")
save(INF + "/Interfaces.cs", s)

# Stub bodies
s = load(INF + "/Stubs.cs")
def fix_body(s, sig_frag, old_ret, new_ret):
    global ok
    idx = s.find(sig_frag)
    if idx < 0:
        print(f"MISS stub method: {sig_frag[:60]}"); ok = False; return s
    end = s.find("\n        }", idx)
    block = s[idx:end]
    if old_ret not in block:
        print(f"MISS stub body {sig_frag[:40]}: {old_ret!r}"); ok = False; return s
    return s[:idx] + block.replace(old_ret, new_ret, 1) + s[end:]

for sig, old, new in [
    ("bool CheckStaffIsBelongDuty", "return null;", "return false;"),
    ("bool btnReturn(", "return null;", "return false;"),
    ("bool btnReturnInAcc(", "return null;", "return false;"),
    ("object GetChongQingMainOfMzjs", "p2 = null;\n            p3 = null;", "p2 = 0;\n            p3 = 0;"),
    ("CqDetailsYbTfhDto GetCQDetailsMzjsYbTfh", "p3 = null;\n            p4 = null;\n            p5 = null;", "p3 = 0;\n            p4 = 0;\n            p5 = 0;"),
    ("object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out string p6, out decimal p7, object p8, object p9, object p10, object p11, object p12, object p13)",
     "p7 = null;", "p7 = 0;"),
    ("object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out string p6, out decimal p7, object p8, object p9, object p10, object p11, object p12, object p13, object p14, object p15)",
     "p7 = null;", "p7 = 0;"),
]:
    s = fix_body(s, sig, old, new)
save(INF + "/Stubs.cs", s)
print("ALL OK" if ok else "SOME MISSES")
sys.exit(0 if ok else 1)
