using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class GuianRybl21OutInfoRepo : RepositoryBase<GuianRybl21OutInfoEntity>, IGuianRybl21OutInfoRepo
    {
        public GuianRybl21OutInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// theo số nhập viện lấyQuý AnBHYTnhập viện phản hồi thông tin
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="OrganizeId">ID tổ chức</param>
        /// <returns></returns>
        public GuianRybl21OutInfoEntity GetInfoByZyh(string zyh, string OrganizeId)
        {
            var GuianRybl21Info = new List<GuianRybl21OutInfoEntity>();

            GuianRybl21Info = this.IQueryable().Where(p => p.OrganizeId == OrganizeId && p.prm_ykc010 == zyh).ToList();
            if (GuianRybl21Info.Count > 0)
                return GuianRybl21Info[0];
            else
                return null;
        }
    }
}
