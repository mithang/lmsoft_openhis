using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class PrepareMedicineController : OrgControllerBase
    {

        private readonly IPrepareMedicineApp _prepareMedicineApp;
        private readonly ISysUserDmnService sysuserDmnService;
        private readonly IPrepareMedicineDmnService _prepareMedicineDmnService;

        // GET: NurseManage/PrepareMedicine
        public ActionResult Index()
        {
            return View();
        }

        #region 科室备药损益
        public ActionResult ReportLossAndProfit()
        {
            return View();
        }

        /// <summary>
        /// Lấy số phiếu lãi lỗ mới
        /// </summary>
        /// <returns></returns>
        public ActionResult initialSYDH(string djmc)
        {
            var sydh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo(djmc, Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
            return Content(sydh);
        }

        /// <summary>
        /// Tải lý do lãi lỗ theo loại
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        public ActionResult GetLossProfitReasonListByType(string sylx)
        {
            var list = _prepareMedicineApp.GetLossProfitReasonListByType(sylx);
            var obj = list.Select(a => new { syyyId = a.syyyId, syyy = a.syyy }).ToJson();
            return Content(obj);
        }

        /// <summary>
        /// Lấy người chịu trách nhiệmlist
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public ActionResult GetZRRList(string inputCode)
        {
            var list = sysuserDmnService.GetStaffListByOrg(OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Tra cứu thuốc lãi lỗlist
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public ActionResult SelectLossAndProfitMedicineList(string inputCode)
        {
            var list = _prepareMedicineApp.SelectLossAndProfitMedicineList(inputCode);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Gửi báo hao hụt, báo thừa
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        public ActionResult SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
        {
            if (syxx == null || syxx.Length <= 0) return Error("请传入损益明细");
            var organizeId = OrganizeId;
            var yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var userCode = OperatorProvider.GetCurrent().UserCode;
            var syIds = "";
            Parallel.ForEach(syxx, item =>
            {
                item.syId = Guid.NewGuid().ToString();
                item.OrganizeId = organizeId;
                item.yfbmCode = yfbmCode;
                item.CreatorCode = userCode;
                item.CreateTime = DateTime.Now;
                item.zt = "1";
                item.Bgsj = DateTime.Now;
                syIds += item.syId + ",";
            });
            var result = _prepareMedicineApp.SubmitReportLossAndProfit(syxx);
            return string.IsNullOrWhiteSpace(result) ? Success(syIds) : Error(result);
        }

        ///// <summary>
        ///// Báo hao hụt, báo thừa (Lưu)
        ///// </summary>
        ///// <param name="syxx"></param>
        ///// <returns></returns>
        //public ActionResult SaveReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
        //{
        //    var syxxList = syxx != null && syxx.Length > 0 ? syxx.ToList() : new List<SysMedicineProfitLossEntity>();
        //    var profitLossEntityList = new List<YpSyxxVo>();
        //    if (syxxList.Count > 0)
        //    {
        //        syxxList.ForEach(p =>
        //        {
        //            profitLossEntityList.Add(new YpSyxxVo
        //            {
        //                Bgsj = DateTime.Now,
        //                Djh = p.Djh,
        //                Jj = p.Jj,
        //                Lsj = p.Lsj,
        //                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
        //                pc = p.pc,
        //                Pfj = p.Pfj,
        //                Ph = p.Ph,
        //                remark = p.remark,
        //                Sykc = p.Sykc,
        //                Sysl = p.Sysl,
        //                Syyy = p.Syyy,
        //                yfbmCode = Constants.CurrentYfbm.yfbmCode,
        //                Yklsj = p.Yklsj,
        //                Ykpfj = p.Ykpfj,
        //                Ypdm = p.Ypdm,
        //                Yxq = p.Yxq,
        //                Zhyz = p.Zhyz,
        //                Zrr = p.Zrr
        //            });
        //        });
        //    }
        //    else
        //    {
        //        throw new FailedException("Thông tin lãi lỗ không được trống");
        //    }
        //    var result = _medicineDmnService.SaveReportLossAndProfit(profitLossEntityList);
        //    return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        //}

        #endregion

        #region 科室备药损益查询

        /// <summary>
        /// Trả lại thuốc dự trữ khoa
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportLossAndProfitQuery()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            return View();
        }


        /// <summary>
        /// Tra cứu báo hao hụt, báo thừa
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string syyy, string inputCode, int syqk)
        {
            var list = new
            {
                rows = _prepareMedicineApp.SelectLossAndProditInfoList(pagination, startTime, endTime, inputCode, syyy, syqk),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }


        /// <summary>
        /// Tra cứu tổng giá bán sỉ, tổng giá bán lẻ
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">Lý do lãi lỗ</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">Tình hình lãi lỗ</param>
        /// <returns></returns>
        public ActionResult ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk)
        {
            return Content(_prepareMedicineApp.ComputePjzeAndLjze(startTime, endTime, syyy, inputCode, syqk).ToJson());
        }
        #endregion

        #region 科室备药退回

        /// <summary>
        /// Trả lại thuốc dự trữ khoa
        /// </summary>
        /// <returns></returns>
        public ActionResult PrepareMedicineReturn()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            return View();
        }

        /// <summary>
        /// Tạo phiếu yêu cầu trả lại mới
        /// </summary>
        /// <returns></returns>
        public ActionResult PrepareMedicineReturnAdd() {
            return View();
        }

        /// <summary>
        /// Lưu trả lại thuốc dự trữ khoa
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult SubmitKsbyth(BythDjInfoDTO Djnr)
        {
            OperatorModel user = this.UserIdentity;
            var data = _prepareMedicineDmnService.PrepareMedicineReturnSubmit(user, this.OrganizeId, Djnr);
            return Success(data);
        }

        /// <summary>
        /// Phiếu yêu cầu thuốc dự trữ khoa: liên kết thông tin thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbm"></param>
        /// <returns></returns>
        public ActionResult DrugAndStockSearch(string keyword, string yfbm)
        {
            var result = _prepareMedicineDmnService.GetDrugAndStock(yfbm, keyword, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// Tra cứu điền thuốc dự trữ khoa
        /// </summary>
        /// <param name="ypcodestr"></param>
        /// <param name="yfbmstr"></param>
        /// <returns></returns>
        public ActionResult ApplyDrugsSearch(string ypcodestr, string yfbmstr)
        {
            var result = _prepareMedicineDmnService.GetApplyDrugAndStock(ypcodestr, yfbmstr, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// Danh sách tra cứu trả lại thuốc dự trữ khoa
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="xm"></param>
        /// <param name="bqCode"></param>
        /// <param name="ypmc"></param>
        /// <param name="cw"></param>
        /// <param name="zyh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult GetPrepareMedicineReturnGridJson(Pagination pagination, string thzt,  DateTime kssj, DateTime jssj)
        {
            var tt = _prepareMedicineDmnService.GetPrepareMedicineReturnGridJson(pagination, thzt, kssj, jssj, OrganizeId);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        /// <summary>
        /// Chi tiết chứng từ
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>
        public ActionResult QueryPrepareMedicine(string djId)
        {
            var result = _prepareMedicineDmnService.QueryPrepareMedicine(djId, this.OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Gửi chứng từ thuốc dự trữ
        /// </summary>
        /// <param name="djmc"></param>
        /// <returns></returns>
        public ActionResult UpdatePrepareMedicineReturn(PrapareMedicineReturnEntity entity)
        {
            OperatorModel user = this.UserIdentity;
            entity.OrganizeId = this.OrganizeId;
            entity.LastModifierCode = user.UserId;
            entity.LastModifyTime= DateTime.Now.Date;
            entity.tjsj = DateTime.Now.Date;
            var data = _prepareMedicineDmnService.UpdatePrepareMedicineReturn( entity);
            return Success(data);
        }
        /// <summary>
        /// TheoIDTra cứu danh sách chứng từ
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>

        public ActionResult QueryPrepareMedicineReturnbyId(string djId)
        {
            var result = _prepareMedicineDmnService.QueryPrepareMedicineReturnbyId(djId, this.OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// TheoIDTra cứu chi tiết chứng từ
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>
        public ActionResult QueryPrepareMedicineReturnMXbyId(string djId)
        {
            var result = _prepareMedicineDmnService.QueryPrepareMedicineReturnMXbyId(djId, this.OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// Lấy số lượng tồn kho thuốc
        /// </summary>
        /// <param name="ypbm"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="yfbm"></param>
        /// <returns></returns>
        public ActionResult BydjQueryKykc(string ypbm, string pc, string ph, string yfbm)
        {
            var result = new
            {
                kysl = _prepareMedicineDmnService.BydjQueryKykc(ypbm, pc, ph, yfbm, this.OrganizeId)
            };
            return Content(result.ToJson());
        }

        /// <summary>
        /// Thu hồi
        /// </summary>
        /// <param name="Djh"></param>
        /// <returns></returns>
        public ActionResult Bydjback(string Djh)
        {
            OperatorModel user = this.UserIdentity;
            var data = _prepareMedicineDmnService.PrepareMedicineReturnback(Djh, OrganizeId, user);
            return Success(data);
        }
        /// <summary>
        /// Hủy bỏ
        /// </summary>
        /// <param name="ById"></param>
        /// <returns></returns>
        public ActionResult Bydjdelete(string ById)
        {
            OperatorModel user = this.UserIdentity;
            var data = _prepareMedicineDmnService.PrepareMedicineReturndelete(ById, OrganizeId, user);
            return Success(data);
        }
        #endregion

        #region 药品使用查询
        public ActionResult MedicineUseQuery()
        {
            //ViewBag.OrgId = this.OrganizeId;
            //ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            return View();
        }

        //Thông tin thuốc
        public ActionResult GetypGridJson(Pagination pagination, MedicineInfoParam model, string keyword, string organizeId)
        {
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            model.ypzt = model.ypzt ?? "";
            model.syzt = model.syzt ?? "";
            var data = new
            {
                rows = _prepareMedicineDmnService.GetMedicineInfoListV2(pagination, model, this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        #endregion
    }
}