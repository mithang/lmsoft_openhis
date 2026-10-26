using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Thường dùng (đặt ở Repo hay DmnService đều không phù hợp, nhưng lại cần yêu cầu cơ sở dữ liệu)
    /// Ví dụ lấy mã duy nhất (tự tăng, nhưng không được trùng lặp)
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        private EFDBBaseFuncHelper(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        private IDatabaseFactory _databaseFactory;

        /// <summary>
        /// Ngữ cảnh DB mặc định
        /// </summary>
        public static EFDBBaseFuncHelper Instance
        {
            get
            {
                IDatabaseFactory databaseFactory
                    = DependencyDefaultInstanceResolver.GetInstance<IDefaultDatabaseFactory>();
                return new EFDBBaseFuncHelper(databaseFactory);
            }
        }

        /// <summary>
        /// Ngữ cảnh DB đã chỉ định
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static EFDBBaseFuncHelper GetInstance(EnumDBFactoryType? dbType = null)
        {
            if (!dbType.HasValue)
            {
                dbType = EnumDBFactoryType.DefaultDB;  //Mặc định
            }
            IDatabaseFactory databaseFactory = null;
            switch (dbType)
            {
                case EnumDBFactoryType.DefaultDB:
                    databaseFactory = DependencyDefaultInstanceResolver.GetInstance<IDefaultDatabaseFactory>();
                    break;
            }
            return new EFDBBaseFuncHelper(databaseFactory);
        }

        #region Phương pháp

        //-------------------------------------Ví dụ------------------------------------------------
        ///// <summary>
        ///// Lấy Id duy nhất của trường khóa chính
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <returns></returns>
        //public string GetNewPrimaryKeyString(string tableName)
        //{
        //    var sqlParList = new List<SqlParameter>();
        //    sqlParList.Add(new SqlParameter("@tableName", tableName));

        //    var outParameter = new SqlParameter("@NumStr", System.Data.SqlDbType.VarChar, 8);
        //    outParameter.Direction = System.Data.ParameterDirection.Output;
        //    sqlParList.Add(outParameter);

        //    _databaseFactory.Get().Database.ExecuteSqlCommand("exec spGetNewID @tableName, @NumStr out", sqlParList.ToArray());

        //    return outParameter.Value.ToString();
        //}

        #endregion

        /// <summary>
        /// Lấy giá trị duy nhất của trường/nghiệp vụ
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgId"></param>
        /// <param name="initFormat"></param>
        /// <param name="dateFlag"></param>
        /// <returns>Kiểu string</returns>
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
        /// Lấy số chứng từ
        /// </summary>
        /// <param name="djmc"></param>
        /// <param name="orgId">Id tổ chức tương ứng với bệnh viện hiện tại</param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public string GetNewMedicineReceiptNo(string djmc, string yfbmCode, string orgId)
        {
            var outpar = new SqlParameter("@result", System.Data.SqlDbType.VarChar, 30)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var sqlParList = new List<SqlParameter>
            {
                new SqlParameter("@djmc", djmc),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@yfbmCode", yfbmCode),
                outpar
            };
            _databaseFactory.Get().Database.ExecuteSqlCommand("exec sp_ksby_Getypdjh @djmc, @yfbmCode, @orgId, @result out", sqlParList.ToArray());
            if (outpar.Value != null && !string.IsNullOrWhiteSpace(outpar.Value.ToString()))
            {
                return outpar.Value.ToString();
            }
            return null;
        }
        /// <summary>
        /// Lấy giá trị duy nhất của trường/nghiệp vụ
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgId"></param>
        /// <param name="initFormat"></param>
        /// <param name="dateFlag"></param>
        /// <returns>Kiểu string</returns>
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
        /// Lấy số đơn thuốc
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
        /// Lấy giá trị duy nhất của trường/nghiệp vụ, trả về Tuple
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgId"></param>
        /// <param name="initFormat"></param>
        /// <param name="dateFlag"></param>
        /// <returns></returns>
        private Tuple<object, object> GetNewFieldUniqueObjectValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var sqlParList = new List<SqlParameter>();

            sqlParList.Add(new SqlParameter("@OrganizeId", orgId));
            sqlParList.Add(new SqlParameter("@TopOrganizeId", Constants.TopOrganizeId));
            sqlParList.Add(new SqlParameter("@FieldName", fieldName));
            sqlParList.Add(new SqlParameter("@DateFlag", dateFlag));
            sqlParList.Add(new SqlParameter("@currDate", DateTime.Now.ToString("yyyyMMdd")));
            sqlParList.Add(new SqlParameter("@InitFormat", initFormat));
            var outParameter1 = new SqlParameter("@NumStr", System.Data.SqlDbType.VarChar, 50);
            outParameter1.Direction = System.Data.ParameterDirection.Output;
            sqlParList.Add(outParameter1);
            var outParameter2 = new SqlParameter("@Format", System.Data.SqlDbType.VarChar, 50);
            outParameter2.Direction = System.Data.ParameterDirection.Output;
            sqlParList.Add(outParameter2);
            _databaseFactory.Get().Database.ExecuteSqlCommand("exec spFieldGetNewID  @OrganizeId, @TopOrganizeId, @FieldName, @DateFlag, @currDate, @InitFormat, @NumStr out, @Format out", sqlParList.ToArray());

            var val1 = outParameter1.Value;
            var val2 = outParameter2.Value;
            return Tuple.Create(val1, val2);
        }
        #endregion

    }

    /// <summary>
    /// Loại ngữ cảnh
    /// </summary>
    public enum EnumDBFactoryType
    {
        DefaultDB,

    }


}
