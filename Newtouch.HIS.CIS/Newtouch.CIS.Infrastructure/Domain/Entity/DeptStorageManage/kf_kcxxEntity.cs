using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.CIS.Compat;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Tồn kho bảng
    /// </summary>
    [SugarTable("kf_kcxx")]
    public class KfKcxxEntity
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
        /// KhoID
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        ///  vật tư ID
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
        /// Hạn hiệu lực
        /// </summary>
        public DateTime yxq { get; set; }

        /// <summary>
        /// Tồn kho Số lượng Đơn vị nhỏ nhất Số lượng
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// đông kết Số lượng Đơn vị nhỏ nhất Số lượng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// ra Nhập khochi tiết ID
        /// </summary>
        public long crkmxId { get; set; }

        /// <summary>
        /// vào giá chuyển hóa vì con tương ứng Đơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal jj { get; set; }

        /// <summary>
        /// chuyển hóa vì con,chuyển hóa thành hiện tại Kho Đơn vị
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Tồn kho khóa 0/null:chưa trên khóa      >0:đã khóa
        /// </summary>
        public int? locked { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

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
