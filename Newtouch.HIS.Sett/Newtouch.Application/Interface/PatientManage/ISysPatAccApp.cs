using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatAccApp
    {
        /// <summary>
        /// theo số nhập viện lấy tài khoản thông tin
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        SysPatientAccountEntity GetAccInfoByZHY(string zyh);
    }
}
