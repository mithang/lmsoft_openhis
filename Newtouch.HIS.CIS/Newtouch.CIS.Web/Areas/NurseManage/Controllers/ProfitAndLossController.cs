using System.Collections.Generic;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.CIS.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.IDomainServices;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Application.Interface;
using NewtouchHIS.Lib.Framework.Operator;

using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.IDomainServices.DeptStorageManage;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.IRepository.DeptStorageManage;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;


namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// Báo hỏngbáo thừa
    /// </summary>
    [Area("NurseManage")]
    public class ProfitAndLossController : OrgControllerBase
    {
        private readonly IDeptKcSyyyRepo _iDeptKcSyyyRepo;
        private readonly IKcSyxxDmnService _kcSyxxDmnService;
        private readonly ISysUserDmnService sysuserDmnService;
        private readonly IStockInventoryApp _stockInventoryApp;

        public ProfitAndLossController(IDeptKcSyyyRepo iDeptKcSyyyRepo, IKcSyxxDmnService kcSyxxDmnService, IStockInventoryApp stockInventoryApp)
        {
            this._iDeptKcSyyyRepo = iDeptKcSyyyRepo;
            this._kcSyxxDmnService = kcSyxxDmnService;
            this._stockInventoryApp = stockInventoryApp;
        }

        #region Nguyên nhân thừa thiếuBảo trì

        /// <summary>
        /// nguyên nhân lãi lỗ
        /// </summary>
        /// <returns></returns>
        public IActionResult Syyy()
        {
            return View();
        }
        
        /// <summary>
        /// nguyên nhân lãi lỗthông tinTra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetSyyyGridJson(Pagination pagination, string keyWord = "")
        {
            var data = new
            {
                rows = _iDeptKcSyyyRepo.FindList(p => "" == keyWord || p.syyy.Contains(keyWord.Trim()), pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// nguyên nhân lãi lỗ xemhình
        /// </summary>
        /// <returns></returns>
        public IActionResult SyyyForm()
        {
            return View();
        }

        /// <summary>
        /// Xóanguyên nhân lãi lỗ
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteSyyyForm(string keyValue)
        {
            return _iDeptKcSyyyRepo.DeleteSyyyById(keyValue) > 0 ? Success("Xóa thành công") : Error("Xóathất bại");
        }

        /// <summary>
        /// get Syyy information
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetSyyyFormJson(string keyWord)
        {
            var syyy = _iDeptKcSyyyRepo.FindEntity(p => p.Id == keyWord);
            return Content(syyy.ToJson());
        }

        /// <summary>
        /// Submit SyyyForm
        /// </summary>
        /// <param name="deptKcSyyyEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult SubmitSyyyForm(DeptKcSyyyEntity deptKcSyyyEntity, string keyWord)
        {
            deptKcSyyyEntity.zt = deptKcSyyyEntity.zt == "true" ? "1" : "0";
            return _iDeptKcSyyyRepo.SubmitForm(deptKcSyyyEntity, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tácthất bại");
        }

        /// <summary>
        /// Lấynguyên nhân lãi lỗdướikéo
        /// </summary>
        /// <returns></returns>
        public IActionResult GetLossProfitReasonListByType(string sybz = "")
        {
            return Content(_iDeptKcSyyyRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString() && (p.sybz == sybz || "" == sybz)).ToJson());
        }

        #endregion

        #region Thừa thiếuTra cứu

        /// <summary>
        /// lãi lỗTra cứu
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryProfitAndLoss()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Lấylãi lỗchi tiết
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IActionResult SelectLossAndProditInfoList(Pagination pagination, LossAndProditSearchDTO param)
        {
            var list = new
            {
                // hiện tạiKhoađănglục người
                rows = _kcSyxxDmnService.SelectLossAndProditInfoList(pagination, param, UserIdentity.DepartmentCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        #endregion

        #region Generate profit or loss

        /// <summary>
        /// LấyBáo hỏngbáo thừaphiếusố phiếu
        /// </summary>
        /// <returns></returns>
        public IActionResult GenerateDjh()
        {
            return Content(ReceiptNoManage.GetNewReceiptNo(EnumOutOrInStorageBillType.Bsby.GetDescription()));
        }


        /// <summary>
        /// LấyTrách nhiệm ngườilist
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public IActionResult GetZRRList(string inputCode)
        {
            var list = sysuserDmnService.GetStaffListByOrg(OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// lãi lỗGửi
        /// </summary>
        /// <param name="plist"></param>
        /// <returns></returns>
        public IActionResult LossAndProfitSubmit(List<LossAndProditSubmitDTO> plist)
        {
            //todo Lấyhiện tạiKhoa
            var result = _stockInventoryApp.LossAndProfitSubmit(plist, "");
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion
    }
}