using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.HLYYRequest
{
    public class EngDataStatusRes
    {
        public ResponseInfo Info { get; set; }
    }
    
    public class ResponseInfo
    {
        public string Rest { get; set; } // Đơn thuốcTrạng tháithông tin
        public string Message { get; set; } // hủytin
        public string Suggest { get; set; } // xâynghị
        public string Username { get; set; } // Người dùngtên
        public int DispenseFlag { get; set; } // phốithuốcCờ
    }
}