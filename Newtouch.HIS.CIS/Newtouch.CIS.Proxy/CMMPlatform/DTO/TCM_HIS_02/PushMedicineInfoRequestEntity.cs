using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_02
{
    /// <summary>
    /// Thân yêu cầu đẩy thông tin thảo dược
    /// </summary>
    [XmlRoot("Request")]
    public class PushMedicineInfoRequestEntity : RequestBase
    {
        /// <summary>
        /// Thân thông tin thảo dược
        /// </summary>
        public Medicine Medicine { get; set; }
    }

    /// <summary>
    /// Thân thông tin thảo dược
    /// </summary>
    public class Medicine
    {
        /// <summary>
        /// Số thuốc
        /// Bắt buộc
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Tên thuốc
        /// Bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Mã thuốc
        /// Bắt buộc
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string specification { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string manufacturer { get; set; }

        /// <summary>
        /// Có bật hay không: Y: Có N: Không
        /// </summary>
        public string activeFlg { get; set; }

        /// <summary>
        /// Mã tổ chức
        /// Bắt buộc
        /// </summary>
        public string orgCode { get; set; }

    }
}