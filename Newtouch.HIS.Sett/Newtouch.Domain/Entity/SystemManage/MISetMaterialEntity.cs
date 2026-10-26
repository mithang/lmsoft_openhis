using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("yb_setup_cl")]
    public class MISetMaterialEntity : IEntity<MISetMaterialEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long cl_id { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yjml { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ejml { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sanjml { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sijml { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zch { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ggxh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string scqy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jxqy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? xxqxrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? xxsxrq { get; set; }

        /// <summary>
        /// Ngày bắt đầu hiệu lực chứng nhận đăng ký
        /// </summary>
        public DateTime? scsxrq { get; set; }

        /// <summary>
        /// Ngày kết thúc hiệu lực chứng nhận đăng ký
        /// </summary>
        public DateTime? scjsrq { get; set; }

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
