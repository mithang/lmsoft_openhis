using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Cài đặt khám bệnh
    /// </summary>
    public interface IVisitDeptSetRepo : IRepositoryBase<VisitDeptSetEntity>
    {

        /// <summary>
        /// Lấy thông tin khám bệnh
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VisitDeptSetEntity> SelectData(string ysgh, string organizeId);

        /// <summary>
        /// TheoIDXóa vật lý hồ sơ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteItem(string id);
    }
}