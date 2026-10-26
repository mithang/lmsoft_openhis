using System;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// Chẩn đoán nhập viện của bệnh nhân
    /// </summary>
    public class PatientRyDiagnosisDto
    {
        /// <summary>
        /// Nhiều chẩn đoán nhập việnID
        /// </summary>
        public string rydzdId { get; set; }
        
        /// <summary>
        /// Số nội trú
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Mã chẩn đoán
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// ICD10
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// Tên chẩn đoán
        /// </summary>
        public string zdmc { get; set; }

        /// <summary>
        /// Sắp xếp chẩn đoán   1:Chẩn đoán1 2:Chẩn đoán2  3:Chẩn đoán3
        /// </summary>
        public string zdpx { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }
        
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái 0:Vô hiệu  1:Có hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}