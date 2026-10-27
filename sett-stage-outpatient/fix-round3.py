#!/usr/bin/env python3
"""Round 3 fixes for sett-stage-outpatient build errors."""
import sys

BASE = "/home/hatch/workspace/openhis-core/sett-stage-outpatient"
INF = BASE + "/Newtouch.Sett.Infrastructure/Domain"
CTL = BASE + "/Newtouch.Sett.Web/Areas/OutpatientManage/Controllers"

def load(p):
    with open(p, encoding="utf-8-sig") as f:
        return f.read()

def save(p, s):
    with open(p, "w", encoding="utf-8") as f:
        f.write(s)

ok = True
def rep(s, old, new, path):
    global ok
    n = s.count(old)
    if n == 0:
        print(f"MISS {path}: {old[:90]!r}"); ok = False; return s
    return s.replace(old, new)

# --- Interfaces.cs + Stubs.cs signature pairs (core text, no ';') ---
pairs = [
    ("object GetpatientAccountInfoV4(object p0, object p1)", "List<OutAccountInfoDto> GetpatientAccountInfoV4(object p0, object p1)"),
    ("object DetailsUploadYb(object p0, object p1, object p2, object p3, out object p4, out object p5)",
     "object DetailsUploadYb(object p0, object p1, object p2, object p3, out decimal p4, out decimal p5)"),
    ("object XnhTuiFeiProcess(object p0, object p1, object p2, object p3, object p4, object p5, out TFS21RequestDTO p6)",
     "string XnhTuiFeiProcess(object p0, object p1, object p2, object p3, object p4, object p5, out TFS21RequestDTO p6)"),
]
for path in [INF + "/Interfaces.cs", INF + "/Stubs.cs"]:
    s = load(path)
    for old, new in pairs:
        s = rep(s, old, new, path)
    save(path, s)

# --- Stubs.cs body: DetailsUploadYb out decimals ---
s = load(INF + "/Stubs.cs")
idx = s.find("public object DetailsUploadYb(object p0, object p1, object p2, object p3, out decimal p4, out decimal p5)")
assert idx >= 0
end = s.find("\n        }", idx)
block = s[idx:end]
assert "p4 = null;" in block and "p5 = null;" in block
s = s[:idx] + block.replace("p4 = null;", "p4 = 0;").replace("p5 = null;", "p5 = 0;") + s[end:]
save(INF + "/Stubs.cs", s)

# --- Interfaces.cs DTO props ---
s = load(INF + "/Interfaces.cs")
for old, new in [
    ("""    public class IOutpatientPrescriptionRepo_GetByCfnmListDto
    {
    }""",
     """    public class IOutpatientPrescriptionRepo_GetByCfnmListDto
    {
        public string cfh { get; set; }
        public string cfnm { get; set; }
    }"""),
    ("""    public class RelatedYpDetailDto
    {
    }""",
     """    public class RelatedYpDetailDto
    {
        public string cfh { get; set; }
        public string ypdm { get; set; }
        public string Yfbm { get; set; }
        public string jsmxnm { get; set; }
    }"""),
]:
    s = rep(s, old, new, "Interfaces.cs DTO")
save(INF + "/Interfaces.cs", s)

# --- Types.cs ---
s = load(INF + "/Types.cs")
for old, new in [
    ("""    public class BasicInfoDto2018
    {
        public string fph { get; set; }
        public string isQfyj { get; set; }
        public string mzh { get; set; }
        public string sfrq { get; set; }
    }""",
     """    public class BasicInfoDto2018
    {
        public string fph { get; set; }
        public bool isQfyj { get; set; }
        public string mzh { get; set; }
        public DateTime? sfrq { get; set; }
    }"""),
    ("""    public class OutPatChargeDto
    {
    }""",
     """    public class OutPatChargeDto
    {
        public IOutPatChargeApp_GetOutPatChargeInfoDto patInfo { get; set; }
        public List<OutPatChargeItemVO> ghItemList { get; set; }
        public OutPatChargeDoctorVO ghDocVo { get; set; }
    }"""),
    ("""    public class Input_2203A
    {
    }""",
     """    public class Input_2203A
    {
        public string mdtrt_id { get; set; }
        public string med_type { get; set; }
        public string psn_no { get; set; }
    }"""),
]:
    s = rep(s, old, new, "Types.cs")
save(INF + "/Types.cs", s)

# --- Enums.cs: SysConfigReader.Bool -> bool? + 2-arg overload ---
s = load(INF + "/Enums.cs")
s = rep(s, "        public static bool Bool(string code) { return false; }",
        "        public static bool? Bool(string code) { return false; }\n        public static bool? Bool(string code, bool defaultValue) { return defaultValue; }",
        "Enums.cs")
save(INF + "/Enums.cs", s)

# --- Controllers ---
# OutpatCharge:419 line-targeted (GetPatientGridJson action returns the JSON string itself)
p = CTL + "/OutpatChargeController.cs"
lines = load(p).split("\n")
assert lines[418].strip() == "return Content(data.ToJson());", lines[418]
lines[418] = "            return Content(data);"
save(p, "\n".join(lines))

# OutpatientRefund: handleRefund return type
p = CTL + "/OutpatientRefundController.cs"
s = load(p)
s = rep(s, "        private ActionResult handleRefund(string mzh, int jsnm", "        private IActionResult handleRefund(string mzh, int jsnm", p)
save(p, s)

print("ALL OK" if ok else "SOME MISSES")
sys.exit(0 if ok else 1)
