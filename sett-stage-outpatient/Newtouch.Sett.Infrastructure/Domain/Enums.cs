using System;
using System.ComponentModel;

namespace Newtouch.Sett.Domain
{
    /// <summary>STUB: view-side config reader. The Framework version read Sys_Config from the
    /// current org; the Core port returns neutral defaults ("" / false) until wired to SysConfigRepo.</summary>
    public static class SysConfigReader
    {
        public static string String(string code) { return ""; }
        public static bool? Bool(string code) { return false; }
        public static bool? Bool(string code, bool defaultValue) { return defaultValue; }
        public static string OrgReportLink(string code) { return ""; }
    }

    public enum EnumCardType { YBJYK, XNK, XNHJYK }
    public enum EnumRegSchedule { none = 0 }
    public enum EnumZJLX { sfz = 0 }
    public enum EnumXTZHXZ { MZYJKZH = 0 }
    public enum EnumOutpatientJzbz
    {
        [Description("Chờ khám")] Djz = 1,
        [Description("Đang khám")] Jzz = 2,
        [Description("Kết thúc khám")] Jsjz = 3
    }
    public enum EnumPrescriptionType { [Description("Thuốc")] Medicine = 1, [Description("Khoản mục trị liệu")] Treament = 2 }
    public enum EnumPayStatus { [Description("Thanh toán thành công")] Success = 1, [Description("Thanh toán thất bại")] Failed = 2 }
    public enum EnumRefundStatus { [Description("Hoàn tiền thành công")] Success = 1, [Description("Hoàn tiền thất bại")] Failed = 2, [Description("Không rõ")] UnKnown = 3 }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field == null ? null : (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attr == null ? value.ToString() : attr.Description;
        }
    }
}
