using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuxiliaryDictionaryRepo : IRepositoryBase<SysAuxiliaryDictionaryEntity>
    {
        /// <summary>
        /// Lấyhiện tạitổ chứcdướicủatất cảHiệu lựctừ điển
        /// </summary>
        /// <returns></returns>
        IList<SysAuxiliaryDictionaryEntity> GetValidListByOrg(string orgId, bool withCache = false);

        /// <summary>
        /// Lấytừ điểndanh sách
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
