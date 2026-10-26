
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///khoa hệ thống
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_FailedCodeMessageMapp", "SysFailedCodeMessageMapp")]
    public class SysFailedCodeMessageMappEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// tổ chứcId（bệnh viện có nghiệp vụ cụ thể）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// tổ chức cấp cao nhất（người đăng nhập hiện tại cóOrgkhiOrg，nếu khôngTopOrg）
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
