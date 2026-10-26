using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Log")]
    public class SysLogEntity : IEntity<SysLogEntity>
    {
        /// <summary>
        /// Khóa chính nhật ký
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Ngày
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Loại
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Địa chỉ IP
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Thành phố nơi đặt IP
        /// </summary>
        public string IPAddressName { get; set; }

        /// <summary>
        /// Id module hệ thống
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// Module hệ thống
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public bool? Result { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

    }
}
