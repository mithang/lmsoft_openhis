using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Từ điển, mục từ điển DmnService
    /// </summary>
    public interface IItemDmnService
    {
        /// <summary>
        /// Lấy danh sách mục từ điển có hiệu lực
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId">Cơ sở y tế, null chỉ tra cứu các mục dùng chung</param>
        /// <returns></returns>
        IList<SysItemsDetailEntity> GetValidListByItemCode(string code,string keyword, string orgId = null);

    }
}
