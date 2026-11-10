using Newtouch.MR.ManageSystem.Domain.DTO.InputDto;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Init;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MRMS.Web.Models;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.MR.ManageSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.MRMS.Web.Areas.MRHomePage.Controllers
{
    [Area("MRHomePage")]
    public class MainRecordController : OrgControllerBase
    {
        public MainRecordController(IMainRecordDmnService MainRecordDmnService, IEMRDmnService EMRDmnService, IMrbasyzdRepo MrbasyzdRepo, IMrbasyRepo MrbasyRepo)
        {
            this._MainRecordDmnService = MainRecordDmnService;
            this._EMRDmnService = EMRDmnService;
            this._MrbasyzdRepo = MrbasyzdRepo;
            this._MrbasyRepo = MrbasyRepo;
        }

#pragma warning disable CS0649 // Field 'MainRecordController._MainRecordDmnService' is never assigned to, and will always have its default value null
        private readonly IMainRecordDmnService _MainRecordDmnService;
#pragma warning restore CS0649 // Field 'MainRecordController._MainRecordDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'MainRecordController._EMRDmnService' is never assigned to, and will always have its default value null
        private readonly IEMRDmnService _EMRDmnService;
#pragma warning restore CS0649 // Field 'MainRecordController._EMRDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0169 // The field 'MainRecordController._MrbasyzdRepo' is never used
        private readonly IMrbasyzdRepo _MrbasyzdRepo;
        private readonly IMrbasyRepo _MrbasyRepo;
#pragma warning restore CS0169 // The field 'MainRecordController._MrbasyzdRepo' is never used
        //GET: MRHomePage/MainRecord
        //public override IActionResult Index()
        //{
            
        //    return View();
        //}

        public IActionResult Main()
        {
            return View();
        }

        public IActionResult MainPreView()
        {
            return View();
        }
        public IActionResult MainEdit()
        {
            return View();
        }
        public IActionResult MainPatInfo()
        {
            return View();
        }
        public IActionResult MainDiag()
        {
            return View();
        }
        public IActionResult MainOplist()
        {
            return View();
        }
        public IActionResult MainOpEdit()
        {
            return View();
        }
        public IActionResult OutHosPatQuery()
        {
            return View();
        }
        public IActionResult MainZyInfo()
        {
            return View();
        }
        public IActionResult MainFeeInfo()
        {
            return View();
        }

        public IActionResult GetGridList(Pagination pagination,string ksrq,string jsrq,string bazt,string keyword, string sfzh,string cykb)
        {
            var list = _MainRecordDmnService.PatMainList(pagination, this.OrganizeId,ksrq, jsrq,bazt, null,keyword, sfzh,cykb);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public IActionResult GetEMRPatGridList(Pagination pagination,string keyword,string ksrq,string jsrq,string cyts,string blzt)
        {
            var cybz = Convert.ToInt32(EnumZYBZ.Ycy);
            var list = _EMRDmnService.GetPatList(pagination, keyword, null, cyts,blzt, this.UserIdentity.rygh, this.OrganizeId, cybz);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public IActionResult GetFormJson(string keyValue,string zyh)
        {
            BasyVO entity = new BasyVO();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity = _MainRecordDmnService.GetMainRecord(OrganizeId, keyValue);
            }
            else if(!string.IsNullOrWhiteSpace(zyh))
            {               
                entity = _MainRecordDmnService.GetMainRecordbybrjbxx(OrganizeId, zyh);
            }
            
            return Content(entity.ToJson());
        }

        public IActionResult GetFormJsonFee(string keyValue, string zyh)
        {
            BasyVO entity = new BasyVO();
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                var ety = _MrbasyRepo.FindEntity(p => p.ZYH == zyh && p.OrganizeId == OrganizeId && p.zt == "1");
                if (ety != null)
                {
                    keyValue = ety.Id;
                    entity = _MainRecordDmnService.GetMainRecord(OrganizeId, keyValue);
                }
                else
                {
                    entity = _MainRecordDmnService.GetMainRecordbybrjbxx(OrganizeId, zyh);
                }
            }
            else {
                entity = _MainRecordDmnService.GetMainRecord(OrganizeId, keyValue);
            }

            // FA-008: GetMainRecord/GetMainRecordbybrjbxx tra null khi keyValue/
            // zyh khong co hang (vd keyValue=BAH thay vi Id); truyen null vao
            // GetMainRecordZyFeeDetail gay NRE (error JSON "Object reference...").
            if (entity == null)
            {
                return Error("Không tìm thấy trang đầu bệnh án.");
            }

           // var data = _MainRecordDmnService.GetMainRecordZyFeeDetail(entity, OrganizeId, zyh);
            var data = _MainRecordDmnService.GetMainRecordZyFeeDetail(entity, OrganizeId, zyh);
            return Content(data.ToJson());
        }

        public IActionResult GetZdGridList(string bah,string zyh)
        {
            var list = _MainRecordDmnService.GetDiagLsit(OrganizeId,bah,zyh);
            if (list.Count == 0 && !string.IsNullOrWhiteSpace(bah))
            {
                var exists = _MrbasyzdRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.BAH == bah && p.zt == "1");
                if (exists == null)
                {
                    list = _MainRecordDmnService.GetPatHisZDInfo(bah,zyh, OrganizeId,2);
                }
            }
            var data = new
            {
                rows = list,
                page = 1,
                records = 1,
                total = 1
            };
            return Content(data.ToJson());
        }

        public IActionResult GetOpGridList(string bah, string zyh)
        {
            var list = _MainRecordDmnService.GetOPLsit(OrganizeId, bah, zyh);
            if(list.Count==0 && !string.IsNullOrWhiteSpace(bah))
            {
                var exists = _MrbasyRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.BAH == bah && p.zt == "1");
                if (exists == null)
                {
                    list = _MainRecordDmnService.GetPatHisOperInfo(bah,zyh, OrganizeId);
                }
            }
            var data = new
            {
                rows = list,
                page = 1,
                records = 1,
                total = 1
            };
            return Content(data.ToJson());
        }


        public IActionResult SubmitZdList(List<BasyZdDto> dto,string zyh,string Code)
        {
            var data = "";
            // G20: dto rỗng trước đây vẫn trả {"data":""} mà UI hiểu là lưu thành công.
            if (dto == null || dto.Count == 0)
            {
                return Error("Danh sách chẩn đoán không được để trống.");
            }
            //Thông tin bệnh nhân đã thanh toán
            var jsbrxx = _MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
            //Có đồng bộ thông tin tham số động không
            var dtcsxx = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);
            // G20: DiagnosticSave trả 0 dòng (Code lạ) thì dtcsxx[0] ném IndexOutOfRange.
            if (dtcsxx == null || dtcsxx.Count == 0)
            {
                return Error("Không tìm thấy thông tin tham số chẩn đoán.");
            }

            //Kiểm tra bệnh nhân đã thanh toán chưa
            if (jsbrxx.Count > 0)
            {
                //Tham số: thời gian sửa trống thì lấy thời gian tạo
                var uptime = dtcsxx[0].LastModifyTime.ToString() == null || dtcsxx[0].LastModifyTime.ToString() == "" ? dtcsxx[0].CreateTime : dtcsxx[0].LastModifyTime;
                //Kiểm tra thời gian thanh toán có lớn hơn thời gian sửa hoặc tạo của tham số không
                if ((jsbrxx[0].CreateTime > uptime) && (dtcsxx[0].Value == "OFF" || dtcsxx[0].Value == "off"))
                {
                    data = "Bệnh nhân đã quyết toán không thể thêm thông tin chẩn đoán";
                }
                //Kiểm tra thời gian thanh toán có nhỏ hơn thời gian sửa hoặc tạo của tham số không
                else if ((jsbrxx[0].CreateTime < uptime) && (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on"))
                {
                    //Lưu chẩn đoán
                    _MainRecordDmnService.ZdListSubmit(dto, OrganizeId);
                    data = "Lưu thông tin chẩn đoán thành công";
                }
                else
                {
                    //return Success("Không thể thêm thông tin chẩn đoán");
                    data = "Không thể thêm thông tin chẩn đoán";
                }
            }
            else
            {
                // G20: chưa quyết toán thì lưu trực tiếp — trước đây rơi qua, trả
                // {"data":""} mà không lưu dòng nào (UI vẫn báo thành công).
                _MainRecordDmnService.ZdListSubmit(dto, OrganizeId);
                data = "Lưu thông tin chẩn đoán thành công";
            }
            data = "{\"data\":\"" + data + "\"}";
            return Content(data.ToJson().ToString());
        }

        public IActionResult SubmitOpList(List<BasyOpDto> dto)
        {
            // G20: dto rỗng trước đây vẫn Success "Lưu phẫu thuật thành công".
            if (dto == null || dto.Count == 0)
            {
                return Error("Danh sách phẫu thuật không được để trống.");
            }
            _MainRecordDmnService.OpListSubmit(dto, OrganizeId);
            return Success("Lưu phẫu thuật thành công");
        }

        public IActionResult SubmitPatBasic(BasyVO dto,BasyRelVO reldto, string keyValue) {
            if(!string.IsNullOrWhiteSpace(keyValue))
            {
                if(!string.IsNullOrWhiteSpace(dto.Id) && dto.Id!=keyValue)
                {
                    return Error("Thông tin bệnh án bất thường, Vui lòng làm mới và thử lại");
                }
                else
                {
                    dto.Id = keyValue;
                }
            }
            _MainRecordDmnService.PatBasicSubmit(dto,reldto, OrganizeId,this.UserIdentity.rygh);
            return Success();
        }

        public IActionResult SubmitMR(string keyValue, List<BasyOpDto> opdto, List<BasyZdDto> zddto,string zyh,string Code)
        {
            //_MainRecordDmnService.PatBasicSubmit(dto, reldto, OrganizeId, this.UserIdentity.rygh);
            // G20: cả hai dto null trước đây vẫn Success() (không lưu gì).
            if ((zddto == null || zddto.Count == 0) && (opdto == null || opdto.Count == 0))
            {
                return Error("Không có dữ liệu để lưu.");
            }
            if(zddto!=null&&zddto.Count>0)
            {
                //_MainRecordDmnService.ZdListSubmit(zddto, OrganizeId);
                //Thông tin bệnh nhân đã thanh toán
                var jsbrxx = _MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
                //Có đồng bộ thông tin tham số động không
                var dtcsxx = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);
                // G20: DiagnosticSave trả 0 dòng (Code lạ) thì dtcsxx[0] ném
                // IndexOutOfRange trước cả nhánh op — chặn bằng lỗi tiếng Việt.
                if (dtcsxx == null || dtcsxx.Count == 0)
                {
                    return Error("Không tìm thấy thông tin tham số chẩn đoán.");
                }

                //Kiểm tra bệnh nhân đã thanh toán chưa
                if (jsbrxx.Count > 0)
                {
                    //Tham số: thời gian sửa trống thì lấy thời gian tạo
                    var uptime = dtcsxx[0].LastModifyTime.ToString() == null || dtcsxx[0].LastModifyTime.ToString() == "" ? dtcsxx[0].CreateTime : dtcsxx[0].LastModifyTime;
                    //Kiểm tra thời gian thanh toán có lớn hơn thời gian sửa hoặc tạo của tham số không
                    if ((jsbrxx[0].CreateTime > uptime) && (dtcsxx[0].Value == "OFF" || dtcsxx[0].Value == "off"))
                    {
                        //data = "Bệnh nhân đã quyết toán không thể thêm thông tin chẩn đoán";
                    }
                    //Kiểm tra thời gian thanh toán có nhỏ hơn thời gian sửa hoặc tạo của tham số không
                    else if ((jsbrxx[0].CreateTime < uptime) && (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on"))
                    {
                        //Lưu chẩn đoán
                        _MainRecordDmnService.ZdListSubmit(zddto, OrganizeId);
                        //data = "Lưu thông tin chẩn đoán thành công";
                    }
                    else
                    {
                        //return Success("Không thể thêm thông tin chẩn đoán");
                        //data = "Không thể thêm thông tin chẩn đoán";
                    }
                }
                else
                {
                    // G20: chưa quyết toán thì lưu trực tiếp (cùng fix như
                    // SubmitZdList #1b — trước đây rơi qua, zd không lưu mà op vẫn lưu).
                    _MainRecordDmnService.ZdListSubmit(zddto, OrganizeId);
                }
            }
            if (opdto != null && opdto.Count > 0)
            {
                _MainRecordDmnService.OpListSubmit(opdto, OrganizeId);
            }
            return Success();
        }


        //------------------Lưu trữ Nút
        public IActionResult guidang(string dataId,string ZYH,string XM) {
           var data= _MrbasyRepo.Updatebazt(dataId,ZYH,XM);
            return Content(data.ToJson());
        }

        public IActionResult CXguidang(string dataId, string ZYH, string XM) {
            var LastModifierCode = UserIdentity.UserName;
            var LastModifyTime = DateTime.Now;
            var data1 = _MrbasyRepo.CXUpdatebazt(dataId, ZYH, XM, this.OrganizeId, LastModifierCode, LastModifyTime);
            return Content(data1.ToJson());
        }
        public IActionResult CXGDZT(string dataId, string ZYH, string XM) {
            var LastModifierCode = UserIdentity.UserName;
            var LastModifyTime = DateTime.Now;
            // G20: trước đây gọi nhầm CXUpdatebazt (GDRQ=null + đụng EMR zy_brjbxx)
            // trong khi repo có CXGDZT riêng (GDRQ=now, chỉ đụng MRMS).
            var data1 = _MrbasyRepo.CXGDZT(dataId, ZYH, XM, this.OrganizeId, LastModifierCode, LastModifyTime);
            return Content(data1.ToJson());
        }


        private void EMRBasyPrintReportCom()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.topOrgId = Constants.TopOrganizeId;
            ViewBag.ReportServerHOST = AppSettings.GetConfig("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //Có phải là quản trị viên bệnh viện không
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }


        public IActionResult AjaxValueIndex() {
          
            var str = "{\"orgId\":\"" + this.OrganizeId + "\",\"ReportServerHOST\":\""+ AppSettings.GetConfig("ReportServer.HOST") + "\"}";
            return Content(str.ToString());
        }


        public IActionResult SettlementQuery(string zyh) {
           var data=_MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult MedicalRecordExportQuery(string kssj,string jssj)
        {
            var data = _MainRecordDmnService.MedicalRecordExportQuery(kssj, jssj, this.OrganizeId);
            var data2 = _MainRecordDmnService.MedicalRecordExportFYQuery(kssj, jssj, this.OrganizeId);
            return Success(data + data2);
        }
        public IActionResult DiagnosticSave(string Code)
        {
            var data = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);
            return Content(data.ToJson());
        }
    }
}