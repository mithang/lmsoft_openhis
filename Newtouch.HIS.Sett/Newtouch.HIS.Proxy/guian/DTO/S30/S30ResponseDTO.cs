namespace Newtouch.HIS.Proxy.guian.DTO.S30
{
    /// <summary>
    /// Tải thông tin từ điển bệnh theo dấu thời gian Bản tin trả về
    /// </summary>
    public class item
    {
        /// <summary>
        /// bệnh tên tiếng trung 
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// bệnh mã 
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Hạng mụcLoại
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string specification { get; set; }
        /// <summary>
        /// Dạng bào chế
        /// </summary>
        public string dosageForm { get; set; }
        /// <summary>
        /// Đ ơn vị
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// Sử dụng cấp bậc 
        /// </summary>
        public string rank { get; set; }
        /// <summary>
        /// Bồi thư ờng tỷ lệ 
        /// </summary>
        public string scale { get; set; }
        /// <summary>
        /// BHYTphạm vi1保 trong 0保 ngoài 
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// Có hay không基药
        /// </summary>
        public string isBase { get; set; }
        /// <summary>
        /// Phiên âm mã 
        /// </summary>
        public string pcode { get; set; }
        /// <summary>
        /// Giá(Tỉnh)
        /// </summary>
        public string price5 { get; set; }
        /// <summary>
        /// Giá(Thành phố)
        /// </summary>
        public string price4 { get; set; }
        /// <summary>
        /// Giá(Huyện)
        /// </summary>
        public string price3 { get; set; }
        /// <summary>
        /// Giá(乡)
        /// </summary>
        public string price2 { get; set; }
        /// <summary>
        /// Giá(村)
        /// </summary>
        public string price1 { get; set; }
        /// <summary>
        /// giá trần (tỉnh)
        /// </summary>
        public string limitPrice5 { get; set; }
        /// <summary>
        /// giá trần (thành phố)
        /// </summary>
        public string limitPrice4 { get; set; }
        /// <summary>
        /// giá trần (huyện)
        /// </summary>
        public string limitPrice3 { get; set; }
        /// <summary>
        /// giá trần (乡)
        /// </summary>
        public string limitPrice2 { get; set; }
        /// <summary>
        /// giá trần (村)
        /// </summary>
        public string limitPrice1 { get; set; }
        /// <summary>
        /// 目录 cấp bậc 
        /// </summary>
        public string grade { get; set; }
        /// <summary>
        /// Trạng thái0 chưa kiểm tra 1Đ ã kiểm tra 2 chưa thông qua 
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Thời gian戳
        /// </summary>
        public string datetime { get; set; }
    }
}