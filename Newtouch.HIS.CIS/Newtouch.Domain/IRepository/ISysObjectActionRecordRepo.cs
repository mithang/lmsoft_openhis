using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Ghi nhận thao tác đối tượng chung
    /// </summary>
    public interface ISysObjectActionRecordRepo : IRepositoryBase<SysObjectActionRecordEntity>
    {
        /// <summary>
        /// Thêm ghi nhận thao tác
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Add(SysObjectActionRecordEntity entity);

        /// <summary>
        /// Lấy theo khóaList
        /// </summary>
        /// <param name="objectKey"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysObjectActionRecordVO> GetListByKey(string objectKey, string orgId);

    }
}

