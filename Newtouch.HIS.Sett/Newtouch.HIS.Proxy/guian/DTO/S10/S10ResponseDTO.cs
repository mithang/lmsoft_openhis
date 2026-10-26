using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [XmlRoot("list")]
    public class S10ResponseDTO
    {
        [XmlArrayItem("item")]
        public List<S10item> list { get; set; }
    }

    public class S10item
    {
        /// <summary>
        /// Nhập viện số thứ tự đ ơn thuốc
        /// </summary>
        public decimal? detailNo { get; set; }
        /// <summary>
        /// Hischi tiết thứ tự số 
        /// </summary>
        public string hisDetailCode { get; set; }
        /// <summary>
        /// Chi tiết tên tiếng trung 
        /// </summary>
        public string detailName { get; set; }
        /// <summary>
        /// Thuốc có thể thanh toán tỷ lệ 
        /// </summary>
        public string enableRatio { get; set; }
        /// <summary>
        /// ThuốcCó thể thanh toánSố tiền
        /// </summary>
        public string enableMoney { get; set; }
        /// <summary>
        /// Có hay không là cơ bản thuốc0 không là1 là
        /// </summary>
        public string essentialMedicine { get; set; }
        /// <summary>
        /// Có hay không là trong thuốc 0 không là 1 là
        /// </summary>
        public string chineseMedicine { get; set; }
        /// <summary>
        /// 1:Thành công 0:Thất bại
        /// </summary>
        public string success { get; set; }
        /// <summary>
        /// Thất bạithông báo
        /// </summary>
        public string message { get; set; }
    }
}