using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using System;
using System.Collections.Generic;
using SqlSugar;
using System.Text.RegularExpressions;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Thường dùng(đặtRepo,DmnServiceđều không phù hợp,nhưng lại phải yêu cầu cơ sở dữ liệu)
    /// Ví dụ lấy số hiệu duy nhất(Tự tăng,nhưng không thể trùng lặp)
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        private EFDBBaseFuncHelper(IDefaultDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private readonly IDefaultDatabaseFactory _databaseFactory;

        /// <summary>
        /// Mặc địnhDBNgữ cảnh
        /// </summary>
        public static EFDBBaseFuncHelper Instance
        {
            get
            {
                IDefaultDatabaseFactory databaseFactory = DependencyDefaultInstanceResolver.GetInstance<IDefaultDatabaseFactory>();
                return new EFDBBaseFuncHelper(databaseFactory);
            }
        }

        /// <summary>
        /// Chỉ địnhDBNgữ cảnh
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static EFDBBaseFuncHelper GetInstance(EnumDBFactoryType? dbType = null)
        {
            if (!dbType.HasValue)
            {
                dbType = EnumDBFactoryType.DefaultDB;  //Mặc định
            }
            IDefaultDatabaseFactory databaseFactory = null;
            switch (dbType)
            {
                case EnumDBFactoryType.DefaultDB:
                    databaseFactory = DependencyDefaultInstanceResolver.GetInstance<IDefaultDatabaseFactory>();
                    break;
            }
            return new EFDBBaseFuncHelper(databaseFactory);
        }

        #region DBHelper

        /// <summary>
        /// Lấy duy nhất trường khóa chínhId
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int GetNewPrimaryKeyInt(string tableName)
        {
            var keyVal = GetNewPrimaryKeyString(tableName);
            var newVal = new Regex("^0*").Replace(keyVal, "");
            if (!string.IsNullOrWhiteSpace(newVal))
            {
                return Convert.ToInt32(newVal);
            }
            throw new Exception("Lấy số thứ tự thất bại！");
        }

        /// <summary>
        /// Lấy duy nhất trường khóa chínhId
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public long GetNewPrimaryKeyLong(string tableName)
        {
            var keyVal = GetNewPrimaryKeyString(tableName);
            var newVal = new Regex("^0*").Replace(keyVal, "");
            if (!string.IsNullOrWhiteSpace(newVal))
            {
                return Convert.ToInt64(newVal);
            }
            throw new Exception("Lấy số thứ tự thất bại！");
        }

        /// <summary>
        /// Lấy duy nhất trường khóa chínhId
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetNewPrimaryKeyString(string tableName)
        {
            var sqlParList = new List<SugarParameter> { new SugarParameter("@tableName", tableName) };
            var outParameter = new SugarParameter("@NumStr", "") { DbType = System.Data.DbType.AnsiString, Size = 8, Direction = System.Data.ParameterDirection.Output };
            sqlParList.Add(outParameter);
            PdsDb.GetConnection().Ado.ExecuteCommand("exec spGetNewID @tableName, @NumStr out", sqlParList.ToArray());
            return outParameter.Value.ToString();
        }
        /// <summary>
        /// Lấy Trường/Nghiệp vụ Giá trị duy nhất
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string GetNewFieldUniqueValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var sqlParList = new List<SugarParameter>
            {
                new SugarParameter("@OrganizeId", orgId),
                new SugarParameter("@TopOrganizeId", Constants.TopOrganizeId),
                new SugarParameter("@FieldName", fieldName),
                new SugarParameter("@DateFlag", dateFlag),
                new SugarParameter("@currDate", DateTime.Now.ToString("yyyyMMdd")),
                new SugarParameter("@InitFormat", initFormat)
            };

            var outParameter1 = new SugarParameter("@NumStr", "") { DbType = System.Data.DbType.AnsiString, Size = 50, Direction = System.Data.ParameterDirection.Output };
            sqlParList.Add(outParameter1);
            var outParameter2 = new SugarParameter("@Format", "") { DbType = System.Data.DbType.AnsiString, Size = 50, Direction = System.Data.ParameterDirection.Output };
            sqlParList.Add(outParameter2);
            PdsDb.GetConnection().Ado.ExecuteCommand("exec spFieldGetNewID  @OrganizeId, @TopOrganizeId, @FieldName, @DateFlag, @currDate, @InitFormat, @NumStr out, @Format out", sqlParList.ToArray());

            if (outParameter2.Value == null || string.IsNullOrWhiteSpace(outParameter2.Value.ToString()))
            {
                return outParameter1.Value.ToString();
            }
            return string.Format(outParameter2.Value.ToString(), Convert.ToInt32(outParameter1.Value));
        }

        /// <summary>
        /// Lấy số chứng từ
        /// </summary>
        /// <param name="djmc"></param>
        /// <param name="orgId">Tổ chức tương ứng bệnh viện hiện tạiId</param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public string GetNewMedicineReceiptNo(string djmc, string yfbmCode, string orgId)
        {
            var outpar = new SugarParameter("@result", "") { DbType = System.Data.DbType.AnsiString, Size = 30, Direction = System.Data.ParameterDirection.Output };
            var sqlParList = new List<SugarParameter>
            {
                new SugarParameter("@djmc", djmc),
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@yfbmCode", yfbmCode),
                outpar
            };
            PdsDb.GetConnection().Ado.ExecuteCommand("exec sp_yp_Getypdjh @djmc, @yfbmCode, @orgId, @result out", sqlParList.ToArray());
            if (outpar.Value != null && !string.IsNullOrWhiteSpace(outpar.Value.ToString()))
            {
                return outpar.Value.ToString();
            }
            return null;
        }

        #endregion

    }

    /// <summary>
    /// Ngữ cảnh Loại
    /// </summary>
    public enum EnumDBFactoryType
    {
        /// <summary>
        /// Kho chính hệ thống
        /// </summary>
        DefaultDB
    }

}
