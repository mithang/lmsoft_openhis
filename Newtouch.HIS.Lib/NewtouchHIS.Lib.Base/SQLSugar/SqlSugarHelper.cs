using SqlSugar;

namespace NewtouchHIS.Lib.Base.SQLSugar
{
    public class SqlSugarHelper
    {
        /// <summary>
        /// Khởi tạoSqlSugarClient
        /// </summary>
        /// <returns>Trả về đối tượng SqlSugarClient</returns>
        public static SqlSugarClient GetInstance(DbType dbType, string dbName)
        {
            var strConnectionString = string.Empty;
            switch (dbType)
            {
                case DbType.SqlServer:
                    strConnectionString = GetSqlDBconnection(dbName);
                    //strConnectionString = @"Data Source=61.172.179.30,18040;Initial Catalog=NewtouchHIS_Base;User ID=sa;Password=1qazxsw2#;";
                    //strConnectionString = @"server = IP; database =ERP_PROD; uid = dbadmin; pwd = cltest22; Connect Timeout = 1200;";
                    break;
                case DbType.Oracle:
                    strConnectionString = @"Data Source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =IP)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME =LABELING)));User ID=label;Password=label20221123;";
                    //strConnectionString = "Data Source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME =ORCL)));User ID=TEST;Password=TEST;";
                    break;
                case DbType.MySql:
                    strConnectionString = @"Server=DESKTOP-9GRFFRR;database=Test_Demo;Trusted_Connection=True;MultipleActiveResultSets=True;";
                    break;
            }
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = strConnectionString,
                DbType = dbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,

            });
            return db;
        }

        private static string GetSqlDBconnection(string dbName)
        {
            string strConnectionString = @"Data Source=61.172.179.73,41108;Initial Catalog={0};User ID=sa;Password=a1+b2*c3=d4;";
            switch (dbName)
            {
                case "sett":
                    return string.Format(strConnectionString, "NewtouchHIS_Sett");
                case "base":
                    return string.Format(strConnectionString, "NewtouchHIS_Base");
                case "mrms":
                    return string.Format(strConnectionString, "Newtouch_MRMS");
                case "cis":
                    return string.Format(strConnectionString, "Newtouch_CIS");
            }
            return strConnectionString;
        }


        /// <summary>
        /// Tạo thực thể theo bảng
        /// </summary>
        /// <param name="path"></param>
        public static void DbFirst(DbType dbType, string path, Dictionary<string, string> tabledic, string dbName)
        {
            List<string> basename = tabledic.Keys.ToList();
            List<string> tablename = tabledic.Values.ToList();
            //db.DbFirst.IsCreateAttribute().CreateClassFile(path);
            var db = GetInstance(dbType, dbName);
            //ALLTất cả các bảng  
            //List<DbTableInfo> list = db.DbMaintenance.GetTableInfoList().Where(x => x.Name.Contains("mr")).ToList();
            List<DbTableInfo> list = db.DbMaintenance.GetTableInfoList().Where(x => tablename.Contains("ALL") ? x.Name.IsNormalized() : tablename.Contains(x.Name.Trim())).ToList();
            //Tất cả các khung nhìn
            List<DbTableInfo> viewList = db.DbMaintenance.GetViewInfoList().Where(p => tablename.Contains(p.Name.Trim())).ToList();

            var allList = list.Concat(viewList);

            //Lặp qua tất cả các bảng và khung nhìn, chúng thuộc cùng một lớp DbTableInfo
            foreach (DbTableInfo table in allList)
            {
                //Chuyển chữ cái đầu thành chữ hoa 
                string table_name = table.Name.Substring(0, 1).ToUpper() + table.Name.Substring(1).ToLower().Replace("_", "");
                //Bảng ánh xạ thêm tên thực thể và tên bảng
                db.MappingTables.Add(table_name, table.Name);
                //Lấy tất cả các trường của bảng theo tên bảng
                List<DbColumnInfo> dd = db.DbMaintenance.GetColumnInfosByTableName(table.Name);
                var @base = tabledic.FirstOrDefault(q => tablename.Contains("ALL") ? q.Value == "ALL" : q.Value == table.Name).Key;
                foreach (DbColumnInfo item in dd)
                {
                    string columnname = item.DbColumnName.ToLower();
                    //Thêm trường ánh xạ (tên trường, tên trường, tên bảng)
                    db.MappingColumns.Add(columnname, columnname, table_name);
                }
                db.DbFirst.
                SettingClassTemplate(old =>
                {
                    string snp = "\r\n    ";
                    var sugartable = GetClassTemplate().Replace("{SugarTable}", @$"{snp}[Tenant(""{dbName}"")]{snp}[SugarTable(""{table.Name}"",""{@base}"")]");
                    return sugartable;
                })
                .SettingNamespaceTemplate(old =>
                {
                    return old;
                })
                .SettingPropertyDescriptionTemplate(old =>
                {
                    //Mẫu tùy chỉnh
                    return old;//GetPropertyDescriptionTemplate();
                })
                .SettingPropertyTemplate((columns, temp, type) =>
                {
                    string comp = "\r\n           ";
                    var columnattribute = $"{comp}[SugarColumn({{0}})]";
                    List<string> attributes = new List<string>();
                    if (columns.IsPrimarykey)
                        attributes.Add("IsPrimaryKey=true");
                    if (columns.IsIdentity)
                        attributes.Add("IsIdentity=true");
                    if (attributes.Count == 0)
                        columnattribute = string.Empty;
                    List<string> customattributes = new List<string>();
                    if (!columns.IsNullable)
                        customattributes.Add($@"{comp}[Required(ErrorMessage =""{string.Format("{0} không được để trống", columns.DbColumnName)}"" )]");
                    if (!columns.DataType.ToLower().Equals("int"))
                        customattributes.Add($@"{comp}[StringLength({columns.Length}, ErrorMessage =""{string.Format("{0} giới hạn độ dài là {1}", columns.DbColumnName, columns.Length)}"" )]");

                    var t = temp.Replace("{PropertyType}", type)
                            .Replace("{PropertyName}", columns.DbColumnName)
                            .Replace("{SugarColumn}", attributes.Count > 0 ? string.Format(columnattribute, string.Join(",", attributes)) : string.Join("", customattributes));
                    return t;
                })
                .SettingConstructorTemplate(old =>
                {
                    return old;
                }).IsCreateAttribute().Where(table.Name).CreateClassFile(path);
            }
        }

        /// <summary>
        /// Mẫu của không gian tên
        /// </summary>
        /// <returns></returns>
        public static string GetClassTemplate()
        {
            return @"using System.ComponentModel.DataAnnotations;
{using}
namespace {Namespace}
{
{ClassDescription}{SugarTable}
    public partial class {ClassName}
    {
           public {ClassName}() { }

{Constructor}{PropertyName}
    }
}
";
        }

        /// <summary>
        /// Mẫu của trường
        /// </summary>
        /// <returns></returns>
        public static string GetPropertyDescriptionTemplate()
        {
            return @"
   /// <summary>
   /// Remark:{PropertyDescription}
   /// Default:{DefaultValue}
   /// Nullable:{IsNullable}
   /// </summary>";
        }


        #region Phương thức chung
        /// <summary>
        /// Ghép thành Sql đầy đủ để tiện xem
        /// </summary>
        public class ToSqlExplain
        {
            public static string GetSql(KeyValuePair<string, SugarParameter[]> queryString)
            {
                var sql = queryString.Key;//sqlcâu lệnh
                var par = queryString.Value;//tham số

                //Thay thế chuỗi MethodConst1x sẽ thay thế tất cả MethodConst1, phải thay từ sau ra trước,Không thể dùngforeach,Sau này có thể tối ưu
                for (int i = par.Length - 1; i >= 0; i--)
                {
                    if (par[i].ParameterName.StartsWith("@") && par[i].ParameterName.Contains("UnionAll"))
                    {
                        sql = sql.Replace(par[i].ParameterName, par[i].Value.ToString());
                    }
                }

                for (int i = par.Length - 1; i >= 0; i--)
                {
                    if (par[i].ParameterName.StartsWith("@Method"))
                    {
                        sql = sql.Replace(par[i].ParameterName, "'" + par[i].Value.ToString() + "'");
                    }
                }
                for (int i = par.Length - 1; i >= 0; i--)
                {
                    if (par[i].ParameterName.StartsWith("@Const"))
                    {
                        sql = sql.Replace(par[i].ParameterName, par[i].Value.ToString());
                    }
                }
                for (int i = par.Length - 1; i >= 0; i--)
                {
                    if (par[i].ParameterName.StartsWith("@"))
                    {
                        //Giá trị ghép dấu nháy đơn, sql lấy ra sẽ không báo lỗi
                        sql = sql.Replace(par[i].ParameterName, "'" + Convert.ToString(par[i].Value) + "'");
                    }
                }
                return sql;
            }
        }

        #endregion
    }
}
