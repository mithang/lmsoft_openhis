using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Thông tin thuốc truyền dịch ngoại trú
    /// </summary>
    public interface IMzsyypxxRepo : IRepositoryBase<MzsyypxxEntity>
    {
        void Exec(List<long> syIds,string OrganizeId);
        void CanCelExec(List<long> syIds, string OrganizeId);
    }
}
