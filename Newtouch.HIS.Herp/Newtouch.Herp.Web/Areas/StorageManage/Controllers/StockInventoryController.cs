using System;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// 库存盘点 (kiểm kê tồn kho).
    /// InventoryQuery grid data comes from normal JSON actions, so it is
    /// ported fully; only its SSRS print buttons (tempCode 1262/1263) are
    /// stubbed in the view. Index() is inherited from the controller base
    /// (as in the Framework version, which declared no Index action).
    /// </summary>
    [Area("StorageManage")]
    public class StockInventoryController : ControllerBase
    {
        private readonly IStockInventoryDmnService _stockInventoryDmnService;
        private readonly IStockInventoryApp _stockInventoryApp;
        private readonly IKcKcjzDmnService _kcKcjzDmnService;

        public StockInventoryController(IStockInventoryDmnService stockInventoryDmnService,
            IStockInventoryApp stockInventoryApp, IKcKcjzDmnService kcKcjzDmnService)
        {
            _stockInventoryDmnService = stockInventoryDmnService;
            _stockInventoryApp = stockInventoryApp;
            _kcKcjzDmnService = kcKcjzDmnService;
        }

        public IActionResult InventoryAction()
        {
            return View();
        }

        public IActionResult InventoryQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        public IActionResult GetPdsj()
        {
            var result = _stockInventoryDmnService.GetPdSj(Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult StartInventory()
        {
            return Success(null, _stockInventoryApp.StartInventory(Constants.CurrentKf.currentKfId, OrganizeId));
        }

        public IActionResult QueryInventoryInfoList(Pagination pagination, InventorySearchDTO inventorySearchDTO)
        {
            var list = new
            {
                rows = inventorySearchDTO == null || inventorySearchDTO.pdId <= 0 ? null : _stockInventoryDmnService.QueryInventoryInfoList(pagination, inventorySearchDTO, Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public IActionResult QueryInventoryInfoListNoPc(Pagination pagination, InventorySearchDTO inventorySearchDTO)
        {
            var list = new
            {
                rows = inventorySearchDTO == null || inventorySearchDTO.pdId <= 0 ? null : _stockInventoryDmnService.QueryInventoryInfoListNoPc(pagination, inventorySearchDTO, Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public IActionResult SaveInventory(string resultObjArr, long pdId, string noPc)
        {
            _stockInventoryApp.SaveInventoryInfo(resultObjArr.ToList<SaveInventoryDTO>(), pdId, noPc);
            return Success();
        }

        public IActionResult CancelInventory(long pdId)
        {
            _stockInventoryApp.CancelInventory(pdId);
            return Success();
        }

        public IActionResult EndInventory(long pdId, string autoCarryDown)
        {
            _stockInventoryApp.EndInventory(pdId);
            if (!"1".Equals(autoCarryDown)) return Success();
            var carryOverResult = _kcKcjzDmnService.CarryOverProduct(Constants.CurrentKf.currentKfId, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            if (!string.IsNullOrWhiteSpace(carryOverResult)) throw new Exception(carryOverResult);
            return Success();
        }
    }
}
