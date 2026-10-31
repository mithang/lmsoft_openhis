using System.Xml.Serialization;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07
{
    /// <summary>
    /// nhắclấyđiện tửHồ sơhưởngứngthể
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
        /// Cơ sởMã
        /// bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Bệnh nhânSố hiệu
        /// bắt buộc
        /// </summary>
        public string patiId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// bắt buộc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Bệnh nhânTuổi
        /// bắt buộc
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// Bệnh nhânGiới tính
        /// bắt buộc
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// Khám bệnhNgày-YYYYMMDD
        /// bắt buộc
        /// </summary>
        public string clinicDate { get; set; }

        /// <summary>
        /// Bác sĩSố hiệu（Người dùngtên）
        /// bắt buộc
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Bác sĩTên
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
        /// quaTiền sử dị ứng
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
        /// thểôKiểm tra
        /// </summary>
        public string psyCheck { get; set; }

        /// <summary>
        /// Bác sĩxâynghị
        /// </summary>
        public string suggession { get; set; }

        /// <summary>
        /// Y lệnhSố hiệu
        /// </summary>
        public string recordId { get; set; }
    }
}