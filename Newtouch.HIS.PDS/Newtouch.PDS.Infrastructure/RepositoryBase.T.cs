using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Triển khai kho
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class RepositoryBase<TEntity> : Infrastructure.EF.RepositoryBase<TEntity> where TEntity : class, new()
    {

        public RepositoryBase() : base() { }

        public RepositoryBase(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) : base(databaseFactory) { }


        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="databaseFactory"></param>
        
        /// <summary>
        /// Cập nhật một phần trường
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dstnFieldNameList">Cột đích</param>
        /// <param name="ignoreFieldNameList">Cột loại trừ</param>
        /// <param name="attach">Có cần khôngEntityThêm ngữ cảnh vào(Nếu cái nàyEntitylà từdbcontexttra ra,false)</param>
        /// <returns></returns>
        public override int Update(TEntity entity, IEnumerable<string> dstnFieldNameList = null, IEnumerable<string> ignoreFieldNameList = null, bool attach = true)
        {
            return base.Update(entity, dstnFieldNameList, ignoreFieldNameList, attach);
        }


    }
}
