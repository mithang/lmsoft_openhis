using System.Collections.Generic;

namespace Newtouch.HIS.Proxy.guian.DTO.S21
{
    /// <summary>
    /// Tra cứu chi tiết chi phí ngoại trú lần này đ ã tải lên bản tin trả về
    /// </summary>
    public class S21ResponseDTO
    {
        /// <summary>
        /// lần này hoàn thành giao dịch củachi tiết dữ liệu lư ợng 
        /// </summary>
        public decimal num { get; set; }

        /// <summary>
        /// Chi tiết
        /// </summary>
        public List<item> list { get; set; }
    }

    /// <summary>
    /// Hạng mục
    /// </summary>
    public class item
    {
        /// <summary>
        /// Đ ơn thuốc ngoại trú số thứ tự 
        /// </summary>
        public string detailNo  { get; set; }

        /// <summary>
        /// Hischi tiết thứ tự số (duy nhất số hiệu)
        /// </summary>
        public string hisDetailCode { get; set; }

        /// <summary>
        /// Chi tiết tên tiếng trung 
        /// </summary>
        public string detailName { get; set; }

    }
}