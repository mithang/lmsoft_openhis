using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_FailedCodeMessageMapp")]
    public class SysFailedCodeMessageMappEntity : IEntity<SysFailedCodeMessageMappEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// tổ chức Id (有具体业务的医院)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// tổ chức cấp cao nhất (hiện tại đăng nhập nhân viên có Org giờ Org, không thì TopOrg)
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
