using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.OR.ManageSystem.Domain;
using Newtouch.OR.ManageSystem.Domain.DTO;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtouch.OR.ManageSystem.Repository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    [Area("Operation")]
    public class OpArrangementController : OrgControllerBase
    {
        private readonly IOpArrangementDmnService _OpArrangementDmnService;
        private readonly IORArrangementRepo _ORArrangementRepo;
        private readonly IORApplyInfoRepo _ORApplyInfoRepo;
        private readonly IORApplyInfoExpandRepo _ORApplyInfoExpandRepo;
        private readonly ISysStaffRepo _sysStaff;
        public OpArrangementController(IOpArrangementDmnService opArrangementDmnService, IORArrangementRepo oRArrangementRepo, IORApplyInfoRepo oRApplyInfoRepo, IORApplyInfoExpandRepo oRApplyInfoExpandRepo, ISysStaffRepo sysStaff)
        {
            _OpArrangementDmnService = opArrangementDmnService;
            _ORArrangementRepo = oRArrangementRepo;
            _ORApplyInfoRepo = oRApplyInfoRepo;
            _ORApplyInfoExpandRepo = oRApplyInfoExpandRepo;
            _sysStaff = sysStaff;
        }

        // GET: Operation/OpArrangement
        public IActionResult Index()
        {
            return View();
        }
        //Sắp xếp lịch phẫu thuật
        public IActionResult ArrangementDetail()
        {
            return View();
        }
        public IActionResult OperationProcDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetGridList(PagingData<QueryArrangeDto> reqdata)
        {
            var dto = reqdata.queryParams;
            if (dto.ksrq != null)
            {
                dto.ksrq = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", dto.ksrq).ToDate();
            }
            if (dto.jsrq != null)
            {
                dto.jsrq = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", dto.jsrq).ToDate();
            }
            if (string.IsNullOrWhiteSpace(dto.orgId))
            {
                dto.orgId = OrganizeId;
            }
            var list = _OpArrangementDmnService.GetArrangeList(dto);

            var data = new
            {
                rows = list,
                total = list.Count
            };
            return Content(data.ToJson());
        }
        public IActionResult GetGridList(Pagination pagination, QueryArrangeDto dto)
        {
            #region Dữ liệu kiểm tra
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
            //    },
            //    new{
            //        applyno="ss0000002",
            //        oparrangeno="",
            //        xm="Nguyễn Văn A",
            //        xb="Nam",
            //        nl="26",
            //        zyh="001",
            //        patid="12345",
            //        bq="Khoa 1",
            //        ch="C098",
            //        rysj="2019-10-1",
            //        ryzd="Viêm ruột thừa",
            //        sqssrq="2019-10-1 15:30",
            //        sqssmc="Cắt ruột thừa2",
            //        sqzdys="Bác sĩ 1",
            //        sqzt="Chờ duyệt",
            //        sssj="",
            //        ssmc="",
            //        zdys="",
            //        yszs1="",
            //        yszs2="",
            //        xshs="",  //Y tá rửa tay
            //        xhhs="",  //Y tá lưu động
            //        ssroom="",
            //        ssno=""
            //    }
            //};
            //return Content(list.ToJson());
            #endregion

            //Chuyển đổi định dạng thời gian
            if (dto.ksrq != null)
            {
                dto.ksrq = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", dto.ksrq).ToDate();
            }
            if (dto.jsrq != null)
            {
                dto.jsrq = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", dto.jsrq).ToDate();
            }
            if (string.IsNullOrWhiteSpace(dto.orgId))
            {
                dto.orgId = OrganizeId;
            }
            var list = _OpArrangementDmnService.GetArrangeList(dto);

            var data = new
            {
                rows = list,
                total = list.Count,
                page = 1,
                records = list.Count
            };
            return Content(data.ToJson());
        }

        public IActionResult submitForm(ORArrangementEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            var ApplyId = keyValue;
            //Chuyển số đơn đăng ký thành lịch phẫu thuậtId
            var ArrangeId = getArrangeIdByApplyId(ApplyId);
            //Đổi trạng thái đơn của bảng xếp lịch thành Đã duyệt
            _ORApplyInfoRepo.UpdateSqzt(ApplyId, "2");
            //Đổi trạng thái đơn của bảng đơn đăng ký thành Đã xếp lịch
            _ORArrangementRepo.SubmitForm(entity, ApplyId, ArrangeId);
            return Success("Thao tác thành công.");
        }

        public string getArrangeIdByApplyId(string ApplyId)
        {
            var result = _OpArrangementDmnService.getArrangeIdByApplyId(ApplyId, OrganizeId);
            return result;
        }

        public IActionResult GetArrangeObjByApplyId(string ApplyId)
        {
            var result = _OpArrangementDmnService.GetArrangeObjByApplyId(ApplyId, OrganizeId);
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
        /// <summary>
        /// Lấy chi tiết đơn đăng ký phẫu thuật(Trang xếp lịch phẫu thuật)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetApplyInfoByKey(string keyValue)
        {
            ApplyVO applyVO = new ApplyVO();
            var entity = _OpArrangementDmnService.GetApplyInfoByKey(keyValue, OrganizeId);
            if (entity.sqzt == null)
            {
                entity.sqzt = "1";

            }
            entity.sqzt = ((EnumSqzt)int.Parse(entity.sqzt)).GetDescription();

            applyVO.AnesCode = entity.AnesCode;
            applyVO.Applyno = entity.Applyno;
            applyVO.bq = entity.bq;
            applyVO.ch = entity.ch;
            applyVO.CreateTime = entity.CreateTime;
            applyVO.CreatorCode = entity.CreatorCode;
            applyVO.csrq = entity.csrq;
            applyVO.Id = entity.Id;
            applyVO.isgl = entity.isgl;
            applyVO.ks = entity.ks;
            applyVO.LastModifierCode = entity.LastModifierCode;
            applyVO.LastModifyTime = entity.LastModifyTime;
            applyVO.mzys = entity.mzys;
            applyVO.OrganizeId = entity.OrganizeId;
            applyVO.ryrq = entity.ryrq;
            applyVO.sfz = entity.sfz;
            applyVO.sqzt = entity.sqzt;
            applyVO.ss = _ORApplyInfoExpandRepo.getApplyExtendByApplyno(OrganizeId, entity.Applyno);
            applyVO.ssbw = entity.ssbw;
            applyVO.sssj = entity.sssj;
            applyVO.xb = entity.xb;
            applyVO.xm = entity.xm;
            applyVO.ysgh = entity.ysgh;
            applyVO.ysxm = entity.ysxm;
            applyVO.zd = entity.zd;
            applyVO.zt = entity.zt;
            applyVO.zyh = entity.zyh;
            applyVO.ssmcn = entity.ssmcn;
            applyVO.ssdm = entity.ssdm;
            return Content(applyVO.ToJson());
        }

        public IActionResult DeleteForm(string keyValue)
        {
            //Cập nhật trạng thái phẫu thuật đã xếp lịch
            var ArrangeId = getArrangeIdByApplyId(keyValue);
            _ORArrangementRepo.DeleteForm(ArrangeId);
            //Hủy xếp lịch, cập nhật trạng thái đơn đăng ký(1.Đã đăng ký)
            _ORApplyInfoRepo.UpdateSqzt(keyValue, "1");
            return Success("Thao tác thành công.");
        }
    }
}