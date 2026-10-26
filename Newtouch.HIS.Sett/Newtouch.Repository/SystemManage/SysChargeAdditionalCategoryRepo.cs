using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    public class SysChargeAdditionalCategoryRepo : RepositoryBase<SysChargeAdditionalCategoryEntity>, ISysChargeAdditionalCategoryRepo
    {
        public SysChargeAdditionalCategoryRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <returns></returns>
        public List<SysChargeAdditionalCategoryEntity> GetEffectiveList(int keyValue, string orgId)
        {
            return this.IQueryable().Where(a => a.zt == "1" && a.dlbh == keyValue && a.OrganizeId == orgId).ToList();
        }
        /// <summary>
        /// Lấy tất cả các hiệu lựcdanh sách
        /// </summary>
        /// <returns></returns>
        public List<SysChargeAdditionalCategoryEntity> SelectALLEffectiveList(string orgId)
        {
            return this.IQueryable().Where(a => a.zt == "1" && a.OrganizeId == orgId).ToList();
        }
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysChargeAdditionalCategoryEntity GetForm(int keyValue, string orgId)
        {
            return this.IQueryable().Where(a=>a.dlbh == keyValue && a.OrganizeId == orgId).FirstOrDefault();
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(int keyValue, string orgId)
        {
            this.Delete(t => t.dlbh == keyValue && t.OrganizeId == orgId);
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="sysChargeAddCategEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysChargeAdditionalCategoryEntity sysChargeAddCategEntity, int keyValue, string orgId)
        {
            if (keyValue > 0)
            {
                sysChargeAddCategEntity.Modify(keyValue);
                this.Update(sysChargeAddCategEntity);
            }
            else
            {
                sysChargeAddCategEntity.OrganizeId = orgId;
                sysChargeAddCategEntity.Create();
                this.Insert(sysChargeAddCategEntity);
            }

        }

    }
}
