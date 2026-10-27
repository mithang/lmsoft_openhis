using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    [Area("Operation")]
    public class OpRegisterController :  OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IORRegistrationRepo _ORRegistrationRepo;
        private readonly IOPRegisterDmnService _OPRegisterDmnService;
        private readonly IORArrangementRepo _ORArrangementRepo;
        private readonly IOROpStaffRecordRepo _OROpStaffRecordRepo;
		private readonly IORApplyInfoExpandRepo _ORApplyInfoExpandRepo;
        private readonly ISysStaffRepo _sysStaff;
        public OpRegisterController(ICommonDmnService commonDmnService, IORRegistrationRepo oRRegistrationRepo, IOPRegisterDmnService oPRegisterDmnService, IORArrangementRepo oRArrangementRepo, IOROpStaffRecordRepo oROpStaffRecordRepo, IORApplyInfoExpandRepo oRApplyInfoExpandRepo, ISysStaffRepo sysStaff)
        {
            _CommonDmnService = commonDmnService;
            _ORRegistrationRepo = oRRegistrationRepo;
            _OPRegisterDmnService = oPRegisterDmnService;
            _ORArrangementRepo = oRArrangementRepo;
            _OROpStaffRecordRepo = oROpStaffRecordRepo;
            _ORApplyInfoExpandRepo = oRApplyInfoExpandRepo;
            _sysStaff = sysStaff;
        }
        // GET: Operation/OpRegister
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PostoperativeReg()
        {
            return View();
        }

        public IActionResult GetPagintionListForRegistration(Pagination pagination, string keyword,string bq,string djzt)
        {
            #region
            //object[] list = new[]{
            //    new{
            //        applyno="ss0000001",
            //        oparrangeno="20191001002",
            //        xm="Nguyễn Văn A",
            //        xb="Nam",
            //        nl="26",
            //        zyh="001",
            //        patid="12345",
            //        bq="Khoa 1",
            //        ch="C098",
            //        rysj="2019-10-1",
            //        ryzd="Viêm ruột thừa",
            //        sqssrq="2019-10-1 10:00",
            //        sqssmc="Cắt ruột thừa1",
            //        sqzdys="Bác sĩ 1",
            //        sqzt="Đã duyệt",
            //        sssj="2019-10-1 10:00",
            //        ssmc="Cắt ruột thừa1",
            //        zdys="Bác sĩ 1",
            //        yszs1="ys01",
            //        yszs2="ys02",
            //        xshs="hs01",  //Y tá rửa tay
            //        xhhs="hs02",  //Y tá lưu động
            //        ssroom="Phòng phẫu thuật 1",
            //        ssno="2"
            //    }
            //};
            #endregion
            var list = _OPRegisterDmnService.GetPagintionList(pagination,OrganizeId,keyword,bq,djzt);
            var data = new
            {
				rows = list,
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
            return Content(data.ToJson());
        }

        public IActionResult getRegistrationInfo(string ArrangeId, string sqzt) {
			var result = _OPRegisterDmnService.getRegistrationInfo(OrganizeId,ArrangeId, sqzt);
            result.ss= _ORApplyInfoExpandRepo.getApplyExtendByApplyno(OrganizeId, result.applyno);
            if (!string.IsNullOrWhiteSpace(result.zlys1) && string.IsNullOrWhiteSpace(result.zlys1name))
            {
                result.zlys1name = _sysStaff.GetNameByGh(result.zlys1, OrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(result.zlys2) && string.IsNullOrWhiteSpace(result.zlys2name))
            {
                result.zlys2name = _sysStaff.GetNameByGh(result.zlys2, OrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(result.xshs) && string.IsNullOrWhiteSpace(result.xshsname))
            {
                result.xshsname = _sysStaff.GetNameByGh(result.xshs, OrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(result.xhhs) && string.IsNullOrWhiteSpace(result.xhhsname))
            {
                result.xhhsname = _sysStaff.GetNameByGh(result.xhhs, OrganizeId);
            }
            return Content(result.ToJson());
        }

        public IActionResult submitForm(RegistrationListVO entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            var ssxh = "";
			if (!(string.IsNullOrWhiteSpace(keyValue) || keyValue == "null"))
			{//Sửa
				ssxh = entity.ssxh;
			}
			else
			{//Thêm mới
				ssxh = EFDBBaseFuncHelper.GetOPSsxh(OrganizeId);
			}
			//var ssxh = "S20191127A000002";
			//Sửa bảng hồ sơ nhân viên
			List<OROpStaffRecordEntity> staffRecordList = getStaffRecordList(entity, ssxh);
			foreach (var ent in staffRecordList)
			{
				_OROpStaffRecordRepo.SubmitForm(ent, "");
			}
			//Sửa trạng thái xếp lịch(Đã đăng ký)
			_ORArrangementRepo.UpdateSqzt(entity.arrangeId, "4");
			//Sửa bảng đăng ký
			_ORRegistrationRepo.SubmitForm(entity, keyValue, ssxh);
			return Success("Thao tác thành công.");
        }

        //Lấy danh sách hồ sơ nhân viên theo thông tin nhân viên y tế
        public List<OROpStaffRecordEntity> getStaffRecordList(RegistrationListVO entity,string ssxh) {
            List<OROpStaffRecordEntity> list = new List<OROpStaffRecordEntity>();
            //Bác sĩ mổ chính
            if (!string.IsNullOrWhiteSpace(entity.ysgh))
            {
                OROpStaffRecordEntity staff = new OROpStaffRecordEntity();
                staff.ssxh = ssxh;
                staff.rylb = "1";
                staff.rygh = entity.ysgh;
                staff.ryxm = _CommonDmnService.GetStafflist(entity.ysgh, OrganizeId)[0].ryxm;
                staff.px = 1;
                staff.zt = "1";
                staff.OrganizeId = OrganizeId;
                list.Add(staff);
            }
            //Phụ mổ 1
            if (!string.IsNullOrWhiteSpace(entity.zlys1))
            {
                OROpStaffRecordEntity staff = new OROpStaffRecordEntity();
                staff.ssxh = ssxh;
                staff.rylb = "2";
                staff.rygh = entity.zlys1;
                staff.ryxm = _CommonDmnService.GetStafflist(entity.zlys1, OrganizeId)[0].ryxm;
                staff.px = 1;
                staff.zt = "1";
                staff.OrganizeId = OrganizeId;
                list.Add(staff);
            }
            //Phụ mổ 2
            if (!string.IsNullOrWhiteSpace(entity.zlys2))
            {
                OROpStaffRecordEntity staff = new OROpStaffRecordEntity();
                staff.ssxh = ssxh;
                staff.rylb = "2";
                staff.rygh = entity.zlys2;
                staff.ryxm = _CommonDmnService.GetStafflist(entity.zlys2, OrganizeId)[0].ryxm;
                staff.px = 2;
                staff.zt = "1";
                staff.OrganizeId = OrganizeId;
                list.Add(staff);
            }
            //Y tá lưu động

            if (!string.IsNullOrWhiteSpace(entity.xhhs))
            {
                OROpStaffRecordEntity staff = new OROpStaffRecordEntity();
                staff.ssxh = ssxh;
                staff.rylb = "3";
                staff.rygh = entity.xhhs;
                staff.ryxm = _CommonDmnService.GetStafflist(entity.xhhs, OrganizeId)[0].ryxm;
                staff.px = 1;
                staff.zt = "1";
                staff.OrganizeId = OrganizeId;
                list.Add(staff);
            }
            //Y tá rửa tay
            if (!string.IsNullOrWhiteSpace(entity.xshs))
            {
                OROpStaffRecordEntity staff = new OROpStaffRecordEntity();
                staff.ssxh = ssxh;
                staff.rylb = "4";
                staff.rygh = entity.xshs;
                staff.ryxm = _CommonDmnService.GetStafflist(entity.xshs, OrganizeId)[0].ryxm;
                staff.px = 1;
                staff.zt = "1";
                staff.OrganizeId = OrganizeId;
                list.Add(staff);
            }
            //Bác sĩ gây mê
            if (!string.IsNullOrWhiteSpace(entity.mzys))
            {
                OROpStaffRecordEntity staff = new OROpStaffRecordEntity();
                staff.ssxh = ssxh;
                staff.rylb = "5";
                staff.rygh = entity.mzys;
                staff.ryxm = _CommonDmnService.GetStafflist(entity.mzys, OrganizeId)[0].ryxm;
                staff.px = 1;
                staff.zt = "1";
                staff.OrganizeId = OrganizeId;
                list.Add(staff);
            }
            return list;
        }

        public IActionResult DeleteData(string keyValue,string arrangeId,string ssxh)
        {
            //Sửa trạng thái xếp lịch(Đã xếp lịch)
            _ORArrangementRepo.UpdateSqzt(arrangeId, "2");
            //Sửa đăng ký nhân viên
            var list=_OROpStaffRecordRepo.getIdBySsxh(ssxh);
            foreach (var obj in list) {
                _OROpStaffRecordRepo.DeleteForm(obj.Id);
            }
            //Sửa trạng thái đăng ký thành3Đã hủy bỏ,Trạng thái là0
            _ORRegistrationRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }
    }
}