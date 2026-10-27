using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.NonTreatmentItemManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("NonTreatmentItemManage")]

    public class NonTreatmentItemController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly INonTreatmentItemDmnService _nonTreatmentItemDmnService;
        private readonly INonTreatmentItemBillingRepo _nonTreatmentItemBillingRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        public NonTreatmentItemController(INonTreatmentItemDmnService nonTreatmentItemDmnService, INonTreatmentItemBillingRepo nonTreatmentItemBillingRepo, ISysConfigRepo sysConfigRepo)
        {
            _nonTreatmentItemDmnService = nonTreatmentItemDmnService;
            _nonTreatmentItemBillingRepo = nonTreatmentItemBillingRepo;
            _sysConfigRepo = sysConfigRepo;
        }



        // GET: NonTreatmentItemManage/NonTreatmentItemDmnServices
        public IActionResult Bookkeeping()
        {
            //Có hiển thị số nhập viện không
            var IsShowZyh = _sysConfigRepo.GetValueByCode("NonTreatItem_ShowZyh", this.OrganizeId);
            ViewBag.IsShowZyh = IsShowZyh;
            return View();
        }
        public IActionResult BookkeepingQuery()
        {           
            //Có hiển thị số nhập viện không
            var IsShowZyh = _sysConfigRepo.GetValueByCode("NonTreatItem_ShowZyh", this.OrganizeId);
            ViewBag.IsShowZyh = IsShowZyh;
            return View();
        }
        public IActionResult BookkeepingRefund()
        {
            //Có hiển thị số nhập viện không
            var IsShowZyh = _sysConfigRepo.GetValueByCode("NonTreatItem_ShowZyh", this.OrganizeId);
            ViewBag.IsShowZyh = IsShowZyh;
            return View();
        }

        public IActionResult RefundDetailConfirmForm()
        {
            return View();
        }

        #region Khoản mục ghi sổ
        /// <summary>
        /// căndữ liệubệnhlịchsốđượclấypatIdvà họ tên
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public IActionResult SelectPatientInfoByblhOrzyh(string blh, string zyh, string xm)
        {
            var data = _nonTreatmentItemDmnService.SelectPatientInfoByblhOrzyh(this.OrganizeId, blh, zyh, xm);
            return Content(data.ToJson());
        }

        /// <summary>
        /// bảo hiểmlưughitài khoản
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IActionResult SaveBilling(string data)
        {
            var jzList = JsonConvert.DeserializeObject<List<NonTreatmentItemBillingEntity>>(data);
            List<NonTreatmentItemBillingEntity> billingList = new List<NonTreatmentItemBillingEntity>();
            foreach (var item in jzList)
            {
                NonTreatmentItemBillingEntity entity = new NonTreatmentItemBillingEntity();
                entity.jfbId = Guid.NewGuid().ToString();
                entity.OrganizeId = this.OrganizeId;
                entity.zyh = item.zyh;
                entity.sfxmCode = item.sfxmCode;
                entity.dlCode = item.dlCode;
                entity.sl = item.sl;
                entity.dj = item.dj;
                entity.je = item.je;
                entity.blh = item.blh;
                entity.patId = item.patId;
                entity.xm = item.xm;
                entity.sfxmCode = item.sfxmCode;
                entity.smry = item.smry;
                entity.smksCode = item.smksCode;
                entity.tjr = item.tjr;
                entity.cxry = null;
                entity.cxrq = null;
                entity.cxjfbId = null;
                entity.jzrq = item.jzrq;
                entity.zt = "1";
                entity.px = null;
                entity.Create();
                billingList.Add(entity);
            }
            _nonTreatmentItemBillingRepo.SaveBilling(billingList);
            return Success();
        }
        #endregion

        #region ghi sổ Tra cứu
        //thuphíphânloạisốdữ liệunguồn
        [HttpGet]

        public IActionResult GetTreeSelectChargeCategoryJson()
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var data = _nonTreatmentItemDmnService.GetChargeCategoryTreeList(orgId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.dlCode;
                treeModel.text = item.dlmc;
                treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                    ? data.Where(p => p.dlId == item.ParentId).Select(p => p.dlCode).FirstOrDefault()
                    : null;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// Tra cứu
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectNonTreatmentItemList(Pagination pagination, string sfdl, string keyword, string smry, string smks, DateTime? kssj, DateTime? jssj,string kehu, string zyh, string blh)
        {
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                pagination.sidx = pagination.sidx + ", CreateTime desc";
            }
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var list = new
            {
                rows = _nonTreatmentItemDmnService.SelectNonTreatmentItemList(pagination, sfdl, keyword, smry, smks, kssj, jssj, orgId, kehu, zyh, blh),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        #endregion

        #region ghi sổ Hoàn phí
        /// <summary>
        /// Tra cứu
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="smry"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IActionResult SelectRefundItemList(Pagination pagination, string zyh, string blh, string keyword, string smry, string kehu, DateTime? kssj, DateTime? jssj)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var list = new
            {
                rows = _nonTreatmentItemDmnService.SelectRefundItemList(pagination, zyh, blh, keyword, smry, kehu, kssj, jssj, orgId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// bảo hiểmlưuhoànphí
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IActionResult SaveRefund(string data)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var jzList = JsonConvert.DeserializeObject<List<NonTreatmentItemBillingInfoVO>>(data);
            List<NonTreatmentItemBillingEntity> billingList = new List<NonTreatmentItemBillingEntity>();
            foreach (var item in jzList)
            {
                NonTreatmentItemBillingEntity entity = new NonTreatmentItemBillingEntity();
                entity.jfbId = Guid.NewGuid().ToString();
                entity.OrganizeId = orgId;
                entity.zyh = item.zyh;
                entity.sfxmCode = item.sfxmCode;
                entity.dlCode = item.dlCode;
                entity.sl = 0-item.tsl.Value;    //Số lượng hoàn
                entity.dj = item.dj.Value;    //Số lượng hoàn
                entity.je = 0-item.tje.Value;    //hoàncủatiềnmức
                entity.cxry = this.UserIdentity.UserCode;   //hủyhủyngườiviên
                entity.cxrq = DateTime.Now;    //hủyhủyngàykỳ
                entity.cxjfbId = item.jfbId;    //hủyhủyđốiphảicủajfbId
                entity.blh = item.blh;
                entity.patId = item.patId;
                entity.xm = item.xm;
                entity.sfxmCode = item.sfxmCode;
                entity.smry = item.smry;
                entity.smksCode = item.smksCode;
                entity.tjr = item.tjr;
                entity.jzrq = item.jzrq;
                entity.zt = "1";
                entity.px = null;
                entity.Create();
                billingList.Add(entity);
            }
            _nonTreatmentItemBillingRepo.SaveBilling(billingList);
            return Success();
        }

        #endregion
    }
}
