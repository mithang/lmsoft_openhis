using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// 库存结转 (kết chuyển tồn kho).
    /// Grid data comes from normal JSON actions, so this page is ported fully;
    /// only its SSRS print button (tempCode 1270) is stubbed in the view.
    /// </summary>
    [Area("StorageManage")]
    public class StockCarryOverController : ControllerBase
    {
        private readonly IKcKcjzRepo _kcKcjzRepo;
        private readonly IKcKcjzDmnService _kcKcjzDmnService;

        public StockCarryOverController(IKcKcjzRepo kcKcjzRepo, IKcKcjzDmnService kcKcjzDmnService)
        {
            _kcKcjzRepo = kcKcjzRepo;
            _kcKcjzDmnService = kcKcjzDmnService;
        }

        public override IActionResult Index()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        [HttpGet]
        public IActionResult GetlsjzDateTime()
        {
            var list = _kcKcjzRepo.GetlsjzDateTime(Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(list.ToJson());
        }

        [HttpGet]
        public IActionResult GetJzJssjDateTime()
        {
            var list = _kcKcjzRepo.GetlsjzDateTime(Constants.CurrentKf.currentKfId, OrganizeId);
            list.Insert(0, new SelectVo { value = "", text = "Hiện tại" });
            return Content(list.ToJson());
        }

        [HttpGet]
        public IActionResult QueryLastCarryTime()
        {
            var lastCarryTime = _kcKcjzRepo.GetLastJzData(Constants.CurrentKf.currentKfId, OrganizeId);
            return Success(null, lastCarryTime == null ? "" : lastCarryTime.jzsj.ToString(Constants.DateTimeFormat));
        }

        public IActionResult SelectCarryOverDetail(Pagination pagination, string jzsj, string keyWork)
        {
            var list = new
            {
                rows = _kcKcjzDmnService.SelectCarryOverDetail(pagination, jzsj, keyWork, Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        [HttpPost]
        public IActionResult CarryOverProduct()
        {
            var ret = _kcKcjzDmnService.CarryOverProduct(Constants.CurrentKf.currentKfId, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            return string.IsNullOrWhiteSpace(ret) ? Success() : Error("Kết chuyển thất bại");
        }
    }
}
