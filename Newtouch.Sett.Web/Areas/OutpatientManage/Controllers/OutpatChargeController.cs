using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;







































namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("OutpatientManage")]

    public class OutpatChargeController : Newtouch.Sett.Web.Controllers.ControllerBase
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

        public OutpatChargeController(IOutPatChargeApp outPatChargeApp, IOutPatChargeDmnService outChargeDmnService, ISysConfigRepo sysConfigRepo, IOutpatientPrescriptionRepo outpatientPrescriptionRepo, IOutPatientUniversalDmnService outPatientUniversalDmnService, IOutpatientSettlementRepo outpatientSettlementRepo, IOutpatientRegistRepo outpatientRegistRepo, IGuiAnOutpatientXnhApp guiAnOutpatientXnhApp, ISysPatientBasicInfoRepo sysPatientBasicInfoRepo, IPhysicalexamDmnService iphysicalexamDmnService, ICqybUploadPresList04Repo iCqybUploadPresList04Repo, ICqybMedicalReg02Repo cqybMedicalReg02Repo, ICqybMedicalInPut02Repo cqybMedicalinput02Repo, ICqybSett05Repo cqybSett05Repo, ICqybSett23Repo cqybSett23Repo, ICqybUpdateMedicalInput03Repo cqybupdatemedicalInput03Repo, IPatientBasicInfoDmnService PatientBasicInfoDmnService, IOutPatientSettleDmnService outPatientSettleDmnService)
        {
            _outPatChargeApp = outPatChargeApp;
            _outChargeDmnService = outChargeDmnService;
            _sysConfigRepo = sysConfigRepo;
            _outpatientPrescriptionRepo = outpatientPrescriptionRepo;
            _outPatientUniversalDmnService = outPatientUniversalDmnService;
            _outpatientSettlementRepo = outpatientSettlementRepo;
            _outpatientRegistRepo = outpatientRegistRepo;
            _guiAnOutpatientXnhApp = guiAnOutpatientXnhApp;
            _sysPatientBasicInfoRepo = sysPatientBasicInfoRepo;
            _iphysicalexamDmnService = iphysicalexamDmnService;
            _iCqybUploadPresList04Repo = iCqybUploadPresList04Repo;
            _cqybMedicalReg02Repo = cqybMedicalReg02Repo;
            _cqybMedicalinput02Repo = cqybMedicalinput02Repo;
            _cqybSett05Repo = cqybSett05Repo;
            _cqybSett23Repo = cqybSett23Repo;
            _cqybupdatemedicalInput03Repo = cqybupdatemedicalInput03Repo;
            _PatientBasicInfoDmnService = PatientBasicInfoDmnService;
            _outPatientSettleDmnService = outPatientSettleDmnService;
        }

        #region  view
        /// <summary>
        /// thu phí ngoại trúgiao diện chính
        /// </summary>
        /// <returns></returns>
        public IActionResult ChargeIndex()
        {
            return View();
        }

        public IActionResult ConfirmForm()
        {
            return View();
        }
        public IActionResult SettConfirmForm()
        {
            return View();
        }

        public IActionResult ChargeConfirm()
        {
            return View();
        }
        /// <summary>
        /// thu phí
        /// </summary>
        /// <returns></returns>
        public IActionResult ChargeAddPay()
        {
            return View();
        }

        /// <summary>
        /// thanh toánquyết toánhộp bật xác nhận
        /// </summary>
        /// <returns></returns>
        public IActionResult ChargePayDialog()
        {
            return View();
        }
        /// <summary>
        /// cửa sổ mẫu
        /// </summary>
        /// <returns></returns>
        public IActionResult ItemTemplate()
        {
            return View();
        }

        /// <summary>
        /// tính chất bệnh nhâncửa sổ
        /// </summary>
        /// <returns></returns>
        public IActionResult ChargeBrxzDialog()
        {
            return View();
        }
        /// <summary>
        /// ngoại trúkhám chuyển tự chi trảBHYT
        /// </summary>
        /// <returns></returns>
        public IActionResult dkfsxz_mz()
        {
            return View();
        }
        #endregion

        #region Thu phí ngoại trú
        /// <summary>
        /// thu phí ngoại trútra cứuthông tin cá nhân
        /// </summary>
        /// <param name="kh">số thẻ</param>
        /// <param name="brxz"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ChargeIndex(string kh, string brxz)
        {
            var patChargeDto = new OutPatChargeDto();
            var patChargeInfo = _outPatChargeApp.GetOutPatChargeInfo(kh, brxz);

            patChargeDto.patInfo = patChargeInfo;
            if (patChargeInfo == null)
            {
                return Success("", patChargeDto);
            }
            //xác định làkhôngcónhiềutính chất bệnh nhân
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
        /// lấyđăng ký khámthông tin
        /// </summary>
        /// <param name="patid">bệnh nhânpatid</param>
        /// <param name="brxzbh">mã tính chất bệnh nhân</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getGHSource(int patid, string brxz)
        {
            var docVo = new OutPatChargeDoctorVO();
            List<OutPatChargeItemVO> ghItemList = _outPatChargeApp.GetOutGhInfo(patid, brxz, out docVo);
            return Success("", ghItemList);
        }

        /// <summary>
        /// lấythuốcvàkhoản thu phí
        /// </summary>
        /// <param name="keyword">điều kiện tra cứu từ khóa</param>
        /// <returns></returns>
        [HttpPost]
                public IActionResult GetYPItemInfo(string keyword)
        {
            return keyword == null ? null : Json(_outPatChargeApp.GetYpItemInfo(keyword.Trim().Replace("'", "").Replace(",", "").Replace("%", "")));
        }

        /// <summary>
        /// lấythông tin phí dịch vụ
        /// </summary>
        /// <param name="sfxmtype">loại khoản mục xác định làthuốchay làkhoản mục</param>
        /// <param name="lscfh">số đơn thuốc tạm thời</param>
        /// <param name="brxz">tính chất bệnh nhân</param>
        /// <param name="dl">nhóm lớn</param>
        /// <param name="sfxm">khoản thu phí</param>
        /// <param name="dj">đơn giá</param>
        /// <param name="sl">số lượng</param>
        /// <returns></returns>
        [HttpPost]
                public IActionResult GetCfAndSfInfo(string sfxmtype, string lscfh, string brxz, string dl, string sfxm, string dj, string sl)
        {
            return Success("", _outPatChargeApp.GetCfhAndFjfMoney(sfxmtype, lscfh, brxz, dl, sfxm, dj, sl));
        }


        /// <summary>
        /// lấysố đơn thuốc
        /// </summary>
        /// <param name="sfxmtype">loại khoản mục xác định làthuốchay làkhoản mục</param> 
        /// <returns></returns>
        [HttpPost]
                public IActionResult GetCFH(string sfxmtype)
        {
            string cfh = _outPatChargeApp.GetCfh(sfxmtype);
            return Success("", cfh);
        }

        /// <summary>
        /// lấykhoản thu phímẫu
        /// </summary>
        /// <param name="ks">khoa</param>
        /// <returns></returns>
        [HttpPost]
                public IActionResult GetChargeItemData(string ks)
        {
            var data = _outPatChargeApp.GetChargeTemplateAll(ks);
            return Success("", data);
        }


        /// <summary>
        /// thêm thông tin đơn thuốc，quyết toán
        /// </summary>
        /// <param name="chargeDto">thông tin danh sách đơn thuốc</param>
        /// <param name="kh">số thẻ</param>
        /// <param name="fph">số hóa đơn</param>
        /// <param name="xjzffs">phương thức thanh toán tiền mặt</param>
        /// <param name="xjzffsbh"></param>
        /// <param name="zfje">số tiền thanh toán-tổng số tiền bệnh nhân thanh toán</param>
        /// <param name="ysk">số tiền phải thu-phảinàysố tiền thanh toán</param>
        /// <param name="zl">tiền thừa</param>
        /// <returns></returns>
        [HttpPost]
                public IActionResult AddCfInfo(List<OutpatChargeDto> chargeDto, string kh, string fph, string xjzffs, string xjzffsbh, string zfje, string ysk, string zl)
        {
            var settDto = _outPatChargeApp.OutPatChargeSett(chargeDto, kh, fph, xjzffs, xjzffsbh, Convert.ToDecimal(zfje), Convert.ToDecimal(ysk), Convert.ToDecimal(zl));
            return Success("", settDto);
        }

        #endregion

        #region Thu phí2018bản

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index2018()
        {
            ViewBag.sfxm_dj = _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId).Value;
            ViewBag.ChargeItemConfig = _sysConfigRepo.GetByCode("ChargeItemConfig", OrganizeId) == null ? "original" : _sysConfigRepo.GetByCode("ChargeItemConfig", OrganizeId).Value;
            ViewBag.ISMedicineSearchRelatedKC = _sysConfigRepo.GetBoolValueByCode("IS_MedicineSearchRelatedKC", OrganizeId);
            ViewBag.ReportServerHOST = "";
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.isOpenQfyj = _sysConfigRepo.GetBoolValueByCode("Outpatient_Charge_Open_Qfyj", this.OrganizeId);
            ViewBag.Xzqh = _outPatientSettleDmnService.GetOrgXzqh(this.OrganizeId);
            return View();
        }

        /// <summary>
        /// chọn bản ghi khám
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatAccListView()
        {
            return View();
        }

        /// <summary>
        /// lấybản ghi lịch sử ghi sổ
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public IActionResult GetpatientAccountList(DateTime kssj, DateTime jssj, string kh,string zjh, string cardType)
        {
            return Content(_outChargeDmnService.GetpatientAccountList(kssj, jssj, kh,zjh, cardType, OrganizeId).ToJson());
        }

        /// <summary>
        /// lấybản ghi lịch sử ghi sổ
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <param name="isTF">làkhônglà từ hoàn phí</param>
        /// <returns></returns>
        public IActionResult GetpatientAccountInfo(string mzh, string kh,string zjh, string cardType, bool? isTF)
        {
            if (string.IsNullOrWhiteSpace(mzh) && (string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(cardType))&& string.IsNullOrWhiteSpace(zjh))
            {
                throw new FailedException("Thiếu tham số tra cứu: Số khám ngoại trú hoặc Số thẻ");
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

            if (resultDto.OutpatAccListDto.Count < 1) return Error("Tra cứu thất bại");
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                return Error("Tra cứu bất thường, số khám ngoại trú tra cứu ra nhiều bản ghi khám");
            }

            if ((string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(cardType)) && string.IsNullOrWhiteSpace(zjh)) return Error("Tra cứu thất bại");
            //tra bằng thẻ，tra đang khámhoặcchờ khámcủabản ghi
            var theInfo = resultDto.OutpatAccListDto.OrderByDescending(p => p.ghrq).FirstOrDefault();
            //if (!(isTF == true) && theInfo.ghzt == "0")
            if (isTF != true)  //nếu không phải hoàn phí，thì trả về thông tin khám lần cuối cùng
            {
                //chưa quyết toántrạng thái
                resultDto.OutpatAccInfoDto = theInfo;
                return Success(null, resultDto);
            }

            if (theInfo.ghzt != "1") return Error("Tra cứu bất thường, không thể xác định bản ghi khám chờ thu phí duy nhất");
            //đã quyết toántrạng thái
            resultDto.OutpatAccInfoDto = theInfo;
            return Success(null, resultDto);
        }
        public IActionResult GetpatientAccountInfobykh(string mzh, string kh, string zjh, string cardType, bool? isTF)
        {
            if (string.IsNullOrWhiteSpace(mzh) && (string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(cardType)) && string.IsNullOrWhiteSpace(zjh))
            {
                throw new FailedException("Thiếu tham số tra cứu: Số khám ngoại trú hoặc Số thẻ");
            }
            var OutpatAccListDtoList = _outChargeDmnService.GetOutpatChargePatInfoInAcc(mzh, kh, zjh, cardType, this.OrganizeId);
            var datadsf = _outChargeDmnService.GetPatientGridJson(OrganizeId);//lấy số khám ngoại trú chờ thu phí để lọc thông tin bệnh nhân
            var data = JsonConvert.DeserializeObject<List<OutPatientRegVO>>(datadsf) ?? new List<OutPatientRegVO>();
            List<OutpatAccInfoDto> patlist = new List<OutpatAccInfoDto>();
            foreach (var item in data)
            {
                var mhzlist = OutpatAccListDtoList.Where(p => p.mzh == item.mzh).FirstOrDefault();
                if (mhzlist!=null)
                {
                    patlist.Add(mhzlist);
                }
            }
            if (patlist.Count < 1) return Error("Tra cứu thất bại");
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
        public IActionResult ChoosePrescription()
        {
            return View();
        }
        public IActionResult ChooseMzh()
        {
            return View();
        }
        public IActionResult GetPatientGridJson()
        {
            var data = _outChargeDmnService.GetPatientGridJson(OrganizeId);
            return Content(data);
        }

        /// <summary>
        /// theo mã cá nhân hợp tác nông thôn mớilấynhấtsaumột lần chờquyết toán
        /// </summary>
        /// <param name="xnhgrbm">mã cá nhân hợp tác nông thôn mới</param>
        /// <returns></returns>
        public IActionResult GetLastSettleRecord(string xnhgrbm)
        {
            var result = new OutPatientRegVO
            {
                xnhgrbm = xnhgrbm
            };
            var tdata = _outChargeDmnService.GetPatientGridJson(OrganizeId);
            var data = JsonConvert.DeserializeObject<List<OutPatientRegVO>>(tdata) ?? new List<OutPatientRegVO>();
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
        /// lấybệnh nhânđăng ký khámsố thẻ
        /// </summary>
        /// <param name="xnhgrbm"></param>
        /// <returns></returns>
        //public IActionResult GetPatientkh(string xnhgrbm)
       // {
            //var brxx = _sysPatientBasicInfoRepo.FindEntity(p => p.xnhgrbm == xnhgrbm
            //                                                      && p.OrganizeId == OrganizeId
            //                                                      && p.zt == "1") ?? new SysPatientBasicInfoEntity();
            //var ghxx = _outpatientRegistRepo.IQueryable(p =>
            //    p.blh == brxx.blh && p.OrganizeId == brxx.OrganizeId
            //                      && p.ghzt == "1" && p.zt == "1").OrderByDescending(o => o.CreateTime).FirstOrDefault();
            //return Content(new { kh = ghxx.kh, cardType = ghxx.CardType }.ToJson());
        //}

        public IActionResult GetUnSettedPrescriptionBymzh(string mzh)
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

        public IActionResult GetNewUnSettedPrescriptionByMzh(string mzh)
        {
            var data = _outChargeDmnService.GetNewUnSettedPrescriptionByMzh(mzh, OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult ChooseCf()
        {
            return View();
        }

        public IActionResult GetNewAllUnSettedListByMzh(string mzh, string cfnms)
        {
            var data = _outChargeDmnService.GetNewAllUnSettedListByMzh(mzh, cfnms, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// tảichờ thu phí（chi tiết đơn thuốc）
        /// </summary>
        /// <param name="cfnmList"></param>
        /// <returns></returns>
        public IActionResult loadPrescriptionDetailDataBycfnm(IList<int> cfnmList)
        {
            var data = _outChargeDmnService.GetPrescriptionDetailBycfnm(cfnmList, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// tảichờ thu phí（chi tiết đơn thuốc + ngoài đơn thuốckhoản mụcchi tiết）
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetAllUnSettedListByMzh(string mzh)
        {
            var data = _outChargeDmnService.GetAllUnSettedListListByMzh(mzh, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// chi tiết chưa quyết toántải lên
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult DetailsUploadYb(string mzh)
        {
            _outPatChargeApp.DetailsUploadYb(mzh);
            return Success();
        }
        /// <summary>
        /// Quý AnBHYTquyết toántải lêntrích xuất chi tiết
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetGuiAnDetailsMzjsYb(string mzh)
        {
            guianMainAllOfMzjs guianMainOfMzjsModel = _outPatChargeApp.GetGuianMainOfMzjs(mzh);
            return Content(guianMainOfMzjsModel.ToJson());
        }
        /// <summary>
        /// lấyhoàn phísau，Quý AnBHYTtải lêncần thiếtdữ liệu
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetGuiAnDetailsMzjsYbTfh(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh)
        {
            guianMainAllOfMzjs guianMainOfMzjsModel = _outPatChargeApp.GetGuiAnDetailsMzjsYbTfh(jsnm, tjsxmDict, mzh);
            return Content(guianMainOfMzjsModel.ToJson());
        }

		#region BHYT Trùng Khánh
		/// <summary>
		/// ngoại trúđăng ký khám bệnhsửalấy
		/// </summary>
		/// <param name="mzh"></param>
		/// <returns></returns>
		public IActionResult GetCQjzdjInfo(string mzh)
		{
			Input_2203A model = _outChargeDmnService.GetCQjzdjInfo(mzh, this.OrganizeId);
			return Content(model.ToJson());
		}
	    /// <summary>
	    /// lấyđăng ký khám bệnh nhâncủagiao dịchsố giao dịch
	    /// </summary>
	    /// <param name="mzh"></param>
	    /// <returns></returns>
	    public IActionResult GetCQjzdjDataInfo(string zymzh)
	    {
            
		    CqybMedicalReg02Entity entity =
			    _cqybMedicalReg02Repo.FindEntity(p => p.zymzh == zymzh && p.zt == "1" && p.OrganizeId == this.OrganizeId);
			return Content(entity.ToJson());
		}
		/// <summary>
		/// lấyngoại trúkhámquyết toánchi tiết lúc đótải lênchi tiết（ybzjetổng số tiền，zfzjelànơicósố tiền tự chi trả）
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="mzh"></param>
		/// <returns></returns>
		public IActionResult GetChongQingDetailsMzjsYb(string mzh, string cfnm)
		{
            if (string.IsNullOrWhiteSpace(cfnm))
            {
                return Error("Tham số mã nội bộ đơn thuốc (cfnm) chưa được truyền hoặc giá trị trống");
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
	    
		public IActionResult SaveChongQingUploadPrescriptions(List<CqybUploadPresList04Entity> entityList, List<UploadPrescriptionsListInPut> cflist,string zymzh,string jytype)
	    {
		    _iCqybUploadPresList04Repo.SaveCqybUploadPresList(entityList, jytype, zymzh,this.OrganizeId);
			//tải lêncủađơn thuốc cũngghi nhận，lưu số đơn thuốc，làđã hoàn đơn
		    _outPatChargeApp.SaveCqybUploadInPres(cflist, zymzh, jytype);
            return Success();
	    }
        public IActionResult SaveUploadPch(string zymzh, string jytype, string cfh, string pch)
        {
            _outPatChargeApp.SaveCqybUploadInPres(zymzh, jytype, cfh, pch);
            return Success();
        }

        public IActionResult SaveCqybUpdateMedicalReg(CqybUpdateMedicalInput03Entity entity)
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
        /// hủy đơn thuốcsau，hủy04tải lênghi nhận
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="cflist"></param>
        /// <param name="zymzh"></param>
        /// <param name="jytype"></param>
        /// <param name="cfh"></param>
        /// <param name="yynm"></param>
        /// <returns></returns>
        public IActionResult UpDateUploadPrescriptions(string zymzh, string cfh)
        {
            _outPatChargeApp.UpdateCqyb04InPres(zymzh, cfh, this.OrganizeId);
            return Success();
        }
        /// <summary>
        /// hủy theo số lô
        /// </summary>
        /// <param name="zymzh"></param>
        /// <param name="pch"></param>
        /// <returns></returns>
        public IActionResult UpDateUploadPch(string zymzh, string cfh, string pch)
        {
            if (string.IsNullOrWhiteSpace(cfh))
            {
                return Error("Tham số số đơn thuốc (cfh) chưa được truyền hoặc giá trị trống");
            }

            _outPatChargeApp.UpDateUploadPch(zymzh, cfh, pch, this.OrganizeId);
            return Success();
        }

        /// <summary>
        /// lưuTrùng Khánhquyết toánđảo đỏ99trả vềdữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="jslb"></param>
        /// <returns></returns>
        // ChongQing 99 VO carries money/count fields as strings; HOS-typed entity needs parsing.
        private static int ParseInt(string s) { int v; return int.TryParse(s, out v) ? v : 0; }
        private static decimal? ParseDec(string s) { decimal v; return decimal.TryParse(s, out v) ? (decimal?)v : null; }

        public IActionResult SaveChongQing99HQ(Output_2207VO entity,string jslb)
	    {
            if (entity!=null)
            {
                try {
                    CqybSett05Entity ybentity = new CqybSett05Entity();
                    ybentity.jylsh = entity.setl_id;
                    ybentity.OrganizeId = this.OrganizeId;
                    ybentity.jslb = jslb;
                    ybentity.jsnm = ParseInt(entity.jsnm);
                    ybentity.cqtczf = ParseDec(entity.hifp_pay);
                    ybentity.zhzf = ParseDec(entity.acct_pay);
                    ybentity.cqxjzf = ParseDec(entity.psn_cash_pay);
                    ybentity.gwybz = ParseDec(entity.cvlserv_pay);
                    ybentity.delpje = ParseDec(entity.hifob_pay);
                    ybentity.dbzddyljgdz = ParseDec(entity.hosp_part_amt);
                    ybentity.zhye = ParseDec(entity.balc);
                    ybentity.pch = entity.pch;
                    ybentity.yllb = entity.yllb;
                    _cqybSett05Repo.SaveCqybSett(ybentity, null);
                } catch (Exception e)
                { }
            }
           
		    return Success();
	    }
		public IActionResult GetCqyb10Data(string zymzh,string type)
	    {
			var data= _outPatChargeApp.GetCqyb10Data(zymzh,type);
		    return Success(null, data);
		}

        public IActionResult GetCqybMxCxData(string zymzh, string type,string ybver)
        {
            var data = _outPatChargeApp.GetCqybMxCxData(zymzh, type,ybver);
            return Success(null, data);
        }
        
        /// <summary>
        /// lấyhoàn phísau，Trùng KhánhBHYTtải lêncần thiếtdữ liệu
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetCQDetailsMzjsYbTfh(string mzh, string jsnm, Dictionary<string, decimal> tjsxmDict)
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

	    public IActionResult ZFToYBForm(string mzh)
	    {
		    return View();
	    }

	    public IActionResult ZFToYB_Step_1(string mzh)
	    {
			System.Threading.Thread.Sleep(1000 * 3);
		    var data = _outPatChargeApp.ZFToYB_Step_1(mzh);
		    return Success(null, data);
		}

	    public IActionResult ZFToYB_Step_3(string mzh, string sbbh, string xm)
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
	    /// <param name="ryblInfo">BHYTthông tin thẻ</param>
	    /// <returns></returns>
	    public IActionResult ZFToYB_Step_6(string mzh, int patid, ZYToYBDto patCardInfo)
	    {
		    var data = _outPatChargeApp.ZFToYB_Step_6(mzh, patid, patCardInfo);
		    return Success(null, data);
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="zyh"></param>
	    /// <returns></returns>
	    public IActionResult ZFToYB_Step_8(string mzh)
	    {
		    var data = _outPatChargeApp.ZFToYB_Step_8(mzh);
		    return Success(null, data);
	    }

        public IActionResult SaveCqS23(CqybSett23Entity entity)
        {
            entity.OrganizeId = this.OrganizeId;
            _cqybSett23Repo.SaveCqybS23Sett(entity,"");
            return Success();
        }

        public IActionResult GetMzbzml(string mllx)
        {
            var data = _outChargeDmnService.GetMzbzml(mllx);
            return Content(data.ToJson());
        }
        #endregion

        /// <summary>
        /// thu phí ngoại trúquyết toán
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="feeRelated">số tiền và thông tin thanh toán</param>
        /// <param name="ybfeeRelated">BHYTthông tin chi phí liên quan</param>
        /// <param name="isXnhjyjz">"1"-hợp tác nông thôn mớiquyết toán khác-không phải hợp tác nông thôn mớiquyết toán</param>
        /// <param name="xmList">trong kho chưa có</param>
        /// <param name="cfnmList"></param>
        /// <param name="extxmnmList">lịch sử đã gửi（trong kho đãcó）</param>
        /// <param name="outTradeNo">số giao dịch thanh toán</param>
        /// <returns></returns>
        public IActionResult SubmitForm(BasicInfoDto2018 bacDto
            , OutpatientSettFeeRelatedDTO feeRelated
            , CQMzjs05Dto ybfeeRelated
            , string isXnhjyjz
            , List<OutGridInfoDto2018> xmList, IList<int> cfnmList, IList<int> extxmnmList
            , string outTradeNo,List<OutGridInfoDto2018> deletelist)
        {
            var accDto = _outPatChargeApp.togetherData(xmList);
            if (!(accDto != null || (cfnmList != null && cfnmList.Count > 0) || (extxmnmList != null && extxmnmList.Count > 0)))
            {
                return Error("Thu phíThất bại, thiếu chi tiết tính phí");
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
                    return Error("Thiếu cấu hình tham số địa phương BHYT: Outpatient_MedicalInsurance");
                }
            }

            #region Quyết toán hợp tác nông thôn

            var xnhybfeeRelated = new S25ResponseDTO();
            var outpId = "";
            if ("1".Equals(isXnhjyjz ?? "0"))
            {
                var xnhSettResult = _guiAnOutpatientXnhApp.Sett(bacDto.mzh, OrganizeId, UserIdentity.UserCode, out xnhybfeeRelated, out outpId);
                if (!string.IsNullOrWhiteSpace(xnhSettResult)
                    && xnhSettResult.IndexOf("门诊补偿序号不能为空！", StringComparison.Ordinal) < 0
                    && xnhSettResult.IndexOf("门诊补偿序号未落地，请检查是否有门诊上传操作", StringComparison.Ordinal) < 0)
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
                if (!resultnewjs) return Success(bacDto.isQfyj ? "Thao tác quyết toán trước nợ phí thành công" : "Thu phí thành công");
                //thu phí ngoại trúcủasau khi thu phí đơn thuốc thành côngsaulàkhôngđồng bộthu phítrạng tháiđếnCIS
                //dù là quyết toán trước nợ phí cũngcầnđồng bộđã qua，CIScủađơn thuốc không thể nữasửa
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
                                AppLogger.Info(string.Format("đơn thuốcThu phíTrạng tháiCập nhậtAPIđồng bộđếnCIS，Số đơn thuốc：{0}，kết quả：{1}、{2}", string.Join(",", cfnmList), apiResp.code, apiResp.sub_code));
                            }
                            else
                            {
                                AppLogger.Info(string.Format("đơn thuốcThu phíTrạng tháiCập nhậtAPIđồng bộđếnCIS，Số đơn thuốc：{0}，kết quả：ChưaLấyđến phản hồi，đồng bộThất bại", string.Join(",", cfnmList)));
                            }
                        }
                    }
                }

                // ||false ||thanh toán khác（BHYTcó thể báo、ghi sổ v.v.）>0
                if (bacDto.isQfyj || ((feeRelated == null || !(feeRelated.zje > 0))) || jsnmList.Count != 1) return Success(bacDto.isQfyj ? "Thao tác quyết toán trước nợ phí thành công" : "Thu phí thành công");
                var res = new
                {
                    jsnm = jsnmList[0]
                }; 
                //không đồng bộđẩythu phí đơn thuốc thành côngcủathông báo chonhà thuốckho thuốc
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
        ///// hợp tác nông thôn mớingoại trúkhámquyết toán
        ///// </summary>
        ///// <param name="mzh"></param>
        ///// <param name="s25ResponseDto"></param>
        ///// <returns></returns>
        //public string XnhSett(string mzh, out S25ResponseDTO s25ResponseDto)
        //{
        //    return _guiAnOutpatientXnhApp.Sett(mzh, OrganizeId, out s25ResponseDto);
        //}

        /// <summary>
        /// quyết toán trước nợ phí sửa lại của khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult ArrearageSettedUpdate()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetOutpatientSettArrearageJson(string mzh)
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
        /// theojsnm lấyquyết toáncủadanh sách chi tiết
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public IActionResult GetOutpatientSettDetailGridJson(int jsnm)
        {
            var list = _outChargeDmnService.GetOutpatientSettDetailList(jsnm, OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// sửabản ghi quyết toán trước nợ phí của thu phíngàykỳ、phương thức thanh toán tiền mặt v.v.
        /// </summary>
        /// <param name="updateDto"></param>
        /// <param name="feeRelated">số tiền và thông tin thanh toán</param>
        /// <returns></returns>
        public IActionResult SubmitArrearageSettedUpdate(OutPatArrearageSettedUpdateDTO updateDto, OutpatientSettFeeRelatedDTO feeRelated)
        {
            _outPatientUniversalDmnService.UpdateArrearageSettlement(this.UserIdentity.OrganizeId, updateDto.jsnm, feeRelated, updateDto.sfrq, updateDto.fph);
            return Success("Thao tác thành công");
        }
        /// <summary>
        /// BHYTquyết toánchuyển tự chi trả，cập nhật tính chất bệnh nhân
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult UpdatePatBrxzInfo(string mzh)
        {
            //tính chất bệnh nhân，tạm mặc định lấy0
            _outpatientRegistRepo.updatePatBrxzInfo(this.OrganizeId, mzh, "0");
            return Success("Thao tác thành công");
        }
        /// <summary>
        /// đăng ký khám ngoại trúkhông tiến hànhBHYTđăng ký khi thu phí tiến hành đăng ký và cập nhật thẻ tự chi trảlàBHYTthẻ
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="kh"></param>
        /// <param name="cardtype"></param>
        /// <param name="brxz"></param>
        /// <returns></returns>
        public IActionResult UpdateChargeMzghInfo(string mzh,string kh,string cardtype,string brxz,string zjh,string status)
        {
            _outpatientRegistRepo.UpdateChargeMzghInfo(this.OrganizeId, mzh,kh,cardtype,brxz,zjh, status);
            return Success("Thao tác thành công");
        }
        /// <summary>
        /// cập nhậtđăng ký khámBHYTkhámidsố giao dịch
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="jzid"></param>
        /// <param name="jzpzlx"></param>
        /// <returns></returns>
        public IActionResult UpdateChargeMzghJzid(string mzh, string jzid, string jzpzlx)
        {
            _outpatientRegistRepo.updateYbghjzId(this.OrganizeId, mzh,jzid,jzpzlx);
            return Success("Thao tác thành công");
        }

        #endregion

        #region Tạo số đơn thuốc
        /// <summary>
        /// tạo số đơn thuốc mới
        /// </summary>
        /// <param name="type">loại đơn thuốc1thuốc 2không phảithuốc</param>
        /// <returns></returns>
        public IActionResult GeneratePresNo(int type = 0)
        {
            if (type <= 0)
            {
                return Error("Vui lòng chỉ định loại đơn thuốc");
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
        /// không đồng bộđẩythu phí đơn thuốc thành côngcủathông báo chonhà thuốckho thuốc
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
                var ypcfList = _outpatientPrescriptionRepo.GetByCfnmList(cfnmList, ypCflx);

                if (ypcfList.Count > 0)
                {
                    var creatorCode = this.UserIdentity.UserCode;
                    var context = HttpContext;
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
                                ? string.Format("HISThu phísauThuốcđẩyđếnnhà thuốc，Số đơn thuốc：{0}，kết quả：{1}、{2}", reqObj.Cfh, apiResp.code,
                                    apiResp.sub_code)
                                : string.Format("HISThu phísauThuốcđẩyđếnnhà thuốc，Số đơn thuốc：{0}，kết quả：ChưaLấyđến phản hồi，đồng bộThất bại", reqObj.Cfh));
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("Đẩy sang giao diện nhà thuốc sau quyết toán HIS thất bại", ex);
            }
        }

        #endregion

        /// <summary>
        /// lấyngoại trúkhámquyết toán/hoàn phí thông tin xuất viện bệnh nhân（BHYTsố quyết toán、số BHXH、lý do khám、khoa、bác sĩ、chẩn đoán）
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetSettPatientInfo(string mzh)
        {
            System.Threading.Thread.Sleep(2 * 1000);
            var data = _outPatientUniversalDmnService.GetSettPatientInfo(mzh, this.OrganizeId);
            object tempData = data;
            //object tempData = new
            //{
            //    ybjsh = data.ybjsh,    //BHYTsố quyết toán
            //    sbbh = "8222212",    //số BHXH
            //    jzyy = "00", //lý do khám
            //    ys = "000001",   //xuất việnbác sĩmã saumặt đối chiếu
            //    ysmc = "Trương Vinh",   //xuất việnbác sĩhọ tên saumặt đối chiếu
            //    zdicd10 = "A09.901", //chẩn đoán đầu tiênICD10
            //    zdmc = "viêm dạ dày ruột", //tên chẩn đoán đầu tiên
            //    ks = "00000008", //khámkhoamã saumặt đối chiếu
            //    ksmc = "khoa Y học cổ truyền",//khámkhoatên saumặt đối chiếu
            //};
            return Success(null, tempData);
        }

        /// <summary>
        /// cập nhậtquyết toánsố hóa đơn
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public IActionResult UpdateSettedFph(int jsnm, string fph)
        {
            _outpatientSettlementRepo.UpdateSettedFph(jsnm, fph, this.OrganizeId);
            return Success();
        }

        /// <summary>
        /// BHYTkiểm tra kết nối
        /// </summary>
        /// <returns></returns>
        public IActionResult YibaoConnectedTest()
        {
            return Error("Kiểm tra kết nối bảo hiểm chưa được triển khai trong bản .NET Core này.");
        }

        #region Thu phí bổ sung ngoại trú（bổ sung về trạng thái chưa thu phí）

        /// <summary>
        /// ngoại trútính phí bù khám
        /// </summary>
        /// <returns></returns>
        public IActionResult AdditionalIndex2018()
        {
            ViewBag.sfxm_dj = _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId) == null ? "OFF" : _sysConfigRepo.GetByCode("sfxm_dj", OrganizeId).Value;
            ViewBag.ChargeItemConfig = _sysConfigRepo.GetByCode("ChargeItemConfig", OrganizeId) == null ? "original" : _sysConfigRepo.GetByCode("ChargeItemConfig", OrganizeId).Value;
            ViewBag.ISMedicineSearchRelatedKC = _sysConfigRepo.GetBoolValueByCode("IS_MedicineSearchRelatedKC", OrganizeId);
            ViewBag.ReportServerHOST = "";
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.isOpenQfyj = _sysConfigRepo.GetBoolValueByCode("Outpatient_Charge_Open_Qfyj", this.OrganizeId);
            return View();
        }

        /// <summary>
        /// ngoại trútính phí bù khám gửi
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="xmList">trong kho chưa có</param>
        /// <returns></returns>
        public IActionResult AdditionalSubmit(BasicInfoDto2018 bacDto
            , List<OutGridInfoDto2018> xmList)
        {
            var accDto = _outPatChargeApp.togetherData(xmList);
            if (!(accDto != null))
            {
                return Error("Thất bại, thiếu chi tiết tính phí");
            }

            _outPatChargeApp.submitOutpatAdditional(this.OrganizeId, bacDto, accDto);

            return Success("Gửi thành công");
        }

        #endregion

        /// <summary>
        /// hợp tác nông thôn mới Quý AnBHYTquyết toán
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GuiAnXnhSett(string mzh)
        {
            S24ResponseDTO s24;
            var result = _guiAnOutpatientXnhApp.SimulationSettWholeProcess(mzh, UserIdentity.UserCode, OrganizeId, out s24);
            return string.IsNullOrWhiteSpace(result) ? Success("", s24) : Error(result);
        }

        #region Khám sức khỏe/Vắc xin

        public IActionResult AddPhysicalexamForm()
        {
            return View();
        }

        /// <summary>
        /// gửi khám sức khỏe vắc xinForm
        /// </summary>
        /// <returns></returns>
        public IActionResult SubmitPhysicalexamForm(AddPhysicalexamDto dto)
        {
            var data = _iphysicalexamDmnService.SubmitPhysicalexamForm(dto, OrganizeId, this.UserIdentity.UserCode);
            return Success("", data.ToJson());
        }
        #endregion

        #region PACS
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //luôn chấp nhận
        }

        public IActionResult pushApplicationform(BasicInfoDto2018 bacDto, IList<int> cfnmList)
        { return Error("Đẩy đơn kiểm tra sang PACS chưa được triển khai trong bản .NET Core này."); }

		#endregion

		/// <summary>
		/// lập vật tưkhoản mụctrừ tồn khosố lượng trừ đóng băngsố lượng
		/// </summary>
		/// <returns></returns>
		public IActionResult Sumwzdj(string[] cfh)
		{
			var list = _outPatientUniversalDmnService.Sumwzdj(cfh, OrganizeId, UserIdentity.rygh);
			var data = new
			{
				row = list
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// lập vật tưkhoản mụcđóng băng tồn khosố lượng
		/// </summary>
		/// <returns></returns>
		public IActionResult TuiHuiwzdj(string[] cfh)
		{
			var list = _outPatientUniversalDmnService.TuiHuiwzdj(cfh, OrganizeId, UserIdentity.rygh);
			var data = new
			{
				row = list
			};
			return Content(data.ToJson());
		}


        /// <summary>
        /// lấyđăng ký khám bệnh nhâncủagiao dịchsố giao dịchTần Hoàng Đảo
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetQHDjzdjDataInfo(string zyh)
        {
            var medicalReg = _PatientBasicInfoDmnService.GetQHDjzdjInfo(zyh, OrganizeId);
            return Content(medicalReg.ToJson());
        }
    }
}
