using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Tools;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Nhà thuốc kho thuốc
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

        #region Tra cứu chứng từ

        /// <summary>
        /// Truy vấn chứng từ Khung nhìn v2
        /// </summary>
        /// <returns></returns>
        public IActionResult ReceiptQueryV2()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Truy vấn chứng từ Thông tin chính
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
        public IActionResult SelectReceiptMainInfo(Pagination pagination, DateTime? qsrj, DateTime? jsrj, string pdh, string fph, int? djlx, string shzt, string from = "query")
        {
            var curYfbmjb = Constants.CurrentYfbm.yfbmjb;
            var allUseableDjlx = "1,2,3,4,5,6";
            switch (from)
            {
                case "query":
                    if (curYfbmjb == "2")//Nhà thuốc
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
        /// Truy vấn chứng từ Thông tin chính
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
        public IActionResult SelectReceiptMainInfoV2(Pagination pagination, DateTime? qsrj, DateTime? jsrj, string pdh, string fph, int? djlx, string shzt, string from = "query")
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
                    if (curYfbmjb != "1")//Nhà thuốc
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
                fph = Request.Query["fph"].ToString(),
                djlx = djlx,
                shzt = shzt,
                alldjlx = allUseableDjlx,
                orgId = OperatorProvider.GetCurrent().OrganizeId,
                curYfbmCode = Constants.CurrentYfbm.yfbmCode,
                gys = Request.Query["gys[]"].ToString()
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
        /// Truy vấn chuẩn bị thuốc cho khoa
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="qsrj"></param>
        /// <param name="jsrj"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public IActionResult SelectDrupreparationInfo(Pagination pagination, DateTime? qsrj, DateTime? jsrj, string shzt)
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
        /// Chi tiết truy vấn chứng từ
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        public IActionResult SelectReceipDetailInfo(string crkId, int djlx)
        {
            var list = _pharmacyDrugStorageApp.SelectReceipDetailInfo(crkId, djlx);
            return Content(list.ToJson());
        }

        #endregion

        #region Duyệt chứng từ

        /// <summary>
        /// Duyệt chứng từ
        /// </summary>
        /// <returns></returns>
        public IActionResult ReceiptApproval()
        {
            return View();
        }

        /// <summary>
        /// Chứng từ xuất nhập kho Duyệt
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public IActionResult SubmitReceiptApproval(string crkId, int djlx, string shzt)
        {
            return _sysMedicineReceiptDmnService.ApprovalStorageIoReceipt(crkId, djlx, shzt) ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }
        /// <summary>
        /// Chứng từ xuất nhập kho Duyệt
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public IActionResult SubmitDrupreparation(string byId, string shzt)
        {
            return _sysMedicineReceiptDmnService.SubmitDrupreparation(byId, shzt) ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }

        #endregion

        /// <summary>
        ///  Truy vấn lượng tồn kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="tybz">Cờ ngừng dùng</param>
        /// <param name="zt"></param>
        /// <param name="noShow0Kc"></param>
        /// <returns></returns>
        public IActionResult SelectStockTotal(Pagination pagination, string inputCode, string tybz, string zt, bool noShow0Kc)
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
        /// Chi tiết truy vấn lượng tồn kho
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SelectStockDetail(string ypCode, string yfbmCode)
        {
            var list = _pharmacyDrugStorageApp.SelectStockDetail(ypCode, yfbmCode);
            return Content(list.ToJson());
        }
        
        #region Kiểm kê tồn kho


        /// <summary>
        /// Kiểm kê tồn kho
        /// </summary>
        /// <returns></returns>
        public IActionResult StockInventoryV2()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//Khoa dượccode
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//Tổ chứcid
            return View("StockInventoryIndex");
        }

        public IActionResult StockInventoryV3()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//Khoa dượccode
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//Tổ chứcid
            return View("StockInventoryIndexV2");
        }


        /// <summary>
        /// Truy vấn kiểm kê
        /// </summary>
        /// <returns></returns>
        public IActionResult InventoryQuery()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//Khoa dượccode
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//Tổ chứcid
            return View();
        }

        public IActionResult InventoryQueryV2()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//Khoa dượccode
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//Tổ chứcid
            return View();
        }

        /// <summary>
        /// Bắt đầu kiểm kê
        /// </summary>
        /// <returns></returns>
        public IActionResult InventoryAction()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//Khoa dượccode
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//Tổ chứcid
            return View();
        }

        public IActionResult InventoryActionV2()
        {
            ViewBag.bmCode = Constants.CurrentYfbm.yfbmCode;//Khoa dượccode
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//Tổ chứcid
            return View();
        }


        /// <summary>
        /// Lấy thời gian bắt đầu kiểm kê
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPDDateDropdownList()
        {
            var list = _pharmacyDrugStorageApp.GetPdDateDropdownList();
            return Success(null, list);
        }

        /// <summary>
        /// Lấy loại thuốc （Kiểm kê）
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMedicineCategoryList()
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
        /// Bắt đầu kiểm kê
        /// </summary>
        /// <returns></returns>
        public IActionResult StartInventory()
        {
            return Success(null, _pharmacyDrugStorageApp.StartInventory());
        }

        /// <summary>
        /// Truy vấn thông tin thuốc cần kiểm kê
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectInventoryInfoList(Pagination pagination, string pdId, string pdsj, string srm, string ypzt, string yplb, int kcxs = -1)
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
        /// Truy vấn chi tiết cần kiểm kê Trả về Số lượng đơn vị bộ phận và Số lượng đơn vị nhỏ nhất độc lập với nhau
        /// </summary>
        /// <returns></returns>
        public IActionResult InventoryInfoQuery(Pagination pagination, string pdId, string pdsj, string srm, string ypzt, string yplb, int kcxs = -1)
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
        /// Chuyển đổi số lượng hiển thị trên trang  XXhộpXXviên
        /// </summary>
        /// <param name="sl"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public IActionResult GetYfbmYpComplexYpSlandDw(int sl, string ypCode)
        {
            var slstr = _pharmacyDrugStorageDmnService.GetYfbmYpComplexYpSlandDw(sl, ypCode);
            return Content(slstr);
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveInventoryInfo(string resultObjArr, string pdId)
        {
            _pharmacyDrugStorageApp.SaveInventoryInfo(resultObjArr.ToObject<List<SaveInventoryInfoVO>>(), pdId);
            return Success();
        }

        /// <summary>
        /// Lưu thông tin mã truy xuất tồn kho
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveInventoryZsmInfo(string resultObjArr, string pdId)
        {
            _pharmacyDrugStorageApp.SaveInventoryZsmInfo(resultObjArr.ToObject<List<SaveInventoryInfoVO>>());
            return Success();
        }

        /// <summary>
        /// Kết thúc kiểm kê
        /// </summary>
        /// <param name="pdId">Đơn kiểm kêID</param>
        /// <param name="autoCarryDown">null/0：Không tự động kết chuyển  1：Tự động kết chuyển</param>
        /// <returns></returns>
        public IActionResult EndInventory(string pdId, string autoCarryDown)
        {
            _pharmacyDrugStorageApp.EndInventory(pdId);
            if ("1".Equals(autoCarryDown)) _drugStorageApp.CarryOverMedicine(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Success();
        }

        /// <summary>
        /// Hủy kiểm kê
        /// </summary>
        /// <returns></returns>
        public IActionResult CancelInventory(string pdId)
        {
            _pharmacyDrugStorageApp.CancelInventory(pdId);
            return Success();
        }

        #endregion

        #region Kết chuyển tồn kho

        /// <summary>
        /// Hộp thả xuống kỳ sổ sách
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAccountList()
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
        /// Truy vấn thời gian kết chuyển lần trước
        /// </summary>
        /// <returns></returns>
        public IActionResult GetLastCarryTime()
        {
            var lastCarryTime = _sysMedicineStockCarryOverRepo.GetLastAccountPeriodAndCarrayTime().lastCarrayTime;
            return Content(lastCarryTime);
        }

        /// <summary>
        /// Truy vấn kỳ kết chuyển lần trước
        /// </summary>
        /// <returns></returns>
        public IActionResult GetLastAccountPeriod()
        {
            var lastAccountPeriod = _sysMedicineStockCarryOverRepo.GetLastAccountPeriodAndCarrayTime().lastAccountPeriod;
            return Content(lastAccountPeriod);
        }

        /// <summary>
        /// Kết chuyển
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IActionResult CarryOverMedicine(string zq, DateTime kssj, DateTime jssj)
        {
            _pharmacyDrugStorageApp.CarryOverMedicine(zq, kssj, jssj);
            return Success();
        }

        /// <summary>
        /// Truy vấn thông tin thuốc đã kết chuyển
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectCarryOverMedicineList(Pagination pagination, string zq, string inputCode)
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

        #region Nhập xuất tồn

        /// <summary>
        /// Lấy kỳ kết chuyển và thời gian bắt đầu/kết thúc tương ứng của kỳ
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAccountPeriodDropDownList()
        {
            var list = _pharmacyDrugStorageDmnService.GetAccountPeriodDropDownList();
            return Content(list.ToJson());
        }
        /// <summary>
        /// Lấy dạng bào chế thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMedicineFormulationList()
        {
            var list = _pharmacyDrugStorageApp.GetMedicineFormulationList();
            return Content(list.ToJson(new[] { "jxCode", "jxmc" }));
        }

        /// <summary>
        /// Lấy loại thuốc （Nhập xuất tồn）
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMedicineCategoryList2()
        {
            var list = _pharmacyDrugStorageApp.GetMedicineCategoryList2();
            return Content(list.ToJson());
        }

        /// <summary>
        /// Thống kê nhập xuất tồn
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
        public IActionResult PSIStatisticsInfoList(Pagination pagination, string kszq, string jszq, DateTime ksjzsj, DateTime jsjzsj, string inputCode, string deptCode, string drugType, string dosage, string drugState, string rate)
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

        #region Khác

        /// <summary>
        /// Người dùng đăng nhập hiện tại Nhà thuốc có thể thao tác（Kho thuốc）Danh sách Nguồn dữ liệu
        /// </summary>
        /// <returns></returns>
        public IActionResult PharmacyDepartmentList()
        {
            var curUser = OperatorProvider.GetCurrent();
            var list = _sysUserDmnService.GetUserPharmacyDepartmentList(curUser.OrganizeId, curUser.UserId);
            return Content(list.ToJson());
        }

        #endregion

        /// <summary>
        /// Sửa số hóa đơn
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public IActionResult UpdateFph(string crkmxId, string fph)
        {
            return _sysMedicineStorageIOReceiptDetailRepo.UpdateFph(crkmxId, fph) > 0 ? Success() : Error("Sửa số hóa đơn thất bại");
        }

    }
}