using System;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class PrescriptionInfoReq
    {

    }

    public class PrescriptionInfoResp
    {
        public string PresId { get; set; }
        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        public string PresNo { get; set; }
        /// <summary>
        /// Đ ơn thuốc số tiền
        /// </summary>
        public decimal PresAmt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeptName { get; set; }
        public string DoctorName { get; set; }
        /// <summary>
        /// Loại đ ơn thuốc
        /// </summary>
        //public string PresType { get; set; }
        /// <summary>
        /// Mô tả loại đơn thuốc
        /// </summary>
        public string PresTypeName { get; set; }
        /// <summary>
        /// Thời gian lập
        /// </summary>
        public string PresTime { get; set; }
    }

    public class PrescriptionDetailResp
    {
        public string PresId { get; set; }
        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        public string PresNo { get; set; }
        public string PresItemId { get; set; }
        /// <summary>
        /// Tên hạng mục
        /// </summary>
        public string PresItemName { get; set; }
        /// <summary>
        /// Số lư ợng
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Đ ơn vị
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// Đ ơn giá
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal Amt { get; set; }
        /// <summary>
        /// Hạng mục kết hợp tên
        /// </summary>
        public string ItemGroupName { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        //public string Spec { get; set; }

        //public int? PresTypeName { get; set; }

    }
}
