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
    /// Ngoại trú đăng ký khám Phòng khám quan hệ bảng
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
        /// gọi số Trạng thái (chờ gọi số1,đã gọi2,qua số3 ứng đáp4 lại gọi số5)
        /// </summary>
        public int? calledstu { get; set; }

    }
}
