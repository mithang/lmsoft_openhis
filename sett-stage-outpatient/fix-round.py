#!/usr/bin/env python3
"""Bulk fix round for sett-stage-outpatient build errors: interface/DTO type fixes."""
import re, sys

BASE = "/home/hatch/workspace/openhis-core/sett-stage-outpatient"
INF = BASE + "/Newtouch.Sett.Infrastructure/Domain"
CTL = BASE + "/Newtouch.Sett.Web/Areas/OutpatientManage/Controllers"

def load(p):
    with open(p, encoding="utf-8-sig") as f:
        return f.read()

def save(p, s):
    with open(p, "w", encoding="utf-8") as f:
        f.write(s)

def rep(s, old, new, path, count=1):
    n = s.count(old)
    if n < count:
        print(f"MISS in {path}: {old[:90]!r} (found {n}, need {count})")
        return s, False
    return s.replace(old, new), True

ok = True
def apply(path, pairs):
    global ok
    s = load(path)
    for old, new in pairs:
        s, good = rep(s, old, new, path)
        ok = ok and good
    save(path, s)

# ---------- 1. Interface + stub signature pairs (identical text in both files) ----------
sig_pairs = [
    ("object GetCfh(object p0);", "string GetCfh(object p0);"),
    ("object CheckStaffIsBelongDuty(object p0, object p1);", "bool CheckStaffIsBelongDuty(object p0, object p1);"),
    ("object btnReturn(object p0, object p1, object p2, out object p3);", "bool btnReturn(object p0, object p1, object p2, out bool p3);"),
    ("object btnReturnInAcc(object p0, object p1, object p2, out object p3);", "bool btnReturnInAcc(object p0, object p1, object p2, out bool p3);"),
    ("object GetGridViewMx(object p0);", "List<IRefundApp_GetGridViewMxDto> GetGridViewMx(object p0);"),
    ("IOutPatChargeApp_GetOutPatBasicInfoInAccDto GetOutPatBasicInfoInAcc(object p0, object p1);",
     "List<OutpatAccInfoDto> GetOutPatBasicInfoInAcc(object p0, object p1);"),
    ("List<IOutPatChargeDmnService_GetOutpatChargePatInfoInAccDto> GetOutpatChargePatInfoInAcc(object p0, object p1, object p2, object p3, object p4);",
     "List<OutpatAccInfoDto> GetOutpatChargePatInfoInAcc(object p0, object p1, object p2, object p3, object p4);"),
    ("object GetPatInfoInOptima(object p0, object p1, object p2);", "OutpatAccInfoDto GetPatInfoInOptima(object p0, object p1, object p2);"),
    ("object GetInfo(object p0);", "List<OutAccountInfoDto> GetInfo(object p0);"),
    ("IOutPatChargeDmnService_GetOutpatientSettArrearageVOListDto GetOutpatientSettArrearageVOList(object p0, object p1);",
     "List<IOutPatChargeDmnService_GetOutpatientSettArrearageVOListDto> GetOutpatientSettArrearageVOList(object p0, object p1);"),
    ("List<IOutPatChargeDmnService_GetPatientGridJsonDto> GetPatientGridJson(object p0);", "string GetPatientGridJson(object p0);"),
    ("object GetSettlementList(object p0, object p1, object p2, object p3, object p4, object p5);",
     "List<OutpatientSettlementVO> GetSettlementList(object p0, object p1, object p2, object p3, object p4, object p5);"),
    ("object RegChargeQuery(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10);",
     "List<OutPatientRegChargeMVO> RegChargeQuery(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10);"),
    ("object ZFRegChargeQuery(object p0, object p1, object p2, object p3);", "List<OutPatientRegChargeMVO> ZFRegChargeQuery(object p0, object p1, object p2, object p3);"),
    ("List<MzjsMxDto> LoadMzjsMXRecords(object p0);", "OutPatientReprintOrSuppPrintSettleDetailDto LoadMzjsMXRecords(object p0);"),
    ("IOutPatChargeDmnService_GetCQjzdjInfoDto GetCQjzdjInfo(object p0, object p1);", "Input_2203A GetCQjzdjInfo(object p0, object p1);"),
    ("object GetGuiAnDetailsMzjsYbTfh(object p0, object p1, object p2);", "guianMainAllOfMzjs GetGuiAnDetailsMzjsYbTfh(object p0, object p1, object p2);"),
    ("object GetGuianMainOfMzjs(object p0);", "guianMainAllOfMzjs GetGuianMainOfMzjs(object p0);"),
    ("object GetChongQingGHMzjs(object p0, object p1, object p2, object p3, object p4);",
     "List<IOutPatChargeDmnService_GetChongQingGHMzjsDto> GetChongQingGHMzjs(object p0, object p1, object p2, object p3, object p4);"),
    ("object getDateInfosjd(object p0);", "List<string> getDateInfosjd(object p0);"),
    ("object getDateInfosjdcount(object p0, object p1);", "List<string> getDateInfosjdcount(object p0, object p1);"),
    ("object getStaffListByKs(object p0, object p1);", "List<string> getStaffListByKs(object p0, object p1);"),
    ("List<IOutBookRepo_getStaffByKsDto> getStaffByKs(object p0);", "List<string> getStaffByKs(object p0);"),
    ("List<IOutBookRepo_getStaffByKsDto> getStaffByKs(object p0, object p1);", "List<string> getStaffByKs(object p0, object p1);"),
    ("object GetPagintionList(object p0, object p1, object p2);", "List<IOutBookScheduleRepo_GetPagintionListDto> GetPagintionList(object p0, object p1, object p2);"),
    ("object GetChongQingMainOfMzjs(object p0, object p1, out object p2, out object p3);",
     "object GetChongQingMainOfMzjs(object p0, object p1, out decimal p2, out decimal p3);"),
    ("CqDetailsYbTfhDto GetCQDetailsMzjsYbTfh(object p0, object p1, object p2, out object p3, out object p4, out object p5);",
     "CqDetailsYbTfhDto GetCQDetailsMzjsYbTfh(object p0, object p1, object p2, out decimal p3, out decimal p4, out decimal p5);"),
    ("string Sett(object p0, object p1, object p2, object p3, out object p4);", "string Sett(object p0, object p1, object p2, object p3, out S25ResponseDTO p4);"),
    ("string Sett(object p0, object p1, object p2, out object p3, out object p4);", "string Sett(object p0, object p1, object p2, out S25ResponseDTO p3, out string p4);"),
    ("object XnhTuiFeiProcess(object p0, object p1, object p2, object p3, object p4, object p5, out object p6);",
     "object XnhTuiFeiProcess(object p0, object p1, object p2, object p3, object p4, object p5, out TFS21RequestDTO p6);"),
    ("object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out object p6, out object p7, object p8, object p9, object p10, object p11, object p12, object p13);",
     "object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out string p6, out decimal p7, object p8, object p9, object p10, object p11, object p12, object p13);"),
    ("object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out object p6, out object p7, object p8, object p9, object p10, object p11, object p12, object p13, object p14, object p15);",
     "object RefundSettlement(object p0, object p1, object p2, object p3, object p4, out object p5, out string p6, out decimal p7, object p8, object p9, object p10, object p11, object p12, object p13, object p14, object p15);"),
    ("int TradeRefund(object p0, object p1, object p2, object p3, out object p4);", "int TradeRefund(object p0, object p1, object p2, object p3, out string p4);"),
    ("bool submitOutpatCharge(object p0, object p1, object p2, object p3, object p4, object p5, object p6, out object p7, object p8, object p9);",
     "bool submitOutpatCharge(object p0, object p1, object p2, object p3, object p4, object p5, object p6, out IList<int> p7, object p8, object p9);"),
    ("ref object p19, ref object p20, object p21", "ref short? p19, ref string p20, object p21"),
    ("ICqybMedicalReg02Repo_FindEntityDto FindEntity(object p0);",
     "CqybMedicalReg02Entity FindEntity(System.Linq.Expressions.Expression<System.Func<CqybMedicalReg02Entity, bool>> predicate);"),
    ("ISysPatientBasicInfoRepo_FindEntityDto FindEntity(object p0);",
     "SysPatientBasicInfoEntity FindEntity(System.Linq.Expressions.Expression<System.Func<SysPatientBasicInfoEntity, bool>> predicate);"),
]
apply(INF + "/Interfaces.cs", sig_pairs)
apply(INF + "/Stubs.cs", sig_pairs)

