using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatAccApp : AppBase, ISysPatAccApp
    {
        private readonly ISysPatientAccountRepo _sysPatAccRepository;

        /// <summary>
        /// theo số nhập viện lấy tài khoản thông tin
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public SysPatientAccountEntity GetAccInfoByZHY(string zyh)
        {
            return _sysPatAccRepository.GetAccInfoByZHY(zyh, this.OrganizeId);
        }
    }
}
