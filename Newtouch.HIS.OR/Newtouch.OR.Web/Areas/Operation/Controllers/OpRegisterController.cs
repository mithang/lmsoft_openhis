using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Exceptions;
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
			// FA-009: ArrangeId trong truoc day xuong service gay loi SQL
			// "@arrangeId" chua khai bao; ArrangeId khong ton tai truoc day
			// NRE o result.applyno. Ca hai tra VO rong (form dang ky trong)
			// thay vi loi — trang dang ky sau mo mo cho lich chua dang ky can
			// dang nay, UI luon gui ArrangeId da chon tu luoi.
			if (string.IsNullOrWhiteSpace(ArrangeId))
			{
				return Content(new RegistrationListVO().ToJson());
			}
			var result = _OPRegisterDmnService.getRegistrationInfo(OrganizeId,ArrangeId, sqzt);
			if (result == null)
			{
				return Content(new RegistrationListVO().ToJson());
			}
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
            var isEdit = !(string.IsNullOrWhiteSpace(keyValue) || keyValue == "null");
            // G24: sua phai ton tai trong cung co so; sua khong gui arrangeId
            // thi lay lich cua chinh dang ky; khong cho doi dang ky sang lich
            // khac (UI khong co luong nay).
            ORRegistrationEntity regEntity = null;
            if (isEdit)
            {
                regEntity = _ORRegistrationRepo.FindEntity(keyValue);
                if (regEntity == null || regEntity.OrganizeId != OrganizeId || regEntity.zt != "1")
                {
                    return Error("Không tìm thấy đăng ký phẫu thuật");
                }
                if (string.IsNullOrWhiteSpace(entity.arrangeId))
                {
                    entity.arrangeId = regEntity.ArrangeId;
                }
                if (entity.arrangeId != regEntity.ArrangeId)
                {
                    return Error("Không thể đổi lịch xếp của đăng ký phẫu thuật");
                }
            }
            // G24: validate lich TRUOC KHI dot serial / ghi gi ca — base dot
            // serial roi moi throw o UpdateSqzt (create loi van dot so ssxh),
            // va insert staff truoc roi moi loi nen de lai staff mo coi.
            if (string.IsNullOrWhiteSpace(entity.arrangeId))
            {
                return Error("Không tìm thấy lịch xếp phẫu thuật");
            }
            var arrangement = _ORArrangementRepo.FindEntity(entity.arrangeId);
            if (arrangement == null || arrangement.OrganizeId != OrganizeId || arrangement.zt != "1")
            {
                return Error("Không tìm thấy lịch xếp phẫu thuật");
            }
            if (!isEdit && arrangement.sqzt != "2")
            {
                return Error("Lịch phẫu thuật đã được đăng ký hoặc đã hủy");
            }
            string ssxh;
            if (isEdit)
            {
                // G24: sua khong gui ssxh -> dung ssxh cua chinh dang ky
                // (base: staff insert ssxh NULL -> loi SQL).
                ssxh = string.IsNullOrWhiteSpace(entity.ssxh) ? regEntity.ssxh : entity.ssxh;
            }
            else
            {
                ssxh = EFDBBaseFuncHelper.GetOPSsxh(OrganizeId);
            }
            // Build staff truoc (validate ten nhan vien, chua ghi gi ca).
            List<OROpStaffRecordEntity> staffRecordList = getStaffRecordList(entity, ssxh);
            // G24: ghi dang ky TRUOC khi ghi staff — dang ky loi thi khong de
            // lai staff mo coi (base: staff truoc, dang ky sau).
            _ORRegistrationRepo.SubmitForm(entity, keyValue, ssxh);
            //Sửa bảng hồ sơ nhân viên
            foreach (var ent in staffRecordList)
            {
                _OROpStaffRecordRepo.SubmitForm(ent, "");
            }
            //Sửa trạng thái xếp lịch(Đã đăng ký)
            _ORArrangementRepo.UpdateSqzt(entity.arrangeId, "4");
            return Success("Thao tác thành công.");
        }

        // Tra ve ten nhan vien theo ma; nem FailedException ro rang thay vi
        // ArgumentOutOfRangeException khi GetStafflist tra ve danh sach rong (ma khong ton tai).
        private string GetStaffNameByGh(string gh)
        {
            var staffList = _CommonDmnService.GetStafflist(gh, OrganizeId);
            if (staffList == null || staffList.Count == 0)
            {
                throw new FailedException("Không tìm thấy nhân viên có mã: " + gh);
            }
            return staffList[0].ryxm;
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
                staff.ryxm = GetStaffNameByGh(entity.ysgh);
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
                staff.ryxm = GetStaffNameByGh(entity.zlys1);
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
                staff.ryxm = GetStaffNameByGh(entity.zlys2);
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
                staff.ryxm = GetStaffNameByGh(entity.xhhs);
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
                staff.ryxm = GetStaffNameByGh(entity.xshs);
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
                staff.ryxm = GetStaffNameByGh(entity.mzys);
                staff.px = 1;
                staff.zt = "1";
                staff.OrganizeId = OrganizeId;
                list.Add(staff);
            }
            return list;
        }

        public IActionResult DeleteData(string keyValue,string arrangeId,string ssxh)
        {
            // G24: validate tat ca TRUOC KHI doi gi ca — base UpdateSqzt truoc
            // roi moi NRE o DeleteForm nen lich da lat + staff da xoa trong
            // khi dang ky con nguyen (half-write); arrangeId phai la lich cua
            // chinh dang ky, khong lat lich khac.
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Error("Không tìm thấy đăng ký phẫu thuật");
            }
            var dbEntity = _ORRegistrationRepo.FindEntity(keyValue);
            if (dbEntity == null || dbEntity.OrganizeId != OrganizeId || dbEntity.zt != "1")
            {
                return Error("Không tìm thấy đăng ký phẫu thuật");
            }
            if (string.IsNullOrWhiteSpace(arrangeId) || arrangeId != dbEntity.ArrangeId)
            {
                return Error("Không tìm thấy lịch xếp phẫu thuật");
            }
            var arrangement = _ORArrangementRepo.FindEntity(arrangeId);
            if (arrangement == null || arrangement.OrganizeId != OrganizeId || arrangement.zt != "1")
            {
                return Error("Không tìm thấy lịch xếp phẫu thuật");
            }
            //Sửa trạng thái xếp lịch(Đã xếp lịch)
            _ORArrangementRepo.UpdateSqzt(arrangeId, "2");
            //Sửa đăng ký nhân viên — xoa theo ssxh cua chinh dang ky, khong
            // theo ssxh do client gui (co the xoa staff cua dang ky khac).
            var list=_OROpStaffRecordRepo.getIdBySsxh(dbEntity.ssxh);
            foreach (var obj in list) {
                _OROpStaffRecordRepo.DeleteForm(obj.Id);
            }
            //Sửa trạng thái đăng ký thành3Đã hủy bỏ,Trạng thái là0
            _ORRegistrationRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }
    }
}