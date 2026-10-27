
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
    public class AllergyRepo: RepositoryBase<AllergyEntity>, IAllergyRepo
    {
        public AllergyRepo()
            : base()
        {
        }
    }
}
