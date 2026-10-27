using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Ported from binary FrameworkBase SystemManage ISysDepartmentRepo.
    /// Surface derived from CIS Web usage only (ClientsData department list).
    /// The flag argument is accepted for signature compatibility; the
    /// implementation returns all valid departments of the org.
    /// </summary>
    public interface ISysDepartmentRepo
    {
        IList<SysDepartmentEntity> GetList(string orgId, string flag, string keyword = null);
    }
}
