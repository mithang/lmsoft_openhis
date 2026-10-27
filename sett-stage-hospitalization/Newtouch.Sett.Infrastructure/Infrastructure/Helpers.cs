using Newtouch.Sett.Domain;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;

namespace Newtouch.Sett.Infrastructure
{
    // Helpers for the HospitalizationManage port.

    public static class SettKeyHelper
    {
        // Framework EFDBBaseFuncHelper.GetNewPrimaryKeyInt equivalent (MAX+1),
        // used only by write paths (not verified read-only in this phase).
        public static int GetNewPrimaryKeyInt(string table)
        {
            if (table != "zy_xmjfb" && table != "zy_ypjfb")
            {
                throw new NotSupportedException("Bảng '" + table + "' không hỗ trợ sinh khóa mới.");
            }
            var db = SettDb.GetConnection();
            var max = db.Ado.GetInt("SELECT ISNULL(MAX(jfbbh),0) FROM " + table + " WITH (NOLOCK)");
            return max + 1;
        }
    }

    // CIS integration stub: the CIS host does not exist in this deployment.
    // Throws explicitly instead of silently succeeding.
    public static class SiteCISAPIHelper
    {
        public static void UpdateInpatientBasicInfo(InpatientPatientInfoDTO dto)
        {
            throw new NotSupportedException("Đồng bộ thông tin nhập viện sang CIS chưa được chuyển đổi trong giai đoạn này (máy chủ CIS không tồn tại trong môi trường này).");
        }
    }

    public static class Constants
    {
        public struct xtmzpz
        {
            public const string ZYTFPZ_INCLUDEYP = "mz.ZYTFPZ_INCLUDEYP";
        }
    }

    public static class SettExtensions
    {
        public static decimal ToDecimal(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0m;
            }
            if (value is decimal d)
            {
                return d;
            }
            decimal.TryParse(value.ToString(), out var result);
            return result;
        }

        public static int ToInt(this object value)
        {
            return ToInt(value, 0);
        }

        public static int ToInt(this object value, int defaultValue)
        {
            if (value == null || value == DBNull.Value)
            {
                return defaultValue;
            }
            if (value is int i)
            {
                return i;
            }
            if (int.TryParse(value.ToString(), out var result))
            {
                return result;
            }
            return defaultValue;
        }

        public static string GetDescription(this Enum value)
        {
            return GetDescription(value, value.ToString());
        }

        public static string GetDescription(this Enum value, string defaultDescription)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? defaultDescription;
        }

        // Age in years from a birth date (used by the SettPreview view).
        public static int ToAge(this DateTime? birthDate)
        {
            if (!birthDate.HasValue)
            {
                return 0;
            }
            return ToAge(birthDate.Value);
        }

        public static int ToAge(this DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }
            return age < 0 ? 0 : age;
        }

        public static int ToAge(this string birthDateText)
        {
            if (DateTime.TryParse(birthDateText, out var dt))
            {
                return ToAge(dt);
            }
            return 0;
        }
    }
}

    // Framework Newtouch.Tools.XmlToDtUntility equivalent (DataSet only; the
    // Optima XmlToIList path is stubbed in the controllers).
    public static class XmlToDtUntility
    {
        public static DataSet XmlToDataSet(string xml)
        {
            var ds = new DataSet();
            using (var sr = new StringReader(xml))
            {
                ds.ReadXml(sr);
            }
            return ds;
        }
    }
