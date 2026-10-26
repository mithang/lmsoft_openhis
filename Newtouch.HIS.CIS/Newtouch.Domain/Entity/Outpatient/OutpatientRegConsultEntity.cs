using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity.Outpatient
{
    /// <summary>
    /// Bảng quan hệ phòng khám đăng ký khám ngoại trú
    /// </summary>
    [Table("mz_ghzs")]
    public class OutpatientRegConsultEntity: IEntity<OutpatientRegConsultEntity>
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
        /// Trạng thái gọi số (Chờ gọi số1,Đã gọi2,Quá số3 Trả lời4 Gọi lại số5)
        /// </summary>
        public int? calledstu { get; set; }

    }
}
