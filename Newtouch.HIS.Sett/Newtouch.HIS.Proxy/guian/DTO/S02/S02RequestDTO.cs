using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO.S02
{
    /// <summary>
    /// 客户端 truyền 入 ngư ời dùng 名 mật khẩu tiến hành thân phận 认证quay lại票据mã, mật khẩu cầnMD5 mã hóa 32 vị 大写 request dto
    /// </summary>
    [XmlRoot("body")]
    public class S02RequestDTO
    {
        /// <summary>
        /// ngư ời dùng 名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// mật khẩu 
        /// </summary>
        public string passWord { get; set; }
    }
}
