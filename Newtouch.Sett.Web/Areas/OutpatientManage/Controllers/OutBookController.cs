using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;













namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers
{
    [Area("OutpatientManage")]

    public class OutBookController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IOutBookDmnService _OutBookDmnService;
        private readonly IOutBookRepo _OutBookRepo;
        private readonly IOutBookDateRepo _OutBookDateRepo;
        private readonly IOutBookScheduleRepo _OutBookScheduleRepo;
        private readonly IOutBookScheduleService _OutBookScheduleService;
        private readonly ISysConfigRepo _sysConfigRepo;

        public OutBookController(IOutBookDmnService OutBookDmnService, IOutBookRepo OutBookRepo, IOutBookDateRepo OutBookDateRepo, IOutBookScheduleRepo OutBookScheduleRepo, IOutBookScheduleService OutBookScheduleService, ISysConfigRepo sysConfigRepo)
        {
            _OutBookDmnService = OutBookDmnService;
            _OutBookRepo = OutBookRepo;
            _OutBookDateRepo = OutBookDateRepo;
            _OutBookScheduleRepo = OutBookScheduleRepo;
            _OutBookScheduleService = OutBookScheduleService;
            _sysConfigRepo = sysConfigRepo;
        }

        // GET: OutpatientManage/OutBook
        public override IActionResult Index()
        {
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
        public IActionResult RosterCalendar()
        {
            return View();
        }
        public IActionResult RosterCalendarForm()
        {
            return View();
        }
        public IActionResult Doctor()
        {
            return View();
        }

        public IActionResult TimeForm()
        {
            return View();
        }

        public IActionResult Create() {
            return View();
        }
        public IActionResult PatOutBookManage()
        {
            return View();
        }

        public IActionResult GetPagintionList(Pagination pagination,string orgId, string keyword)
        {
            if (orgId == null)
            {
                orgId = OrganizeId;
            }
            var oplist = _OutBookDmnService.GetPagintionList(pagination, orgId,keyword);
            //for (int i = 0; i < oplist.Count; i++)
            //{
            //    var ghList = _OutBookRepo.getStaffByKs(oplist[i].ks);//lấy danh sách mã nhân viên bác sĩ theo khoa
            //    oplist[i].ys = getStaffGh(ghList);
            //    oplist[i].ysxm = getStaffName(ghList);//chuyển danh sách mã nhân viên bác sĩ thành tên bác sĩ phân tách bằng dấu phẩy
            //}
            //for (int i = 0; i < oplist.Count; i++) {
            //    oplist[i].ysxm = getStaffName(oplist[i].ys);
            //}
            var data = new
            {
                rows = oplist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public IActionResult GetStaffList(string orgId) {
            if (orgId == null)
            {
                orgId = OrganizeId;
            }
            var result = _OutBookDmnService.GetStaffList(orgId);
            return Content(result.ToJson());
        }
        public IActionResult GetMzpbZlxmzhList(string zhcode,string keyword) {
            var result = _OutBookDmnService.GetMzpbZlxmzh(zhcode,keyword,OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult UpdateForm(int keyValue,string ks, string gh) {
            var orgId = OrganizeId;
            var CreateCode=UserIdentity.UserName;
            var CreateTime = DateTime.Now;
            _OutBookRepo.UpdateForm(keyValue, orgId,ks, gh, CreateCode, CreateTime);
            return Success("Thao tác thành công.");
        }

        public IActionResult SubmitForm(string ks, List<string> ghList)
        {
            var orgId = OrganizeId;
            var CreateCode = UserIdentity.UserName;
            var CreateTime = DateTime.Now;
            //thiết lậpnàykhoadướitrạng tháilà0
            _OutBookRepo.updateZt(ks, CreateCode, CreateTime,OrganizeId);
            foreach (var gh in ghList)
            {
                //thêm mới cập nhật
                _OutBookRepo.SubmitForm(orgId, ks, gh, CreateCode, CreateTime);
            }
            return Success("Thao tác thành công.");
        }

        public IActionResult getArrangeInfo(int ghpbId) {
            if (ghpbId == 0)
            {
                //thêm mới
                return Content(null);
            }
            else
            {
                var result = _OutBookDmnService.getArrangeInfo(ghpbId,OrganizeId);
                var ghList = _OutBookRepo.getStaffByKs(result.ks,OrganizeId);//lấy danh sách mã nhân viên bác sĩ theo khoa
                //if (result != null && result.mjzbz != ((int)EnumOutPatientType.expertOutpat).ToString())
                //{
                //    result.ys = getStaffGh(ghList);//nối phân tách bằng dấu phẩycủabác sĩmã nhân viên
                //    result.ysxm = getStaffName(ghList);//chuyển danh sách mã nhân viên bác sĩ thành tên bác sĩ phân tách bằng dấu phẩy
                //}
                return Content(result.ToJson());
            }
        }
        public IActionResult getDateInfo(int ghpbId)
        {
            var orgId = OrganizeId;
            var result = _OutBookDmnService.getDateInfo(orgId, ghpbId);
            return Content(result.ToJson());
        }
        public IActionResult getDateTimeInfo(int ghpbId,string timeslot)
        {
            var orgId = OrganizeId;
            var result = _OutBookDmnService.getDateTimeInfo(orgId, ghpbId, timeslot);
            return Content(result.ToJson());
        }
        //chuyển danh sách mã nhân viên bác sĩ thành tên bác sĩ phân tách bằng dấu phẩy
        public string getStaffName(IList<string> ghList) {
            if (ghList == null) {
                return null;
            }
            var strName = "";
            foreach (var gh in ghList) {
                strName += _OutBookDmnService.getStaffName(gh) + ",";
            }
            if (strName.Contains(","))
            {
                strName = strName.Substring(0, strName.Length - 1);
            }
            return strName;
        }

        //đembác sĩchuyển danh sách mã nhân viên thành phân tách bằng dấu phẩycủachuỗi
        public string getStaffGh(IList<string> ghList) {
            if (ghList == null)
            {
                return null;
            }
            var strGh = "";
            foreach (var gh in ghList)
            {
                strGh += gh+",";
            }
            if (strGh.Contains(","))
            {
                strGh = strGh.Substring(0, strGh.Length - 1);
            }
            return strGh;
        }
        public IActionResult DeleteArrange(int ghpbId)
        {
            _OutBookDmnService.DeleteArrange(ghpbId, this.OrganizeId);
            return Success("Thao tác thành công.");
        }
            public IActionResult SubmitArrange(OutBookArrangeVO entity, int ghpbId)
        {
            var orgId = OrganizeId;
            var User = UserIdentity.UserName;
            var Time = DateTime.Now;
            if (ghpbId == 0)
            {//thêm mới
                //lấylịch trựcmã số
                var ghpbIdNew = _OutBookDmnService.getghpbId();
                //thêm mớithông tin lịch trực[mz_ghpb_config]
                _OutBookDmnService.InsertArrange(entity, orgId, User, Time, ghpbIdNew);
                //thêm mớibác sĩquan hệ[mz_ghpb_rel_doc]
                //thêm mớithời gian lịch trực[mz_ghpb_date]
                for (int i = 1; i <= 7; i++)
                {
                    //thêm mới cập nhật thời gian lịch trực [mz_ghpb_date]
                    _OutBookDateRepo.UpdateDate(entity, ghpbId, i.ToString(), orgId, User, Time, ghpbIdNew);
                }
                return Success("Thao tác thành công.");
            }
            else
            {
                //cập nhật thông tin[mz_ghpb_config]
                _OutBookDmnService.UpdateArrange(entity, ghpbId, orgId, User, Time);
                for (int i = 1; i <= 7; i++)
                {
                    //thêm mới cập nhật thời gian lịch trực [mz_ghpb_date]
                    _OutBookDateRepo.UpdateDate(entity, ghpbId, i.ToString(), orgId, User, Time,0);
                }
                return Success("Thao tác thành công.");
            }
        }
        public IActionResult SubmitArrangeADD(OutBookArrangeVO entity, int ghpbId)
        {
            var orgId = OrganizeId;
            var User = UserIdentity.UserName;
            var Time = DateTime.Now;
            var ghpbIdNew = _OutBookDmnService.getghpbId();
            _OutBookDmnService.InsertArrange(entity, orgId, User, Time, ghpbIdNew);
            return Success(ghpbIdNew.ToString());
        }
        /// <summary>
        /// lưu
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveDataPbhy(List<OutBookScheduleEntity> pbList)
        {
            string orgId = this.OrganizeId;
            _OutBookDmnService.SaveDatapb(pbList, orgId);
            return Success();
        }
        /// <summary>
        /// sửakhám bệnhhoặcngười ngừng khámtrạng thái
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveDatatzcz(Decimal ScheduId,string czzt,string tzyy)
        {
            string orgId = this.OrganizeId;
            _OutBookDmnService.SaveDatatzcz(ScheduId, czzt, orgId,tzyy);
            return Success();
        }
        public IActionResult getDateInfosjd()
        {
            string orgId = OrganizeId;
            IList<string> result = _OutBookDmnService.getDateInfosjd(orgId);
            return Content(result.ToJson());
            //return Content(staffName);
        }
        public IActionResult getDateInfosjdcount(int ghpbId)
        {
            string orgId = OrganizeId;
            IList<string> result = _OutBookDmnService.getDateInfosjdcount(ghpbId,orgId);
            return Content(result.ToJson());
        }

        public IActionResult SubmitghpbTime(string timestr)
        {
            var orgId = OrganizeId;
            var User = UserIdentity.UserName;
            var Time = DateTime.Now;
            for (int i = 0; i < timestr.Split(',').Length; i++)
            {
                _OutBookDmnService.InsertghpbTime(timestr.Split(',')[i].Split('-')[0], timestr.Split(',')[i].Split('-')[1], orgId, User, Time,i+1);
            }
            return Success("Thao tác thành công.");
        }

        public IActionResult getStaffByKs(string ks) {
            string orgId = OrganizeId;
            IList<string> result= _OutBookDmnService.getStaffListByKs(ks, orgId);
            return Content(result.ToJson());
            //return Content(staffName);
        }

        public IActionResult CreateSchedule(Pagination pagination,string time) {
           
            var orgId = OrganizeId;
            var Time = Convert.ToDateTime(time);
            //thực thi thủ tục lưu trữ
            string zlxmzhconf = _sysConfigRepo.GetValueByCode("EnableZlxmGroup", orgId);
            if (!string.IsNullOrWhiteSpace(zlxmzhconf) && zlxmzhconf == "1")
            {
                _OutBookScheduleRepo.ExecSchedulebyGroup();
            }
            else
            {
                _OutBookScheduleRepo.ExecSchedule();
            }
                
            //lấydanh sách
            var list= _OutBookScheduleRepo.GetPagintionList(pagination,orgId, Time);
            var oplist = new List<OutBookScheduleVO>();
            for (int i = 0; i < list.Count; i++)
            {
                var ghList = _OutBookRepo.getStaffByKs(list[i].czks,OrganizeId);//lấy danh sách mã nhân viên bác sĩ theo khoa
                list[i].ysgh = getStaffGh(ghList);
                list[i].ysxm = getStaffName(ghList);//chuyển danh sách mã nhân viên bác sĩ thành tên bác sĩ phân tách bằng dấu phẩy
            }

            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public IActionResult GetSchedule(string kssj,string jssj,string ys,string czztcx,string ScheduId,string ks,string lx)
        {

            var orgId = OrganizeId;
            //lấydanh sách
            var list = _OutBookScheduleRepo.GetPagintionListTime(orgId, kssj, jssj, ys, czztcx, ScheduId, ks, lx);
            var oplist = new List<OutBookScheduleVO>();
            var data = new
            {
                rows = list
            };
            return Content(data.ToJson());
        }

        #region HIS Hẹn/Hủy hẹn
        public IActionResult PatBookGh(string cardNo,int ScheduId, string brxz, string Doctor, DateTime OutDate)
        {
            var bookId = _OutBookDmnService.PatBookGh(cardNo,ScheduId, brxz, Doctor, OutDate,this.OrganizeId);
            return Success();
        }

        public IActionResult CancalBook(string BookId)
        {
            var bookId = _OutBookDmnService.CancalBook(BookId);
            return Success();
        }
        #endregion
    }
}
