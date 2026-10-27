using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.HospitalizationManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("HospitalizationManage")]
    public class DischargeSettleController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        // GET: HospitalizationManage/DischargeSettle

        private readonly IDischargeSettleApp _dischargeSettleApp;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IPayApp _payApp;
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;
        private readonly IHospSimulateSettlementGAYBFeeRepo _hospSimulateSettlementGAYBFeeRepo;
        private readonly IHospFeeDmnService _hospFeeDmnService;
        private readonly IHospSettlementGAYBZYMXXRFeeRepo _hospSettlementGAYBZYMXXRFeeRepo;
        private readonly IInpatientApp _inpatientApp;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
        private readonly ICqybSett05Repo _cqybSett05Repo;
        private readonly IBookingDmnService _bookingDmnService;
        private readonly IHospMultiDiagnosisRepo _hospMultiDiagnosisRepo;
        private readonly IHospSettlementRepo _hospSettlementRepo;
        private readonly IHospDrugBillingRepo _hospdrugbillingRepo;
        public DischargeSettleController(IDischargeSettleApp dischargeSettleApp, ISysConfigRepo sysConfigRepo, IPayApp payApp, IDischargeSettleDmnService dischargeSettleDmnService, IHospSimulateSettlementGAYBFeeRepo hospSimulateSettlementGAYBFeeRepo, IHospFeeDmnService hospFeeDmnService, IHospSettlementGAYBZYMXXRFeeRepo hospSettlementGAYBZYMXXRFeeRepo, IInpatientApp inpatientApp, IPatientBasicInfoDmnService patientBasicInfoDmnService, ICqybSett05Repo cqybSett05Repo, IBookingDmnService bookingDmnService, IHospMultiDiagnosisRepo hospMultiDiagnosisRepo, IHospSettlementRepo hospSettlementRepo, IHospDrugBillingRepo hospdrugbillingRepo)
        {
            _dischargeSettleApp = dischargeSettleApp;
            _sysConfigRepo = sysConfigRepo;
            _payApp = payApp;
            _dischargeSettleDmnService = dischargeSettleDmnService;
            _hospSimulateSettlementGAYBFeeRepo = hospSimulateSettlementGAYBFeeRepo;
            _hospFeeDmnService = hospFeeDmnService;
            _hospSettlementGAYBZYMXXRFeeRepo = hospSettlementGAYBZYMXXRFeeRepo;
            _inpatientApp = inpatientApp;
            _patientBasicInfoDmnService = patientBasicInfoDmnService;
            _cqybSett05Repo = cqybSett05Repo;
            _bookingDmnService = bookingDmnService;
            _hospMultiDiagnosisRepo = hospMultiDiagnosisRepo;
            _hospSettlementRepo = hospSettlementRepo;
            _hospdrugbillingRepo = hospdrugbillingRepo;
        }


        public override IActionResult Index()
        {
            return View();
        }

        public IActionResult CancelIndex()
        {
            return View();
        }

        public IActionResult MedicalInsuranceApproval()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult ConfirmForm()
        {
            return View();
        }

        public IActionResult SettConfirmForm()
        {
            return View();
        }

        public IActionResult HosAccountDeposit()
        {
            return View();
        }
        public IActionResult SimulateFeeItems()
        {
            return View();
        }
        public IActionResult HospFeeItems()
        {
            return View();
        }
        public IActionResult DetailedQuery()
        {
            return View();
        }
        public IActionResult HospFeeDetailedQuery()
        {
            return View();
        }
        public IActionResult HospZyFeeDetailedQuery()
        {
            return View();
        }
        public IActionResult SimulateIndex2021()
        {
            return View();
        }

        public IActionResult Index2021()
        {
            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult GetUploadFeeDetails(Pagination pagination, string zyh)
        {
            decimal ybzje = Convert.ToDecimal(0.0000);
            var data = new
            {
                updata = _dischargeSettleDmnService.GetUploadFeeDetails(pagination, zyh, this.OrganizeId, this.UserIdentity.UserName, out ybzje),
                ybzje = ybzje,
                total = pagination.total,
                page = pagination.page
            };
            return Content(data.ToJson());
        }

        #region BHYT Trùng Khánh
        public IActionResult GetCQUploadFeeDetails(Pagination pagination, string zyh)
        {
            decimal ybzje = Convert.ToDecimal(0.0000);
            decimal zfzje = Convert.ToDecimal(0.0000);
            var data = new
            {
                updata = _dischargeSettleDmnService.GetCQUploadFeeDetails(pagination, zyh, this.OrganizeId, this.UserIdentity.UserCode, out ybzje, out zfzje),
                ybzje = ybzje,
                //zfzje = zfzje,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public IActionResult GetCQAlreadyUploadFeeDetails(string mzzyh)
        {
            var ybzje = _dischargeSettleDmnService.GetCQAlreadyUploadFeeDetails(mzzyh, this.OrganizeId);
            var data = new
            {
                ybzje = ybzje
            };
            return Content(data.ToJson());
        }
        public IActionResult GetNogjybdm(string mzzyh, string cfnms, string cflx)
        {
            var xmmc = _dischargeSettleDmnService.Getgjybdm(mzzyh, cfnms, cflx, this.OrganizeId);
            var data = new
            {
                xmmc = xmmc
            };
            return Content(data.ToJson());

        }
        /// <summary>
        /// Tổng phí tải lên BHYT
        /// </summary>
        /// <param name="mzzyh"></param>
        /// <returns></returns>
        public IActionResult GetCQAlreadyUploadFeeDetailsV2(string mzzyh, DateTime jssj)
        {
            var ybzje = _dischargeSettleDmnService.GetCQAlreadyUploadFeeDetailsV2(mzzyh, this.OrganizeId, jssj);
            var data = new
            {
                ybzje = ybzje.zje,
                csrq = ybzje.cyrq.ToString("yyyy-MM-dd")
            };
            return Content(data.ToJson());
        }
        public IActionResult ValialUploadData(string zyh, DateTime jssj, string jsbz)
        {
            var zjjfbh = "";
            var jfbh = _dischargeSettleDmnService.ValialUploadData(zyh, this.OrganizeId, jssj);
            if (jsbz == "2")//quyết toán giữa kỳ
            {
                zjjfbh = _dischargeSettleDmnService.ValialPartialUploadData(zyh, this.OrganizeId, jssj);
            }
            var data = new
            {
                jfbh = jfbh,
                zjjfbh = zjjfbh
            };
            return Content(data.ToJson());

        }
        /// <summary>
        /// Lấy thông tin BHYT Thượng Hải chưa tải lên
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="jssj"></param>
        /// <param name="jsbz"></param>
        /// <returns></returns>
        public IActionResult ValialUploadDataShyb(string zyh, DateTime jssj, string jsbz)
        {
            var zjjfbh = "";
            var jfbh = _dischargeSettleDmnService.ValialUploadDataShyb(zyh, this.OrganizeId, jssj);
            var data = new
            {
                jfbh = jfbh,
                zjjfbh = zjjfbh
            };
            return Content(data.ToJson());

        }
        /// <summary>
        /// Lấy thông tin hoàn trảquyết toán
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public IActionResult GetCQJshtInfo(int jsnm)
        {
            CqybSett05Entity entity =
                _cqybSett05Repo.FindEntity(p => p.jsnm == jsnm && p.zt == "1" && p.OrganizeId == this.OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lấy thông tinquyết toán
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public IActionResult GetCQJsPatInfo(string zyh)
        {
            var data = _dischargeSettleDmnService.GetCQInpatientSettYbPatInfo(zyh, this.OrganizeId);
            if (data == null)
            {
                return Error("Không tìm thấy thông tin làm thủ tục nhập viện");
            }
            return Content(data.ToJson());
        }
        #endregion

        #region Nông thôn mới Quý An

        public IActionResult GuiAnXnhMnjs(string zyh)
        {
            return Error("Chức năng này chưa được chuyển đổi sang .NET Core trong giai đoạn này (tích hợp BHYT GuiAn / xuất Excel / CIS).");
        }

        /// <summary>
        /// quyết toán nông thôn mới Quý An
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GuiAnXnhSettlement(string zyh)
        {
            return Error("Chức năng này chưa được chuyển đổi sang .NET Core trong giai đoạn này (tích hợp BHYT GuiAn / xuất Excel / CIS).");
        }

        /// <summary>
        /// Hủyquyết toán xuất viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GuiAnXnhSettCancel(string zyh)
        {
            return Error("Chức năng này chưa được chuyển đổi sang .NET Core trong giai đoạn này (tích hợp BHYT GuiAn / xuất Excel / CIS).");
        }
        /// <summary>
        /// Hoàn trả xuất viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GuiAnXnhCyblCancel(string zyh)
        {
            return Error("Chức năng này chưa được chuyển đổi sang .NET Core trong giai đoạn này (tích hợp BHYT GuiAn / xuất Excel / CIS).");
        }



        /// <summary>
        /// Xóa thông tin chi tiết phí đã tải lên của bệnh nhân nhập viện
        /// </summary>
        /// <param name="inpId"></param>
        /// <returns></returns>
        public bool GuiAnXnhDelFeeDetails(string inpId, out string delMsg)
        {
            delMsg = "Chức năng xóaChi tiết đã tải lên BHYT GuiAn chưa được chuyển đổi sang .NET Core trong giai đoạn này.";
            return false;
        }
        /// <summary>
        /// Lấy mã nhập viện theo mã cá nhân（Nông thôn mới）
        /// </summary>
        /// <param name="xnhgrbm">Mã cá nhân</param>
        /// <param name="sfjs">Cóquyết toán hay không1Đãquyết toán 0 chưakết</param>
        /// <returns></returns>
        public IActionResult GetZyhByGrbm(string xnhgrbm, string sfjs)
        {
            var zyh = _dischargeSettleDmnService.GetZyhByGrbm(xnhgrbm, sfjs, this.OrganizeId);
            return Success(null, zyh);
        }

        #endregion

        #region quyết toán xuất viện//（Không có thuật toán BHYT）//20190408sửalàCó thuật toán BHYT

        /// <summary>
        /// Dữ liệu truy vấn mã nhập viện（bao gồmBệnh nhânthông tinvàChi tiết tính phí） zyh hoặc kh +cardType hoặc sfz +cardType
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="sfz"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver)
        {
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 0);//Đồng bộ phí mục
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 1);//Đồng bộ phí thuốc
            }
            var dto = _dischargeSettleApp.GetInpatientSettleStatusDetail(zyh, sfz, kh, cardType, jslx, ver);
            return Success("", dto);
        }
        [HandlerAjaxOnly]
        public IActionResult GetInpatientSettleStatusDetailbySfdl(Pagination pagination, string zyh, string dlCode)
        {
            var dto = _dischargeSettleDmnService.GetHospGroupFeeVOList(pagination, zyh, OrganizeId, dlCode);
            var data = new
            {
                rows = dto,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// mangmẫuhồTruy vấn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetInpatientSettleStatusDetailbySfdlData(Pagination pagination, string zyh, string keyword)
        {
            var dto = _dischargeSettleDmnService.GetHospGroupFeeVOData(pagination, zyh, OrganizeId, keyword);
            var data = new
            {
                rows = dto,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// Truy vấnChi tiết mục
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetDetailedQuery(Pagination pagination, string zyh, string sfxmCode, string zzfbz, decimal? dj)
        {
            var dto = _dischargeSettleDmnService.GetDetailedQuery(pagination, zyh, OrganizeId, sfxmCode, zzfbz, dj);
            var data = new
            {
                rows = dto,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// Truy vấnnhập việnphíHóa đơn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetZyDetailedQuery(Pagination pagination, string zyh, string sfxmCode, string zzfbz)
        {
            var dto = _dischargeSettleDmnService.GetFyzdDetailedQuery(pagination, zyh, OrganizeId, sfxmCode);
            var data = new
            {
                rows = dto,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #region Truyền dữ liệu BHYT
        [HandlerAjaxOnly]
        public IActionResult GetZybrInfo(string zyh, string jslx)
        {
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 0);//Đồng bộ phí mục
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 1);//Đồng bộ phí thuốc
            }
            var dto = _dischargeSettleApp.GetZyUploadDetail(zyh, jslx);
            return Success("", dto);
        }

        public IActionResult GetZyUploadDetailJe(string zyh, string sczt, DateTime jssj, string xmmc)
        {
            var dto = _dischargeSettleApp.GetInpatientSettleJe(zyh, sczt, null, jssj, xmmc);
            return Success("", dto);
        }

        [HandlerAjaxOnly]
        public IActionResult GetZyUploadDetail(Pagination pagination, string zyh, string sczt, DateTime jssj, string xmmc, string isnewyb)
        {
            var dto = _dischargeSettleDmnService.GetHospXmYpFeeVOList(pagination, zyh, OrganizeId, sczt, xmmc, null, jssj, isnewyb);
            var data = new
            {
                rows = dto,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public IActionResult GetHospPreSettInfo(string zyh, DateTime jzsj)
        {
            var InpatObj = _dischargeSettleDmnService.GetHospPreInfo(zyh, this.OrganizeId, jzsj);
            //var ybzje = _dischargeSettleDmnService.GetCQAlreadyUploadFeeDetailsV2(zyh, this.OrganizeId);
            var data = new
            {
                zje = InpatObj.zje,
                cyrq = InpatObj.cyrq.ToString("yyyy-MM-dd"),
                ybzje = InpatObj.ybzje
            };
            return Content(data.ToJson());
        }
        #endregion
        /// <summary>
        /// Chọn ngày xuất viện
        /// </summary>
        /// <returns></returns>
        public IActionResult SettSelectCyrq()
        {
            return View();
        }

        /// <summary>
        /// Lưuquyết toán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedcyrq"></param>
        /// <param name="fph"></param>
        /// <param name="feeRelated">Số tiềnvàThông tin thanh toán</param>
        /// <param name="ybfeeRelated">Liên quan BHYTThông tin phí</param>
        /// <param name="outTradeNo">Số giao dịch thanh toán</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SaveSettle(string zyh, DateTime expectedcyrq, string fph, InpatientSettFeeRelatedDTO feeRelated
            , CQZyjs05Dto ybfeeRelated, S13OtherResponseDTO xnhfeeRelated, string jslx
            , string outTradeNo)
        {
            return Error("Chức năng này chưa được chuyển đổi sang .NET Core trong giai đoạn này (tích hợp BHYT GuiAn / xuất Excel / CIS).");
        }

        /// <summary>
        /// lấyquyết toán BHYT cơ quantạovàotham
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetInpatientSettYbPatInfo(string zyh)
        {
            var data = _dischargeSettleDmnService.GetInpatientSettYbPatInfo(zyh, this.OrganizeId);
            if (data == null)
            {
                return Error("Không tìm thấy thông tin làm thủ tục nhập viện");
            }
            return Success(null, data);
        }

        public IActionResult upLoadReturnData(IList<HospSettlementGAYBZYMXXRFeeEntity> upReturnData, string patzyh)
        {
            if (upReturnData != null && upReturnData.Count > 0)
            {
                foreach (var feeEntity in upReturnData)
                {
                    feeEntity.Create(true);
                    feeEntity.OrganizeId = this.OrganizeId;
                    feeEntity.zt = "1";
                    feeEntity.zyh = patzyh;
                    _hospSettlementGAYBZYMXXRFeeRepo.Insert(feeEntity);
                }
            }

            return Success();
        }

        /// <summary>
        /// quyết toán mô phỏng LưuKết quả
        /// </summary>
        /// <param name="feeEntity"></param>
        /// <returns></returns>
        public IActionResult SubmitSimulateSettlementResult(HospSimulateSettlementGAYBFeeEntity feeEntity)
        {
            feeEntity.Create(true);
            feeEntity.OrganizeId = this.OrganizeId;
            feeEntity.zt = "1";
            _hospSimulateSettlementGAYBFeeRepo.Insert(feeEntity);
            return Success();
        }

        #endregion

        #region Hủyquyết toán xuất viện//（Không có thuật toán BHYT）//20190408sửalàCó thuật toán BHYT

        /// <summary>
        /// Truy vấn hủyquyết toán
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult GetCancelSettleStatusDetail(string zyh, string sfz, string kh, string cardType)
        {
            var dto = _dischargeSettleApp.GetCancelSettleStatusDetail(zyh, sfz, kh, cardType);
            return Success(null, dto);
        }

        /// <summary>
        /// Hủyquyết toán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsnm"></param>
        /// <param name="cancelReason"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult DoCancel(string zyh, int expectedjsnm, string cancelReason, string cancelyblsh)
        {
            string outTradeNo;
            decimal refundAmount;
            _dischargeSettleApp.DoCancel(zyh, expectedjsnm, cancelReason, cancelyblsh, out outTradeNo, out refundAmount);

            if (_sysConfigRepo.GetBoolValueByCode("HOSP_INTERFACE_WITH_CPOE", this.OrganizeId) == true)
            {
                //Đồng bộzybz Chờquyết toán choCIS
                Newtouch.Sett.Infrastructure.SiteCISAPIHelper.UpdateInpatientBasicInfo(new InpatientPatientInfoDTO()
                {
                    zyh = zyh,
                    zybz = ((int)EnumZYBZ.Djz).ToString(),
                });
            }

            bool? isTradeRefundError = null;
            if (!string.IsNullOrWhiteSpace(outTradeNo) && refundAmount > 0)   //Cần hoàn trả theo đường cũ
            {
                string errorMsg;
                var refundReuslt = _payApp.TradeRefund(outTradeNo, refundAmount, "Ngoại trú hoàn phí", "", out errorMsg);
                isTradeRefundError = refundReuslt == (int)Newtouch.Sett.Domain.EnumRefundStatus.Failed || refundReuslt == (int)Newtouch.Sett.Domain.EnumRefundStatus.UnKnown;    //Thất bại hoặc chưabiết
            }
            _hospdrugbillingRepo.Updatezy_brxxexpand(this.OrganizeId, zyh);
            var msg = "Hủyquyết toán thành công";  
            if (isTradeRefundError.HasValue)
            {
                if (!isTradeRefundError.Value)
                {
                    msg = "Hủy quyết toán thành công, số tiền phải hoàn đã hoàn trả theo đường cũ";
                }
                else
                {
                    msg = "HIS hủy quyết toán thành công, nhưng số tiền phải hoàn hoàn trả thất bại, vui lòng kiểm tra thủ công";
                }
            } 
            return Success(msg);
        }
        #endregion

        #region quyết toán mô phỏng（GA）

        /// <summary>
        /// quyết toán mô phỏng
        /// </summary>
        /// <returns></returns>
        public IActionResult SimulateIndex()
        {
            return View();
        }

        /// <summary>
        /// quyết toán mô phỏng Hiển thị kết quả trả về BHYT
        /// </summary>
        /// <returns></returns>
        public IActionResult SimulateForm()
        {
            return View();
        }
        public IActionResult SimulateForm2021()
        {
            return View();
        }
        public IActionResult SimulateFormShyb2023()
        {
            return View();
        }
        /// <summary>
        /// quyết toán mô phỏng Gửisaumáy lưuKết quả
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SimulateSettSubmit()
        {
            return Success();
        }

        #endregion

        #region private methods

        /// <summary>
        /// lấyTrả vềtrongdungtrongcủaflaggiá trị
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string resp_GetResultAttr(string xml, string attrName)
        {
            var dt = getDataTableFromXml(xml, "result");
            if (dt != null && dt.Rows.Count == 1)
            {
                var flagRow = dt.Rows[0][attrName];
                if (flagRow != null)
                {
                    return flagRow.ToString().Trim();
                }
            }
            return null;
        }

        /// <summary>
        /// từxmltrongTrích xuấttableNamecủaDataTable
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private DataTable getDataTableFromXml(string xml, string tableName)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return null;
            }
            var ds = XmlToDtUntility.XmlToDataSet(xml);
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (ds.Tables[i].TableName.Equals(tableName))
                {
                    return ds.Tables[i];
                }
            }
            return null;
        }

        #endregion


        #region quyết toán giữa kỳ
        public IActionResult PartialSettleIndex()
        {
            return View();
        }
        public IActionResult PartialSettleIndex2021()
        {
            return View();
        }

        /// <summary>
        /// quyết toán giữa kỳ
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="fph"></param>
        /// <param name="feeRelated"></param>
        /// <param name="ybfeeRelated"></param>
        /// <param name="xnhfeeRelated"></param>
        /// <param name="jslx"></param>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult PartialSettleSave(string zyh, DateTime startTime, DateTime endTime, string fph, InpatientSettFeeRelatedDTO feeRelated
            , CQZyjs05Dto ybfeeRelated, string jslx
            , string outTradeNo)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                throw new FailedCodeException("HOSP_ZYH_IS_EMPTY");
            }

            if (string.IsNullOrEmpty(jslx))
            {
                return Error("Loạiquyết toán không được để trống, vui lòng liên hệ vận hành HIS！");
            }


            int jsnm;
            _dischargeSettleApp.SavePartialSettle(zyh, startTime, endTime, fph, feeRelated, ybfeeRelated, outTradeNo, jslx, out jsnm);
            _hospdrugbillingRepo.Updatezy_brxxexpand(this.OrganizeId, zyh);
            var res = new
            {
                jsnm = jsnm
            };
            return Success(null, res);
        }
        /// <summary>
        /// Sauquyết toán giữa kỳ cần xử lý bản ghi chi tiết đã tải lên Tránh việc BHYT lấy số liệu không chính xác khi xuất viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult ExecPartialSettleDeatil(string zyh, string jsnm, string czlx)
        {
            _hospdrugbillingRepo.ExecPartialSettleFeeDetail(zyh, jsnm, czlx);
            return Success();
        }
        /// <summary>
        /// Dữ liệu truy vấn mã nhập viện（bao gồmBệnh nhânthông tinvàChi tiết tính phí） zyh hoặc kh +cardType hoặc sfz +cardType
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="sfz"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetPartialInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver)
        {
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 0);//Đồng bộ phí mục
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 1);//Đồng bộ phí thuốc
            }
            var dto = _dischargeSettleApp.GetPartialInpatientSettleStatusDetail(zyh, sfz, kh, cardType, jslx, ver);
            return Success("", dto);
        }

        /// <summary>
        /// Chuyểnquyết toán xuất viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult TransfertoCYsettle(string zyh)
        {
            return Error("Chức năng này chưa được chuyển đổi sang .NET Core trong giai đoạn này (tích hợp BHYT GuiAn / xuất Excel / CIS).");
        }


        public IActionResult CancelPartialConfirmForm()
        {
            return View();
        }
        /// <summary>
        /// Lấy số tiền hoàn phí
        /// </summary>
        /// <returns></returns>
        public IActionResult Getrefunds_available(int jsnm)
        {
            if (jsnm == 0)
            {
                throw new FailedException("Thiếu mãquyết toán");
            }
            var jsentity = _hospSettlementRepo.IQueryable(p => p.jsnm == jsnm && p.OrganizeId == OrganizeId && p.zt == "1" && p.jszt == "1").ToList().FirstOrDefault();
            return Success("", jsentity.xjzf);
            //db.IQueryable<HospSettlementEntity>(p => p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1" && p.jszt == "2").Select(p => p.cxjsnm);
        }
        #endregion

        #region Hủyquyết toán xuất viện
        public IActionResult GetCQLastjsnm(string zyh)
        {
            var data = _dischargeSettleDmnService.GetCQLastJsnm(zyh, this.OrganizeId);
            return Content(data.ToString());
        }

        public IActionResult GetCQLastLsh(int jsnm)
        {
            var data = _dischargeSettleDmnService.GetCQLastLsh(jsnm, this.OrganizeId);
            return Success("", data);
        }
        public IActionResult GetCancelSettInfo(int jsnm)
        {
            var data = _dischargeSettleDmnService.GetCancelSettInfo(jsnm, this.OrganizeId);
            data.operatorId = this.UserIdentity.rygh;
            data.operatorName = this.UserIdentity.UserName;
            return Content(data.ToJson());
        }
        #endregion

        #region BHYT chưa phê duyệt
        public IActionResult GetCYZD(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("Thiếu thông tin bệnh nhân");
            }

            var DiseaseInfo = _hospMultiDiagnosisRepo.SelectData(zyh, this.OrganizeId);
            var zdmc = DiseaseInfo.Select(p => p.zdmc + ",");
            return Success("", zdmc);
        }


        public IActionResult GetUnapproveddetails(string cfmxlshstr, string zyh)
        {
            var data = _dischargeSettleDmnService.sweep_expired_approvals(cfmxlshstr, this.OrganizeId, zyh);
            return Content(data.ToJson());
        }

        public IActionResult updatespbz(string cfhstr)
        {
            _dischargeSettleDmnService.updatespbz(cfhstr, OrganizeId, this.UserIdentity.UserCode);
            return Success();
        }

        public IActionResult GetCurrentpatientcharge(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("Thiếu mã nhập viện");
            }
            IList<HospItemFeeDetailVO> sfxm = _hospFeeDmnService.GetItemFeeDetailVOList(zyh, this.OrganizeId).ToList();
            IList<HospItemFeeDetailVO> yp = _hospFeeDmnService.GetMedicinFeeDetailVOList(zyh, this.OrganizeId).ToList();
            return Content(sfxm.Union(yp.Select(y => (HospItemFeeDetailVO)y)).ToList().ToJson());
        }

        public IActionResult MedicalInsuranceApprovalIndex()
        {
            return View();
        }
        #endregion

        #region BHYTLưuquyết toán trước
        public IActionResult subybyjsld(CqybSett2303Entity YuJieSuan)
        {
            DateTime dateTime = DateTime.Now;
            YuJieSuan.prejs_id = Guid.NewGuid().ToString();
            YuJieSuan.czrq = dateTime;
            YuJieSuan.czydm = this.UserIdentity.UserCode;
            YuJieSuan.zt = 1;
            YuJieSuan.zt_czy = this.UserIdentity.UserCode;
            YuJieSuan.zt_rq = dateTime;
            _dischargeSettleDmnService.SaveSett2303(YuJieSuan);
            return Success();
        }

        #endregion

        public IActionResult QualityControlIndex()
        {
            return View();
        }

        public IActionResult CountLisIncompletezy(string zyh)
        {
            var num = _dischargeSettleDmnService.CountLisIncompletezy(OrganizeId, zyh);
            var data = new
            {
                num = num
            };
            return Content(data.ToJson());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class InpatientDischargeStatus
    {
        /// <summary>
        /// Mã nhập viện
        /// </summary>
        public string admsNum { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string dischargeStatus { get; set; }

    }
}
