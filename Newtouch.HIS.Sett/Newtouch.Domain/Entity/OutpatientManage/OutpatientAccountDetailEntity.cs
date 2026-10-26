using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết kế hoạch ghi sổ ngoại trú
    /// </summary>
    [Table("mz_jzjhmx")]
    public class OutpatientAccountDetailEntity : IEntity<OutpatientAccountDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jzjhId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int zlsc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? jzsj { get; set; }

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
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        public string ys { get; set; }
        public string ks { get; set; }
        public string ysmc { get; set; }
        public string ksmc { get; set; }
        /// <summary>
        /// Cờ đoàn thể
        /// </summary>
        public bool? ttbz { get; set; }
        public string kflb { get; set; }

        /// <summary>
        /// Liều điều trị
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// Thời gian thực hiện lần cuối
        /// </summary>
        public DateTime? LastEexcutionTime { get; set; }

        /// <summary>
        /// Trạng thái thực hiện Xem enumEnumJzjhZXZT
        /// </summary>
        public int? zxzt { get; set; }
        public string yzxz { get; set; }
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Tổng số lần
        /// </summary>
        public int? zcs { get; set; }

        /// <summary>
        /// Số lần đã thực hiện
        /// </summary>
        public int? yzxcs { get; set; }

        /// <summary>
        /// Khoa thực hiện
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

    }
}
