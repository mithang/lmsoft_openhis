using System;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Common.Web;

using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PatientListController : OrgControllerBase
    {
        private readonly ITreatmentRepo _treatmentRepo;
        private readonly IPatientVitalSignsRepo _patientVitalSignsRepo;
        private readonly IInpatientPatientDmnService _inpatientPatientDmnService;
        private readonly IBaseDataDmnService _iBaseDataDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IIDBDmnService _idbDmnService;
        private readonly IVisitDeptSetRepo visitDeptSetRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
		private readonly IMedicalRecordDmnService _medicalRecordDmnService;
        private readonly ITreatmentDmnService _treatmentDmnService;
        private readonly IInpatientPatientDiagnosisRepo _inpatientPatientDiagnosisRepo;

        // GET: PatientList
        public PatientListController(ITreatmentRepo treatmentRepo, IPatientVitalSignsRepo patientVitalSignsRepo, IInpatientPatientDmnService inpatientPatientDmnService, IBaseDataDmnService iBaseDataDmnService, ISysConfigRepo sysConfigRepo, IIDBDmnService idbDmnService, ISysUserDmnService sysUserDmnService, IMedicalRecordDmnService medicalRecordDmnService, ITreatmentDmnService treatmentDmnService, IInpatientPatientDiagnosisRepo inpatientPatientDiagnosisRepo)
        {
            this._treatmentRepo = treatmentRepo;
            this._patientVitalSignsRepo = patientVitalSignsRepo;
            this._inpatientPatientDmnService = inpatientPatientDmnService;
            this._iBaseDataDmnService = iBaseDataDmnService;
            this._sysConfigRepo = sysConfigRepo;
            this._idbDmnService = idbDmnService;
            this._sysUserDmnService = sysUserDmnService;
            this._medicalRecordDmnService = medicalRecordDmnService;
            this._treatmentDmnService = treatmentDmnService;
            this._inpatientPatientDiagnosisRepo = inpatientPatientDiagnosisRepo;

        }
        // QA-phase2 CIS-01: khong co Views/PatientList/Index.cshtml nen
        // Index() ke thua tu base tra JSON "view not found". Redirect ve
        // action danh sach that dang chay (PatientList) thay vi bia view moi.
        [HttpGet]
        public override IActionResult Index()
        {
            return RedirectToAction("PatientList");
        }

        /// <summary>
        /// Danh sách bệnh nhân
        /// </summary>
        /// <returns></returns>
        public IActionResult PatientList()
        {
            return View();
        }
        /// <summary>
        /// Bệnh nhândựkhám
        /// </summary>
        /// <returns></returns>
        public IActionResult PatientPreDiag()
        {
            return View();
        }
        /// <summary>
        /// Bệnh nhânKhám bệnhthông tin
        /// </summary>
        /// <returns></returns>
        public IActionResult PatientVisitList()
		{
            return View();
		}

    #region Khám bệnhThông tin

    public IActionResult SelectPatientVisitList(Pagination pagination, string kh, string xm, string jzlx,string jszt, DateTime? kssj, DateTime? jssj)
        {
            var data = new
            {
                rows = _treatmentDmnService.GetPatientVisitList(pagination, kh, xm, jzlx, jszt, kssj, jssj, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        public IActionResult GetInPatientInfo(string zyh)
        {
            var data = _treatmentDmnService.GetInPatientInfo(zyh,OrganizeId);
            var ryDiaList = _inpatientPatientDiagnosisRepo.SelectDiagData(zyh, OrganizeId, "1");
            var cyDiaList = _inpatientPatientDiagnosisRepo.SelectDiagData(zyh, OrganizeId, "2");
            data.ryzdList = ryDiaList;
            data.cyzdList = cyDiaList;

            return Content(data.ToJson());
        }
        #endregion

        /// <summary>
        /// TheoSố khám ngoại trúTra cứudấu hiệu sinh tồn
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetVitalSignsByMzh(string mzh)
        {
            var data = _patientVitalSignsRepo.IQueryable().Where(a => a.mzh == mzh && a.zt == "1" && a.OrganizeId == this.OrganizeId).FirstOrDefault();

            return Content(data.ToJson());
        }

        public IActionResult GetTreatEntityByMzh(string mzh)
        {
            var data = _treatmentDmnService.SelectTreatmentEntitie(mzh, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// chưaKhám bệnhdanh sách
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectNotYetTreateList(Pagination pagination, string keyword, int mjzbz,bool? zzhz)
        {

            #region LấyKhoa khám

            var kcCode = UserIdentity.DepartmentCode;
            var visitDept = visitDeptSetRepo.SelectData(OperatorProvider.GetCurrent().rygh, OrganizeId);
            if (visitDept != null && visitDept.Count > 0)
            {
                var tkcCode = new StringBuilder();
                foreach (var deptSetEntity in visitDept)
                {
                    if (!string.IsNullOrWhiteSpace(deptSetEntity.visitksCode))
                    {
                        tkcCode.Append("," + deptSetEntity.visitksCode);
                    }
                }

                if (tkcCode.Length > 0)
                {
                    kcCode = tkcCode.Append(",").ToString();
                    if (kcCode.IndexOf("," + UserIdentity.DepartmentCode + ',', StringComparison.Ordinal) < 0) kcCode += UserIdentity.DepartmentCode;
                    kcCode = kcCode.Trim(',');
                }
            }
            #endregion
            var syncMethod = _sysConfigRepo.GetValueByCode("HISSyncMethod", this.OrganizeId);
            if (syncMethod == "IDB")
            {
                //từtronggiữakhoĐồng bộ
                pagination.sidx = "ghczsj desc";
                pagination.sord = "";
                var list = new
                {
                    rows = _idbDmnService.GetRegistrationInfoList(pagination, this.OrganizeId, kcCode
                       , mjzbz == 3 ? this.UserIdentity.rygh : ""
                       , mjzbz.ToString()
                       , (int)EnumJzzt.NotYetTreate
                       , keyword),
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records
                };
                return Content(list.ToJson());
            }
            pagination.sidx = "operatingTime";
            pagination.sord = "";
            var reqObj = new
            {
                zzhz = zzhz == true ? "1" : "0",
                ksCode = kcCode,
                ysgh = mjzbz == 3 ? this.UserIdentity.rygh : "",    //Lọcchuyên giasố chỉ xemtreotựtựsốcủa
                mjzbz = mjzbz.ToString(),  //1thông thườngNgoại trú 2gấpkhám 3chuyên giaNgoại trú
                jiuzhenbz = ((int)EnumJzzt.NotYetTreate).ToString(),   //Khám bệnhCờ
                keyword = keyword,
                pagination = pagination
            };

            //kéolấyđăng ký khámthông tin
            var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RegisteredInfoResponse>>(
                "/api/Patient/OutPatientRegistrationQuery", reqObj);
            if (apiResp.data == null) return Error("Gọi giao diệnthất bại【" + apiResp.msg + apiResp.sub_msg + "】");

            var showUnTreateDays = _sysConfigRepo.GetValueByCode("ShowUnTreateDays", this.OrganizeId);
            var unTreateDays = 3;
            if (!string.IsNullOrEmpty(showUnTreateDays))
            {
                unTreateDays = Convert.ToInt32(showUnTreateDays);
            }

            var apiRespList = new List<TreatEntityObj>();
            foreach (var item in apiResp.data.list)
            {
                var ds = item.ghsj.Date;
                if (item.ghsj.Date.AddDays(unTreateDays) < DateTime.Now)
                {
                    continue;
                }
                apiRespList.Add(new TreatEntityObj
                {
                    mzh = item.mzh,
                    mjzbz = Convert.ToInt32(item.mjzbz),//1thông thườngNgoại trú 2gấpkhám 3chuyên giaNgoại trú
                    blh = item.blh,
                    xm = item.brxm,
                    xb = item.sexValue,
                    csny = item.birth,
                    brxzmc = item.brxzmc,
                    brxzCode = item.brxz,
                    zjlx = item.zjlx,
                    zjh = item.zjh,
                    ghksmc = item.ksmc,
                    ghys = item.ysxm,//đăng ký khámBác sĩTên（chuyên giasốkhichắcđịnhcó）
                    ghsj = item.ghsj,
                    ghczsj = item.operatingTime,
                    jzzt = (int)EnumJzzt.NotYetTreate,
                    jzks = item.ks,
                    jzys = item.ys,
                    ybjsh = item.ybjsh,
                    cfzbz = item.fzbz,
                    sbbh = item.sbbh,
                    cbdbm = item.cbdbm,
                    py = item.py,
                    kh = item.kh,
                    ContactNum = item.ContactNum,
                    province = item.province,
                    city = item.city,
                    county = item.county,
                    address = item.address,
                    nlshow = item.nlshow,
                    ghlybz = item.ghlybz,
                    grbh=item.grbh,
                    zzbs = zzhz == true ? "转诊" : "",
                    queno=item.queno
                });
            }

            var tlist = new
            {
                rows = apiRespList,
                total = Math.Ceiling(Convert.ToDecimal(apiRespList.Count / pagination.rows)),
                page = apiResp.data.pagination.page,
                records = apiRespList.Count
            };
            return Content(tlist.ToJson());


        }

        /// <summary>
        /// khingàyĐã khám bệnhdanh sách
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectTreatingOrTreatedList(Pagination pagination, int jzzt, string keyword, int mjzbz, DateTime? kzrq, bool? zzhz)
        {
            pagination.sidx = "ghsj desc";
            pagination.sord = "";
            if (jzzt == (int)EnumJzzt.Treated)
            {
                pagination.sidx = "zljssj desc ";
            }
            else if (jzzt == (int)EnumJzzt.Treating)
            {
                pagination.sidx = "zlkssj desc,LastModifyTime desc";
            }

            var data = _treatmentRepo.GetTreatingOrTreatedList(pagination, this.OrganizeId, Convert.ToInt32(jzzt), mjzbz, this.UserIdentity.rygh, kzrq, keyword,zzhz);
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
        /// khôi phụclạiKhám bệnh
        /// </summary>
        /// <param name="jzid"></param>
        /// <returns></returns>
        public IActionResult ResumeTreat(string jzId)
        {
            _treatmentRepo.ResumeTreat(this.OrganizeId, jzId);
            return Success();
        }
        /// <summary>
        /// Lấycuối cùngmột lầnKhám bệnh
        /// </summary>
        /// <param name="ks"></param>
        /// <param name="blh"></param>
        /// <returns></returns>
        public IActionResult GetLastTreate(string blh, int mjzbz)
        {
            var orgId = this.OrganizeId;
            var ks = this.UserIdentity.DepartmentCode;
            var zjhysgh = this.UserIdentity.rygh;
            var data = _treatmentRepo.IQueryable(p => p.OrganizeId == orgId && p.jzks == ks && p.blh == blh && p.mjzbz == mjzbz
                && (p.mjzbz != 3 || p.jzys == zjhysgh) && p.zt == "1").ToList()
                .OrderByDescending(p => p.zljssj == null)
                .ThenByDescending(p => p.zljssj)
                .ThenByDescending(p => p.zlkssj).FirstOrDefault();
            return Content(data == null ? "{}" : data.ToJson());
        }

        /// <summary>
        /// TheoSố bệnh ánĐồng bộBệnh nhâncủaĐơn thuốcThu phíTrạng thái
        /// </summary>
        /// <param name="blh"></param>
        /// <returns></returns>
        public IActionResult SyncPresChargeStatus(string blh)
        {
            var syncMethod = _sysConfigRepo.GetValueByCode("HISSyncMethod", this.OrganizeId);
            if (syncMethod == "IDB")
            {
                _idbDmnService.Refreshcfsfbz(blh, this.OrganizeId);

            }
            return null;
        }

        #region Nhập việnBệnh nhânmộtxem
        /// <summary>
        /// LấyĐang ở khu bệnhcủabệnh người
        /// </summary>
        /// <param name="bqCode"></param>
        /// <param name="ys"></param>
        /// <param name="bedCode"></param>
        /// <returns></returns>
        public IActionResult GetzbqPatientList(int rows,string page, string bqCode, string ys, string bedCode)
        {
            Pagination pagination = new Pagination();
            pagination.sidx = " cwmc";
            pagination.sord = " asc";
            pagination.rows = rows;
            pagination.page = page.ToInt();
            var data = new
            {
                rows = _inpatientPatientDmnService.GetzbqPatientList(new PatientzbqRequestDto { pagination = pagination, bqcode = bqCode, cw = bedCode, ysgh = ys }, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấyđãra khỏi khubệnh người
        /// </summary>
        /// <param name="bqCode"></param>
        /// <param name="ys"></param>
        /// <param name="bedCode"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetycqPatientList(Pagination pagination, string bqCode, string ys, string bedCode, string ksrq, string jsrq, string zyh)
        {
            var data = new
            {
                rows = _inpatientPatientDmnService.GetycqPatientList(new PatientycqRequestDto { pagination = pagination, bqcode = bqCode, cw = bedCode, ysgh = ys, cqksrq = ksrq.AsDateTime(), cqjsrq = jsrq.AsDateTime(), zyh = zyh }, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public IActionResult patientFloatingSelectorSource(string keyword)
        {
            var data = _inpatientPatientDmnService.GetInfoBykeyword(keyword, OrganizeId);
           return Content(data.ToJson());
        }

        public IActionResult inpatientBedUsedInfo(string zyh)
        {
            var data = new
            {
                rows = _inpatientPatientDmnService.GetInpatientBasicInfo(zyh, this.OrganizeId, "2"),
                total = 1,
                page = 1,
                records = 1,
            };
            return Content(data.ToJson());
        }
        #endregion

        #region Dấu hiệu sinh tồn
        /// <summary>
        /// TheoSố khám ngoại trúTra cứudấu hiệu sinh tồn
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string mzh)
        {
            var entity = _patientVitalSignsRepo.IQueryable().Where(a => a.mzh == mzh && a.zt == "1" && a.OrganizeId == this.OrganizeId).FirstOrDefault();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(PatientVitalSignsEntity entity, string keyValue)
        {
            entity.zt = "1";
            entity.OrganizeId = this.OrganizeId;
            _patientVitalSignsRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }
        #endregion

        #region Nhập việnBệnh nhân

        /// <summary>
        /// Bệnh nhân nội trúmộtxem
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientList()
        {
            //ViewBag.bqval = "==Vui lòng chọn==";
            var bqAuth = _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault();
            if (bqAuth != null)
            {
                ViewBag.bqval = bqAuth.bqCode;
                ViewBag.ysgh = UserIdentity.rygh;
                return View();
            }
            else
            {
                return Content("javascript:void()");
            }


        }

        /// <summary>
        /// Nhập việnThông tin bệnh nhân
        /// </summary>
        /// <returns></returns>
        public IActionResult InpatientInfo()
        {
            return View();
        }
        #endregion

        #region Nhập việnBệnh nhânChọnbậtlớp

        /// <summary>
        /// Bệnh nhân nội trúchọnbậtlớp
        /// </summary>
        /// <returns></returns>
        public IActionResult InPatSearchView()
        {
            return View();
        }

        /// <summary>
        /// Bệnh nhân nội trúchọnbậtlớp Nguồn dữ liệu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="bq"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="zybz">ởviệnCờ，nhiềucáidùng逗sốphútcắt</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult InPatSearchInfo(Pagination pagination, string bq, string zyh, string xm,string ch, string zybz = null)
        {
			var StaffId = this.UserIdentity.StaffId;
			//TheoStaffIdLấyKhu bệnh
			var bqCodeList = _inpatientPatientDmnService.GetWardCodeByStaffId(StaffId,this.OrganizeId);
			var bqCode = "";
			foreach (var obj in bqCodeList) {
				bqCode += obj + ",";
			}
			bqCode = bqCode.Substring(0, bqCode.Length - 1);
			var data = new
            {
                rows = _inpatientPatientDmnService.GetInPatSearchPaginationList(pagination, this.OrganizeId, bq, zyh, xm, ch, bqCode,zybz),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        #endregion

        #region Nhập việnBệnh nhânThông tin

        public IActionResult InpatientInfoShow()
        {
            return View();
        }
        #endregion

        #region Quyết toánTra cứu
        public IActionResult PatZySettInfobyJsnm(string zyh)
        {
            var data = _inpatientPatientDmnService.PreSettbyId(zyh,this.OrganizeId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// nợ phímởlập/Thực hiệnnhắcnhắc
        /// </summary>
        /// <param name="patlist"></param>
        /// <returns></returns>
        public IActionResult PatYjjWarn(string patlist)
        {
            var data = _inpatientPatientDmnService.PatYjjWarn(patlist,this.OrganizeId);
            return Content(data.ToJson()); ;
        }
        #endregion

        /// <summary>
        /// Đăng nhậpNgười dùngCóKhôngCóchuyên giaBác sĩ
        /// </summary>
        /// <returns></returns>
        public IActionResult IsExpertByLoginUser() {
            var flag = false;
            var ysgh = this.UserIdentity.UserCode;
            var sysStaffDutyList = _sysUserDmnService.GetStaffDutyListByOrganizeId(this.OrganizeId);
            foreach(var obj in sysStaffDutyList) {
                if (obj.StaffGh == ysgh) {
                    if (obj.DutyCode== "zrys"||obj.DutyCode== "fzrys")
                    {//chínhnhiệmBác sĩ phụchínhnhiệmBác sĩ
                        flag = true;
                        break;
                    }
                }
            }
            return Content(flag.ToJson());
        }


		public IActionResult GetdjzList(Pagination pagination, string keyword, int mjzbz)
		{
			pagination.sidx = " SBXH ";
			var data = _treatmentRepo.GetdjzList(pagination, keyword, mjzbz);
			var apiRespList = new List<TreatEntityObj>();

			pagination.sidx = "operatingTime";
			
			foreach (var item in data)
			{

				string tmpBirthDay = item.CERTNO.Substring(6, 4) + "-" + item.CERTNO.Substring(10, 2) + "-" + item.CERTNO.Substring(12, 2);

				jzdjbrxx brxx = new jzdjbrxx();
				brxx.xm = item.PSN_NAME;
				brxx.zjh = item.CERTNO;
				brxx.xb = item.BRXB;
				brxx.csny = tmpBirthDay;
				brxx.nlshowdw = item.AGE;
				brxx.phone = "";
				brxx.gj = "156";
				brxx.mz = "01";
				brxx.zy = "90";
				brxx.hy = "";
				brxx.brxz = "1";
				brxx.py = "";
				brxx.mzlx = mjzbz.ToString();
				brxx.kscode = this.UserIdentity.DepartmentCode;
				brxx.ksname = this.UserIdentity.DepartmentName;
				brxx.rygh = this.UserIdentity.rygh;
				brxx.ryname = this.UserIdentity.UserName;
				brxx.orgid = this.OrganizeId;
				brxx.grbh = item.PSN_NO;
				brxx.cbdbm = "";
				brxx.ecToken = item.SBXH;
				brxx.jzid = item.Mdtrt_ID;
				brxx.sbbh = item.MZHM;
				brxx.xzlx = item.INSUTYPE;
				brxx.med_type = item.MED_TYPE;
				var reflist= _inpatientPatientDmnService.inserghjz(brxx);
				
			
			
			}
			var reqObj = new
			{
				ksCode = this.UserIdentity.DepartmentCode,
				ysgh = this.UserIdentity.rygh,
				mjzbz = "",
				jiuzhenbz = ((int)EnumJzzt.NotYetTreate).ToString(),   //Khám bệnhCờ
				pagination = pagination
			};

			var datagh = _medicalRecordDmnService.Getdjzlist(pagination, OrganizeId, reqObj.ksCode, reqObj.ysgh, reqObj.mjzbz, reqObj.jiuzhenbz);

			foreach (var item in datagh)
			{
				var ghrq = Convert.ToDateTime(item.ghsj);
				var newghrq = ghrq.AddDays(1);
				if (ghrq.AddDays(1) < DateTime.Now)
				{
					continue;
				}
				apiRespList.Add(new TreatEntityObj
				{
					mzh = item.mzh,
					mjzbz = Convert.ToInt32(item.mjzbz),//1thông thườngNgoại trú 2gấpkhám 3chuyên giaNgoại trú
					blh = item.blh,
					xm = item.brxm,
					xb = item.sexValue,
					csny = item.birth.ToDate(),
					brxzmc = item.brxzmc,
					brxzCode = item.brxz,
					zjlx = item.zjlx.ToInt(),
					zjh = item.zjh,
					ghksmc = item.ksmc,
					ghys = item.ysxm,//đăng ký khámBác sĩTên（chuyên giasốkhichắcđịnhcó）
					ghsj = item.ghsj.ToDate(),
					ghczsj = item.operatingTime.ToDate(),
					jzzt = (int)EnumJzzt.NotYetTreate,
					jzks = item.ks,
					jzys = item.ys,
					ybjsh = item.ybjsh,
					cfzbz = item.fzbz,
					sbbh = item.sbbh,
					cbdbm = item.cbdbm,
					py = item.py,
					kh = item.kh,
					ContactNum = item.contactNum,
					province = item.province,
					city = item.city,
					county = item.county,
					address = item.address,
					nlshow = item.nlshow,
					ghlybz = item.ghlybz,
					grbh = item.grbh
				});
			}


			
			var tlist = new
			{
				rows = apiRespList,
				total = Math.Ceiling(Convert.ToDecimal(apiRespList.Count / pagination.rows)),
				page = 1,
				records = apiRespList.Count
			};
			return Content(tlist.ToJson());
		}
    }
}