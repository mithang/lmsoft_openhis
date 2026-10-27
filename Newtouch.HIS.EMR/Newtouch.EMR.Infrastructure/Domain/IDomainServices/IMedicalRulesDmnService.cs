using Newtouch.EMR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.EMR.Domain.IRepository;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IMedicalRulesDmnService
    {
        void SubmitForm(RulesEntityVo entity);
    }
}
