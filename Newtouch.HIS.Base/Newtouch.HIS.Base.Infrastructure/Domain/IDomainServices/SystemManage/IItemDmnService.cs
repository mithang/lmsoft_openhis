using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Từ điển,Mục từ điển DmnService
    /// </summary>
    public interface IItemDmnService
    {
        /// <summary>
        /// Lấy mục từ điển hiệu lực Danh sách
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId">Cơ sở y tế,nullChỉ tra cứu dùng chung</param>
        /// <returns></returns>
        IList<SysItemsDetailEntity> GetValidListByItemCode(string code,string keyword, string orgId = null);

    }
}
