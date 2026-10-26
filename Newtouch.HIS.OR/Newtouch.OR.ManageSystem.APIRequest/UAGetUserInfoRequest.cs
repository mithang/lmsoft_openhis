using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.API.Common;

namespace Newtouch.OR.ManageSystem.APIRequest
{
    /// <summary>
    /// Đổi mã truy cập lấy thông tin danh tính người dùng
    /// </summary>
    public class UAGetUserInfoRequest : RequestBase
    {
        /// <summary>
        /// Mã truy cập
        /// </summary>
        public string access_token { get; set; }
    }
}
