using Newtouch.HIS.Proxy.Attribute;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S10")]
    [XmlRoot("body")]
    public class S10RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// 开方bác sĩ(liên tỉnh 
        /// </summary>
        public string doctor { get; set; }
        /// <summary>
        /// Số thẻ chứng nhận y tế(liên tỉnh)
        /// </summary>
        public string bookNo { get; set; }
        /// <summary>
        /// Tên bệnh nhân(liên tỉnh)
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Số hộ gia đ ình(liên tỉnh)
        /// </summary>
        public string familyNo { get; set; }
        /// <summary>
        /// 个人số hiệu(liên tỉnh)
        /// </summary>
        public string memberNo { get; set; }
        /// <summary>
        /// lần này tải lênchi tiết dòng(liên tỉnh)
        /// </summary>
        public string rows { get; set; }
        /// <summary>
        /// mã hành chính (liên tỉnh)
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// Có phải bệnh nhân khám chữa bệnh liên tỉnh không1:Là0:Không(liên tỉnh)
        /// </summary>
        public string isTransProvincial { get; set; }

        [XmlArrayItem("detail")]
        public List<S10detail> list { get; set; }
    }

    public class S10detail
    {
        /// <summary>
        /// Chi tiết tên tiếng trung 
        /// </summary>
        public string detailName { get; set; }
        /// <summary>
        /// mã hợp tác nông thôn 
        /// </summary>
        public string detailCode { get; set; }
        /// <summary>
        /// Hischi tiết thứ tự số (duy nhất số hiệu)
        /// </summary>
        public string hisDetailCode { get; set; }
        /// <summary>
        /// Hạng mục số hiệu bệnh viện mã 
        /// </summary>
        public string detailHosCode { get; set; }
        /// <summary>
        /// Loại mã(xem từ điển 目录loại) liên tỉnh quyết toán truyền 00
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
        /// <summary>
        /// 经办 bệnh viện số thứ tự đ ơn thuốc duy nhất ID 贵阳thành phố bệnh nhân必填(liên tỉnh)
        /// </summary>
        public string recipeID { get; set; }
    }
}