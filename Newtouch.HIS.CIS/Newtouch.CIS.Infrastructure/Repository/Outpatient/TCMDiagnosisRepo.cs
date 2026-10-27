using Newtouch.CIS.Compat;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    public class TCMDiagnosisRepo : RepositoryBase<TCMDiagnosisEntity>, ITCMDiagnosisRepo
    {
        public TCMDiagnosisRepo()
            : base()
        {
        }
    }
}
