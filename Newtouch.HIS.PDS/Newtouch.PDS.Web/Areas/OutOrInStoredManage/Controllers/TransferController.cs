using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Core.Common;
using Newtouch.Core.Redis;
using Newtouch.HIS.Application.Implementation.Process;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.DomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    [Area("OutOrInStoredManage")]
    public class TransferController : ControllerBase
    {
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;
        private readonly ISysMedicineStorageIOReceiptRepo _djRepo;
        private readonly ISysMedicineStorageIOReceiptDetailRepo _djmxRepo;
        private readonly IApplyDmnService _applyDmnService;
        private readonly IDrugStorageDmnService _drugStorageDmnService;
        private readonly IHandOutMedicineDmnService _handOutMedicineDmnService;
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacy;

        // NX2-001: file goc thieu ctor nen 7 field readonly khong bao gio
        // duoc gan (runtime NRE). Ca 7 service/repo da dang ky Singleton
        // trong Program.cs (:44, :56, :57, :81, :132, :133, :147).
        public TransferController(IPharmacyDrugStorageDmnService pharmacyDrugStorageDmnService,
            ISysMedicineStorageIOReceiptRepo djRepo,
            ISysMedicineStorageIOReceiptDetailRepo djmxRepo,
            IApplyDmnService applyDmnService,
            IDrugStorageDmnService drugStorageDmnService,
            IHandOutMedicineDmnService handOutMedicineDmnService,
            ISysPharmacyDepartmentMedicineRepo sysPharmacy)
        {
            _pharmacyDrugStorageDmnService = pharmacyDrugStorageDmnService;
            _djRepo = djRepo;
            _djmxRepo = djmxRepo;
            _applyDmnService = applyDmnService;
            _drugStorageDmnService = drugStorageDmnService;
            _handOutMedicineDmnService = handOutMedicineDmnService;
            _sysPharmacy = sysPharmacy;
        }

        /// <summary>
        /// Xin điều chuyển 
        /// GET: OutOrInStoredManage/Transfer/Apply
        /// </summary>
        /// <returns></returns>
        public IActionResult Apply()
        {
            return View();
        }

        public IActionResult GenerateNewDjh()
        {
            const string _prefix = "DBSQD";
            var numbStr = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            var numbInt = Convert.ToInt64(numbStr);
            var newDjh = $"{_prefix}{numbStr}";
            // NX2-001: RedisHelper mo ho giua Newtouch.Core.Redis va
            // Newtouch.Tools (ca 2 deu co trong Shims, cung uy thac ve 1
            // store in-memory). Fully-qualify ve Core.Redis (ban wrapper).
            while (Newtouch.Core.Redis.RedisHelper.Exists(newDjh))
            {
                numbInt += 1;
                newDjh = $"{_prefix}{numbInt}";
            }
            Newtouch.Core.Redis.RedisHelper.StringSet(newDjh, numbInt.ToString(), (DateTime.Now.AddMinutes(2) - DateTime.Now));

            return Content(newDjh);
        }

        /// <summary>
        /// Lấy danh sách nhà thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetYfbmList(string keyword)
        {
            var data = _pharmacyDrugStorageDmnService.GetYfbmList(keyword, OrganizeId);
            if (data == null || data.Count == 0)
            {
                return Content((new List<object>()).ToJson());
            }

            var targetCode = data.Where(p => p.yfbmCode != Constants.CurrentYfbm.yfbmCode)?.Select(p => new { p.yfbmCode, p.yfbmmc }).ToList();
            return Content(targetCode.ToJson());
        }

        /// <summary>
        /// submit
        /// </summary>
        /// <param name="djInfoDTO"></param>
        /// <returns></returns>
        public IActionResult SubmitApply(DjInfoDTO djInfoDTO)
        {
            #region Chứng từ xuất nhập kho

            var dj = new SysMedicineStorageIOReceiptEntity
            {
                crkId = Guid.NewGuid().ToString(),
                OrganizeId = OrganizeId,
                djlx = (int)EnumDanJuLX.shenqingdiaobo,
                Pdh = djInfoDTO.djh,
                Ckbm = djInfoDTO.ckbm,
                Rkbm = Constants.CurrentYfbm.yfbmCode,
                Sqsj = DateTime.Now,
                Czsj = DateTime.Now,
                Crkfsdm = "12",
                zt = "1",
                shzt = ((int)EnumDjShzt.WaitingApprove).ToString(),
                CreateTime = DateTime.Now,
                CreatorCode = UserIdentity.UserCode
            };
            var djmx = new List<SysMedicineStorageIOReceiptDetailEntity>();
            foreach (var item in djInfoDTO.mx)
            {
                item.sldmxId = Guid.NewGuid().ToString();
                djmx.Add(new SysMedicineStorageIOReceiptDetailEntity
                {
                    crkmxId = Guid.NewGuid().ToString(),
                    Ypdm = item.ypdm,
                    Sl = item.sl,
                    Rkbmkc = 0,
                    Rkzhyz = (int)item.rkzhyz,
                    Ckbmkc = (int)item.ckbmkc,
                    Ckzhyz = (int)item.ckzhyz,
                    crkId = dj.crkId,
                    jj = item.jj,
                    Lsj = item.lsj,
                    pc = "",
                    Ph = "",
                    Pfj = item.pfj,
                    sldmxId = item.sldmxId,
                    Yklsj = item.yklsj,
                    Ykpfj = item.ykpfj,
                    Yxq = null,
                    Zje = (decimal)item.zxdwjj * item.sl * (int)item.rkzhyz,
                    zt = "1",
                    CreateTime = DateTime.Now,
                    CreatorCode = dj.CreatorCode,
                });
            }
            // NX2-001: khong co extension List<T>.IsEmpty trong codebase;
            // Count == 0 la equivalent truc tiep, khong them extension moi.
            if (djmx.Count == 0)
            {
                return Error("Vui lòng nhập chi tiết thuốc điều chuyển");
            }
            if (_djRepo.Insert(dj) <= 0)
            {
                return Error("Lưu đơn xin điều chuyển thất bại");
            }

            _djmxRepo.Insert(djmx);

            #endregion

            #region Đơn lĩnh

            // NX2-001: dong goc `djInfoDTO.djlx = (int)EnumSldlx.shenqingdiaobodan;`
            // da XOA — EnumSldlx khong ton tai o bat cu dau trong repo, va gia
            // tri nay khong co consumer: ApplyProcess (consumer duy nhat cua
            // DjInfoDTO o flow nay) khong doc Request.djlx (Validata chi check
            // mx, BeforeAction khong copy djlx, SysMedicineRequisitionEntity
            // khong co field sldlx), ProcessorBase cung khong doc djlx. Xoa
            // dong chet, khong bia gia tri enum. Xem bao cao NX2-001.
            djInfoDTO.rkbm = Constants.CurrentYfbm.yfbmCode;
            var result = new ApplyProcess(djInfoDTO).Process();
            if (!result.IsSucceed)
            {
                _applyDmnService.Cancel(dj.crkId, UserIdentity.UserCode);
            }

            #endregion

            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }

        /// <summary>
        /// Nhập kho điều chuyển
        /// GET: OutOrInStoredManage/Transfer/Warehousing
        /// </summary>
        /// <returns></returns>
        public IActionResult Warehousing()
        {
            return View();
        }

        /// <summary>
        /// Lấy thông tin bảng chính đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh">Số đơn lĩnh</param>
        /// <param name="fybm">Bộ phận xuất kho</param>
        /// <param name="ffzt">Trạng thái phát thuốc</param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <returns></returns>
        public IActionResult RequestInfo(string sldh, string fybm, string ffzt, DateTime startDate, DateTime endDate)
        {
            ffzt = string.IsNullOrWhiteSpace(ffzt) ? "-1" : ffzt;
            var rows = _drugStorageDmnService.GetTransferOrders(sldh, Constants.CurrentYfbm.yfbmCode, fybm, ffzt, startDate, endDate);
            return Content(rows.ToJson());
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
            var data = _drugStorageDmnService.GetSldInfo(sldId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Nhập kho điều chuyển
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IActionResult ExecHandOutMedicineByReq(Dictionary<string, List<SysMedicineReDetailVO>> rkdmx)
        {
            var res = _handOutMedicineDmnService.TransferWarehousing(rkdmx, Constants.CurrentYfbm.yfbmCode, UserIdentity.UserCode, OrganizeId);
            return string.IsNullOrWhiteSpace(res) ? Success() : Error(res);
        }

        /// <summary>
        /// Lấy danh sách thuốc cần lĩnh
        /// </summary>
        /// <param name="rkbm"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DrugStockSearch(string keyword, string yfbmCode)
        {
            var result = _drugStorageDmnService.GetDrugStock(yfbmCode, Constants.CurrentYfbm.yfbmCode, keyword, OrganizeId);
            return Content(result.ToJson());
        }
    }
}