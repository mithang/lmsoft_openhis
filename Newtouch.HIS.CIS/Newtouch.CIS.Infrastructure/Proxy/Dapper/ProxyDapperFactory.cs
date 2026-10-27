using Newtouch.CIS.Compat;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01;
using Newtouch.CIS.Proxy.Dapper.CMMPlatform;

namespace Newtouch.CIS.Proxy.Dapper
{
    /// <summary>
    /// thíchphốiNhà máy
    /// </summary>
    public class ProxyDapperFactory
    {
        /// <summary>
        /// đẩyThông tin bệnh nhân
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public static IProxyDapperExecutable CreateTcmhis01ProxyDapper(Patient patient)
        {
            return new TcmHis01(patient);
        }
    }
}