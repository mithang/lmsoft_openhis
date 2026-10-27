using System.Collections.Generic;
using Newtouch.CIS.Compat;
using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09
{
    /// <summary>
    /// nhắclấyChẩn đoánthông tinyêu cầuthể
    /// </summary>
    [XmlRoot("Request")]
    public class PullDiagInfoRequestEntity : RequestBase
    {

        /// <summary>
        /// Chẩn đoánthông tinthể
        /// </summary>
        public DiagInfo DiagInfo { get; set; }
    }

    /// <summary>
    /// Chẩn đoánthông tinthể
    /// </summary>
    public class DiagInfo
    {
        /// <summary>
        /// Khám bệnhdòngchảysố
        /// bắt buộc
        /// </summary>
        public string serialNo { get; set; }

        /// <summary>
        /// Cơ sởMã
        /// bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Chẩn đoánchi tiếttậphợp
        /// </summary>
        public List<Diagnosis> diagnosislist { get; set; }
    }

    /// <summary>
    /// Chẩn đoán mục
    /// </summary>
    public class Diagnosis
    {
        /// <summary>
        /// bảnđịaChẩn đoánMã
        /// </summary>
        public string localCode { get; set; }

        /// <summary>
        /// Mã chẩn đoán（ICD10）
        /// bắt buộc
        /// </summary>
        public string code { get; set; }

        /// <summary>
        ///Chẩn đoánLoại 0：tâyy 1：trongy
        /// bắt buộc
        /// </summary>
        public string cate { get; set; }

        /// <summary>
        ///Tên chẩn đoán
        /// bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///Chẩn đoán YHCTLoại 0：bệnhbệnh 1：hội chứng； khiChẩn đoánPhân loạilàtrongykhibắt buộc
        /// bắt buộc
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///Chẩn đoán YHCTphútnhóm nàynhận dạngnhaucùngcủabệnhbệnhvàhội chứngChẩn đoánlàmộtnhóm
        /// </summary>
        public string group { get; set; }

        /// <summary>
        ///Chẩn đoán YHCTcấploại 0：chínhcầnChẩn đoán 1： lầncầnChẩn đoán
        /// </summary>
        public string level { get; set; }

        /// <summary>
        ///Mô tả
        /// </summary>
        public string description { get; set; }
    }
}