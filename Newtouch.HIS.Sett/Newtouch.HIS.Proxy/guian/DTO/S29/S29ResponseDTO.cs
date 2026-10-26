namespace Newtouch.HIS.Proxy.guian.DTO.S29
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
        /// Thời gian戳
        /// </summary>
        public string date { get; set; }
    }
}