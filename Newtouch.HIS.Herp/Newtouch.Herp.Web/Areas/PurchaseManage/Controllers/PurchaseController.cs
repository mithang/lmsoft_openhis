using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.DTO.InputDto.Purchase;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.DTO.OutputDto.Purchase;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IDomainServices.Purchase;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.IRepository.Purchase;
using Newtouch.Herp.Domain.ValueObjects.Purchase;
using Newtouch.Herp.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static Newtouch.Herp.Domain.DTO.OutputDto.OutputYY156;

namespace Newtouch.Herp.Web.Areas.PurchaseManage.Controllers
{
    public class PurchaseController :   OrgControllerBase
    {
        // GET: PurchaseManage/Purchase

        private readonly IPurchaseRepo _purchaseRepo;
        private readonly IPurchaseDetailRepo _purchaseDetailRepo;
        private readonly IPurchaseDmnService _purchaseDmnService;
        private readonly ISunPurchaseDmnService _sunPurchaseDmnService;
        private readonly IGysSupplierRepo _gysSupplierRepo;
        private readonly IPurchaseLocationRepo _purchaseLocationRepo;
        private readonly IPurchaseBillRepo _purchaseBillRepo;

        #region Trang
        //Quản lý mua hàng
        public ActionResult Index()
        {
            return View();
        }

        //Thêm mới/sửa mua hàng
        public ActionResult Form()
        {
            return View();
        }
        
        //Danh sách nhập mua hàng
        public ActionResult PurchaseView()
        {
            return View();
        }

        //Tra cứu và nghiệm thu hóa đơn
        public ActionResult Bill()
        {
            return View();
        }

        //Lấy nghiệm thu phiếu giao hàng
        public ActionResult Delivery() {
            return View();
        }
        
        //Trả hàng
        public ActionResult ReIndex()
        {
            return View();
        }
        
        //Chi tiết trả hàng
        public ActionResult ReForm()
        {
            return View();
        }

        //Nghiệm thu hóa đơn
        public ActionResult BillForm()
        {
            return View();
        }
        //Nghiệm thu phiếu giao hàng
        public ActionResult DeliveryForm()
        {
            return View();
        }

        //Xác nhận thanh toán
        public ActionResult BillPay() {
            return View();
        }

        //Hộp bật xác nhận thanh toán
        public ActionResult BillPayForm() {
            return View();
        }

        //Điểm giao hàng
        public ActionResult LocationIndex() {
            return View();
        }

        //Thêm điểm giao hàng mới
        public ActionResult LocationForm() {
            return View();
        }

        #endregion

        #region Trang tra cứu

        //YY151 Lấy thông tin chi tiết mua hàng vật tư tiêu hao(YY151)
        public ActionResult QueryYY151() {
            return View();
        }

        //YY154 Lấy phiếu giao hàng vật tư tiêu hao(YY154)
        public ActionResult QueryYY154()
        {
            return View();
        }

        //YY155 Lấy chi tiết giao hàng vật tư tiêu hao(YY155)
        //public ActionResult QueryYY155()
        //{
        //    return View();
        //}

        //YY157 Lấy thông tin hóa đơn vật tư tiêu hao(YY157)
        public ActionResult QueryYY157()
        {
            return View();
        }

        //YY158 Lấy chi tiết hóa đơn vật tư tiêu hao(YY158)
        //public ActionResult QueryYY158()
        //{
        //    return View();
        //}

        //YY152 Lấy thông tin chi tiết trả hàng vật tư tiêu hao(YY152)
        public ActionResult QueryYY152()
        {
            return View();
        }

        //YY159 Lấy trạng thái chi tiết mua hàng vật tư tiêu hao theo đơn mua hàng(YY159)
        public ActionResult QueryYY159()
        {
            return View();
        }

        //YY160 Lấy trạng thái hóa đơn vật tư tiêu hao(YY160)
        public ActionResult QueryYY160()
        {
            return View();
        }

        //YY161 Lấy trạng thái phiếu giao hàng vật tư tiêu hao(YY161)
        public ActionResult QueryYY161()
        {
            return View();
        }

        //YY162 Trạng thái chi tiết trả hàng lấy theo phiếu trả hàng vật tư tiêu hao(YY162)
        public ActionResult QueryYY162()
        {
            return View();
        }

        //YY164 Lấy thông tin doanh nghiệp(YY164)

        public ActionResult QueryYY164()
        {
            return View();
        }

        #endregion


        #region Quản lý mua hàng

