using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;


























namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// đăng ký khám ngoại trú
    /// </summary>
    [Area("OutpatientManage")]

    public class OutpatientRegController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly IOutpatientRegApp _outpatientRegApp;
        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;
        private readonly IRefundDmnService _refundDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IprintOutpatientChargeBillDmnService _iprintOutpatientChargeBillDmnService;
	    private readonly ICqybMedicalReg02Repo _iCqybMedicalReg02Repo;
	    private readonly ICqybUploadInPres04Repo _iCqybUploadInPres04Repo;
		private readonly IOutPatChargeDmnService _iOutPatChargeDmnService;
        private readonly IOutPatientDmnService _iOutPatientDmnService;
        private readonly IMzghbookRepo _iMzghBookRepo;
        private readonly ICqybMedicalInPut02Repo _cqybmedicalInput02Repo;

        public OutpatientRegController(IOutpatientRegistRepo outpatientRegistRepo, IOutpatientRegApp outpatientRegApp, IOutPatientSettleDmnService outPatientSettleDmnService, IRefundDmnService refundDmnService, ISysConfigRepo sysConfigRepo, IprintOutpatientChargeBillDmnService iprintOutpatientChargeBillDmnService, ICqybMedicalReg02Repo iCqybMedicalReg02Repo, ICqybUploadInPres04Repo iCqybUploadInPres04Repo, IOutPatChargeDmnService iOutPatChargeDmnService, IOutPatientDmnService iOutPatientDmnService, IMzghbookRepo iMzghBookRepo, ICqybMedicalInPut02Repo cqybmedicalInput02Repo)
        {
            _outpatientRegistRepo = outpatientRegistRepo;
            _outpatientRegApp = outpatientRegApp;
            _outPatientSettleDmnService = outPatientSettleDmnService;
            _refundDmnService = refundDmnService;
            _sysConfigRepo = sysConfigRepo;
            _iprintOutpatientChargeBillDmnService = iprintOutpatientChargeBillDmnService;
            _iCqybMedicalReg02Repo = iCqybMedicalReg02Repo;
            _iCqybUploadInPres04Repo = iCqybUploadInPres04Repo;
            _iOutPatChargeDmnService = iOutPatChargeDmnService;
            _iOutPatientDmnService = iOutPatientDmnService;
            _iMzghBookRepo = iMzghBookRepo;
            _cqybmedicalInput02Repo = cqybmedicalInput02Repo;
        }

		#region Lịch trực đăng ký khám
		/// <summary>
		/// đăng ký khámlịch trựcList
		/// </summary>
		/// <param name="keyword">khoa/bác sĩ</param>
		/// <param name="mzlx">ngoại trúloại khám thườngngoại trúkhám/cấp cứuchứng/chuyên giangoại trúkhám</param>
		/// <returns></returns>
		[HttpPost]
                public IActionResult GetRegScheduleList(string keyword, string mzlx)
        {
            var data = _outpatientRegApp.GetRegScheduleList(keyword, mzlx);
            return Json(data);
        }

        /// <summary>
        /// lấymớicủalịch trực
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mzlx"></param>
        /// <returns></returns>
        public IActionResult GetNewRegScheduleList(string keyword, string mzlx)
        {
            var data = _iOutPatientDmnService.GetNewRegScheduleList(keyword,mzlx,this.OrganizeId);
            return Json(data);
        }
        
        public IActionResult GetNewRegSchedulebyGroupList(string keyword, string pbks, string mzlx,DateTime? OutDate,string ys)
        {
            var data = _iOutPatientDmnService.GetRegScheduleListbyGroup(keyword,mzlx,pbks,this.OrganizeId, OutDate, ys);
            return Content(data.ToJson());
        }
        /// <summary>
        /// bệnh nhân làkhôngđãđăng ký khámhẹn
        /// </summary>
        /// <param name="mzlx"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
                public IActionResult GetIsMzghBook(Pagination pagination,string mzlx,string patid,string isfeegroup, string yyzt, string ghly, string keyValue,string ks,
            DateTime? yykssj = null, DateTime? yyjssj = null)
        {
            if (pagination.sidx==null)
            {
                pagination.sidx = "Outdate";
                pagination.sord = "desc";
                pagination.rows = 20;
                pagination.page = 1;
                var record= _iOutPatientDmnService.GetIsMzghBookSchedule(pagination, mzlx, patid, this.OrganizeId, isfeegroup, yyzt, ghly, keyValue,ks, yykssj, yyjssj);
                return Content(record.ToJson());
            }
            var list = _iOutPatientDmnService.GetIsMzghBookSchedule(pagination,mzlx,patid,this.OrganizeId,isfeegroup,yyzt,ghly,keyValue,ks,yykssj,yyjssj);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// tra cứu khingàyđãđăng ký khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        public IActionResult GetCurrentDayRegListJson(string orgId, string creatorCode)
        {
            var data = _outPatientSettleDmnService.GetCurrentDayRegList(this.OrganizeId, this.UserIdentity.UserCode);
            return Content(data.ToJson());
        }

        public IActionResult OutpatientAppointment(string from = "")
        {
            ViewBag.from = from;
            return View();
        }
        /// <summary>
        /// sửahẹn đăng ký khámhẹntrạng thái
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="mzh"></param>
        /// <param name="patid"></param>
        /// <param name="ks"></param>
        /// <param name="zje"></param>
        /// <returns></returns>
        public IActionResult UpdateMzGhBook(string jsnm,string mzh,string patid,string ks,string zje,string outdate,string mzlx,string ghf)
        {
            _iMzghBookRepo.UpdateMzGhAppointment(jsnm,  mzh, Convert.ToInt32(patid),  ks,  zje,  "His",this.OrganizeId,outdate,mzlx,ghf);
            return Success();
        }

		public IActionResult FZsjTB()
		{
			string ghrq = DateTime.Now.ToString("yyyy-MM-dd");
			_iMzghBookRepo.FZsjTB(ghrq,this.UserIdentity.rygh,this.OrganizeId);
			return Success();
		}
        /// <summary>
        /// hẹn đăng ký khámđiểm danh
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult SigninAppointment(string mzh)
        {
            _iMzghBookRepo.SigninAppointment(mzh, this.OrganizeId);
            return Success();
        }
        #endregion

        #region Số hóa đơn
        /// <summary>
        /// chọn hóa đơn
        /// </summary>
        /// <returns></returns>
        public IActionResult ChooseInvoice(string from = "")
        {
            ViewBag.from = from;
            return View();
        }
        /// <summary>
        /// theo mã nhân viênlấy số hóa đơn
        /// </summary>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
                public IActionResult GetInvoice()
        {
            var data = _outpatientRegApp.GetInvoice();
            return Content(data);
        }

        /// <summary>
        /// xác thực nhậpsố hóa đơnlàkhôngcó thể dùng
        /// by caishanshan
        /// </summary>
        /// <param name="inputFPH"></param>
        /// <returns></returns>
                public IActionResult CheckInvoice(string inputFPH)
        {
            _outpatientRegApp.CheckInvoice(inputFPH);
            return Success();
        }
        #endregion

        /// <summary>
        /// lấyđăng ký khámphí phí chẩn trị phí thẻ từ phí làm thẻ
        /// </summary>
        /// <param name="ghlx">đăng ký khámloại</param>
        /// <param name="zlxm">chẩn trịkhoản mục</param>
        /// <param name="isZzjm">giảm miễn chuyển tuyến</param>
        /// <param name="isCkf">phí thẻ từ</param>
        /// <param name="isGbf">phí làm thẻ</param>
        /// <returns></returns>
                public IActionResult GetOutpatientFees(string ghlx, string zlxm, bool isCkf, bool isGbf)
        {
            var fees = _outPatientSettleDmnService.GetOutpatientFees(ghlx, zlxm, isCkf, isGbf);
            return Content(fees.ToJson());
        }
                public IActionResult GetOutpatientFeesbyGroup(string ghlx, string zlxm, bool isCkf, bool isGbf)
        {
            var fees = _outPatientSettleDmnService.GetOutpatientFeesbyGroup(ghlx, zlxm, isCkf, isGbf);
            return Content(fees.ToJson());
        }

        #region Đăng ký khám ngoại trú2018

        /// <summary>
        /// đăng ký khám ngoại trútrang
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientReg2018()
        {
            ViewBag.ReportServerHOST = "";
            ViewBag.OrgId = this.OrganizeId;
            //công tắc:hẹn đăng ký khám
            var brSwitch = (bool)_sysConfigRepo.GetBoolValueByCode("BespeakRegisterSwitch", OrganizeId, false);
            ViewBag.ISOpenBespeakRegister = brSwitch ? "true" : "false";
            ViewBag.Xzqh = _outPatientSettleDmnService.GetOrgXzqh(this.OrganizeId);
            return View();
        }

        /// <summary>
        /// xác định làkhôngtrùng lặpđăng ký khám
        /// </summary>
        /// <param name="blh"></param>
        /// <returns></returns>
        public IActionResult AllowRegh(string blh)
        {
            var result = _outpatientRegistRepo.AllowRegh(blh, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// sửasố điện thoại
        /// </summary>
        /// <param name="blh"></param>
        /// <returns></returns>
        public IActionResult UpdatePatPhone(string patid,string phone)
        {
            var result = _outpatientRegistRepo.UpdatePatPhone(patid, phone,UserIdentity.rygh, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// lớp nổi tra cứu bệnh nhân
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult PatSearchInfo(string keyword)
        {
            var data = _refundDmnService.GetBasicInfoSearchListInRegister(keyword, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// tạo duy nhấtsố khám ngoại trú,số thứ tự khám
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghlx"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <returns></returns>
        public IActionResult GetMzhJzxh(int patid, string ghpbId, string ks, string ys)
        {
            if (patid <= 0 || string.IsNullOrWhiteSpace(ghpbId) || string.IsNullOrWhiteSpace(ks))
            {
                return Error("Dữ liệu yêu cầu chưa đầy đủ");
            }
            var mzhjzxh = _outPatientSettleDmnService.GetMzhJzxh(patid, ghpbId.ToString(), ks, ys, OrganizeId, this.UserIdentity.UserCode);
            var data = new
            {
                jzxh = mzhjzxh.Item1,
                mzh = mzhjzxh.Item2,
            };
            return Success(null, data);
        }
        /// <summary>
        /// chongqing lịch trực mới duy nhấtsố khám ngoại trú,số thứ tự khám
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghpbId"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="QueueNo">số thứ tự khám theo hẹn</param>
        /// <returns></returns>
        public IActionResult GetNewMzhJzxh(int patid,string ghpbId,string ks,string ys,string mjzbz,string QueueNo,string OutDate,bool isybjy)
        {
            var ysxx = new CqybGjbmInfoVo();
            if (patid <= 0 || string.IsNullOrWhiteSpace(ghpbId) || string.IsNullOrWhiteSpace(ks))
            {
                return Error("Dữ liệu yêu cầu chưa đầy đủ");
            }
            var mzhjzxh = _outPatientSettleDmnService.GetCQMzhJzxh(patid, ghpbId.ToString(), ks, ys, OrganizeId, this.UserIdentity.UserCode, mjzbz, QueueNo, OutDate);
            if (isybjy)
            {
                ysxx = _outPatientSettleDmnService.GetDepartmentDoctorIdC(OrganizeId, ks, null);
            }
            var data = new
            {
                jzxh = mzhjzxh.Item1,
                mzh = mzhjzxh.Item2,
                ysxx= ysxx,
            };
            return Success(null,data);
        }
        /// <summary>
        /// lấyBHYTthông tin cần thiết đăng ký khám
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetYbjzdjVo(string mzh)
        {
            var data = new
            {
                ysxx = _outPatientSettleDmnService.GetYbjzdjVo(mzh,this.OrganizeId)
            };
            return Success(null, data);
        }

        #region Lưu đăng ký khám + Hủy số bản mới

        /// <summary>
        /// đăng ký khám ngoại trúlưu
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="kh"></param>
        /// <param name="ghly"></param>
        /// <param name="mjzbz"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="ksmc"></param>
        /// <param name="ysmc"></param>
        /// <param name="ghxm"></param>
        /// <param name="zlxm"></param>
        /// <param name="ghpbId"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="sfrq"></param>
        /// <param name="fph"></param>
        /// <param name="feeRelated"></param>
        /// <param name="brxz">tính chất bệnh nhândo trang truyền sang,vìlàBHYTbệnh nhân có thể đăng ký số tự chi trả</param>
        /// <param name="ybjsh">BHYTphíasố quyết toán(BHYTphía đãđăng ký khám)</param>
        /// <param name="qzjzxh">số thứ tự khám tiền tố</param>
        /// <param name="qzmzh">tiền tốsố khám ngoại trú</param>
        /// <returns></returns>
                public IActionResult Save(int patid, string kh, string ghly, string mjzbz,
            string ks, string ys, string ksmc, string ysmc, string ghxm, string zlxm
            , int ghpbId
            , bool isCkf, bool isGbf
            , DateTime? sfrq, string fph
            , OutpatientSettFeeRelatedDTO feeRelated
            , string brxz
            , string ybjsh
            , short? qzjzxh
            , string qzmzh
            , string jzyy,string jzid,string jzlx,string bzbm,string bzmc,string isjm
            )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(mjzbz) || string.IsNullOrWhiteSpace(ks) || string.IsNullOrWhiteSpace(ksmc) || ghpbId < 0
                    || string.IsNullOrWhiteSpace(brxz) || (feeRelated != null && feeRelated.zje < 0))
                {
                    //181008phải truyềnbrxz
                    //zjecó thể bằng0,nhưng không được nhỏ hơn0
                    return Error("Dữ liệu yêu cầu chưa đầy đủ");
                }
                object newJszbInfo;
                _outpatientRegApp.Save(patid, kh, ghly, mjzbz,
                ks, ys, ksmc, ysmc, ghxm, zlxm, fph, sfrq, isCkf, isGbf, ghpbId, feeRelated, brxz, ybjsh, Request.Query["mzyyghId"].FirstOrDefault(), ref qzjzxh, ref qzmzh, jzyy,jzid,  jzlx,  bzbm,  bzmc, isjm, out newJszbInfo);

                return Success(null, newJszbInfo);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(ybjsh))
                {
                    //BHYTphía đãđăng ký khám,HISđăng ký khámthất bại,phải hủyBHYTphíacủađăng ký khám,nhắc đăng ký lại
                    //20181018tạiClientgọi

                }
                throw ex;
            }
        }

        /// <summary>
        /// chưa quyết toánđăng ký khám hủy số
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult UnSettedRegBackNum(string mzh)
        {
            _outPatientSettleDmnService.UnSettedRegBackNum(mzh, this.OrganizeId);
            return Success();
        }

        #endregion

        /// <summary>
        /// hẹn đăng ký khám
        /// </summary>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="blh"></param>
        /// <returns></returns>
        public IActionResult BespeakRegister(int? zjlx, string zjh, string blh, string ksCode, int mzlx, string ysgh)
        {
            var param = new BespeakRegisterParamDTO
            {
                blh = blh,
                ksCode = ksCode,
                mzlx = mzlx,
                ysgh = ysgh,
                zjh = zjh,
                zjlx = zjlx
            };
            var result = new MzBespeakRegisterProcess(param).Process();
            return result.IsSucceed ? Success("", result.Data) : Error(result.ResultMsg);
        }

        #endregion
        public IActionResult OutpatientRegSearch()
        {
            return View();
        }
        public IActionResult GetRegListJson(Inparameter inparameter)
        {
            var data = _outPatientSettleDmnService.GetRegListJson(inparameter, this.OrganizeId);
            return Content(data.ToJson());
        }
        #region Book

        /// <summary>
        /// xác địnhtính chất bệnh nhânlàkhôngáp dụng chongoại trúkhám
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public IActionResult CheckBrxz(string kh, string updateBrxz)
        {
            _outpatientRegApp.CheckBrxz(kh, updateBrxz);
            return Success();
        }

        /// <summary>
        /// đặt trước
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="ssk"></param>
        /// <param name="fph"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="updateBrxz"></param>
        /// <returns></returns>
                public IActionResult Book(OutpatientRegistEntity gh, string ssk, string fph, bool isCkf, bool isGbf, string updateBrxz)
        {
            //cấu hình hệ thống
            var confEntity = _sysConfigRepo.GetByCode("OrderSwitch", OperatorProvider.GetCurrent().OrganizeId);
            if (confEntity != null && "T".Equals(confEntity.Value.Trim().ToUpper()))
            {
                return Content(_outpatientRegApp.Book(gh, Convert.ToDecimal(ssk), fph, isCkf, isGbf, updateBrxz).ToString());
            }
            return null;
        }

        #endregion

        #region Hóa đơn thu phí ngoại trúfastreportIn
        public IActionResult printOutpatientChargeBill(string jsnm)
        {
            var data = _iprintOutpatientChargeBillDmnService.GetmjzpjJson(jsnm, this.OrganizeId);
            return Success("", data);
        }

        public IActionResult printCqOutpatientChargeBill(string jsnm)
        {
            var data = _iprintOutpatientChargeBillDmnService.GetCqmjzpjJson(jsnm, this.OrganizeId);
            return Success("", data);
        }
        #endregion

        #region BHYT Trùng Khánh
        public IActionResult SaveCqybMedicalReg(CqybMedicalReg02Entity entity, CqybMedicalInPut02Entity medicalInList)
        {
           
			if (entity!=null)
			{
				entity.OrganizeId = this.OrganizeId;
				entity.zt = "1";
                _iCqybMedicalReg02Repo.SaveCqybMedicalReg(entity, null);
			}

            var jytype = entity.jytype;
            if (medicalInList != null)
            {
                medicalInList.OrganizeId = this.OrganizeId;
                medicalInList.jytype = jytype;
                medicalInList.zt = "1";
                _cqybmedicalInput02Repo.SaveCqybMedicalS02InReg(medicalInList, null);
            }

            return Success();
	    }
		/// <summary>
		/// lấyđăng ký khámchi phí
		/// </summary>
		/// <param name="ghlx"></param>
		/// <param name="zlxm"></param>
		/// <param name="isCkf"></param>
		/// <param name="isGbf"></param>
		/// <returns></returns>
	    public IActionResult GetOutpatientGhFees(string ghlx, string zlxm, bool isCkf, bool isGbf,string qzmzh,string yszjh,string ksbm,string ksbmmc,string gjysdm)
	    {
			var ghdata = _iOutPatChargeDmnService.GetChongQingGHMzjs(ghlx, zlxm, isCkf, isGbf, this.OrganizeId);
		    decimal ybje = Convert.ToDecimal(0.0000);
		    decimal zfje = Convert.ToDecimal(0.0000);
			foreach (var item in ghdata)
		    {
				//xác địnhkhoản mụclàBHYTkhoản mụchay tự chi trảkhoản mục,ở đây vìlàsypcchắc chắnlànull,vì vậy khi tra cứu trước tiên dùng trường này làmlàBHYTdấu hiệugán trường,có thể không thêm trường mới nữa
			    if (item.issc==1)
			    {
					ybje += Convert.ToDecimal(item.je);
			    }
			    else
			    {
					zfje += Convert.ToDecimal(item.je);
				}
		    }
		    var retData = new 
		    {
			    ybzje = ybje,
			    zfzje = zfje
			};
			return Content(retData.ToJson());
	    }
        #endregion


      
        public IActionResult OutPatientSbkhInput(string from = "")
        {
            ViewBag.from = from;
            return View();
        }
        public IActionResult OutPatientSfzhInput(string from = "")
        {
            ViewBag.from = from;
            return View();
        }
        /// <summary>
        /// xuấtđăng ký khámtra cứu theo điều kiện tra cứu
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="pagination"></param>
        /// <param name="createTimestart"></param>
        /// <param name="createTimeEnd"></param>
        /// <param name="syy"></param>
        /// <returns></returns>
        public IActionResult SelectRegChargeQueryExcel(DateTime? ksrq, DateTime? jsrq, string keywordo, string keywordt, int colStanWidth, bool? isContainFilter, string cols)
        { return Content("Xuất Excel chưa được triển khai trong bản .NET Core này (giai đoạn 1 chỉ xác minh đọc)."); }

    }
}
