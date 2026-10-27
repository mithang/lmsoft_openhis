using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Thông tin tồn kho thuốc hệ thống xt_yp_kcxx
    /// </summary>
    [Table("xt_yp_kcxx")]
    [SugarTable("xt_yp_kcxx")]
    public class SysMedicineStockInfoEntity : IEntity<SysMedicineStockInfoEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
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
        /// 0：Không kiểm soát1：Kiểm soát
        /// </summary>
        public short kzbz { get; set; }

        /// <summary>
        /// Đã chuẩn bị thuốc chưa phát（nhận）Số lượng thuốc
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 0：Bật1：Ngừng dùng
        /// </summary>
        public short tybz { get; set; }

        /// <summary>
        /// Từ bảng XT_YP_CRKMXK 
        /// </summary>
        public string crkmxId { get; set; }

        /// <summary>
        /// Giá nhập  Đơn vị kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// Số chia lẻ của nhà thuốc tương ứng
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Danh mục nơi sản xuất
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string pc { get; set; }

        /// <summary>
        /// Đã khóa chưa  0：Chưa khóa ；    1/null：Đã khóa
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
