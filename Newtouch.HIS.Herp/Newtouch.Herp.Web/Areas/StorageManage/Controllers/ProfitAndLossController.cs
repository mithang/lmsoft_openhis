using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// Báo hỏng/báo thừa.
    /// QueryProfitAndLoss grid data comes from a normal JSON action, so it is
    /// ported fully; only its SSRS print button (tempCode 1265) is stubbed
    /// in the view. Index() is inherited from the controller base (as in the
    /// Framework version, which declared no Index action).
    /// </summary>
    [Area("StorageManage")]
    public class ProfitAndLossController : ControllerBase
    {
        private readonly IKcSyyyRepo _kcSyyyRepo;
        private readonly IKcSyxxDmnService _kcSyxxDmnService;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IStockInventoryApp _stockInventoryApp;

        public ProfitAndLossController(IKcSyyyRepo kcSyyyRepo, IKcSyxxDmnService kcSyxxDmnService,
            ISysUserDmnService sysUserDmnService, IStockInventoryApp stockInventoryApp)
        {
            _kcSyyyRepo = kcSyyyRepo;
            _kcSyxxDmnService = kcSyxxDmnService;
            _sysUserDmnService = sysUserDmnService;
            _stockInventoryApp = stockInventoryApp;
        }

        #region Bảo trì nguyên nhân hao hụt/thặng dư

        public IActionResult Syyy()
        {
            return View();
        }

        public IActionResult GetSyyyGridJson(Pagination pagination, string keyWord = "")
        {
            // The Core IRepositoryBase has no paged FindList overload (the
            // Framework one did), so paging is applied here over the filtered list.
            var kw = (keyWord ?? "").Trim();
            var all = _kcSyyyRepo.FindList(p => "" == kw || p.syyy.Contains(kw));
            pagination.records = all.Count;
            var page = pagination.page ?? 1;
            var pageSize = pagination.rows ?? 15;
            var rows = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var data = new
            {
                rows = rows,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public IActionResult SyyyForm()
        {
            return View();
        }

        public IActionResult DeleteSyyyForm(string keyValue)
        {
            return _kcSyyyRepo.DeleteSyyyById(keyValue) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        public IActionResult GetSyyyFormJson(string keyWord)
        {
            var syyy = _kcSyyyRepo.FindEntity(p => p.Id == keyWord);
            return Content(syyy.ToJson());
        }

        public IActionResult SubmitSyyyForm(KcSyyyEntity kcSyyyEntity, string keyWord)
        {
            // G14: base probe — tao moi zt=on (dung UI SyyyForm.cshtml
            // formSerialize) luu zt=0, dropdown GetLossProfitReasonListByType
            // filter zt='1' nen khong thay dong moi; sua dong baseline cung
            // zt 1->0; body rong / syyy rong / sybz rong / syyy 101 ky tu ->
            // loi SQL tho "Cannot insert ... / truncated". Cung ho bug zt
            // nhu G13 #10/#12: nhan ca "on" va "true".
            if (kcSyyyEntity == null) return Error("Thông tin nguyên nhân không được để trống");
            if (string.IsNullOrWhiteSpace(kcSyyyEntity.syyy)) return Error("Nguyên nhân lãi lỗ bắt buộc");
            if (System.Text.Encoding.Default.GetBytes(kcSyyyEntity.syyy).Length > 100)
                return Error("Nguyên nhân lãi lỗ không được vượt quá 100 ký tự");
            if (string.IsNullOrWhiteSpace(kcSyyyEntity.sybz)
                || (kcSyyyEntity.sybz != ((int)EnumSybz.Loss).ToString()
                    && kcSyyyEntity.sybz != ((int)EnumSybz.Profit).ToString()))
                return Error("Dấu hiệu lãi lỗ không hợp lệ");
            kcSyyyEntity.zt = kcSyyyEntity.zt == "true" || kcSyyyEntity.zt == "on" ? "1" : "0";
            return _kcSyyyRepo.SubmitForm(kcSyyyEntity, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }

        public IActionResult GetLossProfitReasonListByType(string sybz = "")
        {
            return Content(_kcSyyyRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString() && (p.sybz == sybz || "" == sybz)).ToJson());
        }

        #endregion

        #region Truy vấn hao hụt/thặng dư

        public IActionResult QueryProfitAndLoss()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        public IActionResult SelectLossAndProditInfoList(Pagination pagination, LossAndProditSearchDTO param)
        {
            var list = new
            {
                rows = _kcSyxxDmnService.SelectLossAndProditInfoList(pagination, param ?? new LossAndProditSearchDTO(), Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        #endregion

        #region Generate profit or loss

        public IActionResult GenerateDjh()
        {
            return Content(ReceiptNoHelper.GetNewReceiptNo(EnumOutOrInStorageBillType.Bsby));
        }

        public IActionResult GetZRRList(string inputCode)
        {
            // Original ignored inputCode and returned all staff of the org.
            var list = _sysUserDmnService.GetStaffListByOrg(OrganizeId);
            return Content(list.ToJson());
        }

        public IActionResult LossAndProfitSubmit(List<LossAndProditSubmitDTO> plist)
        {
            var result = _stockInventoryApp.LossAndProfitSubmit(plist, Constants.CurrentKf.currentKfId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion
    }
}
