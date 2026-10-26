using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07
{
    /// <summary>
    /// Thân phản hồi trích xuất bệnh án điện tử
    /// </summary>
    [XmlRoot("Request")]
    public class PullRecordRequestEntity : RequestBase
    {
        /// <summary>
        /// Thân thông tin bệnh án
        /// </summary>
        public Record Record { get; set; }
    }

    /// <summary>
    /// Thân thông tin bệnh án
    /// </summary>
    public class Record
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
        /// Mã bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string patiId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Tuổi bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// Giới tính bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// Ngày khám -YYYYMMDD
        /// Bắt buộc
        /// </summary>
        public string clinicDate { get; set; }

        /// <summary>
        /// Mã bác sĩ (tên đăng nhập)
        /// Bắt buộc
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Tên bác sĩ
        /// Bắt buộc
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// Triệu chứng chính
        /// </summary>
        public string priDepict { get; set; }

        /// <summary>
        /// localDisHis
        /// </summary>
        public string localDisHis { get; set; }

        /// <summary>
        /// Tiền sử dị ứng
        /// </summary>
        public string allergicHis { get; set; }

        /// <summary>
        /// Tiền sử
        /// </summary>
        public string anamnesis { get; set; }

        /// <summary>
        /// Tiền sử cá nhân
        /// </summary>
        public string individualHis { get; set; }

        /// <summary>
        /// Tiền sử kinh nguyệt và sinh đẻ
        /// </summary>
        public string mcObstetrical { get; set; }

        /// <summary>
        /// Tiền sử gia đình
        /// </summary>
        public string fhx { get; set; }

        /// <summary>
        /// Vọng chẩn
        /// </summary>
        public string inspection { get; set; }

        /// <summary>
        /// Văn chẩn
        /// </summary>
        public string smelling { get; set; }

        /// <summary>
        /// Thiết chẩn
        /// </summary>
        public string palpation { get; set; }

        /// <summary>
        /// Điểm biện chứng
        /// </summary>
        public string syndrome { get; set; }

        /// <summary>
        /// Khám thực thể
        /// </summary>
        public string psyCheck { get; set; }

        /// <summary>
        /// Đề xuất của bác sĩ
        /// </summary>
        public string suggession { get; set; }

        /// <summary>
        /// Mã y lệnh
        /// </summary>
        public string recordId { get; set; }
    }
}