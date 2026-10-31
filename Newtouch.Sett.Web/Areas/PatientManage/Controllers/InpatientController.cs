using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.PatientManage.Controllers
{

    /// <summary>
    /// Bệnh nhân nhập viện
    /// </summary>
    [Area("PatientManage")]

    public class InpatientController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ICache _cache;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysWardRepo _sysWardRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IBookkeepInHosDmnService _bookkeepInHosDmnService;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;
        private readonly IInpatientApp _inpatientApp;
        private readonly ISysPatientBasicInfoRepo _sysPatientBasicInfoRepo;
        public InpatientController(ICache cache, ISysOrganizeDmnService sysOrganizeDmnService, ISysWardRepo sysWardRepo, IHospPatientBasicInfoRepo hospPatientBasicInfoRepo, IBookkeepInHosDmnService bookkeepInHosDmnService, IPatientBasicInfoDmnService patientBasicInfoDmnService, IDischargeSettleDmnService dischargeSettleDmnService, IInpatientApp inpatientApp, ISysPatientBasicInfoRepo sysPatientBasicInfoRepo)
        {
            _cache = cache;
            _sysOrganizeDmnService = sysOrganizeDmnService;
            _sysWardRepo = sysWardRepo;
            _hospPatientBasicInfoRepo = hospPatientBasicInfoRepo;
            _bookkeepInHosDmnService = bookkeepInHosDmnService;
            _patientBasicInfoDmnService = patientBasicInfoDmnService;
            _dischargeSettleDmnService = dischargeSettleDmnService;
            _inpatientApp = inpatientApp;
            _sysPatientBasicInfoRepo = sysPatientBasicInfoRepo;
        }


        /// <summary>
        /// Lấy bệnh nhân nhập viện Cấp ba(Cơ sở+Khoa bệnh+bệnh nhân)
        /// </summary>
        /// <returns></returns>

        public IActionResult GetInpatientSelectorTreeData(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true)
        {
            //if (string.IsNullOrWhiteSpace(organizeId))
            //{
            //    organizeId = base.GetAuthOrganizeId();  //mặc định QuyềnOrganizeId
            //}
            ////
            //var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
            var treeList = new List<TreeViewModel>();
            //foreach (var orgItem in organizedata)
            //{
            //    //Tập hợp khu bệnh của cơ quan
            //    var orgBqData = _sysWardRepo.GetbqList(orgItem.Id);
            //    //máycơ quanbệnhngườitậphợp
            //    var orgPatientData =_bookkeepInHosDmnService.GetPendingExecutionPatientVOList(orgItem.Id);
            //    #region máycơ quandướinơicóbệnhquận
            //    foreach (var bqItem in orgBqData)
            //    {
            //        var bqPatientData = orgPatientData.Where(p => p.bqCode == bqItem.bqCode).ToList();
            //        #region máycơ quandướinơicóbệnhngười
            //        foreach (var patientItem in bqPatientData)
            //        {
            //            TreeViewModel subsubTree = new TreeViewModel();
            //            subsubTree.id = "br_" + patientItem.zyh;
            //            subsubTree.text = patientItem.xm + "(zyh:" + patientItem.zyh + ")";
            //            if (patientItem.ybsycs.HasValue && patientItem.ybsycs.Value > 0)
            //            {
            //                subsubTree.text += "(BHYT còn lại:" + patientItem.ybsycs + "lần)";
            //            }
            //            subsubTree.value = "br_" + patientItem.zyh;
            //            subsubTree.parentId = "bq_" + bqItem.bqCode;
            //            subsubTree.hasChildren = false;
            //            subsubTree.showcheck = true;
            //            subsubTree.checkstate = 0;   //Gán giá trị ở ngoài,Thuận tiện cho bộ nhớ đệm

            //            subsubTree.Ex1 = "1";    //tiêuhiển thịlàbệnhngười,dùngtạiquậnphân bệnh nhân,bệnhquậncủalạichọnkhung

            //            treeList.Add(subsubTree);
            //        }
            //        #endregion
            //        TreeViewModel subTree = new TreeViewModel();
            //        subTree.id = "bq_" + bqItem.bqCode;
            //        subTree.text = bqItem.bqmc;
            //        subTree.value = "bq_" + bqItem.bqCode;
            //        subTree.parentId = orgItem.Id;
            //        subTree.isexpand = isExpand;  //Mặc định không mở rộng,Quá nhiều người
            //        subTree.complete = true;
            //        subTree.showcheck = true;
            //        subTree.hasChildren = bqPatientData.Count > 0;
            //        treeList.Add(subTree);
            //    }
            //    #endregion
            //    TreeViewModel tree = new TreeViewModel();
            //    tree.id = orgItem.Id;
            //    tree.text = orgItem.Name;
            //    tree.value = orgItem.Code;
            //    tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //Đặc biệt
            //    tree.isexpand = true;
            //    tree.complete = true;
            //    tree.showcheck = false;
            //    tree.hasChildren = orgBqData.Count > 0 || orgPatientData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
            //    treeList.Add(tree);
            //}
            ////

            ////if (from == "selecttherapist")   //Chọn trị liệu sư của hệ thống
            ////{
            ////    var whereList = _sysUserDmnService.GetStaffIdListByParentOrg(organizeId, "RehabDoctor");   //Kỹ thuật viên trị liệu
            ////    treeList = treeList.Where(p => !p.showcheck || whereList.Contains(p.id)).ToList();  //chỉhiển thịhiển thịđiều trịđiều trịsư
            ////}

            //IList<string> checkedStaffIdList = null;
            ////if (from == "selecttherapist")   //Chọn trị liệu sư của hệ thống
            ////{

            ////}

            //if (checkedStaffIdList != null && checkedStaffIdList.Count > 0)
            //{
            //    foreach (var treeItem in treeList)
            //    {
            //        if (treeItem.showcheck && checkedStaffIdList.Contains(treeItem.id))
            //        {
            //            treeItem.checkstate = 1;
            //            //Đồng thời để cấp trên mở rộng
            //            var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
            //            if (thisParentNode != null)
            //            {
            //                thisParentNode.isexpand = true;  //Cấp dưới có dòng được chọn,Ở đây nhất định mở rộng
            //            }
            //        }
            //    }
            //}
            //if (!isShowEmpty)   //khônghiển thịhiển thịnhiềucòn lạicủa
            //{
            //    var count = 1;  //di chuyểntrừcủathực hiệnsố
            //    while (count > 0)
            //    {
            //        count = treeList.RemoveAll(p => !p.showcheck && !p.hasChildren);
            //        //nặngmớiđiềuchỉnhdướicây gỡ khôngcódướicấp(dướicấpvừabịdi chuyểnrồi) lại khônghiển thịhiển thịshowcheckcủa
            //        treeList.RemoveAll(p => !p.showcheck && !treeList.Any(sub => sub.parentId == p.id));
            //    }
            //}
            return Content(treeList.TreeViewJson(null));
        }

        public IActionResult GetAccountingpatientInfo(string organizeId = null, string from = null)
        {
            List<PendingExecutionPatientVO> data = new List<PendingExecutionPatientVO> { };
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //mặc định QuyềnOrganizeId
            }
            //
            var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
            foreach (var orgItem in organizedata)
            {
                //Tập hợp khu bệnh của cơ quan
                var orgBqData = _sysWardRepo.GetbqList(orgItem.Id);
                IList<PendingExecutionPatientVO> orgPatientData = new List<PendingExecutionPatientVO>();
                if (from == "mz")
                {
                    orgPatientData = _bookkeepInHosDmnService.GetOutPendingExecutionPatientVOList(orgItem.Id, UserIdentity.DepartmentCode);
                }
                else
                {
                    orgPatientData = _bookkeepInHosDmnService.GetPendingExecutionPatientVOList(orgItem.Id, UserIdentity.DepartmentCode);
                }
                data.AddRange(orgPatientData);
            }
            return Content(data.ToJson());
        }
        /// <summary>
        /// lớp nổi,Nguồn dữ liệu Lấy bệnh nhân nhập viện
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>

        public IActionResult GetInpatientListJson(string keyword = null, string zybz = "1")
        {
            var list = _hospPatientBasicInfoRepo.GetInpatientList(this.OrganizeId, keyword, zybz);
            return Content(list == null ? "[]" : list.ToJson());
        }

        /// <summary>
        /// Tra cứu bệnh nhân nhập viện View
        /// </summary>
        /// <returns></returns>
        public IActionResult InPatientQueryIndex()
        {
            return View();
        }
        /// <summary>
        /// kếttínhxóaphiếutrênchuyểnchỉnhhợptrangmặt
        /// </summary>
        /// <returns></returns>
        public IActionResult SettlementListUpload()
        {
            return View();
        }
        //kếttínhxóaphiếuchấtđiều khiểntratra cứu
        public IActionResult SettlementListResultFrom()
        {
            ViewBag.OrgId = this.OrganizeId;
            return View();
        }
        //kếttínhxóaphiếusốlượngthốngtínhkếtkết quảtratra cứu
        public IActionResult QuerySettlementResultFrom()
        {
            ViewBag.OrgId = this.OrganizeId;
            return View();
        }


        public IActionResult PatientUploadYbIndex()
        {
            return View();
        }
        /// <summary>
        /// Tra cứu bệnh nhân nhập viện dữ liệu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zybz"></param>
        /// <param name="keyword"></param>
        /// <param name="ryrqkssj"></param>
        /// <param name="ryrqjssj"></param>
        /// <returns></returns>
        public IActionResult GridInPatientGridJson(Pagination pagination
            , string zybz = null
            , string keyword = null, DateTime? ryrqkssj = null, DateTime? ryrqjssj = null,string ksmc=null,string bqmc=null)
        {
            string flag = null;
            string msg = null;
            var data = new
            {
                rows = _patientBasicInfoDmnService.GetInPatientList(this.OrganizeId
                , ref flag, ref msg, zybz: zybz, keyword: keyword, pagination: pagination, ryrqkssj: ryrqkssj, ryrqjssj: ryrqjssj, ksmc:ksmc, bqmc:bqmc),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Chuyển tự phí sang BHYT khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult ZFToYBForm()
        {
            return View();
        }

        /// <summary>
        /// Chuyển BHYT sang tự chi trả khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult YBToZFForm()
        {
            return View();
        }
        public IActionResult dkfsxz()
        {
            return View();
        }
        public IActionResult XNHToZFForm()
        {
            return View();
        }

        #region ZFToYB steps
        /// <summary>
        /// làkhôngđầulầny tếbảo hiểmthìkhám
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult CheckFistYbVisit(string mzzyh, string sbkh)
        {
            var orgId = this.OrganizeId;
            //int patid =0;
            var code = "0";
            //var xtbrxxList = _sysPatientBasicInfoRepo.IQueryable().Where(p => p.sbbh == sbkh && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
            //if (xtbrxxList != null)
            //{
            //    code = "1";
            //}
            return Success(null, code);
        }
        public IActionResult ZFToYB_Step_1(string zyh)
        {
            System.Threading.Thread.Sleep(1000 * 3);
            var data = _inpatientApp.ZFToYB_Step_1(zyh);
            return Success(null, data);
        }

        public IActionResult ZFToYB_Step_2(string zyh)
        {
            var data = _inpatientApp.ZFToYB_Step_2(zyh);
            return Success(null, data);
        }

        public IActionResult ZFToYB_Step_4(string zyh, string sbbh, string xm)
        {
            var data = _inpatientApp.ZFToYB_Step_4(zyh, sbbh, xm);
            return Success(null, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="patid"></param>
        /// <param name="cardInfo"></param>
        /// <param name="ryblInfo">Thông tin thẻ BHYT</param>
        /// <returns></returns>
        public IActionResult ZFToYB_Step_6(string zyh, int patid, GACardReadInfoDTO cardInfo, GuianRybl21OutInfoEntity ryblInfo)
        {
            var data = _inpatientApp.ZFToYB_Step_6(zyh, patid, cardInfo, ryblInfo);
            return Success(null, data);
        }
		/// <summary>
		/// nặngmừngtựphíchuyểny tếbảo hiểm--Nội trú
		/// </summary>
		/// <param name="zyh"></param>
		/// <param name="patid"></param>
		/// <param name="cardInfo"></param>
		/// <param name="ryblInfo"></param>
		/// <returns></returns>
	    public IActionResult CQZFToYB_Step_6(string zyh, int patid, ZYToYBDto patInfo, CqybMedicalReg02Entity ryblInfo)
	    {
		    var data = _inpatientApp.CQZFToYB_Step_6(zyh, patid, patInfo, ryblInfo);
		    return Success(null, data);
	    }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public IActionResult ZFToYB_Step_8(string zyh)
        {
            var data = _inpatientApp.ZFToYB_Step_8(zyh);
            return Success(null, data);
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public IActionResult UpqdScData(string kssj, string jssj)
        {
            var data = _inpatientApp.UpqdScData(kssj, jssj);
            return Success(null, data);
        }

        #endregion

        #region YBToZF steps

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult YBToZF_Step_1(string zyh)
        {
            var data = _inpatientApp.YBToZF_Step_1(zyh);
            return Success(null, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult YBToZF_Step_3(string zyh, string sbbh, string xm)
        {
            var data = _inpatientApp.YBToZF_Step_3(zyh, sbbh, xm);
            return Success(null, data);
        }
		/// <summary>
		/// nặngmừngy tếbảo hiểmđượclấybệnhngườithìkhámđăngghirơiđịasốdữ liệu
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
	    public IActionResult CqYBToZF_Step_3(string zyh)
	    {
		    var data = _inpatientApp.CqYBToZF_Step_3(zyh);
		    return Success(null, data);
	    }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public IActionResult YBToZF_Step_4(string zyh)
        {
            var data = _inpatientApp.YBToZF_Step_4(zyh);
            return Success(null, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult YBToZF_Step_5(string zyh, int patid)
        {
            var data = _inpatientApp.YBToZF_Step_5(zyh, patid);
            return Success(null, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult YBToZF_Step_7(string zyh)
        {
            var data = _inpatientApp.YBToZF_Step_7(zyh);
            return Success(null, data);
        }

        #endregion

        #region XNHToZF steps
        public IActionResult GuiAnXnhYbToZf(string zyh)
        {
            try
            {
                bool isSuccess = true;
                string message = string.Empty;
                string state = "1";
                if (string.IsNullOrEmpty(zyh))
                {
                    isSuccess = false;
                    state = "0";
                    message = "Mã nhập viện không được để trống，Vui lòng thử lại！";
                }

                InpatientSettXnhPatInfoVO patinfo = null;
                if (isSuccess)
                {
                    patinfo = _dischargeSettleDmnService.GetInpatientSettXnhPatInfo(zyh, this.OrganizeId);
                    if (patinfo == null || string.IsNullOrEmpty(patinfo.inpId))
                    {
                        isSuccess = false;
                        state = "0";
                        message = "Lấy Bệnh nhân nông thôn mới Nhập viện bồi thường Số thứ tựinp Id là trống，Vui lòng liên hệhis xử lý";
                    }
                    if (patinfo.zybz== ((int)EnumZYBZ.Wry).ToString()|| patinfo.zybz == ((int)EnumZYBZ.Ycy).ToString())
                    {
                        isSuccess = false;
                        state = "0";
                        message = "này Bệnh nhân đã xuất viện Hoặc Đã hủy bỏ！";
                    }

                    if (isSuccess)
                    {
                        S05RequestDTO S05ReqDTO = new S05RequestDTO()
                        {
                            inpId = patinfo.inpId,
                            areaCode = "",
                            isTransProvincial = "0",
                            cancelCause = ""
                        };
                        Response<string> S05ResDto = HospitalizationProxy.GetInstance(OrganizeId).S05(S05ReqDTO);
                        if (!S05ResDto.state)
                        {
                            isSuccess = false;
                            state = "0";
                            message = "Rút lại đăng ký nhập viện thất bại，Bảo hiểm nông thôn Thông tin lỗi trả về là：【" + S05ResDto.message + "】";
                        }
                        else
                        {
                            //1.hisChuyển BHYT sang tự chi trả   2.đồng bộcpoe
                            _inpatientApp.YBToZF_Step_5(zyh, patinfo.patid);
                            YBToZF_Step_7(zyh);
                        }
                    }
                }
                var data = new
                {
                    state = state,
                    message = message
                };
                return Content(data.ToJson());
            }
            catch (Exception e)
            {
                var data = new
                {
                    state = "0",
                    message = "Hủy xuất viện Quyết toán thất bại：【" + e.Message + "】"
                };
                return Content(data.ToJson());
            }
        }

        #endregion

        #region ZFToXNH steps
        public IActionResult GuiAnZFToXNH(string zyh, HosPatZFToXNHVO patInfo)
        {
            try
            {
                bool isSuccess = true;
                string message = string.Empty;
                string state = "1";
                if (string.IsNullOrEmpty(zyh))
                {
                    isSuccess = false;
                    state = "0";
                    message = "Mã nhập viện không được để trống，Vui lòng thử lại！";
                }

                string msg1 = "";
                bool step04 = _inpatientApp.ZFToXNH_Step_4(zyh, patInfo.xnhgrbm, patInfo.xm, out msg1);
                if (!step04)
                {
                    isSuccess = false;
                    state = "0";
                    message = msg1;
                }
                if (isSuccess)
                {

                    S04RequestDTO S04ReqDTO = _inpatientApp.GetZfToXnhPatInfo(zyh);
                    if (S04ReqDTO==null || string.IsNullOrEmpty(S04ReqDTO.memberId) || string.IsNullOrEmpty(S04ReqDTO.inpatientNo) || string.IsNullOrEmpty(S04ReqDTO.admissionDate) || string.IsNullOrEmpty(S04ReqDTO.admissionDepartments))
                    {
                        isSuccess = false;
                        state = "0";
                        message = "Làm thủ tục nhập viện Thiếu phải của Giá trị！";
                    }
                    if (isSuccess)
                    {

                        Response<S04ResponseDTO> S04ResDto = HospitalizationProxy.GetInstance(OrganizeId).S04(S04ReqDTO);
                        if (!S04ResDto.state)
                        {
                            isSuccess = false;
                            state = "0";
                            message = "Làm thủ tục nhập viện Thất bại，Bảo hiểm nông thôn Thông tin lỗi trả về là：【" + S04ResDto.message + "】";
                        }
                        else
                        {
                            GuianXnhS04InfoEntity S04InfoEntity = new GuianXnhS04InfoEntity()
                            {
                                Id = Guid.NewGuid().ToString(),
                                inpId = S04ResDto.data.inpId,
                                zyh = zyh,
                                xnhgrbm = patInfo.xnhgrbm,
                                xnhylzh = patInfo.xnhylzh
                            };
                            string msg2 = "";
                            if (_patientBasicInfoDmnService.InpatientZFchangetoXNH(this.OrganizeId, zyh, patInfo, S04InfoEntity, out msg2))
                            {
                                _inpatientApp.ZFToXNH_Step_8(zyh);
                            }
                            else
                            {
                                S05RequestDTO S05ReqDTO = new S05RequestDTO()
                                {
                                    inpId = S04ResDto.data.inpId,
                                    areaCode = "",
                                    isTransProvincial = "0",
                                    cancelCause = ""
                                };
                                Response<string> S05ResDto = HospitalizationProxy.GetInstance(OrganizeId).S05(S05ReqDTO);
                                if (!S05ResDto.state)
                                {
                                    isSuccess = false;
                                    state = "0";
                                    message = "chuyển tự chi trả Hợp tác nông thôn mới His Cục bộ Thất bại【"+ msg2 + "】，trong đó hợp tác nông thôn mới Từ xa Đã Thành công，Rút lại đăng ký nhập viện thất bại，Bảo hiểm nông thôn Thông tin lỗi trả về là：【" + S05ResDto.message + "】";
                                }
                                else
                                {
                                    isSuccess = false;
                                    state = "0";
                                    message = "chuyển tự chi trả Hợp tác nông thôn mới Thất bại【" + msg2 + "】";
                                }
                            }

                            ;
                        }
                    }
                }

               
                var data = new
                {
                    state = state,
                    message = message
                };
                return Content(data.ToJson());
            }
            catch (Exception e)
            {
                var data = new
                {
                    state = "0",
                    message = "chuyển tự chi trả hợp tác nông thôn mới Thất bại：【" + e.Message + "】"
                };
                return Content(data.ToJson());
            }
        }

        #endregion

        /// <summary>
        /// kếttínhxóaphiếutrênchuyểntrongdungtratra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sczt"></param>
        /// <param name="tjzt"></param>
        /// <param name="keyword"></param>
        /// <param name="cykssj"></param>
        /// <param name="cyjssj"></param>
        /// <returns></returns>
        public IActionResult GridSettlementGridJson(Pagination pagination, string sczt, string tjzt, string keyword = null, DateTime? cykssj = null, DateTime? cyjssj = null)
        {
            var data = new
            {
                rows = _patientBasicInfoDmnService.GetSettlementList(this.OrganizeId, sczt: sczt, tjzt: tjzt, keyword: keyword, pagination: pagination, cykssj: cykssj, cyjssj: cyjssj),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

    }
}
