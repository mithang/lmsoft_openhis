using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Tạo：张冬林
    /// Ngày tháng:2018-01-29 15:52
    /// Mô tả:Cấu hình hoa hồng lợi nhuận của kỹ thuật viên điều trị
    /// </summary>
    [Table("zlsps_m_config")]
    public class TherapeutistMonthProfitShareConfigEntity : IEntity<TherapeutistMonthProfitShareConfigEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// dlCode
        /// </summary>
        /// <returns></returns>
        public string dlCode { get; set; }
        /// <summary>
        /// gh
        /// </summary>
        /// <returns></returns>
        public string gh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }
        /// <summary>
        /// bl
        /// </summary>
        /// <returns></returns>
        public decimal? bl { get; set; }
        /// <summary>
        /// yce
        /// </summary>
        /// <returns></returns>
        public int? yce { get; set; }
        /// <summary>
        /// ycebl
        /// </summary>
        /// <returns></returns>
        public decimal? ycebl { get; set; }

        /// <summary>
        /// Vượt mức tháng thứ hai
        /// </summary>
        /// <returns></returns>
        public int? deyce { get; set; }
        /// <summary>
        /// Vượt mức tháng thứ hai Tỷ lệ
        /// </summary>
        /// <returns></returns>
        public decimal? deycebl { get; set; }

        /// <summary>
        /// Vượt mức tháng thứ ba
        /// </summary>
        /// <returns></returns>
        public int? dsyce { get; set; }
        /// <summary>
        /// Tỷ lệ vượt mức tháng thứ ba
        /// </summary>
        /// <returns></returns>
        public decimal? dsycebl { get; set; }
        /// <summary>
        /// nce
        /// </summary>
        /// <returns></returns>
        public int? nce { get; set; }
        /// <summary>
        /// ncebl
        /// </summary>
        /// <returns></returns>
        public decimal? ncebl { get; set; }
        /// <summary>
        /// blhgdje
        /// </summary>
        /// <returns></returns>
        public int? blhgdje { get; set; }
        /// <summary>
        /// bz
        /// </summary>
        /// <returns></returns>
        public string bz { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// px
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}