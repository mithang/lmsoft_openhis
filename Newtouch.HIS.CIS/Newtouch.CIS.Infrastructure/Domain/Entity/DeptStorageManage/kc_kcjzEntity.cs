using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.CIS.Compat;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Tồn kho_Tồn kho kết chuyển
    /// </summary>
    [SugarTable("dept_kcjz")]
    public class KcKcjzEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức Id(có cụ thể thể nghiệp vụ của y viện)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Kho Mã
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// vật tư ID(bắt buộc)
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hiệu lực Thời gian
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Tồn kho Số lượng,Đơn vị nhỏ nhất Số lượng
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Khoa phòng Giá bán lẻ,chuyển hóa vì con tương ứng Đơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal bmlsj { get; set; }

        /// <summary>
        /// vào giá,chuyển hóa vì con tương ứng Đơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal jj { get; set; }

        /// <summary>
        /// chuyển hóa vì con(bắt buộc)
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// kết chuyển Thời gian
        /// </summary>
        public DateTime jzsj { get; set; }

        /// <summary>
        /// Trạng thái  0:Hủy bỏ;1.Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }

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
        /// Sửa cuối người
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