        /// <summary>
        /// Tra cứu phân trang danh sách đơn mua hàng
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj, int ddzt)
        {
            var tt = _purchaseRepo.GetPurchaseGridJson(pagination, kssj, jssj, OrganizeId,ddzt);
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
        public ActionResult PurchaseStateUpdate(string cgId, int ddzt)
        {
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }
        /// <summary>
        /// Báo cáo đơn đặt mua hàng
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <returns></returns>
        public ActionResult PurchaseUpload(string cgId, int ddzt)
        {
            //1.Tải lên nền tảng mua sắm Sunshine YY111
            var ddbh=_sunPurchaseDmnService.Purchase_YY111(this.OrganizeId, cgId);
            //2.Cập nhật ddbh
            _purchaseRepo.PurchaseDdbhUpdate(cgId, ddbh, this.OrganizeId);
            //3.Xác nhận kê khai đơn hàng YY112
            _sunPurchaseDmnService.Purchase_YY112(this.OrganizeId, cgId);
            //4.Cập nhật trạng thái báo cáo
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }
        #endregion

        #region Kiểm tra đơn đặt mua hàng

        /// <summary>
        /// Báo cáo đơn đặt mua hàng
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <returns></returns>
        public ActionResult PurchaseUploadWriteTest(string cgId, int ddzt)
        {
            //1.Tải lên nền tảng mua sắm Sunshine YY111
            var ddbh = _sunPurchaseDmnService.Purchase_YY111(this.OrganizeId, cgId);
            //2.Cập nhật ddbh
            _purchaseRepo.PurchaseDdbhUpdate(cgId, ddbh, this.OrganizeId);
            return Success();
        }

        /// <summary>
        /// Báo cáo đơn đặt mua hàng
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <returns></returns>
        public ActionResult PurchaseUploadSureTest(string cgId, int ddzt)
        {
            //3.Xác nhận kê khai đơn hàng YY112
            _sunPurchaseDmnService.Purchase_YY112(this.OrganizeId, cgId);
            //4.Cập nhật trạng thái báo cáo
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }
        #endregion

        #region Biểu mẫu chi tiết mua hàng
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="cgEntity"></param>
        /// <param name="cgmxList"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitPurchase(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;

            cgEntity.OrganizeId = this.OrganizeId;
            cgEntity.jls = cgmxList.Count;
            // 1.Thêm mới hoặc cập nhật danh sách mua hàng
            var cgId = _purchaseRepo.SubmitForm(cgEntity, keyValue);
            //2.Xóa chi tiết mua hàng
            if (keyValue != null)
            {
                _purchaseDetailRepo.DeleteItem(cgId, this.OrganizeId);
            }
            //3.Thêm chi tiết mua hàng
            foreach (var cgmx in cgmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _purchaseDetailRepo.InsertItem(cgmx, cgId);
            }
            return Success();
        }

