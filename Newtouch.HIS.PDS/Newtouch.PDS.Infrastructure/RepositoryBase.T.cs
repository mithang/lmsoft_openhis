using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class RepositoryBase<TEntity> : Infrastructure.EF.RepositoryBase<TEntity> where TEntity : class, new()
    {

        public RepositoryBase() : base() { }

        public RepositoryBase(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) : base(databaseFactory) { }


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        
        /// <summary>
        /// 更新部分字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dstnFieldNameList">目标列</param>
        /// <param name="ignoreFieldNameList">排除列</param>
        /// <param name="attach">是否需要将Entity附加上上下文（如果这个Entity是从dbcontext查出来的，false）</param>
        /// <returns></returns>
        public override int Update(TEntity entity, IEnumerable<string> dstnFieldNameList = null, IEnumerable<string> ignoreFieldNameList = null, bool attach = true)
        {
            return base.Update(entity, dstnFieldNameList, ignoreFieldNameList, attach);
        }


    }
}
