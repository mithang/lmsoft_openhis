using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.DrugStorage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.DomainServices.OutOrInStoredOperate
{
    /// <summary>
    /// Nhập kho từ ngoài
    /// </summary>
    public class InStorageDmnService : DmnServiceBase, IInStorageDmnService
    {
        private readonly ISysMedicineStorageIOReceiptRepo crkdj;
        private readonly ISysMedicineStorageIOReceiptDetailRepo crdjmx;
        private readonly ISysMedicineStockInfoDmnService kcxx;

        
        /// <summary>
        /// Gửi xuất kho trực tiếp
        /// </summary>
        /// <returns></returns>
        public string SubmitInStorage(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx)
        {
            var result = "";
            // G36 F36-1: crkId do client gửi — trùng ID đã tồn tại thì từ chối bằng
            // message rõ thay vì để lỗi vi phạm khóa chính bị nuốt thành lỗi chung.
            var dupCrk = crkdj.FindEntity(p => p.crkId == dj.crkId && p.OrganizeId == dj.OrganizeId);
            if (dupCrk != null)
            {
                return "Chứng từ đã tồn tại, không thể gửi trùng lặp";
            }
            // G36 F36-5: chặn trùng số chứng từ (Pdh không có unique constraint trên DB;
            // 2 phiếu cùng số làm query hóa đơn gộp sai). Kiểm tra TRƯỚC mọi ghi.
            var dupPdh = crkdj.FindEntity(p => p.Pdh == dj.Pdh && p.djlx == dj.djlx && p.OrganizeId == dj.OrganizeId && p.zt == "1" && p.crkId != dj.crkId);
            if (dupPdh != null)
            {
                return string.Format("Số chứng từ [{0}] đã tồn tại", dj.Pdh);
            }
            // G26: nhập kho không qua frozen nên kiểm tra mã thuốc tồn tại ở
            // đây (defect 8c: bogus ypdm vẫn tạo receipt, sau đó không duyệt
            // được vì AssembleKcxx NRE ở GetYpDetails). Cùng mẫu F5.
            const string ypSql = "select top 1 ypCode from [NewtouchHIS_Base]..V_C_xt_yp where zt = '1' and ypCode = @ypCode and OrganizeId = @orgId";
            foreach (var d in mx)
            {
                var found = FirstOrDefaultNoLog<string>(ypSql, new SugarParameter[]
                {
                    new SugarParameter("@ypCode", d.Ypdm),
                    new SugarParameter("@orgId", dj.OrganizeId)
                });
                if (found == null)
                {
                    return string.Format("Mã thuốc [{0}] không tồn tại", d.Ypdm);
                }
            }
            using (var db = new EFDbTransaction().BeginTrans())
            {
                crkdj.Insert(dj);
                crdjmx.Insert(mx);
                db.Commit();
            }
            return result;
        }

        #region Thao tác duyệt

        /// <summary>
        /// Nhập kho từ ngoài duyệt đạt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="kcxx">Thông tin tồn kho</param>
        /// <param name="auditor">Người duyệt</param>
        /// <returns></returns>
        public string InStorageAdopt(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStockInfoEntity> kcxx, string auditor)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                db.Insert(kcxx);
                dj.Rksj = DateTime.Now;
                dj.Cksj = DateTime.Now;
                dj.shzt = ((int)EnumDjShzt.Approved).ToString();
                dj.Shczy = auditor;
                dj.Rkczy = auditor;
                dj.Modify();
                crkdj.Update(dj);
                db.Commit();
                return "";
            }
        }

        /// <summary>
        /// Nhập kho từ ngoài Thu hồi duyệt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <param name="auditor"></param>
        /// <returns></returns>
        public string InStorageCancel(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx, string auditor)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                foreach(var p in mx)
                {
                    var r = kcxx.WbrkKkc(p.Sl * p.Rkzhyz, p.pc, p.Ph, p.Ypdm, dj.OrganizeId, dj.Rkbm, auditor);
                    if (!string.IsNullOrWhiteSpace(r)) return r;
                }
                dj.shzt = ((int)EnumDjShzt.Cancelled).ToString();
                dj.Shczy = auditor;
                dj.Modify();
                db.Commit();
                return "";
            }
        }


        #endregion
    }
}
