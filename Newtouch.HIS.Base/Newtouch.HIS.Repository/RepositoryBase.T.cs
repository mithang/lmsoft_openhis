using Newtouch.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Triển khai kho lưu trữ
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class RepositoryBase<TEntity> : Newtouch.Infrastructure.EF.RepositoryBase<TEntity>
        where TEntity : class, new()
    {
        public RepositoryBase(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Cập nhật một số trường
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dstnFieldNameList">Cột đích</param>
        /// <param name="ignoreFieldNameList">Cột loại trừ</param>
        /// <param name="attach">Có cần gắn Entity vào context hay không (nếu Entity này được tra cứu từ dbcontext thì là false)</param>
        /// <returns></returns>
        public override int Update(TEntity entity, IEnumerable<string> dstnFieldNameList = null, IEnumerable<string> ignoreFieldNameList = null, bool attach = true)
        {
            if (attach)
            {
                //Trường hợp duy nhất xảy ra ngoại lệ: cố gắng gắn các thực thể khác nhau có cùng khóa chính (lưu ý đây là kiểu tham chiếu, tức là việc Attach lặp lại cùng một thực thể sẽ không có vấn đề)
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
