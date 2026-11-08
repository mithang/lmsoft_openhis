using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// (Ngoại trú)Điều dưỡng nhập,Bác sĩ sách viết Hồ sơ khi mang vào
    /// </summary>
    [SugarTable("xt_brsmtz")]
    public class PatientVitalSignsEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tizhong { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tiwen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? maibo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xueya { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xuetang { get; set; }

        /// <summary>
        /// 
        /// </summary>
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

        public decimal? shengao { get; set; }
        public decimal? shousuoya { get; set; }
        public decimal? shuzhangya { get; set; }
        [SugarColumn(IsIgnore = true)]
        public decimal? huxi { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string xuetangclfs { get; set; }
        [SugarColumn(IsIgnore = true)]
        public int? fzbz { get; set; }
        [SugarColumn(IsIgnore = true)]
        public int? hy { get; set; }

    }
}
