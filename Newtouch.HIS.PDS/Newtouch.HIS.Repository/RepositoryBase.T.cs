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

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="databaseFactory"></param>
        public RepositoryBase(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        { }

        /// <summary>
        /// Cập nhật một số trường
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dstnFieldNameList">Cột đích</param>
        /// <param name="ignoreFieldNameList">Cột loại trừ</param>
        /// <param name="attach">Có cần chuyển hay khôngEntityĐính kèm ngữ cảnh (nếu cái nàyEntityLà từdbcontextĐã tra ra，false）</param>
        /// <returns></returns>
        public override int Update(TEntity entity, IEnumerable<string> dstnFieldNameList = null, IEnumerable<string> ignoreFieldNameList = null, bool attach = true)
        {
            if (attach)
            {
                //Trường hợp duy nhất xảy ra ngoại lệ: cố gắng đính kèm thực thể khác có cùng khóa chính (chú ý đây là kiểu tham chiếu, tức là cùng một thực thể lặp lạiAttachSẽ không có vấn đề）
                _dataContext.Set<TEntity>().Attach(entity);
            }
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
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
