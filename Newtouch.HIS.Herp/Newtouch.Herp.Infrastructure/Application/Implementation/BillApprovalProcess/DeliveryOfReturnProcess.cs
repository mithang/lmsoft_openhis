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
    /// Port of the original DeliveryOfReturnProcess (duyệt trả hàng nội bộ).
    /// Body is identical to DirectDeliveryProcess in the original.
    /// </summary>
    public class DeliveryOfReturnProcess: ProcessorFun<BillApprovalDTO>
    {
        private readonly IKfCrkdjRepo _kfCrkdjRepo;
        private readonly IKfCrkmxRepo _kfCrkmxRepo;
        private readonly IStorageManageDmnService _storageManageDmnService;
        private KfCrkdjEntity _dj;
        private List<KfCrkmxEntity> _mx;

        public DeliveryOfReturnProcess(BillApprovalDTO request, IKfCrkdjRepo kfCrkdjRepo,
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
                if (((int)EnumAuditState.Waiting).ToString()== _dj.auditState)
                throw new FailedException("Không tìm thấy bản ghi duyệt phiếu, hủy phiếu thất bại");
                if (((int)EnumAuditState.Cancelled).ToString() == _dj.auditState)
                throw new FailedException("Phiếu đã hủy, không thể hủy lại");
                if (((int)EnumAuditState.Refuse).ToString() == _dj.auditState)
                throw new FailedException("Phiếu đã duyệt không đạt, không thể hủy");
            }
            else if (Request.shzt == ((int)EnumAuditState.Refuse).ToString())
            {
                if (((int)EnumAuditState.Waiting).ToString()!= _dj.auditState)
                throw new FailedException("Chỉ được thao tác với phiếu đang chờ xử lý");
            }
            else
            {
                if (((int)EnumAuditState.Waiting).ToString()!= _dj.auditState)
                throw new FailedException(" chỉ áp dụng cho phiếu đang chờ xử lý");
            }

            _mx = _kfCrkmxRepo.IQueryable(p => p.crkId == Request.djId && p.zt == ((int)Enumzt.Enable).ToString())
            .ToList();
            if (_mx == null) throw new FailedException("Không tìm thấy chi tiết phiếu");
            return new ActResult();
        }

        protected override void Action(ActResult actResult)
        {
            string result;
            switch (Convert.ToInt32(Request.shzt))
            {
            case (int)EnumAuditState.Adopt:
                result = _storageManageDmnService.DirectDeliveryAdopt(_dj, _mx, OperatorProvider.GetCurrent().UserCode);
                break;
            case (int)EnumAuditState.Refuse:
                result = _storageManageDmnService.Unfreeze(_dj, _mx, OperatorProvider.GetCurrent().UserCode);
                break;
            case (int)EnumAuditState.Cancelled:
                result = _storageManageDmnService.DirectDeliveryCancel(_dj, _mx, OperatorProvider.GetCurrent().UserCode);
                break;
            default:
                result = "Không tìm thấy thao tác phù hợp";
                break;
            }
            if (string.IsNullOrWhiteSpace(result)) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = result;
        }
    }
}
