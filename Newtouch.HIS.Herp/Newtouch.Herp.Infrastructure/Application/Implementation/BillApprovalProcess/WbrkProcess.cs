using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.Herp.Application.Implementation.BillApprovalProcess
{
    /// <summary>
    /// Port of the original WbrkProcess (duyệt nhập kho ngoài).
    /// Same note as WbckProcess: dependencies are constructor-injected here
    /// instead of the original's binary field injection.
    /// </summary>
    public class WbrkProcess: ProcessorFun<BillApprovalDTO>
    {
        private readonly IKfCrkdjRepo _kfCrkdjRepo;
        private readonly IKfCrkmxRepo _kfCrkmxRepo;
        private readonly IStorageManageDmnService _storageManageDmnService;

        private KfCrkdjEntity _dj;
        private List<KfCrkmxEntity> _mx;
        private List<KfKcxxEntity> _kcxx;

        public WbrkProcess(BillApprovalDTO request, IKfCrkdjRepo kfCrkdjRepo,
        IKfCrkmxRepo kfCrkmxRepo, IStorageManageDmnService storageManageDmnService)
        : base(request)
        {
            _kfCrkdjRepo = kfCrkdjRepo;
            _kfCrkmxRepo = kfCrkmxRepo;
            _storageManageDmnService = storageManageDmnService;
        }

        protected override ActResult Validata()
        {
            _dj = _kfCrkdjRepo.FindEntity(p => p.Id == Request.djId && p.zt == ((int)Enumzt.Enable).ToString() && p.OrganizeId == Request.OrganizeId);
            if (_dj == null) throw new FailedException("Không tìm thấy phiếu");
            if (Request.shzt == ((int)EnumAuditState.Cancelled).ToString())
            {
                if (((int)EnumAuditState.Waiting).ToString() == _dj.auditState) throw new FailedException("Không tìm thấy bản ghi duyệt phiếu, hủy phiếu thất bại");
                if (((int)EnumAuditState.Cancelled).ToString() == _dj.auditState) throw new FailedException("Phiếu đã hủy, không thể hủy lại");
                if (((int)EnumAuditState.Refuse).ToString() == _dj.auditState) throw new FailedException("Phiếu đã duyệt không đạt, không thể hủy");
            }
            else if (Request.shzt == ((int)EnumAuditState.Refuse).ToString())
            {
                if (((int)EnumAuditState.Waiting).ToString()!= _dj.auditState) throw new FailedException("Chỉ được thao tác với phiếu đang chờ xử lý");
            }
            else
            {
                if (((int)EnumAuditState.Waiting).ToString()!= _dj.auditState) throw new FailedException(" chỉ áp dụng cho phiếu đang chờ xử lý");
            }

            _mx = _kfCrkmxRepo.IQueryable(p => p.crkId == Request.djId && p.zt == ((int)Enumzt.Enable).ToString()).ToList();
            if (_mx == null) throw new FailedException("Không tìm thấy chi tiết phiếu");

            return new ActResult();
        }

        private List<KfKcxxEntity> AssembleKcxx()
        {
            var result = new List<KfKcxxEntity>();
            _mx.ForEach(p =>
            {
                var item = new KfKcxxEntity
                {
                    crkmxId = p.Id,
                    djsl = 0,
                    jj = p.jj,
                    kcsl = p.sl * p.zhyz,
                    OrganizeId = Request.OrganizeId,
                    pc = p.pc,
                    locked = 0,
                    ph = p.ph,
                    productId = p.productId,
                    yxq = p.yxq,
                    warehouseId = _dj.rkbm,
                    zhyz = p.zhyz,
                    zt = ((int)Enumzt.Enable).ToString(),
                };
                item.Create(true);
                result.Add(item);
            });
            return result;
        }

        protected override void Action(ActResult actResult)
        {
            switch (Convert.ToInt32(Request.shzt))
            {
            case (int)EnumAuditState.Adopt:
                _kcxx = AssembleKcxx();
                _storageManageDmnService.WbrkAdopt(_kcxx, _dj);
                break;
            case (int)EnumAuditState.Refuse:
                _dj.rksj = DateTime.Now;
                _dj.auditState = Request.shzt;
                _dj.shczy = OperatorProvider.GetCurrent().UserCode;
                _dj.Modify();
                if (_kfCrkdjRepo.Update(_dj) <= 0)
                {
                    actResult.IsSucceed = false;
                    actResult.ResultMsg = "Sửa thông tin phiếu thất bại";
                }
                break;
            case (int)EnumAuditState.Cancelled:
                if (((int)EnumAuditState.Adopt).ToString() == _dj.auditState)
                {
                    var excResult = _storageManageDmnService.WbrkCancelled(_mx, _dj);
                    if (!string.IsNullOrWhiteSpace(excResult))
                    {
                        actResult.IsSucceed = false;
                        actResult.ResultMsg = excResult;
                    }
                }
                break;
            default:
                actResult.IsSucceed = false;
                actResult.ResultMsg = "Không tìm thấy thao tác phù hợp";
                break;
            }
        }
    }
}
