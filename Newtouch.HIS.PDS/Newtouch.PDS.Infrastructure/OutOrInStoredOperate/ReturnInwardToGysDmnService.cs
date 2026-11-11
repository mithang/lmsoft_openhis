using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.OutOrInStoredOperate
{
    /// <summary>
    /// Xuất kho ra ngoài
    /// </summary>
    public class ReturnInwardToGysDmnService : DmnServiceBase, IReturnInwardToGysDmnService
    {
        private readonly IKcxxDmnService kcxxDmnService;
        private readonly ISysMedicineStorageIOReceiptRepo crkdj;
        private readonly ISysMedicineStorageIOReceiptDetailRepo crdjmx;

        
        /// <summary>
        /// Gửi xuất kho trực tiếp
        /// </summary>
        /// <returns></returns>
        public string SubmitReturnInwardToGys(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx)
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
            // 2 phiếu cùng số làm query hóa đơn gộp sai). Kiểm tra TRƯỚC mọi đóng băng kho.
            var dupPdh = crkdj.FindEntity(p => p.Pdh == dj.Pdh && p.djlx == dj.djlx && p.OrganizeId == dj.OrganizeId && p.zt == "1" && p.crkId != dj.crkId);
            if (dupPdh != null)
            {
                return string.Format("Số chứng từ [{0}] đã tồn tại", dj.Pdh);
            }
            using (var db = new EFDbTransaction().BeginTrans())
            {
                foreach (var d in mx)
                {
                    var frozenStockResult = kcxxDmnService.FrozenStockReduceByDeliveryDirect(d.Ypdm, dj.OrganizeId, d.pc, d.Ph, d.Sl * d.Ckzhyz, dj.Ckbm);
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
