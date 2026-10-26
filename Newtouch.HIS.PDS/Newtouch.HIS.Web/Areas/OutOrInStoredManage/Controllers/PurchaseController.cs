using FrameworkBase.MultiOrg.Web;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO.PharmacyDrugStorage;
using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository.Purchase;
using Newtouch.HIS.Domain.VO;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    public class PurchaseController : OrgControllerBase
    {
        private readonly IPurchaseRepo _purchaseRepo;
        private readonly IPurchaseDetailRepo _purchaseDetailRepo;
        private readonly IPurchaseDmnService _purchaseDmnService;
        private readonly IPurchaseApp _purchaseApp;
        private readonly IDrugStorageDmnService _drugStorageDmnService;
        private readonly ISunPurchaseDmnService _sunPurchaseDmnService;
        private readonly IPurchaseBillRepo _purchaseBillRepo;
        private readonly IPurchaseBillDetailRepo _purchaseBillDetailRepo;

        // GET: OutOrInStoredManage/Purchase
        #region 页面
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Form()
        {
            return View();
        }

        public ActionResult PurchaseAdd()
        {
            return View();
        }

        //mua hàng nhập danh sách
        public ActionResult PurchaseView()
        {
            return View();
        }

        //hóa đơn nhập danh sách
        public ActionResult BillView() {
            return View();
        }

        //tra cứu hóa đơn và nghiệm thu
        public ActionResult Bill()
        {
            return View();
        }

        //hóa đơn nghiệm thu
        public ActionResult BillForm()
        {
            return View();
        }

        //phân phối đơn lấy nghiệm thu
        public ActionResult Delivery()
        {
            return View();
        }
        /// <summary>
        /// Tạo kế hoạch mua hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchasePlan()
        {
            return View();
        }
        /// <summary>
        /// Nhập chứng từ kế hoạch mua hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchasePlanFrom() {
            return View();
        }
        /// <summary>
        /// Duyệt chứng từ kế hoạch mua hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchaseApproval()
        {
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
        #endregion


        #region 采购管理

        /// <summary>
        /// phần trang tra cứu mua hàng đơn danh sách
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj, int ddzt,string yyjhdh= null,string gysCode=null,string ddbh=null, string ddyy = null)
        {
            var tt = _purchaseRepo.GetPurchaseGridJson(pagination, kssj, jssj, OrganizeId, ddzt, yyjhdh, gysCode,ddbh,ddyy);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        public ActionResult QueryPurchasebyId(string cgId)
        {
            var result = _purchaseDmnService.QueryPurchasebyId(cgId, this.OrganizeId);
            return Content(result.ToJson());
        }
        public ActionResult QueryPurchaseDetailbyId(string cgId)
        {
            var result = _purchaseDmnService.QueryPurchaseDetailbyId(cgId, this.OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult PurchaseDelete(string cgId)
        {
            _purchaseRepo.PurchaseDelete(cgId, this.OrganizeId);
            return Success();
        }
        /// <summary>
        /// mua hàng đơn trạng thái thay đổi 1: chưa duyệt 2: duyệt 3: hủy bỏ
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <returns></returns>
        public ActionResult PurchaseStateUpdate(string cgId, int ddzt)
        {
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }
        /// <summary>
        /// mua hàng đơn hàng gửi báo cáo
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <returns></returns>
        public ActionResult PurchaseUpload(string cgId, int ddzt)
        {
            //1.tải lên đến nền tảng mua sắm Sunshine YY009
            var ddbh = _sunPurchaseDmnService.Purchase_YY009(this.OrganizeId, cgId);
            //2.cập nhật ddbh
            _purchaseRepo.PurchaseDdbhUpdate(cgId, ddbh, this.OrganizeId);
            //4.đơn hàng khai báo xác nhận YY010
            _sunPurchaseDmnService.Purchase_YY010(this.OrganizeId, cgId);
            //5.cập nhật gửi báo cáo trạng thái
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }
        #endregion

        #region 采购订单测试
        public ActionResult PurchaseUploadWriteTest(string cgId, int ddzt)
        {
            //1.tải lên đến nền tảng mua sắm Sunshine YY009
            var ddbh = _sunPurchaseDmnService.Purchase_YY009(this.OrganizeId, cgId);
            //2.cập nhật ddbh
            _purchaseRepo.PurchaseDdbhUpdate(cgId, ddbh, this.OrganizeId);
            return Success();
        }
        public ActionResult PurchaseUploadSureTest(string cgId, int ddzt)
        {
            //3.đơn hàng khai báo xác nhận YY010
            _sunPurchaseDmnService.Purchase_YY010(this.OrganizeId, cgId);
            //4.cập nhật gửi báo cáo trạng thái
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }

        #endregion

        #region 采购详情表单
        /// <summary>
        /// lưu
        /// </summary>
        /// <param name="cgEntity"></param>
        /// <param name="cgmxList"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitPurchase(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;

            cgEntity.OrganizeId = this.OrganizeId;
            cgEntity.yyjhdh = "YPCGD"+DateTime.Now.ToString("yyyyMMddHHmm");
            // 1.thêm mới hoặc cập nhật mua hàng danh sách
            var cgId = _purchaseRepo.SubmitForm(cgEntity, keyValue);
            //2.xóa mua hàng chi tiết
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _purchaseDetailRepo.DeleteItem(cgId, this.OrganizeId);
            }
            //3.thêm mua hàng chi tiết
            foreach (var cgmx in cgmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _purchaseDetailRepo.InsertItem(cgmx, cgId);
            }
            return Success();
        }

        /// <summary>
        /// lưu + gửi báo cáo
        /// </summary>
        /// <param name="cgEntity"></param>
        /// <param name="cgmxList"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitAndUploadPurchase(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;

            cgEntity.OrganizeId = this.OrganizeId;
            cgEntity.jls = cgmxList.Count;
            // 1.thêm mới hoặc cập nhật mua hàng danh sách
            var cgId = _purchaseRepo.SubmitForm(cgEntity, keyValue);
            //2.xóa mua hàng chi tiết
            if (keyValue != null)
            {
                _purchaseDetailRepo.DeleteItem(cgId, this.OrganizeId);
            }
            //3.thêm mua hàng chi tiết
            foreach (var cgmx in cgmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _purchaseDetailRepo.InsertItem(cgmx, cgId);
            }
            //4.mua hàng đơn hàng khai báo và gửi báo cáo
            PurchaseUpload(cgId, 2);
            return Success();
        }
        #endregion

        #region 出入库
        /// <summary>
        /// lấy mua hàng nhập kho đơn chi tiết
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult QueryPurchaseStorebyId(string cgId, string keyword)
        {

            var result = _purchaseDmnService.QueryPurchaseStorebyId(cgId, this.OrganizeId, Constants.CurrentYfbm.yfbmCode);
            return Content(result.ToJson());
        }
        /// <summary>
        /// Bổ sung số hóa đơn cho đơn mua hàng
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public ActionResult UpdateFph(string cgId, string ddbh)
        {
            _purchaseRepo.PurchaseDdbhUpdate(cgId, ddbh, this.OrganizeId);
            return Success();
        }
        #endregion

        #region 按发票出入库-阳光平台
        public ActionResult GetBillStoreGridJson(Pagination pagination, DateTime kssj, DateTime jssj,string fph, string fpzt)
        {
            var tt = _purchaseBillRepo.GetBillStoreGridJson(pagination, kssj, jssj, fph, OrganizeId, fpzt);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        public ActionResult QueryBillStorebyId(string fph)
        {

            var result = _purchaseDmnService.QueryBillStorebyId(fph, this.OrganizeId, Constants.CurrentYfbm.yfbmCode);
            return Content(result.ToJson());
        }

        #endregion


        #region 发票查询与验收
        public ActionResult GetBillGridJson(string yqbm, string fpmxbh)
        {
            //var list = new List<OutputStructYY004>();
            //for (var i = 0; i < 2; i++)
            //{
            //    OutputStructYY004 obj = new OutputStructYY004();
            //    obj.FPH = "000" + i;
            //    obj.FPRQ = "2023-07-12";
            //    obj.FPHSZJE = 200;
            //    obj.YQBM = "BLDYY001";
            //    list.Add(obj);
            //}
            //return Content(list.ToJson());

            ////Kiểm tra tra cứu hóa đơn
            //var list = new List<OutputStructYY004>();
            //OutputStructYY004 obj = new OutputStructYY004();
            //obj.FPH = "07832501";
            //obj.FPRQ = "20230815";
            //obj.FPHSZJE = 7057;
            //obj.YQBM = "GYZDB001";
            //obj.YYBM = "77370296000";
            //obj.PSDBM = "77370296000";
            //obj.DLSGBZ = "0";
            //obj.SFWPSFP = "1";
            //obj.FPMXBH = "20230815010084651141";
            //obj.SPLX = "1";
            //obj.SFCH = "0";
            //obj.ZXSPBM = "XN01BBA064B002010178765";
            //obj.SCPH = "V - 87";
            //obj.SCRQ = "20230514";
            //obj.SPSL = 20;
            //obj.GLMXBH = "20230815000100891374";
            //obj.XSDDH = "12303891304207832501";
            //obj.SXH = 1;
            //obj.YXRQ = "20250513";
            //obj.WSDJ = decimal.Parse("312.2566");
            //obj.HSDJ = decimal.Parse("352.85");
            //obj.SL = 13;
            //obj.SE = decimal.Parse("811.87");
            //obj.HSJE = 7057;
            //obj.PFJ = decimal.Parse("352.86");
            //obj.LSJ = decimal.Parse("405.8");
            //obj.PZWH = "H20140732";
            //list.Add(obj);
            //OutputStructYY004 obj2 = new OutputStructYY004();
            //obj2.FPH = "07832502";
            //obj2.FPRQ = "20230815";
            //obj2.FPHSZJE = 780;
            //obj2.YQBM = "GYZDB001";
            //obj2.YYBM = "77370296000";
            //obj2.PSDBM = "77370296000";
            //obj2.DLSGBZ = "0";
            //obj2.SFWPSFP = "1";
            //obj2.FPMXBH = "20230815010084651143";
            //obj2.SPLX = "1";
            //obj2.SFCH = "0";
            //obj2.ZXSPBM = "XS02AAY040Q010010100830";
            //obj2.SCPH = "230202";
            //obj2.SCRQ = "20230203";
            //obj2.SPSL = 100;
            //obj2.GLMXBH = "20230815000100891373";
            //obj2.XSDDH = "12303891304407832502";
            //obj2.SXH = 1;
            //obj2.YXRQ = "20250131";
            //obj2.WSDJ = decimal.Parse("6.9027");
            //obj2.HSDJ = decimal.Parse("7.8");
            //obj2.SL = 13;
            //obj2.SE = decimal.Parse("89.73");
            //obj2.HSJE = 780;
            //obj2.PFJ = decimal.Parse("7.8");
            //obj2.LSJ = decimal.Parse("8.97");
            //obj2.PZWH = "số chuẩn thuốc quốc gia H10950207";
            //list.Add(obj2);
            //return Content(list.ToJson());

            //var list = new List<OutputStructYY004>();
            //OutputStructYY004 obj3 = new OutputStructYY004();
            //obj3.FPH = "03100220010508178424";
            //obj3.FPRQ = "20230628";
            //obj3.FPHSZJE = decimal.Parse("61.1");
            //obj3.YQBM = "SYSDZD01";
            //obj3.YYBM = "77370296000";
            //obj3.PSDBM = "77370296000";
            //obj3.DLSGBZ = "0";
            //obj3.SFWPSFP = "1";
            //obj3.FPMXBH = "20230628010082660484";
            //obj3.SPLX = "1";
            //obj3.SFCH = "0";
            //obj3.ZXSPBM = "X01211740010012";
            //obj3.SCPH = "2301072";
            //obj3.SCRQ = "20230129";
            //obj3.SPSL = 5;
            //obj3.GLMXBH = "20230627000098852552";
            //obj3.XSDDH = "506202306280335";
            //obj3.SXH = 4;
            //obj3.YXRQ = "20251231";
            //obj3.WSDJ = decimal.Parse("10.814");
            //obj3.HSDJ = decimal.Parse("12.22");
            //obj3.SL = 13;
            //obj3.SE = decimal.Parse("7.03");
            //obj3.HSJE = decimal.Parse("61.1");
            //obj3.PFJ = decimal.Parse("10.8142");
            //obj3.LSJ = decimal.Parse("61.1");
            //obj3.PZWH = "số chuẩn thuốc quốc gia H32024827";
            //list.Add(obj3);
            //return Content(list.ToJson());


            //nền tảng mua sắm Sunshine tra cứu hóa đơn
            var result = _sunPurchaseDmnService.Purchase_YY004(OrganizeId, yqbm, fpmxbh);
            return Content(result.ToJson());

        }

        //chi tiết hóa đơn được ghi nhận
        public ActionResult BillDetailInsert(OutputStructYY004 entity)
        {
            _purchaseBillDetailRepo.InsertItem(entity, this.OrganizeId);
            return Success();
        }

        public ActionResult BillAccept(PurchaseMainYY019 main)
        {
            //1.nền tảng mua sắm Sunshine nghiệm thu
            var result = _sunPurchaseDmnService.Purchase_YY019(main, this.OrganizeId);
            //2.Dữ liệu hóa đơn lưu xuống bảng
            _purchaseBillRepo.InsertItem(main, this.OrganizeId);
            return Success();
        }
        #endregion


        #region 配送单获取验收

        //phân phối đơn tra cứu
        public ActionResult GetDeliveryGridJson(string qybm, string psmxbh)
        {
            var result = _sunPurchaseDmnService.Purchase_YY003(OrganizeId, qybm, psmxbh);
            return Content(result.ToJson());
        }

        //phân phối đơn nghiệm thu
        public ActionResult DeliveryAccept(PurchaseMainYY018 main)
        {
            var result = _sunPurchaseDmnService.Purchase_YY018(main, this.OrganizeId);
            return Success();
        }
        #endregion


        #region
        public ActionResult PurchaseReturnUpload(string thId, int ddzt)
        {
            //1.tải lên đến nền tảng mua sắm Sunshine YY011
            var ddbh = _sunPurchaseDmnService.Purchase_YY011(this.OrganizeId, thId);
            //2.lấy YY011 của số đơn hàng  (bảng lưu YY011)
            //var ddbh = "00001";
            //3.cập nhật ddbh
            //_returnedRepo.PurchaseDdbhUpdate(thId, ddbh, this.OrganizeId);
            //4.đơn hàng khai báo xác nhận YY012
            _sunPurchaseDmnService.Purchase_YY012(this.OrganizeId, thId);
            //cập nhật gửi báo cáo trạng thái
            //_returnedRepo.PurchaseReturnStateUpdate(thId, ddzt, this.OrganizeId);
            return Success();
        }
        #endregion
    }
}