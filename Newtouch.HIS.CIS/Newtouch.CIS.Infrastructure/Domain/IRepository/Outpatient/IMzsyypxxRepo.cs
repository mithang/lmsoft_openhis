using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Ngoại trútruyền dịchThuốcthông tin
    /// </summary>
    public interface IMzsyypxxRepo : IRepositoryBase<MzsyypxxEntity>
    {
        void Exec(List<long> syIds,string OrganizeId);
        void CanCelExec(List<long> syIds, string OrganizeId);
    }
}
