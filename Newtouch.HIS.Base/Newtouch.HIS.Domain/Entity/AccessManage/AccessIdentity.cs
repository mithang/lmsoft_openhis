using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity
{
    public class AccessIdentity
    {
        public string RegCode { get; set; }
        /// <summary>
        /// Tên đăng ký
        /// </summary>
        public string RegName { get; set; }

        /// <summary>
        /// Cấp độ quyền AuthorizedLevEnum
        /// </summary>
        public string AuthorizedLev { get; set; }
        /// <summary>
        /// Thời hạn ủy quyền AuthorizedPeriod
        /// </summary>
        public string AuthorizedPeriod { get; set; }
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// Khóa bí mật
        /// </summary>
        public string accesskey { get; set; }
        public List<AccessAuth> authlist { get; set; }
        //
        // Tóm tắt:
        //     Id ứng dụng
        public string AppId { get; set; }
        //
        // Tóm tắt:
        //     Loại token (loại người dùng)
        public string TokenType { get; set; }
        //
        // Tóm tắt:
        //     Id người dùng
        public string UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        //
        // Tóm tắt:
        //     Tài khoản người dùng
        public string Account { get; set; }
        public string OrganizeId { get; set; }
    }
}
