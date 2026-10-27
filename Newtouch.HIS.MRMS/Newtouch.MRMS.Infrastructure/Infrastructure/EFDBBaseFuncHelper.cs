using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using NewtouchHIS.Lib.Base;
using SqlSugar;

namespace Newtouch.MR.ManageSystem.Infrastructure
{
    /// <summary>
    /// Database helper functions (ported from EFDBBaseFuncHelper).
    /// Unique-number generation runs the vendor stored procedure spFieldGetNewID
    /// via SqlSugar Ado with OUTPUT parameters (MRMS DB, or Newtouch_EMR for the EMR variant).
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        public static EFDBBaseFuncHelper Instance => new EFDBBaseFuncHelper();

        public static EFDBBaseFuncHelper GetInstance(EnumDBFactoryType? dbType = null)
        {
            return new EFDBBaseFuncHelper();
        }

        public string GetNewFieldUniqueValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var tpl = GetNewFieldUniqueObjectValue(fieldName, orgId, initFormat, dateFlag, emr: false);
            if (tpl.Item2 == null || string.IsNullOrWhiteSpace(tpl.Item2.ToString()))
            {
                return tpl.Item1.ToString();
            }
            return string.Format(tpl.Item2.ToString(), Convert.ToInt32(tpl.Item1));
        }

        public string GetNewFieldUniqueValueEMR(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var tpl = GetNewFieldUniqueObjectValue(fieldName, orgId, initFormat, dateFlag, emr: true);
            if (tpl.Item2 == null || string.IsNullOrWhiteSpace(tpl.Item2.ToString()))
            {
                return tpl.Item1.ToString();
            }
            return string.Format(tpl.Item2.ToString(), Convert.ToInt32(tpl.Item1));
        }

        private Tuple<object, object> GetNewFieldUniqueObjectValue(string fieldName, string orgId, string initFormat, bool dateFlag, bool emr)
        {
            var db = MrmsDb.GetConnection();
            var pars = new List<SugarParameter>
            {
                new SugarParameter("@OrganizeId", orgId),
                new SugarParameter("@TopOrganizeId", Constants.TopOrganizeId),
                new SugarParameter("@FieldName", fieldName),
                new SugarParameter("@DateFlag", dateFlag),
                new SugarParameter("@currDate", DateTime.Now.ToString("yyyyMMdd")),
                new SugarParameter("@InitFormat", initFormat),
            };
            var outNum = new SugarParameter("@NumStr", null, System.Data.DbType.String, ParameterDirection.Output, 50);
            var outFmt = new SugarParameter("@Format", null, System.Data.DbType.String, ParameterDirection.Output, 50);
            pars.Add(outNum);
            pars.Add(outFmt);
            string proc = emr
                ? "exec [Newtouch_EMR].[dbo].spFieldGetNewID @OrganizeId, @TopOrganizeId, @FieldName, @DateFlag, @currDate, @InitFormat, @NumStr out, @Format out"
                : "exec spFieldGetNewID @OrganizeId, @TopOrganizeId, @FieldName, @DateFlag, @currDate, @InitFormat, @NumStr out, @Format out";
            db.Ado.ExecuteCommand(proc, pars.ToArray());
            return Tuple.Create(outNum.Value, outFmt.Value);
        }

        public static Dictionary<string, string> GetEnumDescription<T>()
        {
            var dic = new Dictionary<string, string>();
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;
                    dic.Add(field.Name, description);
                }
            }
            return dic;
        }

        public static List<KeyValuePair<string, string>> GetEnumDescriptionList<T>()
        {
            var result = new List<KeyValuePair<string, string>>();
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;
                    result.Add(new KeyValuePair<string, string>(field.Name, description));
                }
            }
            return result;
        }
    }

    public enum EnumDBFactoryType
    {
        DefaultDB,
    }
}
