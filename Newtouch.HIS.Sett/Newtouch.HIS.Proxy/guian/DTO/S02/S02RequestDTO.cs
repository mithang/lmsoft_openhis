using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO.S02
{
    /// <summary>
    /// máy khách truyền vào ngư ời dùng tên mật khẩu tiến hành thân phận xác thựcquay lạichứng từmã, mật khẩu cầnMD5 mã hóa 32 vị chữ hoa request dto
    /// </summary>
    [XmlRoot("body")]
    public class S02RequestDTO
    {
        /// <summary>
        /// ngư ời dùng tên
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// mật khẩu 
        /// </summary>
        public string passWord { get; set; }
    }
}
