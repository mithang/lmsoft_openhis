/**** Định nghĩa và  ApiManageNhất quán với quy tắc ******/
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Areas.ExtClinic
{
    /// <summary>
    /// Đăng ký khám chữa bệnhThông báo kết quả Request
    /// </summary>
    public class TreatedApplyResultRequest
    {
        /// <summary>
        /// His Đăng ký khám chữa bệnhId
        /// </summary>
        public string ApplyId { get; set; }
        /// <summary>
        /// Bên thứ baĐăng ký khám chữa bệnhSố giao dịch
        /// </summary>
        public string? Sqlsh { get; set; }
        /// <summary>
        /// Đăng ký khám chữa bệnhTrạng thái(HIS) EmunRemoteTreatedStu
        /// </summary>
        [Required(ErrorMessage = "Trạng thái đăng ký khám chữa bệnh không được để trống")]
        public int ApplyStu { get; set; }
        /// <summary>
        /// Bác sĩ đã xác nhận hay chưa 
        /// true Đăng kýĐạt, false Đăng kýBác bỏ
        /// </summary>
        [Required(ErrorMessage = "Kết quả xác nhận của bác sĩ không được để trống")]
        public bool IsConfirm { get; set; }
        /// <summary>
        /// Cuộc họpSố
        /// </summary>
        public string roomid { get; set; }
    }
    /// <summary>
    /// Lấy phòng khám điện toán đám mâyBệnh nhânBệnh án Request
    /// </summary>
    public class PatMedicalRecordRequest
    {
        /// <summary>
        /// His Đăng ký khám chữa bệnhId
        /// </summary>
        [Required(ErrorMessage = "His Đăng ký khám chữa bệnhId không được để trống")]
        public string ApplyId { get; set; }
        /// <summary>
        /// Bên thứ baĐăng ký khám chữa bệnhSố giao dịch
        /// </summary>
        public string? Sqlsh { get; set; }
    }
    /// <summary>
    /// Lấy phòng khám điện toán đám mâyBệnh nhânBệnh án Response
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
    /// Gửi tới bên thứ baHISBệnh án
    /// </summary>
    public class SendPatMedicalRecordRequest: OutpMedicalRecordVO
    {
        public string ApplyId { get; set; }

        public string Sqlsh { get; set; }
    }
}
