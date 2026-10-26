using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class RepositoryBase<TEntity> : Newtouch.Infrastructure.EF.RepositoryBase<TEntity>
        where TEntity : class, new()
    {
        public RepositoryBase(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Cập nhật một phần trư ờng 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dstnFieldNameList">目标 cột </param>
        /// <param name="ignoreFieldNameList"> loại trừ cột </param>
        /// <param name="attach">Có hay không cần sẽ Entity附 thêm vào ngữ cảnh(nếu này Entity là từ dbcontext tra cứu 出来của,false)</param>
        /// <returns></returns>
        public override int Update(TEntity entity, IEnumerable<string> dstnFieldNameList = null, IEnumerable<string> ignoreFieldNameList = null, bool attach = true)
        {
            if (attach)
            {
                // duy nhất một loại 发生bất thư ờng của tình hình:尝试附加 giống nhau khóa chính của khác nhau thực thể(lưu ý này là引 dùng loại, tức là cùng một thực thể trùng lặp Attach là sẽ không có问题của)
                _dataContext.Set<TEntity>().Attach(entity);
            }
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == "CreatorCode" || prop.Name == "CreateTime" || prop.Name == "CreatorName")
                {
                    //_dataContext.Entry(entity).Property(prop.Name).IsModified = false;
                    continue;
                }
                if (prop.Name != "LastModifyTime" && prop.Name != "LastModifierCode")
                {
                    if (dstnFieldNameList != null && dstnFieldNameList.Count() > 0)
                    {
                        if (!dstnFieldNameList.Contains(prop.Name))
                        {
                            //_dataContext.Entry(entity).Property(prop.Name).IsModified = false;
                            continue;
                        }
                    }
                    else if (ignoreFieldNameList != null && ignoreFieldNameList.Count() > 0)
                    {
                        if (ignoreFieldNameList.Contains(prop.Name))
                        {
                            //_dataContext.Entry(entity).Property(prop.Name).IsModified = false;
                            continue;
                        }
                    }
                }
                _dataContext.Entry(entity).Property(prop.Name).IsModified = true;
            }
            return _dataContext.SaveChanges();
        }

    }
}
