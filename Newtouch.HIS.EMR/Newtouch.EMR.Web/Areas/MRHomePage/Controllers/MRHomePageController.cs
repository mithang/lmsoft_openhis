using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.EMR.Web.Helpers;
using Newtonsoft.Json;

using Newtouch.EMR.Domain.DTO.InputDto;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects.MRHomePage;
using Newtouch.EMR.Infrastructure.EnumMR;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.EMR.Web.Areas.MRHomePage.Controllers
{
    [Area("MRHomePage")]
    public class MRHomePageController : OrgControllerBase
    {
        private readonly IMRHomePageDmnService _MainRecordDmnService;
        private readonly IMrbasyzdRepo _MrbasyzdRepo;
        private readonly IMrbasyRepo _MrbasyRepo;
        private readonly IZybrjbxxDmnService _zybrjbxxDmnService;
        private readonly IZymeddocsrelationRepo _zymeddocsrelationRep;
        private readonly Ibl_bllxRepo _BllxRepo;
        private readonly IYBInterfaceDmnService _yBInterfaceDmnService;
        private readonly ICommonDmnService _CommonDmnService;

        public MRHomePageController(IMRHomePageDmnService mainRecordDmnService, IMrbasyzdRepo mrbasyzdRepo, IMrbasyRepo mrbasyRepo, IZybrjbxxDmnService zybrjbxxDmnService, IZymeddocsrelationRepo zymeddocsrelationRep, Ibl_bllxRepo bllxRepo, IYBInterfaceDmnService yBInterfaceDmnService, ICommonDmnService commonDmnService)
        {
            this._MainRecordDmnService = mainRecordDmnService;
            this._MrbasyzdRepo = mrbasyzdRepo;
            this._MrbasyRepo = mrbasyRepo;
            this._zybrjbxxDmnService = zybrjbxxDmnService;
            this._zymeddocsrelationRep = zymeddocsrelationRep;
            this._BllxRepo = bllxRepo;
            this._yBInterfaceDmnService = yBInterfaceDmnService;
            this._CommonDmnService = commonDmnService;
        }
        private readonly string YBOrgCode = EmrConfig.GetAppConfigValue("YBOrgCode");

        private static string UploadURL = EmrConfig.GetAppConfigValue("MRUploadURL");

        //// GET: MRHomePage/MainRecord
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Main()
        {
            // FIX 2026-10-26: Request.Query returns StringValues in ASP.NET
            // Core (plain string in Framework). Main.cshtml renders MainEdit
            // as a partial, whose line 57 evaluates (justshow == null ? ...).
            // With a dynamic StringValues value the runtime binder finds the
            // StringValues == overloads ambiguous and throws. Store a plain
            // string so the comparison behaves exactly as in Framework.
            ViewBag.justshow = Request.Query["justshow"].ToString();
            ReportingServiceCom();
            return View();
        }

        //public IActionResult MainPreView()
        //{
        //    return View();
        //}
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
		public IActionResult MainFYInfo()
		{
			return View();
		}
		public IActionResult GetGridList(Pagination pagination, string ksrq, string jsrq, string bazt, string keyword, string cyts)
        {
            var list = _MainRecordDmnService.PatMainList(pagination, this.OrganizeId, ksrq, jsrq, bazt, null, keyword, cyts);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public IActionResult GetEMRPatGridList(Pagination pagination, string keyword, string ksrq, string jsrq, string cyts, string blzt)
        {
            var cybz = Convert.ToInt32(Newtouch.Infrastructure.EnumZYBZ.Ycy);
            var list = _zybrjbxxDmnService.GetPatList(pagination, keyword, null, cyts, blzt, this.UserIdentity.rygh, this.OrganizeId, cybz,"MRMS");
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public IActionResult GetFormJson(string keyValue, string zyh)
        {
            BasyVO entity = new BasyVO();    
             if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity = _MainRecordDmnService.GetMainRecord(OrganizeId, keyValue);
            }
            else if (!string.IsNullOrWhiteSpace(zyh))
            {
                entity = _MainRecordDmnService.GetMainRecordbybrjbxx(OrganizeId, zyh);
            }

            return Content(entity.ToJson());
        }

        public IActionResult GetFormJsonFee(string keyValue, string zyh)
        {
            BasyVO vo = new BasyVO();
            var entity = _MainRecordDmnService.GetMainRecordZyFeeDetail(vo, OrganizeId, zyh);
            return Content(entity.ToJson());
        }

        public IActionResult GetZdGridList(string bah, string zyh)
        {
            var list = _MainRecordDmnService.GetDiagLsit(OrganizeId, bah, zyh);
            if (list.Count == 0 && !string.IsNullOrWhiteSpace(bah))
            {
                var exists = _MrbasyzdRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.BAH == bah && p.zt == "1");
                if (exists == null)
                {
                    list = _MainRecordDmnService.GetPatHisZDInfo(bah, zyh, OrganizeId, 2);
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
            if (list.Count == 0 && !string.IsNullOrWhiteSpace(bah))
            {
                var exists = _MrbasyRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.BAH == bah && p.zt == "1");
                if (exists == null)
                {
                    list = _MainRecordDmnService.GetPatHisOperInfo(bah, zyh, OrganizeId);
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
            // G19: dto null/rỗng trước đây trả Success rỗng (Success giả) mà
            // không gọi service — service đã có guard nhưng controller không tới.
            if (dto == null || dto.Count == 0)
            {
                return Error("Danh sách chẩn đoán không được để trống");
            }
            var data = "";
            if (dto != null && dto.Count > 0)
            {
                //Thông tin bệnh nhân đã thanh toán
                var jsbrxx = _MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
                //Có đồng bộ thông tin tham số động hay không
                var dtcsxx = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);
                //Bệnh nhân đã thanh toán có được phép thêm/sửa chẩn đoán hay không ON Có thể chỉnh sửa  OFF Không thể chỉnh sửa
                var outpatDiagModify = _MainRecordDmnService.DiagnosticSave("OutpatDiagModifyOpen", this.OrganizeId);
                //Phán đoán có phải bệnh nhân đã thanh toán hay không
                if (jsbrxx.Count > 0)
                {
                    if (outpatDiagModify.Count > 0)
                    {
                        if (outpatDiagModify[0].Value == "OFF" || outpatDiagModify[0].Value == "off")
                        {
                            data = "Bệnh nhân đã quyết toán không được thêm/sửa thông tin chẩn đoán";
                        }
                        else
                        {
                            //Lưu chẩn đoán
                            _MainRecordDmnService.ZdListSubmit(dto, OrganizeId, this.UserIdentity.rygh);
                            data = "Lưu thông tin chẩn đoán thành công";
                            if (dtcsxx.Count > 0)
                            {
                                if (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on")
                                {
                                    //Đồng bộ thông tin chẩn đoán sang CIS Chẩn đoán ra viện
                                    _MainRecordDmnService.SynchronizationZD(zyh, this.OrganizeId);
                                }
                            }
                        }
                    }
                    else {
                        //Lưu chẩn đoán
                        _MainRecordDmnService.ZdListSubmit(dto, OrganizeId, this.UserIdentity.rygh);
                        data = "Lưu thông tin chẩn đoán thành công";
                        if (dtcsxx.Count > 0)
                        {
                            if (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on")
                            {
                                //Đồng bộ thông tin chẩn đoán sang CIS Chẩn đoán ra viện
                                _MainRecordDmnService.SynchronizationZD(zyh, this.OrganizeId);
                            }
                        }

                    }
                }
                else {
                    //Khi chưa thanh toán và tham số được bật thì đồng bộ và lưu chẩn đoán
                    _MainRecordDmnService.ZdListSubmit(dto, OrganizeId, this.UserIdentity.rygh);
                    data = "Lưu thông tin chẩn đoán thành công";

                    if (dtcsxx.Count > 0) {
                        if (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on")
                        {
                            //Đồng bộ thông tin chẩn đoán
                            _MainRecordDmnService.SynchronizationZD(zyh, this.OrganizeId);
                        }
                    }
                }
            }
            data = "{\"state\":\"success\",\"data\":\"" + data + "\"}";
            return Content(data.ToJson().ToString());
        }

        public IActionResult SubmitOpList(List<BasyOpDto> dto)
        {
            // G19: dto null/rỗng trước đây trả Success mà không lưu gì.
            if (dto == null || dto.Count == 0)
            {
                return Error("Danh sách phẫu thuật không được để trống");
            }
            _MainRecordDmnService.OpListSubmit(dto, OrganizeId, this.UserIdentity.rygh);
            return Success("Lưu phẫu thuật thành công");
        }
        /// <summary>
        /// Trang đầu bệnh án-Lưu thông tin cơ bản
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="reldto"></param>
        /// <param name="keyValue"></param>
        /// <param name="mbbh"></param>
        /// <returns></returns>
        public IActionResult SubmitPatBasic(BasyVO dto, BasyRelVO reldto, string keyValue,string mbbh)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                if (!string.IsNullOrWhiteSpace(dto.Id) && dto.Id != keyValue)
                {
                    return Error("Thông tin bệnh án bất thường，Vui lòng làm mới và thử lại");
                }
                else
                {
                    dto.Id = keyValue;
                }
            }
            _MainRecordDmnService.PatBasicSubmit(dto, reldto,mbbh, OrganizeId, this.UserIdentity.rygh,this.UserIdentity.UserName);
            return Success();
        }

        public IActionResult SubmitMR(string keyValue, List<BasyOpDto> opdto, List<BasyZdDto> zddto,string zyh,string Code)
        {
            try
            {
                // G19: cả hai danh sách đều rỗng trước đây trả Success mà không lưu gì.
                if ((zddto == null || zddto.Count == 0) && (opdto == null || opdto.Count == 0))
                {
                    return Error("Danh sách chẩn đoán và phẫu thuật không được để trống");
                }
                //_MainRecordDmnService.PatBasicSubmit(dto, reldto, OrganizeId, this.UserIdentity.rygh);
                if (zddto != null && zddto.Count > 0)
                {
//_MainRecordDmnService.ZdListSubmit(zddto, OrganizeId);

                    //Thông tin bệnh nhân đã thanh toán
                    var jsbrxx = _MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
                    //Có đồng bộ thông tin tham số động hay không
                    var dtcsxx = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);
                    //Bệnh nhân đã thanh toán có được phép thêm/sửa chẩn đoán hay không
                    var outpatDiagModify = _MainRecordDmnService.DiagnosticSave("OutpatDiagModifyOpen", this.OrganizeId);
                    //Phán đoán có phải bệnh nhân đã thanh toán hay không
                    if (jsbrxx.Count > 0)
                    { 
                        if (outpatDiagModify.Count > 0)
                        {
                            // G19: điều kiện cũ `!= "OFF" || != "off"` luôn true
                            // (tautology) nên cổng chặn sửa khi đã quyết toán không
                            // bao giờ đóng — sửa thành `==` như SubmitZdList: OFF
                            // thì chặn, còn lại thì lưu + đồng bộ khi DiagnosticSave ON.
                            if (outpatDiagModify[0].Value == "OFF" || outpatDiagModify[0].Value == "off")
                            {
                                return Error("Bệnh nhân đã quyết toán không được thêm/sửa thông tin chẩn đoán");
                            }
                            //Lưu chẩn đoán
                            _MainRecordDmnService.ZdListSubmit(zddto, OrganizeId, this.UserIdentity.rygh);
                            if (dtcsxx.Count > 0) {
                                if (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on")
                                {
                                    //Đồng bộ thông tin chẩn đoán
                                    _MainRecordDmnService.SynchronizationZD(zyh, this.OrganizeId);
                                }
                            }
                        }
                        else {
                            //Lưu chẩn đoán
                            _MainRecordDmnService.ZdListSubmit(zddto, OrganizeId, this.UserIdentity.rygh);
                            if (dtcsxx.Count > 0) {
                                if (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on")
                                {
                                    //Đồng bộ thông tin chẩn đoán
                                    _MainRecordDmnService.SynchronizationZD(zyh, this.OrganizeId);
                                }
                            }
                        }
                    }
                    else
                    {
                        //Lưu chẩn đoán
                        _MainRecordDmnService.ZdListSubmit(zddto, OrganizeId, this.UserIdentity.rygh);
                        if (dtcsxx.Count > 0)
                        {
                            if (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on")
                            {
                                //Đồng bộ thông tin chẩn đoán
                                _MainRecordDmnService.SynchronizationZD(zyh, this.OrganizeId);
                            }
                        }
                    }
                }
                if (opdto != null && opdto.Count > 0)
                {
                    _MainRecordDmnService.OpListSubmit(opdto, OrganizeId, this.UserIdentity.rygh);
                }
                return Success();
            }
            catch (FailedException ex)
            {
                // G19: FailedException chỉ set Msg (không set Message của
                // Exception) nên Error(e.Message) cũ trả về chuỗi
                // "Exception of type '...FailedException' was thrown.".
                return Error(ex.Msg ?? ex.Message);
            }
            catch (Exception)
            {
                // G19: không lộ message SQL/NRE thô ra client.
                return Error("Lưu trang đầu bệnh án thất bại, vui lòng thử lại.");
            }

        }
        public IActionResult HomePageUploadJS(string zyh, string keyValue,string jydm)
        {
            QHDSmartCheckInput data = new QHDSmartCheckInput();
            var ety = _MainRecordDmnService.GetHomePageforYB(OrganizeId, zyh).FirstOrDefault();
            if (ety == null)
            {
                return Error("Vui lòng điền đầy đủ bệnh án, lưu rồi mới tải lên");

            }
            string msg = DocumentAuditYB<YB_7600>(ety);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                return Error(msg);
            }
            else {
                QHDSmartCheckInput rootinput = new QHDSmartCheckInput();
                if (jydm != "7610")
                {
                    YBRequestBase<YB_7600> YBJKDATA = new YBRequestBase<YB_7600>();
                    YBJKDATA.REQUESTDATA = new YBRequestSingle<YB_7600>
                    {
                        MSGNO = "7600",
                        AKB020 = YBOrgCode,
                        OPERID = this.UserIdentity.rygh,
                        OPERNAME = UserIdentity.UserName,
                        PERIODNO = "",
                        MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                        GRANTID = "",
                        INPUT = ety
                    };
                    rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    rootinput.jydm = "7600";
                }
                else
                {
                    var zdinfo = _yBInterfaceDmnService.GetBasyZdInfo(OrganizeId, zyh);
                    if (zdinfo != null && zdinfo.Count > 0) {
                        YBRequestBase<YB_7610> YBJKDATA_ZD = new YBRequestBase<YB_7610>();
                        YBJKDATA_ZD.REQUESTDATA = new YBRequestRows<YB_7610>
                        {
                            MSGNO = "7610",
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = zdinfo.ToList()
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA_ZD);
                        rootinput.jydm = "7610";
                    }
                    
                }
                data = rootinput;                
            }
            return Content(data.ToJson());
        }
        public IActionResult HomePageUpload(string zyh,string keyValue)
        {
            var ety = _MainRecordDmnService.GetHomePageforYB(OrganizeId, zyh).FirstOrDefault();
            if (ety == null)
            {
                return Error("Vui lòng điền đầy đủ bệnh án, lưu rồi mới tải lên");

            }
            string msg = DocumentAuditYB<YB_7600>(ety);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                return Error(msg);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(UploadURL))
                {
                    QHDSmartCheckInput rootinput = new QHDSmartCheckInput();
                    YBRequestBase<YB_7600> YBJKDATA = new YBRequestBase<YB_7600>();
                    YBJKDATA.REQUESTDATA = new YBRequestSingle<YB_7600>
                    {
                        MSGNO = "7600",
                        AKB020 = YBOrgCode,
                        OPERID = this.UserIdentity.rygh,
                        OPERNAME = UserIdentity.UserName,
                        PERIODNO = "",
                        MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                        GRANTID = "",
                        INPUT = ety
                    };
                    rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    rootinput.jydm = "7600";
                    // G19: PostJsonString ném thẳng ra ngoài khi host YB không
                    // nghe (STG: 127.0.0.1:33333 không có server) — bắt lại
                    // thành Error tiếng Việt thay vì lỗi thô/500.
                    string respstr;
                    try
                    {
                        respstr = EmrHttpHelper.PostJsonString(UploadURL + "api/QHDSmartCheck/MedicalRecordUpload", JsonConvert.SerializeObject(rootinput));
                    }
                    catch (Exception)
                    {
                        return Error("Dịch vụ tải lên bảo hiểm y tế hiện không khả dụng, vui lòng thử lại sau.");
                    }
                    var resp = JsonConvert.DeserializeObject<Newtouch.EMR.Domain.DTO.OutputDto.MRUpload.ResponseBase>(respstr);
                    if (resp.code == "1")
                    {
                        // G19: FindEntity có thể trả null khi không có dòng
                        // basy — deref .bllx cũ gây NRE.
                        var bllxEty = _BllxRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.zt == "1" && p.bllxcode == "basy");
                        if (bllxEty == null)
                        {
                            return Error("Không tìm thấy loại bệnh án trang đầu.");
                        }
                        string bllx = bllxEty.bllx;
                        var rel = _zymeddocsrelationRep.FindEntity(p => p.zyh == zyh && p.zt == "1" && p.OrganizeId == OrganizeId && p.bllx == bllx);
                        if (rel != null)
                        {
                            rel.YbUploadFlag = "1";
                            _zymeddocsrelationRep.Update(rel);
                        }
                        var zdinfo = _yBInterfaceDmnService.GetBasyZdInfo(OrganizeId, zyh);
                        if (zdinfo != null && zdinfo.Count > 0)
                        {
                            YBRequestBase<YB_7610> YBJKDATA_ZD = new YBRequestBase<YB_7610>();
                            YBJKDATA_ZD.REQUESTDATA = new YBRequestRows<YB_7610>
                            {
                                MSGNO = "7610",
                                AKB020 = YBOrgCode,
                                OPERID = this.UserIdentity.rygh,
                                OPERNAME = UserIdentity.UserName,
                                PERIODNO = "",
                                MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                                GRANTID = "",
                                INPUT = zdinfo.ToList()
                            };
                            rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA_ZD);
                            rootinput.jydm = "7610";
                            string respstrzd;
                            try
                            {
                                respstrzd = EmrHttpHelper.PostJsonString(UploadURL + "api/QHDSmartCheck/MedicalRecordUpload", JsonConvert.SerializeObject(rootinput));
                            }
                            catch (Exception)
                            {
                                return Error("Dịch vụ tải lên bảo hiểm y tế hiện không khả dụng, vui lòng thử lại sau.");
                            }
                            // G19: trước đây deserialize nhầm respstr (kết quả
                            // 7600) thay vì respstrzd (kết quả 7610) nên quyết
                            // định thành công/thất bại của lần tải chẩn đoán
                            // luôn lấy theo lần tải trang đầu.
                            var respzd = JsonConvert.DeserializeObject<Newtouch.EMR.Domain.DTO.OutputDto.MRUpload.ResponseBase>(respstrzd);
                            if (respzd.code == "1")
                            {
                                return Success("Tải lên thành công");
                            }
                            else
                            {
                                return Error(resp.message);
                            }
                        }
                        return Success("Tải lên thành công"); 
                    }
                    else
                    {
                        msg = resp.message;
                    }

                    return Error(msg);
                }
                else
                {
                    return Error("Không lấy được địa chỉ giao diện") ;
                }
            }
        }
        public IActionResult UpdateUploadStu(string zyh)
        {
            // G19: GetBllxList(org, "basy") lọc theo Id/bllx/bllxmc (không
            // lọc bllxcode) nên luôn trả rỗng — bllxety null rồi NRE ở
            // .Code. Tra thẳng repo theo bllxcode như HomePageUpload.
            var bllxEty = _BllxRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.zt == "1" && p.bllxcode == "basy");
            if (bllxEty == null)
            {
                return Error("Không tìm thấy loại bệnh án trang đầu.");
            }
            var rel = _zymeddocsrelationRep.FindEntity(p => p.zyh == zyh && p.zt == "1" && p.OrganizeId == OrganizeId && p.bllx == bllxEty.bllx);
            if (rel == null)
            {
                return Error("Không tìm thấy thông tin liên kết bệnh án.");
            }
            rel.YbUploadFlag = "1";
            _zymeddocsrelationRep.Update(rel);
            return Success();
        }
        #region Liên quan đến bảo hiểm y tế
        public string DocumentAuditYB<T>(T entity)
        {
            string errmsg = "";
            PropertyInfo[] properties = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo p in properties)
            {
                var required = (RequiredAttribute[])p.GetCustomAttributes(typeof(RequiredAttribute), false);
                var desc = (DescriptionAttribute[])p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (required != null && required.Length > 0 && desc != null && desc.Length > 0)
                {
                    var thisval = p.GetValue(entity, null);
                    if (thisval == null || string.IsNullOrWhiteSpace(thisval.ToString()))
                    {
                        if (string.IsNullOrWhiteSpace(errmsg))
                        {
                            errmsg = "Dữ liệu quan trọng tải lên bảo hiểm y tế!";
                        }
                        errmsg += "[" + desc[0].Description + "] không được để trống;";
                    }
                }
            }
            return errmsg;
        }

        #endregion

        #region private methods

        /// <summary>
        /// 
        /// </summary>
        private void ReportingServiceCom()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = EmrConfig.GetAppConfigValue("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //Có phải quản trị viên bệnh viện hay không
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

        #endregion

        public IActionResult SettlementQuery(string zyh)
        {
            var data = _MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult DiagnosticSave(string Code)
        {
            var data = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);
            return Content(data.ToJson());
        }
    }
}