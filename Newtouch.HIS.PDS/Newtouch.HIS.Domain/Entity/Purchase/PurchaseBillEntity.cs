using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.Purchase
{
    [Table("xt_yp_fp")]
    public class PurchaseBillEntity : IEntity<PurchaseBillEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string fph { get; set; }
        public string yqbm { get; set; }
        public decimal fphszje { get; set; }
        public string fpzt { get; set; } //0.chưa nhập kho 1.đã nhập kho
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
        public string zt { get; set; }
    }
}
