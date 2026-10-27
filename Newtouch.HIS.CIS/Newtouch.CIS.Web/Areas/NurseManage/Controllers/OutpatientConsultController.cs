using Newtouch.Domain.IRepository;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.CIS.Repository;
using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.Domain.Entity.Outpatient;
using Newtouch.Domain.IDomainServices.Outpatient;
using Newtouch.Domain.IRepository.Outpatient;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    [Area("NurseManage")]
    public class OutpatientConsultController : OrgControllerBase
    {
        private readonly IOutpatientConsultDmnService _outpatientConsultDmnService;
        private readonly IOutpatientRegConsultRepo _outpatientRegConsultRepo;
        private readonly IOutpatientConsultDoctorRepo _outpatientConsultDoctorRepo;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        public OutpatientConsultController(IOutpatientConsultDmnService outpatientConsultDmnService, IOutpatientRegConsultRepo outpatientRegConsultRepo, IOutpatientConsultDoctorRepo outpatientConsultDoctorRepo, ISysStaffRepo sysStaffRepo, ISysConfigRepo sysConfigRepo)
        {
            this._outpatientConsultDmnService = outpatientConsultDmnService;
            this._outpatientRegConsultRepo = outpatientRegConsultRepo;
            this._outpatientConsultDoctorRepo = outpatientConsultDoctorRepo;
            this._sysStaffRepo = sysStaffRepo;
            this._sysConfigRepo = sysConfigRepo;
        }

        // GET: NurseManage/OutpatientConsult

        /// <summary>
        /// Ngoại trúphútkhámtrangmặt
        /// </summary>
        /// <returns></returns>
        public override IActionResult Index()
        {
			ViewBag.rykscode = UserIdentity.DepartmentCode;
			return View();
        }

        /// <summary>
        /// phútkhámGọi sốtrangmặt（lớnmàn hình）
        /// </summary>
        /// <returns></returns>
        public IActionResult ConsultCall() {
            ViewBag.orgName = _outpatientConsultDmnService.GetOrgName(this.OrganizeId);
            ViewBag.ConsultCallRefreshTime = _sysConfigRepo.GetValueByCode("ConsultCallRefreshTime", OrganizeId);
            return View();
        }

        /// <summary>
        /// phútkhámBác sĩ
        /// </summary>
        /// <returns></returns>
        public IActionResult ConsultDoctor() {
            return View();
        }

        /// <summary>
        /// Phòng khámthông tin（nhỏmàn hình）
        /// </summary>
        /// <returns></returns>
        public IActionResult ConsultInfo()
        {
            ViewBag.ConsultInfoRefreshTime = _sysConfigRepo.GetValueByCode("ConsultInfoRefreshTime", OrganizeId);
            return View();
        }

        #region Ngoại trúPhân loại khám
        /// <summary>
        /// phúttrangLấychuyên giaPhòng khám
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ksCode"></param>
        /// <param name="keyword"></param>
        /// <param name="ghrq"></param>
        /// <returns></returns>
        public IActionResult GetExpertInfo(Pagination pagination, string ksCode, string keyword, string ghrq)
        {

            var list = _outpatientConsultDmnService.GetExpertInfo(pagination, ksCode, keyword, ghrq, this.OrganizeId);
            //var list = new List<string>(); ;
            return Content(list.ToJson());
        }

        /// <summary>
        /// phúttrangLấythông thườngPhòng khám
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ksCode"></param>
        /// <param name="keyword"></param>
        /// <param name="ghrq"></param>
        /// <returns></returns>
        public IActionResult GetNormalInfo(Pagination pagination, string ksCode, string keyword, string ghrq)
        {
			//ksCode = this.UserIdentity.DepartmentCode;
			var list = new
            {
                rows = _outpatientConsultDmnService.GetNormalInfo(pagination, ksCode, keyword, ghrq, this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lưuđăng ký khámPhòng khám
        /// </summary>
        /// <param name="ztobj"></param>
        /// <param name="ztxmlist"></param>
        /// <returns></returns>
        public IActionResult SaveData( List<OutpatientConsultVO> ghzslist)
        {
            //bảng mẫu
            foreach (var obj in ghzslist) {
                if (!string.IsNullOrEmpty(obj.zsCode))
                {
                    var entity = new OutpatientRegConsultEntity();
                    entity.ghnm = obj.ghnm;
                    entity.zsCode = obj.zsCode;
                    entity.calledstu = 1;//chờGọi số
                    entity.OrganizeId = this.OrganizeId;
                    _outpatientRegConsultRepo.SubmitForm(entity);
                }
            }
            return Success();
        }

        /// <summary>
        /// LấyKhoadướimỗiPhòng khámChờ khám bệnhSố lượng
        /// </summary>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        public IActionResult getConsultCount( string ksCode,string ghrq )
        {
			//ksCode = this.UserIdentity.DepartmentCode;
            var list = _outpatientConsultDmnService.getConsultCount( ksCode, this.OrganizeId, ghrq);
            return Content(list.ToJson());
        }

        #endregion

        #region Phân loại khámGọi số(Màn hình lớn)
        public IActionResult GetConsultCall(string ksCode, string keyword, string ghrq)
        {
			//ksCode = this.UserIdentity.DepartmentCode;
			var list = _outpatientConsultDmnService.GetConsultCall(ksCode, keyword, ghrq, this.OrganizeId);
            return Content(list.ToJson());
        }

		public IActionResult UpdateCalledstu(int ghnm, int calledstu, string mzh)
		{
			
			_outpatientRegConsultRepo.UpdateCalledstu(ghnm, calledstu);//Gọi sốTrạng tháiCập nhậtlà đãGọi số
			var list = _outpatientConsultDmnService.GetConsultnext(this.OrganizeId, ghnm.ToString(), "");
			return Content(list.ToJson());
		}
		/// <summary>
		/// Chờ khám bệnhtrangmặtGọi số
		/// </summary>
		/// <param name="ghnm"></param>
		/// <param name="calledstu"></param>
		/// <param name="mzh"></param>
		/// <returns></returns>
		public IActionResult UpdatePatient(int calledstu, string mzh)
		{
			var ysgh = this.UserIdentity.rygh;
			var falg= _outpatientConsultDmnService.ISfalgPatient(mzh, this.OrganizeId);
			if (falg==1)
			{
				var newdata = new
				{
					falg= falg
				};
				return Content(newdata.ToJson());
			}
			_outpatientConsultDmnService.UpdatePatient(mzh, calledstu, this.OrganizeId);//Gọi sốTrạng tháiCập nhậtlà đãGọi số
			_outpatientConsultDmnService.UpdataZSinsert(mzh, ysgh, this.OrganizeId);
			var list = _outpatientConsultDmnService.GetConsultnext(this.OrganizeId, null, mzh);
			return Content(list.ToJson());
		}
		#endregion


		#region Phân loại khámBác sĩ

		/// <summary>
		/// LấyKhoadanh sách
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetDeptList(Pagination pagination ,string keyValue)
        {
            var list = new
            {
                rows = _outpatientConsultDmnService.GetDeptListByKeyValue(pagination, this.OrganizeId, keyValue),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// LấyKhoa phòngcủaPhòng khámdanh sách
        /// </summary>
        /// <param name="ksCode">Số hiệu khoa</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetConsultDoctorByDept(string ksCode)
        {
			//ksCode = this.UserIdentity.DepartmentCode;
			var list = _outpatientConsultDmnService.GetConsultDoctorByDept(this.OrganizeId, ksCode);

            return Content(list.ToJson());
        }

        public IActionResult SaveConsultDoctor(List<OutpatientConsultDoctorVO> zsyslist)
        {
            //var staff = _sysStaffRepo.GetValidStaffListByOrganizeId(this.OrganizeId);
            //bảng mẫu
            foreach (var obj in zsyslist)
            {
                if (!string.IsNullOrEmpty(obj.gh))
                {
                    var entity = new OutpatientConsultDoctorEntity();
                    entity.gh = obj.gh;
                    entity.zsCode = obj.zsCode;
                    entity.rq = DateTime.Now.ToString("yyyy-MM-dd").ToDate();
                    entity.OrganizeId = this.OrganizeId;
                    _outpatientConsultDoctorRepo.SubmitForm(entity);

				}
				//SửaPhòng khámlầulớpsố Phòng khámphòngsố
				_outpatientConsultDoctorRepo.UpdateZS(obj);
			}
            return Success();
        }


        public IActionResult isDoctorRepeat(List<OutpatientConsultDoctorVO> zsyslist)
        {
            var zsStr = "";
            var ghStr = "";
            foreach (var zsys in zsyslist)
            {
                zsStr += zsys.zsCode + ",";
                ghStr +=  zsys.gh + ",";
            }
            zsStr = zsStr.Substring(0, zsStr.Length - 1);
            ghStr = ghStr.Substring(0, ghStr.Length - 1);
            var list = _outpatientConsultDoctorRepo.GetRepeatDoctor(this.OrganizeId, zsStr, ghStr);
            return Content(list.ToJson());
        }

        #endregion

        #region Phòng khámThông tin(nhỏmàn hình)
        public IActionResult GetConsultInfo(string zsCode,string ghrq)
        {
            var entity = _outpatientConsultDmnService.GetConsultInfo(this.OrganizeId, zsCode,ghrq);

            return Content(entity.ToJson());
        }
        #endregion
    }
}