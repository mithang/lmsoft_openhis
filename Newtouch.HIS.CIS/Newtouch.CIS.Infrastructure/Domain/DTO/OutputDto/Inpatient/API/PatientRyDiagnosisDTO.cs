using System;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// Bệnh nhânChẩn đoán nhập viện
    /// </summary>
    public class PatientRyDiagnosisDto
    {
        /// <summary>
        /// Nhập việnnhiềuChẩn đoánID
        /// </summary>
        public string rydzdId { get; set; }
        
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Chẩn đoánMã
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
        /// Chẩn đoánThứ tự   1：Chẩn đoán1 2：Chẩn đoán2  3：Chẩn đoán3
        /// </summary>
        public string zdpx { get; set; }

        /// <summary>
        /// Tổ chứcId（cócụ thểthểnghiệp vụcủayviện）
        /// </summary>
        public string OrganizeId { get; set; }
        
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// tạoNgày
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// SửaNgày
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
        /// Trạng thái 0:Vô hiệu  1：Hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
