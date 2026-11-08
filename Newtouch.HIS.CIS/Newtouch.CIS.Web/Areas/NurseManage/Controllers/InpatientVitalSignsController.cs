using Newtouch.Domain.IRepository;
using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using NewtouchHIS.Framework.Web.Controllers;
using Newtonsoft.Json;

using Newtouch.CIS.Compat;
using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.InpatientVitalSigns;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using Newtouch.Tools.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Common.Web;
using Newtouch.Domain.ValueObjects;

using Newtouch.Domain.ValueObjects.Inpatient;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// Bệnh nhân nội trú dấu hiệu sinh tồn
    /// </summary>
    [Area("NurseManage")]
    public class InpatientVitalSignsController : OrgControllerBase
    {
        private readonly IInpatientVitalSignsRepo _inpatientVitalSignsRepo;
        private readonly IInpatientPatientDmnService _inpatientPatientDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        public InpatientVitalSignsController(IInpatientVitalSignsRepo inpatientVitalSignsRepo, IInpatientPatientDmnService inpatientPatientDmnService, ISysConfigRepo sysConfigRepo)
        {
            this._inpatientVitalSignsRepo = inpatientVitalSignsRepo;
            this._inpatientPatientDmnService = inpatientPatientDmnService;
            this._sysConfigRepo = sysConfigRepo;
        }

        public IActionResult NursingInput() {
            ViewBag.MutipatientNursingInputFlag = _sysConfigRepo.GetIntValueByCode("MutipatientNursingInputFlag", OrganizeId, 0);
            ViewBag.ScdCode = _sysConfigRepo.GetValueByCode("ScdTimePoint", OrganizeId);//Phiếu đo ba chỉ số Thời gian điểm Lẻ chẵn số Cài đặt 1 lẻ số 0 chẵn số
            return View();
        }
        /// <summary>
        /// điều dưỡng Tra cứu
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryIndex()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            return View();
        }

        public override IActionResult  Form() {
            ViewBag.hsqm = UserIdentity.UserName;
            ViewBag.ScdCode = _sysConfigRepo.GetValueByCode("ScdTimePoint", OrganizeId);//Phiếu đo ba chỉ số Thời gian điểm Lẻ chẵn số Cài đặt 1 lẻ số 0 chẵn số
            return View();
        }

        /// <summary>
        /// phút trang Tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(Pagination pagination, DateTime? kssj, DateTime? jssj, string zyh,string wardCode,string isShowDelete)
        {
            var data = new
            {
                rows = _inpatientVitalSignsRepo.GetPaginationList(pagination, this.OrganizeId, kssj, jssj, zyh,wardCode,isShowDelete),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// điều dưỡng Tra cứu(theo Ngày Thời gian Thứ tự)
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetTemperatureChartJson(DateTime? kssj, DateTime? jssj, string zyh)
        {
            if (!kssj.HasValue || !jssj.HasValue)
            {
                throw new FailedException("HLCX_DATE_REQUIRED", "Vui lòng chọn Ngày");
            }
            if ((jssj.Value.Date - kssj.Value.Date).TotalDays < 0)
            {
                throw new FailedException("Kết thúc Thời gian Không thể Lớn hơn Thời gian bắt đầu");
            }
            if ((jssj.Value.Date - kssj.Value.Date).TotalDays > 30)
            {
                throw new FailedException("HLCX_DATE_OUTOFRANGE", "Phạm vi ngày không thể vượt quá 30 ngày");
            }
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("HLCX_ZYH_REQUIRED", "Vui lòng chọn Số nhập viện");
            }
            var patInfo = _inpatientPatientDmnService.GetInfoByZyh(zyh, this.OrganizeId);
            //2020-4-28 Hủy Phiếu nhiệt độ Tra cứu Thời gian mạnh chế đến Nhập viện Thời gian xuất viện
            if (patInfo!=null&&patInfo.ryrq>kssj)
            {
                kssj = patInfo.ryrq;
                //throw new FailedException("Ngày nhập viện:"+patInfo.ryrq+";vui lòng nhập đang xác của Thời gian bắt đầu");
            }
            if (patInfo != null && patInfo.cqrq != null && patInfo.cqrq < jssj)
            {
                jssj = patInfo.cqrq;
                //throw new FailedException("Ngày xuất viện:" + patInfo.cqrq + ";vui lòng nhập đang xác của Thời gian kết thúc");
            }
           
            var ValidList = _inpatientVitalSignsRepo.GetValidList(this.OrganizeId, kssj, jssj, zyh);
            //var treeList = new List<ValidListVO>();
            //for (int i = 0; i < ValidList.Count(); i++)
            //{
            //    var rqpar = ValidList.Where(p => p.rq == ValidList[i].rq);
            //    var timeslist = new List<times>();
            //    foreach (var item in rqpar)
            //    {
            //        var entity = new times
            //        {
            //            hx = item.hx,
            //            mb = item.mb,
            //            sj = item.sj,
            //            tw = item.tw,
            //            xysz = item.xysz,
            //            xyxz = item.xyxz
            //        };
            //        timeslist.Add(entity);
            //    }
            //    treeList.Add(new ValidListVO { rq = ValidList[i].rq, times = timeslist });
            //}
            //var data = new
            //{
            //    patInfo = patInfo,
            //    list = treeList
            //};

            //20190529về cuộn đến trước đây của


            var req =new {
                OrganizeId = this.OrganizeId,
                ksrq=kssj,
                jsrq=jssj,
                zyh=zyh,
                djzt=2
            };
            var patoplist = SiteORAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/ORPatient/ORPatList", req);
            List<PatOpInfoVO> oplist =new List<PatOpInfoVO>();
            if (patoplist != null && patoplist.code == APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                oplist = Newtouch.CIS.Compat.Tools.Json.ToObject<List<PatOpInfoVO>>(patoplist.data.ToString());
            }

            var data = new
            {
                patInfo = patInfo,
                list = ValidList,
                oplist= oplist
            };

            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            var data = _inpatientVitalSignsRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Gửi Lưu điều dưỡng nhập
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(InpatientVitalSignsEntity entity, string keyValue)
        {
            entity.OrganizeId = this.OrganizeId;
            entity.zt = "1";
            _inpatientVitalSignsRepo.SubmitForm(entity, keyValue);
            return Success("Lưu thành công.");
        }

        public IActionResult Export(DateTime? kssj, DateTime? jssj, string zyh)
        {
            var data = _inpatientVitalSignsRepo.GetList(OrganizeId,kssj,jssj,zyh).ToList();
            var now = DateTime.Now;
            var path = "\\Nhập điều dưỡng Tra cứu" + now.ToString("yyyyMMdd") + "\\" + now.ToString("HHmmssfff") + ".xls";
            var filePath = CommmHelper.GetLocalFilePath("\\Excel Xuất\\Nhập điều dưỡng Tra cứu" + path);
            var cols = new List<ExcelColumn>()
            {
                    new ExcelColumn("rq","Ngày"),
                    new ExcelColumn("sj","Thời gian"),
                    new ExcelColumn("hljb","Cấp độ chăm sóc"),
                    new ExcelColumn("hlys","Ý<br/>thức"),
                    new ExcelColumn("brfood","Ăn uống"),
                    new ExcelColumn("tw","Nhiệt độ"),
                    new ExcelColumn("mb","Mạch"),
                    new ExcelColumn("hx","Hô hấp"),
            };
            var sheet = new ExcelSheet()
            {
                Title = "Danh sách nhật ký ngoại trú",
                columns = cols
            };

            var result = data.ToExcel(filePath, sheet);
            if (result)
            {
                return File(System.IO.File.ReadAllBytes(filePath), "application/x-xls", path.Replace("\\", ""));
            }
            else
            {
                return Content("Xuất tập tin thất bại, vui lòng quay lại trang danh sách thử lại");
            }
        }

        /// <summary>
        /// Hủy bỏ
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _inpatientVitalSignsRepo.DeleteForm(keyValue);
            return Success("Hủy bỏ thành công.");
        }

        /// <summary>
        /// Phiếu nhiệt độ
        /// </summary>
        /// <returns></returns>
        public IActionResult TemperatureChartIndex()
        {
            ViewBag.ScdCode = _sysConfigRepo.GetValueByCode("ScdTimePoint", OrganizeId);//Phiếu đo ba chỉ số Thời gian điểm Lẻ chẵn số Cài đặt 1 lẻ số 0 chẵn số
            ViewBag.HospitalName = Newtouch.CIS.Compat.AppConfig.Get("HospitalName");
            return View();
        }

        /// <summary>
        /// Phiếu nhiệt độ toàn màn hình
        /// </summary>
        /// <returns></returns>
        public IActionResult FullScreenTemperatureChartIndex()
        {
            ViewBag.HospitalName = Newtouch.CIS.Compat.AppConfig.Get("HospitalName");
            return View();
        }

        #region Tần Hoàng ĐảoNhập điều dưỡng bản bản
        public IActionResult GetNursingInputGridJson(string bq,string sj,DateTime rq,string zyh) {
           var data= _inpatientPatientDmnService.GetInPatSearchList(OrganizeId,bq,rq,sj, zyh, ((int)EnumZYBZ.Bqz).ToString());
            return Content(data.ToJson());
        }
        /// <summary>
        /// Lấy Khu bệnh người viên Thực hiện cây
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPatWardTree(string zyzt, string keyword)
        {

            var wardTree = _inpatientPatientDmnService.GetPatTree(OrganizeId, zyzt, keyword);
            var wardonly = wardTree.GroupBy(p => new { p.bqCode, p.bqmc }).Select(p => new { p.Key.bqCode, p.Key.bqmc });


            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.bqCode == item.bqCode).Where(p => p.zyh != "").Where(p => p.zyh != null).ToList();
                var NewPatInfo = patInfo.OrderBy(p => p.BedNo);
                foreach (InpWardPatTreeVO itempat in NewPatInfo)
                { 
                    string gender = itempat.sex == "1" ? "Nam" : "Nữ";
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    //Số giường + Họ tên(Nhập viện Số ngày)+Số nhập viện + Tuổi +Giới tính
                    treepat.text = itempat.BedNo + "-" + itempat.hzxm + "(" + itempat.inHosDays + "ngày)" + "-" + itempat.zyh + "-" + itempat.nl + "tuổi-" + gender;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treepat.Ex2 = itempat.rqrq;
                    treepat.Ex3 = itempat.cqrq;
                    treepat.Ex4 = itempat.hzxm;
                    treeList.Add(treepat);
                }

                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.bqCode;
                tree.text = item.bqmc;
                tree.value = item.bqCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = 0;
                tree.hasChildren = hasChildren;
                tree.Ex1 = "p";
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        } 
        public IActionResult GetPatWard(string zyh)
        {
            var wardTree = _inpatientPatientDmnService.GetPatList(OrganizeId, zyh);
            return Content(wardTree.ToJson());
        }
        public IActionResult submitmutiple(List<InpatientVitalSignsEntity> entityList, int? sjd,DateTime rq,string flag,List<string> xysxzList)
        {
            for (var i = 0; i < entityList.Count; i++) {
                var xysz = 0;
                var xyxz = 0;
                var xyszx = xysxzList[i].Split('/');
                if (xyszx.Length == 2)
                {
                    xysz = xyszx[0].ToInt();
                    xyxz = xyszx[1].ToInt();
                }
                entityList[i].xysz = xysz;
                entityList[i].xyxz = xyxz;
            }
            _inpatientPatientDmnService.submitmutiple(entityList,OrganizeId,sjd,rq,flag);
            return Success();
        }
        #endregion


        #region DrawData 
        public IActionResult DrawData(DateTime? kssj, DateTime? jssj, string zyh,int? pagesize)
		{
			var patInfo = _inpatientPatientDmnService.GetInfoByZyh(zyh, this.OrganizeId);
            //2021-9-7Sửa:Phiếu đo ba chỉ số Bỏ Thời gian Điều kiện tìm kiếm
            //if (!kssj.HasValue || !jssj.HasValue)
            //{
            //    throw new FailedException("HLCX_DATE_REQUIRED", "Vui lòng chọn Ngày");
            //}
            //if ((jssj.Value.Date - kssj.Value.Date).TotalDays < 0)
            //{
            //    throw new FailedException("Thời gian kết thúc không có thể Lớn hơn Thời gian bắt đầu");
            //}
            //if ((jssj.Value.Date - kssj.Value.Date).TotalDays > 30)
            //{
            //    //2021-5-11Sửa:Nhập viện Thời gian vượt qua30ngày của Bệnh nhân có thể Tra cứu Nhập viện Xuất viện phạm vi trong của Ngày
            //    if (!(patInfo.ryrq <= kssj && patInfo.cqrq >= jssj))
            //    {
            //        throw new FailedException("HLCX_DATE_OUTOFRANGE", "Ngày phạm vi không có thể vượt qua30ngày");
            //    }
            //}
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("HLCX_ZYH_REQUIRED", "Vui lòng chọn Số nhập viện");
            }
            //2020-4-28 Hủy Phiếu nhiệt độ Tra cứu Thời gian mạnh chế đến Nhập viện Thời gian xuất viện
            if (patInfo != null && patInfo.ryrq > kssj)
            {
                kssj = patInfo.ryrq;
                //throw new FailedException("Ngày nhập viện:"+patInfo.ryrq+";vui lòng nhập đang xác của Thời gian bắt đầu");
            }
            if (patInfo != null && patInfo.cqrq != null && patInfo.cqrq < jssj  && patInfo.zybz==(int)EnumZYBZ.Ycy)
            {
                jssj = patInfo.cqrq;
                //throw new FailedException("Ngày xuất viện:" + patInfo.cqrq + ";vui lòng nhập đang xác của Thời gian kết thúc");
            }

            var ValidList = _inpatientVitalSignsRepo.GetTempratureData(this.OrganizeId, kssj, jssj, zyh, pagesize??6);
            var req = new
            {
                OrganizeId = this.OrganizeId,
                ksrq = kssj,
                jsrq = jssj,
                zyh = zyh,
                djzt = 2
            };
            string enableor = _sysConfigRepo.GetValueByCode("EnableLinkToOR", OrganizeId);
            List<PatOpInfoVO> oplist = new List<PatOpInfoVO>(); //Có Không Kích hoạt Phẫu thuật Hệ thống
            if (enableor == "ON")
            {
                var patoplist = SiteORAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/ORPatient/ORPatList", req);
                if (patoplist != null && patoplist.code == APIRequestHelper.ResponseResultCode.SUCCESS)
                {
                    oplist = Newtouch.CIS.Compat.Tools.Json.ToObject<List<PatOpInfoVO>>(patoplist.data.ToString());
                }
            }

            var data = new
            {
                patInfo = patInfo,
                list = ValidList,
                oplist = oplist
            };

            return Content(data.ToJson());

        }
        public IActionResult Getzyhrq(string zyh)
        {
            var patInfo = _inpatientPatientDmnService.GetInfoByZyh(zyh, this.OrganizeId);
            string ryrq = "";
            if (patInfo != null && patInfo.ryrq != null)
            {
                ryrq = patInfo.ryrq.ToString();
            }
            return Content(ryrq);
        }
        public IActionResult Getzyhcqrq(string zyh)
        {
            var patInfo = _inpatientPatientDmnService.GetInfoByZyh(zyh, this.OrganizeId);
            string cqrq = "";
            if (patInfo != null && patInfo.cqrq != null && patInfo.cqrq.ToString() != "")
            {
                cqrq = patInfo.cqrq.ToString();
            }
            return Content(cqrq);
        }
        #endregion
    }
}