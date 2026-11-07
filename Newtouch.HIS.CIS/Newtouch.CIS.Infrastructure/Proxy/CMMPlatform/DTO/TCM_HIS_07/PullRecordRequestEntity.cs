using System.Xml.Serialization;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07
{
    /// <summary>
    /// nhắclấyđiện tử Hồ sơhưởngứngthể
    /// </summary>
    [XmlRoot("Request")]
    public class PullRecordRequestEntity : RequestBase
    {
        /// <summary>
        /// Hồ sơthông tinthể
        /// </summary>
        public Record Record { get; set; }
    }

    /// <summary>
    /// Hồ sơthông tinthể
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Khám bệnhdòngchảysố
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
        /// Bác sĩ Số hiệu(Người dùngtên)
        /// bắt buộc
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Bác sĩ Tên
        /// bắt buộc
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// chínhbảo
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
        /// Cá nhânsử
        /// </summary>
        public string individualHis { get; set; }

        /// <summary>
        /// Tiền sử kinh nguyệt, hôn nhân và sinh đẻ
        /// </summary>
        public string mcObstetrical { get; set; }

        /// <summary>
        /// nhàtộcsử
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
        /// cắtkhám
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
        /// Bác sĩxâynghị
        /// </summary>
        public string suggession { get; set; }

        /// <summary>
        /// Y lệnh Số hiệu
        /// </summary>
        public string recordId { get; set; }
    }
}