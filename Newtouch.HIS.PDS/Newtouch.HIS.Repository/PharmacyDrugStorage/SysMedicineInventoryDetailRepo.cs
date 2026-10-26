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
        /// Khi lưu kiểm kê, thay đổi số lượng
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        public void UpdateSlBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                foreach (var item in inventoryInfoList)
                {
                    var entity = db.FindEntity<SysMedicineInventoryDetailEntity>(item.pdmxId);
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
        public void UpdateZsmBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                foreach (var item in inventoryInfoList)
                {
                    var entity = db.FindEntity<SysMedicineInventoryDetailEntity>(item.pdmxId);
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
