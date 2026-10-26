/*******************************************************************************
 * Copyright © 2016 Newtouch Giữ bản quyền
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    public class UserLogOnRepository : RepositoryBase<UserLogOnEntity>, IUserLogOnRepository
    {
        public UserLogOnRepository(INewtouchDatabaseFactory databaseFactory)
            :base(databaseFactory)
        {

        }

    }
}
