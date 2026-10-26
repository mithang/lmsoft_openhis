namespace Newtouch.HIS.Proxy.guian.DTO.S22
{
    /// <summary>
    /// Tra cứu chi tiết chi phí ngoại trú lần này đ ã tải lên bản tin trả về
    /// </summary>
    public class item
    {
        /// <summary>
        /// đ ịa Quận mã 
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// tham gia hợp tác năm 
        /// </summary>
        public string year { get; set; }

        /// <summary>
        /// Bệnh nhân thuộc về huyện Quận số hiệu
        /// </summary>
        public string countryCode { get; set; }

        /// <summary>
        /// Đ ơn thuốc ngoại trú số thứ tự 
        /// </summary>
        public string detailNo { get; set; }

        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        ///  Chi tiếtID
        /// </summary>
        public string detailId { get; set; }

        /// <summary>
        /// mã hợp tác nông thôn 
        /// </summary>
        public string detailCode { get; set; }

        /// <summary>
        /// Loại mã(xem từ điển 目录loại)
        /// </summary>
        public string typeCode { get; set; }

        /// <summary>
        /// Hạng mụcLoạiTên
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Chi tiết tên tiếng trung 
        /// </summary>
        public string detailName { get; set; }

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
        ///  Ngày dùng thuốc(yyyy-mm-dd)
        /// </summary>
        public string date { get; set; }

        /// <summary>
        ///  Quy cách
        /// </summary>
        public string standard { get; set; }

        /// <summary>
        ///  Dạng bào chế
        /// </summary>
        public string formulations { get; set; }

        /// <summary>
        /// 保 trong hoặc保 trong (1-保 trong,0 保 ngoài)
        /// </summary>
        public string flag { get; set; }

        /// <summary>
        /// tỷ lệ thanh toán BHYT 
        /// </summary>
        public decimal proportion { get; set; }

        /// <summary>
        /// Có hay không trong 药
        /// </summary>
        public string isCm { get; set; }

        /// <summary>
        /// 限制 giá trị 
        /// </summary>
        public string limitValue { get; set; }

        /// <summary>
        /// Có hay không cần phê duyệt 
        /// </summary>
        public string isAudit { get; set; }

        /// <summary>
        /// 入 kho ngày 
        /// </summary>
        public string storageTime { get; set; }

        /// <summary>
        /// Có hay khôngSửa
        /// </summary>
        public string isModify { get; set; }

        /// <summary>
        /// bệnh viện sử dụng tên
        /// </summary>
        public string detailLocalName { get; set; }
    }
}