# RefundableQuery + sync GetList (regex: keep params, change return type)
for path, pat, newret in [
    (INF + "/Interfaces.cs", r"object RefundableQuery\(object p0, object p1\)", "List<OutPatientRefundableVO> RefundableQuery(object p0, object p1)"),
    (INF + "/Stubs.cs", r"object RefundableQuery\(object p0, object p1\)", "List<OutPatientRefundableVO> RefundableQuery(object p0, object p1)"),
    (INF + "/Interfaces.cs", r"object GetList\(object p0, object p1, object mzh, object zyh, object zlsgh\)",
     "List<SyncTreatmentServiceRecordVO> GetList(object p0, object p1, object mzh, object zyh, object zlsgh)"),
    (INF + "/Stubs.cs", r"object GetList\(object p0, object p1, object mzh, object zyh, object zlsgh\)",
     "List<SyncTreatmentServiceRecordVO> GetList(object p0, object p1, object mzh, object zyh, object zlsgh)"),
]:
    s = load(path)
    s2, n = re.subn(pat, newret, s)
    if n == 0:
        print(f"MISS regex in {path}: {pat}"); ok = False
    save(path, s2)

# ---------- 2. Stub bodies: value-type returns / out assignments ----------
s = load(INF + "/Stubs.cs")
def fix_stub_body(s, sig_frag, old_ret, new_ret):
    # within the method block starting at sig_frag, replace first old_ret with new_ret
    idx = s.find(sig_frag)
    if idx < 0:
        print(f"MISS stub method: {sig_frag[:60]}"); return s, False
    end = s.find("\n        }", idx)
    block = s[idx:end]
    if old_ret not in block:
        print(f"MISS stub body {sig_frag[:40]}: {old_ret!r}"); return s, False
    return s[:idx] + block.replace(old_ret, new_ret, 1) + s[end:], True

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
    s, good = fix_stub_body(s, sig, old, new)
    ok = ok and good
