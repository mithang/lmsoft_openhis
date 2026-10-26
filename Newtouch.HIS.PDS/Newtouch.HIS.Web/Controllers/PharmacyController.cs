using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using System.Web.Mvc;
using Newtouch.Common.Operator;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// nhà thuốc Controller
    /// </summary>
    public class PharmacyController : ControllerBase
    {
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly IDepartmentMedicineInfoDmnService _departmentMedicineInfoDmnService;
        private readonly IRequisitionDmnService _requisitionDmnService;


        /// <summary>
        /// lĩnh nội bộ
        /// </summary>
        /// <returns></returns>
        public ActionResult RequisitionForm()
        {
            return View();
        }

        /// <summary>
        /// khởi tạo số chứng từ
        /// </summary>
        /// <returns></returns>
        public ActionResult RequisitionGetNewDjh()
        {
            var data = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("内部申领单", Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
            return Content(data);
        }

        /// <summary>
        /// lĩnh nội bộ bộ phận phát thuốc Sele
        /// </summary>
        /// <returns></returns>
        public ActionResult RequisitionFybmSele(string keyword)
        {
            var data = _sysPharmacyDepartmentApp.GetTheUpperYkbmCodeList(keyword, Constants.CurrentYfbm.yfbmCode, OperatorProvider.GetCurrent().OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// lĩnh nội bộ mã nhập Sele
        /// </summary>
        /// <returns></returns>
        public ActionResult RequisitionSrmSele(string keyword, string fybm)
        {
            if (string.IsNullOrWhiteSpace(fybm))
            {
                return null;
            }
            var data = _departmentMedicineInfoDmnService.GetRequisitionDepartmentMedicineSeleVOList((keyword ?? "").Trim(), fybm, OperatorProvider.GetCurrent().OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// lĩnh nội bộ gửi bảng đơn
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitRequisition(string sldh, string fybmCode, IList<RequisitionDepartmentMedicineSubmitItemVO> slmxList)
        {
            if (string.IsNullOrWhiteSpace(sldh))
            {
                throw new FailedException("缺少申领单号");
            }
            if (string.IsNullOrWhiteSpace(fybmCode))
            {
                throw new FailedException("请选择发药部门");
            }
            if (slmxList == null || slmxList.Count == 0)
            {
                throw new FailedException("未提交明细数据（非正确格式）");
            }
            _requisitionDmnService.SubmitRequisition(sldh, fybmCode, Constants.CurrentYfbm.yfbmCode, slmxList, OperatorProvider.GetCurrent().OrganizeId);

            return Success();
        }


        /// <summary>
        /// phiếu lĩnh tra cứu (mà bộ phận nhà thuốc đã phát ra)
        /// </summary>
        /// <returns></returns>
        public ActionResult RequisitionSearchIndex()
        {
            return View();
        }

        /// <summary>
        /// xuất kho theo phiếu lĩnh phiếu lĩnh nguồn dữ liệu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sldh"></param>
        /// <param name="slbm"></param>
        /// <param name="ckbm"></param>
        /// <param name="ffzt"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public ActionResult RequisitionGridJson(Pagination pagination, DateTime? startDate, DateTime? endDate, string sldh, string slbm, string ckbm, int? ffzt, string from = "query")
        {
            var curYfbm = Constants.CurrentYfbm;
            var allUseableFfzt = "0,1,2,3";
            switch (@from)
            {
                case "query":
                    slbm = curYfbm.yfbmCode;
                    break;
                case "slck":
                    ckbm = curYfbm.yfbmCode;
                    allUseableFfzt = "0,1";
                    break;
                default:
                    return null;
            }

            var receiptMaininfoList = new
            {
                rows = _requisitionDmnService.RequisitionSearch(pagination, OperatorProvider.GetCurrent().OrganizeId, startDate, endDate
                , sldh, slbm, ckbm, ffzt, allUseableFfzt),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptMaininfoList.ToJson());
        }

        /// <summary>
        /// xuất kho theo phiếu lĩnh phiếu lĩnh chi tiết dữ liệu nguồn dữ liệu
        /// </summary>
        /// <param name="sldId"></param>
        /// <returns></returns>
        public ActionResult RequisitionDetailList(string sldId)
        {
            var list = _requisitionDmnService.RequisitionDetailListBySlId(sldId, OperatorProvider.GetCurrent().OrganizeId);
            return list == null ? Error("获取申领单明细失败") : Success(null, list);
        }


        /// <summary>
        /// yêu cầu lấy bộ phận hiện tại của ypCode thuốc của tồn kho lượng
        /// </summary>
        /// <param name="ypCode">mã thuốc</param>
        /// <param name="fybm">bộ phận phát thuốc</param>
        /// <returns></returns>
        public ActionResult GetYpKcslAndYpdw(string ypCode, string fybm)
        {
            var data = _departmentMedicineInfoDmnService.GetYpKcslAndYpdw(Constants.CurrentYfbm.yfbmCode, ypCode, OperatorProvider.GetCurrent().OrganizeId, fybm) ?? new DepartmentMedicineStockUnitVO();
            return Success(null, data);
        }

    }
}