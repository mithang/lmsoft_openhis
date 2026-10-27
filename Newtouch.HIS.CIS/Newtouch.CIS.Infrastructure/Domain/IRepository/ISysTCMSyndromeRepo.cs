using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysTCMSyndromeRepo
    {
        IList<SysTCMSyndromeVEntity> GetList(string orgId, string keyword);
    }
}
