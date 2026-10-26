using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_zyzd")]
    public class TCMDiagnosisEntity : IEntity<TCMDiagnosisEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string zyzdId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Liên kết bảng khám bệnh
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Enum 1 Chẩn đoán chính 2 Chẩn đoán phụ
        /// </summary>
        public int zdlx { get; set; }

        /// <summary>
        /// Liên kếtbaseCSDL Bảng chẩn đoán hệ thống
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// Trường dư thừa
        /// </summary>
        public string zdmc { get; set; }

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

        /// <summary>
        /// Cờ nghi ngờ
        /// </summary>
        public bool ysbz { get; set; }

        /// <summary>
        /// Mã chứng hậu
        /// </summary>
        public string zhCode { get; set; }

        /// <summary>
        /// Tên chứng hậu
        /// </summary>
        public string zhmc { get; set; }
        /// <summary>
        /// Ghi chú chẩn đoán đông y
        /// </summary>
        public string zdbz { get; set; }
    }
}
