using System.Net;
using Newtouch.CIS.Compat;
using System.Text;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.HEALRequest;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// tậpthànhđiều trịchưabệnhHệ thống
    /// </summary>
    internal class HEALRequestProxy : GuiAnTelemedicinePlatformProxy<HEALRequestEntity, string>
    {
        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override string Execute(HEALRequestEntity requestDto)
        {
            var requestXml = requestDto.XmlSerialize();
            var uri = new StringBuilder(IntegratedModuleUrl).Append(requestDto.resource)
                .Append("&CertificatesType=").Append(requestDto.CertificatesType)
                .Append("&CertificatesNumber=").Append(requestDto.CertificatesNumber);
            return Monitoring.RequestMonitor(_ => HttpMethods.HttpGet(uri.ToString()), requestXml, "HEALRequestProxy-Execute");
        }
    }
}