using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// nhân viên hệ thống
    /// </summary>
    [Table("V_S_Sys_Staff")]
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_Sys_Staff")]
    [Description("系统人员")]
    public class SysStaffVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// số hiệu công tác
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// họ tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// chữ cái đầu của phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// khoaCode
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// cờ hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// mặc định liên kết loại phục hồi chức năng
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// quyền mẫu
        /// </summary>
        public string mbqx { get; set; }

        /// <summary>
        /// chức danh
        /// </summary>
        public string zc { get; set; }
    }
}
