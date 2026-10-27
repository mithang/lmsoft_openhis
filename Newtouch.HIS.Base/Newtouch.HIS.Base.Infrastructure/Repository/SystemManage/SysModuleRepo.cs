using System.Collections.Generic;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysModuleRepo : RepositoryBase<SysModuleEntity>, ISysModuleRepo
    {
        public SysModuleRepo()
            
        {

        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public IList<SysModuleEntity> GetValidList()
        {
            return this.IQueryable().Where(p => p.zt == "1").ToList();
        }
    }
}


