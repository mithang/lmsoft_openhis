#!/usr/bin/env python3
"""PDS Web fix batch 1 (from web_build5 401-error log). Idempotent-ish: safe to re-run."""
import glob, os, re, sys

ROOT = "/home/hatch/workspace/openhis-core/Newtouch.HIS.PDS"
WEB = ROOT + "/Newtouch.PDS.Web"
INF = ROOT + "/Newtouch.PDS.Infrastructure"
changed = []

def edit(path, fn, label):
    t = open(path, encoding="utf-8").read()
    t2 = fn(t)
    if t2 != t:
        open(path, "w", encoding="utf-8").write(t2)
        changed.append(f"{label}: {os.path.relpath(path, ROOT)}")

def sub(t, pat, rep, count=0):
    return re.sub(pat, rep, t, count=count)

# --- 1a. Exclude 10 dead controllers from Web csproj ---
dead = [
    "Controllers/AntibioticTypeController.cs",
    "Controllers/PharmacyWindowController.cs",
    "Controllers/SysMedicineController.cs",
    "Controllers/SystemManagerController.cs",
    "Controllers/Base/ModuleController.cs",
    "Controllers/Base/OrganizeController.cs",
    "Controllers/Base/RoleAuthorizeController.cs",
    "Controllers/Base/RoleController.cs",
    "Controllers/Base/UserController.cs",
    "Areas/OutOrInStoredManage/Controllers/TransferController.cs",
]
def csproj_fn(t):
    if "Compile Remove" in t:
        return t
    removes = "\n".join(f'    <Compile Remove="{d}" />' for d in dead)
    return t.replace("</Project>", f"  <ItemGroup>\n{removes}\n  </ItemGroup>\n\n</Project>")
edit(WEB + "/Newtouch.PDS.Web.csproj", csproj_fn, "csproj-exclude")

# --- 1b. Extension usings ---
EXT = "using NewtouchHIS.Lib.Base.Extension;"
for f in glob.glob(WEB + "/**/*.cs", recursive=True):
    def add_ext(t, f=f):
        if EXT in t:
            return t
        if not re.search(r"\.(ToJson|GetDescription|ToObject<|MapperTo<|ToDescSelectList)\(", t):
            return t
        # insert after first using line
        return re.sub(r"(using [^;]+;\n)", r"\1" + EXT + "\n", t, count=1)
    edit(f, add_ext, "ext-using")
for vi in [WEB + "/Views/_ViewImports.cshtml", WEB + "/Areas/_ViewImports.cshtml"]:
    def vi_fn(t):
        out = t
        if "NewtouchHIS.Lib.Base.Extension" not in out:
            out += "@using NewtouchHIS.Lib.Base.Extension\n"
        if "Newtouch.HIS.Web.Core.Extensions" not in out:
            out += "@using Newtouch.HIS.Web.Core.Extensions\n"
        return out
    edit(vi, vi_fn, "viewimports")

# --- 1c. BottomButtonViewModel reconstruction ---
bbm = glob.glob(INF + "/**/BottomButtonViewModel.cs", recursive=True)[0]
def bbm_fn(t):
    if "WapperInlineStyle" in t:
        return t
    t = t.replace("        public int[] ShowKeyList { get; set; }",
        "        public int[] ShowKeyList { get; set; }\n\n        public string WapperInlineStyle { get; set; }\n\n        public bool F9Hidden { get; set; }")
    t = t.replace("        public string F10Text { get; set; }",
        """        public string F10Text { get; set; }

        public bool GetIndexHidden(int i)
        {
            // Only F9 has a dedicated hidden flag in the ported views (HandOutMedicine sets F9Hidden=true);
            // other keys have no hidden flag, so they are always shown.
            return i == 9 && F9Hidden;
        }

        public string GetIndexExtInlineStyle(int i)
        {
            return "";
        }

        public string GetIndexText(int i)
        {
            switch (i)
            {
                case 2: return F2Text;
                case 3: return F3Text;
                case 4: return F4Text;
                case 5: return F5Text;
                case 6: return F6Text;
                case 7: return F7Text;
                case 8: return F8Text;
                case 9: return F9Text;
                case 10: return F10Text;
                default: return "";
            }
        }""")
    return t
edit(bbm, bbm_fn, "bottombutton")

