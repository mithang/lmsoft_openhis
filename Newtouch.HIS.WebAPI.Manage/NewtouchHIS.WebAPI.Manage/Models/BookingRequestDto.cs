using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class BookingRequestDto : RequestBase
    {
        public string OrgId { get; set; }
        /// <summary>
        /// định danh duy nhất bệnh nhân
        /// </summary>
        public string patid { get; set; }
        /// <summary>
        /// số chứng minh nhân dân
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// số thẻ
        /// </summary>
        public string kh { get; set; }
        public string xm { get; set; }
        public DateTime? csrq { get; set; }
        public string xb { get; set; }
        public string dh { get; set; }
        public string dz { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }

        public string ksmc { get; set; }
        /// <summary>
        /// khoa
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// ngày khám theo hẹn
        /// </summary>
        public string outDate { get; set; }
        /// <summary>
        /// ngày khám bắt đầu
        /// </summary>
        public string FromoutDate { get; set; }
        public string ys { get; set; }
        /// <summary>
        /// loại ngoại trú chuyên gia thông thường, phó cao cấp, v.v.
        /// </summary>
        public string regType { get; set; }
        /// <summary>
        /// mz_ghpb_config  lịch trựcid
        /// </summary>
        public string ghpbId { get; set; }
        /// <summary>
        /// mz_ghpb_schedule lịch hẹn đã địnhId
        /// </summary>
        public string scheduId { get; set; }
        /// <summary>
        /// tính chất đăng ký khám tự trả, bảo hiểm y tế, v.v. brxz tương đương
        /// </summary>
        public string ghxz { get; set; }
        /// <summary>
        /// số hẹn
        /// </summary>
        public string bookId { get; set; }
        /// <summary>
        /// trạng thái hẹn ngoại trú
        /// </summary>
        public int? yyzt { get; set; }
        /// <summary>
        /// chi phí đã thanh toán
        /// </summary>
        public string PayFee { get; set; }
        /// <summary>
        /// số thứ tự thanh toán
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// thông tin liên hệ
        /// </summary>
        public string lxdh { get; set; }
        /// <summary>
        /// ngày bắt đầu
        /// </summary>
        public string ksrq { get; set; }
        /// <summary>
        /// ngày kết thúc
        /// </summary>
        public string jsrq { get; set; }
        /// <summary>
        /// ngày đăng ký khám
        /// </summary>
        public string ghrq { get; set; }
        /// <summary>
        /// định danh phụ nguồn đăng ký khám phân biệt bệnh nhân được đánh dấu khác nhau từ cùng một nguồn
        /// dùng cho khám sức khỏe
        /// </summary>
        public string ghlybz { get; set; }
    }
}
