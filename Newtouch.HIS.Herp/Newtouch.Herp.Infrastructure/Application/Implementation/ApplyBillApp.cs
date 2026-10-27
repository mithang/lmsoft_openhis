using System.Collections.Generic;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 申领单管理 (Core port)
    /// </summary>
    public class ApplyBillApp : AppBase, IApplyBillApp
    {
        private readonly IKfApplyOrderRepo _kfApplyOrderRepo;
        private readonly IKfApplyOrderDetailRepo _kfApplyOrderDetailRepo;

        // Original relied on binary field injection; Core port injects explicitly.
        // NOTE: the original ctor also declared IWzCrkfsRepo / IKfKcxxRepo /
        // IWzProductDmnService for SubmitApplyOutStock, but those implementations
        // are not ported in this copy (Product/Storage areas), so they are NOT
        // constructor dependencies here; SubmitApplyOutStock is an explicit stub
        // until DeptApplyOutStockProcess + inventory services exist.
        public ApplyBillApp(IKfApplyOrderRepo kfApplyOrderRepo,
            IKfApplyOrderDetailRepo kfApplyOrderDetailRepo)
        {
            _kfApplyOrderRepo = kfApplyOrderRepo;
            _kfApplyOrderDetailRepo = kfApplyOrderDetailRepo;
        }

        /// <summary>
        /// 提交科室申领单
        /// </summary>
        /// <param name="sld"></param>
        /// <param name="sldmx"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SubmitDepartmentApply(KfApplyOrderEntity sld, List<KfApplyOrderDetailEntity> sldmx, string organizeId)
        {
            if (sld == null) return "Thông tin chính phiếu lĩnh không được để trống";
            if (sldmx == null || sldmx.Count == 0) return "Chi tiết phiếu lĩnh không được để trống";
            sld.OrganizeId = organizeId;
            sld.applyProcess = (int)EnumApplyProcess.AuditApproved;
            sld.applyType = (int)EnumApplyType.DepartmentApply;
            // Create(false): long identity Id is assigned by the DB, not here.
            sld.Create(false);
            if (_kfApplyOrderRepo.Insert(sld) <= 0) return "Lưu thông tin chính phiếu lĩnh thất bại";
            // Same identity caveat as PurchasingPlanApp: re-read by plan number
            // when the insert did not populate the long Id.
            if (sld.Id <= 0)
            {
                var fresh = _kfApplyOrderRepo.SelectData(sld.sldh, organizeId);
                if (fresh != null && fresh.Id > 0) sld.Id = fresh.Id;
            }
            sldmx.ForEach(p =>
            {
                p.sldId = sld.Id;
                p.Create(false);
            });
            if (_kfApplyOrderDetailRepo.Insert(sldmx) > 0) return "";
            _kfApplyOrderRepo.Delete(sld);
            return "Lưu chi tiết phiếu lĩnh thất bại";
        }

        /// <summary>
        /// 提交申领发货
        /// </summary>
        /// <param name="ckmx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string SubmitApplyOutStock(List<VApplyBillDetailEntity> ckmx, string organizeId, string userCode)
        {
            // STUB (explicit, per task rules): the original runs
            // DeptApplyOutStockProcess, which needs IKfKcxxDmnService inventory
            // updates + IWzCrkfsRepo + IWzProductDmnService implementations that
            // are not ported in this copy (StorageManage/Product areas). No fake
            // stock movement is performed; returns an error string so the caller
            // surfaces this notice instead of silently succeeding.
            return "Chức năng phát hàng theo phiếu lĩnh chưa khả dụng: quy trình xuất kho (DeptApplyOutStockProcess) phụ thuộc module Quản lý kho chưa được chuyển đổi.";
        }
    }
}