        /// <summary>
        /// Lưu + Báo cáo
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
            // 1.Thêm mới hoặc cập nhật danh sách mua hàng
            var cgId = _purchaseRepo.SubmitForm(cgEntity, keyValue);
            //2.Xóa chi tiết mua hàng
            if (keyValue != null)
            {
                _purchaseDetailRepo.DeleteItem(cgId, this.OrganizeId);
            }
            //3.Thêm chi tiết mua hàng
            foreach (var cgmx in cgmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _purchaseDetailRepo.InsertItem(cgmx, cgId);
            }
            //4.Kê khai và báo cáo đơn đặt mua hàng
            PurchaseUpload(cgId, 2);
            return Success();
        }
        #endregion

        #region Nhập/xuất kho
        //public ActionResult QueryPurchaseStorebyId(string cgId, string keyword)
        //{

        //    var result = _purchaseDmnService.QueryPurchaseStorebyId(cgId, this.OrganizeId, Constants.CurrentYfbm.yfbmCode);
        //    return Content(result.ToJson());
        //}
        #endregion

        #region Tra cứu và nghiệm thu hóa đơn
        public ActionResult GetBillGridJson(string qybm, string fpmxbhcxtj)
        {
            var result = _sunPurchaseDmnService.Purchase_YY156(OrganizeId, qybm, fpmxbhcxtj);
            //if (result != null && result.DETAIL != null)
            //{
            //    List<OutputStructYY156> list = result.DETAIL.STRUCT;
            //    return Content(list.ToJson());
            //}
            return Content(result.ToJson());

        }

        public ActionResult BillAccept(PurchaseMainYY132 main)
        {
            //1.Nghiệm thu trên nền tảng mua sắm Sunshine
            var result = _sunPurchaseDmnService.Purchase_YY132(main, this.OrganizeId);
            //2.Dữ liệu hóa đơn được lưu xuống
            _purchaseBillRepo.InsertItem(main, this.OrganizeId);
            return Success();
        }
        #endregion

        #region Lấy nghiệm thu phiếu giao hàng
        public ActionResult GetDeliveryGridJson(string qybm, string psmxbhcxtj)
        {
            var result = _sunPurchaseDmnService.Purchase_YY153(OrganizeId, qybm, psmxbhcxtj);
            return Content(result.ToJson());
        }

        public ActionResult DeliveryAccept(PurchaseDeliveryVO vo)
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
            return Success();
        }

        //public ActionResult DeliveryAccept(PurchaseMainYY131 main, PurchaseStructYY131 st)
        //{
        //    List<PurchaseStructYY131> list = new List<PurchaseStructYY131>();
        //    list.Add(st);
        //    //var result = _sunPurchaseDmnService.Purchase_YY131(main, st, this.OrganizeId);
        //    return Success();
        //}
        #endregion


        #region Nhập kho ngoài Nhập đơn mua hàng
        public ActionResult QueryPurchaseStorebyId(string cgId, string keyword)
        {

            var result = _purchaseDmnService.QueryPurchaseStorebyId(cgId, this.OrganizeId, Constants.CurrentKf.currentKfId);
            return Content(result.ToJson());
        }
        #endregion

        #region Xác nhận thanh toán hóa đơn
        public ActionResult GetBillPayGridJson(PurchaseMainYY157 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<PurchaseYY157VO> result = new List<PurchaseYY157VO>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY157 entity = _sunPurchaseDmnService.Purchase_YY157(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        if (obj.FPZT == "20" || obj.FPZT == "30")//Lọc trạng thái hóa đơn:20 Đã nghiệm thu 30 Đã thanh toán
                        {
                            PurchaseYY157VO vo = new PurchaseYY157VO();
                            vo.ZFBZ = obj.FPZT == "30" ? "已支付" : "未支付";
                            vo.QYMC = _gysSupplierRepo.IQueryable(p => p.py == obj.QYBM && p.OrganizeId == this.OrganizeId && p.zt == "1").FirstOrDefault().name;
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
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPBH;
                main.FPBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }


        public ActionResult BillPayAccept(PurchaseMainYY133 main)
        {
            var result = _sunPurchaseDmnService.Purchase_YY133(main, this.OrganizeId);
            return Success();
        }
        #endregion

        #region Điểm giao hàng

        //Tra cứu điểm giao hàng
        public ActionResult QueryLocation()
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt == "1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).ToList();


            return Content(result.ToJson());
        }

        //Tra cứu phân trang điểm giao hàng
        public ActionResult GetLocationGridJson(Pagination pagination)
        {
            var tt = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt=="1").OrderByDescending(p => p.CreateTime).ToList();

            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        //Chi tiết điểm giao hàng (Lớp nổi)
        public ActionResult QueryLocationbyId(string Id)
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.Id==Id && p.OrganizeId == this.OrganizeId && p.zt=="1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            return Content(result.ToJson());
        }

        public ActionResult SubmitLocation(PurchaseLocationEntity entity)
        {
            _purchaseLocationRepo.SubmitForm(entity, this.OrganizeId,entity.Id);
            return Success();
        }

        //Báo cáo điểm giao hàng
        public ActionResult LocationUpload(string Id,int psdzt, string czlx)
        {
            //1.Tải lên nền tảng mua sắm Sunshine YY101
            var result=_sunPurchaseDmnService.Purchase_YY101(this.OrganizeId, Id,czlx);
            //2.Cập nhật trạng thái báo cáo (2 Đã báo cáo)
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
            }
            return Success();
        }

        //Hủy điểm giao hàng
        public ActionResult LocationDelete(string Id, int psdzt, string czlx)
        {
            //1.Hủy bỏ trên nền tảng mua sắm Sunshine
            var result=_sunPurchaseDmnService.Purchase_YY101(this.OrganizeId, Id, czlx);
            //2.HIS Hủy điểm giao hàng ( Cập nhật trạng thái điểm giao hàng psdzt 3 Đã hủy bỏ )
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
            }
            return Success();
        }
        #endregion

        #region Trang tra cứu
        //YY154 Lấy phiếu giao hàng vật tư tiêu hao(YY154)
        public ActionResult GetGridJsonYY154(PurchaseMainYY154 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY154> result = new List<OutputStructYY154>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY154 entity = _sunPurchaseDmnService.Purchase_YY154(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        obj.QYBM = _gysSupplierRepo.IQueryable().Where(p => p.py == obj.QYBM && p.OrganizeId == this.OrganizeId && p.zt == "1").FirstOrDefault().name;
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHPSDBH;
                main.PSDBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY155 Lấy chi tiết giao hàng vật tư tiêu hao(YY155)
        public ActionResult GetGridJsonYY155(PurchaseMainYY155 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY155> result = new List<OutputStructYY155>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY155 entity = _sunPurchaseDmnService.Purchase_YY155(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHPSMXBH;
                main.PSMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY151 Lấy thông tin chi tiết mua hàng vật tư tiêu hao(YY151)
        public ActionResult GetGridJsonYY151(PurchaseMainYY151 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY151> result = new List<OutputStructYY151>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY151 entity = _sunPurchaseDmnService.Purchase_YY151(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHDDMXBH;
                main.DDMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }
        
        //YY152 Lấy thông tin chi tiết trả hàng vật tư tiêu hao(YY152)
        public ActionResult GetGridJsonYY152(PurchaseMainYY152 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY152> result = new List<OutputStructYY152>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY152 entity = _sunPurchaseDmnService.Purchase_YY152(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHTHMXBH;
                main.THMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY157 Lấy trạng thái hóa đơn vật tư tiêu hao(YY157)
        public ActionResult GetGridJsonYY157(PurchaseMainYY157 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY157> result = new List<OutputStructYY157>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY157 entity = _sunPurchaseDmnService.Purchase_YY157(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        obj.QYBM = _gysSupplierRepo.IQueryable().Where(p => p.py == obj.QYBM && p.OrganizeId == this.OrganizeId && p.zt == "1").FirstOrDefault().name;
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPBH;
                main.FPBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY158 Lấy chi tiết hóa đơn vật tư tiêu hao(YY158)
        public ActionResult GetGridJsonYY158(PurchaseMainYY158 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY158> result = new List<OutputStructYY158>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY158 entity = _sunPurchaseDmnService.Purchase_YY158(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPMXBH;
                main.FPMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY159 Lấy trạng thái chi tiết mua hàng vật tư tiêu hao theo đơn mua hàng(YY159)
        public ActionResult GetGridJsonYY159(PurchaseMainYY159 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY159> result = new List<OutputStructYY159>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY159 entity = _sunPurchaseDmnService.Purchase_YY159(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHDDMXBH;
                main.DDMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY160 Lấy trạng thái hóa đơn vật tư tiêu hao(YY160)
        public ActionResult GetGridJsonYY160(PurchaseMainYY160 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY160> result = new List<OutputStructYY160>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY160 entity = _sunPurchaseDmnService.Purchase_YY160(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPBH;
                main.FPBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY161 Lấy trạng thái phiếu giao hàng vật tư tiêu hao(YY161)
        public ActionResult GetGridJsonYY161(PurchaseMainYY161 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY161> result = new List<OutputStructYY161>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY161 entity = _sunPurchaseDmnService.Purchase_YY161(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHPSMXBH;
                main.PSMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }
 
        //YY162 Trạng thái chi tiết trả hàng lấy theo phiếu trả hàng vật tư tiêu hao(YY162)
        public ActionResult GetGridJsonYY162(PurchaseMainYY162 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY162> result = new List<OutputStructYY162>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY162 entity = _sunPurchaseDmnService.Purchase_YY162(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và số hóa đơn cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHTHMXBH;
                main.THMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }
 
        //YY164 Lấy thông tin doanh nghiệp(YY164)
        public ActionResult GetGridJsonYY164(PurchaseMainYY164 main)
        {
            var sfwj = "0";//Đã kết thúc chưa  
            var bh = "";//Điều kiện tra cứu theo mã số Lần gọi đầu tiên không điền
            List<OutputStructYY164> result = new List<OutputStructYY164>();
            while (sfwj == "0")//Chưa kết thúc
            {
                //1.Gọi giao diện
                OutputYY164 entity = _sunPurchaseDmnService.Purchase_YY164(main, this.OrganizeId);
                //2.Chèn vào danh sách
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.Cập nhật trạng thái kết thúc và Số hiệu cuối của lần này
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHQYBM;
                main.QYBMCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        #endregion
    }
}