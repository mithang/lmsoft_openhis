using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Lib.Base.Model
{
    public class DBConf
    {
        /// <summary>
        /// tên liên kết cơ sở dữ liệu
        /// </summary>
        public string ConnId { get; set; }
        /// <summary>
        /// tên cơ sở dữ liệu
        /// </summary>
        public string DBName { get; set; }
        public BusinessDBType DBType { get; set; }
        /// <summary>
        /// liên kết cơ sở dữ liệu
        /// </summary>
        public string DBConn { get; set; }
        /// <summary>
        /// có bật hay khôngDB
        /// </summary>
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// bậtsqlxuất ra
        /// </summary>
        public bool EnabledSqlTrace { get; set; } = false;
    }

    public class BusinessDB
    {
        /// <summary>
        /// liên kết mặc định
        /// </summary>
        public string? DefaultConn { get; set; }
        /// <summary>
        /// tên liên kết cơ sở dữ liệu chính
        /// </summary>
        public string MainDB { get; set; }
        /// <summary>
        /// có bật quản lý đa thư viện hay không
        /// </summary>
        public bool EnabledMutiDB { get; set; } = false;
        public List<DBConf> DBConf { get; set; }
    }
}
