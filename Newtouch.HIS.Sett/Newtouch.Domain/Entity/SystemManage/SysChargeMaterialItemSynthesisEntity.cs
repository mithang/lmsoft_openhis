using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Tổng hợp hạng mục vật tư thu phí hệ thống
    /// </summary>
    [Table("xt_sfclxmzh")]
    public class SysChargeMaterialItemSynthesisEntity : IEntity<SysChargeMaterialItemSynthesisEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clzhxmbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string clzhxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string clzhxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Xác định tỷ lệ tiền mặt tự chi trả,Không liên quan đến tính chất của phần này(Tự chi trả theo phân loại,Tự lo)   Xem trườngzfxz      Chú ý:Khi tỷ lệ tự chi trả là số âm,Thể hiện mức tự chi trả cố định   (Ví dụ:Một loại vật tư nào đó12300,Được thanh toán10000,thì tỷ lệ tự chi trả＝-2300)
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// Hạn mức thanh toán tối đa tổng hợp của vật tư dùng một lần
        /// </summary>
        public decimal kbfwsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? qfbs { get; set; }

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
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
