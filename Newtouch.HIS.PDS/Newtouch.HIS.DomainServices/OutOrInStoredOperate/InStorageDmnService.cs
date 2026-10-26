using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.DrugStorage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.DomainServices.OutOrInStoredOperate
{
    /// <summary>
    /// Nhập kho ngoài
    /// </summary>
    public class InStorageDmnService : DmnServiceBase, IInStorageDmnService
    {
        private readonly ISysMedicineStorageIOReceiptRepo crkdj;
        private readonly ISysMedicineStorageIOReceiptDetailRepo crdjmx;
        private readonly ISysMedicineStockInfoDmnService kcxx;

        public InStorageDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Gửi xuất kho trực tiếp
        /// </summary>
        /// <returns></returns>
        public string SubmitInStorage(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx)
        {
            var result = "";
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                crkdj.Insert(dj);
                crdjmx.Insert(mx);
                db.Commit();
            }
            return result;
        }

        #region 审核操作

        /// <summary>
        /// Duyệt nhập kho ngoài đạt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="kcxx">Thông tin tồn kho</param>
        /// <param name="auditor">Người duyệt</param>
        /// <returns></returns>
        public string InStorageAdopt(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStockInfoEntity> kcxx, string auditor)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
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
        /// Nhập kho ngoài, hủy duyệt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <param name="auditor"></param>
        /// <returns></returns>
        public string InStorageCancel(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx, string auditor)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
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
