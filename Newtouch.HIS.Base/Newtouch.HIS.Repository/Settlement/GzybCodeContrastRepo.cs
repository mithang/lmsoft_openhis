using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class GzybCodeContrastRepo : RepositoryBase<GzybCodeContrastEntity>, IGzybCodeContrastRepo
    {
        public GzybCodeContrastRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Xác định dữ liệu đó có tồn tại hay không
        /// </summary>
        /// <param name="aaa100">Danh mục mã</param>
        /// <param name="aaa102">Giá trị mã</param>
        /// <returns></returns>
        public bool Exist(string aaa100, string aaa102)
        {
            return this.IQueryable().Any(a => a.aaa100 == aaa100 && a.aaa102 == aaa102 && a.zt == "1");
        }

        /// <summary>
        /// Ghi dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        public bool InsertInfo(GzybCodeContrastEntity entity)
        {
            entity.Create();

            return this.Insert(entity) > 0;
        }

    }
}
