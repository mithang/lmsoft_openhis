using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Operator;

using Newtouch.Domain.IDomainServices.DeptStorageManage;
using Newtouch.Domain.IRepository.DeptStorageManage;
using Newtouch.Domain.ValueObjects.Storage;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// Tồn khokết chuyển
    /// </summary>
    [Area("NurseManage")]
    public class StockCarryOverController : OrgControllerBase
    {
        private readonly IKcKcjzRepo _kcKcjzRepo;
        private readonly IKcKcjzDmnService _kcKcjzDmnService;

        public StockCarryOverController(IKcKcjzRepo kcKcjzRepo, IKcKcjzDmnService kcKcjzDmnService)
        {
            this._kcKcjzRepo = kcKcjzRepo;
            this._kcKcjzDmnService = kcKcjzDmnService;
        }

        /// <summary>
        /// choxemhìnhtrongOrganizeIdcủaGán giá trị
        /// </summary>
        /// <returns></returns>
        public override IActionResult Index()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// LấyLịch sửkết chuyểnThời gian
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetlsjzDateTime()
        {
            var list = _kcKcjzRepo.GetlsjzDateTime(Constants.DeptCode, OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// kết chuyểnThời gian kết thúc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetJzJssjDateTime()
        {
            var list = _kcKcjzRepo.GetlsjzDateTime(Constants.DeptCode, OrganizeId);
            list.Insert(0, new SelectVo { value = "", text = "Hiện tại" });
            return Content(list.ToJson());
        }

        /// <summary>
        /// Tra cứucái trước lầnkết chuyểncủaThời gian
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryLastCarryTime()
        {
            var lastCarryTime = _kcKcjzRepo.GetLastJzData(Constants.DeptCode, OrganizeId);
            return Success(null, lastCarryTime == null ? "" : lastCarryTime.jzsj.ToString(Constants.DateTimeFormat));
        }

        /// <summary>
        /// Tra cứuđãkết chuyểnvật tưthông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jzsj"></param>
        /// <param name="keyWork"></param>
        /// <returns></returns>
        public IActionResult SelectCarryOverDetail(Pagination pagination, string jzsj, string keyWork)
        {
            var list = new
            {
                rows = _kcKcjzDmnService.SelectCarryOverDetail(pagination, jzsj, keyWork, Constants.DeptCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// kết chuyển
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CarryOverProduct()
        {
            var ret = _kcKcjzDmnService.CarryOverProduct(Constants.DeptCode, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            return string.IsNullOrWhiteSpace(ret) ? Success() : Error("Kết chuyểnthất bại");
        }
    }
}