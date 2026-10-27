using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.DTO.InputDto;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 盘点明细
    /// </summary>
    public class KcPdxxmxRepo : RepositoryBase<KcPdxxmxEntity>, IKcPdxxmxRepo
    {
        public KcPdxxmxRepo()
        {
        }

        /// <summary>
        /// 盘点保存时 变更数量
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        public void UpdateSlBySaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList)
        {
            using (var db = new EFDbTransaction(Db).BeginTrans())
            {
                foreach (var item in inventoryInfoList)
                {
                    var entity = FindEntity(p => p.Id == item.pdmxId);
                    entity.sjsl = item.sjsl;
                    entity.Modify();
                    Update(entity);
                }
                db.Commit();
            }
        }

        /// <summary>
        /// 盘点保存时 变更数量
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        public void UpdateSlBySaveInventoryInfoNoPc(List<SaveInventoryDTO> inventoryInfoList)
        {
            using (var db = new EFDbTransaction(Db).BeginTrans())
            {
foreach (var item in inventoryInfoList)
                {
                    var entity = FindEntity(p => p.Id == item.pdmxId);
                    entity.sjsl = item.sjsl;
                    entity.Modify();
                    Update(entity);
                }
                db.Commit();
            }
        }
    }
}
