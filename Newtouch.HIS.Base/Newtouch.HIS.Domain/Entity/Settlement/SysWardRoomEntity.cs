using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Phòng bệnh
    /// </summary>
    [Table("xt_bf")]
    public class SysWardRoomEntity : IEntity<SysWardRoomEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int bfId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã
        /// </summary>
        public string bfCode { get; set; }

        /// <summary>
        /// Số phòng
        /// </summary>
        public string bfNo { get; set; }

        /// <summary>
        /// Khu bệnh
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

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

    }
}
