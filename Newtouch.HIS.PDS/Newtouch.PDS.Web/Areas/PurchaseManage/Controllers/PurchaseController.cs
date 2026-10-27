using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.HIS.Domain.Entity.Purchase;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository.Purchase;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web.Areas.PurchaseManage.Controllers
{
    [Area("PurchaseManage")]
    public class PurchaseController : OrgControllerBase
    {
        // GET: PurchaseManage/Purchase

        private readonly ISunPurchaseDmnService _sunPurchaseDmnService;
        private readonly IPurchaseLocationRepo _purchaseLocationRepo;

        #region Trang
        public IActionResult Index()
        {
            return View();
        }

        //Điểm giao hàng
        public IActionResult LocationIndex()
        {
            return View();
        }

        //Thêm mới điểm giao hàng
        public IActionResult LocationForm()
        {
            return View();
        }
        #endregion


        #region Điểm phân phối

        //Truy vấn điểm giao hàng
        public IActionResult QueryLocation()
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt == "1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).ToList();


            return Content(result.ToJson());
        }

        //Truy vấn phân trang điểm giao hàng
        public IActionResult GetLocationGridJson(Pagination pagination)
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

        //Chi tiết điểm giao hàng (Lớp nổi)
        public IActionResult QueryLocationbyId(string Id)
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.Id == Id && p.OrganizeId == this.OrganizeId && p.zt == "1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            return Content(result.ToJson());
        }

        public IActionResult SubmitLocation(PurchaseLocationEntity entity)
        {
            _purchaseLocationRepo.SubmitForm(entity, this.OrganizeId, entity.Id);
            return Success();
        }

        //Truyền báo điểm giao hàng
        public IActionResult LocationUpload(string Id, int psdzt, string czlx)
        {
            //1.Tải lên nền tảng mua hàng Sunshine YY001
            var result = _sunPurchaseDmnService.Purchase_YY001(this.OrganizeId, Id, czlx);
            //2.Cập nhật trạng thái truyền báo (2 Đã truyền báo)
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
            }
            return Success();
        }

        //Hủy bỏ điểm giao hàng
        public IActionResult LocationDelete(string Id, int psdzt, string czlx)
        {
            //1.Hủy bỏ nền tảng mua hàng Sunshine
            var result = _sunPurchaseDmnService.Purchase_YY001(this.OrganizeId, Id, czlx);
            //2.HISHủy bỏ điểm giao hàng ( Cập nhật trạng thái điểm giao hàng psdzt 3Đã hủy bỏ )
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
            }
            return Success();
        }
        #endregion


    }
}