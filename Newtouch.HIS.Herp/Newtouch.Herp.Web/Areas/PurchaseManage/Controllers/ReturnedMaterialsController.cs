using FrameworkBase.MultiOrg.Web;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IDomainServices.Purchase;
using Newtouch.Herp.Domain.IRepository.Purchase;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using static Newtouch.Herp.Domain.DTO.OutputDto.OutputYY156;

namespace Newtouch.Herp.Web.Areas.PurchaseManage.Controllers
{
    public class ReturnedMaterialsController :   OrgControllerBase
    {

        private readonly IReturnedRepo _returnedRepo;
        private readonly IReturnedDetailRepo _returnedDetailRepo;
        private readonly IPurchaseDmnService _purchaseDmnService;
        private readonly ISunPurchaseDmnService _sunPurchaseDmnService;

        #region Nội dung phiếu trả hàng
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="cgEntity"></param>
        /// <param name="cgmxList"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitPurchase(ReturnedEntity thEntity, List<ReturnedDetailEntity> thmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;

            thEntity.OrganizeId = this.OrganizeId;
            thEntity.JLS = thmxList.Count;
            // 1.Thêm mới hoặc cập nhật danh sách mua hàng
            var thId = _returnedRepo.SubmitForm(thEntity, keyValue);
            //2.Xóa chi tiết mua hàng
            if (keyValue != null)
            {
                _returnedDetailRepo.DeleteItem(thId, this.OrganizeId);
            }
            //3.Thêm chi tiết mua hàng
            foreach (var cgmx in thmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _returnedDetailRepo.InsertItem(cgmx, thId);
            }
            return Success();
        }
        public ActionResult SubmitAndUploadPurchase(ReturnedEntity thEntity, List<ReturnedDetailEntity> thmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;

            thEntity.OrganizeId = this.OrganizeId;
            thEntity.JLS = thmxList.Count;
            // 1.Thêm mới hoặc cập nhật danh sách mua hàng
            var thId = _returnedRepo.SubmitForm(thEntity, keyValue);
            //2.Xóa chi tiết mua hàng
            if (keyValue != null)
            {
                _returnedDetailRepo.DeleteItem(thId, this.OrganizeId);
            }
            //3.Thêm chi tiết mua hàng
            foreach (var cgmx in thmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _returnedDetailRepo.InsertItem(cgmx, thId);
            }
            //4.Trả hàng và báo cáo đơn đặt mua hàng
            PurchaseUpload(thId, 2);
            return Success();
        }
        #endregion
        /// <summary>
        /// Tra cứu phân trang dữ liệu phiếu trả thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj)
        {
            var tt = _returnedRepo.GetReturnedGridJson(pagination, kssj, jssj, OrganizeId);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); 
        }


        public ActionResult QueryPurchaseReturnbyId(string thId)
        {
            var result = _purchaseDmnService.QueryPurchaseReturnbyId(thId, this.OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult QueryPurchaseReturnDetailbyId(string thId)
        {
            var result = _purchaseDmnService.QueryPurchaseReturnDetailbyId(thId, this.OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult PurchaseReturnDelete(string thId)
        {
            _returnedRepo.PurchaseReturnDelete(thId, this.OrganizeId);
            return Success();
        }
        public ActionResult PurchaseStateUpdate(string thId, int thdzt)
        {
            _returnedRepo.PurchaseReturnStateUpdate(thId, thdzt, this.OrganizeId);
            return Success();
        }

        /// <summary>
        /// Báo cáo phiếu trả hàng mua hàng
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <returns></returns>
        public ActionResult PurchaseUpload(string thId, int ddzt)
        {
            //1.Tải lên nền tảng mua sắm Sunshine YY113
            var ddbh=_sunPurchaseDmnService.Purchase_YY113(this.OrganizeId, thId);
            //2.Lấy YY113 Số đơn hàng của  (YY113 Bảng đích)
            //var ddbh = "00001";
            //3.Cập nhật ddbh
            _returnedRepo.PurchaseDdbhUpdate(thId, ddbh, this.OrganizeId);
            //4.Xác nhận kê khai đơn hàng YY114
            _sunPurchaseDmnService.Purchase_YY114(this.OrganizeId, thId);
            //Cập nhật trạng thái báo cáo
            _returnedRepo.PurchaseReturnStateUpdate(thId, ddzt, this.OrganizeId);
            return Success();
        }
    }
}