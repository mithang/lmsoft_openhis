using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Clinic
{
    public class MeetingResponseVO
    {
        /// <summary>
        /// dùng tài khoản âm thanh/hình ảnh để Secret dùng làm khóa bí mật để tiến hành AES Kết quả sau khi mã hóa đối xứng,Cần dùng khi gọi dịch vụ âm thanh/hình ảnh,Vui lòng giải mã rồi sử dụng
        /// </summary>
        public string rtcuserid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        public string username { get; set; }
        public string roompath { get; set; }
        public string organization { get; set; }
        public string device { get; set; }
    }
}
