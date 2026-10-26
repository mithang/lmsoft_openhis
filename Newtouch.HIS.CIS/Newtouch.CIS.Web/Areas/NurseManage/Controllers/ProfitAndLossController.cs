using System.Collections.Generic;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.IDomainServices.DeptStorageManage;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.IRepository.DeptStorageManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;


namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// Báo hao hụt, báo thừa
    /// </summary>
    public class ProfitAndLossController : OrgControllerBase
    {
        private readonly IDeptKcSyyyRepo _iDeptKcSyyyRepo;
        private readonly IKcSyxxDmnService _kcSyxxDmnService;
        private readonly ISysUserDmnService sysuserDmnService;
        private readonly IStockInventoryApp _stockInventoryApp;

        #region 损益原因维护

        /// <summary>
        /// Lý do lãi lỗ
        /// </summary>
        /// <returns></returns>
        public ActionResult Syyy()
        {
            return View();
        }
        
        /// <summary>
        /// Tra cứu thông tin lý do lãi lỗ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetSyyyGridJson(Pagination pagination, string keyWord = "")
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
        /// Lý do lãi lỗ Chế độ xem
        /// </summary>
        /// <returns></returns>
        public ActionResult SyyyForm()
        {
            return View();
        }

        /// <summary>
        /// Xóa lý do lãi lỗ
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteSyyyForm(string keyValue)
        {
            return _iDeptKcSyyyRepo.DeleteSyyyById(keyValue) > 0 ? Success("删除成功") : Error("删除失败");
        }

        /// <summary>
        /// get Syyy information
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetSyyyFormJson(string keyWord)
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
        public ActionResult SubmitSyyyForm(DeptKcSyyyEntity deptKcSyyyEntity, string keyWord)
        {
            deptKcSyyyEntity.zt = deptKcSyyyEntity.zt == "true" ? "1" : "0";
            return _iDeptKcSyyyRepo.SubmitForm(deptKcSyyyEntity, keyWord) > 0 ? Success("操作成功") : Error("操作失败");
        }

        /// <summary>
        /// Lấy thả xuống lý do lãi lỗ
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLossProfitReasonListByType(string sybz = "")
        {
            return Content(_iDeptKcSyyyRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString() && (p.sybz == sybz || "" == sybz)).ToJson());
        }

        #endregion

        #region 损益查询

        /// <summary>
        /// Tra cứu lãi lỗ
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryProfitAndLoss()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Lấy chi tiết lãi lỗ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult SelectLossAndProditInfoList(Pagination pagination, LossAndProditSearchDTO param)
        {
            var list = new
            {
                // Người đăng nhập của khoa hiện tại
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
        /// Lấy số chứng từ báo hao hụt, báo thừa
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerateDjh()
        {
            return Content(ReceiptNoManage.GetNewReceiptNo(EnumOutOrInStorageBillType.Bsby.GetDescription()));
        }


        /// <summary>
        /// Lấy người chịu trách nhiệmlist
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public ActionResult GetZRRList(string inputCode)
        {
            var list = sysuserDmnService.GetStaffListByOrg(OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Gửi lãi lỗ
        /// </summary>
        /// <param name="plist"></param>
        /// <returns></returns>
        public ActionResult LossAndProfitSubmit(List<LossAndProditSubmitDTO> plist)
        {
            //todo Lấy khoa hiện tại
            var result = _stockInventoryApp.LossAndProfitSubmit(plist, "");
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion
    }
}