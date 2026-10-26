using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity.Inpatient
{
    [Table("zy_bindfee")]
    public class MedicalAdviceBindingFeeEntity : IEntity<MedicalAdviceBindingFeeEntity>
    {
        [Key]
        public string newid { get; set; }
        public string zyh { get; set; }
        public string yzh { get; set; }
        public string zh { get; set; }
        public string sfxm { get; set; }
        public string sfxmmc { get; set; }
        public decimal sl { get; set; }
        public string dw { get; set; }
        public string dl { get; set; }
        public string dlmc { get; set; }
        public decimal dj { get; set; }
        public decimal je { get; set; }
        public string yfdm { get; set; }
        public int? cls { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }


        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        public string yzxz { get; set; }
        public string gg { get; set; }
        public string pcmc { get; set; }
        public string yzId { get; set; }
    }
}