save(INF + "/Stubs.cs", s)

# ---------- 3. Interfaces.cs DTO classes ----------
s = load(INF + "/Interfaces.cs")
dto_pairs = [
    ("""    public class IOutPatChargeApp_GetOutPatChargeInfoDto
    {
        public object brxz { get; set; }
        public object brxzList { get; set; }
        public object patid { get; set; }
    }""",
     """    public class IOutPatChargeApp_GetOutPatChargeInfoDto
    {
        public string brxz { get; set; }
        public List<string> brxzList { get; set; }
        public string patid { get; set; }
    }"""),
    ("""    public class IOutPatChargeDmnService_GetOutpatientSettArrearageVOListDto
    {
        public object Select { get; set; }
    }""",
     """    public class IOutPatChargeDmnService_GetOutpatientSettArrearageVOListDto
    {
        public string jsnm { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal zje { get; set; }
    }"""),
    ("""    public class SyncTreatmentUnitDto
    {
        public object hidden { get; set; }
    }""",
     """    public class SyncTreatmentUnitDto
    {
        public int hidden { get; set; }
    }"""),
    ("""    public class SettZffsDto
    {
        public object jsnm { get; set; }
        public object xjzffsmc { get; set; }
    }""",
     """    public class SettZffsDto
    {
        public string jsnm { get; set; }
        public string xjzffsmc { get; set; }
    }"""),
    ("""    public class CqDetailsYbTfhDto
    {
    }""",
     """    public class CqDetailsYbTfhDto
    {
        public List<CqDetailsYbTfhItemDto> cflist { get; set; }
    }

    public class CqDetailsYbTfhItemDto
    {
        public string cfh { get; set; }
    }

    public class IRefundApp_GetGridViewMxDto
    {
        public string IS_RETURN { get; set; }
        public string czh { get; set; }
    }

    public class IOutBookScheduleRepo_GetPagintionListDto
    {
        public string czks { get; set; }
        public string ysgh { get; set; }
        public string ysxm { get; set; }
    }

    public class IOutPatChargeDmnService_GetChongQingGHMzjsDto
    {
        public int issc { get; set; }
        public decimal je { get; set; }
    }"""),
    ("""    public class RefundZtDto
    {
        public object jsmxnm { get; set; }
        public object sl { get; set; }
        public object ztId { get; set; }
    }""",
     """    public class RefundZtDto
    {
        public int jsmxnm { get; set; }
        public decimal sl { get; set; }
        public string ztId { get; set; }
    }"""),
]
for old, new in dto_pairs:
    s, good = rep(s, old, new, "Interfaces.cs DTO")
    ok = ok and good
