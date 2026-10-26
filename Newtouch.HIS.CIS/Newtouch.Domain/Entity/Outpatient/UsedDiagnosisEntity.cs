using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("mz_cyzd")]
    public class UsedDiagnosisEntity : IEntity<UsedDiagnosisEntity>
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
        /// 1 Chẩn đoán tây y,2 Chẩn đoán đông y
        /// </summary>
        public int? type { get; set; }

        public string ksCode { get; set; }

        /// <summary>
        /// Số lần sử dụng
        /// </summary>
        public int? sycs { get; set; }
        /// <summary>
        /// Liên kếtbaseCSDL Bảng chẩn đoán hệ thống
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// Tên chẩn đoán
        /// </summary>
        public string zdmc { get; set; }

        public string icd10 { get; set; }

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
        

    }
}
