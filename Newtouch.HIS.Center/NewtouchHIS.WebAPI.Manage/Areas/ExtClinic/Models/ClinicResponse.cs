/**** Định nghĩa và  ApiManage Nhất quán với quy tắc ******/
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Areas.ExtClinic
{
    /// <summary>
    /// Đăng ký khám chữa bệnh Thông báo kết quả Request
    /// </summary>
    public class TreatedApplyResultRequest
    {
        /// <summary>
        /// His Đăng ký khám chữa bệnh Id
        /// </summary>
        public string ApplyId { get; set; }
        /// <summary>
        /// Bên thứ ba Đăng ký khám chữa bệnh Số giao dịch
        /// </summary>
        public string? Sqlsh { get; set; }
        /// <summary>
        /// Đăng ký khám chữa bệnh Trạng thái(HIS) EmunRemoteTreatedStu
        /// </summary>
        [Required(ErrorMessage = "Trạng thái đăng ký khám chữa bệnh không được để trống")]
        public int ApplyStu { get; set; }
        /// <summary>
        /// Bác sĩ đã xác nhận hay chưa 
        /// true Đăng ký Đạt, false Đăng ký Bác bỏ
        /// </summary>
        [Required(ErrorMessage = "Kết quả xác nhận của bác sĩ không được để trống")]
        public bool IsConfirm { get; set; }
        /// <summary>
        /// Cuộc họp Số
        /// </summary>
        public string roomid { get; set; }
    }
    /// <summary>
    /// Lấy phòng khám điện toán đám mây Bệnh nhân Bệnh án Request
    /// </summary>
    public class PatMedicalRecordRequest
    {
        /// <summary>
        /// His Đăng ký khám chữa bệnh Id
        /// </summary>
        [Required(ErrorMessage = "His Đăng ký khám chữa bệnh Id không được để trống")]
        public string ApplyId { get; set; }
        /// <summary>
        /// Bên thứ ba Đăng ký khám chữa bệnh Số giao dịch
        /// </summary>
        public string? Sqlsh { get; set; }
    }
    /// <summary>
    /// Lấy phòng khám điện toán đám mây Bệnh nhân Bệnh án Response
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
    /// Gửi tới bên thứ ba HIS Bệnh án
    /// </summary>
    public class SendPatMedicalRecordRequest: OutpMedicalRecordVO
    {
        public string ApplyId { get; set; }

        public string Sqlsh { get; set; }
    }
}
