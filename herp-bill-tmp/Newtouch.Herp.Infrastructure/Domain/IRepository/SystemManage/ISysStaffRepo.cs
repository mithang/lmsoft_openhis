using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Herp-local staff repository contract replacing the Framework binary
    /// ISysStaffRepo (no source). Only the member Herp code uses.
    /// </summary>
    public interface ISysStaffRepo
    {
        List<SysStaffVEntity> GetValidStaffListByOrganizeId(string organizeId);
    }
}
