using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.CIS.Compat;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Tồn khobảng
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
        /// Tổ chứcId(cócụ thểthểnghiệp vụcủayviện)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// KhoID
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        ///  vật tưID
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
        /// Tồn khoSố lượng Đơn vị nhỏ nhấtSố lượng
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// đôngkếtSố lượng Đơn vị nhỏ nhấtSố lượng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// raNhập khochi tiếtID
        /// </summary>
        public long crkmxId { get; set; }

        /// <summary>
        /// vàogiá chuyểnhóavìcontương ứngĐơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal jj { get; set; }

        /// <summary>
        /// chuyểnhóavìcon,chuyểnhóathànhhiện tạiKhoĐơn vị
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Tồn khokhóa 0/null:chưatrênkhóa      >0:đãkhóa
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
