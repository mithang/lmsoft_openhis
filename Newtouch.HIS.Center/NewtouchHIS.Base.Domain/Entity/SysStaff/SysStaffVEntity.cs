using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// Nhân viên hệ thống
    /// </summary>
    [Table("V_S_Sys_Staff")]
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_Sys_Staff")]
    [Description("Nhân viên hệ thống")]
    public class SysStaffVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Chữ cái đầu phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// KhoaCode
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Cờ hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng liên kết mặc định
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// Quyền mẫu
        /// </summary>
        public string mbqx { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string zc { get; set; }
    }
}
