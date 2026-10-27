
using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.Entity.Purchase
{
    /// <summary>
    /// Điểm phân phối mua sắm
    /// </summary>
    [SugarTable("xt_wz_psd")]
    public class PurchaseLocationEntity : IEntity<PurchaseLocationEntity>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string psdbm { get; set; }
        public string psdmc { get; set; }
        public string psdz { get; set; }
        public string lxrxm { get; set; }
        public string lxdh { get; set; }
        public string yzbm { get; set; }
        public string bzxx { get; set; }
        public int? psdzt { get; set; }
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
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
