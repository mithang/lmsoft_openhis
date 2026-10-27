using System;
using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;

using NewtouchHIS.Lib.Framework.Operator;

using Newtouch.CIS.Compat;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("NurseManage")]
    public class OrderAuditController : OrgControllerBase
    {
        private readonly IOrderAuditDmnService _OrderAuditDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IAllergyManageDmnService _allergyManageDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private string IsRehabAuthtoNurse;
        private bool isNurse;
        private bool isRehabDoctor;

        public OrderAuditController(IOrderAuditDmnService OrderAuditDmnService, ISysConfigRepo sysConfigRepo, ISysUserDmnService sysUserDmnService, IAllergyManageDmnService allergyManageDmnService, ISysDepartmentRepo sysDepartmentRepo)
        {
            this._sysConfigRepo = sysConfigRepo;
            this._sysUserDmnService = sysUserDmnService;
            this._allergyManageDmnService = allergyManageDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._OrderAuditDmnService = OrderAuditDmnService;
            //IsRehabAuthtoNurse= _sysConfigRepo.GetValueByCode("IsRehabAuthtoNurse", this.OrganizeId);
            //isNurse = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "Nurse");
            //isRehabDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "RehabDoctor");
        }


        // GET: NurseManage/OrderAudit
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult GetGridJson(Pagination pagination, string patList, string organizeId)
        {
            IsRehabAuthtoNurse = _sysConfigRepo.GetValueByCode("IsRehabAuthtoNurse", this.OrganizeId);
            isNurse = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "Nurse");
            isRehabDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "RehabDoctor");
            IList<OrderAuditVO> list = new List<OrderAuditVO>();
            if (!string.IsNullOrWhiteSpace(IsRehabAuthtoNurse) && IsRehabAuthtoNurse == "0")
            {
                if (isNurse && isRehabDoctor)
                {
                    list = _OrderAuditDmnService.GetOrderAuditYZList(pagination, patList, OrganizeId);
                }
                else if (isRehabDoctor) //Phục hồi chức năngY lệnhchỉKỹ thuật viên phục hồi chức năngPhê duyệt
                {
                    list = _OrderAuditDmnService.GetOrderAuditYZList_KF(pagination, patList, OrganizeId, this.UserIdentity.DepartmentCode);
                }
                else if (isNurse)  //Điều dưỡngkhôngủy quyềnthìkhôngphương phápPhê duyệtPhục hồi chức năngY lệnh
                {
                    list = _OrderAuditDmnService.GetOrderAuditYZList(pagination, patList, OrganizeId, IsRehabAuthtoNurse);
                }
            }
            else
            {
                list = _OrderAuditDmnService.GetOrderAuditYZList(pagination, patList, OrganizeId);
            }
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }


        public IActionResult submitOrderList(IList<OrderAuditVO> orderList)
        {
            //entity.zt = entity.zt == "true" ? "1" : "0";
            OperatorModel user = this.UserIdentity;
            string data = _OrderAuditDmnService.OrderAuditSubmit(user, orderList);

            if (data == "")
            {
                return Success("Duyệtthành công");
            }
            else
            {
                return Success("", data);
            }

        }

        public IActionResult submitOrderListbyPat(string patList, int yzxz, IList<OrderAuditVO> orderList)
        {
            IsRehabAuthtoNurse = _sysConfigRepo.GetValueByCode("IsRehabAuthtoNurse", this.OrganizeId);
            isNurse = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "Nurse");
            isRehabDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "RehabDoctor");
            OperatorModel user = this.UserIdentity;
            if (IsRehabAuthtoNurse != null && IsRehabAuthtoNurse == "0")
            {
                if (isNurse && isRehabDoctor)
                {
                    _OrderAuditDmnService.OrderAuditSubmitbyPat(user, patList, yzxz, orderList);
                }
                else if (isRehabDoctor)
                {
                    _OrderAuditDmnService.OrderAuditSubmitbyPat(user, patList, yzxz, orderList, IsRehabAuthtoNurse, true, this.UserIdentity.DepartmentCode);
                }
                else if (isNurse)
                {
                    _OrderAuditDmnService.OrderAuditSubmitbyPat(user, patList, yzxz, orderList, IsRehabAuthtoNurse);
                }
            }
            else
            {
                var data = _OrderAuditDmnService.OrderAuditSubmitbyPat(user, patList, yzxz, orderList);
                if (data == "")
                {
                    return Success("Duyệtthành công");
                }
                else
                {
                    //data = data.Remove(data.Length - 1, 1);
                    return Success("", data);
                }
            }
            return Success("Duyệtthành công");
        }

        /// <summary>
        /// LấyKhu bệnhBệnh nhânChờ duyệtY lệnhcây
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetPatWardTree(string aa,string keyword)
        {
            IsRehabAuthtoNurse = _sysConfigRepo.GetValueByCode("IsRehabAuthtoNurse", this.OrganizeId);
            var medicalInsurance = _sysConfigRepo.GetValueByCode("medicalInsurance", this.OrganizeId);
            string staffId = this.UserIdentity.StaffId;
            var wardTree = _OrderAuditDmnService.GetWardTree(staffId);
            IList<InpWardPatTreeVO> patTree = new List<InpWardPatTreeVO>();
            if (medicalInsurance != "qinhuangdao")
                patTree = _OrderAuditDmnService.GetPatTree(staffId);
            else
                patTree = _OrderAuditDmnService.GetPatTree(staffId, IsRehabAuthtoNurse);
            if(!string.IsNullOrWhiteSpace(keyword))
                patTree = patTree.Where(p => p.zyh.Contains(keyword)).ToList();
            string[] aasz = new string[200];
            if (!string.IsNullOrWhiteSpace(aa))
            {
                aasz = aa.Split(',');
            }

            var treeList = new List<TreeViewModel>();
            foreach (InpWardPatTreeVO item in wardTree)
            {
                if (IsRehabAuthtoNurse == "0" && isNurse)
                {

                }
                var patInfo = patTree.Where(p => p.bqCode == item.bqCode).ToList();
                var NewPatInfo = patInfo.OrderBy(p => p.BedNo);
                foreach (InpWardPatTreeVO itempat in NewPatInfo)
                {  
                    string gender = itempat.sex == "1" ? "Nam" : "Nữ";
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    //Số giường + Họ tên(Nhập việnSố ngày)+Số nhập viện + Tuổi +Giới tính
                    treepat.text = itempat.BedNo + "-" + itempat.hzxm + "(" + itempat.inHosDays + "ngày)" + "-" + itempat.zyh + "-" + itempat.nl + "tuổi-" + gender;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treeList.Add(treepat);
                    if (((IList)aasz).Contains(itempat.zyh))
                    {
                        treepat.checkstate = 1;
                    }
                }


                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.bqCode;
                tree.text = item.bqmc;
                tree.value = item.bqCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = 0;
                tree.hasChildren = hasChildren;
                tree.Ex1 = "p";
                treeList.Add(tree);
            } 
            return Content(treeList.TreeViewJson(null));
        } 
         
        /// <summary>
        /// nhậpTest dakếtnếu
        /// </summary>
        /// <returns></returns>
        public IActionResult InputresultsSkintest()
        {
            return View();
        }

        //Test datrangmặtcâykiểm soátmục
        public IActionResult SkintestTree(string keyword, string selectkey)
        {
            //IList<SkintestVO> patTree = new List<SkintestVO>();
            //patTree = _OrderAuditDmnService.SkintestVO(this.OrganizeId);
            var wardTree = _OrderAuditDmnService.SkintestVO(this.OrganizeId, keyword, selectkey);
            var wardonly = wardTree.GroupBy(p => new { p.WardCode, p.bqmc }).Select(p => new { p.Key.WardCode, p.Key.bqmc });


            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.WardCode == item.WardCode).Where(p => p.zyh != "").Where(p => p.zyh != null).ToList();

                foreach (SkintestVO itempat in patInfo)
                {
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    treepat.text = itempat.zyh + "-" + itempat.hzxm;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.WardCode;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treeList.Add(treepat);
                }

                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.WardCode;
                tree.text = item.bqmc;
                tree.value = item.WardCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = 0;
                tree.hasChildren = hasChildren;
                tree.Ex1 = "p";
                treeList.Add(tree);
            }
            var a = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));
        }



        public IActionResult Inputinformation(Pagination pagination, string patList, string organizeId, string selectkey)
        {

            IList<SkintestqueryVO> list = new List<SkintestqueryVO>();

            list = _OrderAuditDmnService.Skintestquery(pagination, patList, OrganizeId, selectkey);

            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }


        public IActionResult Inputskintestresults(IList<SkintestqueryVO> orderList)
        {
            //entity.zt = entity.zt == "true" ? "1" : "0";
            OperatorModel user = this.UserIdentity;
            _OrderAuditDmnService.Inputskintestresults(user, orderList);
            return Success("Test daNhậpthành công");
        }

        public IActionResult AuditTips()
        {

            return View();
        }

        public IActionResult Displayinformation(string patList)
        {
            var data = _OrderAuditDmnService.Displayinformation(patList, this.OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult Inputresults()
        {

            return View();
        }

        public IActionResult Enteragain(string zyh, string yzid, string lrjg, string yzlb)
        {
            OperatorModel user = this.UserIdentity;
            var data = _OrderAuditDmnService.Enteragain(user, zyh, yzid, lrjg, yzlb);
            return Success(data);

        }
        public IActionResult EnteragainMuti(string yzids, string lrjg)
        {
            OperatorModel user = this.UserIdentity;
            string msg = _OrderAuditDmnService.Enteragain(user, yzids, lrjg);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                return Error(msg);
            }
            else
            {
                return Success("Lưu thành công");
            }

        }
        public IActionResult Drug_Inquiry()
        {
            return View();
        }
        public IActionResult Ward_Application()
        {
            return View();
        }
        /// <summary>
        /// Nhập việnY lệnhphát thuốcTra cứu
        /// </summary>
        /// <param name="xm">bệnh ngườiHọ tên</param>
        /// <param name="bqCode">Mã khu bệnh</param>
        /// <param name="ypmc">Tên thuốc</param>
        /// <param name="cw">giường ký tự</param>
        /// <param name="kssj">Thời gian bắt đầu</param>
        /// <param name="jssj">Thời gian kết thúc</param>
        /// <returns></returns>
        public IActionResult GetOrdersDrugsGridJson(Pagination pagination, string xm, string bqCode, string ypmc, string cw, string zyh, DateTime kssj, DateTime jssj)
        {
            var tt = _OrderAuditDmnService.GetOrdersDrugsGridJson(pagination, xm, bqCode, ypmc, cw, zyh, kssj, jssj, OrganizeId);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        public IActionResult SkinTestInfo()
        {

            return View();
        }
        public IActionResult GetSkinTestInfoGridJson(Pagination pagination, string zyh)
        {
            var tt = _OrderAuditDmnService.GetSkinTestInfoGridJson(pagination, zyh, OrganizeId);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        #region Đề nghị chuẩn bị thuốc của khoaQuản lý
        /// <summary>
        /// Lấycầnđăng kýlĩnhcủaKhu bệnh
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetTheLowerKsCodeList(string keyword)
        {
            var data = _OrderAuditDmnService.GetTheLowerKsCodeList(OrganizeId, keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// LấyKhoa
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSysDepartmentList()
        {
            var data = _sysDepartmentRepo.GetList(OperatorProvider.GetCurrent().OrganizeId, "1");
            return Json(data);
        }
        /// <summary>
        /// LưuKhoachuẩn bị thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SubmitKsby(BYDjInfoDTO Djnr)
        {
            OperatorModel user = this.UserIdentity;
            var data = _OrderAuditDmnService.PrepareMedicine(user, OrganizeId, Djnr);
            return Success(data);
        }
        public IActionResult DrugAndStockSearch(string keyword, string yfbm)
        {
            var result = _OrderAuditDmnService.GetDrugAndStock(yfbm, keyword, OrganizeId);
            return Content(result.ToJson());
        }
        public IActionResult GetTheLowerYfbmCodeList(string keyword)
        {
            var data = _OrderAuditDmnService.GetTheLowerYfbmCodeList(keyword, OrganizeId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// phiếusố phiếutạo
        /// </summary>
        /// <param name="djmc"></param>
        /// <returns></returns>
        public IActionResult InitDjh(string djmc, string yfbmCode)
        {
            return Success(null, EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo(djmc, yfbmCode, OrganizeId));
        }
        /// <summary>
        /// chuẩn bị thuốcphiếucứGửi
        /// </summary>
        /// <param name="djmc"></param>
        /// <returns></returns>
        public IActionResult Bydjtj(string ById)
        {
            OperatorModel user = this.UserIdentity;
            var data = _OrderAuditDmnService.PrepareMedicineSubmit(ById, OrganizeId, user);
            return Success(data);
        }
        public IActionResult Bydjupdate(string ById)
        {
            OperatorModel user = this.UserIdentity;
            var data = _OrderAuditDmnService.PrepareMedicineSubmit(ById, OrganizeId, user);
            return Success(data);
        }


        /// <summary>
        /// Khoachuẩn bị thuốcĐăng ký
        /// </summary>
        /// <returns></returns>
        public IActionResult PrepareMedicineApply()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            return View();
        }
        /// <summary>
        /// Khoachuẩn bị thuốcTồn khoQuản lý
        /// </summary>
        /// <returns></returns>
        public IActionResult PreparationStock()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            return View();
        }
        /// <summary>
        /// Khoachuẩn bị thuốcdanh sách
        /// </summary>
        /// <param name="Djnr"></param>
        /// <returns></returns>
        public IActionResult PreparationStockGridJson(Pagination pagination, string ypmc)
        {
            var data = new
            {
                rows = _OrderAuditDmnService.PreparationStockGridJson(this.OrganizeId, pagination: pagination, ypmc: ypmc),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// Khoachuẩn bị thuốcdanh sách
        /// </summary>
        /// <param name="Djnr"></param>
        /// <returns></returns>
        public IActionResult PrepareMedicineApplyGridJson(Pagination pagination, string ksbyzt, DateTime? kssj = null, DateTime? jssj = null)
        {
            var data = new
            {
                rows = _OrderAuditDmnService.PrepareMedicineApplyGridJson(this.OrganizeId, pagination: pagination, ksbyzt: ksbyzt, kssj: kssj, jssj: jssj),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// phiếucứchi tiết
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>
        public IActionResult QueryPrepareMedicine(string djId)
        {
            var result = _OrderAuditDmnService.QueryPrepareMedicine(djId, this.OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// LấyThuốcTồn kho
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>
        public IActionResult BydjQueryKykc(string ypbm, string pc, string ph, string yfbm)
        {
            var result = new
            {
                kysl = _OrderAuditDmnService.BydjQueryKykc(ypbm, pc, ph, yfbm, this.OrganizeId)
            };
            return Content(result.ToJson());
        }

        /// <summary>
        /// chínhphiếucứnội dung
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>
        public IActionResult QueryPrepareMedicinebyId(string djId)
        {
            var result = _OrderAuditDmnService.QueryPrepareMedicinebyId(djId, this.OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// chuẩn bị thuốcphiếucứrútvề
        /// </summary>
        /// <param name="djmc"></param>
        /// <returns></returns>
        public IActionResult Bydjback(string Djh)
        {
            OperatorModel user = this.UserIdentity;
            var data = _OrderAuditDmnService.PrepareMedicineback(Djh, OrganizeId, user);
            return Success(data);
        }
        /// <summary>
        /// chuẩn bị thuốcphiếucứrútvề
        /// </summary>
        /// <param name="djmc"></param>
        /// <returns></returns>
        public IActionResult Bydjdelete(string ById)
        {
            OperatorModel user = this.UserIdentity;
            var data = _OrderAuditDmnService.PrepareMedicinedelete(ById, OrganizeId, user);
            return Success(data);
        }
        #endregion

    }
}