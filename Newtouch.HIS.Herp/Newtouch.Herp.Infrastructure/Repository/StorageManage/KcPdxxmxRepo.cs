using System;
using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.DTO.InputDto;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Chi tiết kiểm kê
    /// </summary>
    public class KcPdxxmxRepo : RepositoryBase<KcPdxxmxEntity>, IKcPdxxmxRepo
    {
        public KcPdxxmxRepo()
        {
        }

        /// <summary>
        /// Khi lưu kiểm kê Số lượng thay đổi
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        public void UpdateSlBySaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList)
        {
            // G12: list null -> foreach null NRE; pdmxId bogus -> FindEntity null
            // -> deref entity.sjsl NRE. (App đã guard null/rỗng, repo guard
            // phòng thủ cho mọi caller.)
            if (inventoryInfoList == null) throw new Exception("Không có dữ liệu kiểm kê để lưu");
            using (var db = new EFDbTransaction(Db).BeginTrans())
            {
                foreach (var item in inventoryInfoList)
                {
                    var entity = FindEntity(p => p.Id == item.pdmxId);
                    if (entity == null) throw new Exception("Không tìm thấy chi tiết kiểm kê đã chỉ định");
                    entity.sjsl = item.sjsl;
                    entity.Modify();
                    Update(entity);
                }
                db.Commit();
            }
        }

        /// <summary>
        /// Khi lưu kiểm kê Số lượng thay đổi
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        public void UpdateSlBySaveInventoryInfoNoPc(List<SaveInventoryDTO> inventoryInfoList)
        {
            // G12: cùng guard như UpdateSlBySaveInventoryInfo (dù hiện chưa có
            // caller — app dùng SaveInventoryDetailNoPc cho noPc=1).
            if (inventoryInfoList == null) throw new Exception("Không có dữ liệu kiểm kê để lưu");
            using (var db = new EFDbTransaction(Db).BeginTrans())
            {
foreach (var item in inventoryInfoList)
                {
                    var entity = FindEntity(p => p.Id == item.pdmxId);
                    if (entity == null) throw new Exception("Không tìm thấy chi tiết kiểm kê đã chỉ định");
                    entity.sjsl = item.sjsl;
                    entity.Modify();
                    Update(entity);
                }
                db.Commit();
            }
        }
    }
}
