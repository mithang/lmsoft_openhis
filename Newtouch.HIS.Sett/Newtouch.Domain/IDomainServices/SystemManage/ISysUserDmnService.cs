using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Liên quan người dùngDmnService
    /// </summary>
    public interface ISysUserExDmnService
    {
        /// <summary>
        /// TheoUserIdLấy bộ phận nhà thuốc người dùng hệ thống có thể thao tácCode List
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<string> GetYfbmCodeListByUserId(string userId, string orgId);

    }
}
