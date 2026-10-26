using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("XT_YP_KCXXK")]
    [Obsolete("不该在此库")]
    public class SysDrugInventoryEntity : IEntity<SysDrugInventoryEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Kcxh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ksdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// 0:Không kiểm soát1:Kiểm soát
        /// </summary>
        public short Kzbz { get; set; }

        /// <summary>
        /// Đã pha thuốc chưa phát(lĩnh)Số lượng thuốc
        /// </summary>
        public int Djsl { get; set; }

        /// <summary>
        /// 0:Kích hoạt1:Ngừng sử dụng
        /// </summary>
        public short Tybz { get; set; }

        /// <summary>
        /// Đến từ Bảng XT_YP_CRKMXK 
        /// </summary>
        public int Crkmxxh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// Số lẻ đã tách của nhà thuốc tương ứng
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Danh mục nơi sản xuất
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
