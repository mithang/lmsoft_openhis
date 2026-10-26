using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian.DTO.S24;
using Newtouch.HIS.Proxy.guian.DTO.S25;
using Newtouch.HIS.Proxy.Log;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using newtouchyibao;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.YibaoInterfaceManage;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtouch.Infrastructure.Model;
using System.Configuration;
using Newtouch.HIS.DomainServices;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OutpatChargeController : ControllerBase
    {
        // GET: OutpatientManage/OutPatCharge
        private readonly IOutPatChargeApp _outPatChargeApp;
        private readonly IOutPatChargeDmnService _outChargeDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IOutpatientPrescriptionRepo _outpatientPrescriptionRepo;
        private readonly IOutPatientUniversalDmnService _outPatientUniversalDmnService;
        private readonly IOutpatientSettlementRepo _outpatientSettlementRepo;
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly IGuiAnOutpatientXnhApp _guiAnOutpatientXnhApp;
        private readonly ISysPatientBasicInfoRepo _sysPatientBasicInfoRepo;
        private readonly IPhysicalexamDmnService _iphysicalexamDmnService;
	    private readonly ICqybUploadPresList04Repo _iCqybUploadPresList04Repo;
	    private readonly ICqybMedicalReg02Repo _cqybMedicalReg02Repo;
        private readonly ICqybMedicalInPut02Repo _cqybMedicalinput02Repo;
	    private readonly ICqybSett05Repo _cqybSett05Repo;
        private readonly ICqybSett23Repo _cqybSett23Repo;
        private readonly ICqybUpdateMedicalInput03Repo _cqybupdatemedicalInput03Repo;
        private readonly IPatientBasicInfoDmnService _PatientBasicInfoDmnService;
        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;

        #region  view
        /// <summary>
        /// Giao diện chính thu phí ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult ChargeIndex()
        {
            return View();
        }

        public ActionResult ConfirmForm()
        {
            return View();
        }
        public ActionResult SettConfirmForm()
        {
            return View();
        }

        public ActionResult ChargeConfirm()
        {
            return View();
        }
        /// <summary>
        /// Thu phí
        /// </summary>
        /// <returns></returns>
        public ActionResult ChargeAddPay()
        {
            return View();
        }

        /// <summary>
        /// Hộp bật xác nhận thanh toán quyết toán
        /// </summary>
        /// <returns></returns>
        public ActionResult ChargePayDialog()
        {
            return View();
        }
        /// <summary>
        /// Cửa sổ mẫu
        /// </summary>
        /// <returns></returns>
        public ActionResult ItemTemplate()
        {
            return View();
        }

        /// <summary>
        /// Cửa sổ tính chất bệnh nhân
        /// </summary>
        /// <returns></returns>
        public ActionResult ChargeBrxzDialog()
        {
            return View();
        }
        /// <summary>
        /// Chuyển tự trả ngoại trú sang bảo hiểm y tế
        /// </summary>
        /// <returns></returns>
        public ActionResult dkfsxz_mz()
        {
            return View();
        }
        #endregion

        #region 门诊收费
        /// <summary>
        /// Tra cứu thông tin cá nhân thu phí ngoại trú
        /// </summary>
        /// <param name="kh">Số thẻ</param>
        /// <param name="brxz"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChargeIndex(string kh, string brxz)
        {
            var patChargeDto = new OutPatChargeDto();
            var patChargeInfo = _outPatChargeApp.GetOutPatChargeInfo(kh, brxz);

            patChargeDto.patInfo = patChargeInfo;
            if (patChargeInfo == null)
            {
                return Success("", patChargeDto);
            }
            //Xác định có nhiều tính chất bệnh nhân hay không
            if (patChargeInfo.brxzList != null && patChargeInfo.brxzList.Count >= 0)
            {
                return Success("", patChargeDto);
            }
            OutPatChargeDoctorVO docVo;
            var ghItemList = _outPatChargeApp.GetOutGhInfo(patChargeInfo.patid, patChargeInfo.brxz, out docVo);
            patChargeDto.ghItemList = ghItemList;
            patChargeDto.ghDocVo = docVo;

            return Success("", patChargeDto);
        }

        /// <summary>
        /// Lấy thông tin đăng ký khám
        /// </summary>
        /// <param name="patid">Bệnh nhân patid</param>
        /// <param name="brxzbh">Mã tính chất bệnh nhân</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getGHSource(int patid, string brxz)
        {
            var docVo = new OutPatChargeDoctorVO();
            List<OutPatChargeItemVO> ghItemList = _outPatChargeApp.GetOutGhInfo(patid, brxz, out docVo);
            return Success("", ghItemList);
        }

        /// <summary>
        /// Lấy thuốc và khoản mục thu phí
        /// </summary>
        /// <param name="keyword">Điều kiện tra cứu theo từ khóa</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public JsonResult GetYPItemInfo(string keyword)
        {
            return keyword == null ? null : Json(_outPatChargeApp.GetYpItemInfo(keyword.Trim().Replace("'", "").Replace(",", "").Replace("%", "")));
        }

        /// <summary>
        /// Lấy thông tin phí dịch vụ
        /// </summary>
        /// <param name="sfxmtype">Loại khoản mục Xác định là thuốc hay khoản mục</param>
        /// <param name="lscfh">Mã đơn thuốc tạm thời</param>
        /// <param name="brxz">Tính chất bệnh nhân</param>
        /// <param name="dl">Loại lớn</param>
        /// <param name="sfxm">Khoản mục thu phí</param>
        /// <param name="dj">Đơn giá</param>
        /// <param name="sl">Số lượng</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetCfAndSfInfo(string sfxmtype, string lscfh, string brxz, string dl, string sfxm, string dj, string sl)
        {
            return Success("", _outPatChargeApp.GetCfhAndFjfMoney(sfxmtype, lscfh, brxz, dl, sfxm, dj, sl));
        }


        /// <summary>
        /// Lấy mã đơn thuốc
        /// </summary>
        /// <param name="sfxmtype">Loại khoản mục Xác định là thuốc hay khoản mục</param> 
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetCFH(string sfxmtype)
        {
            string cfh = _outPatChargeApp.GetCfh(sfxmtype);
            return Success("", cfh);
        }

        /// <summary>
        /// Lấy mẫu khoản mục thu phí
        /// </summary>
        /// <param name="ks">Khoa</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetChargeItemData(string ks)
        {
            var data = _outPatChargeApp.GetChargeTemplateAll(ks);
            return Success("", data);
        }


        /// <summary>
        /// Thêm thông tin đơn thuốc，Quyết toán
        /// </summary>
        /// <param name="chargeDto">Thông tin danh sách đơn thuốc</param>
        /// <param name="kh">Số thẻ</param>
        /// <param name="fph">Số hóa đơn</param>
        /// <param name="xjzffs">Phương thức thanh toán tiền mặt</param>
        /// <param name="xjzffsbh"></param>
        /// <param name="zfje">Số tiền thanh toán-Tổng số tiền bệnh nhân thanh toán</param>
        /// <param name="ysk">Khoản phải thu-Số tiền phải thanh toán</param>
        /// <param name="zl">Tiền thừa</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult AddCfInfo(List<OutpatChargeDto> chargeDto, string kh, string fph, string xjzffs, string xjzffsbh, string zfje, string ysk, string zl)
        {
            var settDto = _outPatChargeApp.OutPatChargeSett(chargeDto, kh, fph, xjzffs, xjzffsbh, Convert.ToDecimal(zfje), Convert.ToDecimal(ysk), Convert.ToDecimal(zl));
            return Success("", settDto);
        }

        #endregion

        #region 收费2018版

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index2018()
        {
            ViewBag.sfxm_dj = _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId).Value;
            ViewBag.ChargeItemConfig = _sysConfigRepo.GetByCode("ChargeItemConfig", OrganizeId) == null ? "original" : _sysConfigRepo.GetByCode("ChargeItemConfig", OrganizeId).Value;
            ViewBag.ISMedicineSearchRelatedKC = _sysConfigRepo.GetBoolValueByCode("IS_MedicineSearchRelatedKC", OrganizeId);
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.isOpenQfyj = _sysConfigRepo.GetBoolValueByCode("Outpatient_Charge_Open_Qfyj", this.OrganizeId);
            ViewBag.Xzqh = _outPatientSettleDmnService.GetOrgXzqh(this.OrganizeId);
            return View();
        }

        /// <summary>
        /// Chọn bản ghi khám bệnh
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatAccListView()
        {
            return View();
        }

        /// <summary>
        /// Lấy lịch sử ghi sổ
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public ActionResult GetpatientAccountList(DateTime kssj, DateTime jssj, string kh,string zjh, string cardType)
        {
            return Content(_outChargeDmnService.GetpatientAccountList(kssj, jssj, kh,zjh, cardType, OrganizeId).ToJson());
        }

        /// <summary>
        /// Lấy lịch sử ghi sổ
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <param name="isTF">Có phải đến từ hoàn phí hay không</param>
        /// <returns></returns>
        public ActionResult GetpatientAccountInfo(string mzh, string kh,string zjh, string cardType, bool? isTF)
        {
            if (string.IsNullOrWhiteSpace(mzh) && (string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(cardType))&& string.IsNullOrWhiteSpace(zjh))
            {
                throw new FailedException("缺少查询参数：门诊号或卡号");
            }
            var resultDto = new OutpatOptimAccInfoDto
            {
                OutpatAccInfoDto = null,
                OutpatAccListDto = _outChargeDmnService.GetOutpatChargePatInfoInAcc(mzh, kh,zjh, cardType, this.OrganizeId)
            };
            if (resultDto.OutpatAccListDto.Count == 1)
            {
                resultDto.OutpatAccInfoDto = resultDto.OutpatAccListDto.FirstOrDefault();
                return Success(null, resultDto);
            }

            if (resultDto.OutpatAccListDto.Count < 1) return Error("查询失败");
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                return Error("查询异常，门诊号查询出多条就诊记录");
            }

            if ((string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(cardType)) && string.IsNullOrWhiteSpace(zjh)) return Error("查询失败");
            //Tra bằng thẻ，Tra bản ghi đang khám hoặc chờ khám
            var theInfo = resultDto.OutpatAccListDto.OrderByDescending(p => p.ghrq).FirstOrDefault();
            //if (!(isTF == true) && theInfo.ghzt == "0")
            if (isTF != true)  //Nếu không phải hoàn phí，thì trả về thông tin khám bệnh lần cuối cùng
            {
                //Trạng thái chưa kết
                resultDto.OutpatAccInfoDto = theInfo;
                return Success(null, resultDto);
            }

            if (theInfo.ghzt != "1") return Error("查询异常，无法定位唯一待收费就诊记录");
            //Trạng thái đã kết
            resultDto.OutpatAccInfoDto = theInfo;
            return Success(null, resultDto);
        }
        public ActionResult GetpatientAccountInfobykh(string mzh, string kh, string zjh, string cardType, bool? isTF)
        {
            if (string.IsNullOrWhiteSpace(mzh) && (string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(cardType)) && string.IsNullOrWhiteSpace(zjh))
            {
                throw new FailedException("缺少查询参数：门诊号或卡号");
            }
            var OutpatAccListDtoList = _outChargeDmnService.GetOutpatChargePatInfoInAcc(mzh, kh, zjh, cardType, this.OrganizeId);
            var datadsf = _outChargeDmnService.GetPatientGridJson(OrganizeId);//Lấy mã ngoại trú chờ thu phí để lọc thông tin bệnh nhân
            var data = datadsf.Serialize().ToObject<List<OutPatientRegVO>>() ?? new List<OutPatientRegVO>();
            List<OutpatAccInfoDto> patlist = new List<OutpatAccInfoDto>();
            foreach (var item in data)
            {
                var mhzlist = OutpatAccListDtoList.Where(p => p.mzh == item.mzh).FirstOrDefault();
                if (mhzlist!=null)
                {
                    patlist.Add(mhzlist);
                }
            }
            if (patlist.Count < 1) return Error("查询失败");
            var resultDto = new OutpatOptimAccInfoDto
            {
                OutpatAccInfoDto = null,
                OutpatAccListDto = patlist
            };
            if (patlist.Count == 1)
            {
                resultDto.OutpatAccInfoDto = patlist.FirstOrDefault();
                return Success(null, resultDto);
            }
            return Success(null, resultDto);
        }
        public ActionResult ChoosePrescription()
        {
            return View();
        }
        public ActionResult ChooseMzh()
        {
            return View();
        }
        public ActionResult GetPatientGridJson()
        {
            var data = _outChargeDmnService.GetPatientGridJson(OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy lần chờ quyết toán cuối cùng theo mã cá nhân bảo hiểm y tế nông thôn mới
        /// </summary>
        /// <param name="xnhgrbm">Mã cá nhân bảo hiểm y tế nông thôn mới</param>
        /// <returns></returns>
        public ActionResult GetLastSettleRecord(string xnhgrbm)
        {
            var result = new OutPatientRegVO
            {
                xnhgrbm = xnhgrbm
            };
            var tdata = _outChargeDmnService.GetPatientGridJson(OrganizeId);
            var data = tdata.Serialize().ToObject<List<OutPatientRegVO>>() ?? new List<OutPatientRegVO>();
            if (data.Count == 0) return Content(result.ToJson());

            var entity = _sysPatientBasicInfoRepo.FindEntity(p => //p.xnhgrbm == xnhgrbm &&
                                                                   p.OrganizeId == OrganizeId
                                                                  && p.zt == "1") ?? new SysPatientBasicInfoEntity();
            if (string.IsNullOrWhiteSpace(entity.blh)) return Content(result.ToJson());
            result = data.OrderByDescending(p => p.mzh).FirstOrDefault(w => w.blh == entity.blh) ?? new OutPatientRegVO
            {
                xnhgrbm = xnhgrbm
            };
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy số thẻ đăng ký khám của bệnh nhân
        /// </summary>
        /// <param name="xnhgrbm"></param>
        /// <returns></returns>
        //public ActionResult GetPatientkh(string xnhgrbm)
       // {
            //var brxx = _sysPatientBasicInfoRepo.FindEntity(p => p.xnhgrbm == xnhgrbm
            //                                                      && p.OrganizeId == OrganizeId
            //                                                      && p.zt == "1") ?? new SysPatientBasicInfoEntity();
            //var ghxx = _outpatientRegistRepo.IQueryable(p =>
            //    p.blh == brxx.blh && p.OrganizeId == brxx.OrganizeId
            //                      && p.ghzt == "1" && p.zt == "1").OrderByDescending(o => o.CreateTime).FirstOrDefault();
            //return Content(new { kh = ghxx.kh, cardType = ghxx.CardType }.ToJson());
        //}

        public ActionResult GetUnSettedPrescriptionBymzh(string mzh)
        {
            var cfList = _outChargeDmnService.GetPrescriptionBymzh(mzh, OrganizeId);
            var sItemList = _outChargeDmnService.GetAllUnSettedSingleItemListByMzh(mzh, OrganizeId);
            var data = new
            {
                cfList = cfList,
                sItemList = sItemList,
            };
            return Success(null, data);
        }

        public ActionResult GetNewUnSettedPrescriptionByMzh(string mzh)
        {
            var data = _outChargeDmnService.GetNewUnSettedPrescriptionByMzh(mzh, OrganizeId);
            return Content(data.ToJson());
        }

        public ActionResult ChooseCf()
        {
            return View();
        }

        public ActionResult GetNewAllUnSettedListByMzh(string mzh, string cfnms)
        {
            var data = _outChargeDmnService.GetNewAllUnSettedListByMzh(mzh, cfnms, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Tải chờ thu phí（Chi tiết đơn thuốc）
        /// </summary>
        /// <param name="cfnmList"></param>
        /// <returns></returns>
        public ActionResult loadPrescriptionDetailDataBycfnm(IList<int> cfnmList)
        {
            var data = _outChargeDmnService.GetPrescriptionDetailBycfnm(cfnmList, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Tải chờ thu phí（Chi tiết đơn thuốc + Chi tiết khoản mục không theo đơn）
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult GetAllUnSettedListByMzh(string mzh)
        {
            var data = _outChargeDmnService.GetAllUnSettedListListByMzh(mzh, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Tải lên chi tiết chưa kết
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult DetailsUploadYb(string mzh)
        {
            _outPatChargeApp.DetailsUploadYb(mzh);
            return Success();
        }
        /// <summary>
        /// Trích xuất chi tiết tải lên quyết toán bảo hiểm y tế Quý An
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult GetGuiAnDetailsMzjsYb(string mzh)
        {
            guianMainAllOfMzjs guianMainOfMzjsModel = _outPatChargeApp.GetGuianMainOfMzjs(mzh);
            return Content(guianMainOfMzjsModel.ToJson());
        }
        /// <summary>
        /// Lấy sau khi hoàn phí，Dữ liệu cần thiết để tải lên bảo hiểm y tế Quý An
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult GetGuiAnDetailsMzjsYbTfh(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh)
        {
            guianMainAllOfMzjs guianMainOfMzjsModel = _outPatChargeApp.GetGuiAnDetailsMzjsYbTfh(jsnm, tjsxmDict, mzh);
            return Content(guianMainOfMzjsModel.ToJson());
        }

		#region 重庆医保
		/// <summary>
		/// Lấy sửa đăng ký khám bệnh ngoại trú
		/// </summary>
		/// <param name="mzh"></param>
		/// <returns></returns>
		public ActionResult GetCQjzdjInfo(string mzh)
		{
			Input_2203A model = _outChargeDmnService.GetCQjzdjInfo(mzh, this.OrganizeId);
			return Content(model.ToJson());
		}
	    /// <summary>
	    /// Lấy mã giao dịch đăng ký khám bệnh của bệnh nhân
	    /// </summary>
	    /// <param name="mzh"></param>
	    /// <returns></returns>
	    public ActionResult GetCQjzdjDataInfo(string zymzh)
	    {
            
		    CqybMedicalReg02Entity entity =
			    _cqybMedicalReg02Repo.FindEntity(p => p.zymzh == zymzh && p.zt == "1" && p.OrganizeId == this.OrganizeId);
			return Content(entity.ToJson());
		}
		/// <summary>
		/// Lấy chi tiết tải lên khi quyết toán ngoại trú（ybzje Tổng số tiền，zfzje là tất cả số tiền tự trả）
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="mzh"></param>
		/// <returns></returns>
		public ActionResult GetChongQingDetailsMzjsYb(string mzh, string cfnm)
		{
            if (string.IsNullOrWhiteSpace(cfnm))
            {
                return Error("处方内码(cfnm)参数未传递或值为空");
            }

			decimal ybzje = Convert.ToDecimal(0.0000);
			decimal zfzje = Convert.ToDecimal(0.0000);
            _outPatChargeApp.GetChongQingMainOfMzjs(mzh, cfnm, out ybzje, out zfzje);
			var data = new
			{
				ybzje = ybzje,
				zfzje= zfzje,
                zje= ybzje+zfzje,
            };
			return Success(null, data);
		}
	    
		public ActionResult SaveChongQingUploadPrescriptions(List<CqybUploadPresList04Entity> entityList, List<UploadPrescriptionsListInPut> cflist,string zymzh,string jytype)
	    {
		    _iCqybUploadPresList04Repo.SaveCqybUploadPresList(entityList, jytype, zymzh,this.OrganizeId);
			//Đơn thuốc đã tải lên cũng được lưu xuống，Lưu mã đơn thuốc，Để trả đơn thuốc
		    _outPatChargeApp.SaveCqybUploadInPres(cflist, zymzh, jytype);
            return Success();
	    }
        public ActionResult SaveUploadPch(string zymzh, string jytype, string cfh, string pch)
        {
            _outPatChargeApp.SaveCqybUploadInPres(zymzh, jytype, cfh, pch);
            return Success();
        }

        public ActionResult SaveCqybUpdateMedicalReg(CqybUpdateMedicalInput03Entity entity)
        {
            if (entity != null)
            {
                entity.OrganizeId = this.OrganizeId;
                entity.zt = "1";
                _cqybupdatemedicalInput03Repo.SaveCqybUpdateMedicalS03InReg(entity, null);
            }
            return Success();
        }

        /// <summary>
        /// Sau khi hủy đơn thuốc，Hủy 04 Tải lên và lưu xuống
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="cflist"></param>
        /// <param name="zymzh"></param>
        /// <param name="jytype"></param>
        /// <param name="cfh"></param>
        /// <param name="yynm"></param>
        /// <returns></returns>
        public ActionResult UpDateUploadPrescriptions(string zymzh, string cfh)
        {
            _outPatChargeApp.UpdateCqyb04InPres(zymzh, cfh, this.OrganizeId);
            return Success();
        }
        /// <summary>
        /// Hủy theo số lô
        /// </summary>
        /// <param name="zymzh"></param>
        /// <param name="pch"></param>
        /// <returns></returns>
        public ActionResult UpDateUploadPch(string zymzh, string cfh, string pch)
        {
            if (string.IsNullOrWhiteSpace(cfh))
            {
                return Error("处方号(cfh)参数未传递或值为空");
            }

            _outPatChargeApp.UpDateUploadPch(zymzh, cfh, pch, this.OrganizeId);
            return Success();
        }

        /// <summary>
        /// Lưu đảo ngược quyết toán Trùng Khánh 99 Trả về dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="jslb"></param>
        /// <returns></returns>
        public ActionResult SaveChongQing99HQ(Output_2207VO entity,string jslb)
	    {
            if (entity!=null)
            {
                try {
                    CqybSett05Entity ybentity = new CqybSett05Entity();
                    ybentity.jylsh = entity.setl_id;
                    ybentity.OrganizeId = this.OrganizeId;
                    ybentity.jslb = jslb;
                    ybentity.jsnm = entity.jsnm;
                    ybentity.cqtczf = entity.hifp_pay;
                    ybentity.zhzf = entity.acct_pay;
                    ybentity.cqxjzf = entity.psn_cash_pay;
                    ybentity.gwybz = entity.cvlserv_pay;
                    ybentity.delpje = entity.hifob_pay;
                    ybentity.dbzddyljgdz = entity.hosp_part_amt;
                    ybentity.zhye = entity.balc;
                    ybentity.pch = entity.pch;
                    ybentity.yllb = entity.yllb;
                    _cqybSett05Repo.SaveCqybSett(ybentity, null);
                } catch (Exception e)
                { }
            }
           
		    return Success();
	    }
		public ActionResult GetCqyb10Data(string zymzh,string type)
	    {
			var data= _outPatChargeApp.GetCqyb10Data(zymzh,type);
		    return Success(null, data);
		}

        public ActionResult GetCqybMxCxData(string zymzh, string type,string ybver)
        {
            var data = _outPatChargeApp.GetCqybMxCxData(zymzh, type,ybver);
            return Success(null, data);
        }
        
        /// <summary>
        /// Lấy sau khi hoàn phí，Dữ liệu cần thiết để tải lên bảo hiểm y tế Trùng Khánh
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult GetCQDetailsMzjsYbTfh(string mzh, string jsnm, Dictionary<string, decimal> tjsxmDict)
	    {
			decimal ybzje = Convert.ToDecimal(0.0000);
		    decimal zfzje = Convert.ToDecimal(0.0000);
		    decimal tfzje = Convert.ToDecimal(0.0000);
		    var ybUpList = _outPatChargeApp.GetCQDetailsMzjsYbTfh(mzh, jsnm, tjsxmDict, out ybzje, out zfzje,
			    out tfzje);
            var model = _outChargeDmnService.GetCQjzdjInfo(mzh, this.OrganizeId);
            //var entity =
            //   _cqybupdatemedicalInput03Repo.FindEntity(p => p.zymzh == mzh && p.zt == "1" && p.OrganizeId == this.OrganizeId);
            var data = new
		    {
			    ybzje = ybzje,
			    zfzje = zfzje,
				tfzje=tfzje,
                jzid=model.mdtrt_id,
                yllb=model.med_type,
                rybh=model.psn_no,
                cfh = ybUpList.cflist.Count > 0 ? ybUpList.cflist[0].cfh : ""
            };
		    return Content(data.ToJson());
		}

	    public ActionResult ZFToYBForm(string mzh)
	    {
		    return View();
	    }

	    public ActionResult ZFToYB_Step_1(string mzh)
	    {
			System.Threading.Thread.Sleep(1000 * 3);
		    var data = _outPatChargeApp.ZFToYB_Step_1(mzh);
		    return Success(null, data);
		}

	    public ActionResult ZFToYB_Step_3(string mzh, string sbbh, string xm)
	    {
		    var data = _outPatChargeApp.ZFToYB_Step_3(mzh, sbbh, xm);
		    return Success(null, data);
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="zyh"></param>
	    /// <param name="patid"></param>
	    /// <param name="cardInfo"></param>
	    /// <param name="ryblInfo">Thông tin thẻ bảo hiểm y tế</param>
	    /// <returns></returns>
	    public ActionResult ZFToYB_Step_6(string mzh, int patid, ZYToYBDto patCardInfo)
	    {
		    var data = _outPatChargeApp.ZFToYB_Step_6(mzh, patid, patCardInfo);
		    return Success(null, data);
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="zyh"></param>
	    /// <returns></returns>
	    public ActionResult ZFToYB_Step_8(string mzh)
	    {
		    var data = _outPatChargeApp.ZFToYB_Step_8(mzh);
		    return Success(null, data);
	    }

        public ActionResult SaveCqS23(CqybSett23Entity entity)
        {
            entity.OrganizeId = this.OrganizeId;
            _cqybSett23Repo.SaveCqybS23Sett(entity,"");
            return Success();
        }

        public ActionResult GetMzbzml(string mllx)
        {
            var data = _outChargeDmnService.GetMzbzml(mllx);
            return Content(data.ToJson());
        }
        #endregion

        /// <summary>
        /// Quyết toán thu phí ngoại trú
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="feeRelated">Số tiền và thông tin thanh toán</param>
        /// <param name="ybfeeRelated">Thông tin chi phí liên quan đến bảo hiểm y tế</param>
        /// <param name="isXnhjyjz">"1"-Quyết toán bảo hiểm y tế nông thôn mới Khác-Quyết toán không phải bảo hiểm y tế nông thôn mới</param>
        /// <param name="xmList">Trong kho chưa có</param>
        /// <param name="cfnmList"></param>
        /// <param name="extxmnmList">Lịch sử đã gửi（Trong kho đã có）</param>
        /// <param name="outTradeNo">Mã giao dịch thanh toán</param>
        /// <returns></returns>
        public ActionResult SubmitForm(BasicInfoDto2018 bacDto
            , OutpatientSettFeeRelatedDTO feeRelated
            , CQMzjs05Dto ybfeeRelated
            , string isXnhjyjz
            , List<OutGridInfoDto2018> xmList, IList<int> cfnmList, IList<int> extxmnmList
            , string outTradeNo,List<OutGridInfoDto2018> deletelist)
        {
            var accDto = _outPatChargeApp.togetherData(xmList);
            if (!(accDto != null || (cfnmList != null && cfnmList.Count > 0) || (extxmnmList != null && extxmnmList.Count > 0)))
            {
                return Error("收费失败，缺少计费明细");
            }
            IList<int> jsnmList;
            if (ybfeeRelated != null && !ybfeeRelated.IsNull())
            {
                var medicalInsurance = _sysConfigRepo.GetValueByCode("Outpatient_MedicalInsurance",
                    this.OrganizeId);
                if (!string.IsNullOrWhiteSpace(medicalInsurance))
                {
                    ybfeeRelated.ybdlx = medicalInsurance;
                }
                else
                {
                    return Error("缺少医保地参数配置：Outpatient_MedicalInsurance");
                }
            }

            #region 新农合结算

            var xnhybfeeRelated = new S25ResponseDTO();
            var outpId = "";
            if ("1".Equals(isXnhjyjz ?? "0"))
            {
                var xnhSettResult = _guiAnOutpatientXnhApp.Sett(bacDto.mzh, OrganizeId, UserIdentity.UserCode, out xnhybfeeRelated, out outpId);
                if (!string.IsNullOrWhiteSpace(xnhSettResult)
                    && xnhSettResult.IndexOf("门诊补偿序号不能为空！", StringComparison.Ordinal) < 0
                    && xnhSettResult.IndexOf("门补偿序号未落地，请检查是否有门诊上传操作", StringComparison.Ordinal) < 0)
                {
                    return Error(xnhSettResult);
                }
            }
            #endregion

            try
            {
                if (deletelist!=null)
                {
                    _outPatientUniversalDmnService.UpdateYPcfxm(deletelist, this.OrganizeId, this.UserIdentity.UserCode);
                }
                int isOpenBill = -1; string openMsg = "";
                var resultnewjs = _outPatChargeApp.submitOutpatCharge(bacDto, feeRelated, ybfeeRelated, xnhybfeeRelated, accDto, OrganizeId, cfnmList, out jsnmList, extxmnmList, outTradeNo);
                if (!resultnewjs) return Success(bacDto.isQfyj ? "欠费预结操作成功" : "成功收费");
                //Sau khi đơn thuốc thu phí ngoại trú thu phí thành công có đồng bộ trạng thái thu phí sang hay không CIS
                //Ngay cả khi dự quyết toán nợ phí cũng phải đồng bộ sang，CIS của đơn thuốc không thể sửa được nữa
                if (cfnmList != null && cfnmList.Count > 0)
                {
                    var toCIS = _sysConfigRepo.GetBoolValueByCode("Outpatient_ChargeFee_AutoSyncPrescriptionStatus", this.OrganizeId) ?? false;
                    if (toCIS)
                    {
                        var cfList = cfnmList.Distinct()
                            .Select(p =>
                            {
                                var cfzbEntity = _outpatientPrescriptionRepo.GetValidEntityByCfnm(this.OrganizeId, p);
                                return cfzbEntity != null && cfzbEntity.cfly == "1" ? cfzbEntity.cfh : null;
                            })
                            .Where(p => !string.IsNullOrWhiteSpace(p))
                            .Select(p => new PrescriptionChargeStatusUpdateRequestDTO
                            {
                                cfh = p
                            }).ToList();
                        if (cfList.Count > 0)
                        {
                            var reqObj = new
                            {
                                cfList = cfList,
                                sfbz = true,
                            };
                            var apiResp = SiteCISAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                                "api/Prescription/UpdateChargeStatus", reqObj);
                            if (apiResp != null)
                            {
                                AppLogger.Info(string.Format("处方收费状态更新API同步至CIS，处方号：{0}，结果：{1}、{2}", string.Join(",", cfnmList), apiResp.code, apiResp.sub_code));
                            }
                            else
                            {
                                AppLogger.Info(string.Format("处方收费状态更新API同步至CIS，处方号：{0}，结果：未获取到响应，同步失败", string.Join(",", cfnmList)));
                            }
                        }
                    }
                }

                // ||false ||Thanh toán khác（Bảo hiểm y tế có thể thanh toán、Ghi sổ v.v.）>0
                if (bacDto.isQfyj || ((feeRelated == null || !(feeRelated.zje > 0))) || jsnmList.Count != 1) return Success(bacDto.isQfyj ? "欠费预结操作成功" : "成功收费");
                var res = new
                {
                    jsnm = jsnmList[0]
                }; 
                //Đẩy bất đồng bộ thông báo đơn thuốc thu phí thành công tới nhà thuốc/kho thuốc
                notifyPDS(jsnmList[0], bacDto.sfrq ?? DateTime.Now, cfnmList, bacDto.fph);
                _outpatientRegistRepo.RecordOutpId(bacDto.mzh, "", UserIdentity.UserCode, OrganizeId);
                
                  return Success(null, res);
            }
            catch (Exception e)
            {
                _guiAnOutpatientXnhApp.CancelSett(bacDto.mzh, OrganizeId, UserIdentity.UserCode);
                LogCore.Error("OutpatChargeController SubmitForm error", e);
                throw;
            }
        }
         
        ///// <summary>
        ///// Quyết toán ngoại trú bảo hiểm y tế nông thôn mới
        ///// </summary>
        ///// <param name="mzh"></param>
        ///// <param name="s25ResponseDto"></param>
        ///// <returns></returns>
        //public string XnhSett(string mzh, out S25ResponseDTO s25ResponseDto)
        //{
        //    return _guiAnOutpatientXnhApp.Sett(mzh, OrganizeId, out s25ResponseDto);
        //}

        /// <summary>
        /// Dự quyết toán nợ phí Sửa lại của View
        /// </summary>
        /// <returns></returns>
        public ActionResult ArrearageSettedUpdate()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult GetOutpatientSettArrearageJson(string mzh)
        {
            if (string.IsNullOrWhiteSpace(mzh))
            {
                return null;
            }
            var list = _outChargeDmnService.GetOutpatientSettArrearageVOList(mzh, OrganizeId);
            var data = list.Select(p => new
            {
                jsnm = p.jsnm,
                Text = p.CreateTime.ToString("yyyy-MM-dd ￥") + Math.Round(p.zje, 2).ToString()
            });
            return Content(data.ToJson());
        }

        /// <summary>
        /// Theo jsnm Lấy danh sách chi tiết quyết toán
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public ActionResult GetOutpatientSettDetailGridJson(int jsnm)
        {
            var list = _outChargeDmnService.GetOutpatientSettDetailList(jsnm, OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Sửa bản ghi dự quyết toán nợ phí của Ngày thu phí、Phương thức thanh toán tiền mặt v.v.
        /// </summary>
        /// <param name="updateDto"></param>
        /// <param name="feeRelated">Số tiền và thông tin thanh toán</param>
        /// <returns></returns>
        public ActionResult SubmitArrearageSettedUpdate(OutPatArrearageSettedUpdateDTO updateDto, OutpatientSettFeeRelatedDTO feeRelated)
        {
            _outPatientUniversalDmnService.UpdateArrearageSettlement(this.UserIdentity.OrganizeId, updateDto.jsnm, feeRelated, updateDto.sfrq, updateDto.fph);
            return Success("操作成功");
        }
        /// <summary>
        /// Chuyển quyết toán bảo hiểm y tế sang tự trả，Cập nhật tính chất bệnh nhân
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult UpdatePatBrxzInfo(string mzh)
        {
            //Tính chất bệnh nhân，Tạm thời lấy mặc định 0
            _outpatientRegistRepo.updatePatBrxzInfo(this.OrganizeId, mzh, "0");
            return Success("操作成功");
        }
        /// <summary>
        /// Đăng ký khám ngoại trú không thực hiện đăng ký bảo hiểm y tế Khi thu phí thực hiện đăng ký và cập nhật thẻ tự trả thành thẻ bảo hiểm y tế
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="kh"></param>
        /// <param name="cardtype"></param>
        /// <param name="brxz"></param>
        /// <returns></returns>
        public ActionResult UpdateChargeMzghInfo(string mzh,string kh,string cardtype,string brxz,string zjh,string status)
        {
            _outpatientRegistRepo.UpdateChargeMzghInfo(this.OrganizeId, mzh,kh,cardtype,brxz,zjh, status);
            return Success("操作成功");
        }
        /// <summary>
        /// Cập nhật khám bệnh bảo hiểm y tế đã đăng ký id Mã giao dịch
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="jzid"></param>
        /// <param name="jzpzlx"></param>
        /// <returns></returns>
        public ActionResult UpdateChargeMzghJzid(string mzh, string jzid, string jzpzlx)
        {
            _outpatientRegistRepo.updateYbghjzId(this.OrganizeId, mzh,jzid,jzpzlx);
            return Success("操作成功");
        }

        #endregion

        #region 生成处方号
        /// <summary>
        /// Tạo mã đơn thuốc mới
        /// </summary>
        /// <param name="type">Loại đơn thuốc 1 Thuốc 2 Không phải thuốc</param>
        /// <returns></returns>
        public ActionResult GeneratePresNo(int type = 0)
        {
            if (type <= 0)
            {
                return Error("请指定处方类型");
            }
            var cfh = _outpatientPrescriptionRepo.GeneratePresNo(this.OrganizeId, type);
            return Success(null, cfh);
        }
        #endregion

        #region private methods

        class PrescriptionChargeStatusUpdateRequestDTO
        {
            /// <summary>
            /// 
            /// </summary>
            public string cfh { get; set; }
        }

        /// <summary>
        /// Đẩy bất đồng bộ thông báo đơn thuốc thu phí thành công tới nhà thuốc/kho thuốc
        /// </summary>
        /// <param name="cfnmList"></param>
        private void notifyPDS(int jsnm, DateTime sfrq, IList<int> cfnmList, string Fph)
        {
            if (cfnmList == null || cfnmList.Count == 0)
            {
                return;
            }
            try
            {
                //cfnm cfh
                var ypCflx = (int)EnumPrescriptionType.Medicine;
                var ypcfList = _outpatientPrescriptionRepo.IQueryable(p => cfnmList.Contains(p.cfnm) && p.cflx == ypCflx).ToList();

                if (ypcfList.Count > 0)
                {
                    var creatorCode = this.UserIdentity.UserCode;
                    var context = System.Web.HttpContext.Current;
                    foreach (var cf in ypcfList)
                    {
                        Task.Run(() =>
                        {
                            var reqObj = new
                            {
                                Jsnm = jsnm,
                                Sfsj = sfrq,
                                Cfh = cf.cfh,
                                Cfnm = cf.cfnm,
                                Fph = Fph,
                                OrganizeId = OrganizeId,
                                CreatorCode = creatorCode,
                                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")//,
                                //Token = token
                            };
                            var apiResp = SitePDSAPIHelper.Request<APIRequestHelper.DefaultResponse>("api/ResourcesOperate/OutpatientCommit", reqObj, autoAppendToken: false,httpContext: context);

                            LogCore.Info("OutpatientCommit response", apiResp.ToJson());
                            AppLogger.Info(apiResp != null
                                ? string.Format("HIS收费后药品推送至药房，处方号：{0}，结果：{1}、{2}", reqObj.Cfh, apiResp.code,
                                    apiResp.sub_code)
                                : string.Format("HIS收费后药品推送至药房，处方号：{0}，结果：未获取到响应，同步失败", reqObj.Cfh));
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("HIS结算后推送到药房接口失败", ex);
            }
        }

        #endregion

        /// <summary>
        /// Lấy quyết toán ngoại trú/Hoàn phí Thông tin xuất viện của bệnh nhân（Mã quyết toán bảo hiểm y tế、Mã bảo hiểm xã hội、Lý do khám bệnh、Khoa、Bác sĩ、Chẩn đoán）
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult GetSettPatientInfo(string mzh)
        {
            System.Threading.Thread.Sleep(2 * 1000);
            var data = _outPatientUniversalDmnService.GetSettPatientInfo(mzh, this.OrganizeId);
            object tempData = data;
            //object tempData = new
            //{
            //    ybjsh = data.ybjsh,    //Mã quyết toán bảo hiểm y tế
            //    sbbh = "8222212",    //Mã bảo hiểm xã hội
            //    jzyy = "00", //Lý do khám bệnh
            //    ys = "000001",   //Mã bác sĩ xuất viện Đối chiếu sau
            //    ysmc = "Trương Vinh",   //Tên bác sĩ xuất viện Đối chiếu sau
            //    zdicd10 = "A09.901", //Chẩn đoán thứ nhất ICD10
            //    zdmc = "Viêm dạ dày ruột", //Tên chẩn đoán thứ nhất
            //    ks = "00000008", //Mã khoa khám bệnh Đối chiếu sau
            //    ksmc = "Khoa Y học cổ truyền",//Tên khoa khám bệnh Đối chiếu sau
            //};
            return Success(null, tempData);
        }

        /// <summary>
        /// Cập nhật số hóa đơn quyết toán
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public ActionResult UpdateSettedFph(int jsnm, string fph)
        {
            _outpatientSettlementRepo.UpdateSettedFph(jsnm, fph, this.OrganizeId);
            return Success();
        }

        /// <summary>
        /// Kiểm tra kết nối bảo hiểm y tế
        /// </summary>
        /// <returns></returns>
        public ActionResult YibaoConnectedTest()
        {
            var data = YibaoUniteInterface.QCatalog("1", "1311", "", "");
            return Success(null, data);
        }

        #region 门诊补计费（补至未收费状态）

        /// <summary>
        /// Tính phí bổ sung ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult AdditionalIndex2018()
        {
            ViewBag.sfxm_dj = _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId).Value;
            ViewBag.ChargeItemConfig = _sysConfigRepo.GetByCode("ChargeItemConfig", OrganizeId) == null ? "original" : _sysConfigRepo.GetByCode("ChargeItemConfig", OrganizeId).Value;
            ViewBag.ISMedicineSearchRelatedKC = _sysConfigRepo.GetBoolValueByCode("IS_MedicineSearchRelatedKC", OrganizeId);
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.isOpenQfyj = _sysConfigRepo.GetBoolValueByCode("Outpatient_Charge_Open_Qfyj", this.OrganizeId);
            return View();
        }

        /// <summary>
        /// Tính phí bổ sung ngoại trú Gửi
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="xmList">Trong kho chưa có</param>
        /// <returns></returns>
        public ActionResult AdditionalSubmit(BasicInfoDto2018 bacDto
            , List<OutGridInfoDto2018> xmList)
        {
            var accDto = _outPatChargeApp.togetherData(xmList);
            if (!(accDto != null))
            {
                return Error("失败，缺少计费明细");
            }

            _outPatChargeApp.submitOutpatAdditional(this.OrganizeId, bacDto, accDto);

            return Success("提交成功");
        }

        #endregion

        /// <summary>
        /// Quyết toán bảo hiểm y tế nông thôn mới Quý An
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public ActionResult GuiAnXnhSett(string mzh)
        {
            S24ResponseDTO s24;
            var result = _guiAnOutpatientXnhApp.SimulationSettWholeProcess(mzh, UserIdentity.UserCode, OrganizeId, out s24);
            return string.IsNullOrWhiteSpace(result) ? Success("", s24) : Error(result);
        }

        #region 体检/疫苗

        public ActionResult AddPhysicalexamForm()
        {
            return View();
        }

        /// <summary>
        /// Gửi vắc-xin khám sức khỏe Form
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitPhysicalexamForm(AddPhysicalexamDto dto)
        {
            var data = _iphysicalexamDmnService.SubmitPhysicalexamForm(dto, OrganizeId, this.UserIdentity.UserCode);
            return Success("", data.ToJson());
        }
        #endregion

        #region PACS
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //Luôn chấp nhận
        }

        public ActionResult pushApplicationform(BasicInfoDto2018 bacDto, IList<int> cfnmList)
        {
            var isopenpacs = _sysConfigRepo.GetValueByCode("ISOpenPacsSett", this.OrganizeId);
            if (isopenpacs != "ON")
            {
                return Success("无数据");
            }
            var uri = ConfigurationHelper.GetAppConfigValue("pacsUrl");
			List<CheckApplicationfromDTO> datalist = new List<CheckApplicationfromDTO>();
			if (cfnmList!=null)
			{
				foreach (var item in cfnmList)
				{
					var data = _outPatientUniversalDmnService.pushApplicationform(bacDto, item, this.OrganizeId, "Y");
					if (data != null)
					{
						datalist.Add(data);
					}

				}
			}
            
            if (datalist == null)
			{
				return Success("无数据");
			}
			var mesagssjson = "";
			var url = uri+"URISService/services/interface/requestorder";
			foreach (var data in datalist)
			{
				string datajson = Tools.Json.ToJson(data);
				try
				{
					System.Net.HttpWebRequest request = null;
					System.Net.WebResponse response = null;

					request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
					if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
					{
						ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
					}
					request.ProtocolVersion = System.Net.HttpVersion.Version10;
					request.Method = "POST";
					request.ContentType = "application/json";
					request.CookieContainer = null;//Lấy được khi lấy mã xác minh cookie Sẽ được gắn thêm vào container này
					request.AllowAutoRedirect = true;
					request.KeepAlive = true;//Thiết lập kết nối bền vững
											 //request.ContentLength = cs.Length;
					request.Host = "192.168.0.101";
					//request.UserAgent = "PostmanRuntime/7.29.2";
					request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
					request.Accept = "*/*";
                    request.Timeout = 5;
					byte[] datas = System.Text.Encoding.UTF8.GetBytes(datajson);
					using (System.IO.Stream stream = request.GetRequestStream())
					{
						stream.Write(datas, 0, datas.Length);
					}

					response = (System.Net.HttpWebResponse)request.GetResponse();
					string outputText = string.Empty;
					using (System.IO.Stream responseStm = response.GetResponseStream())
					{
						System.IO.StreamReader redStm = new System.IO.StreamReader(responseStm, System.Text.Encoding.UTF8);
						outputText = redStm.ReadToEnd();
					}
					var apiresp = JavaScriptJsonSerializerHelper.Deserialize<RefJson>(outputText);
					
					if (apiresp != null && apiresp.status != "Success")
					{
						mesagssjson = apiresp.errorCode;
						AppLogger.Info(string.Format("Pacs检查申请单入参：{0}，出参结果：{1}", datajson, outputText));
					}
					else if (apiresp != null && apiresp.status == "Success")
					{
						//pacs Sau khi đơn đăng ký thành công thì thay đổi mz_cf bảng sqdzt là 0
						_outPatientUniversalDmnService.pushApplicationformRef(data.Patient.Request.Reqno, this.OrganizeId, 0);
					}
					var refdatalist = new
					{
						mesagssjsosn = mesagssjson,
						jsonref = datajson
					};
				}
				catch (Exception e)
				{

					throw;
				}
			}
			return Success("");

		}

		#endregion

		/// <summary>
		/// Kê khoản mục vật tư trừ số lượng tồn kho Trừ số lượng đóng băng
		/// </summary>
		/// <returns></returns>
		public ActionResult Sumwzdj(string[] cfh)
		{
			var list = _outPatientUniversalDmnService.Sumwzdj(cfh, OrganizeId, UserIdentity.rygh);
			var data = new
			{
				row = list
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// Kê khoản mục vật tư đóng băng số lượng tồn kho
		/// </summary>
		/// <returns></returns>
		public ActionResult TuiHuiwzdj(string[] cfh)
		{
			var list = _outPatientUniversalDmnService.TuiHuiwzdj(cfh, OrganizeId, UserIdentity.rygh);
			var data = new
			{
				row = list
			};
			return Content(data.ToJson());
		}


        /// <summary>
        /// Lấy mã giao dịch đăng ký khám bệnh của bệnh nhân tại Tần Hoàng Đảo
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult GetQHDjzdjDataInfo(string zyh)
        {
            var medicalReg = _PatientBasicInfoDmnService.GetQHDjzdjInfo(zyh, OrganizeId);
            return Content(medicalReg.ToJson());
        }
    }
}