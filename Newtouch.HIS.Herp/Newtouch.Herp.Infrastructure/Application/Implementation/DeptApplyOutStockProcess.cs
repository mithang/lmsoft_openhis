using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Infrastructure.EF;
using SqlSugar;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Port of the original DeptApplyOutStockProcess (phát hàng theo phiếu lĩnh).
    /// Same note as the bill-approval processes: dependencies are
    /// constructor-injected instead of the original's binary field injection.
    /// Transaction handling follows the ported pattern already used by
    /// PurchasingOrderApp: new EFDbTransaction(HerpDb.GetConnection()).
    /// The yfsl UPDATE uses db.Db.Ado.ExecuteCommand with SugarParameter,
    /// exactly like PurchasingOrderApp's detail UPDATE.
    /// </summary>
    public class DeptApplyOutStockProcess: ProcessorFun<DjInfDTO>
    {
        private readonly IKfCrkdjRepo _crkdjRepo;
        private readonly IKfCrkmxRepo _crkmxRepo;
        private readonly IKfKcxxDmnService iKfKcxxDmnService;
        private readonly IKfApplyOrderRepo _kfApplyOrderRepo;
        private readonly IKfApplyOrderDetailRepo _kfApplyOrderDetailRepo;

        public DeptApplyOutStockProcess(DjInfDTO request, IKfCrkdjRepo crkdjRepo,
        IKfCrkmxRepo crkmxRepo, IKfKcxxDmnService kfKcxxDmnService,
        IKfApplyOrderRepo kfApplyOrderRepo, IKfApplyOrderDetailRepo kfApplyOrderDetailRepo)
        : base(request)
        {
            _crkdjRepo = crkdjRepo;
            _crkmxRepo = crkmxRepo;
            iKfKcxxDmnService = kfKcxxDmnService;
            _kfApplyOrderRepo = kfApplyOrderRepo;
            _kfApplyOrderDetailRepo = kfApplyOrderDetailRepo;
        }

        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("Nội dung phiếu không được để trống");
            if (Request.crkdj == null) throw new FailedException("Thông tin chính phiếu không được để trống");
            if (Request.crkdjmx == null || Request.crkdjmx.Count == 0) throw new FailedException("Thông tin chi tiết phiếu không được để trống");
            if (Request.applyBillDetail == null || Request.applyBillDetail.Count == 0) throw new FailedException("Thông tin phiếu lĩnh không được để trống");
            return base.Validata();
        }

        protected override void Action(ActResult actResult)
        {
            try
            {
                if (_crkdjRepo.Insert(Request.crkdj) <= 0) throw new FailedException("Tạo phiếu nhập/xuất kho thất bại");
                if (Request.crkdj.Id <= 0)
                {
                    var fresh = _crkdjRepo.FindEntity(q => q.Pdh == Request.crkdj.Pdh && q.OrganizeId == Request.crkdj.OrganizeId);
                    if (fresh != null && fresh.Id > 0) Request.crkdj.Id = fresh.Id;
                }
                using (var db = new EFDbTransaction(HerpDb.GetConnection()).BeginTrans())
                {
                    Request.crkdjmx.ForEach(p =>
                    {
                        p.crkId = Request.crkdj.Id;
                        db.Insert(p);
                    });

                    var updateKcslParam = AssembleUpdateKcslDTO();
                    var subtractKcslResult = iKfKcxxDmnService.JustSubtractKcslNoTrans(updateKcslParam, db);
                    if (!string.IsNullOrWhiteSpace(subtractKcslResult)) throw new Exception(subtractKcslResult);

                    foreach (var sldh in Request.applyBillDetail.Select(p => p.sldh).Distinct())
                    {
                        var abd = Request.applyBillDetail.FindAll(p => p.sldh == sldh);
                        foreach (var mx in abd)
                        {
                            const string updateYfslSql = @"
                            UPDATE dbo.kf_applyOrderDetail
                            SET yfsl=ISNULL(yfsl,0)+@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
                            WHERE Id=@sldmxId AND zt='1'
                            ";
                            db.Db.Ado.ExecuteCommand(updateYfslSql,
                            new SugarParameter("@sldmxId", mx.sldmxId),
                            new SugarParameter("@sl", mx.xfsl),
                            new SugarParameter("@userCode", Request.crkdj.CreatorCode));
                        }

                        var appBillDetail = _kfApplyOrderDetailRepo.SelectData(sldh, Request.crkdj.OrganizeId);
                        if (appBillDetail == null || appBillDetail.Count == 0) throw new Exception(string.Format("Phiếu lĩnhkhông tìm thấy chi tiết phiếu lĩnh hợp lệ", sldh));

                        var applyBill = _kfApplyOrderRepo.SelectData(sldh, Request.crkdj.OrganizeId);
                        if (applyBill == null) throw new Exception(string.Format("Số phiếu lĩnhkhông hợp lệ", sldh));

                        applyBill.applyProcess = (int)(appBillDetail.Exists(p => p.sl > p.yfsl)
                        ? EnumApplyProcess.PartialCompletion
                        : EnumApplyProcess.Completion);
                        applyBill.LastModifierCode = Request.crkdj.CreatorCode;
                        applyBill.LastModifyTime = Request.crkdj.CreateTime;
                        db.Update(applyBill);
                    }

                    db.Commit();
                }
            }
            catch (Exception e)
            {
                _crkdjRepo.Delete(Request.crkdj);
                actResult.IsSucceed = false;
                actResult.ResultMsg = e.Message;
            }
        }

        private List<UpdateKcslDTO> AssembleUpdateKcslDTO()
        {
            return Request.crkdjmx.Select(p => new UpdateKcslDTO
            {
                productId = p.productId,
                pc = p.pc,
                ph = p.ph,
                warehouseId = Request.crkdj.ckbm,
                sl = p.sl * p.zhyz,
                organizeId = Request.crkdj.OrganizeId,
                userCode = Request.crkdj.CreatorCode
            }).ToList();
        }
    }
}
