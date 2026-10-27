using Newtouch.OR.ManageSystem.Infrastructure;
using SqlSugar;
using System;
using System.Data;

namespace Newtouch.OR.ManageSystem.Infrastructure
{
    /// <summary>
    /// Business-number helpers (stored proc spFieldGetNewID, verified present
    /// in the restored Newtouch_OR database). Replaces the EF6 version that
    /// resolved an IDatabaseFactory through the Framework service locator.
    /// SqlSugar's SugarParameter carries output values back after ExecuteCommand.
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        public static EFDBBaseFuncHelper Instance => new EFDBBaseFuncHelper();

        /// <summary>
        /// 指定DB上下文 (kept for signature compatibility; the OR connection
        /// is always used — cross-DB variants pass a fully-qualified proc name
        /// through GetNewFieldUniqueValueDB instead).
        /// </summary>
        public static EFDBBaseFuncHelper GetInstance(EnumDBFactoryType? dbType = null)
        {
            return new EFDBBaseFuncHelper();
        }

        public string GetNewFieldUniqueValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var tpl = GetNewFieldUniqueObjectValue(fieldName, orgId, initFormat, dateFlag);
            if (tpl.Item2 == null || string.IsNullOrWhiteSpace(tpl.Item2.ToString()))
            {
                return tpl.Item1.ToString();
            }
            else
            {
                return string.Format(tpl.Item2.ToString(), Convert.ToInt32(tpl.Item1));
            }
        }

        private Tuple<object, object> GetNewFieldUniqueObjectValue(string fieldName, string orgId, string initFormat, bool dateFlag)
        {
            return CallFieldProc("spFieldGetNewID", fieldName, orgId, initFormat, dateFlag);
        }

        public string GetNewFieldUniqueValueDB(string db, string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var tpl = GetNewFieldUniqueObjectValueDB(db, fieldName, orgId, initFormat, dateFlag);
            if (tpl.Item2 == null || string.IsNullOrWhiteSpace(tpl.Item2.ToString()))
            {
                return tpl.Item1.ToString();
            }
            else
            {
                return string.Format(tpl.Item2.ToString(), Convert.ToInt32(tpl.Item1));
            }
        }

        private Tuple<object, object> GetNewFieldUniqueObjectValueDB(string db, string fieldName, string orgId, string initFormat, bool dateFlag)
        {
            return CallFieldProc(db + " spFieldGetNewID", fieldName, orgId, initFormat, dateFlag);
        }

        private Tuple<object, object> CallFieldProc(string proc, string fieldName, string orgId, string initFormat, bool dateFlag)
        {
            var outNum = new SugarParameter("@NumStr", null)
            {
                DbType = System.Data.DbType.String,
                Size = 50,
                Direction = ParameterDirection.Output,
            };
            var outFmt = new SugarParameter("@Format", null)
            {
                DbType = System.Data.DbType.String,
                Size = 50,
                Direction = ParameterDirection.Output,
            };
            OrDb.GetConnection().Ado.ExecuteCommand(
                $"exec {proc} @OrganizeId, @TopOrganizeId, @FieldName, @DateFlag, @currDate, @InitFormat, @NumStr out, @Format out",
                new SugarParameter("@OrganizeId", orgId),
                new SugarParameter("@TopOrganizeId", Constants.TopOrganizeId),
                new SugarParameter("@FieldName", fieldName),
                new SugarParameter("@DateFlag", dateFlag),
                new SugarParameter("@currDate", DateTime.Now.ToString("yyyyMMdd")),
                new SugarParameter("@InitFormat", initFormat),
                outNum,
                outFmt);
            return Tuple.Create(outNum.Value, outFmt.Value);
        }

        #region 应用
        /// <summary>
        /// 获取手术申请号
        /// </summary>
        public static string GetOPApplyNo(string orgId)
        {
            var thisDate = DateTime.Now.ToString("yyyyMMdd");
            var No = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("OR_ApplyInfo.Applyno", orgId, "{0:D6}", true);
            return "S" + thisDate + "A" + No;
        }

        public static string GetOPSsxh(string orgId)
        {
            var thisDate = DateTime.Now.ToString("yyyyMMdd");
            var No = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("OR_Registration.ssxh", orgId, "{0:D6}", true);
            return "S" + thisDate + "A" + No;
        }
        #endregion
    }

    /// <summary>
    /// 上下文 类型 (kept for signature compatibility; only DefaultDB is used)
    /// </summary>
    public enum EnumDBFactoryType
    {
        DefaultDB,
    }
}
