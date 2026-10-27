using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("Sys_Log")]
    public class SysLogEntity : EntityBase
    {
        /// <summary>
        /// Khóa chính nhật ký
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
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
        /// IPĐịa chỉ
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// IPThành phố nơi ở
        /// </summary>
        public string IPAddressName { get; set; }

        /// <summary>
        /// Mô-đun hệ thốngId
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// Mô-đun hệ thống
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
