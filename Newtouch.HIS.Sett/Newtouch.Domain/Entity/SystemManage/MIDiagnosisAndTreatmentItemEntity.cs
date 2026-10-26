using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Hạng mục khám chữa bệnh BHYT
    /// (Tất cả hạng mục BHYT,Biên tậpxt_sfxmKhi đó tự động đưa raybdm,wjdmv.v.)
    /// </summary>
    [Table("yb_setup_zlxm")]
    public class MIDiagnosisAndTreatmentItemEntity : IEntity<MIDiagnosisAndTreatmentItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int setup_zlxmbh { get; set; }

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
        public string wjdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xmnh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cwnr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfbf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fylb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xdnr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xxzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? xxqsrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? xxsxrq { get; set; }

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
        public string zt { get; set; }

    }
}
