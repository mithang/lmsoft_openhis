using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity.Outpatient
{
    /// <summary>
    /// Ngoại trúđăng ký khámPhòng khámquan hệbảng
    /// </summary>
    [SugarTable("mz_ghzs")]
    public class OutpatientRegConsultEntity
    {
        [Key]
        public int Id { get; set; }
        public int ghnm { get; set; }
        public string zsCode { get; set; }
        public string OrganizeId { get; set; }
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
        /// gọisốTrạng thái （chờgọisố1，đãgọi2，quasố3 ứngđáp4 lạigọisố5）
        /// </summary>
        public int? calledstu { get; set; }

    }
}
