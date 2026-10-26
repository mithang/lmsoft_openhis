namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.HLYYRequest
{
    public class EngDataStatusRes
    {
        public ResponseInfo Info { get; set; }
    }
    
    public class ResponseInfo
    {
        public string Rest { get; set; } // Thông tin trạng thái đơn thuốc
        public string Message { get; set; } // Thông báo
        public string Suggest { get; set; } // Đề xuất
        public string Username { get; set; } // Tên đăng nhập
        public int DispenseFlag { get; set; } // Cờ phát thuốc
    }
}