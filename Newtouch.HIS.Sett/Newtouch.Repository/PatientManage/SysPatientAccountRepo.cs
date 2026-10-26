using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    public class SysPatientAccountRepo : RepositoryBase<SysPatientAccountEntity>, ISysPatientAccountRepo
    {
        public SysPatientAccountRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// theo số nhập viện lấy tài khoản thông tin
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public SysPatientAccountEntity GetAccInfoByZHY(string zyh, string orgId)
        {
            return this.IQueryable().Where(p => p.zhxz == ((int)EnumXTZHXZ.ZYYJKZH).ToString() && p.zyh == zyh && p.zt == "1" && p.OrganizeId == orgId).FirstOrDefault();
        }

        /// <summary>
        /// ThêmTài khoản
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddAcc(SysPatientAccountEntity entity, string orgId, string curUserCode)
        { 
            entity.Create();
            entity.zhbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("xt_brzh");
            entity.zt = "1";
            entity.OrganizeId = orgId;
            entity.LastModifierCode = curUserCode;
            entity.LastModifyTime = null;
            this.Insert(entity);
            return true;
        }

        /// <summary>
        /// Cập nhậtTài khoản
        /// </summary>
        /// <returns></returns>
        public bool ModifyAcc(SysPatientAccountEntity entity)
        {
            entity.Modify(entity.zhbh);
            entity.zt = "1";
            this.Update(entity);
            return true;

        }
    }
}
