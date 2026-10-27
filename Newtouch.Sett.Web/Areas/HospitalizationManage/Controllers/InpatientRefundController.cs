using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.HospitalizationManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("HospitalizationManage")]
    public class InpatientRefundController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IInpatientRefundApp _inpatientRefundApp;
        private readonly IHospItemBillingRepo _hospItemBillingRepo;
        private readonly IInpatientRefundDmnService _inpatientRefundDmnService;
        private readonly IHospDrugBillingRepo _hospDrugBillingRepo;
        private readonly IBookkeepInHosDmnService _BookkeepInHosDmnService;
        private readonly ISysConfigRepo _SysConfigRepo;
        private readonly IHospDrugBillingRepo _hospdrugbillingRepo;
        public InpatientRefundController(IInpatientRefundApp inpatientRefundApp, IHospItemBillingRepo hospItemBillingRepo, IInpatientRefundDmnService inpatientRefundDmnService, IHospDrugBillingRepo hospDrugBillingRepo, IBookkeepInHosDmnService bookkeepInHosDmnService, ISysConfigRepo sysConfigRepo, IHospDrugBillingRepo hospdrugbillingRepo)
        {
            _inpatientRefundApp = inpatientRefundApp;
            _hospItemBillingRepo = hospItemBillingRepo;
            _inpatientRefundDmnService = inpatientRefundDmnService;
            _hospDrugBillingRepo = hospDrugBillingRepo;
            _BookkeepInHosDmnService = bookkeepInHosDmnService;
            _SysConfigRepo = sysConfigRepo;
            _hospdrugbillingRepo = hospdrugbillingRepo;
        }


        public override IActionResult  Index()
        {
            return View();

        }
        public IActionResult RefundDetailConfirmForm()
        {
            return View();
        }

        /// <summary>
        /// Truy vấn
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult InpatientRefundQuery(string zyh, DateTime? kssj, DateTime? jssj, string xmlb, string xmmc, string conf = null)
        {
            string ks = "";
            if (!string.IsNullOrWhiteSpace(conf))
            {
                ks = _SysConfigRepo.GetValueByCode(conf, OrganizeId);
            }
            var dto = _inpatientRefundApp.InpatientRefundQuery(zyh, kssj, jssj, ks, xmlb, xmmc);
            var ztlist = dto.InpatientSettleItemBO.TreatmentItemList.Where(p => p.ztbh != null).ToList();
            //Mẫuthu phíDữ liệuXử lý
            if (ztlist.Count()>0)
            {
                ztlist = ztlist.GroupBy(m => new { m.isYP, m.sfmbmc, m.tdrq, m.ztbh, m.ztsl,m.ztylsl, m.dcztbs }).Select(p =>
                new TreatmentItemFeeDetailVO {
                    isYP = p.Key.isYP,
                    jfbbh = p.Max(m => m.jfbbh),
                    yzwym="",
                    dw = "Bộ",
                    dj = Math.Round(Convert.ToDecimal(p.Sum(m => m.je) / p.Key.ztsl), 2, MidpointRounding.AwayFromZero),
                    sfxm = p.Key.sfmbmc,
                    dl = p.Key.sfmbmc,
                    dlmc = p.Key.sfmbmc,
                    sfxmmc = p.Key.sfmbmc,
                    CreateTime = p.Max(m => m.CreateTime),
                    jmje = Convert.ToDecimal(0.00),
                    jmbl = Convert.ToDecimal(0.00),
                    zfbl = Convert.ToDecimal(0.00),
                    zfxz = null,
                    ybbm =null,
                    tdrq =p.Key.tdrq,
                    sl= p.Key.ztsl ?? 0,
                    tsl= p.Key.ztsl ?? 0,
                    je=p.Sum(m=>m.je),
                    ylsl= p.Key.ztylsl ?? 0,
                    ztbh=p.Key.ztbh,
                    dcztbs=p.Key.dcztbs,
                    ztsl=p.Key.ztsl,
                    jfbbhs= string.Join(",", p.Select(s => s.jfbbh).ToArray())

                }).ToList();
            }
            dto.InpatientSettleItemBO.TreatmentItemList = dto.InpatientSettleItemBO.TreatmentItemList
                .Where(p => p.ztbh == null).ToList().Concat(ztlist).ToList();
            return Success("", dto);
        }
        /// <summary>
        ///LưuHoàn phí
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SaveRefund(string data,string zyh)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new FailedException("Thiếu chi tiết hoàn phí");
            }
            var list = JsonConvert.DeserializeObject<List<InpatientRefundJsonVO>>(data);
            //Bảng tính phí mục
            List<HospItemBillingEntity> xmjfbEntitylist = new List<HospItemBillingEntity>();
            //Bảng tính phí thuốc
            List<HospDrugBillingEntity> ypjfbEntitylist = new List<HospDrugBillingEntity>();
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.isYp))
                {
                    throw new FailedCodeException("HOSP_BILLING ITEMS_BILLING DRUGS_DISTINGUISH_BETWEEN_FAILURE");
                }
                //Có phải mục không điều trị hay không 0：không 1：là
                if (item.isYp == "1")
                {
                    if (string.IsNullOrEmpty(item.jfbbh))
                    {
                        throw new FailedCodeException("HOSPREFUND_YPJFBBH_IS_EMPTY");
                    }
                    var jfbbh = Convert.ToInt32(item.jfbbh);
                    var oldDrugEntity = _hospDrugBillingRepo.IQueryable().Where(a => a.jfbbh == jfbbh && a.zt=="1" && a.OrganizeId == orgId ).ToList().FirstOrDefault();
                    HospDrugBillingEntity entity = new HospDrugBillingEntity();
                    entity = oldDrugEntity.Clone();
                    entity.jfbbh = SettKeyHelper.GetNewPrimaryKeyInt("zy_ypjfb");
                    entity.sl = 0 - item.tsl.Value;
                    entity.cxzyjfbbh = oldDrugEntity.jfbbh;
                    entity.cxry = this.UserIdentity.UserCode;
                    entity.cxrq = DateTime.Now;
                    entity.Create();  
                    ypjfbEntitylist.Add(entity);
                }
                else if (item.isYp == "0")
                {
                    if (string.IsNullOrEmpty(item.jfbbh))
                    {
                        throw new FailedCodeException("HOSPREFUND_XMJFBBH_IS_EMPTY");
                    }
                    string [] jfbbhar ;
                    jfbbhar = string.IsNullOrWhiteSpace(item.jfbbhs) ? item.jfbbh.Split(',') : item.jfbbhs.Split(',');
                    foreach (var zh in jfbbhar)
                    {
                        var jfbbh = Convert.ToInt32(zh);
                        var oldItemEntity = _hospItemBillingRepo.IQueryable().Where(a => a.jfbbh == jfbbh && a.zt == "1" && a.OrganizeId == orgId).ToList().FirstOrDefault();

                        HospItemBillingEntity entity = new HospItemBillingEntity();
                        entity = oldItemEntity.Clone();
                        entity.jfbbh = SettKeyHelper.GetNewPrimaryKeyInt("zy_xmjfb");
                        entity.sl =string.IsNullOrWhiteSpace(entity.ztbh)? 0 - item.tsl.Value: 0 - (item.tsl.Value*(entity.sl/ entity.ztsl.Value));
                        entity.ztsl = string.IsNullOrWhiteSpace(entity.ztbh) ? 0-entity.ztsl : 0-(entity.ztsl - item.tsl);
                        entity.cxzyjfbbh = oldItemEntity.jfbbh;
                        entity.cxry = this.UserIdentity.UserCode;
                        entity.cxtdrq = DateTime.Now;
                        entity.Create();
                        xmjfbEntitylist.Add(entity);
                    }
                    
                }
            }
            //Lưu
            string result =_inpatientRefundDmnService.SaveRefund(xmjfbEntitylist, ypjfbEntitylist, zyh, orgId);
            if (string.IsNullOrWhiteSpace(result))
            {
                if (_hospdrugbillingRepo.getHckcWith(this.OrganizeId) > 0) {
                    //hoàncòntrừgiảmVật tư tiêu haoTồn kho
                    var hclist = xmjfbEntitylist.Where(m => m.yzwym == null && m.sfxm.Contains("wz")).ToList();//ghi sổ nhập việnnơimởVật tư tiêu hao
                    var yszhclist = xmjfbEntitylist.Where(m => m.yzwym != null && m.sfxm.Contains("wz")).ToList();//Bác sĩtrạmmởlậpVật tư tiêu hao
                    if (hclist.Count > 0)
                    {
                        string jfbhString = string.Join(",", xmjfbEntitylist.Where(m => m.sfxm.Contains("wz")).Select(p => p.cxzyjfbbh));
                        _hospdrugbillingRepo.Updatezy_wzkcReturn(this.OrganizeId, jfbhString, this.UserIdentity.UserCode);
                    }
                    if (yszhclist.Count > 0)
                        _hospdrugbillingRepo.Updatezyyz_wzkcReturn(yszhclist, this.OrganizeId, this.UserIdentity.UserCode);
                }
                //cập nhậtcisnhập việnBệnh nhânXem trướcphảibênphíThẻphimthông tin
                _hospdrugbillingRepo.Updatezy_brxxexpand(this.OrganizeId, zyh);
            }else
                throw new FailedException(result);
            return Success();
        }


    }
}
