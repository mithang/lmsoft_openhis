using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;


namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Chi tiết kiểm kê thuốc xt_yp_pdxxmx
    /// </summary>
    public class SysMedicineInventoryDetailRepo : RepositoryBase<SysMedicineInventoryDetailEntity>, ISysMedicineInventoryDetailRepo
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysMedicineInventoryDetailRepo(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }

        /// <summary>
        /// Khi lưu kiểm kê Thay đổi số lượng
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId">Đơn kiểm kê ID (chi tiết phải thuộc đơn này)</param>
        public void UpdateSlBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList, string pdId)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                foreach (var item in inventoryInfoList)
                {
                    if (string.IsNullOrWhiteSpace(item.pdmxId)) throw new FailedException("Không tìm thấy chi tiết kiểm kê.");
                    var entity = db.FindEntity<SysMedicineInventoryDetailEntity>(p => p.pdmxId == item.pdmxId);
                    if (entity == null) throw new FailedException("Không tìm thấy chi tiết kiểm kê.");
                    if (entity.pdId != pdId) throw new FailedException("Chi tiết kiểm kê không thuộc kiểm kê này");
                    if (item.sjsl < 0) throw new FailedException("Số lượng thực tế không thể là số âm");
                    if (!string.IsNullOrEmpty(item.zsm) && item.zsm.Length > 1000) throw new FailedException("Mã truy xuất tối đa 1000 ký tự");
                    if (item.sfcl.HasValue && item.sfcl.Value != 1 && item.sfcl.Value != 2) throw new FailedException("Chia lẻ chỉ nhận giá trị 1 hoặc 2");
                    entity.Sjsl = item.sjsl;
                    entity.zsm = item.zsm;
                    entity.sfcl = item.sfcl;
                    entity.Modify();
                    db.Update(entity);
                }
                db.Commit();
            }
        }


        // <summary>
        /// Lưu thông tin mã truy xuất tồn kho
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId">Đơn kiểm kê ID (chi tiết phải thuộc đơn này)</param>
        public void UpdateZsmBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList, string pdId)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                foreach (var item in inventoryInfoList)
                {
                    if (string.IsNullOrWhiteSpace(item.pdmxId)) throw new FailedException("Không tìm thấy chi tiết kiểm kê.");
                    var entity = db.FindEntity<SysMedicineInventoryDetailEntity>(p => p.pdmxId == item.pdmxId);
                    if (entity == null) throw new FailedException("Không tìm thấy chi tiết kiểm kê.");
                    if (entity.pdId != pdId) throw new FailedException("Chi tiết kiểm kê không thuộc kiểm kê này");
                    if (!string.IsNullOrEmpty(item.zsm) && item.zsm.Length > 1000) throw new FailedException("Mã truy xuất tối đa 1000 ký tự");
                    if (item.sfcl.HasValue && item.sfcl.Value != 1 && item.sfcl.Value != 2) throw new FailedException("Chia lẻ chỉ nhận giá trị 1 hoặc 2");
                    entity.zsm = item.zsm;
                    entity.sfcl = item.sfcl;
                    entity.Modify();
                    db.Update(entity);
                }
                db.Commit();
            }
        }

    }
   
}