# --- 1d. Base enums copy (verbatim values + descriptions from Base EnumDB.cs) ---
enum_file = INF + "/Enum/BaseEnums.cs"
if not os.path.exists(enum_file):
    open(enum_file, "w", encoding="utf-8").write('''using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    public enum EnumBQJZQZ
    {
        [Description("每天取整")]
        Day = 1,
        [Description("每次取整")]
        Times
    }

    public enum EnumJybz
    {
        [Description("国基药")]
        Gjy = 1,
        [Description("贵安增补基药")]
        Gazbjy = 2,
        [Description("非基药")]
        Fjy = 3,
        [Description("非前述几种")]
        dbs = 4,
    }

    public enum EnumYpsx
    {
        [Description("麻醉药品")]
        Mzyp = 9,
        [Description("毒性药品")]
        Dxyp = 4,
        [Description("精一药品")]
        Jyyp = 5,
        [Description("精二药品")]
        Jeyp = 6,
    }

    // FrameworkBase binary helper used by YPDrugComparisonTable view; the ported
    // view already reads ReportUrl from config on the next line, so OrgReportLink
    // returns the same configured value ("" when unconfigured).
    public static class SysConfigReader
    {
        public static string OrgReportLink()
        {
            return Newtouch.Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("ReportUrl") ?? "";
        }
    }
}
''')
    changed.append("base-enums: Enum/BaseEnums.cs")

# --- TreeSelectModel + TreeSelectJson (Newtouch.Common) ---
tree_file = INF + "/Common/TreeModels.cs"
if not os.path.exists(tree_file):
    os.makedirs(os.path.dirname(tree_file), exist_ok=True)
    open(tree_file, "w", encoding="utf-8").write('''using System.Collections.Generic;
using Newtonsoft.Json;

namespace Newtouch.Common
{
    public class TreeSelectModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string parentId { get; set; }
    }

    public static class TreeSelectExtensions
    {
        public static string TreeSelectJson(this List<TreeSelectModel> list, string parentId)
        {
            return JsonConvert.SerializeObject(list ?? new List<TreeSelectModel>());
        }
    }
}
''')
    changed.append("tree-models: Common/TreeModels.cs")

# --- 1e. Specific fixes ---
# MapperTo in its own file, in the Lib.Base.Extension namespace (so the Extension using finds it)
mapper_file = INF + "/Shims/MapperExtensions.cs"
if not os.path.exists(mapper_file):
    open(mapper_file, "w", encoding="utf-8").write('''namespace NewtouchHIS.Lib.Base.Extension
{
    public static class PdsMapperExtensions
    {
        public static TTo MapperTo<TTo>(this object src) where TTo : new()
        {
            if (src == null) return default(TTo);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TTo>(Newtonsoft.Json.JsonConvert.SerializeObject(src));
        }
        public static TTo MapperTo<TFrom, TTo>(this TFrom src) where TTo : new()
        {
            if (src == null) return default(TTo);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TTo>(Newtonsoft.Json.JsonConvert.SerializeObject(src));
        }
    }
}
''')
    changed.append("mapperto: Shims/MapperExtensions.cs")

util = INF + "/Shims/UtilShims.cs"
def util_fn(t):
    # msg alias on both DefaultResponse classes
    t = t.replace("            public string message { get; set; }\n            public string sub_msg { get; set; }",
                  "            public string message { get; set; }\n            public string msg { get => message; set => message = value; }\n            public string sub_msg { get; set; }")
    # WidthTimes on ExcelColumn
    if "WidthTimes" not in t:
        t = t.replace("        public Func<object, object> FuncGetCellValue { get; set; }",
                      "        public Func<object, object> FuncGetCellValue { get; set; }\n        public double WidthTimes { get; set; }")
    return t
edit(util, util_fn, "utilshims")

# Program.cs: drop Utils using, qualify ConfigurationHelper
def prog_fn(t):
    t = t.replace("using Newtouch.Core.Common.Utils;\n", "")
    t = t.replace("ConfigurationHelper.Init(app.Configuration);",
                  "Newtouch.Core.Common.Utils.ConfigurationHelper.Init(app.Configuration);")
    return t
edit(WEB + "/Program.cs", prog_fn, "program")

# Hosp: qualify ResponseResultCode (both uses are APIRequestHelper's SUCCESS)
def hosp_fn(t):
    return t.replace("ResponseResultCode.SUCCESS",
                     "Newtouch.Common.Web.APIRequestHelper.ResponseResultCode.SUCCESS")
edit(WEB + "/Controllers/HospitalizationPharmacyController.cs", hosp_fn, "hosp-rrc")

