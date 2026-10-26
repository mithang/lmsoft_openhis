using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Hệ thống bệnh nhân tài khoản liên quan 
    /// </summary>
    public class SysPatAccountDmnService : DmnServiceBase, ISysPatAccountDmnService
    {
        public SysPatAccountDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


    }
}
