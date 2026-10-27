using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Kết chuyển tồn kho thuốc hệ thống
    /// </summary>
    [Table("xt_yp_kcjzk")]
    [SugarTable("xt_yp_kcjzk")]
    public class SysMedicineStockCarryDownEntity : IEntity<SysMedicineStockCarryDownEntity>
    {
        /// <summary>
        /// Thành phần
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Giá trị mặc địnhGetdate()
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// Số lượng tồn kho
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// Giá nhập kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Jj { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// Thời gian kết chuyển
        /// </summary>
        public DateTime Jzsj { get; set; }

        /// <summary>
        /// Trạng thái 0：Không hiệu lực  1：Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
