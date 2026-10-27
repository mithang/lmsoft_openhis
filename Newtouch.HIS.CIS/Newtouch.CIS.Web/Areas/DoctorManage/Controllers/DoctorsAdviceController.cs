
using Newtouch.Domain.IDomainServices;
using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;


using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;

using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    [Area("DoctorManage")]
    public class DoctorsAdviceController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IBaseDataDmnService _iBaseDataDmnService;
        private readonly IInpatientOrderPackageDmnService _inpatientOrderPackageDmnService;
        private readonly IInpatientOrderPackageRepo _inpatientOrderPackageRepo;
        private readonly IDoctorserviceDmnService _iDoctorserviceDmnService;
        private readonly IInpatientLongTermOrderRepo _inpatientLongTermOrderRepo;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysUserDmnService _sysUserDmnService;

        public DoctorsAdviceController(ISysConfigRepo sysConfigRepo, IBaseDataDmnService iBaseDataDmnService, IInpatientOrderPackageDmnService inpatientOrderPackageDmnService, IInpatientOrderPackageRepo inpatientOrderPackageRepo, IDoctorserviceDmnService iDoctorserviceDmnService, IInpatientLongTermOrderRepo inpatientLongTermOrderRepo, ISysStaffRepo sysStaffRepo, ISysUserDmnService sysUserDmnService)
        {
            this._sysConfigRepo = sysConfigRepo;
            this._iBaseDataDmnService = iBaseDataDmnService;
            this._inpatientOrderPackageDmnService = inpatientOrderPackageDmnService;
            this._inpatientOrderPackageRepo = inpatientOrderPackageRepo;
            this._iDoctorserviceDmnService = iDoctorserviceDmnService;
            this._inpatientLongTermOrderRepo = inpatientLongTermOrderRepo;
            this._sysStaffRepo = sysStaffRepo;
            this._sysUserDmnService = sysUserDmnService;
        }

        [HandlerAuthorizeIgnore]
        public override IActionResult Index()
        {
            //判断登陆者身份是否是护士
            var isDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "Doctor");
            if (!isDoctor)
            {
                ViewBag.bhbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId, true);//住院医嘱套餐是否包含病区
                //如果是护士，进入住院医嘱查看页面
                return View("ZyyzQuery");
            }
            ViewBag.ysgh = UserIdentity.rygh;//医生工号
            ViewBag.pcStr = _sysConfigRepo.GetValueByCode("FrequencyStr", OrganizeId) == "" ? "" : _sysConfigRepo.GetValueByCode("FrequencyStr", OrganizeId);//临时频次
            ViewBag.bhbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId, true);//住院医嘱套餐是否包含病区
            ViewBag.zzlconfig = _sysConfigRepo.GetValueByCode("zllconfig", OrganizeId) == "" ? "Liều mỗi lần điều trị" : _sysConfigRepo.GetValueByCode("zllconfig", OrganizeId);//住院项目录入治疗量文本配置
            ViewBag.bwhide = _sysConfigRepo.GetBoolValueByCode("bwhide", OrganizeId, false);//住院项目录入部位显示隐藏配置                                           
            ViewBag.IsOpenJyJcSwitch = _sysConfigRepo.GetBoolValueByCode("OpenZYJyJcSwitch", this.OrganizeId, false);//是否开放检验检查
            ViewBag.IsOpenRehabSwitch = _sysConfigRepo.GetBoolValueByCode("OpenZYRehabSwitch", this.OrganizeId, false);//是否开放康复医嘱
            
            ViewBag.ISOpenWzhc = _sysConfigRepo.GetBoolValueByCode("openWzhccf", this.OrganizeId);//开关：是否开放医用耗材
            ViewBag.sfxmService = _sysConfigRepo.GetValueByCode("sfxmService", this.OrganizeId) == "" ? "cg" : _sysConfigRepo.GetValueByCode("sfxmService", this.OrganizeId);//是否开常规项目
            ViewBag.ISOpenSsyz = _sysConfigRepo.GetBoolValueByCode("ISOpenSsyzSwitch", this.OrganizeId, false);//是否开放膳食医嘱																					  
            ViewBag.ISWithRehabSuggestion = _sysConfigRepo.GetBoolValueByCode("IS_WithRehabSuggestion", this.OrganizeId, false);//康复处方是否Trích dẫn治疗建议
                                                                                                                                
            var frequencyStr = _sysConfigRepo.GetValueByCode("FrequencyStr", OrganizeId) ?? "";//临时频次串 分割的频次编码字符串例如：00,01
            ViewBag.isqfswith = _sysConfigRepo.GetValueByCode("accountqfexecute_switch", OrganizeId);//欠费医嘱开立、Thực hiện开关
            ViewBag.isopenPriorReview = _sysConfigRepo.GetValueByCode("OpenPriorReview", OrganizeId);//是否开启事前审核接口
            ViewBag.openYjzxSwitch = _sysConfigRepo.GetValueByCode("openYjzxSwitch", OrganizeId); //开关：医技管理是否启用 
            if (!string.IsNullOrWhiteSpace(frequencyStr))
            {
                var FequencyList = frequencyStr.Split(',');
                var pcInfo = _iDoctorserviceDmnService.getpcInfoByCode(FequencyList[0], OrganizeId);
                if (pcInfo != null)
                {
                    ViewBag.pccode = pcInfo.yzpcCode;
                    ViewBag.zxcs = pcInfo.zxcs;
                    ViewBag.zxzq = pcInfo.zxzq;
                    ViewBag.zxzqdw = pcInfo.zxzqdw;
                    ViewBag.pcmc = pcInfo.yzpcmc;
                }
                else {
                    throw new Exception($"Không tìm thấy thông tin tần suất y lệnh hiệu lực (số hiệu tần suất y lệnh: {FequencyList[0]}), vui lòng đợi giây lát thử lại hoặc liên hệ quản trị viên.");
                }
                
            }
            #region Kháng sinhliên quan
            ViewBag.IsQyKssKz = _sysConfigRepo.GetBoolValueByCode("openKssQxSwitch", OrganizeId);//是否启用抗生素
            ViewBag.qxjb = "0";//抗生素权限级别(默认 0 非限制用药)
            SysStaffVEntity staff = _sysStaffRepo.GetValidStaffByGh(UserIdentity.rygh, OrganizeId);
            if (staff != null)
            {
                switch (staff.zc)
                {
                    case "ys":
                        ViewBag.qxjb = "0";//医师 对应 非限制用药 0
                        break;
                    case "zzys":
                        ViewBag.qxjb = "1";//主治医师 对应 限制用药 1
                        break;
                    case "zrys":
                        ViewBag.qxjb = "2";//主任医师 对应 特殊用药 2
                        break;
                    default:
                        ViewBag.qxjb = "0";//医师 对应 非限制用药 0
                        break;
                }
            }
            #endregion Kháng sinhliên quan
            //影像配置
            ViewBag.PACSCode = _sysConfigRepo.GetValueByCode("PACSCode", OrganizeId);
            ViewBag.isOpenRationalUse = _sysConfigRepo.GetValueByCode("OpenRationalUse", OrganizeId);//是否开启合理用药

            #region Tốc độ truyền
            var frds = _sysConfigRepo.GetValueByCode("frequencyRelDroppingSpeed", OrganizeId);
            frds = string.IsNullOrWhiteSpace(frds) ? "静滴" : frds;
            ViewBag.frequencyRelDroppingSpeed = frds;
            #endregion

            //是否开启中草药医嘱
            ViewBag.ISOpenzcyyzSwitch = _sysConfigRepo.GetBoolValueByCode("ISOpenzcyyzSwitch", OrganizeId, false);
            return View();
        }

        #region Chế độ xemreturn
        public IActionResult AdviceList()
        {
            ViewBag.isopenPriorReview = _sysConfigRepo.GetValueByCode("OpenPriorReview", OrganizeId);//是否开启事前审核接口
            ViewBag.isOpenRationalUse = _sysConfigRepo.GetValueByCode("OpenRationalUse", OrganizeId);//是否开启合理用药
            return View();
        }

        public IActionResult AdviceListLook()
        {
            return View();
        }
        public IActionResult ZyyzQueryAdviceList()
        {
            return View();
        }

        public IActionResult AdviceStop()
        {
            return View();
        }

        public IActionResult AdviceLeaveHospitalStop()
        {
            return View();
        }

        public IActionResult ContinuePrint()
        {
            return View();
        }

        public IActionResult TemplatePresForm()
        {
            return View();
        }
        public IActionResult HistoricalOrders()
        {
            return View();
        }
        public IActionResult PatientHistoricalOrders()
        {
            return View();
        }
        public IActionResult TransferWardStop()
        {
            return View();
        }
        #endregion

        #region Y lệnhMẫu

        public IActionResult saveAsTemplate(InpatientOrderPackageEntity mbObj, List<InpatientOrderPackageVO> mxList)
        {
            if (mbObj.tcfw == (int)EnumTcfw.Dept)//科室
            {
                mbObj.DeptCode = UserIdentity.DepartmentCode;

            }
            if (mbObj.tcfw == (int)EnumTcfw.Ward)//病区
            {
                mbObj.WardCode = _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault().bqCode;
            }
            _inpatientOrderPackageDmnService.saveAsTemplate(mbObj, mxList, OrganizeId);
            return Success();
        }


        /// <summary>
        /// 模板树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeList(int tcfw, string yzlx)
        {
            var treeList = new List<TreeViewModel>();
            int linqyzlx = int.Parse(yzlx);
            var alldata = _inpatientOrderPackageRepo.IQueryable().Where(p => p.tcfw == tcfw && p.tclx == linqyzlx && p.OrganizeId == OrganizeId && p.zt == "1").OrderBy(a => a.CreateTime).ThenBy(a => a.tcmc).ToList();
            switch (tcfw)
            {
                case (int)EnumTcfw.Person:
                    alldata.Where(p => p.ysgh == UserIdentity.rygh);
                    break;
                case (int)EnumTcfw.Dept:
                    alldata.Where(p => p.DeptCode == UserIdentity.DepartmentCode);
                    break;
                case (int)EnumTcfw.Ward:
                    alldata.Where(p => p.WardCode == _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault().bqCode);
                    break;
                case (int)EnumTcfw.Hosp:
                    alldata.Where(p => p.ysgh == "*" && p.DeptCode == "*" && p.WardCode == "*");
                    break;
                default: break;
            }
            foreach (var mb in alldata)
            {
                treeList.Add(new TreeViewModel()
                {
                    id = mb.Id,
                    value = mb.Id,
                    text = mb.tcmc,
                    parentId = null,
                    hasChildren = false,
                    isexpand = false,
                    complete = true
                });
            }
            return Content(treeList.TreeViewJson(null));
        }

        public IActionResult GetMBDetailByMainId(string Id)
        {
            var data = _inpatientOrderPackageDmnService.GetMBDetailByMainId(Id, OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult GetHistoricalOrders(string zyh, string type, string kssj, string jssj, string cqorls)
        {
            var data = _inpatientOrderPackageDmnService.GetHistoricalOrders(zyh, type, kssj, jssj, cqorls, OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult GetPatientQuery(string zyh, string type, string kssj, string jssj, string cqorls)
        {
            var data = _inpatientOrderPackageDmnService.GetPatientQuery(zyh, OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult GetMBDetailByDetailId(string idList)
        {
            var data = _inpatientOrderPackageDmnService.GetMBDetailByDetailId(idList, OrganizeId);
            return Content(data.ToJson());
        }

        #endregion

        #region Y lệnhTrích dẫn

        public IActionResult GetHistoricalOrdersById(string yzlistId)
        {
            var data = _inpatientOrderPackageDmnService.GetHistoricalOrdersById(yzlistId, OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult GetHistoricalRegist(string zyh, string curzyh, string yzlistId)
        {
            var data = _inpatientOrderPackageDmnService.GetHistoricalRegist(zyh, curzyh, yzlistId, OrganizeId, this.UserIdentity.UserCode);
            return Success("", data);
        }



        #endregion

        #region  Y lệnhQuản lý
        /// <summary>
        /// 医嘱查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IActionResult AdviceGridView(Pagination pagination, AdviceListRequestVO req)
        {
            req.orgId = OrganizeId;
            var data = new
            {
                rows = _iDoctorserviceDmnService.AdviceGridView(pagination, req),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 停止、作废医嘱
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="tzsj"></param>
        /// <returns></returns>
        public IActionResult StopSubmitForm(string yzId, DateTime? tzsj, string yzlx, string zyh, string iszt)
        {
            _iDoctorserviceDmnService.AdviceStop(yzId, tzsj, UserIdentity.rygh, OrganizeId, yzlx, zyh, iszt);
            return Success();
        }


        /// <summary>
        /// 删除未审核的医嘱
        /// </summary>
        /// <param name="yzId"></param>
        /// <returns></returns>
        public IActionResult DelForm(string yzId, string yzlx, string zyh, string iszt, string conflinktoOR)
        {
            conflinktoOR = Newtouch.CIS.Compat.AppConfig.Get("EnableLinkToOR");
            _iDoctorserviceDmnService.AdviceDel(yzId, OrganizeId, yzlx, zyh, iszt, conflinktoOR);
            return Success();
        }

        /// <summary>
        /// Hủy DC
        /// </summary>
        /// <param name="yzId"></param>
        /// <returns></returns>
        public IActionResult advicedc(string yzId, string yzlx, string zyh, string isFeeGroup)
        {
            _iDoctorserviceDmnService.Advicedc(yzId, OrganizeId, yzlx, zyh, isFeeGroup);
            return Success();
        }

        /// <summary>
        /// 出院全停
        /// </summary>
        /// <returns></returns>
        public IActionResult AdviceLeaveHospitalStopSubmit(string zyh, DateTime tzsj)
        {
            _iDoctorserviceDmnService.AdviceLeaveHospitalStopSubmit(zyh, tzsj, OrganizeId, UserIdentity.rygh);
            return Success();
        }

        /// <summary>
        /// 转区全停
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult AdviceTransferWardStopSubmit(string zyh, string bq, DateTime? kssj)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("ThiếuSố nhập viện");
            }
            if (string.IsNullOrWhiteSpace(bq) || bq == "==Vui lòng chọn==")
            {
                throw new FailedException("ThiếuKhu bệnh");
            }
            if (kssj == DateTime.MaxValue || kssj == DateTime.MinValue || kssj == null)
            {
                throw new FailedException("ThiếuThời gian bắt đầu");
            }
            var req = new TransferWardRequestVO
            {
                zyh = zyh,
                bq = bq,
                czr = UserIdentity.rygh,
                kssj = DateTime.Parse(kssj.ToString()),
                orgId = OrganizeId
            };
            _iDoctorserviceDmnService.AdviceTransferWardStopSubmit(req);
            return Success();
        }
        /// <summary>
        /// 已停止医嘱复制新建
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="yzId"></param>
        /// <param name="yzlb"></param>
        /// <returns></returns>
        public IActionResult RegistTzyz(string zyh, string yzId, string yzlb)
        {
            _iDoctorserviceDmnService.RegistTzyz(yzId, OrganizeId, yzlb, zyh);
            return Success();
        }

        #endregion

        #region Y lệnhGói

        /// <summary>
        /// 判断当前病人是否存在未审核的长期医嘱
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult boolwsh(string zyh)
        {
            int wshzt = (int)EnumYzzt.Ds;
            var wsh = _inpatientLongTermOrderRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == OrganizeId && p.zt == "1" && p.yzzt == wshzt);
            if (wsh != null && wsh.Count() > 0)
            {
                return Error("Tồn tại y lệnh chưa duyệt");
            }
            return Success();
        }

        /// <summary>
        /// 返回套餐范围
        /// </summary>
        /// <returns></returns>
        public IActionResult GettcfwContainWard()
        {
            SysStaffVEntity sysStaffEntity = _sysStaffRepo.GetValidStaffByGh(this.UserIdentity.rygh, this.OrganizeId);
            int mbqx = Convert.ToInt32((sysStaffEntity.mbqx == null || sysStaffEntity.mbqx == "") ? "1" : sysStaffEntity.mbqx);
            var data = new List<object>();
            var qxz = new
            {
                id = "",
                text = "==Vui lòng chọn=="
            };
            data.Add(qxz);
            bool containbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId).ToBool();
            foreach (EnumTcfw item in Enum.GetValues(typeof(EnumTcfw)))
            {
                if ((!containbq) && (item == EnumTcfw.Ward || mbqx < (int)item))
                {
                    continue;
                }
                var obj = new
                {
                    id = (int)item,
                    text = ((EnumTcfw)item).GetDescription()
                };
                data.Add(obj);
            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// 选套餐明细
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTemplatePresDetailByMbId(string mbId)
        {
            return View();
        }

        /// <summary>
        /// 医嘱查询生成医嘱类型下拉框时，配置的检验检查显示
        /// </summary>
        /// <returns></returns>
        public IActionResult GetyzlxContain()
        {
            var data = new List<object>();
            var qxz = new
            {
                id = "",
                text = "Tất cả"
            };
            data.Add(qxz);
            bool containbq = _sysConfigRepo.GetBoolValueByCode("OpenZYJyJcSwitch", OrganizeId, false).ToBool();
            //膳食医嘱
            bool containss = _sysConfigRepo.GetBoolValueByCode("ISOpenSsyzSwitch", OrganizeId, false).ToBool();
            //中草药医嘱
            bool containzcy = _sysConfigRepo.GetBoolValueByCode("ISOpenzcyyzSwitch", OrganizeId, false).ToBool();
            foreach (EnumYzlx item in Enum.GetValues(typeof(EnumYzlx)))
            {
                if (((!containbq) && (item == EnumYzlx.jc || item == EnumYzlx.jy)) || item == EnumYzlx.oper || (!containss && item == EnumYzlx.ssyz) || (!containzcy && item == EnumYzlx.zcy))
                {
                    continue;
                }
                var obj = new
                {
                    id = (int)item,
                    text = ((EnumYzlx)item).GetDescription()
                };
                data.Add(obj);
            }
            return Content(data.ToJson());
        }

        #endregion

        #region Quyết toánThông tin
        public IActionResult SettInfoShow()
        {
            return View();
        }

        #endregion


        #region  lis pacs Báo cáo
        /// <summary>
        /// //根据住院号获取LIS/PACS报告完成数量
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult CountLISztzy(string zyh)
        {
            var num = _iDoctorserviceDmnService.CountLISztzy(OrganizeId, zyh);
            var data = new
            {
                num = num
            };
            return Content(data.ToJson());
        }
        #endregion
    }
}