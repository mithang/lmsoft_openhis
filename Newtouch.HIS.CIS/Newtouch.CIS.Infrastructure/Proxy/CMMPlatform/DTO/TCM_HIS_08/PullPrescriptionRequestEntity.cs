using System.Collections.Generic;
using Newtouch.CIS.Compat;
using Newtouch.Domain.ValueObjects;
using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08
{
    /// <summary>
    /// nhắc lấy Đơn thuốc yêu cầu thể
    /// </summary>
    [XmlRoot("Request")]
    public class PullPrescriptionRequestEntity : RequestBase
    {
        /// <summary>
        /// Đơn thuốc thông tin thể
        /// </summary>
        public Prescription Prescription { get; set; }
    }

    /// <summary>
    /// Đơn thuốc thông tin thể
    /// </summary>
    public class Prescription
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
        /// trực tiếp chọn phương của phương con Tên
        /// </summary>
        public string preName { get; set; }

        /// <summary>
        /// thuốc phương thông tin tập hợp
        /// </summary>
        public List<DrugData> drugDataList { get; set; }
    }

    /// <summary>
    /// thuốc phương thông tin tập hợp
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
    /// thuốc phương mở rộng triển thông tin
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