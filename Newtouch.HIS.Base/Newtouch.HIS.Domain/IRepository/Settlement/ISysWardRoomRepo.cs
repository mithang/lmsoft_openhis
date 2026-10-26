using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysWardRoomRepo : IRepositoryBase<SysWardRoomEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysWardRoomEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword);

        /// <summary>
        /// Cập nhật thông tin khu bệnh
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysWardRoomEntity entity, int? keyValue);

        ///// <summary>
        ///// Lấy danh sách tất cả các khu bệnh
        ///// </summary>
        //List<SysWardEntity> SelectWardList(string orgId);
    }
}
