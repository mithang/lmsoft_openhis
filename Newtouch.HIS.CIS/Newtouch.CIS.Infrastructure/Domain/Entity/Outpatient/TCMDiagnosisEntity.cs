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
        /// liên kết Khám bệnh bảng
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Liệt kê 1 chính Chẩn đoán 2 phụ Chẩn đoán
        /// </summary>
        public int zdlx { get; set; }

        /// <summary>
        /// liên kếtbasekho Chẩn đoán hệ thống bảng
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// Dư thừa chữ đoạn
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
        /// hội chứng Mã
        /// </summary>
        public string zhCode { get; set; }

        /// <summary>
        /// hội chứng Tên
        /// </summary>
        public string zhmc { get; set; }
        /// <summary>
        /// Chẩn đoán YHCT Ghi chú
        /// </summary>
        public string zdbz { get; set; }
    }
}
