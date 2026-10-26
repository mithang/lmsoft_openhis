using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{

    public class detail
    {
        /// <summary>
        /// Hạng mụcchi tiết thứ tự số 
        /// </summary>
        public string detailId { get; set; }
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// mã hợp tác nông thôn 
        /// </summary>
        public string detailCode { get; set; }
        /// <summary>
        /// Loại mã(xem từ điển danh mụcloại)
        /// </summary>
        public string typeCode { get; set; }
        /// <summary>
        /// Số lư ợng(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal num { get; set; }
        /// <summary>
        /// Đ ơn giá(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// Tổng giá(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal totalCost { get; set; }
        /// <summary>
        /// Ngày dùng thuốc(yyyy-mm-dd)
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// Bồi thư ờng tỷ lệ 
        /// </summary>
        public decimal scale { get; set; }
        /// <summary>
        /// BHYTPhạm vi
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// Đ ơn vị
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string standard { get; set; }
        /// <summary>
        /// Dạng bào chế
        /// </summary>
        public string formulations { get; set; }
    }
    
}