using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Nhân viên hệ thống
    /// </summary>
    [Table("xt_ry")]
    [Obsolete("please use the view")]
    public class SysStaffEntity : IEntity<SysStaffEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int rybh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short ry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string rymc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yhm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 0 Không phải bác sĩ 1 Bác sĩ thực tập 2 Bác sĩ luân chuyển    3 Bác sĩ nội trú 4 Bác sĩ điều trị chính 5 Phó chủ nhiệm bác sĩ 6 Bác sĩ trưởng khoa
        /// </summary>
        public string ysbz { get; set; }

        /// <summary>
        /// 0 Không thể thao tác hệ thống 1 Có thể thao tác hệ thống
        /// </summary>
        public string czbz { get; set; }

        /// <summary>
        /// 1 Có quyền tối đa 0 Không có quyền tối đa
        /// </summary>
        public string zdqxbz { get; set; }

        /// <summary>
        /// 0 Không khả dụng 1 Khả dụng
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zp { get; set; }

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
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ryCode { get; set; }

    }
}
