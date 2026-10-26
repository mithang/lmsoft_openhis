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
    [Table("zy_ksbyth")]
    public class PrapareMedicineReturnEntity : IEntity<PrapareMedicineReturnEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string djh { get; set; }
        public string thzt { get; set; }
        public string yfbm { get; set; }
        public string ksbm { get; set; }
        public string bqbm { get; set; }
        public DateTime? tjsj { get; set; }
        public string thyy { get; set; }
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
    }
}
