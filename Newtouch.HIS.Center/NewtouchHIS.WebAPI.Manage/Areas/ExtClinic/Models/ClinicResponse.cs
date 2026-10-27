/**** 定义 và  ApiManage规则一致 ******/
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Areas.ExtClinic
{
    /// <summary>
    /// Đăng ký khám chữa bệnh结果通知 Request
    /// </summary>
    public class TreatedApplyResultRequest
    {
        /// <summary>
        /// His Đăng ký khám chữa bệnhId
        /// </summary>
        public string ApplyId { get; set; }
        /// <summary>
        /// 第三方Đăng ký khám chữa bệnh流水号
        /// </summary>
        public string? Sqlsh { get; set; }
        /// <summary>
        /// Đăng ký khám chữa bệnh状态（HIS） EmunRemoteTreatedStu
        /// </summary>
        [Required(ErrorMessage = "Trạng thái đăng ký khám chữa bệnh không được để trống")]
        public int ApplyStu { get; set; }
        /// <summary>
        /// 医生是否已确认 
        /// true Đăng ký通过， false Đăng ký驳回
        /// </summary>
        [Required(ErrorMessage = "Kết quả xác nhận của bác sĩ không được để trống")]
        public bool IsConfirm { get; set; }
        /// <summary>
        /// Cuộc họp号
        /// </summary>
        public string roomid { get; set; }
    }
    /// <summary>
    /// 获取云诊所Bệnh nhân病历 Request
    /// </summary>
    public class PatMedicalRecordRequest
    {
        /// <summary>
        /// His Đăng ký khám chữa bệnhId
        /// </summary>
        [Required(ErrorMessage = "His Đăng ký khám chữa bệnhId không được để trống")]
        public string ApplyId { get; set; }
        /// <summary>
        /// 第三方Đăng ký khám chữa bệnh流水号
        /// </summary>
        public string? Sqlsh { get; set; }
    }
    /// <summary>
    /// 获取云诊所Bệnh nhân病历 Response
    /// </summary>
    public class PatMedicalRecordResponse : OutpMedicalRecordVO
    {
        public string patid { get; set; }
        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string xm { get; set; }
    }
    /// <summary>
    /// 向第三方发送HIS病历
    /// </summary>
    public class SendPatMedicalRecordRequest: OutpMedicalRecordVO
    {
        public string ApplyId { get; set; }

        public string Sqlsh { get; set; }
    }
}
