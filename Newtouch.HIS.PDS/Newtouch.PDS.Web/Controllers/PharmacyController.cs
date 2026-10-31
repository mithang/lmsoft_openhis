using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Infrastructure.EF;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Nhà thuốcController
    /// </summary>
    public class PharmacyController : ControllerBase
    {
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly IDepartmentMedicineInfoDmnService _departmentMedicineInfoDmnService;
        private readonly IRequisitionDmnService _requisitionDmnService;


        /// <summary>
        /// Lĩnh nội bộ
        /// </summary>
        /// <returns></returns>
        public IActionResult RequisitionForm()
        {
            return View();
        }

        /// <summary>
        /// Khởi tạo số chứng từ
        /// </summary>
        /// <returns></returns>
        public IActionResult RequisitionGetNewDjh()
        {
            var data = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("内部申领单", Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
            return Content(data);
        }

        /// <summary>
        /// Lĩnh nội bộ Bộ phận phát thuốc Sele
        /// </summary>
        /// <returns></returns>
        public IActionResult RequisitionFybmSele(string keyword)
        {
            var data = _sysPharmacyDepartmentApp.GetTheUpperYkbmCodeList(keyword, Constants.CurrentYfbm.yfbmCode, OperatorProvider.GetCurrent().OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lĩnh nội bộ Mã nhập Sele
        /// </summary>
        /// <returns></returns>
        public IActionResult RequisitionSrmSele(string keyword, string fybm)
        {
            if (string.IsNullOrWhiteSpace(fybm))
            {
                return null;
            }
            var data = _departmentMedicineInfoDmnService.GetRequisitionDepartmentMedicineSeleVOList((keyword ?? "").Trim(), fybm, OperatorProvider.GetCurrent().OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lĩnh nội bộ Gửi biểu mẫu
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitRequisition(string sldh, string fybmCode, IList<RequisitionDepartmentMedicineSubmitItemVO> slmxList)
        {
            if (string.IsNullOrWhiteSpace(sldh))
            {
                throw new FailedException("Thiếu số đơn lĩnh");
            }
            if (string.IsNullOrWhiteSpace(fybmCode))
            {
                throw new FailedException("Vui lòng chọn khoa phát thuốc");
            }
            if (slmxList == null || slmxList.Count == 0)
            {
                throw new FailedException("Chưa gửi dữ liệu chi tiết（Định dạng không đúng）");
            }
            _requisitionDmnService.SubmitRequisition(sldh, fybmCode, Constants.CurrentYfbm.yfbmCode, slmxList, OperatorProvider.GetCurrent().OrganizeId);

            return Success();
        }


        /// <summary>
        /// Truy vấn đơn lĩnh(Bộ phận nhà thuốc gửi đi)
        /// </summary>
        /// <returns></returns>
        public IActionResult RequisitionSearchIndex()
        {
            return View();
        }

        /// <summary>
        /// Xuất kho theo đơn lĩnh Đơn lĩnh Nguồn dữ liệu
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
        public IActionResult RequisitionGridJson(Pagination pagination, DateTime? startDate, DateTime? endDate, string sldh, string slbm, string ckbm, int? ffzt, string from = "query")
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
        /// Xuất kho theo đơn lĩnh Đơn lĩnh Dữ liệu chi tiết Nguồn dữ liệu
        /// </summary>
        /// <param name="sldId"></param>
        /// <returns></returns>
        public IActionResult RequisitionDetailList(string sldId)
        {
            var list = _requisitionDmnService.RequisitionDetailListBySlId(sldId, OperatorProvider.GetCurrent().OrganizeId);
            return list == null ? Error("Lấy chi tiết đơn lĩnh thất bại") : Success(null, list);
        }


        /// <summary>
        /// Yêu cầu lấy bộ phận hiện tại của ypCodeThuốc của Lượng tồn kho
        /// </summary>
        /// <param name="ypCode">Mã thuốc</param>
        /// <param name="fybm">Bộ phận phát thuốc</param>
        /// <returns></returns>
        public IActionResult GetYpKcslAndYpdw(string ypCode, string fybm)
        {
            var data = _departmentMedicineInfoDmnService.GetYpKcslAndYpdw(Constants.CurrentYfbm.yfbmCode, ypCode, OperatorProvider.GetCurrent().OrganizeId, fybm) ?? new DepartmentMedicineStockUnitVO();
            return Success(null, data);
        }

    }
}