using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;





























namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("OutpatientManage")]

    public class OutpatientRefundController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IRefundDmnService _refundService;
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly IRefundApp _IRefundApp;
        private readonly IOutPatientUniversalDmnService _outPatientUniversalDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IPayApp _payApp;
        private readonly IOutpatientSettlementGAYBFeeRepo _outpatientSettlementGAYBFeeRepo;
        private readonly IGuiAnOutpatientXnhApp _guiAnOutpatientXnhApp;
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly ICqybSett05Repo _cqybSett05Repo;
        private readonly IOutpatientItemRepo _outpatientItemRepo;

        public OutpatientRefundController(IRefundDmnService refundService, IOutPatientDmnService outPatientDmnService, IRefundApp IRefundApp, IOutPatientUniversalDmnService outPatientUniversalDmnService, ISysConfigRepo sysConfigRepo, IPayApp payApp, IOutpatientSettlementGAYBFeeRepo outpatientSettlementGAYBFeeRepo, IGuiAnOutpatientXnhApp guiAnOutpatientXnhApp, IOutpatientRegistRepo outpatientRegistRepo, ICqybSett05Repo cqybSett05Repo, IOutpatientItemRepo outpatientItemRepo)
        {
            _refundService = refundService;
            _outPatientDmnService = outPatientDmnService;
            _IRefundApp = IRefundApp;
            _outPatientUniversalDmnService = outPatientUniversalDmnService;
            _sysConfigRepo = sysConfigRepo;
            _payApp = payApp;
            _outpatientSettlementGAYBFeeRepo = outpatientSettlementGAYBFeeRepo;
            _guiAnOutpatientXnhApp = guiAnOutpatientXnhApp;
            _outpatientRegistRepo = outpatientRegistRepo;
            _cqybSett05Repo = cqybSett05Repo;
            _outpatientItemRepo = outpatientItemRepo;
        }
        #region Khởi tạo trang

        public IActionResult QueJsList()
        {
            return View();
        }

        /// <summary>
        /// xem trước thông tin bệnh nhân khi tra cứu hoàn phí
        /// </summary>
        /// <returns></returns>
        public IActionResult SysPatEntitiesReView(string from = "")
        {
            ViewBag.from = from;
            return View();
        }
        #endregion

        /// <summary>
        /// phân trangdữ liệutải
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        [HttpGet]
                public IActionResult PatSearchInfo(Pagination pagination, string blh, string mzh, string xm, string kssj, string jssj
            , bool curUserCreate = false, string jiuzhenbiaozhi = null)
        {
            var data = new
            {
                rows = _refundService.GetBasicInfoSearchList(pagination, blh, mzh, xm, kssj, jssj, OrganizeId
                , curUserCreate ? UserIdentity.UserCode : "", jiuzhenbiaozhi),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// tra cứuquyết toánthông tin
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public IActionResult btnQuery(string blh, string startTime, string endTime)
        {
            var data = _refundService.GetjsInfoByblh(blh, startTime, endTime, OrganizeId);
            return Success("", data);
        }

        /// <summary>
        /// theoquyết toántra theo mã nội bộngoại trúkhámkhoản mục
        /// </summary>
        /// <param name="jsnm">quyết toánmã nội bộ</param>
        /// <returns></returns>
        public IActionResult GetGridViewMxByJsnm(int jsnm)
        {
            var data = _refundService.GetGridViewMx(jsnm, OrganizeId);
            return Success("", data);
        }

        /// <summary>
        /// hoàn phí
        /// </summary>
        public IActionResult btnReturn(string blh, int jsnm, string refundData)
        {
            bool isReturnAll;
            var GridViewMx = refundData.ToList<GridViewMx>();
            var result = _IRefundApp.btnReturnInAcc(blh, GridViewMx, jsnm, out isReturnAll);
            if (result)
            {
                return Success("", isReturnAll);
            }
            throw new FailedCodeException("HOSP_REFUNG_FAIL");
        }

        #region Hoàn phí2018bản

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index2018()
        {
            return View();
        }

        /// <summary>
        /// ngoại trúhoàn phí khám bản ghi chính Query
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult RefundableJsQuery(DateTime? kssj, DateTime? jssj, string mzh)
        {
            if (string.IsNullOrEmpty(mzh) || !kssj.HasValue || !jssj.HasValue)
            {
                return null;
            }
            var list = _refundService.RefundableJsQuery(this.OrganizeId, kssj, jssj, mzh);
            return Content(list.ToJson());
        }

        public IActionResult RefundableGuiAnJsQuery(DateTime? kssj, DateTime? jssj, string mzh)
        {
            if (string.IsNullOrEmpty(mzh) || !kssj.HasValue || !jssj.HasValue)
            {
                return null;
            }
            var list = _refundService.RefundableGuiAnJsQuery(this.OrganizeId, kssj, jssj, mzh);
            return Content(list.ToJson());
        }

        /// <summary>
        /// ngoại trúhoàn phí khám Query
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult RefundableDetailQuery(int jsnm = 0)
        {
            if (jsnm <= 0)
            {
                return null;
            }
            var list = _refundService.RefundableQuery(this.OrganizeId, jsnm);
            var ztlist = list.Where(p => p.ztId != null && p.ztId != "").ToList();
            var flag = true;
            string flagstr = _sysConfigRepo.GetValueByCode("sfxmztbs", this.OrganizeId);
            if (flagstr == "false")
            {
                flag = false;
            }
            if (flag)
            {
                //khoản thu phígộp bộ
                if (ztlist.Count() > 0)
                {
                    ztlist = list.Where(p => p.ztId != null && p.ztId != "").ToList()
                    .GroupBy(m => new { m.ghnm, m.jslx, m.feeType, m.cfh, m.ztmc, m.ztId, m.cfnm, m.ks, m.ys, m.ysmc, m.sfmb, m.sqdzt, m.cflxmc }).Select(p =>
                             new OutPatientRefundableVO
                             {
                                 ghnm = p.Key.ghnm,
                                 jsmxnm = p.Max(m => m.jsmxnm),
                                 jslx = p.Key.jslx,
                                 sl = 1,
                                 ktsl = 1,
                                 tsl = 1,
                                 jsmxje = p.Sum(m => m.jsmxje),
                                 dj =Math.Round(Convert.ToDecimal(p.Sum(m => m.jsmxje)), 2, MidpointRounding.AwayFromZero),
                                 feeType = p.Key.feeType,
                                 dw = "bộ",
                                 cfh = p.Key.cfh,
                                 mc = p.Key.ztmc,
                                 ztmc = p.Key.ztmc,
                                 ztId = p.Key.ztId,
                                 //ztsl = p.Key.ztsl,
                                 sfmb = p.Key.sfmb,
                                 dlmc = p.Key.ztmc,
                                 czh = "",
                                 cfnm = p.Key.cfnm,
                                 sfxmCode = p.Key.sfmb,
                                 sfdlCode = "",
                                 zfbl = null,
                                 zfxz = null,
                                 xmnm = null,
                                 cfmxId = null,
                                 ks = p.Key.ks,
                                 ys = p.Key.ys,
                                 ysmc = p.Key.ysmc,
                                 klsj = p.Max(m => m.klsj),
                                 zljhwzx = null,
                                 cflxmc = p.Key.cflxmc,
                                 sqdzt = p.Key.sqdzt
                             }
                    ).ToList();
                }
            }


            list = list.Where(p => p.ztId == null || p.ztId == "").ToList().Concat(ztlist).ToList();

            return Content(list.ToJson());
        }

        /// <summary>
        /// ngoại trútrang xác nhận hoàn phí khám
        /// </summary>
        /// <returns></returns>
        public IActionResult DetailConfirmForm()
        {
            ViewBag.ReportServerHOST = "";
            ViewBag.OrgId = this.OrganizeId;

            return View();
        }

        /// <summary>
        /// gửi hoàn phí thao tác lưu bền
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="expectedTmxZje"></param>
        /// <param name="newfph">mớiquyết toánsố hóa đơn</param>
        /// <returns></returns>
        public IActionResult RefundSettlement(string mzh, int jsnm
            , Dictionary<string, decimal> tjsxmDict, decimal expectedTmxZje
            , string newfph, Dictionary<string, decimal> tjsxmDictZt, decimal expectedTmxZjeZt, string cfnmArray)
        {
            return handleRefund(mzh, jsnm
            , tjsxmDict, expectedTmxZje, null, null
            , null
            , null
            , newfph, tjsxmDictZt, expectedTmxZjeZt, cfnmArray);
        }

        /// <summary>
        /// 20181120 dừng toàn bộ hoàn phí theo kế hoạch
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public IActionResult AccountPlanRefundAll(int jsnm)
        {
            _outPatientUniversalDmnService.AccountPlanRefundAll(this.OrganizeId, jsnm);
            return Success();
        }

        /// <summary>
        /// khung nhìn-chọnquyết toán
        /// </summary>
        /// <returns></returns>
        public IActionResult QueJsList2018()
        {
            return View();
        }

        #endregion

        #region

        /// <summary>
        /// quy trình hoàn phí.mớicủachi tiết chưa quyết toántải lên
        /// </summary>
        /// <param name="oldjsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="newwjybjsh">mớicủachưa quyết toánBHYTsố quyết toán</param>
        /// <returns></returns>
        public IActionResult DetailsUploadYb(int oldjsnm
            , Dictionary<string, decimal> tjsxmDict, string newwjybjsh)
        {
            var newDict = new Dictionary<int, decimal>();
            foreach (var item in tjsxmDict)
            {
                newDict.Add(Convert.ToInt32(item.Key), item.Value);
            }
            decimal tmxzje;
            decimal yxzje;
            _IRefundApp.DetailsUploadYb(this.OrganizeId, oldjsnm, newDict, newwjybjsh, out tmxzje, out yxzje);

            return Success(null, new { tmxzje = tmxzje, yxzje = yxzje });
        }

        /// <summary>
        /// BHYT gửi hoàn phí thao tác lưu bền
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="expectedTmxZje"></param>
        /// <param name="hcybjsh"></param>
        /// <param name="newwjybjsh"></param>
        /// <param name="hcybfeeRelated">đảo đỏquyết toán của quyết toánBHYTliên quan số tiền</param>
        /// <param name="newybfeeRelated">mớicủaquyết toán（không hoàn toàn bộ） của quyết toánBHYTliên quan số tiền</param>
        /// <returns></returns>
        public IActionResult YbRefundSettlement(string mzh, int jsnm
            , Dictionary<string, decimal> tjsxmDict, decimal expectedTmxZje, string hcybjsh, string newwjybjsh
            , CQMzjs05Dto hcybfeeRelated
            , OutpatientSettYbFeeRelatedDTO newybfeeRelated)
        {
            string newfph = "";
            decimal ztje = Convert.ToDecimal(0.00);
            return handleRefund(mzh, jsnm
            , tjsxmDict, expectedTmxZje, hcybjsh, newwjybjsh
            , hcybfeeRelated
            , newybfeeRelated
            , newfph, null, ztje, null);
        }

        #endregion

        #region Hoàn phí hợp tác nông thôn

        /// <summary>
        /// hoàn phí hợp tác nông thôn mới
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict">hoànquyết toánchi tiết</param>
        /// <param name="mzh"></param>
        /// <param name="outpId">ngoại trúkhámbồi thườngsố thứ tự</param>
        /// <returns></returns>
        public IActionResult XnhTuiFei(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh, string outpId)
        {
            TFS21RequestDTO tfs21Request;
            var tuifeiResult = _guiAnOutpatientXnhApp.XnhTuiFeiProcess(jsnm, tjsxmDict, mzh, outpId, OrganizeId, UserIdentity.UserCode, out tfs21Request);
            return string.IsNullOrWhiteSpace(tuifeiResult) ? Success(tuifeiResult, tfs21Request) : Error(tuifeiResult);
        }

        #endregion

        #region private methods

        /// <summary>
        /// （đã thu phí chưaphát thuốc）đã thu phí chưaphát thuốctrạng thái sự kiện thông báo thay đổi đơn thuốc
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="refundDict">jsmx tsl</param>
        private void refundFeeNotify(string mzh, Dictionary<int, decimal> refundDict)
        {
            try
            {
                var ypCfhList = _outPatientDmnService.GetRelatedYpDetailListByMzh(mzh, this.OrganizeId, refundDict);
                if (ypCfhList.Count <= 0) return;
                var orgId = this.OrganizeId;
                var operatorCode = this.UserIdentity.UserCode;
                //lấytoken
                var token = SitePDSAPIHelper.GetToken();
                var obj = ypCfhList.Select(p => new
                {
                    cfh = p.cfh,
                    ypdm = p.ypdm,
                    Yfbm = p.Yfbm,
                    sl = (int)refundDict.First(a => a.Key == p.jsmxnm).Value,
                    zhyz = 0,
                }).ToList();
                var thread = new Thread(new ThreadStart(delegate { ypCfRefundFeeNotifyThread(token, obj, orgId, operatorCode); }));
                thread.Start();
            }
            catch (Exception ex)
            {
                AppLogger.Instance.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// （đã thu phí chưaphát thuốc）thuốcđơn thuốcthông báo thay đổiPDS
        /// </summary>
        /// <param name="tmxList"></param>
        /// <param name="orgId"></param>
        private void ypCfRefundFeeNotifyThread(string token, object tmxList, string orgId, string operatorCode)
        {
            var reqObj = new
            {
                Items = tmxList,
                OrganizeId = orgId,
                CreatorCode = operatorCode,
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Token = token

            };
            var apiResp = SitePDSAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                "api/ResourcesOperate/OutpatientPartReturnBeforeDispensingMedicine", reqObj, autoAppendToken: false);

            if (apiResp != null && apiResp.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                new NLogger().Info(string.Format("（ĐãThu phíChưaphát thuốc）Thuốcđơn thuốcthông báo thay đổiPDS，Thất bại：{0}、{1}", reqObj.ToJson(), apiResp.ToJson()));
            }
        }

        /// <summary>
        /// HIShoàn phí（BHYT、không phảiBHYTdùng chung）
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="expectedTmxZje"></param>
        /// <param name="hcybjsh"></param>
        /// <param name="newwjybjsh"></param>
        /// <param name="hcybfeeRelated"></param>
        /// <param name="newybfeeRelated"></param>
        /// <param name="newfph"></param>
        /// <returns></returns>
        private IActionResult handleRefund(string mzh, int jsnm
            , Dictionary<string, decimal> tjsxmDict, decimal expectedTmxZje, string hcybjsh, string newwjybjsh
            , CQMzjs05Dto hcybfeeRelated
            , OutpatientSettYbFeeRelatedDTO newybfeeRelated
            , string newfph, Dictionary<string, decimal> tjsxmDictZt, decimal expectedTmxZjeZt, string cfnmArray)
        {
            var newDict = new Dictionary<int, decimal>();
            if (expectedTmxZje <= 0) tjsxmDict = null;
            if (expectedTmxZjeZt <= 0) tjsxmDictZt = null;
            if (tjsxmDict != null)
            {
                foreach (var item in tjsxmDict)
                {
                    newDict.Add(Convert.ToInt32(item.Key), item.Value);
                }
            }

            if (tjsxmDictZt != null)
            {
                var tmzxmlist = _outPatientUniversalDmnService.RefundZtDetail(cfnmArray, OrganizeId);
                foreach (var items in tjsxmDictZt)
                {
                    foreach (var m in tmzxmlist)
                    {
                        if (items.Key == m.ztId) //còn cần cộng thêmcfnm nhiều đơn thuốc có thể có cùng bộ
                        {
                            newDict.Add(m.jsmxnm, Math.Round(items.Value * m.sl, 2, MidpointRounding.AwayFromZero));
                        }
                    }
                }

            }
            expectedTmxZje += expectedTmxZjeZt;
            object newJszbInfo;
            string outTradeNo;
            decimal refundAmount;
            _outPatientUniversalDmnService.RefundSettlement(this.OrganizeId, jsnm, newDict, expectedTmxZje, newfph, out newJszbInfo, out outTradeNo, out refundAmount, hcybjsh, newwjybjsh, hcybfeeRelated, newybfeeRelated, null, null);
             
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                refundFeeNotify(mzh, newDict);
            }

            bool? isTradeRefundError = null;
            if (!string.IsNullOrWhiteSpace(outTradeNo) && refundAmount > 0)   //cần hoàn trả theo đường cũ
            {
                string errorMsg;
                var refundReuslt = _payApp.TradeRefund(outTradeNo, refundAmount, "Hoàn phí ngoại trú", "", out errorMsg);
                isTradeRefundError = refundReuslt == (int)EnumRefundStatus.Failed || refundReuslt == (int)EnumRefundStatus.UnKnown;    //thất bại hoặc không xác định
            }

            var msg = "Hoàn phí thành công"; 
            if (!isTradeRefundError.HasValue) return Success(msg, newJszbInfo);
            msg = !isTradeRefundError.Value ? "Hoàn phí thành công, số tiền phải hoàn đã hoàn theo đường cũ" : "HIS hoàn phí thành công, nhưng số tiền phải hoàn hoàn lại thất bại, vui lòng kiểm tra thủ công";
            
              return Success(msg , newJszbInfo);
        }


        /// <summary>
        /// cập nhậtCIShoàndấu hiệuvàhoàn trảnhà thuốcđóng băng tồn kho
        /// </summary>
        /// <param name="tcfh"></param>
        public void UpdateChargeTbz(List<string> tcfh, List<string> typcfh)
        {
            if (typcfh!=null)
            {
                foreach (var cfh in typcfh)
                {
                    //nội dung giao diện
                    var cancalreqObj = new
                    {
                        cfh = cfh,
                        OrganizeId = OrganizeId,
                        CreatorCode = this.UserIdentity.UserCode,
                    };
                    var apiRespPush = SitePDSAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                  "/api/ResourcesOperate/OutpatientCancelDjYpReturn", cancalreqObj);
                    if (apiRespPush.code != APIRequestHelper.ResponseResultCode.SUCCESS)
                    {
                        AppLogger.Info(string.Format("đơn thuốcHoàn phítrả lại tồn kho bị đóng băngCập nhậtAPIđồng bộđếnPDS，Số đơn thuốc：{0}，kết quả：{1}、{2}", tcfh, apiRespPush.code, apiRespPush.sub_code));
                    }
                }
            }
            var reqObj = new
            {
                cfList = tcfh,
                tbz = true,
            };
            var apiResp = SiteCISAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                           "api/Prescription/UpdateChargeTbz", reqObj);
            if (apiResp != null)
            {
                AppLogger.Info(string.Format("đơn thuốchoàndấu hiệuTrạng tháiCập nhậtAPIđồng bộđếnCIS，Số đơn thuốc：{0}，kết quả：{1}、{2}", string.Join(",", tcfh), apiResp.code, apiResp.sub_code));
            }
            else
            {
                AppLogger.Info(string.Format("đơn thuốchoàndấu hiệuCập nhậtAPIđồng bộđếnCIS，Số đơn thuốc：{0}，kết quả：ChưaLấyđến phản hồi，đồng bộThất bại", string.Join(",", tcfh)));
            }
        }

        public IActionResult MzhtResult(OutpatientSettlementGAYBFeeEntity dto)
        {
            dto.OrganizeId = this.OrganizeId;
            _outpatientSettlementGAYBFeeRepo.InsertEntity(dto);
            return Success("Ghi thành công");
        }

        /// <summary>
        /// hoàn phí（Quý An）
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="expectedTmxZje"></param>
        /// <param name="hcybjsh"></param>
        /// <param name="newwjybjsh"></param>
        /// <param name="hcybfeeRelated"></param>
        /// <param name="newybfeeRelated"></param>
        /// <param name="guianMztfProDto"></param>
        /// <param name="outpId"></param>
        /// <returns></returns>
        public IActionResult guianhandleRefund(string mzh, int jsnm
            , Dictionary<string, decimal> tjsxmDict, decimal expectedTmxZje, string hcybjsh, string newwjybjsh
            , CQMzjs05Dto hcybfeeRelated
            , CQMzjs05Dto newybfeeRelated
            , GuiAnMztfProDto guianMztfProDto
            , string outpId)
        {
            #region Quyết toán hợp tác nông thôn

            S25ResponseDTO xnhybfeeRelated = null;
            if (!string.IsNullOrWhiteSpace(outpId))
            {
                var xnhSettResult = _guiAnOutpatientXnhApp.Sett(mzh, outpId, OrganizeId, UserIdentity.UserCode, out xnhybfeeRelated);
                if (!string.IsNullOrWhiteSpace(xnhSettResult) && xnhSettResult.IndexOf("该结算已红冲", StringComparison.Ordinal) < 0)
                {
                    return Error(xnhSettResult);
                }
            }
            #endregion

            string newfph = "";
            var newDict = new Dictionary<int, decimal>();
            foreach (var item in tjsxmDict)
            {
                newDict.Add(Convert.ToInt32(item.Key), item.Value);
            }

            object newJszbInfo;
            string outTradeNo;
            decimal refundAmount;
            _outPatientUniversalDmnService.RefundSettlement(this.OrganizeId, jsnm, newDict, expectedTmxZje, newfph, out newJszbInfo, out outTradeNo, out refundAmount, hcybjsh, newwjybjsh, hcybfeeRelated, null, newybfeeRelated, guianMztfProDto, xnhybfeeRelated, outpId);

            if (!string.IsNullOrWhiteSpace(mzh))
            {
                refundFeeNotify(mzh, newDict);
            }

            bool? isTradeRefundError = null;
            if (!string.IsNullOrWhiteSpace(outTradeNo) && refundAmount > 0)   //cần hoàn trả theo đường cũ
            {
                string errorMsg;
                var refundReuslt = _payApp.TradeRefund(outTradeNo, refundAmount, "Hoàn phí ngoại trú", "", out errorMsg);
                isTradeRefundError = refundReuslt == (int)EnumRefundStatus.Failed || refundReuslt == (int)EnumRefundStatus.UnKnown;    //thất bại hoặc không xác định
            }

            var msg = "Hoàn phí thành công";
            if (isTradeRefundError.HasValue)
            {
                msg = !isTradeRefundError.Value ? "Hoàn phí thành công" : "HIS hoàn phí thành công, nhưng số tiền phải hoàn hoàn lại thất bại, vui lòng kiểm tra thủ công";
            }

            _outpatientRegistRepo.RecordOutpId(mzh, "", UserIdentity.UserCode, OrganizeId);
            return Success(msg, newJszbInfo);
        }
        #endregion

        #region Hoàn phí BHYT Trùng Khánh
        public IActionResult RefundableChongQingQuery(DateTime? kssj, DateTime? jssj, string mzh)
        {
            if (string.IsNullOrEmpty(mzh) || !kssj.HasValue || !jssj.HasValue)
            {
                return null;
            }
            var list = _refundService.RefundableChongQingQuery(this.OrganizeId, kssj, jssj, mzh);
            return Content(list.ToJson());
        }

        #endregion

        public IActionResult retApplicationform(string mzh, List<string> cfhList)
        { return Error("Thu hồi đơn kiểm tra PACS chưa được triển khai trong bản .NET Core này."); }

    }
}