save(INF + "/Interfaces.cs", s)

# ---------- 4. Types.cs ----------
s = load(INF + "/Types.cs")
type_pairs = [
    ("""    public class CQMzjs05Dto
    {
        public string IsNull { get; set; }""",
     """    public class CQMzjs05Dto
    {
        public bool IsNull() { return false; }"""),
    ("""    public class CqybUpdateMedicalInput03Entity
    {
    }""",
     """    public class CqybUpdateMedicalInput03Entity
    {
        public string OrganizeId { get; set; }
        public string zt { get; set; }
    }"""),
    ("""    public class CqybMedicalReg02Entity
    {
        public string OrganizeId { get; set; }
        public string jytype { get; set; }
        public string zt { get; set; }
    }""",
     """    public class CqybMedicalReg02Entity
    {
        public string OrganizeId { get; set; }
        public string jytype { get; set; }
        public string zt { get; set; }
        public string zymzh { get; set; }
    }"""),
    ("""    public class GridViewMx
    {
    }""",
     """    public class GridViewMx
    {
        public string IS_RETURN { get; set; }
        public string czh { get; set; }
    }"""),
    ("        public string jsmxnm { get; set; }\n        public string klsj { get; set; }",
     "        public int jsmxnm { get; set; }\n        public DateTime klsj { get; set; }"),
    ("        public string jsmxje { get; set; }", "        public decimal jsmxje { get; set; }"),
    ("        public string dj { get; set; }", "        public decimal dj { get; set; }"),
    ("        public string ktsl { get; set; }", "        public int ktsl { get; set; }"),
    ("        public string sl { get; set; }", "        public int sl { get; set; }"),
    ("        public string tsl { get; set; }", "        public int tsl { get; set; }"),
    ("""    public class OutPatientRegChargeMVO
    {
    }""",
     """    public class OutPatientRegChargeMVO
    {
        public string jsnm { get; set; }
        public string zffsmcstr { get; set; }
    }"""),
    ("""    public class OutPatientRegVO
    {
        public string xnhgrbm { get; set; }
    }""",
     """    public class OutPatientRegVO
    {
        public string xnhgrbm { get; set; }
        public string mzh { get; set; }
        public string blh { get; set; }
    }"""),
    ("""    public class OutpatAccBasicInfoDto
    {
    }""",
     """    public class OutpatAccBasicInfoDto
    {
        public string mzh { get; set; }
        public string fph { get; set; }
        public bool isQfyj { get; set; }
        public DateTime? sfrq { get; set; }
    }"""),
    ("""    public class OutpatAccInfoDto
    {
    }""",
     """    public class OutpatAccInfoDto
    {
        public string mzh { get; set; }
        public string blh { get; set; }
        public string xm { get; set; }
        public string ghrq { get; set; }
        public string ghzt { get; set; }
        public string patid { get; set; }
        public string brxz { get; set; }
    }"""),
    ("""    public class OutpatChargeDto
    {
    }""",
     """    public class OutpatChargeDto
    {
        public IOutPatChargeApp_GetOutPatChargeInfoDto patInfo { get; set; }
        public List<OutPatChargeItemVO> ghItemList { get; set; }
        public OutPatChargeDoctorVO ghDocVo { get; set; }
    }"""),
    ("""    public class OutpatOptimAccInfoDto
    {
        public string OutpatAccInfoDto { get; set; }
        public string OutpatAccListDto { get; set; }
    }""",
     """    public class OutpatOptimAccInfoDto
    {
        public OutpatAccInfoDto OutpatAccInfoDto { get; set; }
        public List<OutpatAccInfoDto> OutpatAccListDto { get; set; }
        public List<OutAccountInfoDto> OptimAccInfoDto { get; set; }
    }"""),
    ("""    public class OutpatOptimInfoDto
    {
    }""",
     """    public class OutpatOptimInfoDto
    {
        public OutpatAccInfoDto OutpatAccInfoDto { get; set; }
        public List<SyncTreatmentServiceRecordVO> SyncTreatmentServiceRecordVO { get; set; }
    }"""),
    ("""    public class OutpatientSettlementVO
    {
    }""",
     """    public class OutpatientSettlementVO
    {
        public string jsnm { get; set; }
        public string zffsmcstr { get; set; }
    }"""),
    ("""    public class Output_2207VO
    {
    }""",
     """    public class Output_2207VO
    {
        public string setl_id { get; set; }
        public string jsnm { get; set; }
        public string hifp_pay { get; set; }
        public string acct_pay { get; set; }
        public string psn_cash_pay { get; set; }
        public string cvlserv_pay { get; set; }
        public string hifob_pay { get; set; }
        public string hosp_part_amt { get; set; }
        public string balc { get; set; }
        public string pch { get; set; }
        public string yllb { get; set; }
    }"""),
    ("""    public class SysPatientBasicInfoEntity
    {
    }""",
     """    public class SysPatientBasicInfoEntity
    {
        public string OrganizeId { get; set; }
        public string zt { get; set; }
        public string blh { get; set; }
    }"""),
]
for old, new in type_pairs:
    s, good = rep(s, old, new, "Types.cs")
    ok = ok and good
