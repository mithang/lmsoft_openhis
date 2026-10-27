using Newtouch.Domain.IDomainServices;

using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Application.Interface;
using NewtouchHIS.Lib.Framework.Operator;

using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    [Area("NurseManage")]
    public class PrepareMedicineController : OrgControllerBase
    {

        private readonly IPrepareMedicineApp _prepareMedicineApp;
        private readonly ISysUserDmnService sysuserDmnService;
        private readonly IPrepareMedicineDmnService _prepareMedicineDmnService;

        public PrepareMedicineController(IPrepareMedicineApp prepareMedicineApp, IPrepareMedicineDmnService prepareMedicineDmnService)
        {
            this._prepareMedicineApp = prepareMedicineApp;
            this._prepareMedicineDmnService = prepareMedicineDmnService;
        }

        // GET: NurseManage/PrepareMedicine
        public override IActionResult Index()
        {
            return View();
        }

        #region KhoaChuẩn bị thuốcThừa thiếu
        public IActionResult ReportLossAndProfit()
        {
            return View();
        }

        /// <summary>
        /// Lấymớicủalãi lỗphiếusố
        /// </summary>
        /// <returns></returns>
        public IActionResult initialSYDH(string djmc)
        {
            var sydh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo(djmc, Constants.CurrentYfbm.yfbmCode, this.OrganizeId);
            return Content(sydh);
        }

        /// <summary>
        /// TheoLoạitảinguyên nhân lãi lỗ
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        public IActionResult GetLossProfitReasonListByType(string sylx)
        {
            var list = _prepareMedicineApp.GetLossProfitReasonListByType(sylx);
            var obj = list.Select(a => new { syyyId = a.syyyId, syyy = a.syyy }).ToJson();
            return Content(obj);
        }

        /// <summary>
        /// LấyTrách nhiệm ngườilist
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public IActionResult GetZRRList(string inputCode)
        {
            var list = sysuserDmnService.GetStaffListByOrg(OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Tra cứulãi lỗThuốclist
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public IActionResult SelectLossAndProfitMedicineList(string inputCode)
        {
            var list = _prepareMedicineApp.SelectLossAndProfitMedicineList(inputCode);
            return Content(list.ToJson());
        }

        /// <summary>
        /// GửiBáo hỏngbáolợi
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        public IActionResult SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
        {
            if (syxx == null || syxx.Length <= 0) return Error("Vui lòng truyền vào chi tiết thừa thiếu");
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
        ///// Báo hỏngbáo thừa （Lưu）
        ///// </summary>
        ///// <param name="syxx"></param>
        ///// <returns></returns>
        //public IActionResult SaveReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
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
        //        throw new FailedException("lãi lỗthông tinkhôngcó thểlàtrống");
        //    }
        //    var result = _medicineDmnService.SaveReportLossAndProfit(profitLossEntityList);
        //    return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        //}

        #endregion

        #region KhoaChuẩn bị thuốcThừa thiếuTra cứu

        /// <summary>
        /// Khoachuẩn bị thuốcTrả lại
        /// </summary>
        /// <returns></returns>
        public IActionResult ReportLossAndProfitQuery()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            return View();
        }


        /// <summary>
        /// Báo hỏngbáo thừaTra cứu
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string syyy, string inputCode, int syqk)
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
        /// Giá bán buônTổng số tiền、Giá bán lẻTổng số tiềnTra cứu
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">nguyên nhân lãi lỗ</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">lãi lỗtình hình</param>
        /// <returns></returns>
        public IActionResult ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk)
        {
            return Content(_prepareMedicineApp.ComputePjzeAndLjze(startTime, endTime, syyy, inputCode, syqk).ToJson());
        }
        #endregion

        #region KhoaChuẩn bị thuốcTrả lại

        /// <summary>
        /// Khoachuẩn bị thuốcTrả lại
        /// </summary>
        /// <returns></returns>
        public IActionResult PrepareMedicineReturn()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            return View();
        }

        /// <summary>
        /// Tạo mớiTrả lạiPhiếu
        /// </summary>
        /// <returns></returns>
        public IActionResult PrepareMedicineReturnAdd() {
            return View();
        }

        /// <summary>
        /// LưuKhoachuẩn bị thuốcTrả lại
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SubmitKsbyth(BythDjInfoDTO Djnr)
        {
            OperatorModel user = this.UserIdentity;
            var data = _prepareMedicineDmnService.PrepareMedicineReturnSubmit(user, this.OrganizeId, Djnr);
            return Success(data);
        }

        /// <summary>
        /// Khoachuẩn bị thuốcPhiếu：Thuốcthông tingắn
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbm"></param>
        /// <returns></returns>
        public IActionResult DrugAndStockSearch(string keyword, string yfbm)
        {
            var result = _prepareMedicineDmnService.GetDrugAndStock(yfbm, keyword, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// Khoachuẩn bị thuốcThuốcđiềnTra cứu
        /// </summary>
        /// <param name="ypcodestr"></param>
        /// <param name="yfbmstr"></param>
        /// <returns></returns>
        public IActionResult ApplyDrugsSearch(string ypcodestr, string yfbmstr)
        {
            var result = _prepareMedicineDmnService.GetApplyDrugAndStock(ypcodestr, yfbmstr, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// Khoachuẩn bị thuốcTrả lạiTra cứudanh sách
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
        public IActionResult GetPrepareMedicineReturnGridJson(Pagination pagination, string thzt,  DateTime kssj, DateTime jssj)
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
        /// phiếucứchi tiết
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>
        public IActionResult QueryPrepareMedicine(string djId)
        {
            var result = _prepareMedicineDmnService.QueryPrepareMedicine(djId, this.OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// chuẩn bị thuốcphiếucứGửi
        /// </summary>
        /// <param name="djmc"></param>
        /// <returns></returns>
        public IActionResult UpdatePrepareMedicineReturn(PrapareMedicineReturnEntity entity)
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
        /// TheoIDTra cứuphiếucứdanh sách
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>

        public IActionResult QueryPrepareMedicineReturnbyId(string djId)
        {
            var result = _prepareMedicineDmnService.QueryPrepareMedicineReturnbyId(djId, this.OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// TheoIDTra cứuphiếucứchi tiết
        /// </summary>
        /// <param name="djId"></param>
        /// <returns></returns>
        public IActionResult QueryPrepareMedicineReturnMXbyId(string djId)
        {
            var result = _prepareMedicineDmnService.QueryPrepareMedicineReturnMXbyId(djId, this.OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// LấyThuốcTồn khoSố lượng
        /// </summary>
        /// <param name="ypbm"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="yfbm"></param>
        /// <returns></returns>
        public IActionResult BydjQueryKykc(string ypbm, string pc, string ph, string yfbm)
        {
            var result = new
            {
                kysl = _prepareMedicineDmnService.BydjQueryKykc(ypbm, pc, ph, yfbm, this.OrganizeId)
            };
            return Content(result.ToJson());
        }

        /// <summary>
        /// rútvề
        /// </summary>
        /// <param name="Djh"></param>
        /// <returns></returns>
        public IActionResult Bydjback(string Djh)
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
        public IActionResult Bydjdelete(string ById)
        {
            OperatorModel user = this.UserIdentity;
            var data = _prepareMedicineDmnService.PrepareMedicineReturndelete(ById, OrganizeId, user);
            return Success(data);
        }
        #endregion

        #region ThuốcSử dụngTra cứu
        public IActionResult MedicineUseQuery()
        {
            //ViewBag.OrgId = this.OrganizeId;
            //ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            return View();
        }

        //Thuốcthông tin
        public IActionResult GetypGridJson(Pagination pagination, MedicineInfoParam model, string keyword, string organizeId)
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