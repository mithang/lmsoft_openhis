using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Infrastructure.EF;
using Newtouch.Core.Common.Utils;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;

using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Domain.VO;
using Newtouch.HIS.DomainServices;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Enum;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Nhà thuốc ngoại trúController
    /// </summary>
    public class OutPatientPharmacyController : ControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IPyDmnService _pyDmnService;
        private readonly IfyDmnService _fyDmnService;
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;
        private readonly IMzCfRepo _mzCfRepo;
        private readonly IMzCfmxRepo _mzCfmxRepo;
        private readonly ItyDmnService _tyDmnService;
        private readonly IResourcesOperateApp _resourcesOperateApp;
        private readonly IOutPatientDispensingApp _outPatientDispensingApp;
        private readonly ISysYpksfypzEntityRepo _ISysYpksfypzEntityRepo;
        private readonly string _OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//Tổ chứcid

        #region Trang máy chủ trỏ tới
        /// <summary>
        /// Xếp thuốc ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult DrugArrangement()
        {
            return View();
        }

        /// <summary>
        /// Truy vấn phát thuốc ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult DrugSearch()
        {
            return View();
        }
        public IActionResult MzPrescriptionQuery()
        {
            ViewBag.OrganizeId = _OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }
        #endregion

        #region Gọi chung
        /// <summary>
        /// Tải cấu hình
        /// </summary>
        /// <returns></returns>
        public IActionResult Loadconfig()
        {
            var config = new pyConfig();
            var pycxjgsj = _sysConfigRepo.GetValueByCode("pycxjgsj", OrganizeId);
            if (pycxjgsj == "" || string.IsNullOrWhiteSpace(pycxjgsj))
            {
                throw new FailedException("Vui lòng cấu hình thời gian khoảng cách truy vấn xếp thuốc trước");
            }

            var isAutoPy = _sysConfigRepo.GetValueByCode("IsAutoPy", OrganizeId);
            if (isAutoPy == "" || string.IsNullOrWhiteSpace(isAutoPy))
            {
                throw new FailedException("Vui lòng cấu hình tự động xếp thuốc trước");
            }

            var isAutoFy = _sysConfigRepo.GetValueByCode("IsAutoFy", OrganizeId);
            if (isAutoFy == "" || string.IsNullOrWhiteSpace(isAutoFy))
            {
                throw new FailedException("Vui lòng cấu hình tự động phát thuốc trước");
            }

            config.pycxjgsj = int.Parse(pycxjgsj);
            config.IsAutoFy = isAutoFy;
            config.IsAutoPy = isAutoPy;
            return Content(config.ToJson());
        }

        /// <summary>
        /// Lưu cấu hình
        /// </summary>
        /// <param name="configInfo"></param>
        /// <returns></returns>
        public IActionResult SubmitConfig(pyConfig configInfo)
        {
            var isAutoFy = configInfo.IsAutoFy == "true" ? "Y" : "N";
            var isAutoPy = configInfo.IsAutoPy == "true" ? "Y" : "N";
            //_sysConfigRepo.SetValueByCode("IsAutoFy", isAutoFy, OrganizeId);
            //_sysConfigRepo.SetValueByCode("IsAutoPy", isAutoPy, OrganizeId);
            //_sysConfigRepo.SetValueByCode("pycxjgsj", configInfo.pycxjgsj.ToString(), OrganizeId);
            return Success();
        }
        #endregion

        #region In bổ sung thông tin phát thuốc


        /// <summary>
        /// In bổ sung đơn phát thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult RePrintMedicineDoc()
        {
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// In bổ sung thông tin phát thuốc Tải thông tin đơn thuốc
        /// </summary>
        /// <param name="fph"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IActionResult fyInfoSearch(string fph, string keyword, DateTime kssj, DateTime jssj)
        {
            if (kssj == null || jssj == null)
            {
                throw new FailedCodeException("THERE_ARE_UNRECOGNIZED_RECORDS_OF_THE_DRUG_AND_CAN_NOT_BE_REAPPLIED");
            }

            var result = _fyDmnService.SelectRpList(keyword, fph, kssj, jssj, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Content(result.ToJson());
        }
        #endregion

        #region Xếp thuốc ngoại trú

        #region old code

        /// <summary>
        /// Khởi tạo nhân viên xếp thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult initPYY()
        {
            var data = OperatorProvider.GetCurrent();
            return Content(data.ToJson());
        }

        /// <summary>
        /// Tải thông tin thống kê xếp thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult loadpyhj()
        {
            var yyqconfig = _sysConfigRepo.GetValueByCode("fyyxq", OrganizeId);

            if (yyqconfig == "" || string.IsNullOrWhiteSpace(yyqconfig))
            {
                throw new FailedException("Vui lòng cấu hình hạn dùng phát thuốc trước");
            }

            var yxq = int.Parse(yyqconfig);
            var reqObj = new
            {
                yfbmCode = Constants.CurrentYfbm.yfbmCode,
                yxq = yxq,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/WaitingDispenseMedicineQuery", reqObj);
            return Content(apiResp.data.ToJson());
        }

        /// <summary>
        /// Tải thông tin bảng chính xếp thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult PyInfoSearch()
        {
            var yyqconfig = _sysConfigRepo.GetValueByCode("fyyxq", OrganizeId);

            if (yyqconfig == "" || string.IsNullOrWhiteSpace(yyqconfig))
            {
                throw new FailedException("Vui lòng cấu hình hạn dùng phát thuốc trước");
            }
            var yxq = int.Parse(yyqconfig);
            var reqObj = new
            {
                yfbmCode = Constants.CurrentYfbm.yfbmCode,
                yxq = yxq,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/WaitingDispenseMedicineQuery", reqObj);
            if (apiResp.code == Newtouch.Common.Web.APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                return Content(apiResp.data.ToJson());
            }
            throw new FailedException("Lấy thông tin giao diện xếp thuốc thất bại");
        }

        /// <summary>
        /// Tải thông tin bảng con xếp thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult pyDeailInfoSearch(string pCfh)
        {
            if (string.IsNullOrWhiteSpace(pCfh) || pCfh == "undefined")
            {
                return Content(null);
            }

            var result = _fyDmnService.SelectRpDetail(pCfh, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Xếp thuốc
        /// </summary>
        /// <param name="cfnm">Mã nội bộ đơn thuốc</param>
        /// <param name="sfck">Cửa sổ thu phí</param>
        /// <returns></returns>
        public IActionResult SetPyInfo(string cfnm, string sfck)
        {
            //1. Lấy thông tin thuốc đơn thuốc
            var reqObj = new
            {
                cfnm = cfnm,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/GetpyParListBycfnm", reqObj);
            if (apiResp.code != Newtouch.Common.Web.APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                throw new FailedException("Lấy thông tin giao diện đơn thuốc thất bại");
            }
            //2. Lấy tổng Số tiền đơn thuốc và nhà thuốc nhận thuốc
            reqObj = new
            {
                cfnm = cfnm,
                TimeStamp = DateTime.Now
            };
            var apicfRep = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/GetcfInfoBycfnm", reqObj);
            if (apicfRep.code != Newtouch.Common.Web.APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                throw new FailedException("Lấy tổng Số tiền đơn thuốc và nhà thuốc nhận thuốc giao diện thất bại");
            }
            var vo = apicfRep.data.ToJson().ToList<cdInfoVO>().FirstOrDefault();

            //3. Lặp chuẩn bị thuốc
            var res = _pharmacyDrugStorageDmnService.OutPatientBookItem(apiResp.data, cfnm, vo);
            if (!string.IsNullOrWhiteSpace(res))
            {
                return Error(res);
            }

            //5. Cập nhật trạng thái phát
            var reqzt = new
            {
                cfnm = cfnm,
                lyck = sfck ?? "",
                user_code = this.UserIdentity.UserCode,
                TimeStamp = DateTime.Now
            };
            var apiztRep = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/Updatefyzt", reqzt);
            if (apiztRep.code != Newtouch.Common.Web.APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                throw new FailedException("Xử lý giao diện chuẩn bị thuốc thất bại");
            }
            return Success("Cấp thuốc thành công！");
        }
        #endregion

        #region Xếp thuốc  new


        /// <summary>
        /// Truy vấn xếp thuốc ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult DrugArrangementQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }

        /// <summary>
        /// Truy vấn xếp thuốc ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientDrugArrangementForzenQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }

        /// <summary>
        /// Tải thông tin bảng chính xếp thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryCfs()
        {
            var queryResult = new QueryCfsApp("").Process();
            if (queryResult != null && queryResult.IsSucceed)
            {
                return Content(((List<MzCfEntity>)queryResult.Data).ToJson());
            }
            throw new FailedException(queryResult != null ? queryResult.ResultMsg : "Lấy đơn thuốc thất bại");
        }

        /// <summary>
        /// Kéo không đồng bộ đơn thanh toán mới
        /// </summary>
        /// <returns></returns>
        public IActionResult WaitingDispenseMedicineQuery()
        {
            var reqObj = new
            {
                yfbmCode = Constants.CurrentYfbm.yfbmCode,
                yxq = 100,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/WaitingDispenseMedicineQuery", reqObj);
            var tags = new Dictionary<string, string>()
            {
                { Constants.OrganizeId, OperatorProvider.GetCurrent().OrganizeId},
                { Constants.Yfbm, Constants.CurrentYfbm.yfbmCode},
                { Constants.CreatorCode, OperatorProvider.GetCurrent().UserCode}
            };
            if (apiResp.code == Newtouch.Common.Web.APIRequestHelper.ResponseResultCode.SUCCESS || apiResp.data != null)
            {
                var cfs = apiResp.data.ToString().ToObject<List<Domain.DTO.OutPatientPharmacy.OutPatientpyCfListDTO>>();
                if (cfs == null || cfs.Count <= 0)
                {
                    return Success();
                }

                var downloadResult = new DownloadCfsApp(cfs).Process();
                if (downloadResult != null && downloadResult.IsSucceed)
                {
                    return Content(((List<MzCfEntity>)downloadResult.Data).ToJson()); ;
                }
                LogCore.Fatal("QueryCfsApp WaitingDispenseMedicineQuery fail", message: downloadResult != null ? downloadResult.ResultMsg : "Tải xuống đơn thuốc thất bại", addInfo: tags);
            }
            LogCore.Fatal("QueryCfsApp WaitingDispenseMedicineQuery fail", message: apiResp.ToJson(), addInfo: tags);
            return Error("Kéo không đồng bộ đơn thanh toán mới thất bại");
        }

        /// <summary>
        /// Lấy thông tin đơn thuốc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetCfInfo(string cardNo, string xm)
        {

            var rpList = _mzCfRepo.GetRpInfo(Constants.CurrentYfbm.yfbmCode, cardNo, xm, EnumFybz.Wp, OrganizeId);
            if (rpList == null)
            {
                return Content(null);
            }

            decimal zje = 0;
            var completeCfhs = new StringBuilder();
            var completeFph = new StringBuilder();
            rpList.ForEach(p =>
            {
                zje += p.je;
                completeCfhs.Append(p.cfh + ",");
                if (p.Fph != null && completeFph.ToString().IndexOf(p.Fph, StringComparison.Ordinal) == -1)
                {
                    completeFph.Append(p.Fph + ",");
                }
            });


            var partCfhs = completeCfhs.Length > 70 ? completeCfhs.ToString().Substring(0, 70) + "..." : completeCfhs.ToString();
            var partFph = completeFph.Length > 70 ? completeFph.ToString().Substring(0, 30) + "..." : completeFph.ToString();

            var result = new cfInfoVo
            {
                xm = rpList[0].xm,
                nl = rpList[0].nl,
                brxzmc = rpList[0].brxzmc,
                CardNo = rpList[0].CardNo,
                cfh = partCfhs.Trim(','),
                cfhComplete = completeCfhs.ToString().Trim(','),
                cfnm = rpList[0].cfnm,
                Fph = partFph.Trim(','),
                FphComplete = completeFph.ToString().Trim(','),
                fybz = rpList[0].fybz,
                ysmc = rpList[0].ysmc,
                ksmc = rpList[0].ksmc,
                Zje = zje,
            };
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy thông tin đơn thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult GetCfInfo(string cfh)
        {
            var entity = _mzCfRepo.FindEntity(p => p.cfh == cfh);
            return Content(entity != null ? entity.ToJson() : null);
        }

        /// <summary>
        /// Tải thông tin bảng con xếp thuốc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryCfmx(string cardNo, string xm)
        {
            var result = new List<CfmxVo>();
            var cfs = _mzCfRepo.GetCfs(cardNo, xm);
            var failMsg = "";
            if (cfs != null && cfs.Count > 0)
            {
                ActResult appResult;
                cfs.ForEach(p =>
                {
                    appResult = new DownloadCfmxApp(p.cfh).Process();
                    if (appResult.IsSucceed && appResult.Data != null)
                    {
                        var ret = appResult.Data as List<MzCfmxEntity>;
                        if (ret != null)
                        {
                            ret.ForEach(q =>
                            {
                                var cfmx = q.MapperTo<MzCfmxEntity, CfmxVo>();
                                cfmx.sfsj = p.sfsj;
                                cfmx.fph = p.Fph;
                                result.Add(cfmx);
                            });
                        }
                    }
                    failMsg = appResult.ResultMsg;
                });
            }

            if (result.Count > 0)
            {
                result = result.OrderBy(p => p.fph).ThenBy(p => p.cfh).ThenBy(p => p.czh).ToList();
                return Content(result.ToJson());
            }
            if (!string.IsNullOrWhiteSpace(failMsg))
            {
                throw new FailedException(failMsg);
            }

            return Content(null);
        }

        /// <summary>
        /// Tải thông tin bảng con xếp thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryCfmx(string pCfh)
        {
            var result = new DownloadCfmxApp(pCfh).Process();
            if (result.IsSucceed && result.Data != null)
            {
                var ret = result.Data as List<MzCfmxEntity>;
                if (ret != null)
                {
                    return Content(ret.ToJson());
                }
            }
            throw new FailedException(result.ResultMsg);
        }

        /// <summary>
        /// Lấy tên bệnh nhân và thời gian thu phí
        /// </summary>
        /// <returns></returns>
        public IActionResult GetXmAndSfsj(Pagination pagination)
        {
            var result = _mzCfRepo.GetXmAndCardNo(pagination, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            if (result == null)
            {
                throw new FailedException("Tất cả chờ xếp hoàn thành，Vui lòng đồng bộ đơn thuốc mới！");
            }

            var data = new
            {
                rows = result,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Tải thông tin bảng con xếp thuốc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RpDetailQuery(string cardNo, string xm)
        {
            var result = new List<CfmxVo>();
            var yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var cfs = _mzCfRepo.IQueryable(p => p.xm == xm && p.CardNo == cardNo && p.OrganizeId == OrganizeId &&
                                               p.lyyf == yfbmCode && p.zt == "1" &&
                                               p.fybz == ((int)EnumFybz.Wp).ToString() &&
                                               p.jsnm > 0);
            if (!cfs.Any())
            {
                return Content(null);
            }

            cfs.ToList().ForEach(p =>
            {
                var rpDetails = _mzCfmxRepo.IQueryable(q => q.cfh == p.cfh && q.zt == "1" && q.OrganizeId == OrganizeId);
                if (!rpDetails.Any())
                {
                    return;
                }

                var result1 = result;
                Parallel.ForEach(rpDetails.ToList(), t =>
                {
                    var tmp = t.MapperTo<MzCfmxEntity, CfmxVo>();
                    tmp.sfsj = p.sfsj;
                    tmp.fph = p.Fph;
                    result1.Add(tmp);
                });
            });
            result = result.OrderBy(p => p.fph).ThenBy(p => p.cfh).ThenBy(p => p.czh).ToList();
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy đơn thuốc xếp thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="xm"></param>
        /// <param name="cardNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="ksmc"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="jszt">Trạng thái thanh toán 0-Chưa thanh toán 1-Đã thanh toán 2/Khác-Tất cả</param>
        /// <param name="settExpired">Thanh toán đã hết hạn 1-Có 0-Không</param>
        /// <returns></returns>
        public IActionResult QueryDrugArrangementRpByPage(Pagination pagination, string xm, string cardNo, string invoiceNo, string ksmc, DateTime kssj, DateTime jssj, int jszt, string settExpired)
        {
            var result = _pyDmnService.SelectRpList(pagination, xm, cardNo, invoiceNo, ksmc, kssj, jssj, OrganizeId, Constants.CurrentYfbm.yfbmCode, jszt, settExpired);
            if (result.Count > 0)
            {
                result = result.Where(p => new[] { ((int)EnumFybz.Yp).ToString(), ((int)EnumFybz.Wp).ToString() }.Contains(p.fybz)).ToList();
            }
            var data = new
            {
                rows = result,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy đơn thuốc xếp thuốc
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="cardNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="ksmc"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="jszt">Trạng thái thanh toán 0-Chưa thanh toán 1-Đã thanh toán Khác-Tất cả</param>
        /// <returns></returns>
        public IActionResult QueryDrugArrangementRp(string xm, string cardNo, string invoiceNo, string ksmc, DateTime kssj, DateTime jssj, int jszt)
        {
            var result = _pyDmnService.SelectRpList(xm, cardNo, invoiceNo, ksmc, kssj, jssj, OrganizeId,
                Constants.CurrentYfbm.yfbmCode, jszt);
            if (result.Count > 0)
            {
                result = result.Where(p =>
                    new[] { ((int)EnumFybz.Yp).ToString(), ((int)EnumFybz.Wp).ToString() }.Contains(p.fybz)).ToList();
            }

            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy chi tiết đơn thuốc xếp thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public IActionResult QueryDrugArrangementRpDetail(string cfh, string cardNo)
        {
            if (string.IsNullOrWhiteSpace(cfh))
            {
                return Content(null);
            }

            var result = _pyDmnService.SelectRpArrangementDetail(cardNo, cfh, OrganizeId, Constants.CurrentYfbm.yfbmCode);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Hủy xếp thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public IActionResult DrugArrangementReturn(string cfh, string cardNo)
        {
            if (string.IsNullOrWhiteSpace(cfh))
            {
                throw new FailedException("Vui lòng chọn bệnh nhân cần thao tác！");
            }

            string result;
            try
            {
                var userCode = OperatorProvider.GetCurrent().UserCode;
                result = _pyDmnService.CancelArrangedDrug(cardNo, cfh, Constants.CurrentYfbm.yfbmCode, OrganizeId, userCode);
            }
            catch (FailedException e)
            {
                result = e.Msg;
            }
            return string.IsNullOrWhiteSpace(result) ? Success("Thao tác thành công") : Error(result);
        }
        /// <summary>
        /// Xếp thuốc ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult DrugArrangement2018()
        {
            return View();
        }

        /// <summary>
        /// Xếp thuốc ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult DrugArrangement2019()
        {
            return View();
        }

        /// <summary>
        /// Xếp thuốc
        /// </summary>
        /// <param name="patients"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DrugArrangementV2(patientInfoVO[] patients)
        {
            if (patients == null || patients.Length <= 0)
            {
                throw new FailedException("Vui lòng chọn bệnh nhân cần xếp thuốc！");
            }

            var errorMsg = new StringBuilder();
            var yfbmCode = Constants.CurrentYfbm.yfbmCode;
            foreach (var item in patients)
            {
                errorMsg.Append(_pyDmnService.DrugArrangement(item, yfbmCode, OrganizeId, OperatorProvider.GetCurrent().UserCode));

            }
            if (string.IsNullOrWhiteSpace(errorMsg.ToString()))
            {
                return Success("Xếp thuốc thành công");
            }

            throw new FailedException(errorMsg.ToString());
        }

        #endregion

        #endregion

        #region Phát thuốc ngoại trú

        #region old code

        /// <summary>
        /// Truy vấn thông tin bảng chính phát thuốc ngoại trú
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SearchFyInfo(string keyword)
        {
            var reqObj = new
            {
                keyword = keyword ?? "",
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/GetfyMainInfoList", reqObj);
            return Content(apiResp.data.ToJson());
        }

        /// <summary>
        /// Truy vấn thông tin bảng con phát thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        public IActionResult SearchFyDetail(string cfh)
        {
            if (string.IsNullOrEmpty(cfh))
            {
                return null;
            }
            var reqObj = new
            {
                keyword = cfh,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/GetfyDetailInfoList", reqObj);
            if (apiResp.code != Newtouch.Common.Web.APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                throw new FailedException("Truy vấn chi tiết đơn phát thuốc ngoại trú thất bại");
            }
            var result = _fyDmnService.GetDetailInfo(apiResp.data.ToJson().ToList<fyDetailListRequest>()).ToJson();
            return Content(result);
        }

        ///// <summary>
        ///// Phát thuốc ngoại trú
        ///// </summary>
        ///// <param name="pCfh"></param>
        ///// <returns></returns>
        //public IActionResult ExecMzHandOutMedicine(string pCfh)
        //{
        //    if (string.IsNullOrEmpty(pCfh))
        //    {
        //        return Error(string.Format("Đơn thuốc{0} Không có thông tin cần phát thuốc!", pCfh));
        //    }
        //    var reqObj = new
        //    {
        //        cfh = pCfh,
        //        usercode = Constants.CurrentYfbm.yfbmCode,
        //        TimeStamp = DateTime.Now,
        //        Token = SiteSettAPIHelper.GetToken()
        //    };
        //    var apiResp = SiteSettAPIHelper.Request<DefaultResponse>("api/OutpatientPharmacy/GetfyDetailCFInfo", reqObj);
        //    if (apiResp.data == null || apiResp.data.ToJson() == "")
        //    {
        //        return Error("Đơn thuốc này không tồn tại thông tin phát thuốc");
        //    }
        //    var reqdata = apiResp.data.ToJson().ToList<GetfyDetailCFList>();
        //    var data = _fyDmnService.MzDistributingMedicines(reqdata);
        //    if (data == null || data.Count <= 0) return Error(string.Format("Đơn thuốc{0} Phát thuốc thất bại", pCfh));
        //    var ztObj = new
        //    {
        //        cfnm = string.Join(",", data),
        //        user_code = UserIdentity.UserCode,
        //        TimeStamp = DateTime.Now,
        //        Token = SiteSettAPIHelper.GetToken()
        //    };
        //    SiteSettAPIHelper.Request<DefaultResponse>("api/OutpatientPharmacy/UpdatefyztByFY", ztObj);
        //    return Success();
        //}

        ///// <summary>
        ///// Phát thuốc tất cả
        ///// </summary>
        ///// <param name="cfh"></param>
        ///// <returns></returns>
        //public IActionResult ExecAllMzHandOutMedicine(List<string> cfh)
        //{
        //    foreach (var item in cfh)
        //    {
        //        if (string.IsNullOrEmpty(item))
        //        {
        //            return Error(string.Format("Đơn thuốc{0}Không có thông tin cần phát thuốc!", item));
        //        }
        //        var reqObj = new
        //        {
        //            cfh = item,
        //            usercode = OperatorProvider.GetCurrent().DepartmentCode,
        //            TimeStamp = DateTime.Now,
        //            Token = SiteSettAPIHelper.GetToken()
        //        };
        //        var apiResp = SiteSettAPIHelper.Request<DefaultResponse>("api/OutpatientPharmacy/GetfyDetailCFInfo", reqObj);
        //        if (apiResp.data.ToJson() == null || (apiResp.data.ToJson() == ""))
        //        {
        //            return Error(string.Format("Đơn thuốc{0}Không tồn tại thông tin phát thuốc", item));
        //        }
        //        var reqdata = apiResp.data.ToJson().ToList<GetfyDetailCFList>();
        //        var data = _fyDmnService.MzDistributingMedicines(reqdata);
        //        if (data == null || data.Count <= 0)
        //        {
        //            return Error(string.Format("Đơn thuốc{0} Phát thuốc thất bại", item));
        //        }

        //        var ztObj = new
        //        {
        //            cfnm = string.Join(",", data),
        //            user_code = UserIdentity.UserCode,// OperatorProvider.GetCurrent().DepartmentCode,
        //            TimeStamp = DateTime.Now,
        //            Token = SiteSettAPIHelper.GetToken()
        //        };
        //        SiteSettAPIHelper.Request<DefaultResponse>("api/OutpatientPharmacy/UpdatefyztByFY", ztObj);
        //    }
        //    return Success();
        //}
        #endregion

        #region new Phát thuốc ngoại trú

        /// <summary>
        /// Phát thuốc ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult DrugDelivery2018()
        {
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }
        public IActionResult ChooseCf()
        {

            return View();
        }

        /// <summary>
        /// Lấy tên bệnh nhân và thời gian thu phí
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFyXmAndSfsj(Pagination pagination, string keyword = "")
        {
            var result = _mzCfRepo.GetXmAndCardNo(pagination, Constants.CurrentYfbm.yfbmCode, OrganizeId, keyword, EnumFybz.Yp);
            if (result == null) throw new FailedException("Tất cả chờ xếp hoàn thành，Vui lòng đồng bộ đơn thuốc mới！");
            var data = new
            {
                rows = result,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy thông tin đơn phát thuốc
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetCfXX(string cardNo, string xm)
        {
            var rpList = _fyDmnService.GetRpInfo(Constants.CurrentYfbm.yfbmCode, cardNo, xm, EnumFybz.Yp, OrganizeId);
            if (rpList == null)
            {
                return Content(null);
            }
            return Content(rpList.ToJson());
        }

        [HttpGet]
        public IActionResult GetFyCfInfo(string cardNo, string xm)
        {
            var rpList = _fyDmnService.GetRpInfo(Constants.CurrentYfbm.yfbmCode, cardNo, xm, EnumFybz.Yp, OrganizeId);
            if (rpList == null)
            {
                return Content(null);
            }

            decimal zje = 0;
            var completeCfhs = new StringBuilder();
            var completeFph = new StringBuilder();
            rpList.ForEach(p =>
            {
                zje += p.je;
                completeCfhs.Append(p.cfh + ",");
                if (p.Fph != null && completeFph.ToString().IndexOf(p.Fph, StringComparison.Ordinal) == -1)
                {
                    completeFph.Append(p.Fph + ",");
                }
            });


            var partCfhs = completeCfhs.Length > 70 ? completeCfhs.ToString().Substring(0, 70) + "..." : completeCfhs.ToString();
            var partFph = completeFph.Length > 70 ? completeFph.ToString().Substring(0, 30) + "..." : completeFph.ToString();

            if (rpList.Count > 0)
            {
                var result = new cfInfoVo
                {
                    xm = rpList[0].xm,
                    nl = rpList[0].nl,
                    brxzmc = rpList[0].brxzmc,
                    CardNo = rpList[0].CardNo,
                    cfh = partCfhs.Trim(','),
                    cfhComplete = completeCfhs.ToString().Trim(','),
                    cfnm = rpList[0].cfnm,
                    Fph = partFph.Trim(','),
                    FphComplete = completeFph.ToString().Trim(','),
                    fybz = rpList[0].fybz,
                    ysmc = rpList[0].ysmc,
                    ksmc = rpList[0].ksmc,
                    Zje = zje,
                };
                return Content(result.ToJson());
            }
            else
            {
                return Content(null);
            }
        }

        /// <summary>
        /// Tải thông tin bảng con xếp thuốc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryFyCfmx(string cardNo, string xm)
        {
            var result = new List<CfmxVo>();
            var cfs = _mzCfRepo.GetCfs(cardNo, xm, (int)EnumFybz.Yp);
            var failMsg = "";
            if (cfs == null || cfs.Count <= 0)
            {
                return Content(result.ToJson());
            }

            ActResult appResult;
            cfs.ForEach(p =>
            {
                appResult = new DownloadCfmxApp(p.cfh).Process();
                if (appResult.IsSucceed && appResult.Data != null)
                {
                    var ret = appResult.Data as List<MzCfmxEntity>;
                    if (ret != null)
                    {
                        ret.ForEach(q =>
                        {
                            var cfmx = q.MapperTo<MzCfmxEntity, CfmxVo>();
                            cfmx.sfsj = p.sfsj;
                            cfmx.fph = p.Fph;
                            result.Add(cfmx);
                        });
                    }
                }
                failMsg = appResult.ResultMsg;
            });

            return Content(result.ToJson());
        }

        private static object threadLocker = new object();

        /// <summary>
        /// Tải thông tin bảng con xếp thuốc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryFyCfmxV2(string cardNo, string xm)
        {
            var result = new List<CfmxVO>();
            var cfs = _mzCfRepo.GetCf(Constants.CurrentYfbm.yfbmCode, cardNo, xm, EnumFybz.Yp, true, OrganizeId);
            if (cfs.Count <= 0)
            {
                return Content(result.ToJson());
            }

            try
            {
                Parallel.ForEach(cfs, p =>
                {
                    var tmpli = new fyDmnService(new DefaultDatabaseFactory(), false).SelectCfmx(p.xm, p.CardNo, p.cfh, p.OrganizeId, p.lyyf);
                    lock (threadLocker)
                    {
                        result.AddRange(tmpli);
                    }
                });
            }
            catch (Exception e)
            {
                LogCore.Error("QueryFyCfmxV2 error", e, "Ngoại lệ lấy chi tiết đơn phát thuốc ngoại trú");
            }

            return Content(result.ToJson());
        }
        /// <summary>
        /// Truy vấn chi tiết theo đơn thuốc
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryFyCfmxV2_new(string cfh)
        {
            var result = new List<CfmxVO>();
            try
            {
                var tmpli = new fyDmnService(new DefaultDatabaseFactory(), false).SelectCfmx_new(cfh, OrganizeId, Constants.CurrentYfbm.yfbmCode);
                lock (threadLocker)
                {
                    result.AddRange(tmpli);
                }
            }
            catch (Exception e)
            {
                LogCore.Error("QueryFyCfmxV2_new error", e, "Ngoại lệ lấy chi tiết đơn phát thuốc ngoại trú");
            }
            return Content(result.ToJson());
        }
        /// <summary>
        /// Truy vấn thông tin bảng chính phát thuốc ngoại trú
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SearchCfList(string keyword)
        {
            var cfList = _mzCfRepo.GetCfsByKeyword(keyword);
            if (cfList != null && cfList.Count > 0)
            {
                return Content(cfList.ToJson());
            }
            return Content(null);
        }

        /// <summary>
        /// Trả thuốc trước Truy vấn chi tiết đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        public IActionResult SearchRefundeRpDetail(string cfh)
        {
            List<RefundedDrugVo> cfmxList;
            var partReturnSwitchStr = _sysConfigRepo.GetValueByCode("Outpatient_PreRefund_Partreturn", OrganizeId);
            if (!string.IsNullOrWhiteSpace(partReturnSwitchStr) && !("t".Equals(partReturnSwitchStr.ToLower()) || "true".Equals(partReturnSwitchStr.ToLower())))
            {
                cfmxList = _tyDmnService.SelectCompleteRefundedDrugs(cfh, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            }
            else
            {
                cfmxList = _tyDmnService.SelectRefundedDrugs(cfh, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            }
            return Content(cfmxList.ToJson());
        }

        /// <summary>
        /// Truy vấn thông tin bảng con phát thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SearchCfmx(string cfh, string operateType)
        {
            var cfmxList = _fyDmnService.GetDeliveryDrugs(cfh, (EnumOperateType)Convert.ToInt32(operateType));
            if (cfmxList != null && cfmxList.Count > 0)
            {
                return Content(cfmxList.ToJson());
            }
            return Content(null);
        }

        /// <summary>
        /// Phát thuốc tất cả
        /// </summary>
        /// <param name="patients"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ExecAllDeliveryDrug(patientInfoVO[] patients)
        {
            var cfhls = new List<string>();
            var eq = new OutpatienDrugDeliveryInfo
            {
                PatientInfo = patients.ToList().GroupBy(p => new { p.cfh, p.cfnm, p.CardNo, p.xm }).Select(v => new patientInfoVO { cfh = v.Key.cfh, cfnm = v.Key.cfnm, CardNo = v.Key.CardNo, xm = v.Key.xm, zsm = string.Join("|", v.Select(f => f.zsm.Trim())) }).ToList(),
                yfbmCode = Constants.CurrentYfbm.yfbmCode,
                userCode = OperatorProvider.GetCurrent().UserCode,
                organizeId = OrganizeId
            };
            var result = _outPatientDispensingApp.ExecAllDeliveryDrugV2(eq, out cfhls);
            var cfhStr = new StringBuilder();
            cfhls.ForEach(p => cfhStr.Append(p).Append(","));
            return string.IsNullOrWhiteSpace(result) ? Success("", cfhStr.ToString().Trim(',')) : Error(result);
        }

        /// <summary>
        /// Lấy số đơn thuốc
        /// </summary>
        /// <param name="patients"></param>
        /// <returns></returns>
        public string GetAllNeedPrintRpByXm(patientInfoVO[] patients)
        {
            var result = new StringBuilder();
            try
            {
                if (patients == null || patients.Length <= 0)
                {
                    throw new FailedException("Vui lòng chọn đơn thuốc cần in！");
                }

                foreach (var item in patients)
                {
                    var cfhs = _mzCfRepo.GetCf(Constants.CurrentYfbm.yfbmCode, item.CardNo, item.xm, EnumFybz.Yf, true, OrganizeId);
                    if (cfhs.Count > 0)
                    {
                        cfhs.ForEach(p => { result.Append(p.cfh + ","); });
                    }
                }
            }
            catch (Exception e)
            {
                LogCore.Error("GetAllNeedPrintRpByXm error", e, "Lấy thông tin đơn thuốc phát thuốc ngoại trú thất bại");
            }

            return result.ToString();
        }

        /// <summary>
        /// Lấy số nhóm theo số đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        public string GetRecipeGroupNum(string cfh, string type)
        {
            try
            {
                string zh = "";
                var output = _mzCfRepo.GetZhInOutpatient(cfh, type);
                List<string> result = new List<string>();
                foreach (var item in output)
                {
                    result.Add(item.zh);
                }
                HashSet<string> tmp = new HashSet<string>(result);
                var returnStr = string.Join(",", tmp);
                return returnStr;
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion 

        #endregion

        #region Truy vấn phát thuốc ngoại trú

        /// <summary>
        /// Truy vấn phát thuốc ngoại trú hiển thị chi tiết
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>
        public IActionResult SearchFyDetailInDrug(Pagination pagination, string cfnm)
        {
            var reqObj = new
            {
                cfnm = cfnm,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/GetfyQueryDetailInfoList", reqObj);
            if (apiResp == null || apiResp.data == null)
            {
                return Content(null);
            }
            //var data = _fyDmnService.MZHandOutDetailQuery(apiResp.data.ToJson().ToList<fyDetailListRequest>(), pc);
            var data = _fyDmnService.GetMzHandOutDetail(apiResp.data.ToJson().ToList<fyDetailListRequest>());
            return Content(data.ToJson());
        }

        #region Truy vấn phát thuốc ngoại trú new

        /// <summary>
        /// Lấy cờ phát thuốc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetFybz()
        {
            var dic = EnumCommon.Instance().GetEnumDesAndVal<EnumFybz>();
            if (dic != null && dic.Count > 0)
            {
                var arr = new List<object>();
                foreach (var item in dic)
                {
                    if (item.Value == (int)EnumFybz.Wp)
                    {
                        continue;
                    }

                    arr.Add(new { key = item.Key, value = item.Value });
                }
                return Content(arr.ToJson());
            }
            return Content(null);
        }

        /// <summary>
        /// Giao diện truy vấn phát thuốc ngoại trú,Thông tin bảng chính
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IActionResult SearchCfs(Pagination pagination, searchFyInfoReqVO req)
        {
            EnumOperateType operateType;
            switch (req.type)
            {
                case "1":
                    operateType = EnumOperateType.Fy;
                    break;
                case "2":
                    operateType = EnumOperateType.Ty;
                    break;
                default:
                    operateType = EnumOperateType.None;
                    break;
            }
            var cfs = _fyDmnService.SelectDispensedRpInfo(pagination, req.keyWord, req.fph, req.cfh,
                operateType, (DateTime)req.begindate, (DateTime)req.enddate, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            if (cfs == null || cfs.Count <= 0)
            {
                return Content(null);
            }

            var data = new
            {
                rows = cfs,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        #endregion

        #endregion

        #region Trả thuốc ngoại trú

        /// <summary>
        /// Trả thuốc ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult DrugRepercussion()
        {
            var switchValue = _sysConfigRepo.GetValueByCode("outpaitentReturnDrugAuotPrint", OrganizeId);
            ViewBag.autoPrintSwitch = string.IsNullOrWhiteSpace(switchValue) || "true".Equals(switchValue.ToLower()) ? ViewBag.autoPrintSwitch = "checked=\"checked\"" : "";
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            ViewBag.partReturnSwitch = true;
            var partReturnSwitchStr = _sysConfigRepo.GetValueByCode("Outpatient_PreRefund_Partreturn", OrganizeId);
            if (!string.IsNullOrWhiteSpace(partReturnSwitchStr))
            {
                ViewBag.partReturnSwitch = "t".Equals(partReturnSwitchStr.ToLower()) || "true".Equals(partReturnSwitchStr.ToLower());
            }
            return View();
        }

        /// <summary>
        /// Truy vấn thông tin bảng chính trả thuốc ngoại trú
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SearchTyInfo(string keyword)
        {
            var reqObj = new
            {
                keyword = keyword ?? "",
                lyyf = Constants.CurrentYfbm.yfbmCode,//OperatorProvider.GetCurrent().DepartmentCode,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/GettyMainInfoList", reqObj);
            if (apiResp == null || apiResp.data == null)
            {
                return Content("");
            }
            var reqdata = apiResp.data.ToJson().ToList<tyCFMainInfo>();
            if (reqdata.Count > 0)
            {
                reqdata = _fyDmnService.ExecMZExitMedicine(reqdata).Distinct().ToList();
            }
            return Content(reqdata.ToJson());
        }

        /// <summary>
        /// Truy vấn thông tin bảng con trả thuốc ngoại trú
        /// </summary>
        /// <param name="pCfh"></param>
        /// <returns></returns>
        public IActionResult SearchTyDetail(string pCfh)
        {
            if (string.IsNullOrEmpty(pCfh))
            {
                return null;
            }
            var reqObj = new
            {
                cfh = pCfh,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/GettyDetailInfoList", reqObj);
            var result = _fyDmnService.GetAllDetailInfo(apiResp.data.ToJson().ToList<fyDetailListRequest>()).ToJson();
            return Content(result);
        }

        #region Trả thuốc  new

        /// <summary>
        /// Truy vấn thông tin bảng chính phát thuốc ngoại trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IActionResult SearchFyCfList(searchFyInfoReqVO req)
        {
            req = req ?? new searchFyInfoReqVO();
            req.OrganizeId = OrganizeId;
            req.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            req.begindate = req.begindate ?? DateTime.Now.AddDays(-5);
            req.enddate = req.enddate ?? DateTime.Now.AddDays(1);
            var cfList = _mzCfRepo.SelectDispensedRpList(req);
            return Content(cfList.ToJson());
        }

        /// <summary>
        /// Thao tác trả thuốc ngoại trú
        /// </summary>
        /// <param name="tyParam"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ExecReturnStorage(tyParam[] tyParam)
        {
            var tyInfo = new tyInfo
            {
                yfbmCode = Constants.CurrentYfbm.yfbmCode,
                userCode = OperatorProvider.GetCurrent().UserCode,
                organizeId = OrganizeId,
                tyDrugDetail = tyParam.ToList()
            };
            List<string> billNoList;
            var result = _resourcesOperateApp.OutpatientReturnDrugs(tyInfo, out billNoList);
            return string.IsNullOrWhiteSpace(result) ? Success("", billNoList.ToJson()) : Error(result);
        }
        #endregion

        #endregion

        #region Truy vấn đơn ngoại trú

        public IActionResult PrescriptionDetailFrom()
        {
            ViewBag.OrganizeId = _OrganizeId;
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetMzcfGridJson(Pagination pagination, string ks, string cflx, DateTime kssj, DateTime jssj, string keyword)
        {
            var reqObj = new MzcfcxVo
            {
                ks = ks,
                kssj = kssj,
                cflx = cflx,
                jssj = jssj,
                keyword = keyword,
                organizeId = OrganizeId
            };
            var data = new
            {
                rows = _fyDmnService.GetMzcfList(pagination, reqObj).ToList(),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetMzcfGridDetailJson(Pagination pagination, string cfh)
        {
            var reqObj = new MzcfcxVo
            {
                cfh = cfh,
                organizeId = OrganizeId
            };
            var data = new
            {
                rows = _fyDmnService.GetMzcfDetailList(pagination, reqObj).ToList(),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public IActionResult GetTheLowerKsCodeList(string keyword)
        {
            var data = _ISysYpksfypzEntityRepo.GetCodeName(OrganizeId, 0, keyword);
            return Content(data.ToJson());
        }

        #endregion

        #region  Duyệt đơn điện tử
        public IActionResult ElectronicPrescription()
        {
            return View();
        }
        /// <summary>
        /// Lấy thông tin bệnh nhân đơn điện tử
        /// </summary>
        /// <returns></returns>
        public IActionResult GetElectronicPrescription(Pagination pagination, string isysh, string keyword = "")
        {
            var result = _mzCfRepo.GetElectronicPrescription(pagination, OrganizeId, isysh, keyword);
            var data = new
            {
                rows = result,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// Lấy thông tin đơn phát thuốc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetElectronicPrescriptionCfInfo(string cfh, string xm)
        {
            var rpList = _fyDmnService.GetElectronicPrescriptionCfInfo(cfh, xm, OrganizeId);
            return Content(rpList.ToJson());
        }
        /// <summary>
        /// Lấy thông tin chi tiết đơn thuốc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryElectronicPrescriptionCfmx(string cfh, string xm)
        {
            var result = _mzCfRepo.QueryElectronicPrescriptionCfmx(cfh, xm, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
		/// Sửa đăng ký khám ngoại trú lấy
		/// </summary>
		/// <param name="mzh"></param>
		/// <returns></returns>
		public IActionResult GetCQjzdjInfo(string mzh)
        {
            Input_2203A model = _mzCfRepo.GetCQjzdjInfo(mzh, this.OrganizeId);
            return Content(model.ToJson());
        }
        /// <summary>
        /// Sửa ý kiến duyệt không đạt
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="ysshyj"></param>
        /// <returns></returns>
		public IActionResult UpdateCfYsshyj(string cfh, string ysshyj)
        {
            _mzCfRepo.UpdateCfYsshyj(cfh, ysshyj, this.OrganizeId);
            return Content("");
        }

        #endregion
    }
}