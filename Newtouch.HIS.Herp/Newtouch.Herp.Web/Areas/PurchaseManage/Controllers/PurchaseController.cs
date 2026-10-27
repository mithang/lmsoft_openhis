using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Herp.Domain.DTO.InputDto.Purchase;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.DTO.OutputDto.Purchase;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IDomainServices.Purchase;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.IRepository.Purchase;
using Newtouch.Herp.Domain.ValueObjects.Purchase;
using Newtouch.Herp.Infrastructure;

namespace Newtouch.Herp.Web.Areas.PurchaseManage.Controllers
{
    /// <summary>
    /// Quản lý mua sắm (port .NET 6 của PurchaseController gốc).
    /// Integration update: IGysSupplierRepo (area Supplier) nay đã tích hợp
    /// và inject trực tiếp qua constructor (trước đây resolve lười qua
    /// IServiceProvider vì Supplier chưa được port).
    /// Các action gọi dịch vụ mua sắm tập trung (Sun/YY) trả lỗi rõ ràng khi
    /// dịch vụ không khả dụng, không bao giờ trả 404 hay dữ liệu giả.
    /// </summary>
    [Area("PurchaseManage")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepo _purchaseRepo;
        private readonly IPurchaseDetailRepo _purchaseDetailRepo;
        private readonly IPurchaseDmnService _purchaseDmnService;
        private readonly ISunPurchaseDmnService _sunPurchaseDmnService;
        private readonly IGysSupplierRepo _gysSupplierRepo;
        private readonly IPurchaseLocationRepo _purchaseLocationRepo;
        private readonly IPurchaseBillRepo _purchaseBillRepo;

        public PurchaseController(IPurchaseRepo purchaseRepo, IPurchaseDetailRepo purchaseDetailRepo,
            IPurchaseDmnService purchaseDmnService, ISunPurchaseDmnService sunPurchaseDmnService,
            IGysSupplierRepo gysSupplierRepo, IPurchaseLocationRepo purchaseLocationRepo,
            IPurchaseBillRepo purchaseBillRepo)
        {
            _purchaseRepo = purchaseRepo;
            _purchaseDetailRepo = purchaseDetailRepo;
            _purchaseDmnService = purchaseDmnService;
            _sunPurchaseDmnService = sunPurchaseDmnService;
            _gysSupplierRepo = gysSupplierRepo;
            _purchaseLocationRepo = purchaseLocationRepo;
            _purchaseBillRepo = purchaseBillRepo;
        }

        private IActionResult SunUnavailable() =>
            Error("Dịch vụ mua sắm tập trung hiện không khả dụng, vui lòng thử lại sau.");

        #region Trang

        public new IActionResult Index() => View();
        public new IActionResult Form() => View();
        public IActionResult PurchaseView() => View();
        public IActionResult Bill() => View();
        public IActionResult Delivery() => View();
        public IActionResult ReIndex() => View();
        public IActionResult ReForm() => View();
        public IActionResult BillForm() => View();
        public IActionResult DeliveryForm() => View();
        public IActionResult BillPay() => View();
        public IActionResult BillPayForm() => View();
        public IActionResult LocationIndex() => View();
        public IActionResult LocationForm() => View();

        #endregion

        #region Trang truy vấn

        public IActionResult QueryYY151() => View();
        public IActionResult QueryYY154() => View();
        public IActionResult QueryYY157() => View();
        public IActionResult QueryYY152() => View();
        public IActionResult QueryYY159() => View();
        public IActionResult QueryYY160() => View();
        public IActionResult QueryYY161() => View();
        public IActionResult QueryYY162() => View();
        public IActionResult QueryYY164() => View();

        #endregion

        #region Quản lý mua sắm

        public IActionResult GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj, int ddzt)
        {
            var tt = _purchaseRepo.GetPurchaseGridJson(pagination, kssj, jssj, OrganizeId, ddzt);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public IActionResult QueryPurchasebyId(string cgId)
        {
            var result = _purchaseDmnService.QueryPurchasebyId(cgId, this.OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult QueryPurchaseDetailbyId(string cgId)
        {
            var result = _purchaseDmnService.QueryPurchaseDetailbyId(cgId, this.OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult PurchaseDelete(string cgId)
        {
            _purchaseRepo.PurchaseDelete(cgId, this.OrganizeId);
            return Success();
        }

        public IActionResult PurchaseStateUpdate(string cgId, int ddzt)
        {
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }

        /// <summary>Đơn mua sắm truyền báo</summary>
        public IActionResult PurchaseUpload(string cgId, int ddzt)
        {
            var ddbh = _sunPurchaseDmnService.Purchase_YY111(this.OrganizeId, cgId);
            if (ddbh == null) return SunUnavailable();
            _purchaseRepo.PurchaseDdbhUpdate(cgId, ddbh, this.OrganizeId);
            var confirm = _sunPurchaseDmnService.Purchase_YY112(this.OrganizeId, cgId);
            if (!string.IsNullOrEmpty(confirm)) return Error("Xác nhận truyền báo đơn mua sắm thất bại: " + confirm);
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }

        #endregion

        #region Kiểm thử truyền báo đơn mua sắm

        public IActionResult PurchaseUploadWriteTest(string cgId, int ddzt)
        {
            var ddbh = _sunPurchaseDmnService.Purchase_YY111(this.OrganizeId, cgId);
            if (ddbh == null) return SunUnavailable();
            _purchaseRepo.PurchaseDdbhUpdate(cgId, ddbh, this.OrganizeId);
            return Success();
        }

        public IActionResult PurchaseUploadSureTest(string cgId, int ddzt)
        {
            var confirm = _sunPurchaseDmnService.Purchase_YY112(this.OrganizeId, cgId);
            if (!string.IsNullOrEmpty(confirm)) return Error("Xác nhận truyền báo đơn mua sắm thất bại: " + confirm);
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }

        #endregion

        #region Biểu mẫu chi tiết mua sắm

        public IActionResult SubmitPurchase(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;
            cgmxList = cgmxList ?? new List<PurchaseDetailEntity>();
            cgEntity.OrganizeId = this.OrganizeId;
            cgEntity.jls = cgmxList.Count;
            var cgId = _purchaseRepo.SubmitForm(cgEntity, keyValue);
            if (keyValue != null)
            {
                _purchaseDetailRepo.DeleteItem(cgId, this.OrganizeId);
            }
            foreach (var cgmx in cgmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _purchaseDetailRepo.InsertItem(cgmx, cgId);
            }
            return Success();
        }

        public IActionResult SubmitAndUploadPurchase(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;
            cgmxList = cgmxList ?? new List<PurchaseDetailEntity>();
            cgEntity.OrganizeId = this.OrganizeId;
            cgEntity.jls = cgmxList.Count;
            var cgId = _purchaseRepo.SubmitForm(cgEntity, keyValue);
            if (keyValue != null)
            {
                _purchaseDetailRepo.DeleteItem(cgId, this.OrganizeId);
            }
            foreach (var cgmx in cgmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _purchaseDetailRepo.InsertItem(cgmx, cgId);
            }
            return PurchaseUpload(cgId, 2);
        }

        #endregion

        #region Hóa đơn truy vấn và nghiệm thu

        public IActionResult GetBillGridJson(string qybm, string fpmxbhcxtj)
        {
            var result = _sunPurchaseDmnService.Purchase_YY156(OrganizeId, qybm, fpmxbhcxtj);
            if (result == null) return SunUnavailable();
            return Content(result.ToJson());
        }

        public IActionResult BillAccept(PurchaseMainYY132 main)
        {
            var result = _sunPurchaseDmnService.Purchase_YY132(main, this.OrganizeId);
            if (!string.IsNullOrEmpty(result)) return Error("Nghiệm thu hóa đơn thất bại: " + result);
            _purchaseBillRepo.InsertItem(main, this.OrganizeId);
            return Success();
        }

        #endregion

        #region Phiếu giao hàng lấy và nghiệm thu

        public IActionResult GetDeliveryGridJson(string qybm, string psmxbhcxtj)
        {
            var result = _sunPurchaseDmnService.Purchase_YY153(OrganizeId, qybm, psmxbhcxtj);
            if (result == null) return SunUnavailable();
            return Content(result.ToJson());
        }

        public IActionResult DeliveryAccept(PurchaseDeliveryVO vo)
        {
            PurchaseMainYY131 main = new PurchaseMainYY131();
            main.PSYSLX = vo.PSYSLX;
            main.JLS = 1;
            List<PurchaseStructYY131> list = new List<PurchaseStructYY131>();
            PurchaseStructYY131 st = new PurchaseStructYY131();
            st.HCTBDM = vo.HCTBDM;
            st.PSL = vo.PSL;
            st.PSMXBH = vo.PSMXBH;
            st.SCPH = vo.SCPH;
            st.YSBGS = vo.YSBGS;
            st.YSBZSM = vo.YSBZSM;
            st.YSTGS = vo.YSTGS;
            list.Add(st);
            var result = _sunPurchaseDmnService.Purchase_YY131(main, list, this.OrganizeId);
            if (!string.IsNullOrEmpty(result)) return Error("Nghiệm thu phiếu giao hàng thất bại: " + result);
            return Success();
        }

        #endregion

        #region Nhập kho ngoài: nhập đơn mua sắm

        public IActionResult QueryPurchaseStorebyId(string cgId, string keyword)
        {
            var result = _purchaseDmnService.QueryPurchaseStorebyId(cgId, this.OrganizeId, Constants.CurrentKf.currentKfId);
            return Content(result.ToJson());
        }

        #endregion

        #region Xác nhận thanh toán hóa đơn

        public IActionResult GetBillPayGridJson(PurchaseMainYY157 main)
        {
            var sfwj = "0";
            var bh = "";
            List<PurchaseYY157VO> result = new List<PurchaseYY157VO>();
            while (sfwj == "0")
            {
                OutputYY157 entity = _sunPurchaseDmnService.Purchase_YY157(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        if (obj.FPZT == "20" || obj.FPZT == "30")
                        {
                            PurchaseYY157VO vo = new PurchaseYY157VO();
                            vo.ZFBZ = obj.FPZT == "30" ? "Đã thanh toán" : "Chưa thanh toán";
                            vo.QYMC = _gysSupplierRepo.IQueryable(p => p.py == obj.QYBM && p.OrganizeId == this.OrganizeId && p.zt == "1").ToList().FirstOrDefault()?.name ?? obj.QYBM;
                            vo.FPBH = obj.FPBH;
                            vo.FPDM = obj.FPDM;
                            vo.FPH = obj.FPH;
                            vo.FPRQ = obj.FPRQ;
                            vo.FPHSZJE = obj.FPHSZJE;
                            vo.QYBM = obj.QYBM;
                            vo.YYBM = obj.YYBM;
                            vo.PSDBM = obj.PSDBM;
                            vo.FPZT = obj.FPZT;
                            result.Add(vo);
                        }
                    }
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPBH;
                main.FPBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult BillPayAccept(PurchaseMainYY133 main)
        {
            var result = _sunPurchaseDmnService.Purchase_YY133(main, this.OrganizeId);
            if (!string.IsNullOrEmpty(result)) return Error("Xác nhận thanh toán hóa đơn thất bại: " + result);
            return Success();
        }

        #endregion

        #region Điểm giao hàng

        public IActionResult QueryLocation()
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt == "1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).ToList();
            return Content(result.ToJson());
        }

        public IActionResult GetLocationGridJson(Pagination pagination)
        {
            var tt = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt == "1").OrderByDescending(p => p.CreateTime).ToList();
            pagination.records = tt.Count;
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public IActionResult QueryLocationbyId(string Id)
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.Id == Id && p.OrganizeId == this.OrganizeId && p.zt == "1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).ToList().FirstOrDefault();
            return Content(result.ToJson());
        }

        public IActionResult SubmitLocation(PurchaseLocationEntity entity)
        {
            _purchaseLocationRepo.SubmitForm(entity, this.OrganizeId, entity.Id);
            return Success();
        }

        public IActionResult LocationUpload(string Id, int psdzt, string czlx)
        {
            var result = _sunPurchaseDmnService.Purchase_YY101(this.OrganizeId, Id, czlx);
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
                return Success();
            }
            return Error("Truyền báo điểm giao hàng thất bại: " + result);
        }

        public IActionResult LocationDelete(string Id, int psdzt, string czlx)
        {
            var result = _sunPurchaseDmnService.Purchase_YY101(this.OrganizeId, Id, czlx);
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
                return Success();
            }
            return Error("Hủy điểm giao hàng thất bại: " + result);
        }

        #endregion

        #region Trang truy vấn (YY)

        public IActionResult GetGridJsonYY154(PurchaseMainYY154 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY154> result = new List<OutputStructYY154>();
            while (sfwj == "0")
            {
                OutputYY154 entity = _sunPurchaseDmnService.Purchase_YY154(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        obj.QYBM = _gysSupplierRepo.IQueryable().Where(p => p.py == obj.QYBM && p.OrganizeId == this.OrganizeId && p.zt == "1").ToList().FirstOrDefault()?.name ?? obj.QYBM;
                        result.Add(obj);
                    }
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHPSDBH;
                main.PSDBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult GetGridJsonYY155(PurchaseMainYY155 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY155> result = new List<OutputStructYY155>();
            while (sfwj == "0")
            {
                OutputYY155 entity = _sunPurchaseDmnService.Purchase_YY155(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    foreach (var obj in entity.DETAIL.STRUCT) result.Add(obj);
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHPSMXBH;
                main.PSMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult GetGridJsonYY151(PurchaseMainYY151 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY151> result = new List<OutputStructYY151>();
            while (sfwj == "0")
            {
                OutputYY151 entity = _sunPurchaseDmnService.Purchase_YY151(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    foreach (var obj in entity.DETAIL.STRUCT) result.Add(obj);
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHDDMXBH;
                main.DDMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult GetGridJsonYY152(PurchaseMainYY152 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY152> result = new List<OutputStructYY152>();
            while (sfwj == "0")
            {
                OutputYY152 entity = _sunPurchaseDmnService.Purchase_YY152(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    foreach (var obj in entity.DETAIL.STRUCT) result.Add(obj);
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHTHMXBH;
                main.THMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult GetGridJsonYY157(PurchaseMainYY157 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY157> result = new List<OutputStructYY157>();
            while (sfwj == "0")
            {
                OutputYY157 entity = _sunPurchaseDmnService.Purchase_YY157(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    foreach (var obj in entity.DETAIL.STRUCT)
                    {
                        obj.QYBM = _gysSupplierRepo.IQueryable().Where(p => p.py == obj.QYBM && p.OrganizeId == this.OrganizeId && p.zt == "1").ToList().FirstOrDefault()?.name ?? obj.QYBM;
                        result.Add(obj);
                    }
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPBH;
                main.FPBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult GetGridJsonYY158(PurchaseMainYY158 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY158> result = new List<OutputStructYY158>();
            while (sfwj == "0")
            {
                OutputYY158 entity = _sunPurchaseDmnService.Purchase_YY158(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    foreach (var obj in entity.DETAIL.STRUCT) result.Add(obj);
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPMXBH;
                main.FPMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult GetGridJsonYY159(PurchaseMainYY159 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY159> result = new List<OutputStructYY159>();
            while (sfwj == "0")
            {
                OutputYY159 entity = _sunPurchaseDmnService.Purchase_YY159(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    foreach (var obj in entity.DETAIL.STRUCT) result.Add(obj);
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHDDMXBH;
                main.DDMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult GetGridJsonYY160(PurchaseMainYY160 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY160> result = new List<OutputStructYY160>();
            while (sfwj == "0")
            {
                OutputYY160 entity = _sunPurchaseDmnService.Purchase_YY160(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    foreach (var obj in entity.DETAIL.STRUCT) result.Add(obj);
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPBH;
                main.FPBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult GetGridJsonYY161(PurchaseMainYY161 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY161> result = new List<OutputStructYY161>();
            while (sfwj == "0")
            {
                OutputYY161 entity = _sunPurchaseDmnService.Purchase_YY161(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    foreach (var obj in entity.DETAIL.STRUCT) result.Add(obj);
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHPSMXBH;
                main.PSMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult GetGridJsonYY162(PurchaseMainYY162 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY162> result = new List<OutputStructYY162>();
            while (sfwj == "0")
            {
                OutputYY162 entity = _sunPurchaseDmnService.Purchase_YY162(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    foreach (var obj in entity.DETAIL.STRUCT) result.Add(obj);
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHTHMXBH;
                main.THMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        public IActionResult GetGridJsonYY164(PurchaseMainYY164 main)
        {
            var sfwj = "0";
            var bh = "";
            List<OutputStructYY164> result = new List<OutputStructYY164>();
            while (sfwj == "0")
            {
                OutputYY164 entity = _sunPurchaseDmnService.Purchase_YY164(main, this.OrganizeId);
                if (entity == null) return SunUnavailable();
                if (entity.DETAIL != null && entity.DETAIL.STRUCT != null)
                {
                    foreach (var obj in entity.DETAIL.STRUCT) result.Add(obj);
                }
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHQYBM;
                main.QYBMCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        #endregion
    }
}
