using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;

namespace Newtouch.HIS.DomainServices.OutOrInStoredOperate
{
    /// <summary>
    /// Phát thuốc cho khoa
    /// </summary>
    public class DeliveryToDepartmentDmnService : DmnServiceBase, IDeliveryToDepartmentDmnService
    {
        private readonly IKcxxDmnService kcxxDmnService;
        private readonly ISysMedicineStorageIOReceiptRepo crkdj;
        private readonly ISysMedicineStorageIOReceiptDetailRepo crdjmx;

        
        /// <summary>
        /// Gửi phát thuốc cho khoa
        /// </summary>
        /// <returns></returns>
        public string SubmitDeliveryToDepartment(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx)
        {
            var result = "";
            // G36 F36-5: chặn trùng số chứng từ (Pdh không có unique constraint trên DB;
            // 2 phiếu cùng số làm query hóa đơn gộp sai). Kiểm tra TRƯỚC mọi trừ kho.
            var dupPdh = crkdj.FindEntity(p => p.Pdh == dj.Pdh && p.djlx == dj.djlx && p.OrganizeId == dj.OrganizeId && p.zt == "1" && p.crkId != dj.crkId);
            if (dupPdh != null)
            {
                return string.Format("Số chứng từ [{0}] đã tồn tại", dj.Pdh);
            }
            using (var db = new EFDbTransaction().BeginTrans())
            {
                foreach (var d in mx)
                {
                    var frozenStockResult = kcxxDmnService.SubtractStock(d.Ypdm, d.Sl * d.Ckzhyz, d.pc, d.Ph, dj.Ckbm, dj.OrganizeId);
                    if (!string.IsNullOrWhiteSpace(frozenStockResult))
                    {
                        return frozenStockResult;
                    }
                }
                crkdj.Insert(dj);
                crdjmx.Insert(mx);
                db.Commit();
            }
            return result;
        }
    }
}
