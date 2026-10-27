using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Herp.Infrastructure.Log;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Port of the original DeliveryToDepartmentProcess (xuất kho tới khoa,
    /// tự động, không cần duyệt). The original's unused IStorageManageDmnService
    /// field is dropped; the two services it actually uses are injected.
    /// </summary>
    public class DeliveryToDepartmentProcess : ProcessorFun<DjInfDTO>
    {
        private readonly IKfKcxxDmnService iKfKcxxDmnService;
        private readonly IKfCrkdjDmnService iKfCrkdjDmnService;

        public DeliveryToDepartmentProcess(DjInfDTO request, IKfKcxxDmnService kfKcxxDmnService,
        IKfCrkdjDmnService kfCrkdjDmnService)
        : base(request)
        {
            iKfKcxxDmnService = kfKcxxDmnService;
            iKfCrkdjDmnService = kfCrkdjDmnService;
        }

        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("Nội dung phiếu không được để trống");
            if (Request.crkdj == null) throw new FailedException("Thông tin chính phiếu không được để trống");
            if (Request.crkdjmx == null || Request.crkdjmx.Count <= 0) throw new FailedException("Chi tiết phiếu không được để trống");

            return new ActResult();
        }

        protected override void Action(ActResult actResult)
        {
            var djSubmitResult = DeliveryToDepartmentSubmit(Request.crkdj, Request.crkdjmx);
            if (!string.IsNullOrWhiteSpace(djSubmitResult))
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = djSubmitResult;
                return;
            }
            var updateKcslParam = AssembleUpdateKcslDTO();
            var result = iKfKcxxDmnService.JustSubtractKcsl(updateKcslParam);
            if (string.IsNullOrWhiteSpace(result)) return;
            RollBackDj();
            actResult.IsSucceed = false;
            actResult.ResultMsg = result;
        }

        private void RollBackDj()
        {
            try
            {
                if (iKfCrkdjDmnService.DeleteDjById(Request.crkdj.Id, Request.crkdj.OrganizeId) <= 0)
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                LogCore.Error("Xuất kho tự động tới khoa", e, Tags);
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

        private string DeliveryToDepartmentSubmit(KfCrkdjEntity crkdj, List<KfCrkmxEntity> crkdjmx)
        {
            crkdj.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            crkdj.ckczy = OperatorProvider.GetCurrent().UserCode;
            crkdj.ckbm = Constants.CurrentKf.currentKfId;
            crkdj.auditState = ((int)EnumAuditState.Adopt).ToString();
            crkdj.rksj = DateTime.Now;
            crkdj.rkczy = OperatorProvider.GetCurrent().UserCode;
            crkdj.cksj = DateTime.Now;
            crkdj.zt = ((int)Enumzt.Enable).ToString();
            crkdj.djlx = (int)EnumOutOrInStorageBillType.chukuzhikeshi;
            crkdj.CreateTime = DateTime.Now;
            crkdj.CreatorCode = OperatorProvider.GetCurrent().UserCode;
            crkdj.Create(false);

            var mxList = new List<KfCrkmxEntity>();
            foreach (var item in crkdjmx)
            {
                item.rkbmkc = 0;
                item.zt = ((int)Enumzt.Enable).ToString();
                item.CreateTime = DateTime.Now;
                item.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                item.Create(false);
                mxList.Add(item);
            }
            return iKfCrkdjDmnService.SaveDj(crkdj, mxList);
        }
    }
}
