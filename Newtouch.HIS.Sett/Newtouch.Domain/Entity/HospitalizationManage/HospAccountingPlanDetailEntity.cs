using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("zy_jzjhmx")]
    public class HospAccountingPlanDetailEntity : IEntity<HospAccountingPlanDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
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
        public string zyh { get; set; }

        /// <summary>
        /// Tính chất y lệnh 1Tạm thời 2Dài hạn
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// Loại y lệnh 1Thuốc 2Hạng mục điều trị
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Hạng mục thu phíCode
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// Thời lượng đơn vị hạng mục
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Thời gian thực hiện lần cuối
        /// </summary>
        public DateTime? LastEexcutionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int zxzt { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Cờ đoàn thể(Có phải là điều trị tập thể hay không)
        /// </summary>
        public bool? ttbz { get; set; }

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
        public DateTime? PreStopDate { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// Liều điều trị
        /// </summary>
        public int? zll { get; set; }
        /// <summary>
        /// Tổng số lần(Nhưng số lần tối đa mỗi ngày của y lệnh dài hạn nội trú,Kế hoạch dài hạn phải dừng thủ công)
        /// </summary>
        public int? zcs { get; set; }

        /// <summary>
        /// Số lần đã thực hiện
        /// </summary>
        public int? yzxcs { get; set; }
        public string yzId { get; set; }

        /// <summary>
        /// Khoa thực hiện
        /// </summary>
        public string zxks { get; set; }
    }
}
