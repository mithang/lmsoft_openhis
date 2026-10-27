using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class BookingRequestDto : RequestBase
    {
        public string OrgId { get; set; }
        /// <summary>
        /// Bệnh nhânMã định danh duy nhất
        /// </summary>
        public string patid { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        public string xm { get; set; }
        public DateTime? csrq { get; set; }
        public string xb { get; set; }
        public string dh { get; set; }
        public string dz { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string mzh { get; set; }

        public string ksmc { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// Ngày khám theo hẹn
        /// </summary>
        public string outDate { get; set; }
        /// <summary>
        /// Ngày khám bắt đầu
        /// </summary>
        public string FromoutDate { get; set; }
        public string ys { get; set; }
        /// <summary>
        /// Loại ngoại trú Thông thường, chuyên gia, phó cao cấp v.v.
        /// </summary>
        public string regType { get; set; }
        /// <summary>
        /// mz_ghpb_config  Lịch trựcid
        /// </summary>
        public string ghpbId { get; set; }
        /// <summary>
        /// mz_ghpb_schedule Lịch trình đã định của đặt hẹnId
        /// </summary>
        public string scheduId { get; set; }
        /// <summary>
        /// Tính chất đăng ký khám Tự chi trả, BHYT v.v. brxz Tương đương
        /// </summary>
        public string ghxz { get; set; }
        /// <summary>
        /// Số đặt hẹn
        /// </summary>
        public string bookId { get; set; }
        /// <summary>
        /// Trạng thái đặt hẹn ngoại trú
        /// </summary>
        public int? yyzt { get; set; }
        /// <summary>
        /// Chi phí đã thanh toán
        /// </summary>
        public string PayFee { get; set; }
        /// <summary>
        /// Số giao dịch thanh toán
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// Phương thức liên hệ
        /// </summary>
        public string lxdh { get; set; }
        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public string ksrq { get; set; }
        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public string jsrq { get; set; }
        /// <summary>
        /// Ngày đăng ký khám
        /// </summary>
        public string ghrq { get; set; }
        /// <summary>
        /// Mã phụ trợ nguồn đăng ký khám Phân biệt dấu hiệu khác nhau của cùng một nguồnBệnh nhân
        /// Dùng cho khám sức khỏe
        /// </summary>
        public string ghlybz { get; set; }
    }
}
