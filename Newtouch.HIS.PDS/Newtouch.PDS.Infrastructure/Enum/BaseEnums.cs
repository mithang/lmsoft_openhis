using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    public enum EnumBQJZQZ
    {
        [Description("Lấy tròn hàng ngày")]
        Day = 1,
        [Description("Lấy tròn mỗi lần")]
        Times
    }

    public enum EnumJybz
    {
        [Description("Thuốc cơ bản quốc gia")]
        Gjy = 1,
        [Description("Thuốc cơ bản bổ sung Quý An")]
        Gazbjy = 2,
        [Description("Thuốc không cơ bản")]
        Fjy = 3,
        [Description("Các loại khác")]
        dbs = 4,
    }

    public enum EnumYpsx
    {
        [Description("Thuốc gây mê")]
        Mzyp = 9,
        [Description("Thuốc độc")]
        Dxyp = 4,
        [Description("Thuốc hướng thần nhóm 1")]
        Jyyp = 5,
        [Description("Thuốc hướng thần nhóm 2")]
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
    }
}
