using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.HospitalizationPharmacy
{
    [Table("xt_ksbythmx")]
    [SugarTable("xt_ksbythmx")]
    public class XtksbythmxEntity : IEntity<XtksbythmxEntity>
    {
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string byId { get; set; }
        public string OrganizeId { get; set; }
        public string ypdm { get; set; }
        public string ypmc { get; set; }
        public string yplb { get; set; }
        public decimal tsl { get; set; }
        public string dw { get; set; }
        public string gg { get; set; }
        public DateTime? yxq { get; set; }
        //public string yfbm { get; set; }
        public string sccj { get; set; }
        //public string thyy { get; set; }
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
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        public string ph { get; set; }
        public string pc { get; set; }
    }
}
