using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface;
using Newtouch.Common;
using Newtouch.Domain.DTO;
using Newtouch.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Web.Core.Attributes;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

        #region View
        /// <summary>
        /// Đơn thuốc Tây y
        /// </summary>
        /// <returns></returns>
        public ActionResult WMPrescription()
        {
            return View();
        }

        /// <summary>
        /// Đơn thuốc Đông y
        /// </summary>
        /// <returns></returns>
        public ActionResult TCMPrescription()
        {
            return View();
        }
        /// <summary>
         /// Đơn thuốc Đông yNEW
         /// </summary>
         /// <returns></returns>
        public ActionResult TCMPrescription2022()
        {
            return View();
        }

        /// <summary>
        /// Đơn thuốc phục hồi chức năng
        /// </summary>
        /// <returns></returns>
        public ActionResult RehabPrescription()
        {
            return View();
        }

        /// <summary>
        /// Đơn thuốc hạng mục thông thường
        /// </summary>
        /// <returns></returns>
        public ActionResult RegularItemPrescription()
        {
            return View();
        }

        public ActionResult ConsumablesPrescription()
        {
            return View();
        }

        /// <summary>
        /// Danh sách đơn thuốc lịch sử
        /// </summary>
        /// <returns></returns>
        public ActionResult HistoryPresForm()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// Cây đơn thuốc lịch sử
        /// </summary>
        /// <param name="jzId"></param>
        /// <returns></returns>
        public ActionResult GetHistoryPresTree(string blh, string jzId, int cflx)
        {
            var treeList = new List<TreeViewModel>();
            var blList = _medicalRecordRepo.IQueryable().Where(a => a.blh == blh && a.zt == "1"&&a.OrganizeId==OrganizeId).Select(a => new { a.CreateTime, a.jzId }).ToList();
            var historyPartList = blList.Where(p => p.jzId != jzId);    //elseKhám bệnh
            var thisPart = blList.FirstOrDefault(p => p.jzId == jzId);  //Lần khám này
            if (thisPart == null)
            {
                var ptreeItem = new TreeViewModel()
                {
                    id = Guid.NewGuid().ToString(),
                    value = DateTime.Now.ToString("yyyy-MM-dd"),
                    text = DateTime.Now.ToString("yyyy-MM-dd") + "（本次就诊）",
                    parentId = null,
                    hasChildren = true, //Có cấp dưới Kiểu
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
                    text = thisPart.CreateTime.ToString("yyyy-MM-dd") + "（本次就诊）",
                    parentId = null,
                    hasChildren = true, //Có cấp dưới Kiểu
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
                        title = cf.sfbz ? "已收费" : "未收费",
                        img = cf.sfbz ? "fa fa-jpy" : null,
                    });
                }
            }
            //Mới nhất xếp ở trên
            foreach (var rq in historyPartList.Select(p => p.CreateTime.ToString("yyyy-MM-dd")).Distinct().OrderByDescending(p => p))
            {
                //Tất cả bệnh án thuộc ngày
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
                            title = cf.sfbz ? "已收费" : "未收费",
                            img = cf.sfbz ? "fa fa-jpy" : null,
                        });
                    }
                }

                if (!hasChildren)
                {
                    continue;   //Không có bệnh án thì không hiển thị
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
        /// Chi tiết cây đơn thuốc lịch sử
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        public ActionResult GetHistoryPresDetailByCfId(string cfId)
        {
            var list = _prescriptionDmnService.GetHistoryPresDetailByCfId(cfId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Tra cứu số đơn thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNewPresNo()
        {
            var presNo = EFDBBaseFuncHelper.Instance.GetRequisitionNo(this.OrganizeId);
            return Content(presNo);
        }

        /// <summary>
        /// TheocfIdhoặccfmxIdStrTra cứu chi tiết
        /// </summary>
        /// <param name="cflx"></param>
        /// <param name="cfId"></param>
        /// <param name="cfmxIdStr"></param>
        /// <returns></returns>
        public ActionResult GetPresDetailList(int cflx, string cfId, string cfmxIdStr)
        { 
            var data = _prescriptionDmnService.GetPresDetailList(cflx, cfId, cfmxIdStr);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Trích dẫn đề xuất điều trị
        /// </summary>
        /// <returns></returns>
        public ActionResult AddKfjyForm()
        {
            return View();
        }

        /// <summary>
        /// Lấy danh sách đề xuất điều trị chờ khám
        /// </summary>
        /// <param name="brlx"></param>
        /// <param name="mzzyh"></param>
        /// <returns></returns>
        public ActionResult kfjyGridJson(string brlx, string mzzyh)
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
        /// Cập nhật trạng thái chuyển đổi của đề xuất
        /// </summary>
        /// <param name="cvList"></param>
        /// <returns></returns>
        public ActionResult UpdateSuggestionCvStatus(IList<SuggestionCvStatuDTO> cvList)
        {
            _therapistApp.UpdateSuggestionCvStatus(cvList);
            return Success();
        }

        /// <summary>
        /// Giao diện hóa đơn Tần Hoàng Đảo (cung cấp choHIS）
        /// </summary>
        /// <param name="Opt">Loại giao diện: giá trị:zd(Hóa đơn)</param>
        /// <param name="Brwym">Mã duy nhất của bệnh nhân(Bệnh nhân tạiHISmã duy nhất trong, không làmSPtham số dùng, chỉ dùng để đối chiếu)</param>
        /// <param name="Zdlx">Loại hóa đơn:00:Mã vạch01:Thuốc02:Kiểm tra03:Xét nghiệm04:Điều trị không truyền trường này thì trả về tất cả hóa đơn(Đơn thuốc,LIS，PACS, điều trị)</param>
        /// <param name="Brxz">Tính chất bệnh nhân</param>
        /// <param name="startDate">Thời gian bắt đầu</param>
        /// <param name="endDate">Thời gian kết thúc</param>
        /// <returns></returns>
        [HandlerLoginIgnore]
        public ActionResult QhdSvscis(string Opt, string Brwym, string Zdlx, string Brxz, DateTime? startDate, DateTime? endDate)
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
        public ActionResult GetSfxmZxksSelectJson(string sfxmCode, string keyword)
        {
            var Zxks = _iDoctorserviceDmnService.GetSfxmZxksSelectJson(OrganizeId, keyword);
            return Content(Zxks.ToJson());
        }

        public ActionResult GetSfxmItem(string yfCode, string keyword)
        {
            var Zxks = _iDoctorserviceDmnService.GetSfxmYfList(OrganizeId, yfCode, keyword);
            return Content(Zxks.ToJson());
        }


		#region 物资
		/// <summary>
		/// Lập hạng mục vật tư đóng băng số lượng tồn kho
		/// </summary>
		/// <returns></returns>
		public ActionResult Sumwzdj(string[] cfh)
		{
			var list = _prescriptionDmnService.Sumwzdj(cfh,OrganizeId, UserIdentity.rygh);
			var data = new
			{
				row= list
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// Lập hạng mục vật tư đóng băng số lượng tồn kho
		/// </summary>
		/// <returns></returns>
		public ActionResult ZUOFwzdj(string cfh)
		{
			var list = _prescriptionDmnService.ZUOFwzdj(cfh, OrganizeId, UserIdentity.rygh);
			var data = new
			{
				row = list
			};
			return Content(data.ToJson());
		}

        #endregion
        public ActionResult Getmxbzd(string zdcode)
        {
            var mxbstr = _iDoctorserviceDmnService.GetmxbzdList(OrganizeId, zdcode);
            var data = new
            {
                mxbstr = mxbstr
            };
            return Content(data.ToJson());
        }
        public ActionResult Getxzkls(YpxzkldataDTO xzkldata)
        {
            var mxbstr = _iDoctorserviceDmnService.GetxzklsList(OrganizeId, xzkldata);
            var data = new
            {
                message = mxbstr
            };
            return Content(data.ToJson());
        }
        public ActionResult GetYfData()
        {
            var mxbstr = _iDoctorserviceDmnService.GetYfData(OrganizeId);
            return Content(mxbstr.ToJson());
        }

        /// <summary>
        /// Đơn thuốc điện tử bảo hiểm y tế
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        public ActionResult getdzcfnrxs(string cfh)
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