using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Infrastructure;

namespace Newtouch.MRQC.Infrastructure
{
    /// <summary>
    /// thường dùng đến（đặtRepo、DmnServiceđều không phù hợp，nhưng lại phải yêu cầu cơ sở dữ liệu）
    /// ví dụ như lấy số hiệu duy nhất（tự tăng，nhưng có trường hợp không được trùng lặp）
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        private EFDBBaseFuncHelper(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        private IDatabaseFactory _databaseFactory;

        /// <summary>
        /// mặc địnhDBngữ cảnh
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
        /// chỉ địnhDBngữ cảnh
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static EFDBBaseFuncHelper GetInstance(EnumDBFactoryType? dbType = null)
        {
            if (!dbType.HasValue)
            {
                dbType = EnumDBFactoryType.DefaultDB;  //mặc định
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

        #region phương thức

        //-------------------------------------ví dụ------------------------------------------------
        ///// <summary>
        ///// lấy duy nhất trường khóa chínhId
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

    }

    /// <summary>
    /// ngữ cảnh loại
    /// </summary>
    public enum EnumDBFactoryType
    {
        DefaultDB,

    }

}