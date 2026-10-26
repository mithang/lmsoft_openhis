/*******************************************************************************
 * Copyright © 2016 Newtouch Giữ bản quyền
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    public class FilterIPRepository : RepositoryBase<FilterIPEntity>, IFilterIPRepository
    {
        public FilterIPRepository(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}
