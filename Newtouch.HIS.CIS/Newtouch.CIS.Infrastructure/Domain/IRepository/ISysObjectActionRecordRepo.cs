using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.CIS.Repository;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Chungđối tượngThao tácghi chép
    /// </summary>
    public interface ISysObjectActionRecordRepo : IRepositoryBase<SysObjectActionRecordEntity>
    {
        /// <summary>
        /// Thêm mớiThao tácghi chép
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Add(SysObjectActionRecordEntity entity);

        /// <summary>
        /// TheophímLấyList
        /// </summary>
        /// <param name="objectKey"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysObjectActionRecordVO> GetListByKey(string objectKey, string orgId);

    }
}

