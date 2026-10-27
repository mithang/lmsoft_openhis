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
    /// 库存结转
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
        /// 给视图中OrganizeId的赋值
        /// </summary>
        /// <returns></returns>
        public override IActionResult Index()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// 获取Lịch sử结转时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetlsjzDateTime()
        {
            var list = _kcKcjzRepo.GetlsjzDateTime(Constants.DeptCode, OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 结转结束时间
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
        /// 查询上一次结转的时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryLastCarryTime()
        {
            var lastCarryTime = _kcKcjzRepo.GetLastJzData(Constants.DeptCode, OrganizeId);
            return Success(null, lastCarryTime == null ? "" : lastCarryTime.jzsj.ToString(Constants.DateTimeFormat));
        }

        /// <summary>
        /// 查询已结转物资信息
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
        /// 结转
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