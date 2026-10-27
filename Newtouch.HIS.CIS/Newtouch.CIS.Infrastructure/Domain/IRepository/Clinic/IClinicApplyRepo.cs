using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Clinic;
using Newtouch.CIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository.Clinic
{
    public interface IClinicApplyRepo
    {
        /// <summary>
        /// Cập nhậtTrạng thái đăng ký
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sqzt"></param>
        void Updatesqzt(string Id, int sqzt, string orgId);
        ClinicApplyInfoEntity GetYczl(string Id, string orgId);
    }
}
