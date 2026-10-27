using System;
using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using System.Collections.Generic;

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Application.Interface.Inpatient;
using Newtouch.CIS.Proxy.CMMPlatform;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.HLYYRequest;

using Newtouch.Common.Web;
using Newtouch.CIS.Compat;
using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.DTO.OutputDto.Inpatient.API;
using Newtouch.Domain.DTO.OutputDto.Outpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.IRepository.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ViewModels;
using Newtouch.Domain.ViewModels.Outpatient;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.DoctorManage
{
    [Area("DoctorManage")]
    public class MedicineController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IDoctorserviceApp _doctorserviceApp;
        private readonly IDoctorserviceDmnService _doctorserviceDmnService;
        private readonly IQhdZnshSqtxRepo _qhdznshsqtxRepo;
        private readonly IMedicalRecordDmnService _medicalRecordDmnService;
        private readonly IInpatientLongTermOrderRepo _inpatientLongTermOrderRepo;
        private readonly IInpatientSTATOrderRepo _inpatientSTATOrderRepo;
        
        
        public MedicineController(IInpatientLongTermOrderRepo inpatientLongTermOrderRepo,
            IInpatientSTATOrderRepo _inpatientSTATOrderRepo, ISysConfigRepo sysConfigRepo, IDoctorserviceApp doctorserviceApp, IDoctorserviceDmnService doctorserviceDmnService, IQhdZnshSqtxRepo qhdznshsqtxRepo, IMedicalRecordDmnService medicalRecordDmnService)
     
        {
            this._sysConfigRepo = sysConfigRepo;
            this._doctorserviceApp = doctorserviceApp;
            this._doctorserviceDmnService = doctorserviceDmnService;
            this._qhdznshsqtxRepo = qhdznshsqtxRepo;
            this._medicalRecordDmnService = medicalRecordDmnService;
        
            this._inpatientLongTermOrderRepo = inpatientLongTermOrderRepo;
            this._inpatientSTATOrderRepo = _inpatientSTATOrderRepo;
           
        }

        /// <summary>
        /// 医嘱保存
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <param name="deldata"></param>
        /// <returns></returns>
        public IActionResult SubmitdoctorService(List<DoctorServiceRequestDto> reqdoctorservices, List<string> deldata)
        {
            string yzh="";
            _doctorserviceDmnService.SubmitdoctorServiceV2(OrganizeId, reqdoctorservices, deldata, out yzh);
            return Success("", yzh);
        }

        public IActionResult ValidateRepeat(List<DSrepeatRequestVO> req, string zyh)
        {
            var predata = _doctorserviceDmnService.DSTransferCL(req, OrganizeId);
            var data = _doctorserviceDmnService.DoctorserviceValidate(predata, zyh, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 修改医嘱时，根据医嘱Id获取详情
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="yzlx"></param>
        /// <returns></returns>
        public IActionResult GetYZDetail(string zyh, string yzId, string yzlx)
        {
            //string conflinktoOR = _sysConfigRepo.GetValueByCode("EnableLinkToOR", OrganizeId);
            //string b = _sysConfigRepo.GetByCode("EnableLinkToOR", OrganizeId).ToString();
            String conflinktoOR = Newtouch.CIS.Compat.AppConfig.Get("EnableLinkToOR");

            if (!string.IsNullOrWhiteSpace(conflinktoOR) && conflinktoOR == "true")
            {
                var datass = _doctorserviceDmnService.Ssupdate(yzId, zyh, OrganizeId);
                if (datass.Count > 0)
                {
                    throw new FailedException("Phẫu thuậtY lệnhKhông thểSửa");
                }
            }
            var data = _doctorserviceApp.GetYZDetail(zyh, yzId, yzlx, OrganizeId);

            if (data.DoctorServiceUIRequestDto == null || data.DoctorServiceUIRequestDto.Count <= 0) return Content(data.ToJson());
            var d = new List<DocservicekcslRequestDto>();
            foreach (var item in data.DoctorServiceUIRequestDto)
            {
                var e = new DocservicekcslRequestDto { ypCode = item.xmdm, lyyf = item.zxksdm };
                d.Add(e);
            }

            data.DrugStockInfo = Getcurrentkcsl(d);
            return Content(data.ToJson());
            
        }
        /// <summary>
        /// 医嘱诊断
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult OrderDiagnosisForm(string zyh, string brxz)
        {
            ViewBag.brxz = brxz;
            ViewBag.zyh = zyh;
            return View();
        }
        /// <summary>
        /// 构造并调用接口 获取最新库存数量
        /// </summary>
        /// <param name="ypcode"></param>
        /// <returns></returns>
        public string Getcurrentkcsl(List<DocservicekcslRequestDto> ypcodeList)
        {
            try
            {

                var request = new
                {
                    OrganizeId = this.OrganizeId,
                    yplist = ypcodeList,
                    ClientNo = Guid.NewGuid(),
                    TimeStamp = DateTime.Now.ToString()
                };
                var apires = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/Stock/query", request, autoAppendToken: false);
                if (apires.code == APIRequestHelper.ResponseResultCode.SUCCESS && apires.data != null)
                {
                    StockQueryResponseDTO successDoOrder = Newtouch.CIS.Compat.Tools.Json.ToObject<StockQueryResponseDTO>(apires.data.ToString()); //接口返回数据 
                    if (successDoOrder != null && successDoOrder.drugStockInfos.Count > 0)
                    {
                        //List<DrugStockInfo> successDoOrderYp = Newtouch.CIS.Compat.Tools.Json.ToList<DrugStockInfo>();
                        return successDoOrder.drugStockInfos.ToJson();
                    }

                }
                else
                {
                    return "F|GọiNhà thuốcKho thuốcGiao diệnthất bại";
                }

                return "T|Thực hiệnthành công";

            }
            catch (Exception ex)
            {
                return "F|" + ex.InnerException.ToString();
            }
        }

        public IActionResult TCMMedicine() {

            return View();
        }

        /// <summary>
        /// 事前提醒
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <returns></returns>
        public IActionResult GetqhdyzSqtxData(List<DoctorServiceRequestDto> reqdoctorservices , InpatientInfo brxx,string yzcfh)
        {
            var jlId = "";
            if (reqdoctorservices[0].yzlx == (int)EnumYzlx.Wz || reqdoctorservices[0].yzlx == (int)EnumYzlx.oper)
            {
                return Content(null);
            }
            var responsexml = _doctorserviceDmnService.GetqhdyzSqtxData(OrganizeId, reqdoctorservices, brxx, this.UserIdentity.rygh,this.UserIdentity.UserName,yzcfh,out jlId);
            var data = new
            {
                jlId= jlId,
                jydm = "5100",
                xmldata = responsexml
            };
            return Content(data.ToJson());
        }

        public IActionResult SaveLog(string logId, RESPONSEDATA responsedata)
        {
            var jlId = "";
            string responsexml = responsedata.XmlSerialize();
            var entity = new QhdZnshSqtxEntity
            {
                XmlResponse = responsexml
            };
            _qhdznshsqtxRepo.SubmitForm(entity, out jlId, logId);
            return Success();
        }
        /// <summary>
        /// 事前审核接口
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <returns></returns>
        public IActionResult GetPriorReviewData(List<DoctorServiceRequestDto> reqdoctorservices, InpatientInfo brxx,string yzcfh, string GetMAC)
        {
            if (reqdoctorservices[0].yzlx == (int)EnumYzlx.Wz || reqdoctorservices[0].yzlx == (int)EnumYzlx.oper)
            {
                return Content(null);
            }
            string HospitalCode = Newtouch.CIS.Compat.AppConfig.Get("OrganizeCodeSd");
            string HospitalName = Newtouch.CIS.Compat.AppConfig.Get("HospitalName");
            var response = _doctorserviceDmnService.GetPriorReviewData(OrganizeId, reqdoctorservices, brxx, this.UserIdentity.rygh, this.UserIdentity.UserName, HospitalCode, HospitalName,yzcfh,GetMAC);
            return Success(response);
        }
        /// <summary>
        /// 审核单据删除
        /// </summary>
        /// <returns></returns>
        public IActionResult DeletePriorReview(string zyh,string yzid,string yzlx,string GetMAC)
        {
            var response = _doctorserviceDmnService.DeletePriorReview(zyh, yzid, yzlx,OrganizeId, GetMAC);
            return Success(response);
        }
        /// <summary>
        /// 诊断查询服务接口
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDiagnoseData()
        {
            var response = _doctorserviceDmnService.GetDiagnoseData();
            return Success(response);
        }
        /// <summary>
        /// 病案审核服务
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <returns></returns>
        public IActionResult GetBashData(string zyh, string GetMAC)
        {
            var response = _doctorserviceDmnService.GetBashData(OrganizeId, zyh, this.UserIdentity.rygh, this.UserIdentity.UserName, GetMAC);
            return Success(response);
        }
        /// <summary>
        /// DRG服务
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <returns></returns>
        public IActionResult GetDrgData(string zyh, string GetMAC)
        {
            var response = _doctorserviceDmnService.GetDrgData(OrganizeId, zyh, this.UserIdentity.rygh, this.UserIdentity.UserName, GetMAC);
            return Success(response);
        }
        
        
        /// <summary>
        /// 获取医嘱合理用药信息接口
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="yzId"></param>
        /// <param name="yzlx"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public IActionResult GetYzHlyy(List<DoctorServiceRequestDto> reqdoctorservices, string yzh)
        {
            // 创建一个外部的 request 实例，只需要一个
            var request = new EngineReq
            {
                Type = "prescription",
                Patient = new PatientRecord
                {
                    DepartID = "",
                    Department = "",
                    BedNo = "",
                    PresType = "Y lệnh",
                    PresSource = "Nhập viện",
                    PresDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    PayType = "",
                    PatientNo = "",
                    PresNo = "",
                    Name = "",
                    DiagnoseID = "",
                    Diagnose = "",
                    IDCard = "",
                    Address = "",
                    PhoneNo = "",
                    Age = "",
                    Sex = "",
                    Height = "",
                    Weight = "",
                    BirthWeight = "",
                    PreviousHistory = "",
                    NowMedicalHistory = "",
                    Ccr = "",
                    Anaphylactogen = "",
                    AllergicHistory = "",
                    Pregnancy = "",
                    TimeOfPreg = "",
                    Disease = "2",
                    BreastFeeding = "",
                    Dialysis = "",
                    ProxName = "",
                    ProxIDCard = "",
                    DocID = "",
                    DocName = "",
                    TotalAmount = "",
                },
                Operation = new Operation
                {
                    OperationCode = "",
                    OperationName = "",
                    OperationStartTime = "",
                    OperationEndTime = "",
                    IncisionType = "",
                    IncisionStatus = "",
                    Inplant = "false"
                },
            };
            
            var prescriptions = new List<Prescription>();
            foreach (var doctorServiceRequestDto in reqdoctorservices)
            {
                string yzId = null;
                var yzlx = doctorServiceRequestDto.yzlb;

                // 根据医嘱类型获取 yzId
                if (yzlx.Equals("长"))
                {
                    var entity = _inpatientLongTermOrderRepo.FindEntity(p =>
                        p.yzh == yzh && p.zt == "1" && p.OrganizeId == OrganizeId);
                    yzId = entity.Id;
                }
                else if (yzlx.Equals("临"))
                {
                    var entity = _inpatientSTATOrderRepo.FindEntity(p =>
                        p.yzh == yzh && p.zt == "1" && p.OrganizeId == OrganizeId);
                    yzId = entity.Id;
                }

                var zyh = doctorServiceRequestDto.zyh;
                var data = _doctorserviceApp.GetYZDetail(zyh, yzId, yzlx, OrganizeId);
                var orgId = XMLSerializer.GenerateShortUUIDFromString(OrganizeId);

                // 更新 patient 信息
                request.Patient.DepartID = data.patientInfo.ksdm;
                request.Patient.Department = data.patientInfo.ksmc;
                request.Patient.PatientNo = data.patientInfo.zyh + '_' + orgId;
                request.Patient.PresNo = yzId;
                request.Patient.Name = data.patientInfo.xm;
                request.Patient.Diagnose = string.Join("|", data.patientInfo.zdmc);
                request.Patient.Age = data.patientInfo.age + "岁0月";
                request.Patient.Sex = data.patientInfo.sex;
                request.Patient.DocID = data.patientInfo.ysgh;
                request.Patient.DocName = data.patientInfo.ysxm;
                
                var pres = new Prescription();
                // 循环添加 prescriptions
                data.DoctorServiceUIRequestDto.ForEach(val =>
                {
                    var ypjx = _medicalRecordDmnService.GetYpjx(val.xmdm, UserIdentity.OrganizeId);
                     pres = new Prescription
                    {
                        Drug = val.xmdm + '_' + orgId,
                        DrugName = val.xmmc,
                        RegName = val.xmmc,
                        Specification = val.ypjl + val.dw,
                        Package = val.ypgg,
                        Quantity = val.sl,
                        PackUnit = val.dw,
                        UnitPrice = "",
                        Amount = "",
                        GroupNo = val.zh.ToString(), // 表示同组
                        FirstUse = "",
                        PrepForm = ypjx ?? "",
                        AdminRoute = val.yfmcval,
                        AdminArea = "无",
                        AdminFrequency = val.pcmc,
                        AdminDose = val.ypjl+val.dw,
                        AdminMethod = "",
                        Type = yzlx,
                        AdminGoal = "",
                        DocID = data.patientInfo.ysgh,
                        DocName = data.patientInfo.ysxm,
                        DocTitle = "",
                        DepartID = "",
                        Department = data.patientInfo.ksmc,
                        NurseName = "",
                        StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        EndTime = DateTime.Now.AddDays(val.ts.ToDouble()).ToString("yyyy-MM-dd HH:mm:ss"),
                        SpecialPromote = "",
                        ContinueDays = val.ts.ToString()
                    };
                   
                });
                prescriptions.Add(pres);
            } 
            // 把 prescriptions 添加到 request
            request.Prescriptions = prescriptions;
            var hlyyProxy = new HlyyProxy();
            var engineRes = hlyyProxy.engine(request);
            return Success("Tra cứuthành công", engineRes);
        }
       
        /// <summary>
        /// 获取合理用药药品说明书
        /// </summary>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public IActionResult GetHlyySms(string drugId)
        {
            var hlyyProxy = new HlyyProxy();
            var hisSmsJson = hlyyProxy.GetHisSmsJson(drugId);
            return Success("Tra cứuthành công", hisSmsJson);
        }


    }
}