# new classes (append before final closing brace of namespace)
if "class guianMainAllOfMzjs" not in s:
    s = s.rstrip()
    assert s.endswith("}")
    s = s[:-1] + """
    public class guianMainAllOfMzjs
    {
    }

    public class Input_2203A
    {
    }

    public class TFS21RequestDTO
    {
    }

    public class OutPatientReprintOrSuppPrintSettleDetailDto
    {
    }

    public class SyncTreatmentServiceRecordVO
    {
    }

}"""
save(INF + "/Types.cs", s)

# ---------- 5. Helpers.cs ----------
s = load(INF + "/Helpers.cs")
h_pairs = [
    ("""    public static class APIRequestHelper
    {""",
     """    public static class APIRequestHelper
    {
        // SUCCESS value is a stub-phase placeholder: the original lives in the
        // binary-only Newtouch.Common.Web assembly (his-dll-common) and the
        // stub helpers below never return a non-null response, so the value has
        // no runtime effect in phase 1. Confirm against the real CIS/PDS
        // contract at merge time.
        public static class ResponseResultCode { public const string SUCCESS = "200"; }"""),
    ("""    public static class SitePDSAPIHelper
    {""",
     """    public static class SitePDSAPIHelper
    {
        public static string GetToken() { return ""; }"""),
]
for old, new in h_pairs:
    s, good = rep(s, old, new, "Helpers.cs")
    ok = ok and good
if "class NLogger" not in s:
    s = s.rstrip()
    assert s.endswith("}")
    s = s[:-1] + """
    // Stub for Newtouch.Core.NLogger (binary-only in the Framework build).
    public class NLogger
    {
        public void Info(string message) { }
    }

}"""
save(INF + "/Helpers.cs", s)

