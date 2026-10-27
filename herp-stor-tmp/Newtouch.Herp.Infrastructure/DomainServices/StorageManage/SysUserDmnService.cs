using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Staff lookup used by ProfitAndLossController.GetZRRList (originally a
    /// FrameworkBase binary service; only GetStaffListByOrg is used by this area).
    /// Defined here because the Core copy has no ISysUserDmnService yet; the
    /// class name matches the interface name so AddHerpServices registers it.
    /// Semantics reuse SysStaffRepo (valid staff of the org, BaseDb Sys_Staff).
    /// Limitation: SysStaffVEntity carries Id/gh/Name only, so the ProfitAndLoss
    /// responsible-person selector's py column renders empty (original entity
    /// had py; the repo/entity are outside this area's owned files).
    /// </summary>
    public interface ISysUserDmnService
    {
        List<SysStaffVEntity> GetStaffListByOrg(string organizeId);
    }
}

namespace Newtouch.Herp.DomainServices.StorageManage
{
    public class SysUserDmnService : Newtouch.Herp.Domain.IDomainServices.ISysUserDmnService
    {
        private readonly ISysStaffRepo _sysStaffRepo;

        public SysUserDmnService(ISysStaffRepo sysStaffRepo)
        {
            _sysStaffRepo = sysStaffRepo;
        }

        public List<SysStaffVEntity> GetStaffListByOrg(string organizeId)
        {
            return _sysStaffRepo.GetValidStaffListByOrganizeId(organizeId);
        }
    }
}
