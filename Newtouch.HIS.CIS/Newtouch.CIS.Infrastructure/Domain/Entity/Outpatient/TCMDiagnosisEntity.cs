using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_zyzd")]
    public class TCMDiagnosisEntity
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
        /// liên kếtKhám bệnhbảng
        /// </summary>
        public string jzId { get; set; }

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

        /// <summary>
        /// hội chứngMã
        /// </summary>
        public string zhCode { get; set; }

        /// <summary>
        /// hội chứngTên
        /// </summary>
        public string zhmc { get; set; }
        /// <summary>
        /// Chẩn đoán YHCTGhi chú
        /// </summary>
        public string zdbz { get; set; }
    }
}
