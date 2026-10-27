using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    /// <summary>
    /// Điều kiện truy vấn giao diện truy vấn phát thuốc ngoại trú
    /// </summary>
    public class searchFyInfoReqVO
    {
        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime? begindate { get; set; }

        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime? enddate { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Từ khóa
        /// </summary>
        public string keyWord { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Loại
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
