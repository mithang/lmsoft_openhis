using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Repository;
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
        public GzybCodeContrastRepo()
            
        {
        }

        /// <summary>
        /// 判断是否存在该数据
        /// </summary>
        /// <param name="aaa100">代码类别</param>
        /// <param name="aaa102">代码值</param>
        /// <returns></returns>
        public bool Exist(string aaa100, string aaa102)
        {
            return this.IQueryable().Any(a => a.aaa100 == aaa100 && a.aaa102 == aaa102 && a.zt == "1");
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="entity"></param>
        public bool InsertInfo(GzybCodeContrastEntity entity)
        {
            entity.Create();

            return this.Insert(entity) > 0;
        }

    }
}