# ClientsData: cast anonymous select to object
def cli_fn(t):
    return t.replace(".Select(d => new { Name = d.Name, Code = d.Code, zt = d.zt })",
                     ".Select(d => (object)new { Name = d.Name, Code = d.Code, zt = d.zt })")
edit(WEB + "/Controllers/ClientsDataController.cs", cli_fn, "clients-cast")

# Home: ToList on yfbmCodeList
def home_fn(t):
    return t.replace("opr.yfbmCodeList = _sysUserDmnService.GetYfbmCodeListByUserId(opr.UserId, opr.OrganizeId);",
                     "opr.yfbmCodeList = _sysUserDmnService.GetYfbmCodeListByUserId(opr.UserId, opr.OrganizeId)?.ToList();")
edit(WEB + "/Controllers/HomeController.cs", home_fn, "home-tolist")

# PharmDS: QueryString -> Query, ToList<T> -> ToObject<List<T>>
def pds_fn(t):
    t = t.replace('Request.QueryString["fph"]', 'Request.Query["fph"].ToString()')
    t = t.replace('Request.QueryString["gys[]"]', 'Request.Query["gys[]"].ToString()')
    t = t.replace("resultObjArr.ToList<SaveInventoryInfoVO>()", "resultObjArr.ToObject<List<SaveInventoryInfoVO>>()")
    return t
edit(WEB + "/Controllers/PharmacyDrugStorageController.cs", pds_fn, "pharmds")

# Login: WriteOutLoginLog + QuickLoginSuccess fallback stub
def login_fn(t):
    if "WriteOutLoginLog" in t and "private void WriteOutLoginLog" in t:
        return t
    stub = '''
        private void WriteOutLoginLog()
        {
            try { AppLogger.Info(string.Format("{0}退出登录", Constants.AppId)); } catch { }
        }

        // SSO token-login fallback (FrameworkBase LoginController.QuickLoginSuccess is binary-only):
        // with no validated operator session, fall back to the normal login page rather than
        // minting a session from an unverified token. Documented stub, not a silent success.
        private IActionResult QuickLoginSuccess(string account, string organizeId, string returnUrl, string withToken, System.Action acBeforeRedirect)
        {
            try { acBeforeRedirect?.Invoke(); } catch { }
            if (OperatorProvider.GetCurrent() != null && !string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Login");
        }
'''
    # insert before final closing braces of class+namespace: append before last two "}\n" occurrences
    idx = t.rstrip().rfind("}")
    # find class closing: insert before the last "    }\n}" pattern
    m = re.search(r"\n    \}\n\}\s*$", t)
    if m:
        t = t[:m.start()] + stub + t[m.start():]
    return t
edit(WEB + "/Controllers/LoginController.cs", login_fn, "login-stub")

# Home/Index view: IList cast
def homeview_fn(t):
    return t.replace("ViewBag.MediOrgList as IList<FrameworkBase.MultiOrg.Domain.Entity.SysOrganizeVEntity>",
                     "ViewBag.MediOrgList as System.Collections.IList")
edit(WEB + "/Views/Home/Index.cshtml", homeview_fn, "homeview")

# _crossDomain: Context.Request
def cross_fn(t):
    old = 'if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().IndexOf(domain.ToLower(), StringComparison.Ordinal) < 0)'
    new = 'if ((Context.Request.Scheme + "://" + Context.Request.Host + Context.Request.Path).ToLower().IndexOf(domain.ToLower(), StringComparison.Ordinal) < 0)'
    return t.replace(old, new)
edit(WEB + "/Views/Shared/_crossDomain.cshtml", cross_fn, "crossdomain")

# --- Repo / DmnService additions ---
# ISysDepartmentRepo + impl GetList(orgId, zt, keyword)
def dept_if_fn(t):
    if "GetList(string orgId, string zt" in t:
        return t
    return t.replace("        IList<SysDepartmentVEntity> GetList(string orgId);",
        "        IList<SysDepartmentVEntity> GetList(string orgId);\n\n        IList<SysDepartmentVEntity> GetList(string orgId, string zt, string keyword);")
edit(INF + "/IRepository/SystemManage/ISysDepartmentRepo.cs", dept_if_fn, "dept-if")
def dept_repo_fn(t):
    if "GetList(string orgId, string zt" in t:
        return t
    add = '''
        public IList<SysDepartmentVEntity> GetList(string orgId, string zt, string keyword)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Department with(nolock) where OrganizeId = @orgId and zt = @zt";
            var pars = new System.Collections.Generic.List<SugarParameter>
            {
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@zt", zt)
            };
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (Name like @keyword or Code like @keyword or py like @keyword)";
                pars.Add(new SugarParameter("@keyword", "%" + keyword + "%"));
            }
            return this.FindList<SysDepartmentVEntity>(sql, pars.ToArray());
        }

'''
    return t.replace("    }\n}", add + "    }\n}", 1) if False else t + ""
