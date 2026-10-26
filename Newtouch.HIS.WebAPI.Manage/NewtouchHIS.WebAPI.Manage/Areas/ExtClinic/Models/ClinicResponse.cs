/**** định nghĩa và ApiManagequy tắc nhất quán ******/
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Areas.ExtClinic
{
    /// <summary>
    /// thông báo kết quả đăng ký khám chữa bệnh Request
    /// </summary>
    public class TreatedApplyResultRequest
    {
        /// <summary>
        /// His đăng ký khám chữa bệnhId
        /// </summary>
        public string ApplyId { get; set; }
        /// <summary>
        /// số thứ tự đăng ký khám chữa bệnh của bên thứ ba
        /// </summary>
        public string? Sqlsh { get; set; }
        /// <summary>
        /// trạng thái đăng ký khám chữa bệnh（HIS） EmunRemoteTreatedStu
        /// </summary>
        [Required(ErrorMessage = "诊疗申请状态不可为空")]
        public int ApplyStu { get; set; }
        /// <summary>
        /// bác sĩ đã xác nhận hay chưa 
        /// true đăng ký đạt， false đăng ký bị bác
        /// </summary>
        [Required(ErrorMessage = "医生确认结果不可为空")]
        public bool IsConfirm { get; set; }
        /// <summary>
        /// số phòng họp
        /// </summary>
        public string roomid { get; set; }
    }
    /// <summary>
    /// lấy bệnh án bệnh nhân phòng khám trên đám mây Request
    /// </summary>
    public class PatMedicalRecordRequest
    {
        /// <summary>
        /// His đăng ký khám chữa bệnhId
        /// </summary>
        [Required(ErrorMessage = "His 诊疗申请Id不可为空")]
        public string ApplyId { get; set; }
        /// <summary>
        /// số thứ tự đăng ký khám chữa bệnh của bên thứ ba
        /// </summary>
        public string? Sqlsh { get; set; }
    }
    /// <summary>
    /// lấy bệnh án bệnh nhân phòng khám trên đám mây Response
    /// </summary>
    public class PatMedicalRecordResponse : OutpMedicalRecordVO
    {
        public string patid { get; set; }
        /// <summary>
        /// họ tên bệnh nhân
        /// </summary>
        public string xm { get; set; }
    }
    /// <summary>
    /// gửi tới bên thứ baHISbệnh án
    /// </summary>
    public class SendPatMedicalRecordRequest: OutpMedicalRecordVO
    {
        public string ApplyId { get; set; }

        public string Sqlsh { get; set; }
    }
}
