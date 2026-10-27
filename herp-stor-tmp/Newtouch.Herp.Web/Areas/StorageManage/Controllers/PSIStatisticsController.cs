using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// 进销存统计 (thống kê nhập-xuất-tồn).
    /// Grid data comes from a normal JSON action, so this page is ported fully;
    /// only its SSRS print button (tempCode 1271) is stubbed in the view.
    /// </summary>
    [Area("StorageManage")]
    public class PSIStatisticsController : ControllerBase
    {
        private readonly IKcKcjzDmnService _kcKcjzDmnService;

        public PSIStatisticsController(IKcKcjzDmnService kcKcjzDmnService)
        {
            _kcKcjzDmnService = kcKcjzDmnService;
        }

        public override IActionResult Index()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        public IActionResult PSIStatisticsInfoList(Pagination pagination, PsiStatisticsDTO searchParam)
        {
            var list = new
            {
                rows = _kcKcjzDmnService.GetPsiStatistics(pagination, searchParam ?? new PsiStatisticsDTO(), Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
    }
}
