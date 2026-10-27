using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Domain.IDomainServices;
using Newtouch.CIS.Repository;
using Microsoft.AspNetCore.Mvc;

using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Application.Interface;

using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IDomainServices.Inpatient;
using Newtouch.Domain.IRepository.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    [Area("NurseManage")]
    public class WardMaintenanceController : OrgControllerBase
    {
        // GET: NurseManage/WardMaintenance
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IWardMaintenanceDmnService _WardMaintenanceDmnService;
        private readonly IPatientRyDiagnosisApp _patientRyDiagnosisApp;
		private readonly IInpatientBedCardRepo _InpatientBedCardRepo;
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
        private readonly ISysTCMSyndromeRepo _sysTcmsyndromeRepo;

        public WardMaintenanceController(ISysConfigRepo sysConfigRepo, IWardMaintenanceDmnService wardMaintenanceDmnService, IPatientRyDiagnosisApp patientRyDiagnosisApp, IInpatientBedCardRepo inpatientBedCardRepo, ISysDiagnosisRepo sysDiagnosisRepo, ISysTCMSyndromeRepo sysTcmsyndromeRepo)
        {
            this._sysConfigRepo = sysConfigRepo;
            this._WardMaintenanceDmnService = wardMaintenanceDmnService;
            this._patientRyDiagnosisApp = patientRyDiagnosisApp;
            this._InpatientBedCardRepo = inpatientBedCardRepo;
            this._sysDiagnosisRepo = sysDiagnosisRepo;
            this._sysTcmsyndromeRepo = sysTcmsyndromeRepo;
        }
        public IActionResult WardWardRoomBedMaintenance()
        {
            return View();
        }

        /// <summary>
        /// phònggiữaBảo trì
        /// </summary>
        /// <returns></returns>
        public IActionResult RoomForm()
        {
            return View();
        }

        /// <summary>
        /// kèmthêmThuộc tính
        /// </summary>
        /// <returns></returns>
        public IActionResult AttributesPlusBedForm()
        {
            return View();
        }
        public IActionResult DispensingQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }
        public IActionResult GetDispensingItems(Pagination pagination, string kssj, string jssj)
        {
            var reqdata = new DispensingMXRequestDto
            {
                kssj = kssj,
                jssj = jssj,
                OrganizeId = OrganizeId
            };
            var data = new
            {
                rows = _WardMaintenanceDmnService.GetDispensings(pagination, reqdata),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };

            return Content(data.ToJson());
        }
        public IActionResult GetDispensingItemsMX(Pagination pagination, string bysj)
        {
            var data = new
            {
                rows = _WardMaintenanceDmnService.GetDispensingMXs(pagination,bysj),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// Room Form Submit
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitRoomForm(string keyValue)
        {
            return null;
        }
        /// <summary>
        /// Lưu
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SavaBindItems(List<BedItemsVO> mxList, string bedCode,string zyh)
        {
            var saveItemsDto = new WardMaintenanceRequestDto
            {
                bedCode = bedCode,
                OrganizeId = OrganizeId
            };
            _WardMaintenanceDmnService.SaveBedItems(mxList, saveItemsDto, zyh);
            return Success("Lưu thành công.");
        }

        /// <summary>
        /// delete room
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteRoomForm(string keyValue)
        {
            return null;
        }

        public IActionResult BedForm(string bedNo)
        {
            return View();
        }

        /// <summary>
        /// delete bed
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteBedForm(string keyValue)
        {
            return null;
        }

        /// <summary>
        /// Phí giường bệnhgắn
        /// </summary>
        /// <returns></returns>
        public IActionResult BedBindingCostForm(string bedCode,string zyh)
        {
            ViewBag.bedCode = bedCode;
            ViewBag.zyh = zyh;
            return View();
        }

        /// <summary>
        /// LấyKhu bệnhChi phí  
        /// </summary>
        /// <param name="bedCode"></param>
        /// <returns></returns>
        public IActionResult GetBedItems(string bedCode)
        {
            var reqdata = new WardMaintenanceRequestDto
            {
                bedCode = bedCode,
                OrganizeId = OrganizeId
            };
            var xmList = _WardMaintenanceDmnService.GetBedItems(reqdata);

            return Content(xmList.ToJson());
        }

        public IActionResult OutAreaForm(string zyh)
        {
            ViewBag.zyh = zyh;
            ViewBag.isopenPriorReview = _sysConfigRepo.GetValueByCode("OpenPriorReview", OrganizeId);//CóKhôngbậtviệctrướcPhê duyệtgiao diện
            return View();
        }

        /// <summary>
        /// Bác sĩgắn
        /// </summary>
        /// <returns></returns>
        public IActionResult DoctorBindingForm(string zyh)
        {
            ViewBag.zyh = zyh;
            return View();
        }

		/// <summary>
		/// Nhập việngiườngthẻ
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public IActionResult InHospitalBedCard(string zyh)
		{
			ViewBag.zyh = zyh;
			return View();
		}

		/// <summary>
		/// Nhập việngiườngthẻIn
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public IActionResult InHospitalBedCardPrint(string zyh)
		{
			ViewBag.zyh = zyh;
			return View();
		}

		/// <summary>
		/// LấyKhu bệnhChi phí
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public IActionResult GetBedDocs(string zyh)
        {
            var reqdata = new patRequestDto
            {
                zyh = zyh,
                OrganizeId = OrganizeId
            };
            var xmList = _WardMaintenanceDmnService.GetBedDocs(reqdata);

            return Content(xmList.ToJson());
        }

        /// <summary>
        /// Lấyra khỏi khuChẩn đoán
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetPatDiagnosis(string zyh)
        {
            var reqdata = new patRequestDto
            {
                zyh = zyh,
                OrganizeId = OrganizeId
            };
            var zdList = _WardMaintenanceDmnService.GetPatDiagnosis(reqdata);

            return Content(zdList.ToJson());
        }

        /// <summary>
        /// LấyChẩn đoán nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetPatRyDiagnosis(string zyh)
        {
            var result = _patientRyDiagnosisApp.PatientRyDiagnosisQuery(zyh, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// trongChẩn đoán Tây y
        /// </summary>
        /// <returns></returns>
        public IActionResult GetryzdSelect(string ryzd, string ybnhlx,string zdlb=null)
        {
            if (string.IsNullOrEmpty(ybnhlx))
            {
                ybnhlx = null;
            }
            List<ZDSelect> zdlist = new List<ZDSelect>();
            IList<SysDiagnosisVEntity> data = new List<SysDiagnosisVEntity>();
            IList<SysTCMSyndromeVEntity> zyzhdata = new List<SysTCMSyndromeVEntity>();
            if (zdlb == "ZYZH")//trongyhội chứng
            {
                zyzhdata = _sysTcmsyndromeRepo.GetList(this.OrganizeId, ryzd);
            }
            else {
                data = _sysDiagnosisRepo.GetList(this.OrganizeId, ryzd, zdlb, ybnhlx);
            }
            foreach (SysTCMSyndromeVEntity item in zyzhdata)
            {
                ZDSelect a = new ZDSelect();
                a.zdbh = item.zhCode;
                a.icd10 = item.zhCode == null ? "" : item.zhCode;
                a.zdmc = item.zhmc;
                a.py = item.py;
                a.zdnm = int.TryParse(item.zhId, out var _zhId) ? _zhId : 0;
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
        /// Lưu
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SavaBindDocs(List<BedDocsVO> mxList, string zyh)
        {
            var saveDocsDto = new patRequestDto
            {
                zyh = zyh,
                OrganizeId = OrganizeId
            };
            _WardMaintenanceDmnService.SaveBedDocs(mxList, saveDocsDto);
            return Success("Lưu thành công.");
        }

        /// <summary>
        /// Bệnh nhânra khỏi khu
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <returns></returns>
        public IActionResult SavaPatDiagnosis(PatOutAreaInfoVO patOutAreaVO)
        {
            var saveDiagnosisDto = new patBedFeeRequestDto
            {
                zyh = patOutAreaVO.zyh,
                OrganizeId = OrganizeId,
                rq = patOutAreaVO.cqsj,
                user=UserIdentity.rygh
            };
            var isOutAreaMsg = _WardMaintenanceDmnService.GetPatIsOutArea(saveDiagnosisDto);
            if (isOutAreaMsg.Split('|')[0] == "F")
            {
                return Error(isOutAreaMsg.Split('|')[1]);
            }
            var patOutMsg = _WardMaintenanceDmnService.SavaPatDiagnosis(patOutAreaVO, saveDiagnosisDto);
            return patOutMsg.Split('|')[0] == "T" ? Success("Lưu thành công。") : Error(patOutMsg.Split('|')[1]);
        }

        public IActionResult AttributesPlusRoomForm(string keyWord)
        {
            return View();
        }

        /// <summary>
        /// ra khỏi khuChẩn đoán
        /// </summary>
        /// <returns></returns>
        public IActionResult OutAreaDiagnoosis()
        {
            return View();
        }

        /// <summary>
        /// ra khỏi khuChẩn đoán--Bệnh nhânra khỏi khu
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult InAreaDiagnoosisForm(string zyh,string brxz)
        {
            ViewBag.brxz = brxz;
            ViewBag.zyh = zyh;
            return View();
        }

        /// <summary>
        /// ra khỏi khuChẩn đoán--Bệnh nhânra khỏi khu
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult OutAreaDiagnoosisForm(string zyh, string brxz)
        {
            ViewBag.brxz = brxz;
            ViewBag.zyh = zyh;
            return View();
        }

        /// <summary>
        /// Lưu ra khỏi khuChẩn đoán--Bệnh nhânra khỏi khu
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <returns></returns>
        public IActionResult SavaOutPatDiagnosis(PatOutAreaVO patOutAreaVO)
        {
            var saveDiagnosisDto = new patBedFeeRequestDto
            {
                zyh = patOutAreaVO.zyh,
                OrganizeId = this.OrganizeId,
                //rq = patOutAreaVO.cqsj,
                rq = patOutAreaVO.zdrq
            };
            var patOutMsg = _WardMaintenanceDmnService.SavaOutPatDiagnosis(patOutAreaVO, saveDiagnosisDto);
            return patOutMsg.Split('|')[0] == "T" ? Success("Lưu thành công。") : Error(patOutMsg.Split('|')[1]);
        }

        /// <summary>
        /// LưuvàokhuChẩn đoán
        /// </summary>
        /// <param name="patInAreaVo"></param>
        /// <returns></returns>
        public IActionResult SavaInPatDiagnosis(PatInAreaVO patInAreaVo)
        {
            var result = _patientRyDiagnosisApp.SavaInPatDiagnosis(patInAreaVo, OrganizeId, UserIdentity.UserCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

		/// <summary>
		/// Lưura khỏi khuChẩn đoánList
		/// </summary>
		/// <param name="patOutAreaVO"></param>
		/// <returns></returns>
		public IActionResult SavaPatDxList(List<InpatientPatientDiagnosisEntity> PatDxList)
		{
			//var patOutMsg="";
			//foreach (var PatDxEntity in PatDxList) {

			//	patOutMsg=_WardMaintenanceDmnService.SavaPatDxEntity(PatDxEntity);
			//}
			var OrganizeId = this.OrganizeId;
			var patOutMsg = _WardMaintenanceDmnService.SavaPatDxList(PatDxList, this.OrganizeId);
			return patOutMsg.Split('|')[0] == "T" ? Success("Lưu thành công。") : Error(patOutMsg.Split('|')[1]);
		}


		/// <summary>
		/// LưuNhập việngiườngthẻ
		/// </summary>
		//[HttpPost]
		//[HandlerAjaxOnly]
		public IActionResult SavaBedCard(InpatientBedCardEntity entity, string zyh)
		{
			entity.OrganizeId = OrganizeId;
			entity.zyh = zyh;
			_InpatientBedCardRepo.SubmitForm(entity, zyh);
			return Success("Lưu thành công.");
		}

	}

}