using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Lãi lỗ điều chỉnh giá thuốc hệ thống
    /// </summary>
    [Table("xt_yp_tjsy")]
    [SugarTable("xt_yp_tjsy")]
    public class SysMedicinePriceAdjustmentProfitLossEntity : IEntity<SysMedicinePriceAdjustmentProfitLossEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string TjsyId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Thuốccode
        /// </summary>
        public string ypCode{ get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Thời gian điều chỉnh giá
        /// </summary>
        public DateTime Tjsj { get; set; }

        /// <summary>
        /// Tệp điều chỉnh giá
        /// </summary>
        public string Tjwj { get; set; }

        /// <summary>
        /// Số lượng tồn kho lúc đó
        /// </summary>
        public int Dssl { get; set; }

        /// <summary>
        /// Giá bán buôn gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ypfj { get; set; }

        /// <summary>
        /// Giá bán lẻ gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ylsj { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yyklsj { get; set; }

        /// <summary>
        /// Giá bán buôn hiện tại
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ hiện tại
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xlsj { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc hiện tại
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc hiện tại
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xyklsj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
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
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Người thao tác
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian thao tác
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
