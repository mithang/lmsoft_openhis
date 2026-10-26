using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc điều chỉnh giá lãi/lỗ
    /// </summary>
    [Table("xt_yp_tjsy")]
    public class SysMedicinePriceAdjustmentProfitLossEntity : IEntity<SysMedicinePriceAdjustmentProfitLossEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string TjsyId { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// thuốc code
        /// </summary>
        public string ypCode{ get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// điều chỉnh giá thời gian
        /// </summary>
        public DateTime Tjsj { get; set; }

        /// <summary>
        /// điều chỉnh giá tệp
        /// </summary>
        public string Tjwj { get; set; }

        /// <summary>
        /// khi giờ số lượng tồn kho
        /// </summary>
        public int Dssl { get; set; }

        /// <summary>
        /// giá bán buôn gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ypfj { get; set; }

        /// <summary>
        /// giá bán lẻ gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ylsj { get; set; }

        /// <summary>
        /// Giá bán buôn gốc của kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ gốc của kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yyklsj { get; set; }

        /// <summary>
        /// giá bán buôn hiện tại
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xpfj { get; set; }

        /// <summary>
        /// giá bán lẻ hiện tại
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xlsj { get; set; }

        /// <summary>
        /// hiện tại giá bán buôn kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xykpfj { get; set; }

        /// <summary>
        /// hiện tại giá bán lẻ kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xyklsj { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// Lợi nhuận điều chỉnh giá bán buôn
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Pfjtjlr { get; set; }

        /// <summary>
        /// Lợi nhuận điều chỉnh giá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsjtjlr { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// người thao tác
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian thao tác
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
