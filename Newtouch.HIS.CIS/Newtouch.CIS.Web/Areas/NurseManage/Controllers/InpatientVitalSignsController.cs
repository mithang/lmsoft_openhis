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
    /// 住院患者生命体征
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
            ViewBag.ScdCode = _sysConfigRepo.GetValueByCode("ScdTimePoint", OrganizeId);//三测单时间点奇偶数设置 1 奇数 0 偶数
            return View();
        }
        /// <summary>
        /// 护理查询
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
            ViewBag.ScdCode = _sysConfigRepo.GetValueByCode("ScdTimePoint", OrganizeId);//三测单时间点奇偶数设置 1 奇数 0 偶数
            return View();
        }

        /// <summary>
        /// 分页查询
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
        /// 护理查询（按日期时间排序）
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetTemperatureChartJson(DateTime? kssj, DateTime? jssj, string zyh)
        {
            if (!kssj.HasValue || !jssj.HasValue)
            {
                throw new FailedException("HLCX_DATE_REQUIRED", "Vui lòng chọnNgày");
            }
            if ((jssj.Value.Date - kssj.Value.Date).TotalDays < 0)
            {
                throw new FailedException("Kết thúcThời gianKhông thểLớn hơnThời gian bắt đầu");
            }
            if ((jssj.Value.Date - kssj.Value.Date).TotalDays > 30)
            {
                throw new FailedException("HLCX_DATE_OUTOFRANGE", "Phạm vi ngày không thể vượt quá 30 ngày");
            }
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("HLCX_ZYH_REQUIRED", "Vui lòng chọnSố nhập viện");
            }
            var patInfo = _inpatientPatientDmnService.GetInfoByZyh(zyh, this.OrganizeId);
            //2020-4-28 取消体温单查询时间强制到入院出院时间
            if (patInfo!=null&&patInfo.ryrq>kssj)
            {
                kssj = patInfo.ryrq;
                //throw new FailedException("入院日期："+patInfo.ryrq+";请输入正确的开始时间");
            }
            if (patInfo != null && patInfo.cqrq != null && patInfo.cqrq < jssj)
            {
                jssj = patInfo.cqrq;
                //throw new FailedException("出院日期：" + patInfo.cqrq + ";请输入正确的结束时间");
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

            //20190529回滚至之前的


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
        /// 提交保存护理录入
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
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
            var path = DateTime.Now.ToString("\\\\Nhập điều dưỡngTra cứuyyyyMMdd\\\\HHmmssfff") + ".xls";
            var filePath = CommmHelper.GetLocalFilePath("\\ExcelXuất\\Nhập điều dưỡngTra cứu" + path);
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
                     new ExcelColumn("tiwen","Nhiệt độ"),
                      new ExcelColumn("xueya","Huyết áp"),
                       new ExcelColumn("cfz","Khám lần đầu/tái khám"),
                        new ExcelColumn("zs","Triệu chứng chính"),
                        new ExcelColumn("xyzdmc","Chẩn đoánTên"),
                        new ExcelColumn("clfa","Xử lý"),
                        new ExcelColumn("fzjc","Kiểm tra hỗ trợ"),
                         new ExcelColumn("ysqz","Chữ ký bác sĩ"),
                          new ExcelColumn("sfzh","Hiệu lựcSố giấy tờ"),
                          new ExcelColumn("ContactNum","Điện thoại"),
                    new ExcelColumn("ghksmc","Chuyên khoa"),
                    new ExcelColumn("xuetang","Đường huyết"),
                    new ExcelColumn("xbs","Bệnh sử hiện tại"),
                    new ExcelColumn("bz","Ghi chú"),
            };
            var sheet = new ExcelSheet()
            {
                Title = "Danh sách nhật ký ngoại trú",
                columns = cols
            };

            var result = data.ToExcel(filePath, sheet);
            if (result)
            {
                return File(filePath, "application/x-xls", path.Replace("\\", ""));
            }
            else
            {
                return Content("Xuất tập tin thất bại, vui lòng quay lại trang danh sách thử lại");
            }
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _inpatientVitalSignsRepo.DeleteForm(keyValue);
            return Success("Hủy bỏthành công.");
        }

        /// <summary>
        /// 体温单
        /// </summary>
        /// <returns></returns>
        public IActionResult TemperatureChartIndex()
        {
            ViewBag.ScdCode = _sysConfigRepo.GetValueByCode("ScdTimePoint", OrganizeId);//三测单时间点奇偶数设置 1 奇数 0 偶数
            ViewBag.HospitalName = Newtouch.CIS.Compat.AppConfig.Get("HospitalName");
            return View();
        }

        /// <summary>
        /// 体温单 全屏
        /// </summary>
        /// <returns></returns>
        public IActionResult FullScreenTemperatureChartIndex()
        {
            ViewBag.HospitalName = Newtouch.CIS.Compat.AppConfig.Get("HospitalName");
            return View();
        }

        #region 秦皇岛Nhập điều dưỡng版本
        public IActionResult GetNursingInputGridJson(string bq,string sj,DateTime rq,string zyh) {
           var data= _inpatientPatientDmnService.GetInPatSearchList(OrganizeId,bq,rq,sj, zyh, ((int)EnumZYBZ.Bqz).ToString());
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取病区人员Thực hiện树
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
                    //床号 + 姓名(住院天数)+住院号 + 年龄 +性别
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
            //2021-9-7修改：三测单去掉时间查询条件
            //if (!kssj.HasValue || !jssj.HasValue)
            //{
            //    throw new FailedException("HLCX_DATE_REQUIRED", "请选择日期");
            //}
            //if ((jssj.Value.Date - kssj.Value.Date).TotalDays < 0)
            //{
            //    throw new FailedException("结束时间不能大于开始时间");
            //}
            //if ((jssj.Value.Date - kssj.Value.Date).TotalDays > 30)
            //{
            //    //2021-5-11修改:住院时间超过30天的患者 可查询入院出院范围内的日期
            //    if (!(patInfo.ryrq <= kssj && patInfo.cqrq >= jssj))
            //    {
            //        throw new FailedException("HLCX_DATE_OUTOFRANGE", "日期范围不能超过30天");
            //    }
            //}
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("HLCX_ZYH_REQUIRED", "Vui lòng chọnSố nhập viện");
            }
            //2020-4-28 取消体温单查询时间强制到入院出院时间
            if (patInfo != null && patInfo.ryrq > kssj)
            {
                kssj = patInfo.ryrq;
                //throw new FailedException("入院日期："+patInfo.ryrq+";请输入正确的开始时间");
            }
            if (patInfo != null && patInfo.cqrq != null && patInfo.cqrq < jssj  && patInfo.zybz==(int)EnumZYBZ.Ycy)
            {
                jssj = patInfo.cqrq;
                //throw new FailedException("出院日期：" + patInfo.cqrq + ";请输入正确的结束时间");
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
            List<PatOpInfoVO> oplist = new List<PatOpInfoVO>(); //是否启用手术系统
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