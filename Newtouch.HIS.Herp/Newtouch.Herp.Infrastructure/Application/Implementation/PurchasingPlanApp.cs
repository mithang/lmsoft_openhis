using System;
using System.Collections.Generic;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Herp.Infrastructure.Log;
using Newtouch.Infrastructure.EF;
using NewtouchHIS.Lib.Base.Extension;
using SqlSugar;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Kế hoạch mua sắm
    /// </summary>
    public class PurchasingPlanApp : AppBase, IPurchasingPlanApp
    {
        private readonly ICgPurchaseOrderRepo _cgPurchaseOrderRepo;
        private readonly ICgPurchaseOrderDetailRepo _cgPurchaseOrderDetailRepo;

        // Original relied on binary field injection; Core port injects explicitly.
        public PurchasingPlanApp(ICgPurchaseOrderRepo cgPurchaseOrderRepo,
            ICgPurchaseOrderDetailRepo cgPurchaseOrderDetailRepo)
        {
            _cgPurchaseOrderRepo = cgPurchaseOrderRepo;
            _cgPurchaseOrderDetailRepo = cgPurchaseOrderDetailRepo;
        }

        /// <summary>
        /// Gửi kế hoạch mua sắm
        /// </summary>
        /// <param name="pp"></param>
        /// <param name="ppmx"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SubmitFillPurchasingPlan(CgPurchaseOrderEntity pp, List<CgPurchaseOrderDetailEntity> ppmx, string userCode, string organizeId)
        {
            if (pp == null) return "Thông tin chính kế hoạch mua sắm không được để trống";
            if (ppmx == null || ppmx.Count == 0) return "Chi tiết kế hoạch mua sắm không được để trống";
            try
            {
                var oldpp = _cgPurchaseOrderRepo.SelectData(pp.cgdh, organizeId);
                if (oldpp != null && oldpp.Id > 0)
                {
                    var checkResult = CheckIsAuditRefuseOrTemporaryBill(oldpp.cgdh, organizeId, ref oldpp);
                    if (!string.IsNullOrWhiteSpace(checkResult)) return checkResult;

                    UpdatePurchasingPlan(pp, ppmx, oldpp, userCode, organizeId);
                    return "";
                }

                //Thêm mới
                if (!string.IsNullOrWhiteSpace(pp.remark) && System.Text.Encoding.Default.GetBytes(pp.remark).Length > 200)
                {
                    throw new Exception("Độ dài ghi chú không được vượt quá 2000 ký tự");
                }
                var operateTime = DateTime.Now;
                pp.OrganizeId = organizeId;
                pp.CreatorCode = userCode;
                pp.CreateTime = operateTime;
                if (_cgPurchaseOrderRepo.Insert(pp) <= 0) throw new Exception("Lưu thông tin chính kế hoạch mua sắm thất bại");
                // PORT NOTE: copy's entities mark long Id as primary key only
                // (no IsIdentity), so SqlSugar may not populate pp.Id on insert.
                // Re-read by the unique plan number when Id is still 0, so the
                // detail rows link to the real header row.
                if (pp.Id <= 0)
                {
                    var fresh = _cgPurchaseOrderRepo.SelectData(pp.cgdh, organizeId);
                    if (fresh != null && fresh.Id > 0) pp.Id = fresh.Id;
                }
                using (var db = new EFDbTransaction(HerpDb.GetConnection()).BeginTrans())
                {
                    InsertPurchasingPlanDetail(pp.Id, ppmx, operateTime, userCode, db);
                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                // Copy's LogCore takes an optional dictionary, not a string.
                LogCore.Error("SubmitFillPurchasingPlan error", e);
                _cgPurchaseOrderRepo.Delete(pp);
                return "Lưu kế hoạch mua sắm thất bại";
            }
        }

        /// <summary>
        /// Sửa kế hoạch mua sắm
        /// </summary>
        /// <param name="pp"></param>
        /// <param name="ppmx"></param>
        /// <param name="oldpp"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        private void UpdatePurchasingPlan(CgPurchaseOrderEntity pp, List<CgPurchaseOrderDetailEntity> ppmx, CgPurchaseOrderEntity oldpp, string userCode, string organizeId)
        {
            using (var db = new EFDbTransaction(HerpDb.GetConnection()).BeginTrans())
            {
                if (!string.IsNullOrWhiteSpace(pp.remark) && System.Text.Encoding.Default.GetBytes(pp.remark).Length > 200)
                {
                    throw new Exception("Độ dài ghi chú không được vượt quá 2000 ký tự");
                }

                var operateTime = DateTime.Now;
                oldpp.auditState = pp.auditState;
                oldpp.LastModifierCode = userCode;
                oldpp.LastModifyTime = operateTime;
                oldpp.remark = pp.remark;
                oldpp.rkbmCode = pp.rkbmCode;
                db.Update(oldpp);
                const string sql = @"DELETE FROM dbo.cg_purchaseOrderDetail WHERE purchaseId=@purchaseId";
                db.Db.Ado.ExecuteCommand(sql, new SugarParameter("@purchaseId", oldpp.Id));
                InsertPurchasingPlanDetail(oldpp.Id, ppmx, operateTime, userCode, db);

                db.Commit();
            }
        }

        /// <summary>
        /// Lưu chi tiết kế hoạch mua sắm
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <param name="ppmx"></param>
        /// <param name="operateTime"></param>
        /// <param name="userCode"></param>
        /// <param name="db"></param>
        private void InsertPurchasingPlanDetail(long purchaseId, List<CgPurchaseOrderDetailEntity> ppmx, DateTime operateTime, string userCode, EFDbTransaction db)
        {
            ppmx.ForEach(p =>
            {
                p.purchaseId = purchaseId;
                p.zt = ((int)Enumzt.Enable).ToString();
                p.CreatorCode = userCode;
                p.CreateTime = operateTime;
                db.Insert(p);
            });
        }

        /// <summary>
        /// Hủy kế hoạch mua sắm
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string CancelPurchasingPlan(string cgdh, string organizeId, string userCode)
        {
            var entity = new CgPurchaseOrderEntity();
            var checkResult = CheckIsTemporaryBill(cgdh, organizeId, ref entity);
            if (!string.IsNullOrWhiteSpace(checkResult)) return checkResult;
            return _cgPurchaseOrderRepo.CancelPurchasingPlan(cgdh, organizeId, userCode) > 0 ? "" : "Hủy bỏ thất bại";
        }

        /// <summary>
        /// Kiểm tra có phải phiếu lưu tạm không
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CheckIsTemporaryBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity)
        {
            if (entity == null || entity.Id <= 0) entity = _cgPurchaseOrderRepo.SelectData(cgdh, organizeId);
            if (entity == null || entity.Id <= 0) return "Không tìm thấy kế hoạch mua sắm phù hợp yêu cầu";
            return entity.auditState != (int)EnumAuditState.Temporary ? "Thao tác này chỉ áp dụng cho kế hoạch mua sắm lưu tạm" : "";
        }

        /// <summary>
        /// Kiểm tra có phải không duyệt không
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CheckIsAuditRefuseBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity)
        {
            if (entity == null || entity.Id <= 0) entity = _cgPurchaseOrderRepo.SelectData(cgdh, organizeId);
            if (entity == null || entity.Id <= 0) return "Không tìm thấy kế hoạch mua sắm phù hợp yêu cầu";
            return entity.auditState != (int)EnumAuditState.Refuse ? "Thao tác này chỉ áp dụng cho kế hoạch mua sắm bị từ chối duyệt" : "";
        }

        /// <summary>
        /// Kiểm tra có phải phiếu kế hoạch mua sắm không duyệt hoặc lưu tạm không
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CheckIsAuditRefuseOrTemporaryBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity)
        {
            if (entity == null || entity.Id <= 0) entity = _cgPurchaseOrderRepo.SelectData(cgdh, organizeId);
            if (entity == null || entity.Id <= 0) return "Không tìm thấy kế hoạch mua sắm phù hợp yêu cầu";
            return entity.auditState != (int)EnumAuditState.Temporary && entity.auditState != (int)EnumAuditState.Refuse ?
                "Thao tác này chỉ áp dụng cho kế hoạch mua sắm lưu tạm hoặc bị từ chối duyệt" : "";
        }

        /// <summary>
        /// Gửi kế hoạch mua sắm lưu tạm đi duyệt
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SubmitPurchasingPlan(string cgdh, string organizeId)
        {
            var entity = new CgPurchaseOrderEntity();
            var checkResult = CheckIsTemporaryBill(cgdh, organizeId, ref entity);
            if (!string.IsNullOrWhiteSpace(checkResult)) return checkResult;
            entity.auditState = (int)EnumAuditState.Waiting;
            entity.Modify();
            return _cgPurchaseOrderRepo.Update(entity) > 0 ? "" : "Gửi duyệt thất bại";
        }

        /// <summary>
        /// Duyệt kế hoạch mua sắm
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="auditState"></param>
        /// <param name="remarks">Lời phê</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string SubmitAuditPurchasingPlan(string cgdh, int auditState, string remarks, string organizeId, string userCode)
        {
            if (string.IsNullOrWhiteSpace(cgdh)) return "Số kế hoạch mua sắm không được để trống";
            var entity = _cgPurchaseOrderRepo.SelectData(cgdh, organizeId);
            if (entity == null || entity.Id <= 0) return "Không tìm thấy kế hoạch mua sắm hợp lệ theo số kế hoạch";
            if (entity.auditState != (int)EnumAuditState.Waiting) return "Thao tác duyệt chỉ áp dụng cho kế hoạch mua sắm đang chờ duyệt";
            switch (auditState)
            {
                case (int)EnumAuditState.Adopt:
                    entity.auditState = (int)EnumAuditState.Adopt;
                    break;
                case (int)EnumAuditState.Refuse:
                    entity.auditState = (int)EnumAuditState.Refuse;
                    break;
                default:
                    return "Thao tác này không hợp lệ";
            }
            if (!string.IsNullOrWhiteSpace(remarks) && System.Text.Encoding.Default.GetBytes(remarks).Length > 200) return "Độ dài ghi chú không được vượt quá 2000 ký tự";
            entity.remark = remarks;
            entity.LastModifierCode = userCode;
            entity.LastModifyTime = DateTime.Now;
            return _cgPurchaseOrderRepo.Update(entity) > 0 ? "" : "Duyệt thất bại";
        }
    }
}