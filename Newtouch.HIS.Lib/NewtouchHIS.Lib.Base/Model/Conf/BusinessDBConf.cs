using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Lib.Base.Model
{
    public class DBConf
    {
        /// <summary>
        /// Tên liên kết cơ sở dữ liệu
        /// </summary>
        public string ConnId { get; set; }
        /// <summary>
        /// Tên cơ sở dữ liệu
        /// </summary>
        public string DBName { get; set; }
        public BusinessDBType DBType { get; set; }
        /// <summary>
        /// Liên kết cơ sở dữ liệu
        /// </summary>
        public string DBConn { get; set; }
        /// <summary>
        /// Có bật khôngDB
        /// </summary>
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// Bật đầu ra sql
        /// </summary>
        public bool EnabledSqlTrace { get; set; } = false;
    }

    public class BusinessDB
    {
        /// <summary>
        /// Liên kết mặc định
        /// </summary>
        public string? DefaultConn { get; set; }
        /// <summary>
        /// Tên liên kết cơ sở dữ liệu chính
        /// </summary>
        public string MainDB { get; set; }
        /// <summary>
        /// Có bật quản lý nhiều cơ sở dữ liệu không
        /// </summary>
        public bool EnabledMutiDB { get; set; } = false;
        public List<DBConf> DBConf { get; set; }
    }
}
