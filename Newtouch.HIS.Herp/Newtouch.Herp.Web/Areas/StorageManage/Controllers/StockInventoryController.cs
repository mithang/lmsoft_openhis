using System;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// Kiểm kê tồn kho
    /// </summary>
    public class StockInventoryController : ControllerBase
    {
        private readonly IStockInventoryDmnService _stockInventoryDmnService;
        private readonly IStockInventoryApp _stockInventoryApp;
        private readonly IKcKcjzDmnService _kcKcjzDmnService;

        /// <summary>
        /// Bắt đầu kiểm kê
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryAction()
        {
            return View();
        }

        /// <summary>
        /// Bắt đầu kiểm kê
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Lấy thời gian kiểm kê
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPdsj()
        {
            var result = _stockInventoryDmnService.GetPdSj(Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Bắt đầu kiểm kê
        /// </summary>
        /// <returns></returns>
        public ActionResult StartInventory()
        {
            return Success(null, _stockInventoryApp.StartInventory(Constants.CurrentKf.currentKfId, OrganizeId));
        }

        /// <summary>
        /// Tra cứu thông tin kiểm kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inventorySearchDTO"></param>
        /// <returns></returns>
        public ActionResult QueryInventoryInfoList(Pagination pagination, InventorySearchDTO inventorySearchDTO)
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

        /// <summary>
        /// Tra cứu thông tin kiểm kê Không phân lô
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inventorySearchDTO"></param>
        /// <returns></returns>
        public ActionResult QueryInventoryInfoListNoPc(Pagination pagination, InventorySearchDTO inventorySearchDTO)
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

        /// <summary>
        /// Lưu kiểm kê
        /// </summary>
        /// <param name="resultObjArr"></param>
        /// <param name="pdId"></param>
        /// <param name="noPc">0:Kiểm kê theo lô  1:Kiểm kê không theo lô</param>
        /// <returns></returns>
        public ActionResult SaveInventory(string resultObjArr, long pdId, string noPc)
        {
            _stockInventoryApp.SaveInventoryInfo(resultObjArr.ToList<SaveInventoryDTO>(), pdId, noPc);
            return Success();
        }

        /// <summary>
        /// Hủy kiểm kê
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelInventory(long pdId)
        {
            _stockInventoryApp.CancelInventory(pdId);
            return Success();
        }

        /// <summary>
        /// Kết thúc kiểm kê
        /// </summary>
        /// <param name="pdId">Phiếu kiểm kê ID</param>
        /// <param name="autoCarryDown">null/0:Không tự động kết chuyển  1:Tự động kết chuyển</param>
        /// <returns></returns>
        public ActionResult EndInventory(long pdId, string autoCarryDown)
        {
            _stockInventoryApp.EndInventory(pdId);
            if (!"1".Equals(autoCarryDown)) return Success();
            var carryOverResult = _kcKcjzDmnService.CarryOverProduct(Constants.CurrentKf.currentKfId, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            if (!string.IsNullOrWhiteSpace(carryOverResult)) throw new Exception(carryOverResult);
            return Success();
        }
    }
}