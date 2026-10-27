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

namespace Newtouch.Sett.Web.Areas.PatientManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("PatientManage")]

    public class HospiterResController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysPatBasicInfoApp _SysPatBasicInfoApp;
        private readonly ISysCardApp _SysCardApp;
        private readonly ISysCardRepo _sysCardRepository;
        private readonly IPatientBasicInfoDmnService _PatientBasicInfoDmnService;
        private readonly ISysPatAccApp _SysPatAccApp;
        private readonly IAccountManageApp _AccountManageApp;
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
        private readonly ISysPatientBasicInfoRepo _sysPatientBasicInfoRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly IOutPatChargeApp _outPatChargeApp;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly IRefundDmnService _refundDmnService;
        private readonly IHospPatientBasicInfoRepo _hosPatBasicInfoRepository;
        private readonly IGuianRybl21OutInfoRepo _guianRybl21OutInfoRepo;
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;
        private readonly IBaseGaXnhS30Repo _daseGaXnhS30Repo;
        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;
        private readonly ICommonDmnService _commonDmnService;
        private readonly ISysPatientNatureRepo _SysPatiNatureRepo;
        private readonly IG_yb_daysrg_trt_list_bRepo _iG_yb_daysrg_trt_list_bRepo;
        #region hợp tác nông thôn mới 

        private readonly IGuianXnhS04InfoRepo _iGuianXnhS04InfoRepo;
        public HospiterResController(ISysPatBasicInfoApp sysPatBasicInfoApp, ISysCardApp sysCardApp, ISysCardRepo sysCardRepository, IPatientBasicInfoDmnService patientBasicInfoDmnService, ISysPatAccApp sysPatAccApp, IAccountManageApp accountManageApp, ISysDiagnosisRepo sysDiagnosisRepo, ISysPatientBasicInfoRepo sysPatientBasicInfoRepo, IHospPatientBasicInfoRepo hospPatientBasicInfoRepo, ISysConfigRepo sysConfigRepo, IOutpatientRegistRepo outpatientRegistRepo, IOutPatChargeApp outPatChargeApp, ISysOrganizeDmnService sysOrganizeDmnService, IRefundDmnService refundDmnService, IHospPatientBasicInfoRepo hosPatBasicInfoRepository, IGuianRybl21OutInfoRepo guianRybl21OutInfoRepo, IDischargeSettleDmnService dischargeSettleDmnService, IBaseGaXnhS30Repo daseGaXnhS30Repo, IOutPatientSettleDmnService outPatientSettleDmnService, ICommonDmnService commonDmnService, ISysPatientNatureRepo sysPatiNatureRepo, IG_yb_daysrg_trt_list_bRepo iG_yb_daysrg_trt_list_bRepo, IGuianXnhS04InfoRepo iGuianXnhS04InfoRepo)
        {
            _SysPatBasicInfoApp = sysPatBasicInfoApp;
            _SysCardApp = sysCardApp;
            _sysCardRepository = sysCardRepository;
            _PatientBasicInfoDmnService = patientBasicInfoDmnService;
            _SysPatAccApp = sysPatAccApp;
            _AccountManageApp = accountManageApp;
            _sysDiagnosisRepo = sysDiagnosisRepo;
            _sysPatientBasicInfoRepo = sysPatientBasicInfoRepo;
            _hospPatientBasicInfoRepo = hospPatientBasicInfoRepo;
            _sysConfigRepo = sysConfigRepo;
            _outpatientRegistRepo = outpatientRegistRepo;
            _outPatChargeApp = outPatChargeApp;
            _sysOrganizeDmnService = sysOrganizeDmnService;
            _refundDmnService = refundDmnService;
            _hosPatBasicInfoRepository = hosPatBasicInfoRepository;
            _guianRybl21OutInfoRepo = guianRybl21OutInfoRepo;
            _dischargeSettleDmnService = dischargeSettleDmnService;
            _daseGaXnhS30Repo = daseGaXnhS30Repo;
            _outPatientSettleDmnService = outPatientSettleDmnService;
            _commonDmnService = commonDmnService;
            _SysPatiNatureRepo = sysPatiNatureRepo;
            _iG_yb_daysrg_trt_list_bRepo = iG_yb_daysrg_trt_list_bRepo;
            _iGuianXnhS04InfoRepo = iGuianXnhS04InfoRepo;
        }

        #endregion
        #region trang khởi tạo

        public override IActionResult Index()
        {
            ViewBag.IsEditPatientMedicalRecord = _sysConfigRepo.GetBoolValueByCode("PatientMedicalRecord_able", this.OrganizeId, false);
            return base.Index();
        }
        public IActionResult Register()
        {

            ViewBag.zyhpz = _sysConfigRepo.GetBoolValueByCode("xt_zyh", OrganizeId, true).Value ? "ON" : "OFF";
            //是否用卡号搜索，默认OFF，即默认要用病历号搜索
            ViewBag.searchpz = _sysConfigRepo.GetBoolValueByCode("register_search", OrganizeId, false).Value ? "ON" : "OFF";
            ViewBag.isDiagnosisRequired = _sysConfigRepo.GetBoolValueByCode("Inpatient_Registe_Diagnosis_Required", this.OrganizeId);
            ViewBag.CurUserCode = UserIdentity.UserCode;
            ViewBag.CurUserName = UserIdentity.UserName;
            return View();
        }

        public IActionResult MZRegister()
        {
            return View();
        }

        public IActionResult PatientBasic(string keyValue, string mzh = null)
        {
            if (string.IsNullOrWhiteSpace(keyValue) && !string.IsNullOrWhiteSpace(mzh))
            {
                var orgId = this.OrganizeId;
                var patid = _outpatientRegistRepo.IQueryable(p => p.mzh == mzh && p.OrganizeId == orgId).Select(p => p.patid).FirstOrDefault();
                if (patid > 0)
                {
                    var url = Request.Path.Value + Request.QueryString.Value;
                    url = url.Replace("mzh=" + mzh, "keyValue=" + patid);
                    return Redirect(url);
                }
                else
                {
                    return Content("Thiếu tham số");
                }
            }
            ViewBag.blhpz = _sysConfigRepo.GetBoolValueByCode("xt_blh", OrganizeId, false).Value ? "ON" : "OFF";
            ViewBag.zjpz = _sysConfigRepo.GetBoolValueByCode("xt_zj", OrganizeId, true).Value ? "ON" : "OFF";
            ViewBag.khpz = _sysConfigRepo.GetBoolValueByCode("xt_kh", OrganizeId, true).Value ? "ON" : "OFF";
            return View();
        }

        public IActionResult inpatientRegister()
        {
            ViewBag.mzhpz = _sysConfigRepo.GetBoolValueByCode("xt_mzh", OrganizeId, true).Value ? "ON" : "OFF";
            ViewBag.IsDefaultDepart = _sysConfigRepo.GetByCode("IsDefaultDepart", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("IsDefaultDepart", OrganizeId).Value;
            ViewBag.IsDefaultDoc = _sysConfigRepo.GetByCode("IsDefaultDoc", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("IsDefaultDoc", OrganizeId).Value;
            ViewBag.IsOpenSfqr = _sysConfigRepo.GetValueByCode("xt_isopen_sfqr", OrganizeId) == "ON";
            //门诊登记完成后，跳转页面配置
            ViewBag.TZPZ_OutpatientRegister = _sysConfigRepo.GetByCode("TZPZ_OutpatientRegister", OrganizeId) == null ? "" : _sysConfigRepo.GetByCode("TZPZ_OutpatientRegister", OrganizeId).Value;
            return View();
        }

        /// <summary>
        /// 取消挂号
        /// </summary>
        /// <returns></returns>
        public IActionResult CancelRegister()
        {
            return View();
        }
        /// <summary>
        /// 医保病人基本信息、账户信息
        /// </summary>
        /// <returns></returns>
        public IActionResult YbPatientBasicInfo()
        {
            return View();
        }
        
        /// <summary>
        /// 医保目录下载 查询 字典
        /// </summary>
        /// <returns></returns>
        public IActionResult MedicalInsuranceCatalogue()
        {
            return View();
        }

        //加载数据
        public IActionResult CatalogueData(Pagination pagination, string tbname, string key)
        {
            var datapan = new
            {
                rows = _iG_yb_daysrg_trt_list_bRepo.Get_G_yb_mluCommon_Info(pagination, tbname, key),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(datapan.ToJson());
        }

        public IActionResult GetVer(string tbname)
        {

            switch (tbname)
            {
                case "1301":
                    tbname = "G_yb_wm_tcmpat_info_b";
                    break;
                case "1302":
                    tbname = "G_yb_tcmherb_info_b";
                    break;
                case "1303":
                    tbname = "G_yb_selfprep_info_b";
                    break;
                case "1305":
                    tbname = "G_yb_trt_serv_b";
                    break;
                case "1306":
                    tbname = "G_yb_mcs_info_b";
                    break;
                case "1307":
                    tbname = "G_yb_diag_list_b";
                    break;
                case "1308":
                    tbname = "G_yb_oprn_std_b";
                    break;
                case "1309":
                    tbname = "G_yb_opsp_dise_list_b";
                    break;
                case "1310":
                    tbname = "G_yb_dise_setl_list_b";
                    break;
                case "1311":
                    tbname = "G_yb_daysrg_trt_list_b";
                    break;
                case "1313":
                    tbname = "G_yb_tmor_mpy_b";
                    break;
                case "1314":
                    tbname = "G_yb_tcm_diag_b";
                    break;
                case "1315":
                    tbname = "G_yb_tcmsymp_type_b";
                    break;
                case "1320":
                    tbname = "G_yb_zypfklmu_list_b";
                    break;
                case "1321":
                    tbname = "G_yb_ylfuxm_new_list_b";
                    break;
                default:
                    break;
            }
            var data = _iG_yb_daysrg_trt_list_bRepo.Header(tbname.Trim()) ?? "0000";
            return Content(data == "null" ? "0000" : data);
        }

        public IActionResult YibaoInterfaceCancel()
        {
            return View();
        }
        #endregion

        #region bệnh nhân Quản lý
        /// <summary>
        /// 病人管理查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <param name="xlbrbz"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetGridJson(Pagination pagination,string zjh)
        {
            var data = new
            {
                rows = _PatientBasicInfoDmnService.GetList(pagination, this.OrganizeId,zjh),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }
        #endregion

        #region BHYT bất thường giao dịch
        public IActionResult GetCqybGridJson(Pagination pagination, string isjs, string lx,string keyword)
        {
            var data = new
            {
                rows = _PatientBasicInfoDmnService.GetYbCancelList(pagination, isjs, lx, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        #endregion

        #region Thẻ một cửa Nhật ký
        public IActionResult ModifyLog()
        {
            return View();
        }
        /// <summary>
        /// 一卡通日志
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetModifyLog(Pagination pagination, string xm)
        {

            var data = new
            {
                rows = _sysPatientBasicInfoRepo.GetModifyLog(pagination, this.OrganizeId, xm),
                pagination.total,
                pagination.page,
                pagination.records
            };

            return Content(data.ToJson());
        }

        public IActionResult LogDetailsView()
        {

            return View();
        }

        public IActionResult GetDetailsData(string Id)
        {
            var data = _sysPatientBasicInfoRepo.GetDetailsData(Id, this.OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult CardManagement()
        {
            ViewBag.khpz = _sysConfigRepo.GetBoolValueByCode("xt_kh", OrganizeId, true).Value ? "ON" : "OFF";
            return View();
        }

        /// <summary>
        /// 卡管理加载数据
        /// </summary>
        /// <param name="patId">patId</param>
        /// <returns></returns>
        public IActionResult GetCardNoInfo(string patId)
        {
            var data = _sysPatientBasicInfoRepo.GetCardNoInfo(patId, this.OrganizeId, "");

            return Content(data.ToJson());
        }

        public IActionResult SubmitCard(SysHosBasicInfoVO PatientBasicAndCardInfoVO)
        {
            var data = _sysPatientBasicInfoRepo.GetCardNoInfo(PatientBasicAndCardInfoVO.patid.ToString(), this.OrganizeId, PatientBasicAndCardInfoVO.cardtype).Where(p=>p.CardType!=((int)EnumCardType.XNK).ToString()).ToList();
            SysCardEntity newCardEntity = null;
            newCardEntity = _sysCardRepository.GetCardEntity(PatientBasicAndCardInfoVO.cardtype, PatientBasicAndCardInfoVO.kh, this.OrganizeId);
            if (data.Count > 0)
            {
                return Success("Cùng loại không thể thêm nhiều thẻ");
            }
            //验卡号重复使用
            if (newCardEntity != null)
            {
                throw new FailedException("Số thẻ Đã Tồn tại");
            }
            else
            {
                _sysPatientBasicInfoRepo.SubmitCard(PatientBasicAndCardInfoVO, this.OrganizeId);
                return Success("Thao tác thành công");
            }
        }

        public IActionResult CardVoids(string CardId)
        {

            _sysPatientBasicInfoRepo.CardVoids(CardId, this.OrganizeId, UserIdentity.UserCode);
            return Success("Hủy bỏ Thành công");
        }


        #endregion

        #region Nhập viện đăng ký
        //public IActionResult GetWardbyDept(string ks,string keyword)
        //{
        //    var data = _commonDmnService.GetWardbyDept(OrganizeId, ks, keyword);
        //    return Content(data.ToJson());
        //}

        /// <summary>
        /// 新建登记 保存卡号和病人基本信息
        /// </summary>
        /// <param name="PatientBasicAndCardInfoVO"></param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult SubmitForm(SysHosBasicInfoVO PatientBasicAndCardInfoVO)
        {
            _PatientBasicInfoDmnService.SavePatBasicCardInfo(PatientBasicAndCardInfoVO, this.OrganizeId, UserIdentity.UserCode);
            return Success("Thao tác thành công。");
        }

        [HttpGet]

        public IActionResult GetFormJson(string keyValue,string zjh)
        {
            var _vo = _PatientBasicInfoDmnService.GetSyspatientFormJson(keyValue, OrganizeId,zjh);//GetPatientBasicByPatid(keyValue, this.OrganizeId);
            var data = _vo.ToJson();
            return Content(data);
        }

        [HttpGet]

        public IActionResult GetryzdSelect(string ryzd,string ybnhlx)
        {
            if (string.IsNullOrEmpty(ybnhlx))
            {
                ybnhlx = null;
            }
            var data = _sysDiagnosisRepo.GetList(this.OrganizeId, ryzd, "WM", ybnhlx);
            var zydata = _sysDiagnosisRepo.GetList(this.OrganizeId, ryzd, "TCM", ybnhlx);
            List<ZDSelect> zdlist = new List<ZDSelect>();
            foreach (SysDiagnosisVEntity item in zydata)
            {
                ZDSelect a = new ZDSelect();
                a.zdbh = item.zdCode;
                a.icd10 = item.icd10 == null ? "" : item.icd10;
                a.zdmc = item.zdmc;
                a.py = item.py;
                a.zdnm = item.zdId;
                zdlist.Add(a);
            }
            foreach (SysDiagnosisVEntity item in data)
            {
                ZDSelect a = new ZDSelect();
                a.zdbh = item.zdCode;
                a.icd10 = item.icd10 == null ? "" : item.icd10;
                a.zdmc = item.zdmc;
                a.py = item.py;
                a.zdnm = item.zdId;
                zdlist.Add(a);
            }
            return Content(zdlist.ToJson());
        }

        /// <summary>
        /// 检验自费卡是否正常
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="zjh"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult CheckCardState(string kh, string zyh)
        {
            var blh = "";
            //是否用卡号搜索，默认OFF，即默认要用病历号搜索
            var searchpz = _sysConfigRepo.GetBoolValueByCode("register_search", OrganizeId, false).Value ? "ON" : "OFF";
            if (searchpz == "ON" && !string.IsNullOrWhiteSpace(kh))
            {
                //var patid = _SysCardApp.GetPatidByCardNo(kh);
                //blh = _hospPatientBasicInfoRepo.IQueryable().FirstOrDefault(p => p.kh == kh).blh;
                var patid = _SysCardApp.GetPatidByCardNo(kh);
                if (!string.IsNullOrWhiteSpace(patid))
                {
                    var intPatid = Convert.ToInt32(patid);
                    blh = _sysPatientBasicInfoRepo.IQueryable().FirstOrDefault(p => p.patid == intPatid).blh;
                }
            }
            else if (!string.IsNullOrWhiteSpace(zyh))
            {
                blh = _hospPatientBasicInfoRepo.GetBlhByZyh(zyh, OrganizeId);
            }
            //var result = false;
            //var _vo = _PatientBasicInfoDmnService.GetPatBasicCardInfo(patid, out result, OrganizeId).FirstOrDefault();
            //var data = _vo.ToJson();
            return Success(null, blh);
        }

        /// <summary>
        /// 获取最新卡号
        /// </summary>
        /// <returns></returns>
        public IActionResult GetNewCardNoAndBLH(bool khsc, bool blhsc)
        {
            var vo = new SysHosBasicInfoVO();
            var khpz = _sysConfigRepo.GetBoolValueByCode("xt_kh", OrganizeId, true).Value ? "ON" : "OFF";
            if (khpz == "ON" && khsc)
            {
                vo.kh = _SysCardApp.GetCardSerialNo();
            }
            var blhpz = _sysConfigRepo.GetBoolValueByCode("xt_blh", OrganizeId, false).Value ? "ON" : "OFF";
            if (blhpz == "ON" && blhsc)
            {
                vo.blh = _SysPatBasicInfoApp.Getblh();
            }
            return Content(vo.ToJson());
        }

        /// <summary>
        /// 住院登记点击保存时操作
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public IActionResult SaveSysHosBasicInfo(SysHosBasicInfoVO vo,bool isybjy)
        {
            var yssfz = new CqybGjbmInfoVo();
            string res;
            string zyh = _PatientBasicInfoDmnService.SaveSysBasicAccountInfo(vo, OrganizeId, out res);
            if (isybjy)
            {
                yssfz = _outPatientSettleDmnService.GetDepartmentDoctorIdC(OrganizeId, vo.ks, vo.doctor);
            }
            //更新报警额
            if (!string.IsNullOrWhiteSpace(vo.bje.ToString())) {
                _PatientBasicInfoDmnService.updateZybrxxkExpandBje(zyh, OrganizeId,  vo.bje);
            }
            var data = new
            {
                zyh = zyh,
                yszfz= yssfz,
            };
            return Success(res, data);
        }

        /// <summary>
        /// 取消入院
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult CancelAdmission(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("không có Thông tin nhập viện，Không thể hủy nhập viện！");
            }
            if (_PatientBasicInfoDmnService.CheckHasFee(zyh, this.OrganizeId))
            {
                throw new FailedException("có Chưa quyết toán của Nhập viện Chi phí bản ghi，Không thể hủy nhập viện。");
            }
            //if (_PatientBasicInfoDmnService.BoolRuQu(zyh, this.OrganizeId))
            //{
            //    throw new FailedException("该病人已在病区中，不能取消入院！");
            //}
            _PatientBasicInfoDmnService.CancelAdmission(zyh, this.OrganizeId);
            return Success("Hủy nhập viện thành công！");
        }
	    public IActionResult CheckCancelAdmission(string zyh)
	    {
		    if (string.IsNullOrWhiteSpace(zyh))
		    {
			    throw new FailedException("không có Thông tin nhập viện，Không thể hủy nhập viện！");
		    }
		    if (_PatientBasicInfoDmnService.CheckHasFee(zyh, this.OrganizeId))
		    {
			    throw new FailedException("có Chưa quyết toán của Nhập viện Chi phí bản ghi，Không thể hủy nhập viện。");
		    }
            //if (_PatientBasicInfoDmnService.BoolRuQu(zyh, this.OrganizeId))
            //{
            //    throw new FailedException("该病人已在病区中，不能取消入院！");
            //}
            return Success("Hủy nhập viện thành công！");
	    }
		/// <summary>
		/// 根据patid获取住院信息
		/// </summary>
		/// <param name="patid"></param>
		/// <returns></returns>
		public IActionResult GetinpatientInfo(string patid)
        {
            bool result;
            var _vo = _PatientBasicInfoDmnService.GetPatBasicCardInfo(patid, out result, OrganizeId)[0];
            if (!string.IsNullOrWhiteSpace(_vo.zyh))
            {
              var S04entity=  _iGuianXnhS04InfoRepo.FindEntity(p=>p.zyh==_vo.zyh&&p.OrganizeId==OrganizeId);
                if (S04entity!=null)
                {
                    _vo.inpId = S04entity.inpId;
                }
            }
            var data = _vo.ToJson();
            return Content(data);
        }
        #endregion

        #region in

        /// <summary>
        /// 住院基本信息打印
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        public IActionResult PrinyZYInfo(ZYInfoVO print)
        {
            if (print.patid == null)
            {
                return Error("Thông tin cơ bản của bệnh nhân chưa đầy đủ，Không thể in Thông tin nhập viện");
            }
            var xtInfo = _SysPatBasicInfoApp.GetInfoByPatid(print.patid);
            print.phone = xtInfo.phone;
            print.webchat = xtInfo.wechat;
            print.email = xtInfo.email;
            switch (xtInfo.zjlxfs)
            {
                case "1":
                    xtInfo.zjlxfs = "Điện thoại";
                    break;
                case "2":
                    xtInfo.zjlxfs = "Điện thoại di động";
                    break;
                case "3":
                    xtInfo.zjlxfs = "WeChat";
                    break;
                case "4":
                    xtInfo.zjlxfs = "Email";
                    break;
                default:
                    break;
            }
            switch (print.hy)
            {
                case "0":
                    print.hy = "Chưa hôn nhân";
                    break;
                case "1":
                    print.hy = "Đã hôn nhân";
                    break;

            }
            switch (print.lxrgx)
            {
                case "0":
                    print.lxrgx = "Khác";
                    break;
                case "1":
                    print.lxrgx = "Vợ chồng";
                    break;
                case "2":
                    print.lxrgx = "Cha con trai";
                    break;
                case "3":
                    print.lxrgx = "Mẹ con trai";
                    break;
                case "4":
                    print.lxrgx = "Cha con gái";
                    break;
                case "5":
                    print.lxrgx = "Anh em";
                    break;
                case "6":
                    print.lxrgx = "Chị em";
                    break;
            }
            switch (print.lxrgx2)
            {
                case "0":
                    print.lxrgx2 = "Khác";
                    break;
                case "1":
                    print.lxrgx2 = "Vợ chồng";
                    break;
                case "2":
                    print.lxrgx2 = "Cha con trai";
                    break;
                case "3":
                    print.lxrgx2 = "Mẹ con trai";
                    break;
                case "4":
                    print.lxrgx2 = "Cha con gái";
                    break;
                case "5":
                    print.lxrgx2 = "Anh em";
                    break;
                case "6":
                    print.lxrgx2 = "Chị em";
                    break;
            }
            _SysPatBasicInfoApp.PrinZYInfo(print);
            return Success("in Thành công！");
        }

        /// <summary>
        /// 打印腕带
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        public IActionResult PrintWDInfo(WDInfoVO print)
        {

            if (string.IsNullOrWhiteSpace(print.patid) || print == null)
            {
                return Error("Thông tin cơ bản của bệnh nhân chưa đầy đủ，Không thể in Vòng đeo tay");
            }
            print.Flag = "1";
            _SysPatBasicInfoApp.PrintWDInfo(print);
            return Success("in Thành công！");
        }
        #endregion

        #region ngoại trú đăng ký khám
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public IActionResult InpatientSubmit(OutpatientRegistEntity OutpatientRegistEntity, int? keyValue)
        {
            OutpatientRegistEntity.ghf = 0m;
            OutpatientRegistEntity.zlf = 0m;
            OutpatientRegistEntity.ckf = 0m;
            OutpatientRegistEntity.gbf = 0m;

            string mzh;
            var mzhpz = _sysConfigRepo.GetBoolValueByCode("xt_mzh", OrganizeId, true).Value ? "ON" : "OFF";

            var res = _outpatientRegistRepo.SubmitForm(OutpatientRegistEntity, keyValue, mzhpz, OrganizeId, this.UserIdentity.UserCode, out mzh);
            _sysPatientBasicInfoRepo.UpdatelxrInfo(OutpatientRegistEntity.patid.ToString(), OrganizeId, OutpatientRegistEntity.lxr, OutpatientRegistEntity.lxrgx, OutpatientRegistEntity.lxrdh, UserIdentity.UserCode);
            return Success(res, mzh);
        }

        public IActionResult Updatebrjbxx(string kh, string zjh, string cbdbm, string cblb, string grbh, string xzlx)
        {
            _sysPatientBasicInfoRepo.Updateybxx(kh,zjh,cbdbm,cblb,grbh,xzlx,OrganizeId, UserIdentity.UserCode);
            return Success();
        }

        public IActionResult UpdateZt(string patId,string isqy)
        {
            _sysPatientBasicInfoRepo.UpdateZt(patId, OrganizeId, isqy, UserIdentity.UserCode);
            return Success();
        }

        /// <summary>
        /// 病人查询浮层
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult PatSearchInfo(string keyword, string orgId)
        {
            var OrganizeId = orgId;
            if (string.IsNullOrWhiteSpace(orgId))
            {
                OrganizeId = this.OrganizeId;
            }
            var list = _refundDmnService.GetBasicInfoSearchListInRegister(keyword, OrganizeId);
            return Content(list.ToJson());
        }
        /// <summary>
        /// 门诊住院预交金患者浮层
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult PatYjjSearchInfo(string keyword, string orgId, string type)
        {
            var OrganizeId = orgId;
            if (string.IsNullOrWhiteSpace(orgId))
            {
                OrganizeId = this.OrganizeId;
            }
            var list = _refundDmnService.GetZyMzYjjPatientSearch(keyword, OrganizeId,type);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 判断是否重复挂号
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public IActionResult AllowRegh(string blh)
        {
            var result = _outpatientRegistRepo.AllowRegh(blh, OrganizeId);
            return Content(result.ToJson());
        }
        #endregion
        public IActionResult Getjzjsxx(string type,string keyword, string grbh)
        {
            var list = _refundDmnService.Getjzjsxx(type,keyword,grbh,this.OrganizeId);
            return Content(list.ToJson());

        }
        /// <summary>
        /// 根据病历号 或 卡号 查询病人基本信息
        /// </summary>
        /// <param name="blh">病历号</param>
        /// <param name="kh">卡号</param>
        /// <param name="zjh"></param>
        /// <param name="cardType">卡类型</param>
        /// <returns></returns>
        public IActionResult GetOutpatientBasicInfo(string blh, string kh, string zjh, string cardType, string CardId, string ly=null)
        {
            var patChargeInfo = _outPatChargeApp.GetOutPatBasicInfoInRegister(blh, kh, zjh, cardType, ly,CardId);
            return Success("Gửi thành công", patChargeInfo);
        }

        public IActionResult valiPatZjh(string zjh)
        {
            //var patChargeInfo = _outPatChargeApp.GetOutPatBasicInfoInRegister(blh, kh, zjh, cardType, ly, CardId);
            //return Success("提交成功", patChargeInfo);
            return null;
        }

        #region Quý An Dữ liệu BHYT

        public IActionResult GetGuianRyblOutInfoByZyh(string zyh)
        {
            var _vo = _guianRybl21OutInfoRepo.GetInfoByZyh(zyh, OrganizeId);
            var data = _vo.ToJson();
            return Content(data);
        }

        public IActionResult SaveGuianRyblOutInfo(GuianRybl21OutInfoEntity entity)
        {
            entity.OrganizeId = OrganizeId;
            if (_guianRybl21OutInfoRepo.FindEntity(p =>
                    p.OrganizeId == entity.OrganizeId && p.prm_ykc010 == entity.prm_ykc010) != null)
                return Success(_guianRybl21OutInfoRepo.Update(entity) > 0 ? "1" : "0");
            else
                return Success(_guianRybl21OutInfoRepo.Insert(entity) > 0 ? "1" : "0");
        }

        #endregion Quý An Dữ liệu BHYT

        #region hợp tác nông thôn mới Quý An

        public IActionResult XNHParticipantsList()
        {
            return View();
        }

        public IActionResult XNHfamilyrequestParForm()
        {
            return View();
        }

        public IActionResult S03submit(S03RequestDTO request)
        {
            var response = CommonProxy.GetInstance(OrganizeId).S03(request);
            return Success("", response.ToJson());
        }

        /// <summary>
        /// 根据身份证号判断是否初诊
        /// </summary>
        /// <param name="sfzh"></param>
        /// <returns></returns>
        public IActionResult ValidateFirstVisit(string sfzh, string xm)
        {
            var entity = _sysPatientBasicInfoRepo.FindEntity(p => p.zjlx == ((int)EnumZJLX.sfz).ToString()
                                                                  && p.zjh == sfzh
                                                                  && p.xm == xm
                                                                  && p.OrganizeId == OrganizeId
                                                                  && p.zt == "1" );
            return Success("", entity);
        }

        /// <summary>
        /// 根据个人编码获取患者基本信息
        /// </summary>
        /// <param name="xnhgrbm">新农合个人编码</param>
        /// <returns></returns>
        //public IActionResult GetPatientBasicInfo(string xnhgrbm)
        //{
        //    var entity = _sysPatientBasicInfoRepo.FindEntity(p => p.xnhgrbm == xnhgrbm
        //                                                          && p.OrganizeId == OrganizeId
        //                                                          && p.zt == "1");
        //    return Success("", entity);
        //}

        /// <summary>
        /// 获取卡号信息
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="brxz"></param>
        /// <returns></returns>
        public IActionResult GetkhInfoBybrxz(int patid, string brxz)
        {
            var cardtype = "";
            if (string.IsNullOrWhiteSpace(brxz))
            {
                throw new FailedException("Thiếu Tính chất bệnh nhân");
            }
            if (patid == 0)
            {
                throw new FailedException("Thiếu bệnh nhân Mã");
            }
            switch (brxz)
            {
                //医保
                case "1":
                    cardtype = ((int)EnumCardType.YBJYK).ToString();
                    break;
                //新农合
                case "8":
                    cardtype = ((int)EnumCardType.XNHJYK).ToString();
                    break;
                default:
                    cardtype = ((int)EnumCardType.XNK).ToString();
                    break;
            }


            var khentity = _sysCardRepository.FindEntity(p => p.patid == patid && p.OrganizeId == OrganizeId && p.zt == "1" && p.CardType == cardtype);
            return Success("", khentity);
        }
        /// <summary>
        /// 新农合S04 入院办理
        /// </summary>
        /// <param name="VO"></param>
        /// <returns></returns>
        public IActionResult S04submit(SysHosBasicInfoVO VO) {
            var S04par = _PatientBasicInfoDmnService.ComposeS04par(VO, OrganizeId);
            var resp = _PatientBasicInfoDmnService.S04submit(S04par, OrganizeId);
            if (resp.state)
            {

                    GuianXnhS04InfoEntity S06entity = null;
                    S06entity = _iGuianXnhS04InfoRepo.FindEntity(p => p.zyh == VO.zyh && p.OrganizeId == OrganizeId);
                    string keyvalue = null;
                    if (S06entity != null)
                    {
                        keyvalue = S06entity.Id;
                    }
                    else
                    {
                        S06entity = new GuianXnhS04InfoEntity();
                        S06entity.Id = System.Guid.NewGuid().ToString();
                    }

                    S06entity.zyh = VO.zyh;
                    S06entity.OrganizeId = OrganizeId;
                    S06entity.xnhgrbm = VO.xnhgrbm;
                    S06entity.xnhylzh = VO.xnhylzh;
                    S06entity.inpId = resp.data.inpId;
                    S06entity.Create(true);
                    _iGuianXnhS04InfoRepo.SubmitForm(S06entity, keyvalue);
            }
            return Content(resp.ToJson());
        }
        public IActionResult XnhCancelRybl(string zyh)
        {
            #region 拉 lấyS30mục lục đối với照
            //string startDate = "2012-01-28 12:43:27 081";
            //for (int i = 0; i < 100; i++)
            //{
            //    var request = new Newtouch.HIS.Proxy.guian.DTO.S30.S30RequestDTO
            //    {
            //        startDate = startDate
            //    };
            //    var response = CommonProxy.GetInstance("9eb11c59-9b09-4bbf-834e-e324aa30230c").S30(request);
            //    if (response.state)
            //    {
            //        var list = response.data;
            //        BaseGaXnhS30Entity entity = null;
            //        foreach (var item in list)
            //        {
            //            entity = new BaseGaXnhS30Entity();
            //            item.MapperTo(entity);
            //            entity.OrganizeId = this.OrganizeId;
            //            entity.zt = "1";
            //            entity.CreatorCode = "root";
            //            entity.CreateTime = DateTime.Now;
            //            entity.Id = Guid.NewGuid().ToString();
            //            _daseGaXnhS30Repo.Insert(entity);

            //        }
            //        int ss = Convert.ToInt32(entity.datetime.Substring(entity.datetime.Length - 2, 2)) + 1;
            //        string ssss = "";
            //        if (ss.ToString().Length == 1)
            //        {
            //            ssss = "0" + ss.ToString();
            //        }
            //        else
            //        {
            //            ssss = ss.ToString();
            //        }
            //        entity.datetime = entity.datetime.Substring(0, entity.datetime.Length - 2) + ssss;
            //        startDate = entity.datetime;
            //    }
            //    else
            //    {
            //        break;
            //    }

            //}

            //return Content("");
            #endregion

            if (string.IsNullOrWhiteSpace(zyh))
            {
                var data = new { state = "0", message = "không có Thông tin nhập viện，Không thể hủy nhập viện！" };
                return Content(data.ToJson());
            }
            if (_PatientBasicInfoDmnService.CheckHasFee(zyh, this.OrganizeId))
            {
                var data = new { state = "0", message = "có Chưa quyết toán của Nhập viện Chi phí bản ghi，Không thể hủy nhập viện。" };
                return Content(data.ToJson());
            }
            InpatientSettXnhPatInfoVO patinfo = _dischargeSettleDmnService.GetInpatientSettXnhPatInfo(zyh, this.OrganizeId);
            if (patinfo != null && !string.IsNullOrEmpty(patinfo.inpId))
            {
                if (_sysConfigRepo.GetBoolValueByCode("HOSP_INTERFACE_WITH_CPOE",this.OrganizeId) == true)
                {
                    //仅新入院可以取消入院登记
                    if (!(patinfo.zybz == ((int)EnumZYBZ.Xry).ToString()))
                    {
                        if (patinfo.zybz == ((int)EnumZYBZ.Bqz).ToString())
                        {
                            var data = new { state = "0", message = "Vui lòng hủy vào khu bệnh trước, mới có thể hủy nhập viện"};
                            return Content(data.ToJson());
                        }
                        else
                        {
                            var data = new { state = "0", message = "hiện tại là" + NewtouchHIS.Lib.Base.Extension.EnumExtensions.GetDescription((EnumZYBZ)Convert.ToInt32(patinfo.zybz)) + "Trạng thái，Không thể hủy nhập viện" };
                            return Content(data.ToJson());
                        }
                    }
                }
                S05RequestDTO S05ReqDTO = new S05RequestDTO()
                {
                    inpId = patinfo.inpId,
                    areaCode = "",
                    isTransProvincial = "0",
                    cancelCause = ""
                };
                var S05ResDto = _PatientBasicInfoDmnService.S05submit(S05ReqDTO, OrganizeId);
                if (S05ResDto.state)
                {
                    var data = new {state = "1", message = "Hủy nhập viện thành công"};
                    return Content(data.ToJson());
                }
                else
                {
                    var data = new { state = "0", message = "Hủy Nhập viện Thất bại！Hợp tác nông thôn mới giao diện trả về Lỗi：" + S05ResDto.message };
                    return Content(data.ToJson());
                }
            }
            else
            {
                var data = new { state = "0", message = "Hủy Nhập viện Thất bại！Lỗi：Số nhập viện" + zyh + "Lấy Thông tin bệnh nhân Thất bại！" };
                return Content(data.ToJson());
            }

        }

        /// <summary>
        /// 新农合S06 入院办理信息修改
        /// </summary>
        /// <param name="VO"></param>
        /// <returns></returns>
        public IActionResult S06submit(SysHosBasicInfoVO VO) {
            var S06par = _PatientBasicInfoDmnService.ComposeS06par(VO, OrganizeId);
            var S06Res= _PatientBasicInfoDmnService.S06submit(S06par, OrganizeId);
            if (!S06Res.state)
            {
                return Error("Hợp tác nông thôn mới giao diện trả về Thất bại：" + S06Res.message);
            }
            GuianXnhS04InfoEntity S06entity = null;
            S06entity = _iGuianXnhS04InfoRepo.FindEntity(p => p.zyh == VO.zyh && p.OrganizeId == OrganizeId);
            string keyvalue = null;
            if (S06entity != null)
            {
                keyvalue = S06entity.Id;
            }
            else {
                S06entity = new GuianXnhS04InfoEntity();
                S06entity.Id = System.Guid.NewGuid().ToString();
            }

            S06entity.zyh = VO.zyh;
            S06entity.OrganizeId = OrganizeId;
            S06entity.xnhgrbm = VO.xnhgrbm;
            S06entity.xnhylzh = VO.xnhylzh;
            S06entity.inpId = VO.inpId;
            S06entity.Create(true);
            _iGuianXnhS04InfoRepo.SubmitForm(S06entity, keyvalue);
            return Success();
        }

        /// <summary>
        /// 验证新农合接口对照
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IActionResult validateTTentity(string code) {
           var data= _PatientBasicInfoDmnService.validateTTentity(OrganizeId,code);
            return Content(data.ToJson());
        }
		#endregion

		#region BHYT Trùng Khánh
	    public IActionResult GetCQjzdjInfo(string zyh)
	    {
            var model = _PatientBasicInfoDmnService.GetCQjzdjInfo(zyh, this.OrganizeId);
            model.operatorId = this.UserIdentity.rygh;
            model.operatorName = this.UserIdentity.UserName;
            model.orgId = this.OrganizeId;
            return Content(model.ToJson());
	    }
        public IActionResult UpdateCqybOut02(string zyh)
        {
            
            _PatientBasicInfoDmnService.UpdateCqybOut02(zyh,this.OrganizeId);
            return Success();
        }
        #endregion

        #region nghiệp vụ BHYT
        public IActionResult ValidateYbFirstVisit(string sfzh, string xm,string kh=null,string jzpzlx=null)
        {

            var entity = _PatientBasicInfoDmnService.ValidateFirstVisit(sfzh,xm,this.OrganizeId,kh,jzpzlx);
            return Success("", entity);
        }
        #endregion

        #region Tính chất bệnh nhân
        /// <summary>
        /// 根据险种类型获取病人性质
        /// </summary>
        /// <param name="xzlx"></param>
        /// <returns></returns>
        public IActionResult GetBrxzbyxzlx(string xzlx)
        {
            List<SysPatientNatureEntity> brxzlist = new List<SysPatientNatureEntity>();
            if (string.IsNullOrWhiteSpace(xzlx))
                brxzlist = _SysPatiNatureRepo.IQueryable(p => p.zt == "1" && p.OrganizeId == OrganizeId).ToList();
            else
                brxzlist = _SysPatiNatureRepo.IQueryable(p => p.insutype == xzlx && p.zt == "1" && p.OrganizeId == OrganizeId).ToList();
            return Content(brxzlist.ToJson());
        }
        public IActionResult Getzdinfo()
        {
            var data = _PatientBasicInfoDmnService.GetksZzdList(OrganizeId);
            return Content(data.ToJson());
        }

        #endregion
    }
}
