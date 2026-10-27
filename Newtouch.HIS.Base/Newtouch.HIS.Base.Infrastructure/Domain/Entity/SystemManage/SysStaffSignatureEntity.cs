using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chữ ký điện tử bác sĩ
    /// </summary>
    [SugarTable("Sys_StaffSignature")]
    public class SysStaffSignatureEntity : EntityBase
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// Tổ chứcId（tương ứng với khoaOrganizeId）
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Nhân viênID
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// Tên hình ảnh
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// Loại(jpg,pgn)
        /// </summary>
        public string ImageTpye { get; set; }
        /// <summary>
        /// Địa chỉ hình ảnh
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// base64Mã
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
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
