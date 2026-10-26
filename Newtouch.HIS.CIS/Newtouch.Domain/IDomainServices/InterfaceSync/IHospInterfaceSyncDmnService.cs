using Newtouch.Domain.DTO.InterfaceSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices.InterfaceSync
{
    /// <summary>
    /// Lớp giao diện thay thế backend
    /// </summary>
    public interface IHospInterfaceSyncDmnService
    {
        ResponseBase UpdateOccupyByCode(string cwcode, string bq, bool sfzy, string orgId, string user);
    }
}
