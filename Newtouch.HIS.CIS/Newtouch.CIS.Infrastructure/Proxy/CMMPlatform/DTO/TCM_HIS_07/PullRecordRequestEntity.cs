using System.Xml.Serialization;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07
{
    /// <summary>
    /// nhắc lấy điện tử Hồ sơ hưởng ứng thể
    /// </summary>
    [XmlRoot("Request")]
    public class PullRecordRequestEntity : RequestBase
    {
        /// <summary>
        /// Hồ sơ thông tin thể
        /// </summary>
        public Record Record { get; set; }
    }

    /// <summary>
    /// Hồ sơ thông tin thể
    /// </summary>
    public class Record
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
        /// Bệnh nhân Số hiệu
        /// bắt buộc
        /// </summary>
        public string patiId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Bệnh nhân Tuổi
        /// bắt buộc
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// Bệnh nhân Giới tính
        /// bắt buộc
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// Khám bệnh Ngày-YYYYMMDD
        /// bắt buộc
        /// </summary>
        public string clinicDate { get; set; }

        /// <summary>
        /// Bác sĩ Số hiệu(Người dùng tên)
        /// bắt buộc
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Bác sĩ Tên
        /// bắt buộc
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// chính bảo
        /// </summary>
        public string priDepict { get; set; }

        /// <summary>
        /// localDisHis
        /// </summary>
        public string localDisHis { get; set; }

        /// <summary>
        /// qua Tiền sử dị ứng
        /// </summary>
        public string allergicHis { get; set; }

        /// <summary>
        /// Tiền sử
        /// </summary>
        public string anamnesis { get; set; }

        /// <summary>
        /// Cá nhân sử
        /// </summary>
        public string individualHis { get; set; }

        /// <summary>
        /// Tiền sử kinh nguyệt, hôn nhân và sinh đẻ
        /// </summary>
        public string mcObstetrical { get; set; }

        /// <summary>
        /// nhà tộc sử
        /// </summary>
        public string fhx { get; set; }

        /// <summary>
        /// Vọng chẩn (quan sát)
        /// </summary>
        public string inspection { get; set; }

        /// <summary>
        /// Văn chẩn (nghe/ngửi)
        /// </summary>
        public string smelling { get; set; }

        /// <summary>
        /// cắt khám
        /// </summary>
        public string palpation { get; set; }

        /// <summary>
        /// Điểm cần thiết của biện chứng
        /// </summary>
        public string syndrome { get; set; }

        /// <summary>
        /// thểô Kiểm tra
        /// </summary>
        public string psyCheck { get; set; }

        /// <summary>
        /// Bác sĩ xây nghị
        /// </summary>
        public string suggession { get; set; }

        /// <summary>
        /// Y lệnh Số hiệu
        /// </summary>
        public string recordId { get; set; }
    }
}