using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 常 dùng đ ến của(放Repo, DmnService đ ều không 合适của,nhưng又 cần yêu cầu dữ liệu kho của)
    /// ví dụ lấy duy nhất số hiệu(tự 增,nhưng có không thể trùng lặp)
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        private EFDBBaseFuncHelper(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        private IDatabaseFactory _databaseFactory;

        /// <summary>
        /// Mặc đ ịnhDBngữ cảnh
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
        /// chỉ đ ịnh DBngữ cảnh
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static EFDBBaseFuncHelper GetInstance(EnumDBFactoryType? dbType = null)
        {
            if (!dbType.HasValue)
            {
                dbType = EnumDBFactoryType.DefaultDB;  //Mặc đ ịnh
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

        #region 方法

        /// <summary>
        /// Lấy khóa chính trư ờng duy nhất id
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
            else
            {
                throw new Exception("获取流水号失败！");
            }
        }

        /// <summary>
        /// Lấy khóa chính trư ờng duy nhất id
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
            else
            {
                throw new Exception("获取流水号失败！");
            }
        }

        /// <summary>
        /// Lấy khóa chính trư ờng duy nhất id
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetNewPrimaryKeyString(string tableName)
        {
            var sqlParList = new List<SqlParameter>();
            sqlParList.Add(new SqlParameter("@tableName", tableName));

            var outParameter = new SqlParameter("@NumStr", System.Data.SqlDbType.VarChar, 8);
            outParameter.Direction = System.Data.ParameterDirection.Output;
            sqlParList.Add(outParameter);

            _databaseFactory.Get().Database.ExecuteSqlCommand("exec spGetNewID @tableName, @NumStr out", sqlParList.ToArray());

            return outParameter.Value.ToString();
        }

        /// <summary>
        /// Lấy trư ờng / nghiệp vụ duy nhất giá trị 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string GetNewFieldUniqueValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
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

            if (outParameter2.Value == null || string.IsNullOrWhiteSpace(outParameter2.Value.ToString()))
            {
                return outParameter1.Value.ToString();
            }
            else
            {
                return string.Format(outParameter2.Value.ToString(), Convert.ToInt32(outParameter1.Value));
            }
        }

        #endregion


    }

    /// <summary>
    /// Ngữ cảnh loại 
    /// </summary>
    public enum EnumDBFactoryType
    {
        DefaultDB,

    }

}
