using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.OutputDto.Outpatient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset.Zyypyz;
using Newtouch.Tools;
using Newtouch.Domain.ValueObjects.Apply;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using Newtouch.Domain.ViewModels;
using System.IO;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// Thực hiện y lệnh
    /// </summary>
    public class OrderExecutionController : OrgControllerBase
    {

        private readonly IOrderExecutionDmnService _OrderExecutionDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IInpatientPatientInfoRepo _inpatientPatientInfoRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IDoctorserviceDmnService _doctorserviceDmnService;
        private readonly IMedicalAdviceBindingFeeRepo _medicalAdviceBindingFeeRepo;
        private readonly IInpatientLongTermOrderRepo _InpatientLongTermOrderRepo;
        private readonly IInpatientSTATOrderRepo _inpatientSTATOrderRepo;
        private readonly IXtjyjcFileUploadRepo _xyjyjcfileuploadRepo;
        int lyxh = 0;
        private string IsRehabAuthtoNurse;
        private bool isNurse;
        private bool isRehabDoctor;

        private readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        private readonly string[] AllowedPdfExtensions = { ".pdf" };
        private readonly int MaxFileSize = 10 * 1024 * 1024; // 10MB

        /// <summary>
        /// Y lệnh bằng văn bản cần thực hiện
        /// </summary>
        private bool wnes;//true-Y lệnh bằng văn bản cần thực hiện  false-Y lệnh bằng văn bản không cần thực hiện
        private string medicalInsurance;
        private string iskfyzjf;//Y lệnh phục hồi chức năng có tính phí không

        public OrderExecutionController(IOrderExecutionDmnService OrderExecutionDmnService)
        {
            this._OrderExecutionDmnService = OrderExecutionDmnService;
            var wnesV = _sysConfigRepo.GetValueByCode("wordsNeedExecuteSwitch", OrganizeId);
            if (string.IsNullOrWhiteSpace(wnesV)) wnes = false;
            if ("true".Equals(wnesV.ToLower().Trim()) || "t".Equals(wnesV.ToLower().Trim())) wnes = true;
            IsRehabAuthtoNurse = _sysConfigRepo.GetValueByCode("IsRehabAuthtoNurse", this.OrganizeId);
            isNurse = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "Nurse");
            isRehabDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "RehabDoctor");
            medicalInsurance = _sysConfigRepo.GetValueByCode("medicalInsurance", OrganizeId);
            iskfyzjf = _sysConfigRepo.GetValueByCode("iskfyzjf", OrganizeId);
            ViewBag.isqfswith = _sysConfigRepo.GetValueByCode("accountqfexecute_switch", OrganizeId);//Công tắc lập, thực hiện y lệnh nợ phí
        }
        #region 医嘱执行页面
        /// <summary>
        /// Lấy danh sách y lệnh chờ thực hiện
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="patList"></param>
        /// <param name="organizeId"></param>
        /// <param name="zxsj"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string patList, string organizeId, string zxsj)
        {
            IList<OrderExecutionVO> rowData = new List<OrderExecutionVO>();
            if (!string.IsNullOrWhiteSpace(patList))
            {
                if (!string.IsNullOrWhiteSpace(IsRehabAuthtoNurse) && IsRehabAuthtoNurse == "0")
                {
                    if (isNurse && isRehabDoctor)
                    {
                        rowData = _OrderExecutionDmnService.GetOrderExecutionYZList(ref pagination, patList, OrganizeId, zxsj, wnes);
                    }
                    else if (isRehabDoctor)
                    {
                        rowData = _OrderExecutionDmnService.GetOrderExecutionYZList(ref pagination, patList, OrganizeId, zxsj, wnes, IsRehabAuthtoNurse, true, this.UserIdentity.DepartmentCode);
                    }
                    else if (isNurse)
                    {
                        rowData = _OrderExecutionDmnService.GetOrderExecutionYZList(ref pagination, patList, OrganizeId, zxsj, wnes, IsRehabAuthtoNurse, false);
                    }
                }
                else
                {
                    rowData = _OrderExecutionDmnService.GetOrderExecutionYZList(ref pagination, patList, OrganizeId, zxsj, wnes);
                }
            }
            var data = new
            {
                rows = rowData,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        // GET: NurseManage/OrderExecution
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BindingFeeForm()
        {
            ViewBag.openWzhckc = _sysConfigRepo.GetValueByCode("openWzhckc", OrganizeId);//Vật tư tiêu hao có bật logic tồn kho không
            return View();
        }
        /// <summary>
        /// Lấy cây y lệnh chờ thực hiện của bệnh nhân khu bệnh
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetPatWardTree(string aa, DateTime zxsj, string keyword)
        {
            var staffId = UserIdentity.StaffId;
            var wardTree = _OrderExecutionDmnService.GetWardTree(staffId);
            var patTree = _OrderExecutionDmnService.GetPatTree(staffId, zxsj, wnes, OrganizeId);//wnes ? _OrderExecutionDmnService.GetPatTreeIncludeWzyz(staffId, zxsj)
                                                                                                //: _OrderExecutionDmnService.GetPatTree(staffId, zxsj);
            if (!string.IsNullOrWhiteSpace(keyword))
                patTree = patTree.Where(p => p.zyh.Contains(keyword)).ToList();
            string[] aasz = new string[200];
            if (!string.IsNullOrWhiteSpace(aa))
            {
                aasz = aa.Split(',');
            }

            var treeList = new List<TreeViewModel>();
            foreach (var item in wardTree)
            {
                var patInfo = patTree.Where(p => p.bqCode == item.bqCode).ToList();
                var NewPatInfo = patInfo.OrderBy(p => p.BedNo);
                foreach (var itempat in NewPatInfo)
                { 
                    string gender = itempat.sex == "1" ? "男" : "女";
                    var treepat = new TreeViewModel
                    {
                        id = itempat.zyh,
                        //Số giường + Họ tên(Số ngày nhập viện)+Số nhập viện + Tuổi +Giới tính
                        text = itempat.BedNo + "-" + itempat.hzxm + "(" + itempat.inHosDays + "天)" + "-" + itempat.zyh + "-" + itempat.nl + "岁-" + gender,
                        value = itempat.zyh,
                        parentId = item.bqCode,
                        isexpand = false,
                        complete = true,
                        showcheck = true,
                        //checkstate = 0,
                        checkstate = 0,
                        hasChildren = false,
                        Ex1 = "c"
                    };
                    if (((IList)aasz).Contains(itempat.zyh))
                    {
                        treepat.checkstate = 1;
                    }
                    treeList.Add(treepat);
                }

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
        #endregion

        #region 执行当前
        /// <summary>
        /// Thực hiện y lệnh hiện tại
        /// </summary>
        /// <param name="orderList">yzid,yzxh,zyh,yzlx</param>
        /// <param name="Vzxsj">Thời gian thực hiện</param>
        /// <returns></returns>
        public ActionResult submitOrderExecutionList(string patlist, IList<ApiResponseVO> orderList, DateTime Vzxsj)
        {
            Validatepatryrq(patlist, Vzxsj);
            //Gọi giao diện trả về
            var result = doOrderExecution(orderList, Vzxsj);
            if (result.Split('|')[0] != "T") return Error(result.Split('|')[1]);
            var cnt = orderList.Where(a => a.yzlx == Convert.ToInt32(EnumYzlx.Yp) || a.yzlx == Convert.ToInt32(EnumYzlx.Cydy) || a.yzlx == Convert.ToInt32(EnumYzlx.zcy)).ToList().Count;
            var data = new { cnt = cnt, lyxh = lyxh };
            return Success(result.Split('|')[1], data.ToJson());
        }

        /// <summary>
        /// Xây dựng và gọi giao diện
        /// </summary>
        /// <param name="orderListAll">Apilist</param>
        /// <param name="Vzxsj">Thời gian thực hiện</param>
        /// <returns></returns>
        public string doOrderExecution(IList<ApiResponseVO> orderListAll, DateTime Vzxsj, int? yzxz = null)
        {
            try
            {
                var user = UserIdentity;
                //Y lệnh có thể thực hiện
                var isOkOrderExecutionresult = _OrderExecutionDmnService.IsOKOrderExecution(orderListAll, Vzxsj,this.OrganizeId, UserIdentity.rygh);
                if (isOkOrderExecutionresult.Split('|')[0] != "T") return isOkOrderExecutionresult;
                //Y lệnh thuốc(Đẩy đến nhà thuốc)
                IList<ApiResponseVO> orderYpList = orderListAll.Where(a => (a.yzlx == Convert.ToInt32(EnumYzlx.Yp) || a.yzlx == Convert.ToInt32(EnumYzlx.Cydy) || a.yzlx == Convert.ToInt32(EnumYzlx.zcy))
                && a.isjf != EnumSF.f.GetDescription() && a.yply != EnumYply.ksby.GetDescription()).ToList();
                //Y lệnh hạng mục
                IList<ApiResponseVO> orderXmList = orderListAll.Where(a => (a.yzlx != Convert.ToInt32(EnumYzlx.Yp) && a.yzlx != Convert.ToInt32(EnumYzlx.Cydy) && a.yzlx != Convert.ToInt32(EnumYzlx.zcy)) || a.yfztbs != null).ToList();
                //Y lệnh không tính phí, y lệnh thuốc dự trữ khoa 
                IList<ApiResponseVO> nofeeorderYpList = orderListAll.Where(a => (a.yzlx == Convert.ToInt32(EnumYzlx.Yp) || a.yzlx == Convert.ToInt32(EnumYzlx.Cydy) || a.yzlx == Convert.ToInt32(EnumYzlx.zcy))
                && (a.isjf == EnumSF.f.GetDescription() || a.yply == EnumYply.ksby.GetDescription())).ToList();
                //Số thứ tự nhận thuốc
                lyxh = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueIntValue("fyqqk_lyxh", OrganizeId);

                if (orderYpList.Count > 0)
                {
                    //Xây dựngapiGiao diện RequestJson
                    var orderList = _OrderExecutionDmnService.GetapiList(user, orderYpList, Vzxsj, lyxh);
                    var orderExecution = new
                    {
                        OrganizeId = this.OrganizeId,
                        yzList = orderList,
                        ClientNo = Guid.NewGuid(),
                        TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                    };
                    var apiOrderExecution = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/Zyypyz/Yzzx", orderExecution);
                    if (apiOrderExecution.code == APIRequestHelper.ResponseResultCode.SUCCESS && apiOrderExecution.data != null)
                    {
                        var successDoOrder = apiOrderExecution.data.ToString().ToObject<RequestOrderExecMsgDto>(); //Dữ liệu giao diện trả về 
                        if (successDoOrder.Data != null && successDoOrder.IsSucceed && successDoOrder.Data.Count > 0)
                        {
                            string resultMsg = "";
                            List<YzDetail> successDoOrderYp = Tools.Json.ToList<YzDetail>(successDoOrder.Data.ToString());
                            if (successDoOrderYp.Count >= 30)
                            {
                                AppLogger.Info("药房执行成功：" + successDoOrderYp.ToArray().ToJson());
                                //Ngăn cắt ngắn 
                                var zyhlist = successDoOrderYp.Select(p => p.zyh).Distinct();
                                foreach (string zyh in zyhlist)
                                {
                                    var zyhorder = successDoOrderYp.FindAll(p => p.zyh == zyh);
                                    if (zyhorder.Count > 0)
                                    {
                                        resultMsg += _OrderExecutionDmnService.OrderExecutionSubmit(user, zyhorder, lyxh, Vzxsj);
                                        resultMsg += "【" + zyh + "】";
                                        AppLogger.Info(resultMsg);
                                    }
                                }
                            }
                            else
                            {
                                resultMsg = _OrderExecutionDmnService.OrderExecutionSubmit(user, successDoOrderYp, lyxh, Vzxsj);
                                if (resultMsg.Split('|')[0] != "T")
                                {
                                    return resultMsg;
                                }
                            }
                        }
                        else
                        {
                            return "F|" + successDoOrder.ResultMsg;
                        }

                    }
                    else
                    {
                        return "F|" + apiOrderExecution.sub_msg;
                    }
                }
                if (nofeeorderYpList.Count > 0)
                {
                    var resultMsg = _OrderExecutionDmnService.NoFeeOrderExecutionSubmit(user, nofeeorderYpList, lyxh, Vzxsj);
                    if (resultMsg.Split('|')[0] != "T")
                    {
                        return resultMsg;
                    }
                }
                if (orderXmList.Count <= 0) return "T|执行成功";
                //Thực hiện hạng mục
                var xmMsg = wnes ? _OrderExecutionDmnService.OrderExecutionXmWithWzyz(user.rygh, orderXmList, lyxh, Vzxsj, this.OrganizeId, yzxz)
                    : _OrderExecutionDmnService.OrderExecutionXM(user, orderXmList, lyxh, Vzxsj, this.OrganizeId, medicalInsurance, yzxz);
                return xmMsg.Split('|')[0] != "T" ? xmMsg : "T|执行成功";

            }
            catch (Exception ex)
            {
                return "F|" + ex.InnerException;
            }
        }
        #endregion

        #region 执行临时、 长期、全部
        /// <summary>
        /// Thực hiện y lệnh tạm thời, dài hạn, tất cả
        /// </summary>
        /// <param name="patlist">Số nhập viện</param>
        /// <param name="yzxz">Tạm thời, dài hạn, tất cả</param>
        /// <param name="Vzxsj"></param>
        /// <returns></returns>
        public ActionResult submitOrderExecutionListbyPat(string patlist, int yzxz, DateTime Vzxsj)
        {
            List<ApiResponseVO> apiList = new List<ApiResponseVO>();
            Validatepatryrq(patlist, Vzxsj);
            if (!string.IsNullOrWhiteSpace(IsRehabAuthtoNurse) && IsRehabAuthtoNurse == "0")
            {
                if (isNurse && isRehabDoctor)
                {
                    apiList = wnes ? _OrderExecutionDmnService.GetAllYZWithWzYz(OrganizeId, patlist, yzxz, Vzxsj) :
                             _OrderExecutionDmnService.GetAllYZ(patlist, yzxz, Vzxsj, IsRehabAuthtoNurse);//Lấy tất cả y lệnh thực hiện
                }
                else if (isRehabDoctor)
                {
                    apiList = _OrderExecutionDmnService.GetkfYz(OrganizeId, patlist, yzxz, Vzxsj, this.UserIdentity.DepartmentCode);
                }
                else if (isNurse)
                {
                    apiList = wnes ? _OrderExecutionDmnService.GetAllYZWithWzYz(OrganizeId, patlist, yzxz, Vzxsj, IsRehabAuthtoNurse) :
                             _OrderExecutionDmnService.GetAllYZ(patlist, yzxz, Vzxsj, IsRehabAuthtoNurse);//Lấy tất cả y lệnh thực hiện
                }
            }
            else
            {
                apiList = wnes ? _OrderExecutionDmnService.GetAllYZWithWzYz(OrganizeId, patlist, yzxz, Vzxsj) :
                        _OrderExecutionDmnService.GetAllYZ(patlist, yzxz, Vzxsj);//Lấy tất cả y lệnh thực hiện
            }

            //Giao diện trả về list 
            var result = doOrderExecution(apiList, Vzxsj, yzxz);
            if (result.Split('|')[0] != "T") return Error(result.Split('|')[1]);
            var cnt = apiList.Where(a => a.yzlx == Convert.ToInt32(EnumYzlx.Yp) || a.yzlx == Convert.ToInt32(EnumYzlx.Cydy) || a.yzlx == Convert.ToInt32(EnumYzlx.zcy)).ToList().Count;
            var data = new { cnt = cnt, lyxh = lyxh };
            return Success(result.Split('|')[1], data.ToJson());

        }

        /// <summary>
        /// Khi thực hiện y lệnh, xác thực ngày nhập viện+Đánh giá tồn kho vật tư tiêu hao
        /// </summary>
        /// <param name="patlist"></param>
        public void Validatepatryrq(string patlist, DateTime Vzxsj)
        {
            if (string.IsNullOrWhiteSpace(patlist))
            {
                throw new FailedException("缺少病人住院号");
            }
            string[] patarr = patlist.Split(',');
            if (patarr != null && patarr.Count() > 0)
            {
                for (int i = 0; i < patarr.Count(); i++)
                {
                    var patzyh = patarr[i];
                    if (patzyh == null || string.IsNullOrWhiteSpace(patzyh))
                    {
                        continue;
                    }
                    var parentity = _inpatientPatientInfoRepo.IQueryable().Where(p => p.zyh == patzyh && p.OrganizeId == OrganizeId && p.zt == "1").ToList();

                    if (parentity.Count() < 0)
                    {
                        throw new FailedException("住院号为" + patarr[i] + "的病人缺少住院信息");
                    }
                    if (parentity.Count() > 1)
                    {
                        throw new FailedException("住院号为" + patarr[i] + "的病人存在多条住院信息");
                    }

                    if (DateTimeManger.DateDiff(DateInterval.Day, DateTime.Parse(parentity[0].ryrq.ToShortDateString()), DateTime.Parse(Vzxsj.ToShortDateString())) < 0)
                    {
                        throw new FailedException("住院号" + patarr[i] + "的病人入院日期为" + parentity[0].ryrq.ToShortDateString() + "。无法在此之前执行医嘱，请核实");
                    }
                }
            }
        }
        #endregion

        #region pacs 接口
        public ActionResult pushApplicationform(IList<ApiResponseVO> orderList)
        {

            var uri = ConfigurationHelper.GetAppConfigValue("pacsUrl");
            List<CheckApplicationfromDTO> datalist = new List<CheckApplicationfromDTO>();
            foreach (var item in orderList)
            {
                if (item != null)
                {
                    var data = _OrderExecutionDmnService.pushApplicationform(item, this.OrganizeId, "Y");
                    if (data != null)
                    {
                        datalist.Add(data);
                    }

                }
            }
            var url = uri + "URISService/services/interface/requestorder";
            if (datalist == null)
            {
                return Success("无数据");
            }
            var msagss = "";
            foreach (var data in datalist)
            {
                string datajson = Tools.Json.ToJson(data);
                try
                {
                    System.Net.HttpWebRequest request = null;
                    System.Net.WebResponse response = null;

                    request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                    request.ProtocolVersion = System.Net.HttpVersion.Version10;
                    request.Method = "POST";

                    request.ContentType = "application/json";
                    request.CookieContainer = null;//Khi lấy mã xác minh thì lấy đượccookiesẽ được đính kèm trong vùng chứa này
                    request.AllowAutoRedirect = true;
                    request.KeepAlive = true;//Thiết lập kết nối bền vững
                    //request.ContentLength = cs.Length;
                    request.Host = "192.168.0.101";
                    request.UserAgent = "PostmanRuntime/7.29.2";
                    request.Accept = "*/*";
                    byte[] datas = System.Text.Encoding.UTF8.GetBytes(datajson);
                    using (System.IO.Stream stream = request.GetRequestStream())
                    {
                        stream.Write(datas, 0, datas.Length);
                    }

                    response = (System.Net.HttpWebResponse)request.GetResponse();
                    string outputText = string.Empty;
                    using (System.IO.Stream responseStm = response.GetResponseStream())
                    {
                        System.IO.StreamReader redStm = new System.IO.StreamReader(responseStm, System.Text.Encoding.UTF8);
                        outputText = redStm.ReadToEnd();
                    }

                    var apiresp = JavaScriptJsonSerializerHelper.Deserialize<RefJson>(outputText);

                    AppLogger.Info(string.Format("Pacs检查申请单入参：{0}，出参结果：{1}", datajson, outputText));

                    if (apiresp == null || apiresp.status == "Failure")
                    {
                        msagss = apiresp.errorCode;
                        AppLogger.Info(string.Format("Pacs检查申请单入参：{0}，出参结果：{1}", datajson, outputText));
                    }

                }
                catch (Exception e)
                {

                    return Success("失败", e.Message);
                }
            }

            return Success("");


        }
        #endregion

        #region 医技执行
        public ActionResult MedicalSkillExecution()
        {
            return View();
        }
        public ActionResult MedicalSkillQuery()
        {
            return View();
        }
        public ActionResult ApplyFormUploadForm()
        {
            return View();
        }
        /// <summary>
        /// Phiếu yêu cầu chờ thực hiện của khoa kỹ thuật y tế
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fylx"></param>
        /// <param name="hzlx"></param>
        /// <param name="sqdlx"></param>
        /// <param name="zxzt"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetJyjcExecGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string fylx, string hzlx,
            string sqdlx, string zxzt, string keyword = null)
        {
            var data = new
            {
                rows = _OrderExecutionDmnService.GetJyjcSqd(pagination, OrganizeId, kssj, jssj, zxzt, hzlx, fylx, sqdlx, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// Thực hiện hàng loạt
        /// </summary>
        /// <param name="jyjclist"></param>
        /// <returns></returns>
        public ActionResult jyjcExec(List<jyjcExecReq> jyjclist)
        {
            _doctorserviceDmnService.jyjcExec(jyjclist, OrganizeId, UserIdentity.rygh);
            _doctorserviceDmnService.UpdatejyjcExecIsjf(jyjclist.Select(m => m.sqdh).ToList(), OrganizeId, UserIdentity.rygh,"1");
            return Success();
        }
        /// <summary>
        /// Hủy thực hiện
        /// </summary>
        /// <param name="jyjclist"></param>
        /// <returns></returns>
        public ActionResult CancaljyjcExec(List<string> jyjclist)
        {
            _doctorserviceDmnService.CancaljyjcExec(jyjclist, OrganizeId, UserIdentity.rygh);
            _doctorserviceDmnService.UpdatejyjcExecIsjf(jyjclist, OrganizeId, UserIdentity.rygh, "0");
            return Success();
        }
        /// <summary>
        /// Tra cứu hồ sơ thực hiện
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fylx"></param>
        /// <param name="hzlx"></param>
        /// <param name="sqdlx"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetJyjcExecRecordJson(Pagination pagination, DateTime kssj, DateTime jssj, string fylx, string hzlx,
           string sqdlx, string keyword = null)
        {
            var data = new
            {
                rows = _OrderExecutionDmnService.GetJyjcSqdRecord(pagination, OrganizeId, kssj, jssj, hzlx, fylx, sqdlx, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// Báo cáo của khoa kỹ thuật y tế
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ApplyFormUpload(FileUploadModel model)
        {
            var uploadedFiles = new List<UploadedFile>();
            var errorMessages = new List<string>();
            if (ModelState.IsValid)
            {
                List<XtjyjcFileUploadEntity> entityLists = new List<XtjyjcFileUploadEntity>();
                if (model.Files != null && model.Files.Any())
                {
                    foreach (var file in model.Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            // Xác thực kích thước tệp
                            if (file.ContentLength > MaxFileSize)
                            {
                                errorMessages.Add($"{file.FileName} 文件过大，最大允许10MB");
                                continue;
                            }

                            // Xác thực loại tệp
                            var fileExtension = Path.GetExtension(file.FileName).ToLower();
                            if (!AllowedImageExtensions.Contains(fileExtension) &&
                                !AllowedPdfExtensions.Contains(fileExtension))
                            {
                                errorMessages.Add($"{file.FileName} 文件类型不支持，仅支持图片和PDF格式");
                                continue;
                            }

                            try
                            {
                                // Tạo tên tệp duy nhất
                                var fileName = file.FileName;
                                var fileUrl = "~/ReportUploads/" + DateTime.Now.ToString("yyyyMMdd") + "/" + model.sqdh+"/";
                                var path = Path.Combine(Server.MapPath("~/ReportUploads/"+ DateTime.Now.ToString("yyyyMMdd") + "/"+model.sqdh), fileName);
                                // Đảm bảo thư mục tải lên tồn tại
                                Directory.CreateDirectory(Path.GetDirectoryName(path));

                                // Lưu tệp
                                file.SaveAs(path);

                                entityLists.Add(new XtjyjcFileUploadEntity
                                {
                                    OrganizeId = this.OrganizeId,
                                    Sqdh = model.sqdh,
                                    FileName= fileName.Split('.')[0],
                                    FileSize= file.ContentLength,
                                    FileType = fileExtension,
                                    ContentType = file.ContentType,
                                    FileUrl = fileUrl
                                });
                            }
                            catch (Exception ex)
                            {
                                errorMessages.Add($"{file.FileName} 上传失败: {ex.Message}");
                            }
                        }
                    }
                    _xyjyjcfileuploadRepo.SubmitForm(entityLists);
                }
            }
            var reportList = _xyjyjcfileuploadRepo.IQueryable().Where(p=>p.Sqdh==model.sqdh && p.OrganizeId==this.OrganizeId&&p.zt=="1").ToList() ;
            foreach (var data in reportList)
            {
                uploadedFiles.Add(new UploadedFile
                {
                    FileName = data.FileUrl+data.FileName+data.FileType,
                    OriginalName = data.FileName+data.FileType,
                    Size = data.FileSize,
                    ContentType = data.ContentType,
                    Id=data.Id
                });
            }

            ViewBag.UploadedFiles = uploadedFiles;
            ViewBag.ErrorMessages = errorMessages;
            ViewBag.sqdh = model.sqdh;
            ViewBag.Success = uploadedFiles.Any();
            return View("ApplyFormUpload", model);
        }
        /// <summary>
        /// Xóa báo cáo
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public ActionResult DeleteReprot(string reportId)
        {
            _xyjyjcfileuploadRepo.DeleteForm(reportId);
            return Success();
        }
        /// <summary>
        /// Tải xuống báo cáo
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult Download(string fileName)
        {
            //var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
            var path = Server.MapPath(fileName);
            if (System.IO.File.Exists(path))
            {
                var fileBytes = System.IO.File.ReadAllBytes(path);
                var contentType = GetContentType(fileName);
                return File(fileBytes, contentType, fileName);
            }
            return HttpNotFound();
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".pdf":
                    return "application/pdf";
                default:
                    return "application/octet-stream";
            }
        }
        public class UploadedFile
        {
            public string FileName { get; set; }
            public string OriginalName { get; set; }
            public int Size { get; set; }
            public string ContentType { get; set; }
            public string Id { get; set; }

            public string GetFileSize()
            {
                if (Size < 1024) return $"{Size} B";
                if (Size < 1024 * 1024) return $"{Size / 1024.0:F1} KB";
                return $"{Size / (1024.0 * 1024.0):F1} MB";
            }

            public bool IsImage()
            {
                return ContentType.StartsWith("image/");
            }
        }
        #endregion

        #region 附属医嘱
        /// <summary>
        /// Lưu thao tác
        /// </summary>
        /// <returns></returns>
        public ActionResult AddData(string zyh, string yzh, string zh, List<YzbindingfeeVo> ItemFeeVO, string yzId)
        {
            try
            {


                //var bindinglist = _medicalAdviceBindingFeeRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == OrganizeId && p.yzh == yzh && p.zt == "1").ToList();
                //if (bindinglist != null && bindinglist.Count > 0)
                //{
                //    foreach (var item in bindinglist)
                //    {
                //        _medicalAdviceBindingFeeRepo.DeleteForm(item.newid);
                //    }
                //}
                if (ItemFeeVO != null)
                {
                    foreach (var item in ItemFeeVO)
                    {
                        MedicalAdviceBindingFeeEntity medicalAdviceBindingFeeEntity = new MedicalAdviceBindingFeeEntity();
                        medicalAdviceBindingFeeEntity.zyh = zyh;
                        medicalAdviceBindingFeeEntity.yzh = yzh;
                        medicalAdviceBindingFeeEntity.zh = zh;
                        medicalAdviceBindingFeeEntity.sfxm = item.sfxm;
                        medicalAdviceBindingFeeEntity.sfxmmc = item.sfxmmc;
                        medicalAdviceBindingFeeEntity.dlmc = item.dlmc;
                        medicalAdviceBindingFeeEntity.sl = item.sl;
                        medicalAdviceBindingFeeEntity.dw = item.dw;
                        medicalAdviceBindingFeeEntity.dl = item.dl;
                        medicalAdviceBindingFeeEntity.dj = item.dj;
                        medicalAdviceBindingFeeEntity.je = item.je;
                        medicalAdviceBindingFeeEntity.yfdm = item.yfdm;
                        medicalAdviceBindingFeeEntity.cls = item.cls;
                        medicalAdviceBindingFeeEntity.zt = "1";
                        medicalAdviceBindingFeeEntity.OrganizeId = this.OrganizeId;
                        medicalAdviceBindingFeeEntity.gg = item.gg;
                        medicalAdviceBindingFeeEntity.pcmc = item.pcmc;
                        medicalAdviceBindingFeeEntity.yzxz = item.yzxz;
                        medicalAdviceBindingFeeEntity.yzId = yzId;
                        medicalAdviceBindingFeeEntity.Create(true);
                        medicalAdviceBindingFeeEntity.newid = Guid.NewGuid().ToString();
                        _medicalAdviceBindingFeeRepo.SubmitForm(medicalAdviceBindingFeeEntity, null);

                    }
                    _doctorserviceDmnService.addcqyz(zyh, yzh, zh, ItemFeeVO, this.OrganizeId, this.UserIdentity.UserCode);
                }
            }
            catch (Exception ex)
            {
                return Success(ex.Message.ToString());
            }
            return Success("保存成功！");
        }
        public ActionResult GetbindingfeeData(string zyh, string yzh, string zh)
        {
            var bindinglist = _medicalAdviceBindingFeeRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == OrganizeId && p.yzh == yzh && p.zt == "1").ToList();
            return Content(bindinglist.ToJson());
        }
        public ActionResult GetlsorcqyzData(string zyh, string yzid)
        {
            var data = _doctorserviceDmnService.GetlsorcqyzData(zyh, yzid, this.OrganizeId);
            return Content(data.ToJson());
        }
        public ActionResult DeleteBind(string zyh, string yzid, string yzxz)
        {
            var data = _doctorserviceDmnService.DeleteBind(zyh, yzid, yzxz, this.OrganizeId);
            return Success(data);
        }
        #endregion
    }
}