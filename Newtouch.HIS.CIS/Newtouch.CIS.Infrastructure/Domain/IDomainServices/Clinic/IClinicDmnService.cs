using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.ValueObjects.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices.Clinic
{
    public interface IClinicDmnService
    {
        IList<ClinicApplyInfoVO> GetClinicApplyGridJson(Pagination pagination, string orgId, string xm, string zjh, string kssj, string jssj, string ksCode, string ysgh, int sqzt,string userCode);

        OutBookScheduleVO getClinicScheduleId(string orgId, string czks);
        //LấyxatrìnhkhámtrịXemChi tiếtThông tin bệnh nhân
        ClinicPatVO getClinicPatInfo(string orgId, string Id);
    }
}
