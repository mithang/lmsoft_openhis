using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.ValueObjects.AppManage
{
    /// <summary>
    /// Quan hệ ứng dụng đã được ủy quyền
    /// </summary>
    public class AppFriendAuthInfoVo : AuthAppVO
    {
        public string? FriendId { get; set; }
        public string? FriendAppId { get; set; }
        public string? FriendAppName { get; set; }
        public string? AuthLevs { get; set; }
        public List<AppModuleVO>? FriendModules { get; set; }

    }
    /// <summary>
    /// Thông tin mô-đun ứng dụng
    /// </summary>
    public class AppModuleVO
    {
        public int? ModuType { get; set; }
        public string? ModuName { get; set; }
        public string? ModuDesc { get; set; }
        public string? AuthAppId { get; set; }
        public string? ModuPath { get; set; }
        public int? ModuLev { get; set; }
        public string? AuthLevs { get; set; }
    }
}
