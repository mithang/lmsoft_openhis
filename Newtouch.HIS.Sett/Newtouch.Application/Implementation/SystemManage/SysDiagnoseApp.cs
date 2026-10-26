using System.Collections.Generic;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Application.SystemManage
{
    public class SysDiagnoseApp : ISysDiagnoseApp
    {
        private ISysDiagnosisRepo _xt_zdRepository { get; set; }
        public SysDiagnoseApp(ISysDiagnosisRepo xt_zdRepository)
        {
            this._xt_zdRepository = xt_zdRepository;
        }

        /// <summary>
        /// Chẩn đoán hộp thả xuống 
        /// </summary>
        /// <param name="zd"></param>
        /// <returns></returns>
        public List<ZDSelect> GetzdSelect(string zd)
         {
            return _xt_zdRepository.GetzdSelect(zd);
        }
    }
}
