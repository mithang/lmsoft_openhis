using System.Collections.Generic;
using Newtouch.CIS.Compat;
using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09
{
    /// <summary>
    /// nhắc lấy Chẩn đoánthông tin yêu cầu thể
    /// </summary>
    [XmlRoot("Request")]
    public class PullDiagInfoRequestEntity : RequestBase
    {

        /// <summary>
        /// Chẩn đoánthông tin thể
        /// </summary>
        public DiagInfo DiagInfo { get; set; }
    }

    /// <summary>
    /// Chẩn đoánthông tin thể
    /// </summary>
    public class DiagInfo
    {
        /// <summary>
        /// Khám bệnh dòng chảy số
        /// bắt buộc
        /// </summary>
        public string serialNo { get; set; }

        /// <summary>
        /// Cơ sở Mã
        /// bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Chẩn đoánchi tiết tập hợp
        /// </summary>
        public List<Diagnosis> diagnosislist { get; set; }
    }

    /// <summary>
    /// Chẩn đoán mục
    /// </summary>
    public class Diagnosis
    {
        /// <summary>
        /// bản địa Chẩn đoán Mã
        /// </summary>
        public string localCode { get; set; }

        /// <summary>
        /// Mã chẩn đoán(ICD10)
        /// bắt buộc
        /// </summary>
        public string code { get; set; }

        /// <summary>
        ///Chẩn đoán Loại 0:tâyy 1:trongy
        /// bắt buộc
        /// </summary>
        public string cate { get; set; }

        /// <summary>
        ///Tên chẩn đoán
        /// bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///Chẩn đoán YHCT Loại 0:bệnh bệnh 1:hội chứng; khi Chẩn đoán Phân loại là trong y khi bắt buộc
        /// bắt buộc
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///Chẩn đoán YHCT phút nhóm này nhận dạng nhau cùng của bệnh bệnh và hội chứng Chẩn đoánlàmộtnhóm
        /// </summary>
        public string group { get; set; }

        /// <summary>
        ///Chẩn đoán YHCT cấp loại 0:chính cần Chẩn đoán 1: lần cần Chẩn đoán
        /// </summary>
        public string level { get; set; }

        /// <summary>
        ///Mô tả
        /// </summary>
        public string description { get; set; }
    }
}