using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.Entity.Settlement;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysWardRepo : IRepositoryBase<SysWardEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysWardEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword);

        IList<Getcz> Selectzccx(string organizeId, int xz);


        /// <summary>
        /// Cập nhật thông tin khu bệnh
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysWardEntity entity, int? keyValue);

        /// <summary>
        /// Lấy danh sách tất cả khu bệnh
        /// </summary>
        List<SysWardEntity> SelectWardList(string orgId);
    }
}
