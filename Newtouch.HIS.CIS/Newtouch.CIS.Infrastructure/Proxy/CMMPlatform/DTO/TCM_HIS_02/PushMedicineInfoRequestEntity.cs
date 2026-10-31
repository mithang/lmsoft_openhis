using System.Xml.Serialization;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_02
{
    /// <summary>
    /// đẩythảo dượcthông tin yêu cầuthể
    /// </summary>
    [XmlRoot("Request")]
    public class PushMedicineInfoRequestEntity : RequestBase
    {
        /// <summary>
        /// thảo dượcthông tinthể
        /// </summary>
        public Medicine Medicine { get; set; }
    }

    /// <summary>
    /// thảo dượcthông tinthể
    /// </summary>
    public class Medicine
    {
        /// <summary>
        /// ThuốcSố hiệu
        /// bắt buộc
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Tên thuốc
        /// bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Mã thuốc
        /// bắt buộc
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
        /// làKhôngKích hoạt:Y: là N: Không
        /// </summary>
        public string activeFlg { get; set; }

        /// <summary>
        /// Cơ sởMã
        /// bắt buộc
        /// </summary>
        public string orgCode { get; set; }

    }
}