# ---------- 6. Controllers ----------
def ctl(name):
    return CTL + "/" + name

# OutpatChargeController: usings + Serialize replacement + Yibao stub
p = ctl("OutpatChargeController.cs"); s = load(p)
for old, new in [
    ("using System.Net.Security;", "using System.Net.Security;\nusing System.Threading.Tasks;\nusing Newtonsoft.Json;"),
    ("var data = datadsf.Serialize().ToObject<List<OutPatientRegVO>>() ?? new List<OutPatientRegVO>();",
     "var data = JsonConvert.DeserializeObject<List<OutPatientRegVO>>(datadsf) ?? new List<OutPatientRegVO>();"),
    ("var data = tdata.Serialize().ToObject<List<OutPatientRegVO>>() ?? new List<OutPatientRegVO>();",
     "var data = JsonConvert.DeserializeObject<List<OutPatientRegVO>>(tdata) ?? new List<OutPatientRegVO>();"),
    ("""            var data = YibaoUniteInterface.QCatalog("1", "1311", "", "");
            return Success(null, data);""",
     """            return Error("Kiểm tra kết nối bảo hiểm chưa được triển khai trong bản .NET Core này.");"""),
]:
    s, good = rep(s, old, new, p); ok = ok and good
save(p, s)

# RefundController: usings + Tools.Json replacement
p = ctl("RefundController.cs"); s = load(p)
for old, new in [
    ("using NewtouchHIS.Lib.Base.Model;", "using NewtouchHIS.Lib.Base.Model;\nusing NewtouchHIS.Lib.Base.Exceptions;\nusing Newtonsoft.Json;"),
    ("Tools.Json.ToList<GridViewMx>(refundData)", "JsonConvert.DeserializeObject<List<GridViewMx>>(refundData)"),
]:
    s, good = rep(s, old, new, p); ok = ok and good
save(p, s)

# OutpatientRefundController: usings (add if absent)
p = ctl("OutpatientRefundController.cs"); s = load(p)
if "using System.Threading;" not in s:
    s = s.replace("using System;", "using System;\nusing System.Threading;", 1)
if "NewtouchHIS.Lib.Base.Exceptions" not in s:
    s = s.replace("using NewtouchHIS.Lib.Base.Model;", "using NewtouchHIS.Lib.Base.Model;\nusing NewtouchHIS.Lib.Base.Exceptions;", 1)
    if "NewtouchHIS.Lib.Base.Exceptions" not in s:
        s = s.replace("using System;", "using System;\nusing NewtouchHIS.Lib.Base.Exceptions;", 1)
save(p, s)

# OutpatientRegScheduleController: JsonRequestBehavior + OperatorProvider using
p = ctl("OutpatientRegScheduleController.cs"); s = load(p)
for old, new in [
    ("return Json(data, JsonRequestBehavior.AllowGet);", "return Json(data);"),
]:
    s, good = rep(s, old, new, p); ok = ok and good
if "NewtouchHIS.Lib.Framework.Operator" not in s:
    s = s.replace("using Newtouch.Sett.Domain;", "using Newtouch.Sett.Domain;\nusing NewtouchHIS.Lib.Framework.Operator;", 1)
    if "NewtouchHIS.Lib.Framework.Operator" not in s:
        s = s.replace("using System;", "using System;\nusing NewtouchHIS.Lib.Framework.Operator;", 1)
save(p, s)

# OutpatientRegController: OperatorProvider using
p = ctl("OutpatientRegController.cs"); s = load(p)
if "NewtouchHIS.Lib.Framework.Operator" not in s:
    s = s.replace("using Newtouch.Sett.Domain;", "using Newtouch.Sett.Domain;\nusing NewtouchHIS.Lib.Framework.Operator;", 1)
    if "NewtouchHIS.Lib.Framework.Operator" not in s:
        s = s.replace("using System;", "using System;\nusing NewtouchHIS.Lib.Framework.Operator;", 1)
save(p, s)

print("ALL OK" if ok else "SOME MISSES (see above)")
sys.exit(0 if ok else 1)
