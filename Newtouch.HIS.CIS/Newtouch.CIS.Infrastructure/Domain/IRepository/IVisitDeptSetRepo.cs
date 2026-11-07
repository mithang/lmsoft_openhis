using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// rakhám Cài đặt
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
        /// Theo ID vật lý Xóa ghi chép
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteItem(string id);
    }
}
