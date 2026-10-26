using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity.Outpatient;
using Newtouch.Domain.IDomainServices.Outpatient;
using Newtouch.Domain.IRepository.Outpatient;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class OutpatientConsultController : OrgControllerBase
    {
        private readonly IOutpatientConsultDmnService _outpatientConsultDmnService;
        private readonly IOutpatientRegConsultRepo _outpatientRegConsultRepo;
        private readonly IOutpatientConsultDoctorRepo _outpatientConsultDoctorRepo;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        // GET: NurseManage/OutpatientConsult

        /// <summary>
        /// Trang phân loại ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
			ViewBag.rykscode = UserIdentity.DepartmentCode;
			return View();
        }

        /// <summary>
        /// Trang gọi số phân loại (màn hình lớn)
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultCall() {
            ViewBag.orgName = _outpatientConsultDmnService.GetOrgName(this.OrganizeId);
            ViewBag.ConsultCallRefreshTime = _sysConfigRepo.GetValueByCode("ConsultCallRefreshTime", OrganizeId);
            return View();
        }

        /// <summary>
        /// Bác sĩ phân loại
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultDoctor() {
            return View();
        }

        /// <summary>
        /// Thông tin phòng khám (màn hình nhỏ)
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultInfo()
        {
            ViewBag.ConsultInfoRefreshTime = _sysConfigRepo.GetValueByCode("ConsultInfoRefreshTime", OrganizeId);
            return View();
        }

        #region 门诊分诊
        /// <summary>
        /// Lấy phòng khám chuyên gia theo phân trang
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ksCode"></param>
        /// <param name="keyword"></param>
        /// <param name="ghrq"></param>
        /// <returns></returns>
        public ActionResult GetExpertInfo(Pagination pagination, string ksCode, string keyword, string ghrq)
        {

            var list = _outpatientConsultDmnService.GetExpertInfo(pagination, ksCode, keyword, ghrq, this.OrganizeId);
            //var list = new List<string>(); ;
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lấy phòng khám thường theo phân trang
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ksCode"></param>
        /// <param name="keyword"></param>
        /// <param name="ghrq"></param>
        /// <returns></returns>
        public ActionResult GetNormalInfo(Pagination pagination, string ksCode, string keyword, string ghrq)
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
        /// Lưu phòng khám đăng ký
        /// </summary>
        /// <param name="ztobj"></param>
        /// <param name="ztxmlist"></param>
        /// <returns></returns>
        public ActionResult SaveData( List<OutpatientConsultVO> ghzslist)
        {
            //Bảng mẫu
            foreach (var obj in ghzslist) {
                if (!string.IsNullOrEmpty(obj.zsCode))
                {
                    var entity = new OutpatientRegConsultEntity();
                    entity.ghnm = obj.ghnm;
                    entity.zsCode = obj.zsCode;
                    entity.calledstu = 1;//Chờ gọi số
                    entity.OrganizeId = this.OrganizeId;
                    _outpatientRegConsultRepo.SubmitForm(entity);
                }
            }
            return Success();
        }

        /// <summary>
        /// Lấy số lượng chờ khám của từng phòng khám thuộc khoa
        /// </summary>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        public ActionResult getConsultCount( string ksCode,string ghrq )
        {
			//ksCode = this.UserIdentity.DepartmentCode;
            var list = _outpatientConsultDmnService.getConsultCount( ksCode, this.OrganizeId, ghrq);
            return Content(list.ToJson());
        }

        #endregion

        #region 分诊叫号（大屏）
        public ActionResult GetConsultCall(string ksCode, string keyword, string ghrq)
        {
			//ksCode = this.UserIdentity.DepartmentCode;
			var list = _outpatientConsultDmnService.GetConsultCall(ksCode, keyword, ghrq, this.OrganizeId);
            return Content(list.ToJson());
        }

		public ActionResult UpdateCalledstu(int ghnm, int calledstu, string mzh)
		{
			
			_outpatientRegConsultRepo.UpdateCalledstu(ghnm, calledstu);//Trạng thái gọi số được cập nhật thành Đã gọi số
			var list = _outpatientConsultDmnService.GetConsultnext(this.OrganizeId, ghnm.ToString(), "");
			return Content(list.ToJson());
		}
		/// <summary>
		/// Gọi số trên trang chờ khám
		/// </summary>
		/// <param name="ghnm"></param>
		/// <param name="calledstu"></param>
		/// <param name="mzh"></param>
		/// <returns></returns>
		public ActionResult UpdatePatient(int calledstu, string mzh)
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
			_outpatientConsultDmnService.UpdatePatient(mzh, calledstu, this.OrganizeId);//Trạng thái gọi số được cập nhật thành Đã gọi số
			_outpatientConsultDmnService.UpdataZSinsert(mzh, ysgh, this.OrganizeId);
			var list = _outpatientConsultDmnService.GetConsultnext(this.OrganizeId, null, mzh);
			return Content(list.ToJson());
		}
		#endregion


		#region 分诊医生

		/// <summary>
		/// Lấy danh sách khoa
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetDeptList(Pagination pagination ,string keyValue)
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
        /// Lấy danh sách phòng khám của bộ phận
        /// </summary>
        /// <param name="ksCode">Số khoa</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetConsultDoctorByDept(string ksCode)
        {
			//ksCode = this.UserIdentity.DepartmentCode;
			var list = _outpatientConsultDmnService.GetConsultDoctorByDept(this.OrganizeId, ksCode);

            return Content(list.ToJson());
        }

        public ActionResult SaveConsultDoctor(List<OutpatientConsultDoctorVO> zsyslist)
        {
            //var staff = _sysStaffRepo.GetValidStaffListByOrganizeId(this.OrganizeId);
            //Bảng mẫu
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
				//Sửa số tầng phòng khám Số phòng khám
				_outpatientConsultDoctorRepo.UpdateZS(obj);
			}
            return Success();
        }


        public ActionResult isDoctorRepeat(List<OutpatientConsultDoctorVO> zsyslist)
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

        #region 诊室信息（小屏）
        public ActionResult GetConsultInfo(string zsCode,string ghrq)
        {
            var entity = _outpatientConsultDmnService.GetConsultInfo(this.OrganizeId, zsCode,ghrq);

            return Content(entity.ToJson());
        }
        #endregion
    }
}