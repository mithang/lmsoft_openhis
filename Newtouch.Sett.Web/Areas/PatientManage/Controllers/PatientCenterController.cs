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

    [Area("PatientManage")]

    public class PatientCenterController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IPatientCenterDmnService _patientCenterDmnService;
        private readonly IDischargeSettleApp _dischargeSettleApp;
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        public PatientCenterController(IPatientCenterDmnService patientCenterDmnService, IDischargeSettleApp dischargeSettleApp, IDischargeSettleDmnService dischargeSettleDmnService, IHospPatientBasicInfoRepo hospPatientBasicInfoRepo)
        {
            _patientCenterDmnService = patientCenterDmnService;
            _dischargeSettleApp = dischargeSettleApp;
            _dischargeSettleDmnService = dischargeSettleDmnService;
            _hospPatientBasicInfoRepo = hospPatientBasicInfoRepo;
        }

        // GET: PatientManage/PatientCenter
        public IActionResult SiteMap()
        {
            return View();
        }
        
        public IActionResult HospFeeInfo()
        {
            return View();
        }
        public IActionResult HospFeeInfoV2()
        {
            return View();
        }
        public IActionResult HospFeeItemShow()
        {
            return View();
        }
        public IActionResult HospFeeItemDetailShow()
        {
            return View();
        }

        public IActionResult HospAccountInfo()
        {
            return View();
        }
        public IActionResult HospMediInsureInfo()
        {
            return View();
        }
        public IActionResult HospSettInfo()
        {
            return View();
        }
        public IActionResult HospDiagInfo()
        {
            return View();
        }

        #region Thông tin bệnh nhân
        public IActionResult PatientChange()
        {
            return View();
        }
        public IActionResult PatSearchInfo(Pagination pagination, string keyword, string xm,string zyh,string mzh,string ywlx)
        {
            object json=null;
            var list = _patientCenterDmnService.PatientBasic(zyh, mzh, keyword, null, OrganizeId,ywlx);
            if (list != null && list.zyinfolist != null && list.zyinfolist.Count > 0)
            {
                json = list.zyinfolist;
            }
            else if (list != null && list.mzinfolist != null && list.mzinfolist.Count > 0)
            {
                json = list.mzinfolist;
            }
            else if (list != null && list.basic != null && list.basic.Count > 0)
            {
                json = list.basic;
            }
            
            var data = new
            {
                rows = json,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public IActionResult PatZyInfo(string zyh, string patid)
        {
            var patvo = _patientCenterDmnService.InHospitalHistory(patid, zyh, OrganizeId).FirstOrDefault();
            if (patvo != null && (patvo.zybz == ((int)EnumZYBZ.Bqz).ToString() || patvo.zybz == ((int)EnumZYBZ.Zq).ToString()))
            {
                patvo.zyts = DateTimeHelper.GetInHospDays(Convert.ToDateTime(patvo.rysj), DateTime.Now);
            }
            else if (patvo != null && (patvo.zybz == ((int)EnumZYBZ.Djz).ToString()|| patvo.zybz == ((int)EnumZYBZ.Ycy).ToString()))
            {
                patvo.zyts = DateTimeHelper.GetInHospDays(Convert.ToDateTime(patvo.rysj), Convert.ToDateTime(patvo.cqsj));
            }
            var data = _patientCenterDmnService.InHospitalDoctorInfo(zyh, OrganizeId, ref patvo);
            return Content(data.ToJson());
        }

        public IActionResult GetDefaultZyh(string zyh)
        {
            HospPatientBasicInfoEntity zybrjbxx = new HospPatientBasicInfoEntity();
            if (string.IsNullOrWhiteSpace(zyh))
            {
                zybrjbxx = _hospPatientBasicInfoRepo.FindEntity(p => p.zybz == ((int)EnumZYBZ.Bqz).ToString() && p.zt == "1" && p.OrganizeId == OrganizeId);
            }
            else
            {
                zybrjbxx = _hospPatientBasicInfoRepo.FindEntity(p => p.zyh == zyh && p.zt == "1" && p.OrganizeId == OrganizeId);
            }
            //zybrjbxx.zyh = "01309";
            //zybrjbxx.patid = 1027462;
            return Content(zybrjbxx.ToJson());
        }

        public IActionResult PatZySettInfo(string zyh,string jsxz)
        {
            var data = _patientCenterDmnService.InHospitalSett(zyh, jsxz, OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult PatZySettInfobyJsnm(string jsnm, string settid)
        {
            var data = _patientCenterDmnService.InHospitalSettbyJsnm(jsnm, settid, OrganizeId);
            return Content(data.ToJson());
        }
        #endregion


        #region Hóa đơn chi phí


        public IActionResult GetGoldCollectList(string zyh)
        {

            var resultDto = new InpatientSettPatStatusDetailDto()
            { 
                InpatientSettPatInfo = null,
                ////InpatientSettleItemBO = settleItemsBo
                GroupFeeVOList = _dischargeSettleDmnService.GetHospGroupFeeVOList(zyh, this.OrganizeId, ""),
            };
            // return Success("", resultDto);

            return Content(resultDto.GroupFeeVOList.ToJson());
        }
        /// <summary>
        /// đượclấytínhphíbảngphídùng
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="group">1 sfdl 2 sfxm 3 sfxm+jfrq 4 Không có</param>
        /// <param name="sfdl"></param>
        /// <param name="sfxm"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IActionResult GetFeeInfoGridList(string zyh, string group, string sfdl, string sfxm, string orgId, string kssj, string jssj)
        {
            var data = _patientCenterDmnService.GetPatjfbInfo(zyh, group, sfdl, sfxm, OrganizeId,kssj,jssj);
            return Content(data.ToJson());
        }

        /// <summary>
        /// mangmẫuhồtratra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>

        public IActionResult GetFeeInfobySfdl(string zyh, string group, string sfdl, string sfxm, string orgId, string kssj, string jssj,string keyword)
        {
            var dto = _patientCenterDmnService.GetPatjfbInfo(zyh, group, sfdl, sfxm, OrganizeId, kssj, jssj, keyword);
            return Content(dto.ToJson());
        }
        #endregion


        #region tạm ứng

        public IActionResult GetAdvancePayment(string zyh) {
            var data = _patientCenterDmnService.GetAdvancePayment(zyh, this.OrganizeId);
            return Content(data.ToJson());
        }

        #endregion

        #region Chẩn đoán
        public IActionResult GetZdGridList( string bah, string zyh)
        {
            var list = _patientCenterDmnService.GetDiagLsit( OrganizeId, zyh);
            //if (list.Count == 0)
            //{
            //    list = _patientCenterDmnService.GetPatHisZDInfo( zyh, OrganizeId, 2);
            //}
            //var data = new
            //{
            //    rows = list,
            //    total = pagination.total,
            //    page = pagination.page,
            //    records = pagination.records
            //};
            return Content(list.ToJson());
        }


        #endregion

        #region Liên quan BHYT
        public IActionResult GetMedInsurPreSettGrid(Pagination page,string zyh)
        {
            var data = _patientCenterDmnService.MedInsurPreSettList(zyh, OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult GetPreSettbyId(string presettid)
        {
            if (string.IsNullOrWhiteSpace(presettid))
            {
                return Content("");
            }
            var data = _patientCenterDmnService.PreSettbyId(presettid);
            return Content(data.ToJson());
        }

        public IActionResult GetMedInsurSettbyId(string settid)
        {
            var data = _patientCenterDmnService.MedInsurSettbyId(settid);
            return Content(data.ToJson());
        }

        #endregion


    }
}
