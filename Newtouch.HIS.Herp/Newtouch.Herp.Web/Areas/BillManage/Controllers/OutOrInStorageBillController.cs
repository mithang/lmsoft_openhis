using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Herp.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// Thao tác phiếu nhập/xuất kho (Core port)
    /// </summary>
    [Area("BillManage")]
    public class OutOrInStorageBillController : ControllerBase
    {
        private readonly IKfCrkdjDmnService _kfCrkdjDmnService;
        private readonly IKfCrkdjRepo _crkdjRepo;
        private readonly IKfCrkmxRepo _crkmxRepo;
        private readonly IOutOrInStorageBillApprovalApp _outOrInStorageBillApprovalApp;

        public OutOrInStorageBillController(IKfCrkdjDmnService kfCrkdjDmnService,
            IKfCrkdjRepo crkdjRepo, IKfCrkmxRepo crkmxRepo,
            IOutOrInStorageBillApprovalApp outOrInStorageBillApprovalApp)
        {
            _kfCrkdjDmnService = kfCrkdjDmnService;
            _crkdjRepo = crkdjRepo;
            _crkmxRepo = crkmxRepo;
            _outOrInStorageBillApprovalApp = outOrInStorageBillApprovalApp;
        }

        // The copy has no GetDescription() extension (the original came from
        // the Framework-era Newtouch.Core). Read the Description attribute
        // directly; values come from the already-translated EnumDB, except the
        // PROTECTED EnumOutOrInStorageBillType descriptions, which are never
        // translated (they are receipt-number keys, not display text).
        private static string Desc(System.Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
        }

        #region Duyệt chứng từ

        /// <summary>
        /// Duyệt chứng từ
        /// </summary>
        /// <returns></returns>
        public IActionResult Approval()
        {
            return View();
        }

        /// <summary>
        /// Phiếu nhập/xuất kho Duyệt
        /// </summary>
        public IActionResult SubmitReceiptApproval(BillApprovalDTO billApprovalDTO)
        {
            var result = _outOrInStorageBillApprovalApp.Approval(billApprovalDTO);
            return string.IsNullOrWhiteSpace(result) ? Success("Thao tác thành công") : Error(result);
        }

        /// <summary>
        /// Sửa số hóa đơn
        /// </summary>
        public IActionResult UpdateFph(long crkmxId, string fph)
        {
            return _crkmxRepo.UpdateFph(crkmxId, fph) > 0 ? Success() : Error("Sửa số hóa đơn thất bại");
        }

        /// <summary>
        /// Trả lại đơn Từ chờ xử lý chuyển thành bản nháp,Chỉ áp dụng cho nhập kho ngoài
        /// </summary>
        public IActionResult BackToTemporary(long crkId)
        {
            var result = _crkdjRepo.BackToTemporary(crkId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region Truy vấn chứng từ

        /// <summary>
        /// Truy vấn chứng từ
        /// </summary>
        /// <returns></returns>
        public IActionResult Query()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Truy vấn chứng từ Thông tin chính
        /// </summary>
        public IActionResult SelectReceiptMainInfo(Pagination pagination, CrkdjSearchParamDTO param, string from = "query")
        {
            var allUseableDjlx = new[] {
                ((int)EnumOutOrInStorageBillType.Wbrk).ToString(),
                ((int)EnumOutOrInStorageBillType.Wbck).ToString(),
                ((int)EnumOutOrInStorageBillType.Zjck).ToString(),
                ((int)EnumOutOrInStorageBillType.Slck).ToString(),
                ((int)EnumOutOrInStorageBillType.Nbth).ToString(),
                ((int)EnumOutOrInStorageBillType.chukuzhikeshi).ToString()
            };
            switch (from)
            {
                case "query":
                    if (Constants.CurrentKf.currentKfLevel != 1)
                    {
                        allUseableDjlx = new[]
                        {
                            ((int)EnumOutOrInStorageBillType.Zjck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Slck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Nbth).ToString(),
                            ((int)EnumOutOrInStorageBillType.chukuzhikeshi).ToString()
                        };
                    }
                    break;
                case "approval":
                    allUseableDjlx = Constants.CurrentKf.currentKfLevel != 1 ? new[] {
                            ((int)EnumOutOrInStorageBillType.Zjck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Slck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Nbth).ToString() } :
                        new[] {
                            ((int)EnumOutOrInStorageBillType.Wbrk).ToString(),
                            ((int)EnumOutOrInStorageBillType.Wbck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Zjck).ToString(),
                            ((int)EnumOutOrInStorageBillType.Nbth).ToString()
                        };
                    break;
            }
            param.organizeId = OrganizeId;
            var receiptMaininfoList = new
            {
                rows = _kfCrkdjDmnService.GetCrkdjMainList(pagination, param, allUseableDjlx, Constants.CurrentKf.currentKfId, from),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptMaininfoList.ToJson());
        }

        /// <summary>
        /// Chi tiết truy vấn chứng từ
        /// </summary>
        public IActionResult SelectReceipDetailInfo(string crkId)
        {
            return Content(_kfCrkdjDmnService.GetCrkdjmxList(crkId, OrganizeId).ToJson());
        }

        /// <summary>
        /// Lấy trạng thái duyệt thường dùng
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryCommonAuditState()
        {
            var result = new object[] {
               new { id = "", text = "==Tất cả==" },
               new { id = (int)EnumAuditState.Waiting, text = Desc(EnumAuditState.Waiting) },
               new { id = (int)EnumAuditState.Adopt, text = Desc(EnumAuditState.Adopt) },
               new { id = (int)EnumAuditState.Refuse, text = Desc(EnumAuditState.Refuse) },
               new { id = (int)EnumAuditState.Cancelled, text = Desc(EnumAuditState.Cancelled) },
            };
            return Content(result.ToJson());
        }

        /// <summary>
        /// Theo số phiếu giao hàng và nhà cung cấpIDLấy chi tiết phiếu nhập/xuất kho
        /// </summary>
        public IActionResult QueryCrkmxByDeliveryNo(string deliveryNo, string gysId, string djh)
        {
            var result = _kfCrkdjDmnService.SelectCrkmxByDeliveryNo(deliveryNo, djh, Constants.CurrentKf.currentKfId, OrganizeId, gysId);
            return Content(result.ToJson());
        }

        #endregion

        #region Bản nháp của tôi

        /// <summary>
        /// Bản nháp của tôi Chế độ xem
        /// </summary>
        /// <returns></returns>
        public IActionResult MyTemporaryCrkdj()
        {
            return View();
        }

        /// <summary>
        /// Xóa phiếu nháp
        /// </summary>
        public IActionResult DeleteTemporaryCrkdj(BillApprovalDTO billApprovalDTO)
        {
            var result = _kfCrkdjDmnService.DeleteCrkdj(billApprovalDTO.djId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Lấy chi tiết phiếu nháp theo số phiếu giao hàng
        /// </summary>
        [HttpGet]
        public IActionResult QueryTemporaryCrkmx(string deliveryNo, string djh)
        {
            var result = _kfCrkdjDmnService.SelectCrkmxByDeliveryNo(deliveryNo, djh, Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(result.ToJson());
        }

        #endregion
    }
}
