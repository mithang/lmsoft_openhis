using System;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Application.Interface;
using NewtouchHIS.Lib.Framework.Operator;

using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.IDomainServices.DeptStorageManage;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// Tồn kho Kiểm kê
    /// </summary>
    [Area("NurseManage")]
    public class StockInventoryController : OrgControllerBase
    {
        private readonly IStockInventoryDmnService _stockInventoryDmnService;
        private readonly IStockInventoryApp _stockInventoryApp;
        private readonly IKcKcjzDmnService _kcKcjzDmnService;

        public StockInventoryController(IStockInventoryDmnService stockInventoryDmnService, IStockInventoryApp stockInventoryApp, IKcKcjzDmnService kcKcjzDmnService)
        {
            this._stockInventoryDmnService = stockInventoryDmnService;
            this._stockInventoryApp = stockInventoryApp;
            this._kcKcjzDmnService = kcKcjzDmnService;
        }

        /// <summary>
        /// Bắt đầu Kiểm kê
        /// </summary>
        /// <returns></returns>
        public IActionResult InventoryAction()
        {
            return View();
        }

        /// <summary>
        /// Bắt đầu Kiểm kê
        /// </summary>
        /// <returns></returns>
        public IActionResult InventoryQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Lấy Kiểm kê Thời gian
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPdsj()
        {
            var result = _stockInventoryDmnService.GetPdSj(Constants.DeptCode, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Bắt đầu Kiểm kê
        /// </summary>
        /// <returns></returns>
        public IActionResult StartInventory()
        {
            return Success(null, _stockInventoryApp.StartInventory(Constants.DeptCode, OrganizeId));
        }

        /// <summary>
        /// Tra cứu Kiểm kêthông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inventorySearchDTO"></param>
        /// <returns></returns>
        public IActionResult QueryInventoryInfoList(Pagination pagination, InventorySearchDTO inventorySearchDTO)
        {
            var list = new
            {
                rows = inventorySearchDTO == null || inventorySearchDTO.pdId <= 0 ? null : _stockInventoryDmnService.QueryInventoryInfoList(pagination, inventorySearchDTO, UserIdentity.DepartmentCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Tra cứu Kiểm kêthông tin khôngphút Lô
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inventorySearchDTO"></param>
        /// <returns></returns>
        public IActionResult QueryInventoryInfoListNoPc(Pagination pagination, InventorySearchDTO inventorySearchDTO)
        {
            var list = new
            {
                rows = inventorySearchDTO == null || inventorySearchDTO.pdId <= 0 ? null : _stockInventoryDmnService.QueryInventoryInfoListNoPc(pagination, inventorySearchDTO, UserIdentity.DepartmentCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lưu Kiểm kê
        /// </summary>
        /// <param name="resultObjArr"></param>
        /// <param name="pdId"></param>
        /// <param name="noPc">0:theo Lô Kiểm kê  1:không theo Lô Kiểm kê</param>
        /// <returns></returns>
        public IActionResult SaveInventory(string resultObjArr, long pdId, string noPc)
        {
            _stockInventoryApp.SaveInventoryInfo(resultObjArr.ToList<SaveInventoryDTO>(), pdId, noPc);
            return Success();
        }

        /// <summary>
        /// Hủy Kiểm kê
        /// </summary>
        /// <returns></returns>
        public IActionResult CancelInventory(long pdId)
        {
            _stockInventoryApp.CancelInventory(pdId);
            return Success();
        }

        /// <summary>
        /// Kết thúc Kiểm kê
        /// </summary>
        /// <param name="pdId">Kiểm kê phiếu ID</param>
        /// <param name="autoCarryDown">null/0:khôngtựđộngkết chuyển  1:tựđộngkết chuyển</param>
        /// <returns></returns>
        public IActionResult EndInventory(long pdId, string autoCarryDown)
        {
            _stockInventoryApp.EndInventory(pdId);
            if (!"1".Equals(autoCarryDown)) return Success();
            var carryOverResult = _kcKcjzDmnService.CarryOverProduct(UserIdentity.DepartmentCode, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            if (!string.IsNullOrWhiteSpace(carryOverResult)) throw new Exception(carryOverResult);
            return Success();
        }
    }
}