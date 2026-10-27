using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_blmbxyzd")]
    public class MRTemplateWMDiagnosisEntity
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
        public string mbId { get; set; }

        /// <summary>
        /// Liệt kê 1 chínhChẩn đoán 2 phụChẩn đoán
        /// </summary>
        public int zdlx { get; set; }

        /// <summary>
        /// liên kếtbasekho Chẩn đoán hệ thốngbảng
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// Dư thừachữđoạn
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
        /// Nghi ngờCờ
        /// </summary>
        public bool ysbz { get; set; }

    }
}
