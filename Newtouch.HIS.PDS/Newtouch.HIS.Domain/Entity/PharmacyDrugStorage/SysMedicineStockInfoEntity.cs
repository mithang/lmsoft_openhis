using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc thông tin tồn kho xt_yp_kcxx
    /// </summary>
    [Table("xt_yp_kcxx")]
    public class SysMedicineStockInfoEntity : IEntity<SysMedicineStockInfoEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string kcId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypkw { get; set; }

        /// <summary>
        /// 0: không kiểm soát 1: kiểm soát
        /// </summary>
        public short kzbz { get; set; }

        /// <summary>
        /// 已配药未发 (领)thuốc của số lượng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 0: kích hoạt 1: ngừng sử dụng
        /// </summary>
        public short tybz { get; set; }

        /// <summary>
        /// từ bảng XT_YP_CRKMXK 
        /// </summary>
        public string crkmxId { get; set; }

        /// <summary>
        /// giá nhập  đơn vị kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// tương ứng nhà thuốc nhà thuốc của tách lẻ số
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// 产地目录
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string pc { get; set; }

        /// <summary>
        /// 是否已锁  0: 未锁; 1/null: 已锁
        /// </summary>
        public string locked { get; set; }

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
