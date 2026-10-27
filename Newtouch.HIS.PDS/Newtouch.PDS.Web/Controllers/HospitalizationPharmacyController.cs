using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Infrastructure.EF;
using Newtouch.Infrastructure.Common;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Newtouch.Common;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationPharmacy;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Nhà thuốc nội trúController
    /// </summary>
    public class HospitalizationPharmacyController : ControllerBase
    {

        private readonly IDispenseIndexInfoDmnService _idispenseIndexInfoDmnService;
        private readonly ISysMedicineRepo _iSysMedicineRepo;
        private readonly ISysWardRepo _iSysWardRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IDispensingDmnService _dispensingDmnService;
        private readonly IHospitalizationPharmacyApp _hospitalizationPharmacyApp;
        private readonly IResourcesOperateApp _resourcesOperateApp;
        private readonly string _OrganizeId = OperatorProvider.GetCurrent().OrganizeId;//Tổ chứcid

        #region Liên kết
        public IActionResult DispenseIndex()
        {
            var refreshIntervalStr = ConfigurationHelper.GetAppConfigValue("HospitalizationDispenseRefreshInterval");
            int refreshInterval;
            int.TryParse(refreshIntervalStr, out refreshInterval);
            ViewBag.refreshInterval = refreshInterval;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            ViewBag.OrganizeId = _OrganizeId;
            var printSwitch = _sysConfigRepo.GetValueByCode("hospitalizationDispensingDrugAutoPrint", OrganizeId);
            ViewBag.dispensingDrugAutoPrint = string.IsNullOrWhiteSpace(printSwitch) || "true".Equals(printSwitch.ToLower()) ? ViewBag.autoPrintSwitch = "checked=\"checked\"" : "";

            return View();
        }

        /// <summary>
        /// Phát thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryIndex()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = _OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }

        public IActionResult DeptMedicineApplySend()
        {
            var refreshIntervalStr = ConfigurationHelper.GetAppConfigValue("HospitalizationDispenseRefreshInterval");
            int refreshInterval;
            int.TryParse(refreshIntervalStr, out refreshInterval);
            ViewBag.refreshInterval = refreshInterval;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            ViewBag.OrganizeId = _OrganizeId;
            var printSwitch = _sysConfigRepo.GetValueByCode("hospitalizationDispensingDrugAutoPrint", OrganizeId);
            ViewBag.dispensingDrugAutoPrint = string.IsNullOrWhiteSpace(printSwitch) || "true".Equals(printSwitch.ToLower()) ? ViewBag.autoPrintSwitch = "checked=\"checked\"" : "";

            return View();
        }
        public IActionResult DeptMedicineApplyReturn()
        {
            var refreshIntervalStr = ConfigurationHelper.GetAppConfigValue("HospitalizationDispenseRefreshInterval");
            int refreshInterval;
            int.TryParse(refreshIntervalStr, out refreshInterval);
            ViewBag.refreshInterval = refreshInterval;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            ViewBag.OrganizeId = _OrganizeId;
            return View();
        }

       
        public IActionResult PrescriptionDetailFrom()
        {
            ViewBag.OrganizeId = _OrganizeId;
            return View();
        }
        #endregion

        #region Phát thuốc nội trú

        /// <summary>
        /// Khu bệnh phát thuốc nội trú
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult DispensePatientInfoQuery()
        {
            var yfbmCode = Constants.GetCurrentYfbm(OperatorProvider.GetCurrent().UserId).yfbmCode;
            var patientInfo = _idispenseIndexInfoDmnService.GetDispensePatientInfo(yfbmCode, OrganizeId);

            if (patientInfo == null || patientInfo.Count == 0) return Content("");
            var brTreeList = new List<TreeViewModel>();
            var bqTreeList = new List<TreeViewModel>();
            var tvlocker = new object();
            Parallel.ForEach(patientInfo, item =>
            {
                var itempat = new TreeViewModel
                {
                    id = item.zyh,
                    text = item.patientName + "-" + item.cw,
                    value = item.zyh,
                    parentId = item.bqCode,
                    isexpand = false,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = false,
                    Ex1 = "c"
                };
                var itembq = new TreeViewModel
                {
                    id = item.bqCode,
                    text = item.bqmc,
                    value = item.bqCode,
                    parentId = null,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = patientInfo.FindAll(p => p.bqCode == item.bqCode).Count != 0,
                    Ex1 = "p"
                };
                lock (tvlocker)
                {
                    brTreeList.Add(itempat);
                    if (!bqTreeList.Exists(p => p.id == item.bqCode))
                    {
                        bqTreeList.Add(itembq);
                    }
                }
            });
            var treeList = new List<TreeViewModel>();
            treeList.AddRange(brTreeList);
            treeList.AddRange(bqTreeList);
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Tập hợp thông tin y lệnh phát thuốc nội trú
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetDispenseDrugDetail(HospitalizationDispenseDrugParam queryParam)
        {
            var zyhs = queryParam.Zyh ?? "";
            zyhs = zyhs.Trim().Trim(',');
            if (string.IsNullOrWhiteSpace(zyhs)) return Content("");
            queryParam.OrganizeId = OrganizeId;
            queryParam.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            queryParam.Kssj = queryParam.Kssj <= Convert.ToDateTime(Constants.MinDateTime) ? Convert.ToDateTime(Constants.MinDateTime) : queryParam.Kssj;
            queryParam.Jssj = queryParam.Jssj <= Convert.ToDateTime(Constants.MinDateTime) ? DateTime.Now.AddHours(2) : queryParam.Jssj;
            var result = _idispenseIndexInfoDmnService.SelectDispenseDrugDetail(queryParam);
            var zyharr = zyhs.Split(',');
            var r = new List<YPFYPatientInfoVO>();
            var locker = new object();
            if (zyharr.Length > 0)
            {
                Parallel.ForEach(zyharr, zyh =>
                {
                    if (string.IsNullOrWhiteSpace(zyh)) return;
                    var tmp = result.FindAll(p => p.zyh == zyh);
                    if (tmp.Count == 0) return;
                    lock (locker)
                    {
                        r.AddRange(tmp);
                    }
                });
            }
            return Content(r.ToJson());
        }

        /// <summary>
        /// Xóa y lệnh chỉ định
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="zxId">Thực hiện y lệnhID</param>
        /// <returns></returns>
        public IActionResult DeleteYzxx(string yzId, string zxId)
        {
            var result = _idispenseIndexInfoDmnService.DeleteYzxx(yzId, zxId, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Hủy xếp thuốc
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="zxId"></param>
        /// <param name="ypCode"></param>
        /// <param name="yzId"></param>
        /// <returns></returns>
        public IActionResult CancelArrangement(string pc, string ph, string ypCode, string zxId, string yzId)
        {
            var param = new CancelArrangement
            {
                yzId = yzId,
                zxId = zxId,
                OrganizeId = OrganizeId,
                pc = pc,
                ph = ph,
                userCode = OperatorProvider.GetCurrent().UserCode,
                yfbmCode = Constants.CurrentYfbm.yfbmCode,
                ypCode = ypCode
            };
            var result = _dispensingDmnService.HospitalizationCancelArrangementWithTrans(param);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Nhấp thực hiện phát thuốc
        /// </summary>
        /// <param name="fyls">Danh sách phát thuốc</param>
        [HandlerAjaxOnly]
        public IActionResult DispensingDrugs(List<DispensingDrugsParam> fyls)
        {
            var result = _hospitalizationPharmacyApp.HospitalizationDispensing(fyls, OperatorProvider.GetCurrent().UserCode, Constants.CurrentYfbm.yfbmCode, OrganizeId,out string fyid);
            return string.IsNullOrWhiteSpace(result) ? Success("Phát thuốc thành công",fyid) : Error(result);
        }
        #endregion

        #region  Chuẩn bị thuốc cho khoa

        /// <summary>
        /// Cây đơn xin
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="bylx"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult DispenseApplySendInfoQuery(string keyword,string bylx)
        {
            var yfbmCode = Constants.GetCurrentYfbm(OperatorProvider.GetCurrent().UserId).yfbmCode;
            var bqsqdInfo = _idispenseIndexInfoDmnService.GetDeptApplySendInfo(yfbmCode, OrganizeId, bylx);

            if (bqsqdInfo == null || bqsqdInfo.Count == 0)
                return Content("");
            if(!string.IsNullOrWhiteSpace(keyword))
                bqsqdInfo = bqsqdInfo.Where(p => p.bqbm == keyword).ToList();

            var brTreeList = new List<TreeViewModel>();
            var bqTreeList = new List<TreeViewModel>();
            var tvlocker = new object();
            Parallel.ForEach(bqsqdInfo, item =>
            {
                var itempat = new TreeViewModel
                {
                    id = item.djh,
                    text = item.djh,
                    value = item.djh,
                    parentId = item.bqbm,
                    isexpand = false,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = false,
                    Ex6=item.thyy,
                    Ex1 = "c"
                };
                var itembq = new TreeViewModel
                {
                    id = item.bqbm,
                    text = item.bqmc,
                    value = item.bqbm,
                    parentId = null,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = bqsqdInfo.FindAll(p => p.bqbm == item.bqbm).Count != 0,
                    Ex1 = "p"
                };
                lock (tvlocker)
                {
                    brTreeList.Add(itempat);
                    if (!bqTreeList.Exists(p => p.id == item.bqbm))
                    {
                        bqTreeList.Add(itembq);
                    }
                }
            });
            var treeList = new List<TreeViewModel>();
            treeList.AddRange(brTreeList);
            treeList.AddRange(bqTreeList);
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Thông tin đơn xin thuốc chuẩn bị cho khoa
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IActionResult GetDeptApplySendList(string SqdArr)
        {
            string SqdArrs = SqdArr.Trim().Trim(',');
            if (string.IsNullOrWhiteSpace(SqdArrs)) return Content("");
            var result = _idispenseIndexInfoDmnService.SelectDeptApplySendList(SqdArr,OrganizeId, Constants.CurrentYfbm.yfbmCode);
            
            return Content(result.ToJson());
        }
        /// <summary>
        /// Nhấp phát thuốc
        /// </summary>
        /// <param name="SqdArr">Đơn xin</param>
        [HandlerAjaxOnly]
        public IActionResult ApplyNoSendDrugs(string SqdArr)
        {
            var result = _idispenseIndexInfoDmnService.ApplyNoSendDrugs(SqdArr, OperatorProvider.GetCurrent().UserCode, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success("Phát thuốc thành công") : Error(result);
        }
        /// <summary>
        /// Trả lại đơn xin
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <returns></returns>
        public IActionResult ApplyNoReturnDrugs(string SqdArr)
        {
            var sqdObj = new
            {
                OrganizeId = OrganizeId,
                SqdArray = SqdArr,
                UserCode= OperatorProvider.GetCurrent().UserId
            };
            try {
                var apiResp = SiteCisAPIHelper.Request<object, DefaultResponse>("/api/Hospitalized/UpdateApplyNoStatus", sqdObj);

                if (apiResp.code != Newtouch.Common.Web.APIRequestHelper.ResponseResultCode.SUCCESS || apiResp.data == null)
                    return Error(apiResp.msg);
            }
            catch (Exception e) {
                throw new FailedException(e.Message);
            }
            return Success("Trả lại đơn xin chuẩn bị thuốc thành công");
        }

        /// <summary>
        /// Thông tin đơn xin trả lại tồn kho thuốc chuẩn bị cho khoa
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IActionResult GetDeptApplyReturnList(string SqdArr)
        {
            if (string.IsNullOrWhiteSpace(SqdArr)) return Content("");
            var result = _idispenseIndexInfoDmnService.SelectDeptApplyReturnList(SqdArr, OrganizeId, Constants.CurrentYfbm.yfbmCode);

            return Content(result.ToJson());
        }
        /// <summary>
        /// Xác nhận trả thuốc
        /// </summary>
        /// <param name="Sqd"></param>
        /// <returns></returns>
        public IActionResult KcApplyNoReturnDrugsConfirm(string Sqd)
        {
            var result = _idispenseIndexInfoDmnService.KcApplyNoReturnDrugs(Sqd, OperatorProvider.GetCurrent().UserCode, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success("Trả thuốc thành công") : Error(result);
        }
        /// <summary>
        /// Trả lại đơn xin tồn kho khoa
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <returns></returns>
        public IActionResult KcApplyNoReturnDrugs(string Sqd)
        {
            var sqdObj = new
            {
                OrganizeId = OrganizeId,
                SqdArray = Sqd,
                UserCode = OperatorProvider.GetCurrent().UserId
            };
            try
            {
                var apiResp = SiteCisAPIHelper.Request<object, DefaultResponse>("/api/Hospitalized/UpdateReturnApplyNoStatus", sqdObj);

                if (apiResp.code != Newtouch.Common.Web.APIRequestHelper.ResponseResultCode.SUCCESS || apiResp.data == null)
                    return Error(apiResp.msg);
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message);
            }
            return Success("Trả lại đơn xin chuẩn bị thuốc thành công");
        }
        #endregion

        #region Tra cứu phát thuốc nội trú

        //Thuốc Thả xuống Nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetYPSelectJson()
        {
            var data = _iSysMedicineRepo.GetMedicineListByOrg(Constants.TopOrganizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                var treeModel = new TreeSelectModel
                {
                    id = item.ypCode,
                    text = item.ypmc,
                    parentId = item.ypId.ToString()
                };
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        //Khu bệnh Thả xuống Nguồn dữ liệu
        //[HttpGet]
        //[HandlerAjaxOnly]
        public IActionResult GetBQSelectJson()
        {
            var data = _iSysWardRepo.GetListByOrgId(OperatorProvider.GetCurrent().OrganizeId);
            return Content(Tools.Json.ToJson(data));
        }

        /// <summary>
        /// Truy vấn phát thuốc nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Bq"></param>
        /// <param name="patientName"></param>
        /// <param name="pc"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <param name="cw"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetZYFYCXDBGridJson(Pagination pagination, string Bq, string patientName, string ypmc, string pc,
            DateTime Kssj, DateTime Jssj, string cw, string operateType)
        {
            var reqObj = new QueryZYFYInfoReqVO
            {
                bqCode = Bq,
                patientName = patientName,
                ypmc = ypmc,
                pc = pc,
                Kssj = Kssj,
                Jssj = Jssj,
                cw = cw,
                operateType = operateType,
                organizeId = OrganizeId,
                yfbmCode = Constants.CurrentYfbm.yfbmCode
            };
            var drugDetail = new List<HospitalizationDispenseDetail>();
            //switch (operateType)
            //{
            //    case "1"://Phát thuốc
            //        drugDetail = _idispenseIndexInfoDmnService.GetZyfyQueryList(pagination, reqObj).ToList();
            //        break;
            //    case "2"://Trả thuốc
            //        drugDetail = _idispenseIndexInfoDmnService.GetZytyQueryList(pagination, reqObj).ToList();
            //        break;
            //    case ""://Tất cả
            //        drugDetail = _idispenseIndexInfoDmnService.GetZyDrugQueryList(pagination, reqObj).ToList();
            //        break;
            //}
            reqObj.operateType = operateType;
            drugDetail = _idispenseIndexInfoDmnService.GetYzCzjlList(pagination, reqObj).ToList();

            var data = new
            {
                rows = drugDetail,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Truy vấn phát thuốc nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Bq"></param>
        /// <param name="patientName"></param>
        /// <param name="pc"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <param name="cw"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetZYFYCXDBGridJsonV2(Pagination pagination, string Bq, string patientName, string ypmc, string pc,
            DateTime Kssj, DateTime Jssj, string cw, string operateType)
        {
            var reqObj = new QueryZYFYInfoReqVO
            {
                bqCode = Bq,
                patientName = patientName,
                ypmc = ypmc,
                pc = pc,
                Kssj = Kssj,
                Jssj = Jssj,
                cw = cw,
                operateType = operateType,
                organizeId = OrganizeId,
                yfbmCode = Constants.CurrentYfbm.yfbmCode
            };
            var drugDetail = new List<HospitalizationDispenseDetail>();
            reqObj.operateType = operateType;
            drugDetail = _idispenseIndexInfoDmnService.GetYzCzjlListV2(pagination, reqObj).ToList();

            var data = new
            {
                rows = drugDetail,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        #endregion

        #region Trả thuốc nội trú

        /// <summary>
        /// Trả thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult RepercussionIndex()
        {
            ViewBag.OrganizeId = _OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var printSwitch = _sysConfigRepo.GetValueByCode("hospitalizationReturnDrugAutoPrint", OrganizeId);
            ViewBag.returnDrugAutoPrint = string.IsNullOrWhiteSpace(printSwitch) || "true".Equals(printSwitch.ToLower()) ? ViewBag.autoPrintSwitch = "checked=\"checked\"" : "";

            return View("RepercussionIndex2019");
        }
        /// <summary>
        /// Trả thuốc -- Trả thuốc theo đơn xin trả thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult RepercussionIndex2021()
        {
            ViewBag.OrganizeId = _OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var printSwitch = _sysConfigRepo.GetValueByCode("hospitalizationReturnDrugAutoPrint", OrganizeId);
            ViewBag.returnDrugAutoPrint = string.IsNullOrWhiteSpace(printSwitch) || "true".Equals(printSwitch.ToLower()) ? ViewBag.autoPrintSwitch = "checked=\"checked\"" : "";

            return View("RepercussionIndex2021");
        }

        /// <summary>
        /// Dữ liệu thông tin người dùng trả thuốc nội trú
        /// </summary>
        /// <param name="zybrbq"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetZYTYBRGridJson(string zybrbq)
        {
            var rows = _idispenseIndexInfoDmnService.GetFyBq(this.OrganizeId);
            if (rows.Count <= 0)
            {
                return Content("");
            }
            var treeList = new List<TreeViewModel>();
            foreach (var item in rows)
            {
                //Thông tin bệnh nhân trả thuốc
                var patInfo = _idispenseIndexInfoDmnService.GetTyPatient(item.bqCode, OrganizeId);

                treeList.AddRange(patInfo.Select(itempat => new TreeViewModel
                {
                    id = itempat.zyh,
                    text = itempat.patientName + "-" + itempat.cw,
                    value = itempat.zyh,
                    parentId = item.bqCode,
                    isexpand = false,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = false,
                    Ex1 = "c"
                }));

                var tree = new TreeViewModel
                {
                    id = item.bqCode,
                    text = item.bqmc,
                    value = item.bqCode,
                    parentId = null,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = patInfo.Count != 0,
                    Ex1 = "p"
                };
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Dữ liệu thông tin người dùng trả thuốc nội trú
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetZytybrGridJsonV2()
        {
            var patientInfo = _idispenseIndexInfoDmnService.GetReturnDispensePatientInfo(Constants.CurrentYfbm.yfbmCode, OrganizeId);
            if (patientInfo == null || patientInfo.Count == 0) return Content("");
            var brTreeList = new List<TreeViewModel>();
            var bqTreeList = new List<TreeViewModel>();
            var tvlocker = new object();
            Parallel.ForEach(patientInfo, item =>
            {
                var itempat = new TreeViewModel
                {
                    id = item.zyh,
                    text = item.patientName + "-" + item.cw,
                    value = item.zyh,
                    parentId = item.bqCode,
                    isexpand = false,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = false,
                    Ex1 = "c"
                };
                var itembq = new TreeViewModel
                {
                    id = item.bqCode,
                    text = item.bqmc,
                    value = item.bqCode,
                    parentId = null,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = patientInfo.FindAll(p => p.bqCode == item.bqCode).Count != 0,
                    Ex1 = "p"
                };
                lock (tvlocker)
                {
                    brTreeList.Add(itempat);
                    if (!bqTreeList.Exists(p => p.id == item.bqCode))
                    {
                        bqTreeList.Add(itembq);
                    }
                }
            });
            var treeList = new List<TreeViewModel>();
            treeList.AddRange(brTreeList);
            treeList.AddRange(bqTreeList);
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Nhấp thực hiện thao tác trả thuốc
        /// </summary>
        /// <param name="ModelBQBRYZZXTYInfoVO"></param>
        /// <param name="type"></param>
        public string ZYBQTYOperate(List<ModelBQBRYZZXTYInfoVO> ModelBQBRYZZXTYInfoVO, string type)
        {
            return _idispenseIndexInfoDmnService.ZYBQTYOperate(ModelBQBRYZZXTYInfoVO, type,this.OrganizeId);
        }

        /// <summary>
        /// Tập hợp y lệnh trả thuốc nội trú
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetReturnDrugDetail(HospitalizationReturnDrugParam queryParam)
        {
            var zyhs = queryParam.Zyh ?? "";
            zyhs = zyhs.Trim().Trim(',');
            if (string.IsNullOrWhiteSpace(zyhs)) return Content("");
            var zyharr = zyhs.Split(',');
            if (zyharr.Length == 0) return Content("");
            queryParam.OrganizeId = OrganizeId;
            queryParam.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var result = _idispenseIndexInfoDmnService.SelectReturnDrugDetail(queryParam);
            var r = new List<HospitalizationReturnDispenseDetail>();
            var locker = new object();
            Parallel.ForEach(zyharr, zyh =>
            {
                if (string.IsNullOrWhiteSpace(zyh)) return;
                var tmp = result.FindAll(p => p.zyh == zyh);
                if (tmp.Count == 0) return;
                lock (locker)
                {
                    r.AddRange(tmp);
                }
            });
            return Content(r.ToJson());
        }

        /// <summary>
        /// Tập hợp y lệnh trả thuốc nội trú
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetReturnDrugDetailNoBatch(HospitalizationReturnDrugParam queryParam)
        {
            var zyhs = queryParam.Zyh ?? "";
            zyhs = zyhs.Trim().Trim(',');
            if (string.IsNullOrWhiteSpace(zyhs)) return Content(null);
            var zyharr = zyhs.Split(',');
            if (zyharr.Length == 0) return Content("");
            queryParam.OrganizeId = OrganizeId;
            queryParam.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var result = _idispenseIndexInfoDmnService.SelectReturnDrugDetailNoBatch(queryParam);
            var r = new List<HospitalizationReturnDispenseDetail>();
            var locker = new object();
            Parallel.ForEach(zyharr, zyh =>
            {
                if (string.IsNullOrWhiteSpace(zyh)) return;
                var tmp = result.FindAll(p => p.zyh == zyh);
                if (tmp.Count == 0) return;
                lock (locker)
                {
                    r.AddRange(tmp);
                }
            });
            return Content(r.ToJson());
        }

        /// <summary>
        /// Tập hợp y lệnh trả thuốc nội trúV2
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetReturnDrugDetailV2(HospitalizationReturnDrugParam queryParam)
        {
            var zyhs = queryParam.Zyh ?? "";
            zyhs = zyhs.Trim().Trim(',');
            if (string.IsNullOrWhiteSpace(zyhs)) return Content("");
            var zyharr = zyhs.Split(',');
            if (zyharr.Length == 0) return Content("");
            queryParam.OrganizeId = OrganizeId;
            queryParam.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var result = _idispenseIndexInfoDmnService.SelectReturnDrugDetailV2(queryParam);
            var r = new List<HospitalizationReturnDispenseDetail>();
            var locker = new object();
            Parallel.ForEach(zyharr, zyh =>
            {
                if (string.IsNullOrWhiteSpace(zyh)) return;
                var tmp = result.FindAll(p => p.zyh == zyh);
                if (tmp.Count == 0) return;
                lock (locker)
                {
                    r.AddRange(tmp);
                }
            });
            return Content(r.ToJson());
        }

        /// <summary>
        /// Tập hợp y lệnh trả thuốc nội trúV2
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetReturnDrugDetailNoBatchV2(HospitalizationReturnDrugParam queryParam)
        {
            var zyhs = queryParam.Zyh ?? "";
            zyhs = zyhs.Trim().Trim(',');
            if (string.IsNullOrWhiteSpace(zyhs)) return Content(null);
            var zyharr = zyhs.Split(',');
            if (zyharr.Length == 0) return Content("");
            queryParam.OrganizeId = OrganizeId;
            queryParam.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var result = _idispenseIndexInfoDmnService.SelectReturnDrugDetailNoBatchV2(queryParam);
            var r = new List<HospitalizationReturnDispenseDetail>();
            var locker = new object();
            Parallel.ForEach(zyharr, zyh =>
            {
                if (string.IsNullOrWhiteSpace(zyh)) return;
                var tmp = result.FindAll(p => p.zyh == zyh);
                if (tmp.Count == 0) return;
                lock (locker)
                {
                    r.AddRange(tmp);
                }
            });
            return Content(r.ToJson());
        }

        /// <summary>
        /// Lấy số đơn xin trả thuốc nội trú
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetZyTyApplyNo(HospitalizationReturnDrugParam queryParam)
        {
            var zyhs = queryParam.Zyh ?? "";
            zyhs = zyhs.Trim().Trim(',');
            if (string.IsNullOrWhiteSpace(zyhs)) return Content(null);
            var zyharr = zyhs.Split(',');
            if (zyharr.Length == 0) return Content("");
            queryParam.OrganizeId = OrganizeId;
            queryParam.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var result = _idispenseIndexInfoDmnService.GetZyTyApplyNo(queryParam);
            var r = new List<ZyTyApplyNoVO>();
            var locker = new object();
            Parallel.ForEach(zyharr, zyh =>
            {
                if (string.IsNullOrWhiteSpace(zyh)) return;
                var tmp = result.FindAll(p => p.zyh == zyh);
                if (tmp.Count == 0) return;
                lock (locker)
                {
                    r.AddRange(tmp);
                }
            });
            return Content(r.ToJson());
        }

        /// <summary>
        /// Thực hiện trả thuốc nội trú
        /// </summary>
        /// <param name="tyParams"></param>
        /// <returns></returns>
        public IActionResult ExecuteReturnDrug(tyParam[] tyParams)
        {
            if (tyParams == null || tyParams.Length == 0) return Error("Vui lòng truyền thông tin thuốc cần trả");
            string returnDrugBillNo;
            var result = _resourcesOperateApp.HospitalizatiionReturnMedicine(tyParams.ToList(), Constants.CurrentYfbm.yfbmCode, OrganizeId, OperatorProvider.GetCurrent().UserCode, out returnDrugBillNo);
            var zyhArr = tyParams.Select(p => p.zyh).Distinct().ToArray();
            string zyhs = "";
            zyhs = string.Join(",", zyhArr);
            _dispensingDmnService.SyncPatFee(OrganizeId, zyhs, 1);
            _dispensingDmnService.Updatezy_brxxexpand(OrganizeId, zyhs);
            return string.IsNullOrWhiteSpace(result) ? Success("", returnDrugBillNo) : Error(result);
        }

        #endregion

        

        /// <summary>
        /// Thực hiện trả thuốc nội trúV2
        /// </summary>
        /// <param name="tyParams"></param>
        /// <returns></returns>
        public IActionResult ExecuteReturnDrugV2(tyParam[] tyParams)
        {
            if (tyParams == null || tyParams.Length == 0) return Error("Vui lòng truyền thông tin thuốc cần trả");
            string returnDrugBillNo;
            var result = _resourcesOperateApp.HospitalizatiionReturnMedicineV2(tyParams.ToList(), Constants.CurrentYfbm.yfbmCode, OrganizeId, OperatorProvider.GetCurrent().UserCode, out returnDrugBillNo);
            return string.IsNullOrWhiteSpace(result) ? Success("", returnDrugBillNo) : Error(result);
        }
        
        /// <summary>
        /// Truy vấn phát thuốcV2
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryIndexV2()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = _OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }

        public IActionResult FybdIndex()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = _OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }

        /// <summary>
        /// Khu bệnh phát thuốc nội trú
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFybdBrxxTree()
        {
            var yfbmCode = Constants.GetCurrentYfbm(OperatorProvider.GetCurrent().UserId).yfbmCode;
            var patientInfo = _idispenseIndexInfoDmnService.GetFybdBrxxTree(yfbmCode, OrganizeId);

            if (patientInfo == null || patientInfo.Count == 0) return Content("");
            var brTreeList = new List<TreeViewModel>();
            var bqTreeList = new List<TreeViewModel>();
            var tvlocker = new object();
            Parallel.ForEach(patientInfo, item =>
            {
                var itempat = new TreeViewModel
                {
                    id = item.zyh+ item.bqCode+ item.cw,
                    text = item.patientName + "-" + item.cw,
                    value = item.zyh,
                    parentId = item.bqCode,
                    isexpand = false,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = false,
                    Ex1 = "c"
                };
                var itembq = new TreeViewModel
                {
                    id = item.bqCode,
                    text = item.bqmc,
                    value = item.bqCode,
                    parentId = null,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = patientInfo.FindAll(p => p.bqCode == item.bqCode).Count != 0,
                    Ex1 = "p"
                };
                lock (tvlocker)
                {
                    brTreeList.Add(itempat);
                    if (!bqTreeList.Exists(p => p.id == item.bqCode))
                    {
                        bqTreeList.Add(itembq);
                    }
                }
            });
            var treeList = new List<TreeViewModel>();
            treeList.AddRange(brTreeList);
            treeList.AddRange(bqTreeList);
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Truy vấn phát thuốc nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Bq"></param>
        /// <param name="patientName"></param>
        /// <param name="pc"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <param name="cw"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFybdList(Pagination pagination, string operatetime, string Zyh,string Bqbm,
            DateTime Kssj, DateTime Jssj)
        {
            var reqObj = new QueryZYFYInfoReqVOV2
            {
                operatetime = operatetime,
                Zyh = Zyh,
                bqCode= Bqbm,
                Kssj = Kssj,
                Jssj = Jssj,
                organizeId = OrganizeId,
                yfbmCode = Constants.CurrentYfbm.yfbmCode
            };

            var drugDetail = new List<HospitalizationDispenseDetailV2>();
            reqObj.operateType = "1";
            drugDetail = _idispenseIndexInfoDmnService.GetFybdList(pagination, reqObj).ToList();

            var data = new
            {
                rows = drugDetail,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFybdComboboxList(string Zyh,string Bqbm,
            DateTime Kssj, DateTime Jssj)
        {
            var reqObj = new QueryZYFYInfoReqVO
            {
                Zyh = Zyh,
                bqCode= Bqbm,
                Kssj = Kssj,
                Jssj = Jssj,
                organizeId = OrganizeId,
                yfbmCode = Constants.CurrentYfbm.yfbmCode
            };

            var drugDetail = new List<FybdComboboxList>();
            reqObj.operateType = "1";
            drugDetail = _idispenseIndexInfoDmnService.GetFybdComboboxList(reqObj).ToList();

            return Content(drugDetail.ToJson());
        }

        #region Đơn nội trú
        public IActionResult PrescriptionPadQuery()
        {
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            ViewBag.OrganizeId = _OrganizeId;
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetZycfGridJson(Pagination pagination, string bq,string yzxz, DateTime kssj, DateTime jssj, string keyword)
        {
            var reqObj = new ZycfcxVo
            {
                bq = bq,
                kssj = kssj,
                jssj = jssj,
                keyword = keyword,
                organizeId = OrganizeId,
                yzxz= yzxz
            };
            var data = new
            {
                rows = _idispenseIndexInfoDmnService.GetZycfList(pagination, reqObj).ToList(),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetZycfGridDetailJson(Pagination pagination,string yzxz, string yzh, DateTime? zxrq=null)
        {
            var reqObj = new ZycfcxVo
            {
                yzh = yzh,
                yzxz=yzxz,
                zxrq=zxrq,
                organizeId = OrganizeId
            };
            var data = new
            {
                rows = _idispenseIndexInfoDmnService.GetZycfDetailList(pagination, reqObj).ToList(),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        #endregion
    }
}