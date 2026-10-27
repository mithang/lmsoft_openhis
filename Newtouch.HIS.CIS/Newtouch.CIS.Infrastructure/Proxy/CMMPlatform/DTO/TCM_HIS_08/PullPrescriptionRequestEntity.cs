using System.Collections.Generic;
using Newtouch.CIS.Compat;
using Newtouch.Domain.ValueObjects;
using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08
{
    /// <summary>
    /// nhắclấyĐơn thuốcyêu cầuthể
    /// </summary>
    [XmlRoot("Request")]
    public class PullPrescriptionRequestEntity : RequestBase
    {
        /// <summary>
        /// Đơn thuốcthông tinthể
        /// </summary>
        public Prescription Prescription { get; set; }
    }

    /// <summary>
    /// Đơn thuốcthông tinthể
    /// </summary>
    public class Prescription
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
        /// trực tiếpchọnphươngcủaphươngconTên
        /// </summary>
        public string preName { get; set; }

        /// <summary>
        /// thuốcphươngthông tintậphợp
        /// </summary>
        public List<DrugData> drugDataList { get; set; }
    }

    /// <summary>
    /// thuốcphươngthông tintậphợp
    /// </summary>
    public class DrugData
    {

        /// <summary>
        /// Tên thuốc
        /// bắt buộc
        /// </summary>
        public string drugName { get; set; }

        /// <summary>
        /// Mã thuốc
        /// bắt buộc
        /// </summary>
        public string drugCode { get; set; }

        /// <summary>
        /// Đơn vị thuốc
        /// </summary>
        public string drugUnit { get; set; }

        /// <summary>
        /// Số lượng
        /// bắt buộc
        /// </summary>
        public string drugQuantity { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string note { get; set; }
    }

    /// <summary>
    /// thuốcphươngmở rộngtriểnthông tin
    /// </summary>
    public class DrugDataEx : SfxmYpSelectResultVO
    {
        /// <summary>
        /// Tên thuốc
        /// bắt buộc
        /// </summary>
        public string drugName { get; set; }

        /// <summary>
        /// Mã thuốc
        /// bắt buộc
        /// </summary>
        public string drugCode { get; set; }

        /// <summary>
        /// Đơn vị thuốc
        /// </summary>
        public string drugUnit { get; set; }

        /// <summary>
        /// Số lượng
        /// bắt buộc
        /// </summary>
        public string drugQuantity { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string note { get; set; }
    }
}