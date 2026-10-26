/*******************************************************************************
 * Copyright © 2016 Newtouch Bảo lưu bản quyền
 * Author: Newtouch
 * Description: 
*********************************************************************************/

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Giao diện kho
    /// </summary>
    /// <typeparam name="TEntity">Loại thực thể</typeparam>
    public interface IRepositoryBase<TEntity> : Newtouch.Infrastructure.EF.IRepositoryBase<TEntity>
        where TEntity : class, new()
    {

    }
}
