using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuxiliaryDictionaryRepo : IRepositoryBase<SysAuxiliaryDictionaryEntity>
    {
        /// <summary>
        /// Lấy tất cả từ điển có hiệu lực thuộc tổ chức hiện tại
        /// </summary>
        /// <returns></returns>
        IList<SysAuxiliaryDictionaryEntity> GetValidListByOrg(string orgId, bool withCache = false);

        /// <summary>
        /// Lấy danh sách từ điển
        /// </summary>
        /// <returns></returns>
        List<SysAuxiliaryDictionaryEntity> GetListByOrg(string orgId);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysAuxiliaryDictionaryEntity entity, string keyValue);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgId"></param>
        void DeleteForm(string keyValue);

    }
}
