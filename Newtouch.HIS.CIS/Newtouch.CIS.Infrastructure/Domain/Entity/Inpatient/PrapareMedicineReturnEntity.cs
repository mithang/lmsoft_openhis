using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity.Inpatient
{
    [SugarTable("zy_ksbyth")]
    public class PrapareMedicineReturnEntity
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
        /// Sửa người dùng
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// Sửa cuốiNgười dùng
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
