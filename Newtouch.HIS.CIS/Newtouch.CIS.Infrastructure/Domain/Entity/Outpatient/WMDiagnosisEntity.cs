using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_xyzd")]
    public class WMDiagnosisEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string xyzdId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// liên kết Khám bệnhbảng
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Liệt kê 1 chính Chẩn đoán 2 phụ Chẩn đoán
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
        /// Nghi ngờ Cờ
        /// </summary>
        public bool ysbz { get; set; }
        /// <summary>
        /// Chẩn đoán Tây yGhi chú
        /// </summary>
        public string zdbz { get; set; }
        /// <summary>
        /// răng ký tựhình Loại
        /// </summary>
        public string ywlx { get; set; }
        /// <summary>
        /// răng ký tựhình Vị trí
        /// </summary>
        public string ywstr { get; set; }
        /// <summary>
        /// răng ký tựhình Hiển thị
        /// </summary>
        public string ywxs { get; set; }
    }
}
