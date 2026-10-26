using FrameworkBase.MultiOrg.Web;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// Hồ sơ thao tác đối tượng chung
    /// </summary>
    public class SysObjectActionRecordController : OrgControllerBase
    {
        private readonly ISysObjectActionRecordRepo _sysObjectActionRecordRepo;

        /// <summary>
        /// Thêm hồ sơ thao tác
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(SysObjectActionRecordEntity entity)
        {
            entity.OrganizeId = this.OrganizeId;
            _sysObjectActionRecordRepo.Add(entity);
            return Success();
        }

    }
}