using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.CIS.Repository;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Chungđối tượng Thao tácghi chép
    /// </summary>
    public interface ISysObjectActionRecordRepo : IRepositoryBase<SysObjectActionRecordEntity>
    {
        /// <summary>
        /// Thêm mới Thao tácghi chép
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Add(SysObjectActionRecordEntity entity);

        /// <summary>
        /// Theophím Lấy List
        /// </summary>
        /// <param name="objectKey"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysObjectActionRecordVO> GetListByKey(string objectKey, string orgId);

    }
}

