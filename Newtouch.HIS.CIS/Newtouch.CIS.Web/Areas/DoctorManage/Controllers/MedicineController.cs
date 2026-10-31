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
        /// Y lệnhLưu
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
        /// SửaY lệnhkhi,TheoY lệnhIdLấyChi tiết
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
        /// Y lệnhChẩn đoán
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
        /// Cấu trúcvàgọigiao diện LấynhấtmớiTồn khoSố lượng
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
                    StockQueryResponseDTO successDoOrder = Newtouch.CIS.Compat.Tools.Json.ToObject<StockQueryResponseDTO>(apires.data.ToString()); //giao diệnQuay lạiDữ liệu 
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
        /// nhắc trước
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
        /// việctrướcPhê duyệtgiao diện
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
        /// Phê duyệtphiếucứXóa
        /// </summary>
        /// <returns></returns>
        public IActionResult DeletePriorReview(string zyh,string yzid,string yzlx,string GetMAC)
        {
            var response = _doctorserviceDmnService.DeletePriorReview(zyh, yzid, yzlx,OrganizeId, GetMAC);
            return Success(response);
        }
        /// <summary>
        /// Chẩn đoánTra cứuphụcvụgiao diện
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDiagnoseData()
        {
            var response = _doctorserviceDmnService.GetDiagnoseData();
            return Success(response);
        }
        /// <summary>
        /// bệnhánPhê duyệtphụcvụ
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <returns></returns>
        public IActionResult GetBashData(string zyh, string GetMAC)
        {
            var response = _doctorserviceDmnService.GetBashData(OrganizeId, zyh, this.UserIdentity.rygh, this.UserIdentity.UserName, GetMAC);
            return Success(response);
        }
        /// <summary>
        /// DRGphụcvụ
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <returns></returns>
        public IActionResult GetDrgData(string zyh, string GetMAC)
        {
            var response = _doctorserviceDmnService.GetDrgData(OrganizeId, zyh, this.UserIdentity.rygh, this.UserIdentity.UserName, GetMAC);
            return Success(response);
        }
        
        
        /// <summary>
        /// LấyY lệnhhợplýdùngthuốcthông tingiao diện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="yzId"></param>
        /// <param name="yzlx"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public IActionResult GetYzHlyy(List<DoctorServiceRequestDto> reqdoctorservices, string yzh)
        {
            // tạomộtngoàibộcủa request thựcVD,chỉcầnmột
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

                // TheoKiểu y lệnhLấy yzId
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

                // Cập nhật patient thông tin
                request.Patient.DepartID = data.patientInfo.ksdm;
                request.Patient.Department = data.patientInfo.ksmc;
                request.Patient.PatientNo = data.patientInfo.zyh + '_' + orgId;
                request.Patient.PresNo = yzId;
                request.Patient.Name = data.patientInfo.xm;
                request.Patient.Diagnose = string.Join("|", data.patientInfo.zdmc);
                request.Patient.Age = data.patientInfo.age + " tuổi 0 tháng";
                request.Patient.Sex = data.patientInfo.sex;
                request.Patient.DocID = data.patientInfo.ysgh;
                request.Patient.DocName = data.patientInfo.ysxm;
                
                var pres = new Prescription();
                // Lặp lạiThêm prescriptions
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
                        GroupNo = val.zh.ToString(), // biểu thịCùng nhóm
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
            //  prescriptions Thêmđến request
            request.Prescriptions = prescriptions;
            var hlyyProxy = new HlyyProxy();
            var engineRes = hlyyProxy.engine(request);
            return Success("Tra cứuthành công", engineRes);
        }
       
        /// <summary>
        /// LấyhợplýdùngthuốcThuốcMô tảsách
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