using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicinApp
    {
        /// <summary>
        /// tra cứuthuốc(for dư ới kéo xuống)
        /// </summary>
        /// <param name="keywrod"></param>
        /// <returns></returns>
        IList<SysMedicinSimpleInfoVO> GetYpList(string keywrod);

    }
}
