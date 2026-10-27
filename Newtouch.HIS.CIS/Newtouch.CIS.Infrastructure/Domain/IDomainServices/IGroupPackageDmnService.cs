using System.Collections.Generic;
using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IDomainServices
{
    public interface IGroupPackageDmnService
    {
        /// <summary>
        /// Bộ mẫuMục
        /// </summary>
        /// <param name="ztId"></param>
        GroupPackageDetailBO GetGroupPackageDetail(string ztId, string orgId);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="ztobj"></param>
        /// <param name="ztxmlist"></param>
        void SaveData(GroupPackageEntity ztobj, List<GroupPackageItemEntity> ztxmlist);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="ztId"></param>
        void DeleteData(string ztId);
    }
}
