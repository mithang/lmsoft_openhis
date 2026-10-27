using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.CIS.Compat;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Tồn kho_Tồn khokết chuyển
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
        /// Tổ chứcId（cócụ thểthểnghiệp vụcủayviện）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// KhoMã
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// vật tưID(bắt buộc)
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
        /// Hiệu lựcThời gian
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Tồn khoSố lượng，Đơn vị nhỏ nhấtSố lượng
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Khoa phòngGiá bán lẻ，chuyểnhóavìcontương ứngĐơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal bmlsj { get; set; }

        /// <summary>
        /// vàogiá，chuyểnhóavìcontương ứngĐơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal jj { get; set; }

        /// <summary>
        /// chuyểnhóavìcon(bắt buộc)
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// kết chuyểnThời gian
        /// </summary>
        public DateTime jzsj { get; set; }

        /// <summary>
        /// Trạng thái  0:Hủy bỏ；1.Hiệu lực
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
