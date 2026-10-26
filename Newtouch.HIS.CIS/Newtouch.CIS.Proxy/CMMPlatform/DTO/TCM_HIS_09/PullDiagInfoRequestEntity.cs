using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09
{
    /// <summary>
    /// Thân yêu cầu trích xuất thông tin chẩn đoán
    /// </summary>
    [XmlRoot("Request")]
    public class PullDiagInfoRequestEntity : RequestBase
    {

        /// <summary>
        /// Thân thông tin chẩn đoán
        /// </summary>
        public DiagInfo DiagInfo { get; set; }
    }

    /// <summary>
    /// Thân thông tin chẩn đoán
    /// </summary>
    public class DiagInfo
    {
        /// <summary>
        /// Số thứ tự khám bệnh
        /// Bắt buộc
        /// </summary>
        public string serialNo { get; set; }

        /// <summary>
        /// Mã tổ chức
        /// Bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Tập hợp chi tiết chẩn đoán
        /// </summary>
        public List<Diagnosis> diagnosislist { get; set; }
    }

    /// <summary>
    /// Hạng mục chẩn đoán
    /// </summary>
    public class Diagnosis
    {
        /// <summary>
        /// Mã chẩn đoán nội bộ
        /// </summary>
        public string localCode { get; set; }

        /// <summary>
        /// Mã chẩn đoán (ICD10)
        /// Bắt buộc
        /// </summary>
        public string code { get; set; }

        /// <summary>
        ///Loại chẩn đoán 0: Tây y 1: Đông y
        /// Bắt buộc
        /// </summary>
        public string cate { get; set; }

        /// <summary>
        ///Tên chẩn đoán
        /// Bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///Loại chẩn đoán Đông y 0: Bệnh 1: Chứng hậu; bắt buộc khi phân loại chẩn đoán là Đông y
        /// Bắt buộc
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///Nhóm chẩn đoán Đông y: Các chẩn đoán bệnh và chứng hậu có cùng định danh này là một nhóm
        /// </summary>
        public string group { get; set; }

        /// <summary>
        ///Cấp chẩn đoán Đông y 0: Chẩn đoán chính 1: Chẩn đoán phụ
        /// </summary>
        public string level { get; set; }

        /// <summary>
        ///Mô tả
        /// </summary>
        public string description { get; set; }
    }
}