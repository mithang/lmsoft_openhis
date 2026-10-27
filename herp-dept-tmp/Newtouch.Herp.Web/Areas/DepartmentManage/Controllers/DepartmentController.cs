using Microsoft.AspNetCore.Mvc;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Herp.Web.Areas.DepartmentManage.Controllers
{
    /// <summary>
    /// Quản lý khoa phòng
    /// </summary>
    [Area("DepartmentManage")]
    public class DepartmentController : ControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;

        public DepartmentController(ISysDepartmentRepo sysDepartmentRepo, ISysUserDmnService sysUserDmnService)
        {
            _sysDepartmentRepo = sysDepartmentRepo;
            _sysUserDmnService = sysUserDmnService;
        }

        /// <summary>
        /// Trang bảo trì khoa phòng (bản gốc chỉ có view khung, không có action;
        /// action này được thêm để view có thể truy cập được).
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Form thêm mới/sửa (bản gốc chỉ có view khung, không có action;
        /// action này được thêm để view có thể truy cập được).
        /// </summary>
        /// <returns></returns>
        public IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// Lấy danh sách khoa phòng
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult DepartmentQuery(string keyword)
        {
            var depts = _sysDepartmentRepo.GetList(OrganizeId, zt: "1", keyword: keyword);
            return Content(depts.ToJson());
        }

        /// <summary>
        /// Lấy danh sách khoa phòng của người dùng hiện tại
        /// </summary>
        /// <returns></returns>
        public IActionResult CurrentUserDepartmentQuery()
        {
            var account = UserIdentity.UserCode;
            var depts = _sysUserDmnService.SelectUserDepartment(account, OrganizeId);
            return Content(depts.ToJson());
        }
    }
}
