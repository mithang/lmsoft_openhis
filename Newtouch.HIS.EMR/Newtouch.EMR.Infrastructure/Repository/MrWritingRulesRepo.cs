using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    public class MrWritingRulesRepo : RepositoryBase<MrWritingRulesEntity>, IMrWritingRulesRepo
    {


        public void DeleteForm(string Id, string orgId)
        {
            this.Delete(a => a.Id == Id && a.OrganizeId == orgId);
        }
    }
}
