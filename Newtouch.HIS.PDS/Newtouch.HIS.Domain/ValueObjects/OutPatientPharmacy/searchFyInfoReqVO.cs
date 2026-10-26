using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    /// <summary>
    /// Điều kiện truy vấn giao diện truy vấn phát thuốc ngoại trú
    /// </summary>
    public class searchFyInfoReqVO
    {
        /// <summary>
        /// thời gian bắt đầu
        /// </summary>
        public DateTime? begindate { get; set; }

        /// <summary>
        /// thời gian kết thúc
        /// </summary>
        public DateTime? enddate { get; set; }

        /// <summary>
        /// số thẻ
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// từ khóa
        /// </summary>
        public string keyWord { get; set; }

        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// loại
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
