using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// DmnService base
    /// </summary>
    public class DmnServiceBase : EFDBBase
    {
        /// <summary>
        /// Có phải là hay khôngprivate readonly I Phân tích thể hiện
        /// </summary>
        private static readonly bool? PrfAutoResolve;

        /// <summary>
        /// Hàm tạo tĩnh
        /// </summary>
        static DmnServiceBase()
        {
            PrfAutoResolve = ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve_DmnService") ?? ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve");
        }

        /// <summary>
        /// Hàm tạo (tại đây choprf iocĐã gán giá trị）
        /// </summary>
        /// <param name="databaseFactory"></param>
        public DmnServiceBase(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            if (PrfAutoResolve ?? true)
            {
                //Tận dụngIOCLàprivate readonly fieldGán giá trị
                this.PRFAutoResolve();
            }
        }

        /// <summary>
        /// Hàm tạo (tại đây choprf iocĐã gán giá trị）
        /// </summary>
        /// <param name="databaseFactory"></param>
        /// <param name="needIoc"></param>
        public DmnServiceBase(IDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory)
        {
            if (needIoc)
            {
                //Tận dụngIOCLàprivate readonly fieldGán giá trị
                this.PRFAutoResolve();
            }
        }

        #region no log

        /// <summary>
        /// Tra cứuSql，Trả về thực thể
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T FirstOrDefaultNoLog<T>(string sql)
        {
            return _dataContext.Database.SqlQuery<T>(sql).FirstOrDefault();
        }

        /// <summary>
        /// Tra cứuSql，Trả về thực thể
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T FirstOrDefaultNoLog<T>(string sql, DbParameter[] parameters)
        {
            if (parameters == null)
            {
                return FirstOrDefaultNoLog<T>(sql);
            }
            return _dataContext.Database.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }

        /// <summary>
        /// Thực thi trên cơ sở dữ liệu giá trị đã cho DDL/DML Lệnh
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSqlCommandNoLog(string sql, params DbParameter[] parameters)
        {
            int result;
            result = parameters == null ? _dataContext.Database.ExecuteSqlCommand(sql) : _dataContext.Database.ExecuteSqlCommand(sql, parameters);
            return result;
        }

        /// <summary>
        /// Tra cứuSql，Trả về danh sách thực thể
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> FindListNoLog<T>(string sql)
        {
            var result = _dataContext.Database.SqlQuery<T>(sql).ToList<T>();
            return result;
        }

        /// <summary>
        /// Tra cứuSql，Trả về danh sách thực thể
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> FindListNoLog<T>(string sql, DbParameter[] parameters)
        {
            if (parameters == null)
            {
                return FindListNoLog<T>(sql);
            }
            return _dataContext.Database.SqlQuery<T>(sql, parameters).ToList();
        }
        #endregion
    }
}