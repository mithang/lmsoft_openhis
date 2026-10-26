using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.DepartmentManage.Controllers
{
    /// <summary>
    /// Quản lý khoa
    /// </summary>
    public class DepartmentController : ControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;

        /// <summary>
        /// Lấy khoa
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult DepartmentQuery(string keyword)
        {
            var depts = _sysDepartmentRepo.GetList(OrganizeId, zt: "1", keyword: keyword);
            return Content(depts.ToJson());
        }

        /// <summary>
        /// Lấy khoa
        /// </summary>
        /// <returns></returns>
        public ActionResult CurrentUserDepartmentQuery()
        {
            var account = UserIdentity.UserCode;
            var depts = _sysUserDmnService.SelectUserDepartment(account, OrganizeId);
            return Content(depts.ToJson());
        }
    }
}