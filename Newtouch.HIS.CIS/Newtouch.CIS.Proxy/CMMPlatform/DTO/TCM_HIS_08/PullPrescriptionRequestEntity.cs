using System.Collections.Generic;
using System.Xml.Serialization;
using FrameworkBase.MultiOrg.Domain.ValueObjects;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08
{
    /// <summary>
    /// Thân yêu cầu trích xuất đơn thuốc
    /// </summary>
    [XmlRoot("Request")]
    public class PullPrescriptionRequestEntity : RequestBase
    {
        /// <summary>
        /// Thân thông tin đơn thuốc
        /// </summary>
        public Prescription Prescription { get; set; }
    }

    /// <summary>
    /// Thân thông tin đơn thuốc
    /// </summary>
    public class Prescription
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
        /// Tên bài thuốc chọn trực tiếp
        /// </summary>
        public string preName { get; set; }

        /// <summary>
        /// Tập hợp thông tin đơn thuốc
        /// </summary>
        public List<DrugData> drugDataList { get; set; }
    }

    /// <summary>
    /// Tập hợp thông tin đơn thuốc
    /// </summary>
    public class DrugData
    {

        /// <summary>
        /// Tên thuốc
        /// Bắt buộc
        /// </summary>
        public string drugName { get; set; }

        /// <summary>
        /// Mã thuốc
        /// Bắt buộc
        /// </summary>
        public string drugCode { get; set; }

        /// <summary>
        /// Đơn vị thuốc
        /// </summary>
        public string drugUnit { get; set; }

        /// <summary>
        /// Số lượng
        /// Bắt buộc
        /// </summary>
        public string drugQuantity { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string note { get; set; }
    }

    /// <summary>
    /// Thông tin mở rộng của đơn thuốc
    /// </summary>
    public class DrugDataEx : SfxmYpSelectResultVO
    {
        /// <summary>
        /// Tên thuốc
        /// Bắt buộc
        /// </summary>
        public string drugName { get; set; }

        /// <summary>
        /// Mã thuốc
        /// Bắt buộc
        /// </summary>
        public string drugCode { get; set; }

        /// <summary>
        /// Đơn vị thuốc
        /// </summary>
        public string drugUnit { get; set; }

        /// <summary>
        /// Số lượng
        /// Bắt buộc
        /// </summary>
        public string drugQuantity { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string note { get; set; }
    }
}