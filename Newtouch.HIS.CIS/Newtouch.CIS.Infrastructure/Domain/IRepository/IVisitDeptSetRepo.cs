using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// rakhámCài đặt
    /// </summary>
    public interface IVisitDeptSetRepo : IRepositoryBase<VisitDeptSetEntity>
    {

        /// <summary>
        /// Lấyrakhámthông tin
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VisitDeptSetEntity> SelectData(string ysgh, string organizeId);

        /// <summary>
        /// TheoIDvậtlýXóaghi chép
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteItem(string id);
    }
}
