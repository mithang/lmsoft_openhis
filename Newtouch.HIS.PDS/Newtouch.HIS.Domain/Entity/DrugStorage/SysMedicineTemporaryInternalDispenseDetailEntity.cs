    using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc tạm thời trong bộ phát thuốc chi tiết
    /// </summary>
    [Table("xt_yp_ls_nbfymx")]
    public class SysMedicineTemporaryInternalDispenseDetailEntity : IEntity<SysMedicineTemporaryInternalDispenseDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string fymxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Yxrq { get; set; }

        /// <summary>
        /// giá bán buôn
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? Pfj { get; set; }

        /// <summary>
        /// giá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? Lsj { get; set; }

        /// <summary>
        /// tổng tiền bán buôn
        /// </summary>
        public decimal? Pfze { get; set; }

        /// <summary>
        /// tổng tiền bán lẻ
        /// </summary>
        public decimal? Ljze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Slsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Dw { get; set; }

        /// <summary>
        /// tồn kho hiện có
        /// </summary>
        public int? Xykz { get; set; }

        /// <summary>
        /// tách lẻ số
        /// </summary>
        public int? Fyzhyz { get; set; }

        /// <summary>
        /// tách lẻ số
        /// </summary>
        public int? Lyzhyz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Jqm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sldxh { get; set; }

        /// <summary>
        /// hiện tại có tổng tồn kho
        /// </summary>
        public int? Xyzkz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
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
