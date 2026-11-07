using System;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// Bệnh nhân Chẩn đoán nhập viện
    /// </summary>
    public class PatientRyDiagnosisDto
    {
        /// <summary>
        /// Nhập việnnhiều Chẩn đoán ID
        /// </summary>
        public string rydzdId { get; set; }
        
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Chẩn đoán Mã
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
        /// Chẩn đoán Thứ tự   1:Chẩn đoán1 2:Chẩn đoán2  3:Chẩn đoán3
        /// </summary>
        public string zdpx { get; set; }

        /// <summary>
        /// Tổ chức Id(cócụ thểthểnghiệp vụcủayviện)
        /// </summary>
        public string OrganizeId { get; set; }
        
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// tạo Ngày
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Sửa Ngày
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa người
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái 0:Vô hiệu  1:Hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
