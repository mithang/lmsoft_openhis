using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chữ ký điện tử của bác sĩ
    /// </summary>
    [Table("Sys_StaffSignature")]
    public class SysStaffSignatureEntity : IEntity<SysStaffSignatureEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Id tổ chức (OrganizeId tương ứng với khoa)
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// ID nhân viên
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// Tên hình ảnh
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// Loại (jpg, pgn)
        /// </summary>
        public string ImageTpye { get; set; }
        /// <summary>
        /// Địa chỉ hình ảnh
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Mã hóa base64
        /// </summary>
        public string ImagePrefix { get; set; }
        /// <summary>
        /// Dữ liệu nhị phân hình ảnh
        /// </summary>
        public string ImageData { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người dùng tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối cùng
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người dùng sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
