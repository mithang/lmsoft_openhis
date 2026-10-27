#!/usr/bin/env python3
import os, re
ROOT = "/home/hatch/workspace/openhis-core/Newtouch.HIS.PDS"
WEB = ROOT + "/Newtouch.PDS.Web"
INF = ROOT + "/Newtouch.PDS.Infrastructure"
changed = []
def edit(path, fn, label):
    t = open(path, encoding="utf-8").read()
    t2 = fn(t)
    if t2 != t:
        open(path, "w", encoding="utf-8").write(t2)
        changed.append(label)

# 1. Program.cs double qualification
edit(WEB + "/Program.cs",
     lambda t: t.replace("Newtouch.Core.Common.Utils.Newtouch.Core.Common.Utils.ConfigurationHelper",
                         "Newtouch.Core.Common.Utils.ConfigurationHelper"), "program-dedup")

# 2. TreeViewModel + TreeViewJson in Newtouch.Common
tree = INF + "/Common/TreeModels.cs"
def tree_fn(t):
    if "TreeViewModel" in t:
        return t
    return t + '''
    public class TreeViewModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public string parentId { get; set; }
        public bool isexpand { get; set; }
        public bool complete { get; set; }
        public bool showcheck { get; set; }
        public int checkstate { get; set; }
        public bool hasChildren { get; set; }
        public string Ex1 { get; set; }
    }

    public static class TreeViewExtensions
    {
        public static string TreeViewJson(this List<TreeViewModel> list, string parentId)
        {
            return JsonConvert.SerializeObject(list ?? new List<TreeViewModel>());
        }
    }
'''
edit(tree, tree_fn, "treeview")

# 3. SysConfigReader String/Int/Bool via ISysConfigRepo (PDS Sys_Config), fallback to app config
base_enums = INF + "/Enum/BaseEnums.cs"
def se_fn(t):
    if "public static string String(" in t:
        return t
    old = '''    public static class SysConfigReader
    {
        public static string OrgReportLink()
        {
            return Newtouch.Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("ReportUrl") ?? "";
        }
    }'''
    new = '''    public static class SysConfigReader
    {
        public static string OrgReportLink()
        {
            return Newtouch.Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("ReportUrl") ?? "";
        }

        private static string ReadValue(string code)
        {
            try
            {
                var orgId = NewtouchHIS.Lib.Framework.Operator.OperatorProvider.GetCurrent()?.OrganizeId;
                var repo = ServiceLocator.GetInstance<Newtouch.HIS.Domain.IRepository.ISysConfigRepo>();
                if (repo != null)
                {
                    var v = repo.GetValueByCode(code, orgId);
                    if (!string.IsNullOrWhiteSpace(v)) return v;
                }
            }
            catch { }
            return Newtouch.Core.Common.Utils.ConfigurationHelper.GetAppConfigValue(code);
        }

        public static string String(string code)
        {
            return ReadValue(code) ?? "";
        }

        public static int Int(string code)
        {
            var v = ReadValue(code);
            return int.TryParse(v, out var n) ? n : 0;
        }

        public static bool Bool(string code)
        {
            var v = ReadValue(code);
            return v == "1" || string.Equals(v, "true", System.StringComparison.OrdinalIgnoreCase);
        }
    }'''
    assert old in t, "SysConfigReader block not found"
    return t.replace(old, new)
edit(base_enums, se_fn, "sysconfigreader")

# 4. Login:104 IList -> List
edit(WEB + "/Controllers/LoginController.cs",
     lambda t: t.replace("opr.yfbmCodeList = _sysUserDmnService.GetYfbmCodeListByUserId(opr.UserId, opr.OrganizeId);",
                         "opr.yfbmCodeList = _sysUserDmnService.GetYfbmCodeListByUserId(opr.UserId, opr.OrganizeId)?.ToList();"),
     "login-tolist")

# 5. OutPatientPharmacy: qualify ResponseResultCode
edit(WEB + "/Controllers/OutPatientPharmacyController.cs",
     lambda t: t.replace("ResponseResultCode.SUCCESS",
                         "Newtouch.Common.Web.APIRequestHelper.ResponseResultCode.SUCCESS"),
     "outpat-rrc")

print(f"changed {len(changed)}: {changed}")
