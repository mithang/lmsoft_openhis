using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Cách dùngliênđộng
    /// </summary>
    public interface IUsageLinkageApp
    {
        /// <summary>
        /// Gửi
        /// </summary>
        /// <param name="sysUsageLinkageEntity"></param>
        /// <returns></returns>
        string SubmitForm(SysUsageLinkageEntity sysUsageLinkageEntity);
    }
}