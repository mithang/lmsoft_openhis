using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System.Configuration;

namespace NewtouchHIS.Lib.DataBaseSvr
{
    /// <summary>
    /// thông tin ngữ cảnh cơ sở dữ liệu
    /// </summary>
    public class DbContextV2
    {
        /// <summary>
        /// kiểu cơ sở dữ liệu。
        /// </summary>
        public DbType DbType { get; set; }
        /// <summary>
        /// chuỗi kết nối。
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// kiểu cơ sở dữ liệu。
        /// </summary>
        public SqlSugarScope Client { get; set; }
        public SqlSugarClient scopeClient { get; set; }

        public DbContextV2()
        {
            //mặc định dùng tên mục cấu hình
            //appSettings/DefaultDb mục cấu hình là chuỗi kết nối chỉ địnhname
            var dbConfigName = ConfigurationManager.AppSettings["Default"];
            Init(dbConfigName);
        }

        public DbContextV2(string dbConfigName)
        {
            Init(dbConfigName);
        }
        public void Init(string dbConfigName)
        {
            var DbConfig = ConfigInitHelper.DbConfig.DBConf.Where(p=>p.ConnId== ConfigInitHelper.DbConfig.MainDB).FirstOrDefault();
            Client = new SqlSugarScope(new ConnectionConfig()
            {
                DbType = (DbType)DbConfig!.DBType,
                ConnectionString = ConnectionString,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
            });
        }

        public void InitClient(string dbConfigName)
        {
            var DbConfig = ConfigInitHelper.DbConfig;
            var configConnection = new List<ConnectionConfig>();
            DbConfig.DBConf.ForEach(option =>
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
                        InitKeyType = InitKeyType.Attribute,
                        DbType = (DbType)option.DBType,
                        IsAutoCloseConnection = true,
                        MoreSettings = new ConnMoreSettings()
                        {
                            IsAutoRemoveDataCache = true,
                            IsWithNoLockQuery = true,
                            DisableNvarchar = true,
                        },
                        AopEvents=new AopEvents()
                        {
                            OnLogExecuting = (sql, p) =>
                            {

                            }
                        }
                        //InitKeyType = InitKeyType.SystemTable
                    });
                }
                scopeClient = new SqlSugarClient(configConnection);
            });
        }

        /// <summary>
        /// theo chuỗi liên kếtproviderNamequyết định kiểu cơ sở dữ liệu nào
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        private DbType GetSugarDbType(ConnectionStringSettings setting)
        {
            DbType dbType = DbType.SqlServer; //giá trị mặc định
            var providerName = setting.ProviderName;
            if (providerName != null)
            {
                //cơ sở dữ liệuproviderName：SqlClient MySqlClient SQLite OracleManaged/OracleClient Npgsql
                if (providerName.EndsWith(".SqlClient", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.SqlServer;
                }
                else if (providerName.EndsWith(".MySqlClient", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.MySql;
                }
                else if (providerName.EndsWith(".SQLite", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Sqlite;
                }
                else if (providerName.EndsWith("OracleManaged", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Oracle;
                }
                else if (providerName.EndsWith(".OracleClient", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Oracle;
                }
                else if (providerName.EndsWith("Npgsql", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.PostgreSQL;
                }
                else if (providerName.EndsWith("Dm", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Dm;
                }
            }
            return dbType;
        }
    }
}
