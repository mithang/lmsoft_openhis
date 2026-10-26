using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.SystemManage
{
    /// <summary>
    /// Phòng khám hệ thống
    /// </summary>
    [Table("xt_zs")]
    public class SysConsultEntity : IEntity<SysConsultEntity>
    {
        [Key]
        public int zsId { get; set; }
        /// <summary>
        /// Mã phòng khám
        /// </summary>
        public string zsCode { get; set; }
        /// <summary>
        /// Tên phòng khám
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Id tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int xh { get; set; }
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
        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
