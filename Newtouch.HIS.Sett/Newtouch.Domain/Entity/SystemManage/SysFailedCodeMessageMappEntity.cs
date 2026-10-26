using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Tạo：Trương Đông Lâm
    /// Ngày tháng:2017-09-29 19:53
    /// Mô tả:Cấu hình nhắc lỗi
    /// </summary>
    [Table("Sys_FailedCodeMessageMapp")]
    public class SysFailedCodeMessageMappEntity : IEntity<SysFailedCodeMessageMappEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Tổ chức cấp cao nhấtId
        /// </summary>
        /// <returns></returns>
        public string TopOrganizeId { get; set; }
        /// <summary>
        /// Dài kiểm tra siêu dài dài kiểm tra siêu dài dài kiểm tra siêu dài dài kiểm tra siêu dài dài kiểm tra siêu dài dài kiểm tra siêu dài dài kiểm tra siêu dài dài kiểm tra siêu dài dài kiểm tra siêu dài dài kiểm tra siêu dài...
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string code { get; set; }
        /// <summary>
        /// msg
        /// </summary>
        /// <returns></returns>
        public string msg { get; set; }
        /// <summary>
        /// px
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
    }
}