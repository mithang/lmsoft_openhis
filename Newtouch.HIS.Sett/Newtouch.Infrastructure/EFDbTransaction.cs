using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class EFDbTransaction : Newtouch.Infrastructure.EF.EFDbTransaction
    {
        public EFDbTransaction(IDatabaseFactory databaseFactory) 
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Cập nhật một phần trư ờng 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dstnFieldNameList">mục tiêu cột </param>
        /// <param name="ignoreFieldNameList"> loại trừ cột </param>
        /// <param name="attach">Có hay không cần sẽ Entitygắn thêm vào ngữ cảnh(nếu này Entity là từ dbcontext tra cứu racủa,false)</param>
        /// <returns></returns>
        public override int Update<TEntity>(TEntity entity, IEnumerable<string> dstnFieldNameList = null, IEnumerable<string> ignoreFieldNameList = null, bool attach = true)
        {
            if (attach)
            {
                // duy nhất một loại xảy rabất thư ờng của tình hình:thử gắn thêm giống nhau khóa chính của khác nhau thực thể(lưu ý này làtham chiếu dùng loại, tức là cùng một thực thể trùng lặp Attach là sẽ không cóvấn đềcủa)
                _dataContext.Set<TEntity>().Attach(entity);
            }
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == "CreatorCode" || prop.Name == "CreateTime")
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
            return dbTransaction == null ? this.Commit() : 0;
        }

    }
}
