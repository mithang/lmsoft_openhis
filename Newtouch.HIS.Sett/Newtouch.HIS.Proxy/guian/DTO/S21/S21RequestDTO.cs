using System.Collections.Generic;
using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S21
{
    /// <summary>
    /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành ngoại trú chi phí tải lên,quyết toán sau tải lên không hiệu lực bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S21")]
    [XmlRoot("body")]
    public class S21RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// Tải lênChi tiết
        /// </summary>
        public List<detail> list { get; set; }
    }

    /// <summary>
    /// Chi tiết
    /// </summary>
    public class detail
    {
        /// <summary>
        /// Đ ơn thuốc ngoại trú số thứ tự (liên tỉnh)
        /// </summary>
        public string clinicRxNo { get; set; }

        /// <summary>
        /// 开方bác sĩ(liên tỉnh)
        /// </summary>
        public string doctor { get; set; }

        /// <summary>
        /// mã hành chính (liên tỉnh)
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// Có phải bệnh nhân khám chữa bệnh liên tỉnh không1:Là0:Không(liên tỉnh)
        /// </summary>
        public string isTransProvincial { get; set; }

        /// <summary>
        /// Chi tiết tên tiếng trung 
        /// </summary>
        public string detailName { get; set; }

        /// <summary>
        /// mã hợp tác nông thôn 
        /// </summary>
        public string detailCode { get; set; }

        /// <summary>
        /// Hischi tiết thứ tự số (duy nhất số hiệu
        /// </summary>
        public string hisDetailCode { get; set; }

        /// <summary>
        /// Đ ịa phư ơng mã PDAcó thể tải lên mã hợp tác nông thôn 
        /// </summary>
        public string detailHosCode { get; set; }

        /// <summary>
        /// Loại mã(xem từ điển 目录loại)
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
        /// 经办 bệnh viện số thứ tự đ ơn thuốc duy nhất ID贵阳thành phố bệnh nhân必填(liên tỉnh)
        /// </summary>
        public string recipeID { get; set; }

    }

    /// <summary>
    /// Hoàn phí ngoại trú thông tin
    /// </summary>
    public class TFS21RequestDTO
    {
        /// <summary>
        /// S21Bản tin yêu cầu
        /// </summary>
        public S21RequestDTO S21RequestDto { get; set; }

        /// <summary>
        /// Hoàn tiềnTổng số tiền
        /// </summary>
        public decimal tkzje { get; set; }

        /// <summary>
        /// Lại lần nữaQuyết toánBHYTTổng số tiền
        /// </summary>
        public decimal ybzje { get; set; }

        /// <summary>
        /// Lại lần nữa quyết toán tự chi trả tổng số tiền
        /// </summary>
        public decimal zlzje { get; set; }
    }
}