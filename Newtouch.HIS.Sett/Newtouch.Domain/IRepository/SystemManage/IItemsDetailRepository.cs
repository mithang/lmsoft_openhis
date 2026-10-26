/*******************************************************************************
 * Copyright © 2016 Newtouch Bảo lưu bản quyền
 * Author: Newtouch
 * Description: 
*********************************************************************************/

using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface IItemsDetailRepository : IRepositoryBase<ItemsDetailEntity>
    {
        List<ItemsDetailEntity> GetItemList(string enCode);
    }
}
