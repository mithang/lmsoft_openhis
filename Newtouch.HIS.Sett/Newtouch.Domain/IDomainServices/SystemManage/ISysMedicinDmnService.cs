using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicinDmnService
    {
        /// <summary>
        /// Tìm kiếm thuốc(for Thả xuống)
        /// </summary>
        /// <param name="keywrod"></param>
        /// <returns></returns>
        IList<SysMedicinSimpleInfoVO> GetYpList(string keywrod);

    }
}
