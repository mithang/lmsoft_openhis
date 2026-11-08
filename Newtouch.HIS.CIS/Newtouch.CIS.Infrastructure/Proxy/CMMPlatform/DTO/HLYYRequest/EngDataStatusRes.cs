using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.HLYYRequest
{
    public class EngDataStatusRes
    {
        public ResponseInfo Info { get; set; }
    }
    
    public class ResponseInfo
    {
        public string Rest { get; set; } // Đơn thuốc Trạng thái thông tin
        public string Message { get; set; } // hủy tin
        public string Suggest { get; set; } // xây nghị
        public string Username { get; set; } // Người dùng tên
        public int DispenseFlag { get; set; } // phối thuốc Cờ
    }
}