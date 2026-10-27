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
            if (dto!=null && dto.Count>0)
            {
                //结算Thông tin bệnh nhân
                var jsbrxx = _MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
                //是否同步动态参数信息
                var dtcsxx = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);

                //判断是否结算病 người
                if (jsbrxx.Count > 0)
                {
                    //参数Sửa giờ间为空 giờ取创建 giờ间
                    var uptime = dtcsxx[0].LastModifyTime.ToString() == null || dtcsxx[0].LastModifyTime.ToString() == "" ? dtcsxx[0].CreateTime : dtcsxx[0].LastModifyTime;
                    //判断结算 giờ间大于参数的Sửa或创建 giờ间
                    if ((jsbrxx[0].CreateTime > uptime) && (dtcsxx[0].Value == "OFF" || dtcsxx[0].Value == "off"))
                    {
                        data = "Bệnh nhân đã quyết toán không thể thêm thông tin chẩn đoán";
                    }
                    //判断结算 giờ间小于参数的Sửa或创建 giờ间
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
                //_MainRecordDmnService.ZdListSubmit(dto, OrganizeId);
            }
            data = "{\"data\":\"" + data + "\"}";
            return Content(data.ToJson().ToString());
        }

        public IActionResult SubmitOpList(List<BasyOpDto> dto)
        {
            if(dto!=null&&dto.Count>0)
            {
                _MainRecordDmnService.OpListSubmit(dto, OrganizeId);
            }
            return Success("Lưu phẫu thuật thành công");
        }

        public IActionResult SubmitPatBasic(BasyVO dto,BasyRelVO reldto, string keyValue) {
            if(!string.IsNullOrWhiteSpace(keyValue))
            {
                if(!string.IsNullOrWhiteSpace(dto.Id) && dto.Id!=keyValue)
                {
                    return Error("Thông tin bệnh án bất thường，Vui lòng làm mới và thử lại");
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
            if(zddto!=null&&zddto.Count>0)
            {
                //_MainRecordDmnService.ZdListSubmit(zddto, OrganizeId);
                //结算Thông tin bệnh nhân
                var jsbrxx = _MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
                //是否同步动态参数信息
                var dtcsxx = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);

                //判断是否结算病 người
                if (jsbrxx.Count > 0)
                {
                    //参数Sửa giờ间为空 giờ取创建 giờ间
                    var uptime = dtcsxx[0].LastModifyTime.ToString() == null || dtcsxx[0].LastModifyTime.ToString() == "" ? dtcsxx[0].CreateTime : dtcsxx[0].LastModifyTime;
                    //判断结算 giờ间大于参数的Sửa或创建 giờ间
                    if ((jsbrxx[0].CreateTime > uptime) && (dtcsxx[0].Value == "OFF" || dtcsxx[0].Value == "off"))
                    {
                        //data = "Bệnh nhân đã quyết toán không thể thêm thông tin chẩn đoán";
                    }
                    //判断结算 giờ间小于参数的Sửa或创建 giờ间
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
            }
            if (opdto != null && opdto.Count > 0)
            {
                _MainRecordDmnService.OpListSubmit(opdto, OrganizeId);
            }
            return Success();
        }


        //------------------Lưu trữNút
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
            var data1 = _MrbasyRepo.CXUpdatebazt(dataId, ZYH, XM, this.OrganizeId, LastModifierCode, LastModifyTime);
            return Content(data1.ToJson());
        }


        private void EMRBasyPrintReportCom()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.topOrgId = Constants.TopOrganizeId;
            ViewBag.ReportServerHOST = AppSettings.GetConfig("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //是否是医院管理员
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