using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Extension;
using SqlSugar;

namespace NewtouchHIS.Lib.DataBaseSvr
{
    public class DbContext<T> where T : class, new()
    {
        public DbContext()
        {
            var DbConfig = ConfigInitHelper.DbConfig;
            var configConnection = new List<ConnectionConfig>();
            DbConfig.DBConf.Where(p => p.Enabled).ToList()!.ForEach(option =>
            {
                if (string.IsNullOrWhiteSpace(option.DBConn) && !string.IsNullOrWhiteSpace(DbConfig.DefaultConn) && !string.IsNullOrWhiteSpace(option.DBName))
                {
                    option.DBConn = string.Format(DbConfig.DefaultConn, option.DBName);
                }
                if (!string.IsNullOrWhiteSpace(option.DBConn))
                {
                    configConnection.Add(new ConnectionConfig()
                    {
                        ConfigId = option.ConnId/*.ToLower()*/,
                        ConnectionString = option.DBConn,
                        DbType = (DbType)option.DBType,
                        IsAutoCloseConnection = true,
                        MoreSettings = new ConnMoreSettings()
                        {
                            IsAutoRemoveDataCache = true,
                            IsWithNoLockQuery = true,
                            DisableNvarchar = true,
                        }
                        //InitKeyType = InitKeyType.SystemTable
                    });
                }

            });
            Db = new SqlSugarClient(configConnection, db =>
            {
                db.GetConnectionScope(DBEnum.MrmsDb).Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine($"{DBEnum.MrmsDb.GetDescription()}======> {sql}");
                    Console.WriteLine();
                };

            });
            //Db = new SqlSugarClient(new ConnectionConfig()
            //{
            //    ConnectionString = DbConfig.DBConf,
            //    DbType = DbType.SqlServer,
            //    InitKeyType = InitKeyType.Attribute,//Đọc thông tin khóa chính và cột tự tăng từ đặc tính
            //    IsAutoCloseConnection = true, 

            //});
            //Mã gỡ lỗi dùng để inSQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }
        //Lưu ý: không thể viết thành tĩnh
        public SqlSugarClient Db;//Dùng để xử lý truy vấn nhiều bảng giao dịch và các thao tác phức tạp
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//Dùng để thao tác dữ liệu của bảng hiện tại

        //public SqlSugarScopeProvider dbProvider(string ConnId) => Db.GetConnectionScope(ConnId);
        //public SqlSugarScopeProvider dbProviderAttr => Db.GetConnectionScopeWithAttr<T>();
    }
}
