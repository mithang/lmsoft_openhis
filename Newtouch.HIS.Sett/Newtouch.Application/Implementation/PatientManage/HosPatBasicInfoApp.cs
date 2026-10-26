using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class HosPatBasicInfoApp : AppBase, IHosPatBasicInfoApp
    {
        private readonly IHospPatientBasicInfoRepo _hosPatBasicInfoRepository;

        /// <summary>
        /// HủyNhập viện
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public void CancelAdmission(int patid)
        {
            _hosPatBasicInfoRepository.CancelAdmission(patid);
        }

    }
}