# simpler: insert before final closing of class by appending method before last "}\n}"
def dept_repo_fn2(t):
    if "GetList(string orgId, string zt" in t:
        return t
    add = '''        public IList<SysDepartmentVEntity> GetList(string orgId, string zt, string keyword)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Department with(nolock) where OrganizeId = @orgId and zt = @zt";
            var pars = new System.Collections.Generic.List<SugarParameter>
            {
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@zt", zt)
            };
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (Name like @keyword or Code like @keyword or py like @keyword)";
                pars.Add(new SugarParameter("@keyword", "%" + keyword + "%"));
            }
            return this.FindList<SysDepartmentVEntity>(sql, pars.ToArray());
        }

'''
    m = re.search(r"\n    \}\n\}\s*$", t)
    return t[:m.start()] + "\n" + add + t[m.start():] if m else t
edit(INF + "/SystemManage/SysDepartmentRepo.cs", dept_repo_fn2, "dept-repo")

# ISysStaffRepo + impl GetValidStaffListByOrganizeId
def staff_if_fn(t):
    if "GetValidStaffListByOrganizeId" in t:
        return t
    return t.replace("        IList<SysStaffVEntity> GetStaffListByOrganizeId(string orgId);",
        "        IList<SysStaffVEntity> GetStaffListByOrganizeId(string orgId);\n\n        IList<SysStaffVEntity> GetValidStaffListByOrganizeId(string orgId);")
edit(INF + "/IRepository/SystemManage/ISysStaffRepo.cs", staff_if_fn, "staff-if")
def staff_repo_fn(t):
    if "GetValidStaffListByOrganizeId" in t:
        return t
    add = '''        public IList<SysStaffVEntity> GetValidStaffListByOrganizeId(string orgId)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Staff with(nolock) where OrganizeId = @orgId and zt = '1'";
            return this.FindList<SysStaffVEntity>(sql, new[] { new SugarParameter("@orgId", orgId) });
        }

'''
    m = re.search(r"\n    \}\n\}\s*$", t)
    return t[:m.start()] + "\n" + add + t[m.start():] if m else t
edit(INF + "/SystemManage/SysStaffRepo.cs", staff_repo_fn, "staff-repo")

# IItemDmnService + ItemDmnService: GetItemsDetailListByOrgIdAndItemCode
# Dictionary items are top-org scoped (entity has TopOrganizeId, no OrganizeId), so the
# org parameter is applied against TopOrganizeId, mirroring GetValidItemsDetailListByTopOrgAndItemCode + zt filter.
def item_if_fn(t):
    if "GetItemsDetailListByOrgIdAndItemCode" in t:
        return t
    return t.replace("        IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrgAndItemCode(string organizeId, string code);",
        "        IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrgAndItemCode(string organizeId, string code);\n\n        IList<SysItemsDetailVEntity> GetItemsDetailListByOrgIdAndItemCode(string organizeId, string code, string zt);")
edit(INF + "/IDomainServices/SystemManage/IItemDmnService.cs", item_if_fn, "item-if")
def item_svc_fn(t):
    if "GetItemsDetailListByOrgIdAndItemCode" in t:
        return t
    add = '''        public IList<SysItemsDetailVEntity> GetItemsDetailListByOrgIdAndItemCode(string organizeId, string code, string zt)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_ItemsDetail
where 1 = 1
and(TopOrganizeId = @topOrgId or TopOrganizeId = '*')
and zt = @zt
and ItemId in (
select Id from [NewtouchHIS_Base]..V_S_Sys_Items where Code = @code
)";
            return this.FindList<SysItemsDetailVEntity>(sql, new SugarParameter[] {
                new SugarParameter("@code", code),
                new SugarParameter("@topOrgId", organizeId),
                new SugarParameter("@zt", zt)
            });
        }

'''
    m = re.search(r"\n    \}\n\}\s*$", t)
    return t[:m.start()] + "\n" + add + t[m.start():] if m else t
edit(INF + "/SystemManage/ItemDmnService.cs", item_svc_fn, "item-svc")

print(f"changed {len(changed)} files:")
for c in changed:
    print("  ", c)
