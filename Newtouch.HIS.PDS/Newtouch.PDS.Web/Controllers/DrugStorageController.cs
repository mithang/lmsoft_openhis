using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.PDS.Web.Helpers;
using Newtouch.Infrastructure.EF;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Common;
using Newtouch.Tools;
using Newtouch.Tools.Excel;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Kho thuốc Controller
    /// </summary>
    public class DrugStorageController : ControllerBase
    {

        private readonly ISysMedicinePriceAdjustmentRepo _sysMedicinePriceAdjustmentRepo;
        private readonly IDrugStorageApp _drugStorageApp;
        private readonly IHandOutMedicineDmnService _handOutMedicineDmnService;
        private readonly IDrugStorageDmnService _drugStorageDmnService;
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly IMedicineInfoDmnService _medicineInfoDmnService;
        private readonly ISysPharmacyDepartmentMedicineRepo sysPharmacyDepartmentMedicineRepo;
        private readonly ISysMedicineStockCarryDownRepo _sysMedicineStockCarryDownRepo;
        private readonly ISysMedicineStockCarryDownDmnService _sysMedicineStockCarryDownDmnService;
        private readonly ISysMedicineCrkfsDmnService _iSysMedicineCrkfsDmnService;
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;

        #region Nhập kho
        /// <summary>
        /// Nhập kho
        /// </summary>
        /// <returns></returns>
        public IActionResult InStorage()
        {
            return View();
        }

        /// <summary>
        /// Lấy số nhập kho mới
        /// </summary>
        /// <returns></returns>
        public IActionResult initialRKDH()
        {
            var rkdh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("Nhap kho thuoc don", Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
            return Content(rkdh);
        }

        /// <summary>
        /// Mã nhập Thông tin thuốc(Nhập kho)
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectDepartmentMedicineList(string keyword)
        {
            var data = _drugStorageApp.SelectDepartmentMedicineList(keyword, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Nhập kho
        /// </summary>
        /// <param name="crkdj">Chứng từ xuất nhập kho</param>
        /// <param name="crkdjmx">Chi tiết chứng từ xuất nhập kho</param>
        /// <returns></returns>
        /// <summary>
        /// G28 F1: guard header chung cho nhập/xuất kho (chạy trước mọi write).
        /// </summary>
        private static void ValidateStorageIOHeader(string pdh, string bm, string bmName, string crkfsdm)
        {
            if (string.IsNullOrWhiteSpace(pdh)) throw new FailedException("Số chứng từ không được để trống.");
            if (pdh.Trim().Length > 30) throw new FailedException("Số chứng từ không được dài quá 30 ký tự.");
            if (string.IsNullOrWhiteSpace(bm)) throw new FailedException(bmName + " không được để trống.");
            if (bm.Trim().Length > 100) throw new FailedException(bmName + " không được dài quá 100 ký tự.");
            if (string.IsNullOrWhiteSpace(crkfsdm)) throw new FailedException("Phương thức xuất nhập kho không được để trống.");
            if (crkfsdm.Trim().Length > 10) throw new FailedException("Mã phương thức xuất nhập kho không được dài quá 10 ký tự.");
        }

        /// <summary>
        /// G28 F1: guard 1 dòng chi tiết nhập/xuất kho. kcId chỉ bắt buộc với xuất kho.
        /// </summary>
        private static void ValidateStorageIODetail(string ypdM, string fph, string ph, string pc, decimal pfj, decimal lsj, decimal zje, int sl, int zhyz, string kcId, bool isOut)
        {
            if (string.IsNullOrWhiteSpace(ypdM)) throw new FailedException("Mã thuốc không được để trống.");
            if (ypdM.Trim().Length > 20) throw new FailedException("Mã thuốc không được dài quá 20 ký tự.");
            if (!string.IsNullOrEmpty(fph) && fph.Length > 50) throw new FailedException("Số hóa đơn không được dài quá 50 ký tự.");
            if (!string.IsNullOrEmpty(ph) && ph.Length > 30) throw new FailedException("Số lô không được dài quá 30 ký tự.");
            if (string.IsNullOrWhiteSpace(pc)) throw new FailedException("Mã lô không được để trống.");
            if (pc.Trim().Length > 30) throw new FailedException("Mã lô không được dài quá 30 ký tự.");
            if (sl <= 0) throw new FailedException("Số lượng phải lớn hơn 0.");
            if (pfj < 0 || lsj < 0 || zje < 0) throw new FailedException("Giá và thành tiền không được âm.");
            if (zhyz <= 0) throw new FailedException("Hệ số chuyển đổi phải lớn hơn 0.");
            if (isOut && string.IsNullOrWhiteSpace(kcId)) throw new FailedException("Mã tồn kho không được để trống.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveInStorageInfo(SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailEntity[] crkdjmx)
        {
            // G28 F1: guard đầu vào (trước mọi write).
            if (crkdj == null) throw new FailedException("Chứng từ nhập kho không được để trống.");
            ValidateStorageIOHeader(crkdj.Pdh, crkdj.Ckbm, "Bộ phận xuất kho", crkdj.Crkfsdm);
            if (crkdjmx == null || crkdjmx.Length == 0) throw new FailedException("Chi tiết chứng từ không được để trống.");
            foreach (var item in crkdjmx)
            {
                ValidateStorageIODetail(item.Ypdm, item.Fph, item.Ph, item.pc, item.Pfj, item.Lsj, item.Zje, item.Sl, item.Rkzhyz, null, false);
            }
            if (_drugStorageDmnService.ExistsReceiptByPdh(crkdj.Pdh)) throw new FailedException("Số chứng từ đã tồn tại.");
            var ioReceiptEntity = new SysMedicineStorageIOReceiptEntity
            {
                crkId = Guid.NewGuid().ToString(),
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                Pdh = crkdj.Pdh,
                Rkbm = Constants.CurrentYfbm.yfbmCode,
                Ckbm = crkdj.Ckbm,
                //Rksj = DateTime.Now,
                //Cksj = null,
                Rkczy = OperatorProvider.GetCurrent().UserCode,
                Ckczy = null,
                Crkfsdm = crkdj.Crkfsdm,
                Czsj = DateTime.Now,
                Sqsj = DateTime.Now,
                Shczy = null,
                shzt = "0",
                zt = "1",
                djlx = 1
            };
            ioReceiptEntity.Create();

            var crkdjmxList = crkdjmx.ToList();
            var ioReceiptDetailEntityList = new List<SysMedicineStorageIOReceiptDetailEntity>();
            foreach (var item in crkdjmxList)
            {
                var ioReceiptDetailEntity = new SysMedicineStorageIOReceiptDetailEntity
                {
                    crkmxId = Guid.NewGuid().ToString(),
                    crkId = ioReceiptEntity.crkId,
                    Ypdm = item.Ypdm,
                    Fph = item.Fph,
                    Kprq = item.Kprq,
                    Dprq = null,
                    Ph = item.Ph,
                    Yxq = item.Yxq,
                    Pfj = item.Pfj,
                    Lsj = item.Lsj,
                    Ykpfj = item.Pfj,
                    Yklsj = item.Lsj,
                    Zje = item.Zje,
                    Sl = item.Sl,
                    Rkzhyz = item.Rkzhyz,
                    Rkbmkc = item.Rkbmkc,
                    Ckzhyz = item.Rkzhyz,
                    Ckbmkc = 0,
                    Wg = item.Wg,
                    zbbz = 0,
                    jkzcz = null,
                    hgzm = null,
                    ysjg = item.ysjg,
                    Thyy = null,
                    Cljg = null,
                    scrq = item.scrq,
                    kl = item.kl,
                    jj = item.jj,
                    cd = 0,
                    pc = item.pc,
                    zt = "1",
                    px = null
                };
                ioReceiptDetailEntity.Create();

                ioReceiptDetailEntityList.Add(ioReceiptDetailEntity);
            }
            _drugStorageApp.SaveInStorageInfo(ioReceiptEntity, ioReceiptDetailEntityList);
            return Success();
        }

        #endregion

        #region Xuất kho
        /// <summary>
        /// Xuất kho
        /// </summary>
        /// <returns></returns>
        public IActionResult OutStorage()
        {
            return View();
        }

        /// <summary>
        /// Lấy số xuất kho mới
        /// </summary>
        /// <returns></returns>
        public IActionResult initialCKDH()
        {
            var ckdh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("Xuat kho ra ngoai don", Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
            return Content(ckdh);
        }

        /// <summary>
        /// Truy vấn hóa đơn
        /// </summary>
        /// <param name="fph"></param>
        /// <returns></returns>
        public IActionResult SelectMedicineListByFPH(string fph)
        {
            var data = _drugStorageApp.SelectMedicineListByFPH(fph);
            return Json(data);
        }

        /// <summary>
        /// Mã nhập Thông tin thuốc(Xuất kho)
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectDepartmentMedicineList2(string keyword, string fph, string gyscode)
        {
            var data = _drugStorageApp.SelectDepartmentMedicineList2(keyword, fph, gyscode);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Xuất kho
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveOutStorageInfo(SysMedicineStorageIOReceiptEntity crkdj, SysMedicineStorageIOReceiptDetailVO[] crkdjmx)
        {
            // G28 F1: guard đầu vào (trước mọi write) + [HttpPost]/antiforgery
            // (trước đây action này ghi dữ liệu qua GET — CSRF, họ D-G9-HTTPPOST-001).
            if (crkdj == null) throw new FailedException("Chứng từ xuất kho không được để trống.");
            ValidateStorageIOHeader(crkdj.Pdh, crkdj.Rkbm, "Bộ phận nhập kho", crkdj.Crkfsdm);
            if (crkdjmx == null || crkdjmx.Length == 0) throw new FailedException("Chi tiết chứng từ không được để trống.");
            foreach (var item in crkdjmx)
            {
                ValidateStorageIODetail(item.Ypdm, item.Fph, item.Ph, item.pc, item.Pfj, item.Lsj, item.Zje, item.Sl, item.Ckzhyz, item.kcId, true);
            }
            if (_drugStorageDmnService.ExistsReceiptByPdh(crkdj.Pdh)) throw new FailedException("Số chứng từ đã tồn tại.");
            var ioReceiptEntity = new SysMedicineStorageIOReceiptEntity
            {
                crkId = Guid.NewGuid().ToString(),
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                Pdh = crkdj.Pdh,
                Rkbm = crkdj.Rkbm ?? "",
                Ckbm = Constants.CurrentYfbm.yfbmCode,
                //Rksj = null,
                //Cksj = DateTime.Now,
                Rkczy = null,
                Ckczy = OperatorProvider.GetCurrent().UserCode,
                Crkfsdm = crkdj.Crkfsdm ?? "",
                Czsj = DateTime.Now,
                Sqsj = DateTime.Now,
                Shczy = null,
                shzt = ((int)EnumDjShzt.WaitingApprove).ToString(),
                zt = "1",
                djlx = (int)EnumDanJuLX.waibucuku
            };
            ioReceiptEntity.Create();

            var crkdjmxList = crkdjmx.ToList();
            var ioReceiptDetailList = crkdjmxList.Select(item => new SysMedicineStorageIOReceiptDetailVO
            {
                crkmxId = Guid.NewGuid().ToString(),
                crkId = ioReceiptEntity.crkId,
                Ypdm = item.Ypdm,
                Fph = item.Fph,
                Kprq = item.Kprq,
                Dprq = null,
                Ph = item.Ph,
                Yxq = item.Yxq,
                Pfj = item.Pfj,
                Lsj = item.Lsj,
                Ykpfj = item.Pfj,
                Yklsj = item.Lsj,
                Zje = item.Zje,
                Sl = item.Sl,
                Rkzhyz = item.Ckzhyz,
                Rkbmkc = 0,
                Ckzhyz = item.Ckzhyz,
                Ckbmkc = item.Ckbmkc,
                Wg = null,
                zbbz = 1,
                jkzcz = null,
                hgzm = null,
                ysjg = null,
                Thyy = item.Thyy,
                Cljg = item.Cljg,
                scrq = item.scrq,
                kl = item.kl,
                jj = item.jj,
                cd = 0,
                pc = item.pc ?? "",
                zt = "1",
                px = null,
                kcId = item.kcId
            }).ToList();
            _drugStorageApp.SaveOutStorageInfo(ioReceiptEntity, ioReceiptDetailList);
            return Success();

        }

        #endregion

        #region Điều chỉnh giá
        /// <summary>
        ///Đơn xin điều chỉnh giá
        /// </summary>
        /// <returns></returns>
        public IActionResult PriceAdjustmentRequest()
        {
            return View();
        }

        /// <summary>
        ///Duyệt điều chỉnh giá
        /// </summary>
        /// <returns></returns>
        public IActionResult PriceAdjustmentApproval()
        {
            return View();
        }

        /// <summary>
        ///Lịch sử điều chỉnh giá
        /// </summary>
        /// <returns></returns>
        public IActionResult PriceAdjustmentHistory()
        {
            return View();
        }

        /// <summary>
        /// Đơn xin điều chỉnh giá (Truy vấn)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public IActionResult SelectAdjustPriceMedicineInfoList(Pagination pagination, string inputCode)
        {
            var list = new
            {
                rows = _drugStorageApp.SelectAdjustPriceMedicineInfoList(pagination, inputCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        ///  <summary>
        ///  Gửi đơn xin
        ///  </summary>
        ///  <param name="ypCode"></param>
        ///  <param name="pfj"></param>
        ///  <param name="lsj"></param>
        ///  <param name="ypfj"></param>
        ///  <param name="ylsj"></param>
        ///  <param name="tzwj"></param>
        /// <param name="zxsj"></param>
        /// --zt:0:Chưa duyệt 1:Đã duyệt 2:Đã từ chối 3.Đã thu hồi
        /// 	--zxzt:0:Chưa thực hiện 1:Đã thực hiện
        ///  <returns></returns>
        public IActionResult SubmitRequuest(string ypCode, string pfj, string lsj, string ypfj, string ylsj, string tzwj, string zxsj)
        {
            // G28 F4: guard đầu vào (trước mọi write).
            if (string.IsNullOrWhiteSpace(ypCode)) throw new FailedException("Mã thuốc không được để trống.");
            if (ypCode.Trim().Length > 20) throw new FailedException("Mã thuốc không được dài quá 20 ký tự.");
            if (!string.IsNullOrWhiteSpace(tzwj) && tzwj.Length > 30) throw new FailedException("Tên văn bản điều chỉnh không được dài quá 30 ký tự.");
            if (!DecimalParseHelper.TryParseDecimal(pfj, out var pfjVal)) throw new FailedException("Giá mua không đúng định dạng.");
            if (!DecimalParseHelper.TryParseDecimal(lsj, out var lsjVal)) throw new FailedException("Giá bán lẻ không đúng định dạng.");
            if (!DecimalParseHelper.TryParseDecimal(ypfj, out var ypfjVal)) throw new FailedException("Giá mua gốc không đúng định dạng.");
            if (!DecimalParseHelper.TryParseDecimal(ylsj, out var ylsjVal)) throw new FailedException("Giá bán lẻ gốc không đúng định dạng.");
            var notApproveEntity = _sysMedicinePriceAdjustmentRepo.GetMedicinePriceAdjustmentNotApproveEntity(ypCode);
            if (notApproveEntity != null)
            {
                // G28: FailedCodeException không được middleware chuyển thành message
                // (client nhận message=null) — dùng FailedException với message tiếng Việt.
                throw new FailedException("Thuốc đã có đơn xin điều chỉnh giá chưa duyệt, không thể gửi lại.");
            }
            if (!DateTime.TryParse(zxsj, out var zxsjVal))
            {
                throw new FailedException("Thời gian thực hiện không đúng định dạng.");
            }
            var entity = new SysMedicinePriceAdjustmentEntity
            {
                yptjId = Guid.NewGuid().ToString(),
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                yfbmCode = Constants.CurrentYfbm.yfbmCode,
                ypCode = ypCode,
                pfj = pfjVal,
                lsj = lsjVal,
                ypfj = ypfjVal,
                ylsj = ylsjVal,
                shzt = "0",
                xglx = "tj",
                tzsj = DateTime.Now,
                tzwj = tzwj,
                zxsj = zxsjVal,
                tzczy = OperatorProvider.GetCurrent().UserCode,
                shczy = null,
                zxczy = null,
                zxbz = "0",
                px = null
            };
            entity.Create();
            _sysMedicinePriceAdjustmentRepo.AddPriceAdjustmentRecord(entity);
            return Success();
        }

        /// <summary>
        /// Truy vấn duyệt điều chỉnh giá thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public IActionResult SelectMedicineAdjustPriceApprovalInfoList(Pagination pagination, string inputCode, string shzt)
        {
            var list = new
            {
                rows = _drugStorageApp.SelectMedicineAdjustPriceApprovalInfoList(pagination, inputCode, shzt),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        ///  <summary>
        ///  Duyệt thuốc
        ///  </summary>
        /// <param name="ypCodeStr"></param>
        /// <param name="operationType"></param>
        ///  --shzt:0:Chưa duyệt 1:Đã duyệt 2:Đã từ chối 3.Đã thu hồi
        /// 	--zxbz:0:Chưa thực hiện 1:Đã thực hiện
        ///  <returns></returns>
        public IActionResult MedicineAdjustPriceApproval(string ypCodeStr, int operationType)
        {
            if (string.IsNullOrWhiteSpace(ypCodeStr))
            {
                return Error("Mã thuốc không được để trống");
            }
            var ypCodeList = new ArrayList(ypCodeStr.TrimEnd(',').Split(','));

            if (operationType == (int)EnumPriceAdjustOperationType.Execute) //Thực hiện
            {
                return Success(_drugStorageApp.ExecteAdjustPrice(ypCodeList));
            }
            var result = _sysMedicinePriceAdjustmentRepo.MedicinePriceAdjustment(ypCodeList, operationType); //Duyệt Từ chối Thu hồi
            return Success(result);
        }

        /// <summary>
        /// Truy vấn lịch sử điều chỉnh giá thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectMedicineAdjustPriceHistoryInfoList(Pagination pagination, string inputCode, string startTime, string endTime)
        {
            var list = new
            {
                rows = _drugStorageApp.SelectMedicineAdjustPriceHistoryInfoList(pagination, inputCode, startTime, endTime),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        #endregion

        #region Trả hàng phát thuốc ngoại trú
        /// <summary>
        ///  Lấy bản ghi tồn kho thuốc của khoa
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        public IActionResult GetpcInfoList(string ypdm)
        {
            var data = _handOutMedicineDmnService.GetpcList(ypdm);
            return Content(data.ToJson());
        }
        #endregion

        #region Trang máy chủ trỏ tới
        /// <summary>
        ///Phát thuốc nội bộ
        /// </summary>
        /// <returns></returns>
        public IActionResult HandOutMedicine()
        {
            return View();
        }

        /// <summary>
        ///Truy vấn phát thuốc nội bộ
        /// </summary>
        /// <returns></returns>
        public IActionResult HandOutMedicineInfo()
        {
            return View();
        }

        /// <summary>
        ///Xuất kho theo đơn lĩnh
        /// </summary>
        /// <returns></returns>
        public IActionResult HandOutMedicineByRequest()
        {
            return View();
        }

        /// <summary>
        /// Xuất kho theo đơn lĩnh upgrade
        /// </summary>
        /// <returns></returns>
        public IActionResult ApplyDelivery()
        {
            return View();
        }

        /// <summary>
        /// Xem trước thông tin thuốc xuất kho theo đơn lĩnh
        /// </summary>
        /// <returns></returns>
        public IActionResult PreViewInfo()
        {
            return View();
        }

        /// <summary>
        ///Duyệt trả hàng
        /// </summary>
        /// <returns></returns>
        public IActionResult ConfirmationOfReturn()
        {
            return View();
        }

        /// <summary>
        /// Nhà thuốc phát thuốc cho khoa
        /// </summary>
        /// <returns></returns>
        public IActionResult HandOutMedicineToks()
        {
            return View();
        }

        #endregion

        #region Phát thuốc nội bộ, Xuất kho trực tiếp

        /// <summary>
        /// Khởi tạo số chứng từ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult InitDjh(string djmc)
        {
            return Success(null, EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo(djmc, Constants.CurrentYfbm.yfbmCode, OrganizeId));
        }

        /// <summary>
        /// Phát thuốc
        /// </summary>
        /// <param name="XT_YP_LS_NBFYMXK"></param>
        /// <param name="lybm"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExecHandOutMedicine(List<XT_YP_LS_NBFYMXK> XT_YP_LS_NBFYMXK, string lybm, string fydh, string fyfs, int type)
        {
            //G39-F5: guard controller (mẫu MedicineController.ExecHandOutMedicine G30); service còn guard lại + joinability.
            if (XT_YP_LS_NBFYMXK == null || XT_YP_LS_NBFYMXK.Count == 0) return Error("Thông tin thuốc phát không được để trống.");
            if (string.IsNullOrWhiteSpace(lybm)) return Error("Vui lòng chọn bộ phận lĩnh thuốc.");
            if (string.IsNullOrWhiteSpace(fydh)) return Error("Thiếu số đơn phát thuốc.");
            if (string.IsNullOrWhiteSpace(fyfs)) return Error("Vui lòng chọn phương thức xuất/nhập kho.");
            var data = _handOutMedicineDmnService.HandOutMedicine(XT_YP_LS_NBFYMXK, lybm, fydh, fyfs, type);
            return string.IsNullOrWhiteSpace(data) ? Success() : Error(data);
        }

        /// <summary>
        /// Nhà thuốc phát thuốc cho khoa
        /// </summary>
        /// <param name="XT_YP_LS_NBFYMXK"></param>
        /// <param name="lybm"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DispensingMedicineToKs(List<XT_YP_LS_NBFYMXK> XT_YP_LS_NBFYMXK, string lybm, string fydh, string fyfs, int type)
        {
            var data = _handOutMedicineDmnService.DispensingMedicineToKs(XT_YP_LS_NBFYMXK, lybm, fydh, fyfs, type);
            return string.IsNullOrWhiteSpace(data) ? Success() : Error(data);
        }
        #endregion

        #region Truy vấn phát thuốc nội bộ

        /// <summary>
        /// Truy vấn phát thuốc nội bộ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req">Điều kiện tìm kiếm</param>
        /// <returns></returns>
        public IActionResult QueryHandOutMeidicineList(Pagination pagination, QueryMedicineInfoReqVO req)
        {
            var data = new
            {
                rows = _medicineInfoDmnService.GetHandOutMedicineInfoList(pagination, req),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        #endregion

        #region Xuất kho theo đơn lĩnh

        /// <summary>
        /// Lấy thông tin bảng chính đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh">Số đơn lĩnh</param>
        /// <param name="slbm">Bộ phận lĩnh</param>
        /// <param name="ffzt">Trạng thái phát thuốc</param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <returns></returns>
        public IActionResult RequestInfo(Pagination pagination, string sldh, string slbm, string ffzt, string txtStartDate, string txtEndDate)
        {
            ffzt = ffzt == "" ? "-1" : ffzt;
            var data = new
            {
                rows = _drugStorageDmnService.GetMedicineRequestInfo(pagination, sldh, slbm, ffzt, txtStartDate, txtEndDate),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Xuất kho theo đơn lĩnh
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExecHandOutMedicineByReq(Dictionary<string, List<XT_YP_LS_NBFYMXK>> xtYpLsNbfymxk, string fyfs, int type)
        {
            //G39-F7: chặn dict rỗng TRƯỚC khi mint fydh (đỡ đốt 1 số serial cho input rỗng — C-G39-5 một phần).
            //G39-F7b: binder còn tạo dict rác {"fyfs":[],"type":[],...} khi không post dict (8d) — dict
            //có key nhưng mọi list đều rỗng cũng phải chặn ở đây, trước khi mint.
            if (xtYpLsNbfymxk == null || xtYpLsNbfymxk.Count == 0 || xtYpLsNbfymxk.Values.All(v => v == null || v.Count == 0)) return Error("Thông tin đơn lĩnh không được để trống.");
            var fydh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("Linh Phat thuoc don", Constants.CurrentYfbm.yfbmCode, OrganizeId);
            var res = _handOutMedicineDmnService.HandOutMedicineByRequest(xtYpLsNbfymxk, fydh, fyfs, type);
            return string.IsNullOrWhiteSpace(res) ? Success() : Error(res);
        }

        /// <summary>
        /// Lấy thông tin thuốc đơn lĩnh
        /// </summary>
        /// <param name="sldId"></param>
        /// <returns></returns>
        public IActionResult RequestMedicineInfo(string sldId)
        {
            if (sldId == "undefined")
            {
                sldId = "";
            }
            var data = _drugStorageDmnService.GetMedicineDetail(sldId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// (Xuất kho theo đơn lĩnh) Kết thúc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Abandonzt(string id)
        {
            _drugStorageDmnService.UpgradeStatus(id);
            return Success("Kết thúc thành công!");
        }
        #endregion


        #region Gọi chung

        /// <summary>
        /// Tự động gợi ý mã nhập(Nhắc nhở bộ phận cấp 1,Không có hệ số chuyển đổi,Ví dụ khoa dược)
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ckbm">Bộ phận xuất kho(Nhà thuốc phát thuốc)</param>
        /// <returns></returns>
        public IActionResult HandOutMedicinesrmList(string keyword, string ckbm)
        {
            var data = _handOutMedicineDmnService.GetDrugGroupByPc(keyword, ckbm, OrganizeId).Select(p => new
            {
                ypdm = p.ypcode,
                yplb = p.dlmc,
                ypmc = p.ypmc,
                srm = p.py,
                gg = p.ypgg,
                sccj = p.ycmc,
                bmdw = p.bmdw,
                ph = p.ph,
                kykc = p.klsl,
                lsj = p.lsj,
                pfj = p.pfj,
                yxq = (p.yxq ?? DateTime.MinValue).ToString("yyy-MM-dd"),
                zhyz = p.zhyz,
                pc = p.pc,
                zxsl = p.kcsl,
                zxdw = p.zxdw,
                bzs = p.bzs,
                bmdwsl = p.bmdwsl,
                ycmc = p.ycmc,
                bzdwsl = p.bzdwsl,
                bzdw = p.bzdw
            });
            return Content(data.ToJson());
        }

        /// <summary>
        /// Tự động gợi ý mã nhập(Nhắc nhở bộ phận cấp 2,Tính hệ số chuyển đổi,Ví dụ nhà thuốc ngoại trú)
        /// </summary>
        /// <returns></returns>
        public IActionResult MedicinesrmSlevelList(string rkbm, string keyword)
        {
            var data = _handOutMedicineDmnService.GetTyypBySrm(Constants.CurrentYfbm.yfbmCode, rkbm, keyword).Select(p => new
            {
                ypdm = p.ypcode,
                yplb = p.dlmc,
                ypmc = p.ypmc,
                srm = p.py,
                gg = p.ypgg,
                sccj = p.ycmc,
                pc = p.pc,
                ph = p.ph,
                kykc = p.bmdwsl,
                lsj = p.yklsj,
                pfj = p.ykpfj,
                yxq = p.yxq,
                zhyz = p.bzs,
                bzdwsl = p.bzdwsl,
                ykdw = p.bzdw,
                deptdw = p.deptdw,
                zxsl = p.kykcsl,
                zxdw = p.zxdw,
                bzdwslstr = p.bzdwslstr
            });
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy bộ phận nhà thuốc code
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTheLowerYfbmCodeList(string keyword)
        {
            var data = _pharmacyDrugStorageDmnService.GetTheLowerYfbmCodeList(keyword, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy khoa phát thuốc nhà thuốc theo bảng cấu hình
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetTheLowerKsCodeList(string keyword)
        {
            var data = _pharmacyDrugStorageDmnService.GetTheLowerKsCodeList(Constants.CurrentYfbm.yfbmCode, OrganizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Bộ phận nhập kho có quyền dùng thuốc này không
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="rkbm"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CheckRkbmOwnMedicine(string yp, string rkbm)
        {
            var isExist = sysPharmacyDepartmentMedicineRepo.CheckRkbmOwnMedicine(yp, rkbm, OrganizeId);
            return isExist ? Success() : Error("Xin lỗi! Bộ phận nhập kho không có quyền dùng thuốc này");
        }


        #endregion

        #region Kết chuyển tồn kho 2018-08

        /// <summary>
        /// Trang khung nhìn kết chuyển tồn kho
        /// </summary>
        /// <returns></returns>
        public IActionResult StockCarryDown()
        {
            return View();
        }

        /// <summary>
        /// Lấy thời gian kết chuyển lịch sử
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetlsjzDateTime()
        {
            var list = _sysMedicineStockCarryDownRepo.GetlsjzDateTime(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Thời gian kết thúc kết chuyển
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetJzJssjDateTime()
        {
            var list = _sysMedicineStockCarryDownRepo.GetlsjzDateTime(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            list.Insert(0, new Domain.Entity.V.KeyValueVEntity { key = "", value = "Hiện tại" });
            return Content(list.ToJson());
        }

        /// <summary>
        /// Truy vấn thời gian kết chuyển lần trước
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryLastCarryTime()
        {
            var lastCarryTime = _sysMedicineStockCarryDownRepo.GetLastJzData(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Success(null, lastCarryTime == null ? "" : lastCarryTime.Jzsj.ToString(Constants.DateTimeFormat));
        }

        /// <summary>
        /// Kết chuyển
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CarryOverMedicine()
        {
            _drugStorageApp.CarryOverMedicine(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Success();
        }

        /// <summary>
        /// Truy vấn thông tin thuốc đã kết chuyển
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jzsj"></param>
        /// <param name="keyWork"></param>
        /// <returns></returns>
        public IActionResult SelectCarryDownMedicineList(Pagination pagination, string jzsj, string keyWork)
        {
            var list = new
            {
                rows = _sysMedicineStockCarryDownDmnService.SelectCarryDownMedicineList(pagination, jzsj, keyWork),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        #endregion

        #region Thống kê nhập-xuất-tồn 2018-08

        /// <summary>
        /// Trang khung nhìn thống kê nhập xuất tồn
        /// </summary>
        /// <returns></returns>
        public IActionResult PSIStatistics2018()
        {
            ViewBag.YfbmCode = Constants.CurrentYfbm.yfbmCode;
            ViewBag.OrganizeId= this.OrganizeId;
            return View();
        }

        /// <summary>
        /// Thống kê nhập xuất tồn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        public IActionResult PSIStatisticsInfoList(Pagination pagination, PsiStatisticsParam searchParam)
        {
            var list = new
            {
                rows = _drugStorageDmnService.GetPsiStatisticsVo(pagination, searchParam, Constants.CurrentYfbm.yfbmCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        #endregion

        public IActionResult ExcelExportChooseColumns()
        {
            return View();
        }
        public IActionResult ExcelGet(string ksjzsj, string jsjzsj, string ypzt, string srm, string dl, string jx, string rate, string noPSI,
            bool? isContainFilter, int colStanWidth, string cols,string jxtext,string lbtext)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            PsiStatisticsParam sticsParam = new PsiStatisticsParam();
            sticsParam.ksjzsj = ksjzsj;
            sticsParam.jsjzsj = jsjzsj;
            sticsParam.ypzt = ypzt;
            sticsParam.srm = srm;
            sticsParam.dl = dl;
            sticsParam.jx = jx;
            sticsParam.rate = rate;
            sticsParam.noPSI = noPSI;
            if (string.IsNullOrWhiteSpace(cols))
            {
                cols = Request.Cookies["ExportExcelCols"];
                if (!string.IsNullOrWhiteSpace(cols))
                {
                    cols = System.Net.WebUtility.UrlDecode(cols);
                    Response.Cookies.Delete("ExportExcelCols");
                }
            }
            if (string.IsNullOrWhiteSpace(cols))
            {
                throw new FailedException("Chưa chỉ định cột xuất");
            }
            var pagination = new Pagination();
            pagination.sidx = "Ypdm desc"; //Thời gian tăng dần
            pagination.rows = 65536 - 1;    //Excel Số dòng tối đa
            pagination.page = 1;    //Trang đầu tiên truy vấn tất cả
            var list = _drugStorageDmnService.GetPsiStatisticsVo(pagination, sticsParam, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            var colList = cols.ToObject<IList<ExcelColumn>>();
            //var kflbItemDetails = _itemDmnService.GetItemsDetailListByOrgIdAndItemCode(orgId, "RehabTreatmentMethod");
            var sheet = new ExcelSheet()
            {
                Title = "Thống kê nhập-xuất-tồn",
                columns = colList,
            };
            //sheet.columns.Where(p => p.Name == "zlrq").ToList().ForEach(p =>
            //{
            //    p.DateTimeFormat = "yyyy-MM-dd";
            //});
            //sheet.columns.Where(p => p.Name == "kflb").ToList().ForEach(t => t.FuncGetCellValue = (obj) =>
            //{
            //    return kflbItemDetails.Where(p => p.Code == obj.ToString()).Select(p => p.Name).FirstOrDefault();
            //});
            sheet.columns.Where(p => p.Name == "qcpfze").ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "rkpfze").ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "ckpfze" ).ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "sypfze").ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "pyze").ToList().ForEach(t => 
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "pkze").ToList().ForEach(t => 
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "qmpfze").ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.Where(p => p.Name == "tjsyze").ToList().ForEach(t =>
            {
                t.NumberDigits = 2;
            });
            sheet.columns.ToList().ForEach(p =>
            {
                p.WidthTimes = (double)p.Width / colStanWidth;
                p.Width = 0;    //Width đều đặt thành0
            });

            var path = DateTime.Now.ToString("\\\\yyyyMMdd\\\\HHmmssfff")+"Thống kê nhập-xuất-tồn" + ".xls";
            var filePath = CommmHelper.GetLocalFilePath("\\Excel Xuất\\Xuất truy vấn kho thuốc" + path, "D:\\");

            if (isContainFilter == true)
            {
                //Điều kiện lọc
                var filterDict = new Dictionary<string, string>();
                filterDict.Add("Bộ phận xuất", Constants.CurrentYfbm.yfbmmc);
                if (!string.IsNullOrWhiteSpace(sticsParam.ksjzsj))
                {
                    filterDict.Add("Ngày bắt đầu", sticsParam.ksjzsj);
                }
                if (!string.IsNullOrWhiteSpace(sticsParam.jsjzsj))
                {
                    filterDict.Add("Ngày kết thúc", sticsParam.jsjzsj);
                }
                if (!string.IsNullOrWhiteSpace(sticsParam.ypzt))
                {
                    filterDict.Add("Trạng thái thuốc", sticsParam.ypzt=="1"?"Bật":"Ngừng dùng");
                }
                if (!string.IsNullOrWhiteSpace(sticsParam.rate))
                {
                    filterDict.Add("Tỷ lệ chênh lệch bằng 0", sticsParam.rate == "0" ? "Tỷ lệ chênh lệch bằng 0" : "");
                }
                if (!string.IsNullOrWhiteSpace(sticsParam.srm))
                {
                    filterDict.Add("Mã thuốc", sticsParam.srm);
                }
                if (!string.IsNullOrWhiteSpace(jxtext))
                {
                    filterDict.Add("Dạng bào chế", jxtext);
                }
                if (!string.IsNullOrWhiteSpace(lbtext))
                {
                    filterDict.Add("Loại thuốc", lbtext);
                }
                if (!string.IsNullOrWhiteSpace(sticsParam.noPSI))
                {
                    filterDict.Add("Hiển thị thuốc chưa phát sinh nhập-xuất-tồn", sticsParam.noPSI=="0"?"Hiển thị":"Không hiển thị");
                }
                if (filterDict.Count > 0)
                {
                    sheet.filters = filterDict;
                }
            }

            var rest = list.ToExcel(filePath, sheet);

            if (rest)
            {
                ViewBag.filePath = filePath;
                return File(System.IO.File.ReadAllBytes(filePath), "application/x-xls", path.Replace("\\", ""));
            }
            else
            {
                return Content("Xuất tệp thất bại, vui lòng quay lại trang danh sách thử lại");
            }
        }


        #region Lấy thông tin thuốc và tồn kho của bộ phận này

        /// <summary>
        /// Lấy thuốc còn hạn dùng,Không phân lô
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="ypYfbmCode"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetValidMedicinesrmList(string srm, string ypYfbmCode)
        {
            return Content(_drugStorageDmnService.GetValidMedicinesrmList(srm, ypYfbmCode, OrganizeId).ToJson());
        }

        #endregion

        #region Nhập kho nhanh

        /// <summary>
        /// Nhập kho nhanh Khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult BriefInStorage()
        {
            return View();
        }
        #endregion

        #region Xuất kho hàng loạt

        /// <summary>
        /// Xuất kho trực tiếp hàng loạt
        /// </summary>
        /// <returns></returns>
        public IActionResult DirectDeliveryBatch()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Lấy phương thức xuất nhập kho
        /// </summary>
        /// <param name="crkbz">0:Nhập kho  1:Xuất kho</param>
        /// <returns></returns>
        public IActionResult GetCrkfs(string crkbz)
        {
            var result = _iSysMedicineCrkfsDmnService.GetList(crkbz);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy thông tin thuốc xuất kho trực tiếp
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="rkbm"></param>
        /// <returns></returns>
        public IActionResult GetDirectDeliveryDrugsList(Pagination pagination, string rkbm)
        {
            var list = new
            {
                rows = _handOutMedicineDmnService.GetDirectDeliveryDrugsList(pagination, Constants.CurrentYfbm.yfbmCode, rkbm, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Gửi xuất kho hàng loạt
        /// </summary>
        /// <param name="rkbm">Bộ phận nhập kho</param>
        /// <param name="ypCodes">Thuốc Phân tách bằng dấu phẩy</param>
        /// <param name="djh">Số chứng từ</param>
        /// <param name="fyfs">Mã phương thức phát thuốc</param>
        /// <returns></returns>
        public IActionResult DirectDeliveryBatchSubmit(string rkbm, string ypCodes, string djh, string fyfs)
        {
            try
            {
                // G28 F6: guard đầu vào (trước khi gọi proc).
                if (string.IsNullOrWhiteSpace(ypCodes)) return Error("Thuốc không được để trống");
                if (string.IsNullOrWhiteSpace(rkbm)) return Error("Bộ phận nhập kho không được để trống.");
                if (rkbm.Trim().Length > 100) return Error("Mã bộ phận nhập kho không được dài quá 100 ký tự.");
                if (string.IsNullOrWhiteSpace(djh)) return Error("Số chứng từ không được để trống.");
                if (djh.Trim().Length > 30) return Error("Số chứng từ không được dài quá 30 ký tự.");
                if (string.IsNullOrWhiteSpace(fyfs)) return Error("Phương thức phát thuốc không được để trống.");
                if (fyfs.Trim().Length > 10) return Error("Mã phương thức phát thuốc không được dài quá 10 ký tự.");
                var tli = ypCodes.Trim().Split(',');
                var li = tli.Select(i => i.FilterSql()).Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
                if (li.Count == 0) return Error("Thuốc không được để trống");
                var param = new DirectDeliveryBatchDTO
                {
                    Organizeid = OrganizeId,
                    djh = djh,
                    crkId = Guid.NewGuid().ToString(),
                    djlx = (int)EnumDanJuLX.piliangchuku,
                    rkbm = rkbm,
                    yfbmCode = Constants.CurrentYfbm.yfbmCode,
                    userCode = OperatorProvider.GetCurrent().UserCode,
                    crkfs = fyfs,
                    shzt = ((int)EnumDjShzt.Approved).ToString(),
                    ypCodes = string.Join(",", li)
                };
                var result = _handOutMedicineDmnService.DirectDeliveryBatchSubmit(param);
                // G28 F6: proc trả về GUID crkId khi thành công, text lỗi khi thất bại —
                // trước đây controller bọc cả hai trong Success (fake Success).
                if (string.IsNullOrWhiteSpace(result)) return Error("Xuất kho trực tiếp hàng loạt thất bại.");
                if (!Guid.TryParse(result.Trim(), out _)) return Error(result);
                return Success(result);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }
        #endregion
    }
}