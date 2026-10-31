using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("mz_cyzd")]
    public class UsedDiagnosisEntity
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
        /// 1 Chẩn đoán Tây y,2 Chẩn đoán YHCT
        /// </summary>
        public int? type { get; set; }

        public string ksCode { get; set; }

        /// <summary>
        /// Sử dụng lầnsố
        /// </summary>
        public int? sycs { get; set; }
        /// <summary>
        /// liên kếtbasekho Chẩn đoán hệ thốngbảng
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
