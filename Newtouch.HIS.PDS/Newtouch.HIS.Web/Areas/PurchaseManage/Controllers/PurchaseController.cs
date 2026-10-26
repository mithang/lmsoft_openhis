using FrameworkBase.MultiOrg.Web;
using Newtouch.HIS.Domain.Entity.Purchase;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository.Purchase;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web.Areas.PurchaseManage.Controllers
{
    public class PurchaseController : OrgControllerBase
    {
        // GET: PurchaseManage/Purchase

        private readonly ISunPurchaseDmnService _sunPurchaseDmnService;
        private readonly IPurchaseLocationRepo _purchaseLocationRepo;

        #region 页面
        public ActionResult Index()
        {
            return View();
        }

        //điểm phân phối
        public ActionResult LocationIndex()
        {
            return View();
        }

        //điểm phân phối thêm mới
        public ActionResult LocationForm()
        {
            return View();
        }
        #endregion


        #region 配送点

        //tra cứu điểm phân phối
        public ActionResult QueryLocation()
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt == "1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).ToList();


            return Content(result.ToJson());
        }

        //phần trang tra cứu điểm phân phối
        public ActionResult GetLocationGridJson(Pagination pagination)
        {
            var tt = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt == "1").OrderByDescending(p => p.CreateTime).ToList();

            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        //điểm phân phối chi tiết (lớp nổi)
        public ActionResult QueryLocationbyId(string Id)
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.Id == Id && p.OrganizeId == this.OrganizeId && p.zt == "1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            return Content(result.ToJson());
        }

        public ActionResult SubmitLocation(PurchaseLocationEntity entity)
        {
            _purchaseLocationRepo.SubmitForm(entity, this.OrganizeId, entity.Id);
            return Success();
        }

        //điểm phân phối gửi báo cáo
        public ActionResult LocationUpload(string Id, int psdzt, string czlx)
        {
            //1.tải lên đến nền tảng mua sắm Sunshine YY001
            var result = _sunPurchaseDmnService.Purchase_YY001(this.OrganizeId, Id, czlx);
            //2.cập nhật gửi báo cáo trạng thái (2 đã gửi báo cáo)
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
            }
            return Success();
        }

        //điểm phân phối hủy bỏ
        public ActionResult LocationDelete(string Id, int psdzt, string czlx)
        {
            //1.nền tảng mua sắm Sunshine hủy bỏ
            var result = _sunPurchaseDmnService.Purchase_YY001(this.OrganizeId, Id, czlx);
            //2.HIS hủy bỏ điểm phân phối (cập nhật điểm phân phối trạng thái psdzt 3 đã hủy)
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
            }
            return Success();
        }
        #endregion


    }
}