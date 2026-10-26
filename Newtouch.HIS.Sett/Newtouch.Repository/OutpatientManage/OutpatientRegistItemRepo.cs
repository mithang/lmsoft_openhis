using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class OutpatientRegistItemRepo : RepositoryBase<OutpatientRegistItemEntity>, IOutpatientRegistItemRepo
    {
        public OutpatientRegistItemRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// theo ghnmtra c ứuĐ ăng ký khám ngoại trú hạng mục
        /// </summary>
        /// <param name="ghnm"></param>
        /// <returns></returns>
        public List<OutpatientRegistItemEntity> SelectRegProj(int ghnm, string orgId)
        {
            return this.IQueryable().Where(a => a.ghnm == ghnm && a.OrganizeId== orgId).ToList();
        }

    }
}


