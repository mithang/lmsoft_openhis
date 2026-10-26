using Newtouch.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Tools;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// nhà thuốc kho thuốc
    /// </summary>
    public class PharmacyDrugStorageController : ControllerBase
    {
        private readonly IPharmacyDrugStorageApp _pharmacyDrugStorageApp;
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;
        private readonly ISysMedicineReceiptDmnService _sysMedicineReceiptDmnService;
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly ISysMedicineStockCarryOverRepo _sysMedicineStockCarryOverRepo;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly ISysMedicineStockCarryDownRepo _sysMedicineStockCarryDownRepo;
        private readonly ISysMedicineStockCarryDownDmnService _sysMedicineStockCarryDownDmnService;
        private readonly ISysMedicineStorageIOReceiptDetailRepo _sysMedicineStorageIOReceiptDetailRepo;
        private readonly IDrugStorageApp _drugStorageApp;

        #region 单据查询

        /// <summary>
        /// tra cứu chứng từ khung nhìn v2
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiptQueryV2()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// tra cứu chứng từ chính thông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="qsrj"></param>
        /// <param name="jsrj"></param>
        /// <param name="pdh"></param>
        /// <param name="fph"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public ActionResult SelectReceiptMainInfo(Pagination pagination, DateTime? qsrj, DateTime? jsrj, string pdh, string fph, int? djlx, string shzt, string from = "query")
        {
            var curYfbmjb = Constants.CurrentYfbm.yfbmjb;
            var allUseableDjlx = "1,2,3,4,5,6";
            switch (from)
            {
                case "query":
                    if (curYfbmjb == "2")//nhà thuốc
                    {
                        allUseableDjlx = "3,4,5,6";
                    }
                    break;
                case "approval":
                    if (curYfbmjb == "2")
                    {
                        allUseableDjlx = "3,4,6";
                    }
                    else
                    {
                        allUseableDjlx = "1,2,5";
                    }
                    break;
            }
            var receiptMaininfoList = new
            {
                rows = _pharmacyDrugStorageDmnService.SelectReceiptMainInfo(pagination, qsrj, jsrj, pdh, fph, djlx, shzt, allUseableDjlx, OperatorProvider.GetCurrent().OrganizeId, Constants.CurrentYfbm.yfbmCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptMaininfoList.ToJson());
        }

        /// <summary>
        /// tra cứu chứng từ chính thông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="qsrj"></param>
        /// <param name="jsrj"></param>
        /// <param name="pdh"></param>
        /// <param name="fph"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <param name="gys"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public ActionResult SelectReceiptMainInfoV2(Pagination pagination, DateTime? qsrj, DateTime? jsrj, string pdh, string fph, int? djlx, string shzt, string from = "query")
        {
            var curYfbmjb = Constants.CurrentYfbm.yfbmjb;
            var allUseableDjlx = new[] {
                ((int) EnumDanJuLX.yaopinruku).ToString(),
                ((int) EnumDanJuLX.waibucuku).ToString(),
                ((int) EnumDanJuLX.zhijiefayao).ToString(),
                ((int) EnumDanJuLX.shenlingfayao).ToString(),
                ((int) EnumDanJuLX.neibufayaotuihui).ToString(),
                ((int) EnumDanJuLX.keshifayao).ToString()
            };
            switch (from)
            {
                case "query":
                    if (curYfbmjb != "1")//nhà thuốc
                    {
                        allUseableDjlx = new[]
                        {
                            ((int) EnumDanJuLX.zhijiefayao).ToString(),
                            ((int) EnumDanJuLX.shenlingfayao).ToString(),
                            ((int) EnumDanJuLX.neibufayaotuihui).ToString(),
                            ((int) EnumDanJuLX.keshifayao).ToString(),
                            ((int) EnumDanJuLX.piliangchuku).ToString()
                        };
                    }
                    else
                    {
                        allUseableDjlx = new[]
                       {
                            ((int) EnumDanJuLX.yaopinruku).ToString(),
                            ((int) EnumDanJuLX.waibucuku).ToString(),
                            ((int) EnumDanJuLX.zhijiefayao).ToString(),
                            ((int) EnumDanJuLX.shenlingfayao).ToString(),
                            ((int) EnumDanJuLX.neibufayaotuihui).ToString(),
                            ((int) EnumDanJuLX.piliangchuku).ToString()
                        };
                    }
                    break;
                case "approval":
                    if (curYfbmjb != "1")
                    {
                        allUseableDjlx = new[]
                        {
                            ((int) EnumDanJuLX.zhijiefayao).ToString(),
                            ((int) EnumDanJuLX.shenlingfayao).ToString(),
                            ((int) EnumDanJuLX.keshifayao).ToString(),
                            ((int) EnumDanJuLX.piliangchuku).ToString()
                        };
                    }
                    else
                    {
                        allUseableDjlx = new[] {
                            ((int) EnumDanJuLX.yaopinruku).ToString(),
                            ((int) EnumDanJuLX.waibucuku).ToString(),
                            ((int) EnumDanJuLX.neibufayaotuihui).ToString(),
                            ((int) EnumDanJuLX.keshifayao).ToString()
                        };
                    }
                    break;
            }

            var param = new ReceiptQueryParam
            {
                pagination = pagination,
                qsrj = qsrj,
                jsrj = jsrj,
                pdh = pdh,
                fph = Request.QueryString["fph"],
                djlx = djlx,
                shzt = shzt,
                alldjlx = allUseableDjlx,
                orgId = OperatorProvider.GetCurrent().OrganizeId,
                curYfbmCode = Constants.CurrentYfbm.yfbmCode,
                gys = Request.QueryString["gys[]"]
            };
            var receiptMaininfoList = new
            {
                rows = _pharmacyDrugStorageDmnService.SelectReceiptMainInfo(param),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptMaininfoList.ToJson());
        }
        /// <summary>
        /// Tra cứu thuốc dự trữ của khoa
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="qsrj"></param>
        /// <param name="jsrj"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public ActionResult SelectDrupreparationInfo(Pagination pagination, DateTime? qsrj, DateTime? jsrj, string shzt)
        {
            var param = new SelectDrupreParam
            {
                pagination = pagination,
                qsrj = qsrj,
                jsrj = jsrj,
                shzt = shzt,
                orgId = OperatorProvider.GetCurrent().OrganizeId,
                curYfbmCode = Constants.CurrentYfbm.yfbmCode
            };
            var receiptMaininfoList = new
            {
                rows = _pharmacyDrugStorageDmnService.SelectDrupreparationInfo(param),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptMaininfoList.ToJson());
        }
        /// <summary>
        /// tra cứu chứng từ chi tiết
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        public ActionResult SelectReceipDetailInfo(string crkId, int djlx)
        {
            var list = _pharmacyDrugStorageApp.SelectReceipDetailInfo(crkId, djlx);
            return Content(list.ToJson());
        }

        #endregion

        #region 单据审核

        /// <summary>
        /// đơn chứng duyệt
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiptApproval()
        {
            return View();
        }

        /// <summary>
        /// chứng từ nhập/xuất kho duyệt
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public ActionResult SubmitReceiptApproval(string crkId, int djlx, string shzt)
        {
            return _sysMedicineReceiptDmnService.ApprovalStorageIoReceipt(crkId, djlx, shzt) ? Success("操作成功") : Error("操作失败");
        }
        /// <summary>
        /// chứng từ nhập/xuất kho duyệt
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public ActionResult SubmitDrupreparation(string byId, string shzt)
        {
            return _sysMedicineReceiptDmnService.SubmitDrupreparation(byId, shzt) ? Success("操作成功") : Error("操作失败");
        }

        #endregion

        /// <summary>
        ///  tra cứu lượng tồn kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="tybz">cờ ngừng sử dụng</param>
        /// <param name="zt"></param>
        /// <param name="noShow0Kc"></param>
        /// <returns></returns>
        public ActionResult SelectStockTotal(Pagination pagination, string inputCode, string tybz, string zt, bool noShow0Kc)
        {
            var stockTotalList = new
            {
                rows = _pharmacyDrugStorageApp.SelectStockTotal(pagination, inputCode, tybz, zt, noShow0Kc),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(stockTotalList.ToJson());
        }

        /// <summary>
        /// tra cứu lượng tồn kho chi tiết
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SelectStockDetail(string ypCode, string yfbmCode)
        {
            var list = _pharmacyDrugStorageApp.SelectStockDetail(ypCode, yfbmCode);
            return Content(list.ToJson());
        }
        
        #region 库存盘点


        /// <summary>
        /// tồn kho kiểm kê
        /// </summary>
        /// <returns></returns>
        public ActionResult StockInventoryV2()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//bộ phận nhà thuốc code
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//tổ chức id
            return View("StockInventoryIndex");
        }

        public ActionResult StockInventoryV3()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//bộ phận nhà thuốc code
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//tổ chức id
            return View("StockInventoryIndexV2");
        }


        /// <summary>
        /// kiểm kê tra cứu
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryQuery()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//bộ phận nhà thuốc code
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//tổ chức id
            return View();
        }

        public ActionResult InventoryQueryV2()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//bộ phận nhà thuốc code
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//tổ chức id
            return View();
        }

        /// <summary>
        /// bắt đầu kiểm kê
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryAction()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//bộ phận nhà thuốc code
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//tổ chức id
            return View();
        }

        public ActionResult InventoryActionV2()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//bộ phận nhà thuốc code
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//tổ chức id
            return View();
        }


        /// <summary>
        /// lấy thời gian kiểm kê bắt đầu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPDDateDropdownList()
        {
            var list = _pharmacyDrugStorageApp.GetPdDateDropdownList();
            return Success(null, list);
        }

        /// <summary>
        /// lấy loại thuốc  (kiểm kê)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMedicineCategoryList()
        {
            var data = _pharmacyDrugStorageApp.GetMedicineCategoryList();
            var treeList = data.Select(item => new TreeSelectModel
            {
                id = item.dlCode,
                text = item.dlmc,
                parentId = null
            }).ToList();
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// bắt đầu kiểm kê
        /// </summary>
        /// <returns></returns>
        public ActionResult StartInventory()
        {
            return Success(null, _pharmacyDrugStorageApp.StartInventory());
        }

        /// <summary>
        /// tra cứu cần kiểm kê của thông tin thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectInventoryInfoList(Pagination pagination, string pdId, string pdsj, string srm, string ypzt, string yplb, int kcxs = -1)
        {
            var list = new
            {
                rows = string.IsNullOrWhiteSpace(pdId) ? null : _pharmacyDrugStorageApp.SelectInventoryInfoList(pagination, pdId, pdsj, srm, ypzt, yplb, kcxs),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// tra cứu cần chi tiết kiểm kê trả về số lượng đơn vị bộ phận và số lượng đơn vị nhỏ nhất độc lập với nhau
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryInfoQuery(Pagination pagination, string pdId, string pdsj, string srm, string ypzt, string yplb, int kcxs = -1)
        {
            var list = new
            {
                rows = string.IsNullOrWhiteSpace(pdId) ? null : _pharmacyDrugStorageDmnService.SelectInventoryDetailIndependentUnit(pagination, pdId, pdsj, srm, ypzt, yplb, kcxs),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// số lượng hiển thị trang chuyển đổi  XX hộp XX viên
        /// </summary>
        /// <param name="sl"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public ActionResult GetYfbmYpComplexYpSlandDw(int sl, string ypCode)
        {
            var slstr = _pharmacyDrugStorageDmnService.GetYfbmYpComplexYpSlandDw(sl, ypCode);
            return Content(slstr);
        }

        /// <summary>
        /// lưu
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveInventoryInfo(string resultObjArr, string pdId)
        {
            _pharmacyDrugStorageApp.SaveInventoryInfo(resultObjArr.ToList<SaveInventoryInfoVO>(), pdId);
            return Success();
        }

        /// <summary>
        /// lưu tồn kho mã truy xuất thông tin
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveInventoryZsmInfo(string resultObjArr, string pdId)
        {
            _pharmacyDrugStorageApp.SaveInventoryZsmInfo(resultObjArr.ToList<SaveInventoryInfoVO>());
            return Success();
        }

        /// <summary>
        /// kết thúc kiểm kê
        /// </summary>
        /// <param name="pdId">phiếu kiểm kê ID</param>
        /// <param name="autoCarryDown">null/0: không tự động kết chuyển  1: tự động kết chuyển</param>
        /// <returns></returns>
        public ActionResult EndInventory(string pdId, string autoCarryDown)
        {
            _pharmacyDrugStorageApp.EndInventory(pdId);
            if ("1".Equals(autoCarryDown)) _drugStorageApp.CarryOverMedicine(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Success();
        }

        /// <summary>
        /// hủy kiểm kê
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelInventory(string pdId)
        {
            _pharmacyDrugStorageApp.CancelInventory(pdId);
            return Success();
        }

        #endregion

        #region 库存结转

        /// <summary>
        /// kỳ sổ sách hộp thả xuống
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAccountList()
        {
            var accountList = new List<CarryOverAccountPeriodVO>();
            for (var i = -10; i <= 10; i++)
            {
                var entity = new CarryOverAccountPeriodVO();
                var date = DateTime.Now;
                entity.accountId = i;
                entity.account = date.AddMonths(i).ToString("yyyy") + date.AddMonths(i).ToString("MM");
                accountList.Add(entity);
            }
            return Content(accountList.ToJson());
        }

        /// <summary>
        /// tra cứu trên một lần kết chuyển của thời gian
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLastCarryTime()
        {
            var lastCarryTime = _sysMedicineStockCarryOverRepo.GetLastAccountPeriodAndCarrayTime().lastCarrayTime;
            return Content(lastCarryTime);
        }

        /// <summary>
        /// tra cứu trên một lần kết chuyển của kỳ sổ sách
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLastAccountPeriod()
        {
            var lastAccountPeriod = _sysMedicineStockCarryOverRepo.GetLastAccountPeriodAndCarrayTime().lastAccountPeriod;
            return Content(lastAccountPeriod);
        }

        /// <summary>
        /// kết chuyển
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult CarryOverMedicine(string zq, DateTime kssj, DateTime jssj)
        {
            _pharmacyDrugStorageApp.CarryOverMedicine(zq, kssj, jssj);
            return Success();
        }

        /// <summary>
        /// tra cứu đã kết chuyển thông tin thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCarryOverMedicineList(Pagination pagination, string zq, string inputCode)
        {
            var list = new
            {
                rows = _pharmacyDrugStorageApp.SelectCarryOverMedicineList(pagination, zq, inputCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        #endregion

        #region 进销存

        /// <summary>
        /// lấy thời gian bắt đầu/kết thúc tương ứng với kỳ kết chuyển và kỳ sổ sách
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAccountPeriodDropDownList()
        {
            var list = _pharmacyDrugStorageDmnService.GetAccountPeriodDropDownList();
            return Content(list.ToJson());
        }
        /// <summary>
        /// lấy dạng bào chế thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMedicineFormulationList()
        {
            var list = _pharmacyDrugStorageApp.GetMedicineFormulationList();
            return Content(list.ToJson(new[] { "jxCode", "jxmc" }));
        }

        /// <summary>
        /// lấy loại thuốc  (nhập-xuất-tồn)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMedicineCategoryList2()
        {
            var list = _pharmacyDrugStorageApp.GetMedicineCategoryList2();
            return Content(list.ToJson());
        }

        /// <summary>
        /// thống kê nhập-xuất-tồn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kszq"></param>
        /// <param name="jszq"></param>
        /// <param name="ksjzsj"></param>
        /// <param name="jsjzsj"></param>
        /// <param name="inputCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="drugType"></param>
        /// <param name="dosage"></param>
        /// <param name="drugState"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public ActionResult PSIStatisticsInfoList(Pagination pagination, string kszq, string jszq, DateTime ksjzsj, DateTime jsjzsj, string inputCode, string deptCode, string drugType, string dosage, string drugState, string rate)
        {
            var list = new
            {
                rows = _pharmacyDrugStorageDmnService.PsiStatisticsInfoList(pagination, kszq, jszq, ksjzsj, jsjzsj, inputCode, deptCode, drugType, dosage, drugState, rate),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        #endregion

        #region 其他

        /// <summary>
        /// hiện tại người dùng đăng nhập có thể thao tác của nhà thuốc (kho thuốc)danh sách nguồn dữ liệu
        /// </summary>
        /// <returns></returns>
        public ActionResult PharmacyDepartmentList()
        {
            var curUser = OperatorProvider.GetCurrent();
            var list = _sysUserDmnService.GetUserPharmacyDepartmentList(curUser.OrganizeId, curUser.UserId);
            return Content(list.ToJson());
        }

        #endregion

        /// <summary>
        /// sửa số hóa đơn
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public ActionResult UpdateFph(string crkmxId, string fph)
        {
            return _sysMedicineStorageIOReceiptDetailRepo.UpdateFph(crkmxId, fph) > 0 ? Success() : Error("修改发票号失败");
        }

    }
}