using System;
using Newtouch.CIS.Infrastructure;
using SqlSugar;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Ported from Framework CIS EFDBBaseFuncHelper. The EF
    /// IDatabaseFactory/DependencyDefaultInstanceResolver plumbing is replaced
    /// by SqlSugar against the CIS database (CisDb); stored procedures
    /// spFieldGetNewID / sp_ksby_Getypdjh are invoked with output parameters
    /// exactly as before. Requires those procedures to exist in Newtouch_CIS;
    /// if they are absent the call fails with a clear SQL error.
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        private EFDBBaseFuncHelper()
        {
        }

        /// <summary>
        /// Mặc địnhDBtrêndướivăn
        /// </summary>
        public static EFDBBaseFuncHelper Instance
        {
            get
            {
                return new EFDBBaseFuncHelper();
            }
        }

        /// <summary>
        /// chỉđịnhDBtrêndướivăn (dbType is accepted for source compatibility; the port
        /// always uses the CIS database)
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static EFDBBaseFuncHelper GetInstance(EnumDBFactoryType? dbType = null)
        {
            return new EFDBBaseFuncHelper();
        }

        /// <summary>
        /// Lấy chữđoạn/nghiệp vụ duy nhấtmộtGiá trị
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgId"></param>
        /// <param name="initFormat"></param>
        /// <param name="dateFlag"></param>
        /// <returns>stringLoại</returns>
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

        /// <summary>
        /// Lấyphiếusố phiếu
        /// </summary>
        /// <param name="djmc"></param>
        /// <param name="orgId">hiện tạiyviệntương ứngcủaTổ chứcId</param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public string GetNewMedicineReceiptNo(string djmc, string yfbmCode, string orgId)
        {
            var outpar = new SugarParameter("@result", string.Empty, System.Data.DbType.String, System.Data.ParameterDirection.Output, 30);
            CisDb.GetConnection().Ado.ExecuteCommand(
                "exec sp_ksby_Getypdjh @djmc, @yfbmCode, @orgId, @result out",
                new SugarParameter("@djmc", djmc),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@orgId", orgId),
                outpar);
            if (outpar.Value != null && !string.IsNullOrWhiteSpace(outpar.Value.ToString()))
            {
                return outpar.Value.ToString();
            }
            return null;
        }

        /// <summary>
        /// Lấy chữđoạn/nghiệp vụ duy nhấtmộtGiá trị
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgId"></param>
        /// <param name="initFormat"></param>
        /// <param name="dateFlag"></param>
        /// <returns>stringLoại</returns>
        public int GetNewFieldUniqueIntValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var tpl = GetNewFieldUniqueObjectValue(fieldName, orgId, initFormat, dateFlag);

            var val = -1;

            if (tpl.Item1 != null)
            {
                int.TryParse(tpl.Item1.ToString(), out val);
            }

            return val;
        }

        /// <summary>
        /// LấySố đơn thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetRequisitionNo(string orgId)
        {
            var thisDate = DateTime.Now.ToString("yyyyMMdd");
            var No = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_cf.cfh", orgId, "{0:D6}", true);
            return "R" + thisDate + "N" + No;
        }

        #region private methods

        /// <summary>
        /// Lấy chữđoạn/nghiệp vụ duy nhấtmộtGiá trị Quay lạiTuple
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgId"></param>
        /// <param name="initFormat"></param>
        /// <param name="dateFlag"></param>
        /// <returns></returns>
        private Tuple<object, object> GetNewFieldUniqueObjectValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var outParameter1 = new SugarParameter("@NumStr", string.Empty, System.Data.DbType.String, System.Data.ParameterDirection.Output, 50);
            var outParameter2 = new SugarParameter("@Format", string.Empty, System.Data.DbType.String, System.Data.ParameterDirection.Output, 50);
            CisDb.GetConnection().Ado.ExecuteCommand(
                "exec spFieldGetNewID @OrganizeId, @TopOrganizeId, @FieldName, @DateFlag, @currDate, @InitFormat, @NumStr out, @Format out",
                new SugarParameter("@OrganizeId", orgId),
                new SugarParameter("@TopOrganizeId", Constants.TopOrganizeId),
                new SugarParameter("@FieldName", fieldName),
                new SugarParameter("@DateFlag", dateFlag),
                new SugarParameter("@currDate", DateTime.Now.ToString("yyyyMMdd")),
                new SugarParameter("@InitFormat", initFormat),
                outParameter1,
                outParameter2);

            var val1 = outParameter1.Value;
            var val2 = outParameter2.Value;
            return Tuple.Create(val1, val2);
        }
        #endregion
    }

    /// <summary>
    /// trêndướivăn Loại
    /// </summary>
    public enum EnumDBFactoryType
    {
        DefaultDB,
    }
}
