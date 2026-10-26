using System;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Implementation.OutpatientManage.MzBespeakRegisterProcess;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using Newtouch.HIS.Domain.Entity.PatientManage;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.Core.Common;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Tools.Excel;
using System.Linq;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// Đăng ký khám ngoại trú
    /// </summary>
    public class OutpatientRegController : ControllerBase
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

		#region 挂号排班
		/// <summary>
		/// Lịch trực đăng ký khám List
		/// </summary>
		/// <param name="keyword">Khoa/Bác sĩ</param>
		/// <param name="mzlx">Loại ngoại trú Ngoại trú thông thường/Cấp cứu/Ngoại trú chuyên gia</param>
		/// <returns></returns>
		[HttpPost]
        [HandlerAjaxOnly]
        public JsonResult GetRegScheduleList(string keyword, string mzlx)
        {
            var data = _outpatientRegApp.GetRegScheduleList(keyword, mzlx);
            return Json(data);
        }

        /// <summary>
        /// Lấy lịch trực mới
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mzlx"></param>
        /// <returns></returns>
        public JsonResult GetNewRegScheduleList(string keyword, string mzlx)
        {
            var data = _iOutPatientDmnService.GetNewRegScheduleList(keyword,mzlx,this.OrganizeId);
            return Json(data);
        }
        
        public ActionResult GetNewRegSchedulebyGroupList(string keyword, string pbks, string mzlx,DateTime? OutDate,string ys)
        {
            var data = _iOutPatientDmnService.GetRegScheduleListbyGroup(keyword,mzlx,pbks,this.OrganizeId, OutDate, ys);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Bệnh nhân đã đặt lịch đăng ký khám hay chưa
        /// </summary>
        /// <param name="mzlx"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetIsMzghBook(Pagination pagination,string mzlx,string patid,string isfeegroup, string yyzt, string ghly, string keyValue,string ks,
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
        /// Tra cứu đã đăng ký khám trong ngày
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        public ActionResult GetCurrentDayRegListJson(string orgId, string creatorCode)
        {
            var data = _outPatientSettleDmnService.GetCurrentDayRegList(this.OrganizeId, this.UserIdentity.UserCode);
            return Content(data.ToJson());
        }

        public ActionResult OutpatientAppointment(string from = "")
        {
            ViewBag.from = from;
            return View();
        }
        /// <summary>
        /// Sửa trạng thái đặt lịch đăng ký khám
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="mzh"></param>
        /// <param name="patid"></param>
        /// <param name="ks"></param>
        /// <param name="zje"></param>
        /// <returns></returns>
        public ActionResult UpdateMzGhBook(string jsnm,string mzh,string patid,string ks,string zje,string outdate,string mzlx,string ghf)
        {
            _iMzghBookRepo.UpdateMzGhAppointment(jsnm,  mzh, Convert.ToInt32(patid),  ks,  zje,  "His",this.OrganizeId,outdate,mzlx,ghf);
            return Success();
        }

		public ActionResult FZsjTB()
		{
			string ghrq = DateTime.Now.ToString("yyyy-MM-dd");
			_iMzghBookRepo.FZsjTB(ghrq,this.UserIdentity.rygh,this.OrganizeId);
			return Success();
		}
        /// <summary>
        /// Điểm danh đặt lịch đăng ký khám
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult SigninAppointment(string mzh)
        {
            _iMzghBookRepo.SigninAppointment(mzh, this.OrganizeId);
            return Success();
        }
        #endregion

        #region 发票号
        /// <summary>
        /// Chọn hóa đơn
        /// </summary>
        /// <returns></returns>
        public ActionResult ChooseInvoice(string from = "")
        {
            ViewBag.from = from;
            return View();
        }
        /// <summary>
        /// Lấy số hóa đơn theo mã nhân viên
        /// </summary>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetInvoice()
        {
            var data = _outpatientRegApp.GetInvoice();
            return Content(data);
        }

        /// <summary>
        /// Kiểm tra số hóa đơn đã nhập có dùng được hay không
        /// by caishanshan
        /// </summary>
        /// <param name="inputFPH"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult CheckInvoice(string inputFPH)
        {
            _outpatientRegApp.CheckInvoice(inputFPH);
            return Success();
        }
        #endregion

        /// <summary>
        /// Lấy phí đăng ký khám Phí khám chữa bệnh Phí thẻ từ Phí làm thẻ
        /// </summary>
        /// <param name="ghlx">Loại đăng ký khám</param>
        /// <param name="zlxm">Khoản mục khám chữa bệnh</param>
        /// <param name="isZzjm">Miễn giảm chuyển viện</param>
        /// <param name="isCkf">Phí thẻ từ</param>
        /// <param name="isGbf">Phí làm thẻ</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetOutpatientFees(string ghlx, string zlxm, bool isCkf, bool isGbf)
        {
            var fees = _outPatientSettleDmnService.GetOutpatientFees(ghlx, zlxm, isCkf, isGbf);
            return Content(fees.ToJson());
        }
        [HandlerAjaxOnly]
        public ActionResult GetOutpatientFeesbyGroup(string ghlx, string zlxm, bool isCkf, bool isGbf)
        {
            var fees = _outPatientSettleDmnService.GetOutpatientFeesbyGroup(ghlx, zlxm, isCkf, isGbf);
            return Content(fees.ToJson());
        }

        #region 门诊挂号2018

        /// <summary>
        /// Trang đăng ký khám ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientReg2018()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrgId = this.OrganizeId;
            //Công tắc：Đặt lịch đăng ký khám
            var brSwitch = (bool)_sysConfigRepo.GetBoolValueByCode("BespeakRegisterSwitch", OrganizeId, false);
            ViewBag.ISOpenBespeakRegister = brSwitch ? "true" : "false";
            ViewBag.Xzqh = _outPatientSettleDmnService.GetOrgXzqh(this.OrganizeId);
            return View();
        }

        /// <summary>
        /// Xác định có đăng ký khám trùng lặp hay không
        /// </summary>
        /// <param name="blh"></param>
        /// <returns></returns>
        public ActionResult AllowRegh(string blh)
        {
            var result = _outpatientRegistRepo.AllowRegh(blh, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// Sửa số điện thoại
        /// </summary>
        /// <param name="blh"></param>
        /// <returns></returns>
        public ActionResult UpdatePatPhone(string patid,string phone)
        {
            var result = _outpatientRegistRepo.UpdatePatPhone(patid, phone,UserIdentity.rygh, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// Lớp nổi tra cứu bệnh nhân
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult PatSearchInfo(string keyword)
        {
            var data = _refundDmnService.GetBasicInfoSearchListInRegister(keyword, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Tạo mã ngoại trú duy nhất、Số thứ tự khám bệnh
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghlx"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <returns></returns>
        public ActionResult GetMzhJzxh(int patid, string ghpbId, string ks, string ys)
        {
            if (patid <= 0 || string.IsNullOrWhiteSpace(ghpbId) || string.IsNullOrWhiteSpace(ks))
            {
                return Error("请求数据不完整");
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
        /// chongqing Mã ngoại trú duy nhất của lịch trực mới、Số thứ tự khám bệnh
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghpbId"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="QueueNo">Số thứ tự khám bệnh đặt lịch</param>
        /// <returns></returns>
        public ActionResult GetNewMzhJzxh(int patid,string ghpbId,string ks,string ys,string mjzbz,string QueueNo,string OutDate,bool isybjy)
        {
            var ysxx = new CqybGjbmInfoVo();
            if (patid <= 0 || string.IsNullOrWhiteSpace(ghpbId) || string.IsNullOrWhiteSpace(ks))
            {
                return Error("请求数据不完整");
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
        /// Lấy thông tin cần thiết để đăng ký khám bệnh bảo hiểm y tế
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult GetYbjzdjVo(string mzh)
        {
            var data = new
            {
                ysxx = _outPatientSettleDmnService.GetYbjzdjVo(mzh,this.OrganizeId)
            };
            return Success(null, data);
        }

        #region 保存挂号 + 新版退号

        /// <summary>
        /// Lưu đăng ký khám ngoại trú
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
        /// <param name="brxz">Tính chất bệnh nhân được truyền từ trang sang，Vì bệnh nhân bảo hiểm y tế có thể đăng ký số tự trả</param>
        /// <param name="ybjsh">Mã quyết toán phía bảo hiểm y tế（Phía bảo hiểm y tế đã đăng ký khám）</param>
        /// <param name="qzjzxh">Số thứ tự khám bệnh phía trước</param>
        /// <param name="qzmzh">Mã ngoại trú phía trước</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult Save(int patid, string kh, string ghly, string mjzbz,
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
                    //181008 Bắt buộc phải truyền brxz
                    //zje Có thể bằng 0，Nhưng không thể nhỏ hơn 0
                    return Error("请求数据不完整");
                }
                object newJszbInfo;
                _outpatientRegApp.Save(patid, kh, ghly, mjzbz,
                ks, ys, ksmc, ysmc, ghxm, zlxm, fph, sfrq, isCkf, isGbf, ghpbId, feeRelated, brxz, ybjsh, Request.Params["mzyyghId"], ref qzjzxh, ref qzmzh, jzyy,jzid,  jzlx,  bzbm,  bzmc, isjm, out newJszbInfo);

                return Success(null, newJszbInfo);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(ybjsh))
                {
                    //Phía bảo hiểm y tế đã đăng ký khám，HIS Đăng ký khám thất bại，Phải hủy đăng ký khám phía bảo hiểm y tế，Nhắc đăng ký lại
                    //20181018 Tại Client gọi

                }
                throw ex;
            }
        }

        /// <summary>
        /// Đăng ký khám chưa kết Trả số khám
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult UnSettedRegBackNum(string mzh)
        {
            _outPatientSettleDmnService.UnSettedRegBackNum(mzh, this.OrganizeId);
            return Success();
        }

        #endregion

        /// <summary>
        /// Đặt lịch đăng ký khám
        /// </summary>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="blh"></param>
        /// <returns></returns>
        public ActionResult BespeakRegister(int? zjlx, string zjh, string blh, string ksCode, int mzlx, string ysgh)
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
        public ActionResult OutpatientRegSearch()
        {
            return View();
        }
        public ActionResult GetRegListJson(Inparameter inparameter)
        {
            var data = _outPatientSettleDmnService.GetRegListJson(inparameter, this.OrganizeId);
            return Content(data.ToJson());
        }
        #region Book

        /// <summary>
        /// Xác định tính chất bệnh nhân có áp dụng cho ngoại trú hay không
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public ActionResult CheckBrxz(string kh, string updateBrxz)
        {
            _outpatientRegApp.CheckBrxz(kh, updateBrxz);
            return Success();
        }

        /// <summary>
        /// Đặt trước
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="ssk"></param>
        /// <param name="fph"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="updateBrxz"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult Book(OutpatientRegistEntity gh, string ssk, string fph, bool isCkf, bool isGbf, string updateBrxz)
        {
            //Cấu hình hệ thống
            var confEntity = _sysConfigRepo.GetByCode("OrderSwitch", OperatorProvider.GetCurrent().OrganizeId);
            if (confEntity != null && "T".Equals(confEntity.Value.Trim().ToUpper()))
            {
                return Content(_outpatientRegApp.Book(gh, Convert.ToDecimal(ssk), fph, isCkf, isGbf, updateBrxz).ToString());
            }
            return null;
        }

        #endregion

        #region 门诊收费票据fastreport打印
        public ActionResult printOutpatientChargeBill(string jsnm)
        {
            var data = _iprintOutpatientChargeBillDmnService.GetmjzpjJson(jsnm, this.OrganizeId);
            return Success("", data);
        }

        public ActionResult printCqOutpatientChargeBill(string jsnm)
        {
            var data = _iprintOutpatientChargeBillDmnService.GetCqmjzpjJson(jsnm, this.OrganizeId);
            return Success("", data);
        }
        #endregion

        #region 重庆医保
        public ActionResult SaveCqybMedicalReg(CqybMedicalReg02Entity entity, CqybMedicalInPut02Entity medicalInList)
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
		/// Lấy phí đăng ký khám
		/// </summary>
		/// <param name="ghlx"></param>
		/// <param name="zlxm"></param>
		/// <param name="isCkf"></param>
		/// <param name="isGbf"></param>
		/// <returns></returns>
	    public ActionResult GetOutpatientGhFees(string ghlx, string zlxm, bool isCkf, bool isGbf,string qzmzh,string yszjh,string ksbm,string ksbmmc,string gjysdm)
	    {
			var ghdata = _iOutPatChargeDmnService.GetChongQingGHMzjs(ghlx, zlxm, isCkf, isGbf, this.OrganizeId);
		    decimal ybje = Convert.ToDecimal(0.0000);
		    decimal zfje = Convert.ToDecimal(0.0000);
			foreach (var item in ghdata)
		    {
				//Xác định khoản mục là khoản mục bảo hiểm y tế hay tự trả，Ở đây vì sypc Chắc chắn là null，Vì vậy khi tra cứu, trước tiên gán trường này làm trường cờ bảo hiểm y tế，Có thể không cần thêm trường mới nữa
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


      
        public ActionResult OutPatientSbkhInput(string from = "")
        {
            ViewBag.from = from;
            return View();
        }
        public ActionResult OutPatientSfzhInput(string from = "")
        {
            ViewBag.from = from;
            return View();
        }
        /// <summary>
        /// Xuất tra cứu theo điều kiện tra cứu đăng ký khám
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="pagination"></param>
        /// <param name="createTimestart"></param>
        /// <param name="createTimeEnd"></param>
        /// <param name="syy"></param>
        /// <returns></returns>
        public ActionResult SelectRegChargeQueryExcel(DateTime? ksrq, DateTime? jsrq, string keywordo, string keywordt, int colStanWidth, bool? isContainFilter, string cols)
        {
            if (string.IsNullOrWhiteSpace(cols))
            {
                cols = WebHelper.GetCookie("ExportExcelCols");
                if (!string.IsNullOrWhiteSpace(cols))
                {
                    cols = System.Web.HttpUtility.UrlDecode(cols);
                    WebHelper.RemoveCookie("ExportExcelCols");
                }
            }
            if (string.IsNullOrWhiteSpace(cols))
            {
                throw new FailedException("未指定导出列");
            }
            Inparameter inparameter = new Inparameter();
            inparameter.ksrq = ksrq;
            inparameter.jsrq = jsrq;
            if (!string.IsNullOrWhiteSpace(keywordo) && !string.IsNullOrWhiteSpace(keywordt))
            {
                inparameter.keywordo = keywordo;
                inparameter.keywordt = keywordt;
            }
            var list = _outPatientSettleDmnService.GetRegListJson(inparameter, this.OrganizeId);


            foreach (GhJzInfo qd in list)
            {
                qd.xb = qd.xb == "1" ? "男" : "女";
                qd.brxz = qd.brxz == "0" ? "自费" : "医保";
                qd.ghzt = qd.ghzt == "1" ? "已结" : qd.ghzt == "2" ? "已退" : "待结";
                qd.jzzt = ((EnumOutpatientJzbz)(Convert.ToInt32(qd.jzzt))).GetDescription();
            }
            var colList = cols.ToObject<IList<ExcelColumn>>();
            var sheet = new ExcelSheet()
            {
                Title = "挂号查询",
                columns = colList,
            };
            sheet.columns.ToList().ForEach(p =>
            {
                p.WidthTimes = (double)p.Width / colStanWidth;
                p.Width = 0;    //Width đều đặt thành 0
            });

            var path = DateTime.Now.ToString("\\\\yyyyMMdd\\\\HHmmssfff挂号查询") + ".xls";

            var filePath = CommmHelper.GetLocalFilePath("\\Excel导出\\挂号查询" + path);

            if (isContainFilter == true)
            {
                //Điều kiện lọc
                var filterDict = new Dictionary<string, string>();
                if (inparameter.ksrq.HasValue && inparameter.jsrq.HasValue)
                {
                    filterDict.Add("挂号日期", inparameter.ksrq.Value.ToString("yyyy-MM-dd") + " 至 " + inparameter.jsrq.Value.ToString("yyyy-MM-dd"));
                }

                if (!string.IsNullOrWhiteSpace(inparameter.keywordt))
                {
                    filterDict.Add("挂号科室", inparameter.keywordt);
                }

                if (filterDict.Count > 0)
                {
                    sheet.filters = filterDict;
                }
            }

            var rest = list.ToExcel(filePath, sheet);

            if (rest)
            {
                return File(filePath, "application/x-xls", path.Replace("\\", ""));
            }
            else
            {
                return Content("文件导出失败，请返回列表页重试");
            }
        }
    }
}