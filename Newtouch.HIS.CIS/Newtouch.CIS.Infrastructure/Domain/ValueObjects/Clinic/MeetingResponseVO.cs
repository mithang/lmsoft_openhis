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
        /// Tài khoản âm thanh/hình ảnh; Secret là khóa bí mật, kết quả trả về của lệnh gọi được mã hóa AES; khi gọi dịch vụ âm thanh/hình ảnh cần dùng đến, vui lòng giải mã rồi sử dụng
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
