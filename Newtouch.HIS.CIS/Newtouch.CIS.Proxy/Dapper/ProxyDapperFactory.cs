using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01;
using Newtouch.CIS.Proxy.Dapper.CMMPlatform;

namespace Newtouch.CIS.Proxy.Dapper
{
    /// <summary>
    /// Factory tương thích
    /// </summary>
    public class ProxyDapperFactory
    {
        /// <summary>
        /// Đẩy thông tin bệnh nhân
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public static IProxyDapperExecutable CreateTcmhis01ProxyDapper(Patient patient)
        {
            return new TcmHis01(patient);
        }
    }
}