using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Lịch trực đăng ký khám ngoại trú
    /// </summary>
    [Table("mz_ghpb")]
    public class OutpatientRegistScheduleEntity : IEntity<OutpatientRegistScheduleEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ghpbId { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 0 Tất cả bác sĩ
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 0không 1Cả ngày 2Buổi sáng 3Buổi chiều
        /// </summary>
        public string zyi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zsan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zsi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zwu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlv { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Tổng lượng số
        /// </summary>
        public int? zhl { get; set; }

        /// <summary>
        /// Bệnh chuyên khoa đăng ký khám
        /// </summary>
        public string ghzb { get; set; }

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
        /// Hạng mục đăng ký khám
        /// </summary>
        public string ghlx { get; set; }

        /// <summary>
        /// Hạng mục khám chữa bệnh
        /// </summary>
        public string zlxm { get; set; }

        /// <summary>
        /// Cờ khám ngoại trú/cấp cứu Khám ngoại trú Cấp cứu Chuyên gia
        /// </summary>
        public string mjzbz { get; set; }

    }
}
