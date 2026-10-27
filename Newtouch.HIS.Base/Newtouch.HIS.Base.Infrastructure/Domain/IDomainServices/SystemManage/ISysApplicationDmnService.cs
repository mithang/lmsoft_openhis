using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysApplicationDmnService
    {
        /// <summary>
        /// Lấy danh sách tổ chức đã được ủy quyền cho ứng dụng
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        IList<SysOrganizeEntity> GetAuthedOrgListByAppId(string appId);

        /// <summary>
        /// Ủy quyền ứng dụng cho tổ chức được chỉ định
        /// </summary>
        /// <param name="appId"></param>
        void UpdateAuthOrganizeList(string appId, string orgList, string curUserCode);

        /// <summary>
        /// Ủy quyền ứng dụng cho tất cả tổ chức
        /// </summary>
        /// <param name="appId"></param>
        void AuthAllOrganize(string appId, string curUserCode);

        /// <summary>
        /// Hủy tất cả ủy quyền（Tổ chức）
        /// </summary>
        /// <param name="appId"></param>
        void AuthCancelAllOrganize(string appId);

    }
}
