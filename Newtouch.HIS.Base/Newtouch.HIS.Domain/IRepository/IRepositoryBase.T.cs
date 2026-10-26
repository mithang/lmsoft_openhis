using Newtouch.Common;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Interface kho lưu trữ
    /// </summary>
    /// <typeparam name="TEntity">Loại thực thể</typeparam>
    public interface IRepositoryBase<TEntity> : Newtouch.Infrastructure.EF.IRepositoryBase<TEntity>
        where TEntity : class, new()
    {

    }
}