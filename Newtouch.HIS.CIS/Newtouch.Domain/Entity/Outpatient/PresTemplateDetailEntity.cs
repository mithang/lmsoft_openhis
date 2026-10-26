using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_cfmbmx")]
    public class PresTemplateDetailEntity : IEntity<PresTemplateDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string mxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Liên kết bảng mẫu
        /// </summary>
        public string mbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Trường dư thừa
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Mã dư thừa
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Đối với đơn thuốc phục hồi chức năng
        /// </summary>
        public int? mczll { get; set; }

        /// <summary>
        /// Đối với thuốc
        /// </summary>
        public decimal? mcjl { get; set; }

        /// <summary>
        /// Đối với thuốc
        /// </summary>
        public string mcjldw { get; set; }

        /// <summary>
        /// Đối với thuốc
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pcCode { get; set; }

        /// <summary>
        /// Đối với thuốc
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Đối với đơn thuốc phục hồi chức năng
        /// </summary>
        public int? zl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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
        public string zt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Khoa thực hiện(Khoa lĩnh thuốc yfbmCode)
        /// </summary>
        public string zxks { get; set; }
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }
        
    }
}