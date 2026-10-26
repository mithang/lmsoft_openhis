using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class PreSettReq : BookingReqBaseDto
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "CardNo is required")]
        public string CardNo { get; set; }
        /// <summary>
        /// 0: Đ ăng ký khám 1:Đ ơn thuốc
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "FeeType is required")]
        public string FeeType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "TotalAmount is required")]
        public decimal TotalAmount { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "OrderNo is required")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 0:Tự trả 1:职工 2: cư dân 3: nghỉ hưu 11:BHYT thông thư ờng
        /// </summary>
        public string PatType { get; set; }
        public string Mzh { get; set; }
        /// <summary>
        /// số đ ặt lịch Đ ăng ký khám预结比 truyền 
        /// </summary>
        public int? BookID { get; set; }
        /// <summary>
        /// Có hay khôngkhi NgàyĐ ăng ký khám性质 Y:Là N:Không Đ ăng ký khám预结比 truyền 
        /// </summary>
        public string IsBooking { get; set; }
    }

    public class SettReq : BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "CardNo is required")]
        public string CardNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mzh is required")]
        public string Mzh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "TotalAmount is required")]
        public decimal TotalAmount { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "OrderNo is required")]
        public string OrderNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayLsh is required")]
        public string PayLsh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayFee is required")]
        public decimal PayFee { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayTime is required")]
        public DateTime PayTime { get; set; }

        public string PatType { get; set; }
    }
}
