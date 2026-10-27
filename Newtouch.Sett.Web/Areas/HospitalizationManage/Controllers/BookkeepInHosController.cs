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
    public class BookkeepInHosController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IPatientBasicInfoDmnService _PatientBasicInfoDmnService;
        private readonly IBookkeepInHosDmnService _BookkeepInHosDmnService;
        private readonly ISysPatiChargeAddApp _SysPatiChargeAddApp;
        private readonly ISysPatiChargeWaiverApp _SysPatiChargeWaiverApp;
        private readonly IHospItemFeeApp _HospItemFeeApp;
        private readonly IHospMedicinFeeApp _HospMedicinFeeApp;
        private readonly IHospSettApp _OutHospBillApp;
        private readonly IOutPatChargeApp _outPatChargeApp;
        private readonly IBookkeepInpatientApp _bookkeepInpatientApp;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IMoneyUpperLimitReminderDmnService _moneyUpperLimitReminderDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISyncTreatmentServiceRecordDmnService _syncTreatmentServiceRecordDmnService;
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly IRefundDmnService _refundDmnService;
        private readonly IHosPatAccDmnService _hosPatAccDmnService;
        private readonly IHospSettlementRepo _hospSettlementRepo;
        private readonly IHospItemBillingRepo _hospItemBillingRepo;
        private readonly IHospAccountingPlanDetailRepo _hospAccountingPlanDetailRepo;
        private readonly IItemDmnService _itemDmnService;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly IOutPatientApp _outpatientApp;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IAccountManageApp _accountmanageApp;
        private readonly ISysChargeTemplateRepo _sysChargeTemplateRepo;
        private readonly ISysChargeTemplateItemMappRepo _sysChargeTemplateItemMappRepo;
        private readonly IHospDrugBillingRepo _hospdrugbillingRepo;
        public BookkeepInHosController(IPatientBasicInfoDmnService patientBasicInfoDmnService, IBookkeepInHosDmnService bookkeepInHosDmnService, ISysPatiChargeAddApp sysPatiChargeAddApp, ISysPatiChargeWaiverApp sysPatiChargeWaiverApp, IHospItemFeeApp hospItemFeeApp, IHospMedicinFeeApp hospMedicinFeeApp, IHospSettApp outHospBillApp, IOutPatChargeApp outPatChargeApp, IBookkeepInpatientApp bookkeepInpatientApp, ISysUserDmnService sysUserDmnService, IMoneyUpperLimitReminderDmnService moneyUpperLimitReminderDmnService, ISysConfigRepo sysConfigRepo, ISyncTreatmentServiceRecordDmnService syncTreatmentServiceRecordDmnService, IOutPatientDmnService outPatientDmnService, IRefundDmnService refundDmnService, IHosPatAccDmnService hosPatAccDmnService, IHospSettlementRepo hospSettlementRepo, IHospItemBillingRepo hospItemBillingRepo, IHospAccountingPlanDetailRepo hospAccountingPlanDetailRepo, IItemDmnService itemDmnService, ISysStaffRepo sysStaffRepo, IOutPatientApp outpatientApp, IHospPatientBasicInfoRepo hospPatientBasicInfoRepo, IAccountManageApp accountmanageApp, ISysChargeTemplateRepo sysChargeTemplateRepo, ISysChargeTemplateItemMappRepo sysChargeTemplateItemMappRepo, IHospDrugBillingRepo hospdrugbillingRepo)
        {
            _PatientBasicInfoDmnService = patientBasicInfoDmnService;
            _BookkeepInHosDmnService = bookkeepInHosDmnService;
            _SysPatiChargeAddApp = sysPatiChargeAddApp;
            _SysPatiChargeWaiverApp = sysPatiChargeWaiverApp;
            _HospItemFeeApp = hospItemFeeApp;
            _HospMedicinFeeApp = hospMedicinFeeApp;
            _OutHospBillApp = outHospBillApp;
            _outPatChargeApp = outPatChargeApp;
            _bookkeepInpatientApp = bookkeepInpatientApp;
            _sysUserDmnService = sysUserDmnService;
            _moneyUpperLimitReminderDmnService = moneyUpperLimitReminderDmnService;
            _sysConfigRepo = sysConfigRepo;
            _syncTreatmentServiceRecordDmnService = syncTreatmentServiceRecordDmnService;
            _outPatientDmnService = outPatientDmnService;
            _refundDmnService = refundDmnService;
            _hosPatAccDmnService = hosPatAccDmnService;
            _hospSettlementRepo = hospSettlementRepo;
            _hospItemBillingRepo = hospItemBillingRepo;
            _hospAccountingPlanDetailRepo = hospAccountingPlanDetailRepo;
            _itemDmnService = itemDmnService;
            _sysStaffRepo = sysStaffRepo;
            _outpatientApp = outpatientApp;
            _hospPatientBasicInfoRepo = hospPatientBasicInfoRepo;
            _accountmanageApp = accountmanageApp;
            _sysChargeTemplateRepo = sysChargeTemplateRepo;
            _sysChargeTemplateItemMappRepo = sysChargeTemplateItemMappRepo;
            _hospdrugbillingRepo = hospdrugbillingRepo;
        }

        
        #region HISghi sổ nhập viện Chọn mục tính phí（Thuốc），Điền số lượng v.v.，Ghi trực tiếp vàozy_xmjfb zy_ypjfbghi dữ liệu tính phí


        public override IActionResult Index()
        {
            var mayMedicine = _sysConfigRepo.GetBoolValueByCode("HOSP_AccountingPage_May_Medicine", OrganizeId);
            if (!(mayMedicine == false))
            {
                ViewBag.ISMedicineSearchRelatedKC = _sysConfigRepo.GetBoolValueByCode("IS_MedicineSearchRelatedKC", OrganizeId);
            }
            ViewBag.ISAccountingPageMayMedicine = mayMedicine;
            var ss = _sysConfigRepo.GetValueByCode("OperationDeptConfig", OrganizeId);
            ViewBag.staffDept = this.UserIdentity.DepartmentCode;
            ViewBag.staffDeptName = this.UserIdentity.DepartmentName;
            ViewBag.OperationDeptConfig = ss == null ? "" : ss;
            return base.Index();
        }
        
        /// <summary>
        /// Truy vấn thông tin cơ bản bệnh nhân nhập viện theo số thẻ hoặc mã nhập viện
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string kh, string zyh)
        {
            if (!string.IsNullOrWhiteSpace(kh) || !string.IsNullOrWhiteSpace(zyh))
            {
                var data = _PatientBasicInfoDmnService.GetSysBasicByZHY(kh, zyh, this.OrganizeId);//[0];
                if (data.Count > 0)
                {
                    #region Tính số tiền chưaquyết toán
                    if (!string.IsNullOrWhiteSpace(data[0].zyh))
                    {
                        var dlentitys = _OutHospBillApp.GetHospFeeClassifyWithDLBOList(data[0].zyh);
                        decimal wjje = 0;
                        if (dlentitys.Count != 0 && dlentitys != null)
                        {
                            for (int i = 0; i < dlentitys.Count; i++)
                            {
                                wjje += dlentitys[i].jsje;
                            }
                        }
                        data[0].wjje = wjje;
                    }
                    #endregion

                    return Content(data[0].ToJson());
                }
                else
                {
                    return Error("Không tìm thấy bệnh nhân này！");
                }
            }
            else
            {
                return Error("Số thẻ hoặc mã nhập viện để trống！");
            }

        }

        /// <summary>
        /// Lấy thuốc và mụcthu phí
        /// </summary>
        /// <param name="keyword">Truy vấn theo từ khóađiều kiện</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult GetYPItemInfo(string keyword)
        {
            List<ChargeItemDetailVO> ypItemList = _outPatChargeApp.GetYpItemInfo(keyword);
            // return Content(ypItemList.ToJson());
            return Content(ypItemList.ToJson());
        }

        /// <summary>
        /// LưuDữ liệulúccủaThao tác
        /// </summary>
        /// <param name="xmjf"></param>
        /// <returns></returns>
        public IActionResult AddData(string zyh,List<ItemFeeVO> ItemFeeVO)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("Thiếu mã nhập viện");
            }
            if (ItemFeeVO != null && ItemFeeVO.Count > 0)
            {
                var zyxx=  _hospPatientBasicInfoRepo.FindEntity(p=>p.zyh==zyh&&p.OrganizeId==this.OrganizeId);
                if (zyxx.zybz==((int)EnumZYBZ.Wry).ToString()||zyxx.zybz==((int)EnumZYBZ.Ycy).ToString())
                {
                    throw new FailedException("Bệnh nhân không còn ở viện");
                }
                DateTime? cqrq = _PatientBasicInfoDmnService.IFCQRQISJZSJ(zyh,this.OrganizeId);
                //Bảng tính phí mục
                List<HospItemBillingEntity> xmjfbEntitylist = new List<HospItemBillingEntity>();
                //Bảng tính phí thuốc
                List<HospDrugBillingEntity> ypjfbEntitylist = new List<HospDrugBillingEntity>();
                for (int i = 0; i < ItemFeeVO.Count; i++)
                {
                    if (ItemFeeVO[i].tdrq> cqrq)
                    {
                        return Error("Ngàyghi sổ không được muộn hơn ngày ra khoa của bệnh nhân!");
                    }
                    if (ItemFeeVO[i].ryrq > ItemFeeVO[i].tdrq && (ItemFeeVO[i].tdrq != new DateTime(0001, 01, 01)))
                    {
                        return Error("Ngàyghi sổ không được sớm hơn ngày nhập viện của bệnh nhân!");
                    }
                    if ((ItemFeeVO[i].cyrq != new DateTime(0001, 01, 01)) && ItemFeeVO[i].cyrq < ItemFeeVO[i].tdrq)
                    {
                        return Error("Ngàyghi sổ không được muộn hơn ngày xuất viện của bệnh nhân!");
                    }
                    if (string.IsNullOrWhiteSpace(ItemFeeVO[i].yzlx))
                    {
                        return Error("Thiếu loại mục!");
                    }
                    if (ItemFeeVO[i].yzlx == ((int)EnumxmType.XM).ToString())
                    {
                        #region Bảng tính phí mục
                        if (ItemFeeVO[i].zfxz=="9")
                        {
                            var mbmx = _sysChargeTemplateItemMappRepo.getmbmx(ItemFeeVO[i].sfxm, this.OrganizeId);
                            if (mbmx == null|| mbmx.Count==0)
                            {
                                throw new FailedException("Không tìm thấy chi tiết mẫuthu phí tương ứng");
                            }
                            var newid= Guid.NewGuid().ToString();
                            for (int k = 0; k < mbmx.Count; k++)
                            {
                                HospItemBillingEntity zyXmjfb = new HospItemBillingEntity();
                                zyXmjfb.zyh = zyh;
                                zyXmjfb.tdrq = ItemFeeVO[i].tdrq;
                                zyXmjfb.sfxm = mbmx[k].sfxmcode;
                                zyXmjfb.dl = mbmx[k].sfdlcode;
                                zyXmjfb.ys = ItemFeeVO[i].ys;
                                zyXmjfb.ks = ItemFeeVO[i].ks;
                                zyXmjfb.ysmc = ItemFeeVO[i].ysmc;
                                zyXmjfb.ksmc = ItemFeeVO[i].ksmc;
                                zyXmjfb.bq = zyxx.bq;
                                zyXmjfb.cw = zyxx.cw;
                                zyXmjfb.dj = mbmx[k].dj;
                                zyXmjfb.sl = (mbmx[k].sl ?? 0) * ItemFeeVO[i].sl;
                                zyXmjfb.jfdw = mbmx[k].dw;
                                zyXmjfb.zxks = this.UserIdentity.DepartmentCode;

                                zyXmjfb.yzxz = ((int)EnumYZXZ.LSYZ).ToString();
                                zyXmjfb.yzzt = ((int)EnumYZZT.WCX).ToString();
                                //zyXmjfb.zfxz = ItemFeeVO[i].zfxz;
                                zyXmjfb.zfxz = _BookkeepInHosDmnService.getzfxzById(mbmx[k].sfxmcode,this.OrganizeId);
                                zyXmjfb.zfbl = ItemFeeVO[i].zfbl;
                                zyXmjfb.dcztbs = newid;
                                zyXmjfb.cxzyjfbbh = 0;
                                zyXmjfb.OrganizeId = this.OrganizeId;
                                zyXmjfb.ztbh = mbmx[k].sfmbbh;
                                zyXmjfb.ztsl= ItemFeeVO[i].ztsl;
                                zyXmjfb.Create(true, SettKeyHelper.GetNewPrimaryKeyInt("zy_xmjfb"));
                                xmjfbEntitylist.Add(zyXmjfb);
                                //_HospItemFeeApp.SubmitForm(zyXmjfb, null);
                            }
                        }
                        else
                        {
                            HospItemBillingEntity zyXmjfb = new HospItemBillingEntity();
                            zyXmjfb.zyh = zyh;
                            zyXmjfb.tdrq = ItemFeeVO[i].tdrq;
                            zyXmjfb.sfxm = ItemFeeVO[i].sfxm;
                            zyXmjfb.dl = ItemFeeVO[i].dl;
                            zyXmjfb.ys = ItemFeeVO[i].ys;
                            zyXmjfb.ks = ItemFeeVO[i].ks;
                            zyXmjfb.ysmc = ItemFeeVO[i].ysmc;
                            zyXmjfb.ksmc = ItemFeeVO[i].ksmc;
                            zyXmjfb.bq = zyxx.bq;
                            zyXmjfb.cw = zyxx.cw;
                            zyXmjfb.dj = ItemFeeVO[i].dj;
                            zyXmjfb.sl = ItemFeeVO[i].sl;
                            zyXmjfb.jfdw = ItemFeeVO[i].dw;
                            zyXmjfb.zxks = this.UserIdentity.DepartmentCode;

                            zyXmjfb.yzxz = ((int)EnumYZXZ.LSYZ).ToString();
                            zyXmjfb.yzzt = ((int)EnumYZZT.WCX).ToString();
                            zyXmjfb.zfxz = ItemFeeVO[i].zfxz;
                            zyXmjfb.zfbl = ItemFeeVO[i].zfbl;
                            zyXmjfb.cxzyjfbbh = 0;
                            zyXmjfb.OrganizeId = this.OrganizeId;
                            //_HospItemFeeApp.SubmitForm(zyXmjfb, null);
                            zyXmjfb.Create(true, SettKeyHelper.GetNewPrimaryKeyInt("zy_xmjfb"));
                            xmjfbEntitylist.Add(zyXmjfb);
                        }
                        #endregion
                    }
                    else
                    {
                        #region Bảng tính phí thuốc
                        HospDrugBillingEntity zyYpjfb = new HospDrugBillingEntity();
                        zyYpjfb.zyh = zyh;
                        zyYpjfb.tdrq = ItemFeeVO[i].tdrq;
                        zyYpjfb.yp = ItemFeeVO[i].sfxm;
                        zyYpjfb.dl = ItemFeeVO[i].dl;
                        zyYpjfb.ys = ItemFeeVO[i].ys;
                        zyYpjfb.ysmc = ItemFeeVO[i].ysmc;
                        zyYpjfb.ks = ItemFeeVO[i].ks;
                        zyYpjfb.ksmc = ItemFeeVO[i].ksmc;
                        zyYpjfb.bq = zyxx.bq;
                        zyYpjfb.cw = zyxx.cw;
                        zyYpjfb.dj = ItemFeeVO[i].dj;
                        zyYpjfb.sl = ItemFeeVO[i].sl;
                        zyYpjfb.jfdw = ItemFeeVO[i].dw;

                        zyYpjfb.lyyf = ItemFeeVO[i].yfdm;
                        zyYpjfb.yzxz = ((int)EnumYZXZ.LSYZ).ToString();
                        zyYpjfb.yzzt = ((int)EnumYZZT.WCX).ToString();
                        zyYpjfb.zfxz = ItemFeeVO[i].zfxz;
                        zyYpjfb.zfbl = ItemFeeVO[i].zfbl;
                        zyYpjfb.cxzyjfbbh = 0;
                        zyYpjfb.ybbm = ItemFeeVO[i].ybbm;
                        zyYpjfb.yfdm = ItemFeeVO[i].yfdm;
                        zyYpjfb.OrganizeId = this.OrganizeId;
                        zyYpjfb.cls = (short?)ItemFeeVO[i].cls;
                        zyYpjfb.zxks = ItemFeeVO[i].yfdm; //ghi sổ bổ sung thuốc  Khoa thực hiện mặc định là
                        zyYpjfb.Create(true, SettKeyHelper.GetNewPrimaryKeyInt("zy_ypjfb"));
                        //_HospMedicinFeeApp.SubmitForm(zyYpjfb, null);
                        ypjfbEntitylist.Add(zyYpjfb);
                        //_hospdrugbillingRepo.Updatezyaddfee(OrganizeId, ItemFeeVO[i].sl, ItemFeeVO[i].yfdm, ItemFeeVO[i].sfxm);
                        #endregion
                    }
                }
                //Tồn tại vật tư tiêu hao cần kiểm tra tồn kho
                var wzList = xmjfbEntitylist.Where(p => p.sfxm.Contains("wz")).ToList();
                if (wzList.Count>0)
                {
                    var kclist= _BookkeepInHosDmnService.Verification(string.Join(",", wzList.Where(m => m.sfxm.Contains("wz")).Select(p => p.sfxm)),this.OrganizeId, ItemFeeVO[0].yfdm);
                    foreach (var item in wzList) {
                        var resultNum = kclist.Where(p => p.productCode == item.sfxm && p.kykc >= item.sl).Count();
                        if(resultNum == 0)
                            throw new FailedException("Mã là："+item.sfxm +" Không đủ tồn kho！");
                    }
                }
                //Lưu
                string result=_BookkeepInHosDmnService.SaveFee(xmjfbEntitylist, ypjfbEntitylist);
                if (string.IsNullOrWhiteSpace(result))
                {
                    string jfbhs = string.Join(",", wzList.Where(m => m.sfxm.Contains("wz")).Select(p => p.jfbbh));
                    _hospItemBillingRepo.Updatezyaddfee(OrganizeId, jfbhs,ItemFeeVO[0].yfdm,this.UserIdentity.UserCode);
                    //cập nhậtcisnhập việnBệnh nhân预览phải侧phíThẻ片thông tin
                    _hospdrugbillingRepo.Updatezy_brxxexpand(OrganizeId, zyh);
                }
                else
                    throw new FailedException(result);
            }
            else
            {
                throw new FailedException("Không có thông tinghi sổ！");
            }
            return Success("Lưu thành công！");
        }

        public IActionResult ChargeItemTemplate()
        {
            return View();
        }

        /// <summary>
        /// Theo khoa Lấy mẫu mụcthu phí
        /// </summary>
        /// <returns></returns>
        public IActionResult GetChargeItemData(string ks)
        {
            var data = _BookkeepInHosDmnService.GetChargeTemplate(ks, OrganizeId);
            if (data != null)
            {
                return Content(data.ToJson());
            }
            return View();
        }

        /// <summary>
        /// Theo số hiệu mẫuthu phí Lấy nội dung mẫu （Danh sách mục）
        /// </summary>
        /// <param name="sfxmbh"></param>
        /// <returns></returns>
        public IActionResult loadSfxmDataBySfmbbh(string sfmbbh)
        {
            var data = _BookkeepInHosDmnService.GetChargeItemContent(sfmbbh, this.OrganizeId);
            if (data != null)
            {
                return Content(data.ToJson());
            }
            return Content("[]");
        }

        #endregion HIS ghi sổ nhập viện

        #region ghi sổ nhập viện ghi sổ theo kế hoạch，写jzjh，Thông qua thực hiện，Vàozy_xmjfb zy_ypjfbghi dữ liệu tính phí 2018.03Logic một phần kế hoạch ngoại trú cũng ở đây

        #region

        /// <summary>
        /// ghi sổ nhập viện
        /// Phương thứcghi sổ：Dài hạn or Tạm thời
        /// </summary>
        /// <returns></returns>
        public IActionResult Accounting()
        {
            //Công tắc giới hạn số tiềnghi sổ của trị liệu sư
            var IsUpperLimitReminder = _sysConfigRepo.GetValueByCode("xt_UpperLimitReminder", this.OrganizeId);
            ViewBag.IsUpperLimitReminder = IsUpperLimitReminder;
            ViewBag.IsBlh = _sysConfigRepo.GetByCode("IsBlh", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("IsBlh", OrganizeId).Value;
            var isRehabDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(this.UserIdentity.StaffId, "RehabDoctor");
            if (isRehabDoctor)
            {
                ViewBag.CurYsStaffId = this.UserIdentity.StaffId;
                ViewBag.CurYs = this.UserIdentity.rygh;
                ViewBag.CurYsmc = this.UserIdentity.UserName;
                ViewBag.CurKs = this.UserIdentity.DepartmentCode;
                ViewBag.CurKsmc = this.UserIdentity.DepartmentName;
            }

            //ghi sổ theo kế hoạch Đơn giá Có thể chỉnh sửa Cấu hình công tắc
            ViewBag.jhjz_dj_editable = _sysConfigRepo.GetBoolValueByCode("jhjz.dj.editable", OrganizeId);

            return View();
        }

        /// <summary>
        /// ghi sổ thực hiện 计划 thực hiện页
        /// </summary>
        /// <returns></returns>
        public IActionResult AccountingExecution()
        {
            var isRehabDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(this.UserIdentity.StaffId, "RehabDoctor");
            if (isRehabDoctor)
            {
                //ViewBag.CurYsStaffId = this.UserIdentity.StaffId;
                ViewBag.CurYs = this.UserIdentity.rygh;
            }


            ViewBag.OrgId = this.OrganizeId;
            return View();
        }

        public IActionResult ExecutionDateComfirm()
        {
            ViewBag.DefaultPrevDate = _sysConfigRepo.GetIntValueByCode("AccountingExecution_DefaultPrevDate", this.OrganizeId, 3);

            return View();
        }

        /// <summary>
        /// Truy vấn thực hiện
        /// </summary>
        /// <returns></returns>
        public IActionResult AccountingExecutionQuery()
        {
            return View();
        }

        /// <summary>
        /// Kế hoạchghi sổ Truy vấn
        /// </summary>
        /// <returns></returns>
        public IActionResult AccountingPlan()
        {
            return View();
        }

        /// <summary>
        /// PopLớp Dừng kế hoạchghi sổ Chọn 预（Dừng）Ngày
        /// </summary>
        /// <returns></returns>
        public IActionResult StopPlan()
        {
            return View();
        }

        /// <summary>
        /// Theo mã nhập viện lấy Bệnh nhân ghi sổ thông tin
        /// </summary>
        /// <param name="zyh">Mã nhập viện</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetAccountingStatusDetail(string zyh)
        {
            var data = _bookkeepInpatientApp.GetAccountingStatusDetail(zyh);
            data.patInfo.cqrq= _PatientBasicInfoDmnService.IFCQRQISJZSJ(zyh, this.OrganizeId);
            return Success(null, data);
        }

        /// <summary>
        /// Gửi kế hoạchghi sổ
        /// </summary>
        /// <param name="zyh">Mã nhập viện</param>
        /// <param name="xmList">Danh sách mụcghi sổ</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitAccountingPlan(string zyh, IList<InpatientAccountingPlanItemDto> xmList)
        {
            _bookkeepInpatientApp.SubmitAccountingPlan(zyh, xmList);
            return Success("Gửi thành công");
        }

        /// <summary>
        /// Hủy dừng trước
        /// </summary>
        /// <param name="jzjhmxIdStr"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult CancelPreStopAccountingPlan(string jzjhmxIdStr)
        {
            _bookkeepInpatientApp.CancelPreStopAccountingPlan(jzjhmxIdStr);
            return Success("Thao tác thành công");
        }

        /// <summary>
        /// Truy vấn Chờ thực hiện Kế hoạchghi sổ Danh sách
        /// </summary>
        /// <param name="zyhList"></param>
        /// <param name="zxrq"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult WaitingAccountingPlanQuery(string zyhStr, string from = null)
        {

            var list = _bookkeepInpatientApp.WaitingAccountingPlanQuery((zyhStr ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray(),from);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Thực hiện kế hoạchghi sổ
        /// </summary>
        /// <param name="jzjhmxIdStr"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult ExecuteAccountingPlan(DateTime? zxrq, IList<zxGirdDto> zxItem, string from = null)
        {
            _bookkeepInpatientApp.ExecuteAccountingPlan(zxItem, zxrq, from);
            return Success("Thực hiện thành công, đã tính phí");
        }

        /// <summary>
        /// 终止 Kế hoạchghi sổ
        /// </summary>
        /// <param name="jzjhmxIdStr"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult StopAccountingPlan(string jzjhmxIdStr, DateTime? stopDate
            , string from)
        {
            var mxIdList = (jzjhmxIdStr ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (mxIdList == null || mxIdList.Length == 0)
            {
                throw new FailedException("Thiếu tham số kế hoạchghi sổ");
            }
            if (from == "mz")
            {
                foreach (var mxId in mxIdList)
                {
                    _outPatientDmnService.overAccountingPlan(mxId, this.OrganizeId, this.UserIdentity.rygh);
                }
            }
            else
            {
                foreach (var mxId in mxIdList)
                {
                    _BookkeepInHosDmnService.StopAccountingPlan(mxIdList.ToList(), DateTime.Now.Date, this.UserIdentity.DepartmentCode, this.UserIdentity.rygh);
                }
            }
            return Success("Thao tác thành công");
        }


        /// <summary>
        /// Truy vấn thực hiện
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zls"></param>
        /// <param name="zxzt"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetAccountingExecutionQuery(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, string zls
            , int? zxzt, string from, string cxzt, string jzjhmxId
            , string sfzt)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var list = new
            {
                rows = from == "mz"
                ? _BookkeepInHosDmnService.SelectOutAccountingExecuteList(pagination, keyword, kssj, jssj, zxzt, orgId, zls, cxzt, jzjhmxId, sfzt)
                : _BookkeepInHosDmnService.SelectAccountingExecuteList(pagination, keyword, kssj, jssj, zxzt, orgId, zls, cxzt, jzjhmxId, sfzt),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Thực hiện kế hoạchghi sổ XuấtExcel
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zls"></param>
        /// <param name="zxzt"></param>
        /// <param name="from"></param>
        /// <param name="cxzt"></param>
        /// <param name="jzjhmxId"></param>
        /// <param name="cols"></param>
        /// <param name="colStanWidth"></param>
        /// <returns></returns>
        public IActionResult AccountingExecutionExportExcel(string keyword, DateTime? kssj, DateTime? jssj, string zls
            , int? zxzt, string from, string cxzt, string jzjhmxId
            , string cols, int colStanWidth
            , bool? isContainFilter
            , string sfzt)
        {
            return Error("Chức năng này chưa được chuyển đổi sang .NET Core trong giai đoạn này (tích hợp BHYT GuiAn / xuất Excel / CIS).");
        }

        /// <summary>
        /// Truy vấn kế hoạchghi sổ
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult AccountingPlanQuery(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj
            , int? zxzt, string from,int? zsftx,int? sycstx
            , string sfzt
            , int? yzxz)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var list = new
            {
                rows =
                   from == "mz"
                   ? _outPatientDmnService.SelectAccountingPlanList(pagination, keyword, kssj, jssj, zxzt, orgId, zsftx, sycstx, sfzt)
                   : _BookkeepInHosDmnService.SelectAccountingPlanList(pagination, keyword, kssj, jssj, zxzt, orgId, yzxz),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// XuấtExcel Chọn chế độ xem cột
        /// </summary>
        /// <returns></returns>
        public IActionResult ExcelExportChooseColumns()
        {
            return View();
        }

        /// <summary>
        /// Xuất kế hoạchghi sổExcel
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zxzt"></param>
        /// <param name="from"></param>
        /// <param name="cols"></param>
        /// <param name="colStanWidth"></param>
        /// <returns></returns>
        public IActionResult AccountingPlanExportExcel(string keyword, DateTime? kssj, DateTime? jssj
            , int? zxzt, string from
            , string cols, int colStanWidth
            , bool? isContainFilter,int? zsftx,int? sycstx
            , string sfzt
            , int? yzxz)
        {
            return Error("Chức năng này chưa được chuyển đổi sang .NET Core trong giai đoạn này (tích hợp BHYT GuiAn / xuất Excel / CIS).");
        }

        /// <summary>
        /// Truy vấn Trị liệu sư Tháng này đã Số tiềnghi sổ（jfb、mzxm、mzcfmx）、Số tiền giới hạnghi sổ
        /// </summary>
        /// <param name="gh"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetStaffjfjeInfo(string gh, string ks)
        {
            decimal jfjesx;
            decimal yjfzje;
            _moneyUpperLimitReminderDmnService.GetStaffjfjeInfo(this.OrganizeId, gh, ks, out jfjesx, out yjfzje);
            return Success(null, new { jfjesx = jfjesx, yjfzje = yjfzje });
        }

        #endregion

        #region ghi sổ ngoại trú bản 4，Lượng điều trị mỗi lần
        /// <summary>
        /// Lưughi sổ ngoại trútrong容
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="xmList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SavepatientAcountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> xmList)
        {
            _outpatientApp.SaveoutpatientAccountInfo(bacDto, xmList, OrganizeId);
            //_outPatientDmnService.SaveoutpatientAccountInfo(bacDto, xmList, OrganizeId, UserIdentity.UserCode);
            return Success("Gửi thành công");
        }


        /// <summary>
        /// Tìm kiếm thông tin cơ bản bệnh nhân
        /// </summary>
        /// <returns></returns>
        public IActionResult PatSearchInfo(string keyword)
        {
            var OrganizeId = this.OrganizeId;
            var list = _refundDmnService.GetBasicInfoSearchListInRegister(keyword, OrganizeId);
            //return Json(list, JsonRequestBehavior.AllowGet);
            return Content(list.ToJson());
        }
        #endregion

        #region Truy vấn kế hoạchghi sổ
        public IActionResult ViewPlanDetail()
        {
            return View();
        }

        public IActionResult UpdateExecForm()
        {
            return View();
        }

        public IActionResult GetFormJson(string keyValue, string from)
        {
            var data = _BookkeepInHosDmnService.GetFormPlan(keyValue, OrganizeId, from);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy chi tiết kế hoạchghi sổ
        /// </summary>
        /// <param name="jzjhmxId"></param>
        /// <returns></returns>
        public IActionResult AccountingPlanDetailQuery(Pagination pagination, string jzjhmxId, string from)
        {

            var data = _BookkeepInHosDmnService.GetFormPlan(pagination, jzjhmxId, this.OrganizeId, from);
            var list = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        ///  Hủy thực hiện
        /// </summary>
        /// <param name="zxbh"></param>
        /// <param name="jzjhmxId"></param>
        /// <returns></returns>
        public IActionResult cancelExce(string zxbh, string jzjhmxId, string from)
        {
            _BookkeepInHosDmnService.RevokeExec(jzjhmxId, zxbh, OrganizeId, from);
            return Success("Hủy thành công");
        }

        public IActionResult updateExc(cxzxGirdDto zxItem, string from)
        {
            if (zxItem != null)
            {
                _BookkeepInHosDmnService.UpdateExec(zxItem, from, OrganizeId);
            }
            return Success("Sửa đổi thành công");
        }
        #endregion

        #endregion ghi sổ nhập viện Kế hoạchghi sổ

        #region ghi sổ nhập viện vàOptimaĐồng bộ dữ liệu，OptimaĐẩy bản ghi điều trị cho chúng tôi，Xác nhậncủa，Vàozy_xmjfb zy_ypjfbghi dữ liệu tính phí

        /// <summary>
        /// ghi sổ nhập viện Index
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientAccountingIndex()
        {
            var isRehabDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(this.UserIdentity.StaffId, "RehabDoctor");
            if (isRehabDoctor)
            {
                ViewBag.CurYsStaffId = this.UserIdentity.StaffId;
                ViewBag.CurYs = this.UserIdentity.rygh;
                ViewBag.CurYsmc = this.UserIdentity.UserName;
                ViewBag.CurKs = this.UserIdentity.DepartmentCode;
                ViewBag.CurKsmc = this.UserIdentity.DepartmentName;
            }
            //ghi sổ nhập viện Đơn giá Có thể chỉnh sửa Cấu hình công tắc
            ViewBag.zyjz_dj_editable = _sysConfigRepo.GetBoolValueByCode("zyjz.dj.editable", OrganizeId);

            return View();
        }

        /// <summary>
        /// Theo mã nhập viện lấy Bệnh nhân ghi sổ thông tin
        /// </summary>
        /// <param name="zyh">Mã nhập viện</param>
        /// <returns></returns>
        public IActionResult GetInpatientAccountingStatusDetail(string zyh)
        {
            var data = _bookkeepInpatientApp.GetInpatientAccountingStatusDetail(zyh);
            return Success(null, data);
        }

        /// <summary>
        /// ghi sổ nhập viện GửiTính phí
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="xmList"></param>
        /// <returns></returns>
        public IActionResult InpatientAccountingSubmitAccounting(string zyh
            , IList<InpatientAccountingItemDto> xmList)
        {
            _BookkeepInHosDmnService.SaveInpatientAccounting(zyh, xmList, this.OrganizeId, this.UserIdentity.UserCode);
            return Success("Gửi thành công");
        }

        /// <summary>
        /// Truy vấn bản ghi điều trị đồng bộ chưa xác nhận
        /// </summary>
        /// <returns></returns>
        public IActionResult GetUnConfirmedSyncedTreatmentRecord(string zyh, string mzh)
        {
            var data = _syncTreatmentServiceRecordDmnService.GetList(OrganizeId, clzt: 1, mzh: mzh, zyh: zyh, zlsgh: this.UserIdentity.rygh);

            return Success(null, data);
        }

        #endregion ghi sổ nhập viện vàOptimaĐồng bộ dữ liệu

        #region quyết toán phí quyết toán xuất viện（nó实là配合nhập việnghi sổ theo kế hoạch加trêncủa，nhưng感觉比较通用）

        /// <summary>
        /// quyết toán phí chưaquyết toánIndex
        /// </summary>
        /// <returns></returns>
        public IActionResult UnSettedFeeSettIndex()
        {
            return View();
        }

        /// <summary>
        /// Lấyquyết toán giữa kỳ gần nhất Kết thúcNgàyquyết toán
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetLastValidMidwaySettTimeJson(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return Error("zyh is required");
            }
            var dt = _BookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);
            return Success(null, dt);
        }

        /// <summary>
        /// quyết toán phí chưaquyết toán Items Query
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetUnSettedFeeItemListJson(string zyh, DateTime? endTime,string ver)
        {
            if (!endTime.HasValue || string.IsNullOrWhiteSpace(zyh))
            {
                return Error("Thiếu mã nhập viện (zyh) hoặc ngày kết thúc.");
            }
            //Ngày bắt đầu
            var startTime = _BookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);
            if (!startTime.HasValue)
            {
                //取Ngày nhập viện
                startTime = new DateTime(1970, 01, 01);
            }
            //Để không tra được
            endTime = endTime ?? new DateTime(1970, 01, 01);

            var list = _BookkeepInHosDmnService.GetChargeItemPaginationList(new Pagination()
            {
                page = 1,
                rows = 100000,
                sidx = "CreateTime"
            }, zyh, this.OrganizeId, startTime.Value, endTime.Value);
            if (ver=="2")
            {
                //Kết quả
                var dataV2 = list.GroupBy(p => p.sfxmmc).Select(p => new
                {
                    sfxmmc = p.First().sfxmmc,
                    sl = p.Sum(i => i.sl),
                    zje = p.Sum(i => Math.Round(i.je, 2, MidpointRounding.AwayFromZero))
                });
                return Content(dataV2.ToJson());
            }
            //Kết quả
            var data = list.GroupBy(p => p.sfxmmc).Select(p => new
            {
                sfxmmc = p.First().sfxmmc,
                sl = p.Sum(i => i.sl),
                zje = p.Sum(i =>Math.Round(i.dj * i.sl,2,MidpointRounding.AwayFromZero))
            });

            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="lastValidMidwaySettTime"></param>
        /// <param name="endTime"></param>
        /// <param name="expectedCount"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult UnSettedItemFeeConfirm(string zyh, DateTime startTime, DateTime endTime, int? expectedCount)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return Error("zyh is required");
            }
            var dbDt = _BookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);
            if (dbDt.HasValue && dbDt.Value != startTime)
            {
                return Error("Trạng tháiquyết toán đã thay đổi trong quá trình xử lý, vui lòng thử lại!");
            }

            _BookkeepInHosDmnService.MidwaySettlement(zyh, this.OrganizeId, startTime, endTime, expectedCount);

            return Success("Thao tác thành công");
        }

        /// <summary>
        /// quyết toán xuất viện
        /// </summary>
        /// <returns></returns>
        public IActionResult DischargeSettlementIndex()
        {
            return View();
        }

        /// <summary>
        /// lấyĐãquyết toán quyết toán giữa kỳListJson
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetMidwaySettedListJson(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return Error("Thiếu mã nhập viện (zyh).");
            }

            var list = _hospSettlementRepo.GetValidList(zyh, this.OrganizeId);

            return Content(list.ToJson());
        }

        /// <summary>
        /// Có tồn tại bản ghi chưaquyết toán hay không
        /// Thông qua thời gianquyết toán giữa kỳ
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult CheckIsExistUnSetted(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return Error("zyh is required");
            }
            DateTime? startTime = null;
            startTime = _BookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);
            if (!startTime.HasValue)
            {
                startTime = new DateTime(1970, 01, 01);  //Chưaquyết toán过của话 từNhập việnBắt đầu算所cóMục
            }
            var jsItemList = _hospItemBillingRepo.GetItemFeeEntityListByTime(zyh, this.OrganizeId, startTime.Value, DateTime.Now);

            return Success(null, jsItemList.Count > 0);
        }

        /// <summary>
        /// quyết toán xuất viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="cyrq"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult DischargeSettlement(string zyh, DateTime cyrq)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return Error("zyh is required");
            }

            if (_sysConfigRepo.GetBoolValueByCode("CYJS_Optima_ZT_Check", this.OrganizeId) == true)
            {
                checkOptimaZyhSettleable(zyh, cyrq);
            }

            DateTime? startTime = null;
            startTime = _BookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);
            if (!startTime.HasValue)
            {
                return Error("Chưa phát sinh phí, không thể xuất viện");
            }
            if (cyrq.Date < startTime.Value.Date)
            {
                return Error("Ngày xuất viện không đúng, không được trước ngàyquyết toán phí");
            }
            var jsItemList = _hospItemBillingRepo.GetItemFeeEntityListByTime(zyh, this.OrganizeId, startTime.Value, DateTime.Now);
            if (jsItemList.Count > 0)
            {
                return Error("Còn phí chưaquyết toán, không thể xuất viện");
            }
            if (_hospAccountingPlanDetailRepo.IQueryable(p => p.zyh == zyh && p.OrganizeId == this.OrganizeId && p.zt == "1").ToList().Any(p => p.zxzt == (int)EnumJzjhZXZT.None
             || p.zxzt == (int)EnumJzjhZXZT.Part))
            {
                return Error("Cònghi sổ chưa thực hiện xong, không thể xuất viện");
            }

            _BookkeepInHosDmnService.DischargeSettlement(zyh, this.OrganizeId, cyrq);

            return Success("Xuất viện thành công");
        }

        /// <summary>
        /// Hủyquyết toán giữa kỳ gần nhất
        /// </summary>
        /// <param name="zyh"></param>
        public IActionResult CancelTheLastMidwaySett(string zyh,string zffs1,string zffsmc1)
        {
            string szId = "";
            decimal refoundmoney;
            _BookkeepInHosDmnService.CancelTheLastMidwaySett(zyh,zffs1, this.OrganizeId,out refoundmoney);
            if (zffs1=="3"&& refoundmoney>0)
            {
                var zhxx = _accountmanageApp.GetHosPatInfo(zyh);//Thông tin tài khoản
                string sjpzh = _accountmanageApp.GetFinRepSJPZH();
                //nạp tiền
                if (!_accountmanageApp.PayDepositPost(new DeposDto
                {
                    patid = zhxx.patid,
                    pzh = sjpzh,
                    szxz = (int)EnumSZXZ.zyjsth,
                    zffsbh = zffs1.ToInt(),
                    zffsmc = zffsmc1,
                    zfje = refoundmoney,
                    zh = zhxx.zh,
                    zhxz = zhxx.zhxz.ToInt()

                }, out szId))
                {
                    throw new FailedException("Nạp tiền thất bại");
                }
            }

            return Success("Thao tác thành công");
        }

        #endregion

        #region private methods

        /// <summary>
        /// Kiểm tra có thểquyết toán xuất viện hay không
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private bool checkOptimaZyhSettleable(string zyh, DateTime cyrq)
        {
            throw new NotSupportedException("Tích hợp Optima/BHYT GuiAn chưa được chuyển đổi sang .NET Core trong giai đoạn này.");
        }

        /// <summary>
        /// lấyTrả vềtrong容trongcủaflaggiá trị
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string resp_GetResultAttr(string xml, string attrName)
        {
            var dt = getDataTableFromXml(xml, "result");
            if (dt != null && dt.Rows.Count == 1)
            {
                var flagRow = dt.Rows[0][attrName];
                if (flagRow != null)
                {
                    return flagRow.ToString().Trim();
                }
            }
            return null;
        }

        /// <summary>
        /// từxmltrong提取tableNamecủaDataTable
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private DataTable getDataTableFromXml(string xml, string tableName)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return null;
            }
            var ds = XmlToDtUntility.XmlToDataSet(xml);
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (ds.Tables[i].TableName.Equals(tableName))
                {
                    return ds.Tables[i];
                }
            }
            return null;
        }

        #endregion private methods


        /// <summary>
        /// Ngoại trú/Nhập kế hoạch nhập viện Lớp nổi tìm kiếm bệnh nhân
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult PatAccountingSearchInfo(string keyword,string from, string brzybzType = null, bool curUserCreate = false)
         {
            var data=  _refundDmnService.GetBasicInfoSearchList(keyword, this.OrganizeId, curUserCreate ? UserIdentity.UserCode : "", "", from, brzybzType);
            return Content(data.ToJson());
        }
    }
}
