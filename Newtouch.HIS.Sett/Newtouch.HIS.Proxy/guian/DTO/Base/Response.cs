using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    /// <summary>
    /// response
    /// </summary>
    [XmlRoot("result")]
    public class Response<T>
    {
        /// <summary>
        /// giao dịch kết quả trạng tháitrue thành côngfalse thất bại, bắt buộc 
        /// </summary>
        public bool state { get; set; }

        /// <summary>
        /// Nguyên nhân thất bại
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public T data { get; set; }
    }
}