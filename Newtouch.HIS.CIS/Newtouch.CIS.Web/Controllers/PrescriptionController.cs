using Newtouch.Domain.IRepository;
using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Application.Interface;

using Newtouch.Domain.DTO;
using Newtouch.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PrescriptionController : OrgControllerBase
    {
        private readonly IPrescriptionDetailRepo _prescriptionDetailRepo;
        private readonly IPrescriptionRepo _prescriptionRepo;
        private readonly IMedicalRecordDmnService _medicalRecordDmnService;
        private readonly IMedicalRecordRepo _medicalRecordRepo;
        private readonly IPrescriptionDmnService _prescriptionDmnService;
        private readonly ITherapistApp _therapistApp;
        private readonly IIDBDmnService _idbDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        // GET: Prescription

        // QA-phase2 CIS-01: khong co Views/Prescription/Index.cshtml nen
        // Index() ke thua tu base tra JSON "view not found". Redirect ve
        // action don thuoc that dang chay (WMPrescription) thay vi bia view moi.
        [HttpGet]
        public override IActionResult Index()
        {
            return RedirectToAction("WMPrescription");
        }

        #region View
        /// <summary>
        /// tâyyĐơn thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult WMPrescription()
        {
            return View();
        }

        /// <summary>
        /// trongyĐơn thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult TCMPrescription()
        {
            return View();
        }
        /// <summary>
         /// trongyĐơn thuốcNEW
         /// </summary>
         /// <returns></returns>
        public IActionResult TCMPrescription2022()
        {
            return View();
        }

        /// <summary>
        /// Phục hồi chức năngĐơn thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult RehabPrescription()
        {
            return View();
        }

        /// <summary>
        /// Hạng mục thông thườngĐơn thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult RegularItemPrescription()
        {
            return View();
        }

        public IActionResult ConsumablesPrescription()
        {
            return View();
        }

        /// <summary>
        /// Đơn thuốc lịch sửdanh sách
        /// </summary>
        /// <returns></returns>
        public IActionResult HistoryPresForm()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// Đơn thuốc lịch sửcây
        /// </summary>
        /// <param name="jzId"></param>
        /// <returns></returns>
        public IActionResult GetHistoryPresTree(string blh, string jzId, int cflx)
        {
            var treeList = new List<TreeViewModel>();
            var blList = _medicalRecordRepo.IQueryable().Where(a => a.blh == blh && a.zt == "1"&&a.OrganizeId==OrganizeId).Select(a => new { a.CreateTime, a.jzId }).ToList();
            var historyPartList = blList.Where(p => p.jzId != jzId);    //elseKhám bệnh
            var thisPart = blList.FirstOrDefault(p => p.jzId == jzId);  //bản lầnKhám bệnh
            if (thisPart == null)
            {
                var ptreeItem = new TreeViewModel()
                {
                    id = Guid.NewGuid().ToString(),
                    value = DateTime.Now.ToString("yyyy-MM-dd"),
                    text = DateTime.Now.ToString("yyyy-MM-dd") + "(Lần khám này)",
                    parentId = null,
                    hasChildren = true, //códướicấp kiểu
                    isexpand = true,
                    complete = true,
                };
                treeList.Add(ptreeItem);
            }
            if (thisPart != null)
            {
                var cfList = _prescriptionRepo.IQueryable().Where(a => a.jzId == thisPart.jzId && a.cflx == cflx && a.zt == "1" && a.OrganizeId == OrganizeId).OrderBy(p => p.sfbz).ThenByDescending(p => p.cfh).ToList();
                var ptreeItem = new TreeViewModel()
                {
                    id = Guid.NewGuid().ToString(),
                    value = thisPart.CreateTime.ToString("yyyy-MM-dd"),
                    text = thisPart.CreateTime.ToString("yyyy-MM-dd") + "(Lần khám này)",
                    parentId = null,
                    hasChildren = true, //códướicấp kiểu
                    isexpand = true,
                    complete = true,
                };
                treeList.Add(ptreeItem);
                foreach (var cf in cfList)
                {
                    treeList.Add(new TreeViewModel()
                    {
                        id = cf.cfId,
                        value = cf.cfh,
                        text = cf.cfh,
                        parentId = ptreeItem.id,
                        hasChildren = false,
                        isexpand = false,
                        complete = true,
                        Ex1 = cf.sfbz.ToString(),
                        Ex2 = "current",
                        title = cf.sfbz ? "Đã thu phí" : "Chưa thu phí",
                        img = cf.sfbz ? "fa fa-jpy" : null,
                    });
                }
            }
            //nhấtmớicủaxếpởphía trên
            foreach (var rq in historyPartList.Select(p => p.CreateTime.ToString("yyyy-MM-dd")).Distinct().OrderByDescending(p => p))
            {
                //Ngàydướicủatất cảHồ sơ
                var newId = Guid.NewGuid().ToString();
                bool hasChildren = false;

                var rqblList = historyPartList.Where(p => p.CreateTime.ToString("yyyy-MM-dd") == rq).ToList();
                foreach (var item in rqblList)
                {
                    var cfList = _prescriptionRepo.IQueryable().Where(a => a.jzId == item.jzId && a.cflx == cflx && a.zt == "1").ToList();
                    foreach (var cf in cfList)
                    {
                        hasChildren = true;
                        treeList.Add(new TreeViewModel()
                        {
                            id = cf.cfId,
                            value = cf.cfh,
                            text = cf.cfh,
                            parentId = newId,
                            hasChildren = false,
                            isexpand = false,
                            complete = true,
                            Ex1 = cf.sfbz.ToString(),
                            title = cf.sfbz ? "Đã thu phí" : "Chưa thu phí",
                            img = cf.sfbz ? "fa fa-jpy" : null,
                        });
                    }
                }

                if (!hasChildren)
                {
                    continue;   //khôngHồ sơcủakhôngHiển thị
                }
                var ptreeItem = new TreeViewModel()
                {
                    id = newId,
                    value = rq,
                    text = rq,
                    parentId = null,
                    hasChildren = true,
                    isexpand = true,
                    complete = true,
                };
                treeList.Add(ptreeItem);
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Đơn thuốc lịch sửcâychi tiết
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        public IActionResult GetHistoryPresDetailByCfId(string cfId)
        {
            var list = _prescriptionDmnService.GetHistoryPresDetailByCfId(cfId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Tra cứuSố đơn thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult GetNewPresNo()
        {
            var presNo = EFDBBaseFuncHelper.Instance.GetRequisitionNo(this.OrganizeId);
            return Content(presNo);
        }

        /// <summary>
        /// TheocfIdhoặccfmxIdStrTra cứuchi tiết
        /// </summary>
        /// <param name="cflx"></param>
        /// <param name="cfId"></param>
        /// <param name="cfmxIdStr"></param>
        /// <returns></returns>
        public IActionResult GetPresDetailList(int cflx, string cfId, string cfmxIdStr)
        { 
            var data = _prescriptionDmnService.GetPresDetailList(cflx, cfId, cfmxIdStr);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Trích dẫnĐiều trịxâynghị
        /// </summary>
        /// <returns></returns>
        public IActionResult AddKfjyForm()
        {
            return View();
        }

        /// <summary>
        /// kéolấyChờ khám bệnhĐiều trịxâynghịdanh sách
        /// </summary>
        /// <param name="brlx"></param>
        /// <param name="mzzyh"></param>
        /// <returns></returns>
        public IActionResult kfjyGridJson(string brlx, string mzzyh)
        {
            int dayNum = 3;
            if (!string.IsNullOrWhiteSpace(_sysConfigRepo.GetValueByCode("GetRehabiDayNum", this.OrganizeId)))
            {
                dayNum = Convert.ToInt32(_sysConfigRepo.GetValueByCode("GetRehabiDayNum", this.OrganizeId));
            }
            var list = _therapistApp.GetWaitCvSuggestionList(brlx, mzzyh, dayNum);
            return Content(list.ToJson());
        }

        /// <summary>
        /// hơnTạo mớinghịcủachuyểnđổiTrạng thái
        /// </summary>
        /// <param name="cvList"></param>
        /// <returns></returns>
        public IActionResult UpdateSuggestionCvStatus(IList<SuggestionCvStatuDTO> cvList)
        {
            _therapistApp.UpdateSuggestionCvStatus(cvList);
            return Success();
        }

        /// <summary>
        /// Tần Hoàng Đảosổphiếugiao diện(nhắccung cấpchoHIS)
        /// </summary>
        /// <param name="Opt">giao diệnLoại:lấyGiá trị:zd(sổphiếu)</param>
        /// <param name="Brwym">bệnh ngườiduy nhấtmộtmã(bệnh ngườiởHIStrongcủaduy nhấtmộtmã,khônglàmSPcủatham sốdùng,chỉlàmhạt nhânđốidùng)</param>
        /// <param name="Zdlx">sổphiếuLoại:00:dònghìnhmã01:Thuốc02:Kiểm tra03:Xét nghiệm04:Điều trịkhôngtruyềnnàychữđoạnthìQuay lạiTất cảsổphiếu(Đơn thuốc,LIS,PACS,Điều trị)</param>
        /// <param name="Brxz">Tính chất bệnh nhân</param>
        /// <param name="startDate">Thời gian bắt đầu</param>
        /// <param name="endDate">Thời gian kết thúc</param>
        /// <returns></returns>
        [HandlerAuthorizeIgnore] // ported: external billing API, must skip login (was Web.Core HandlerLoginIgnore)
        public IActionResult QhdSvscis(string Opt, string Brwym, string Zdlx, string Brxz, DateTime? startDate, DateTime? endDate)
        {
                var str = _idbDmnService.GetCisPrescription(Opt, Brwym, Zdlx, Brxz, startDate, endDate);
                RtnToQhd rtnToQhd = new RtnToQhd();
                rtnToQhd.zlMessage =
                    str.Where(p => p.zdlx == (int)EnumCflx.RegularItemPres).ToList();
                rtnToQhd.kfMessage = str.Where(p => p.zdlx == (int)EnumCflx.RehabPres).ToList();//Phục hồi chức năng
                rtnToQhd.ypMessage = str.Where(p => p.zdlx == (int)EnumCflx.TCMPres || p.zdlx == (int)EnumCflx.WMPres).ToList();//Thuốc
                rtnToQhd.lisMessage = str.Where(p => p.zdlx == (int)EnumCflx.ExaminationPres).ToList();//Kiểm tra
                rtnToQhd.pacsMessage = str.Where(p => p.zdlx == (int)EnumCflx.InspectionPres).ToList();//Xét nghiệm
                return Content(rtnToQhd.ToJson());
        }
        private readonly IDoctorserviceDmnService _iDoctorserviceDmnService;

        public PrescriptionController(IPrescriptionDetailRepo prescriptionDetailRepo, IPrescriptionRepo prescriptionRepo, IMedicalRecordDmnService medicalRecordDmnService, IMedicalRecordRepo medicalRecordRepo, IPrescriptionDmnService prescriptionDmnService, ITherapistApp therapistApp, IIDBDmnService idbDmnService, ISysConfigRepo sysConfigRepo, IDoctorserviceDmnService iDoctorserviceDmnService)
        {
            this._prescriptionDetailRepo = prescriptionDetailRepo;
            this._prescriptionRepo = prescriptionRepo;
            this._medicalRecordDmnService = medicalRecordDmnService;
            this._medicalRecordRepo = medicalRecordRepo;
            this._prescriptionDmnService = prescriptionDmnService;
            this._therapistApp = therapistApp;
            this._idbDmnService = idbDmnService;
            this._sysConfigRepo = sysConfigRepo;
            this._iDoctorserviceDmnService = iDoctorserviceDmnService;
        }
        public IActionResult GetSfxmZxksSelectJson(string sfxmCode, string keyword)
        {
            var Zxks = _iDoctorserviceDmnService.GetSfxmZxksSelectJson(OrganizeId, keyword);
            return Content(Zxks.ToJson());
        }

        public IActionResult GetSfxmItem(string yfCode, string keyword)
        {
            var Zxks = _iDoctorserviceDmnService.GetSfxmYfList(OrganizeId, yfCode, keyword);
            return Content(Zxks.ToJson());
        }


		#region Vật tư
		/// <summary>
		/// mởlậpvật tưMụcđôngkếtTồn khoSố lượng
		/// </summary>
		/// <returns></returns>
		public IActionResult Sumwzdj(string[] cfh)
		{
			var list = _prescriptionDmnService.Sumwzdj(cfh,OrganizeId, UserIdentity.rygh);
			var data = new
			{
				row= list
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// mởlậpvật tưMụcđôngkếtTồn khoSố lượng
		/// </summary>
		/// <returns></returns>
		public IActionResult ZUOFwzdj(string cfh)
		{
			var list = _prescriptionDmnService.ZUOFwzdj(cfh, OrganizeId, UserIdentity.rygh);
			var data = new
			{
				row = list
			};
			return Content(data.ToJson());
		}

        #endregion
        public IActionResult Getmxbzd(string zdcode)
        {
            var mxbstr = _iDoctorserviceDmnService.GetmxbzdList(OrganizeId, zdcode);
            var data = new
            {
                mxbstr = mxbstr
            };
            return Content(data.ToJson());
        }
        public IActionResult Getxzkls(YpxzkldataDTO xzkldata)
        {
            var mxbstr = _iDoctorserviceDmnService.GetxzklsList(OrganizeId, xzkldata);
            var data = new
            {
                message = mxbstr
            };
            return Content(data.ToJson());
        }
        public IActionResult GetYfData()
        {
            var mxbstr = _iDoctorserviceDmnService.GetYfData(OrganizeId);
            return Content(mxbstr.ToJson());
        }

        /// <summary>
        /// Bảo hiểm y tếđiện tửĐơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        public IActionResult getdzcfnrxs(string cfh)
        {
            var data = _prescriptionDmnService.getdzcfnrxs(cfh, this.OrganizeId);
            if (data.Count > 0)
            {
                return Content("ok");
            }
            return Content("");
        }
    }
}