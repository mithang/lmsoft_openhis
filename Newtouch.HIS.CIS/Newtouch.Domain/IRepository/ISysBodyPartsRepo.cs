using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    public interface ISysBodyPartsRepo : IRepositoryBase<SysBodyPartsEntity>
    {
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <returns></returns>
        List<SysBodyPartsEntity> GetListByOrg(string orgId);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysBodyPartsEntity entity, string keyValue);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);
    }
}
