using System.Net;
using Newtouch.CIS.Compat;
using System.Text;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.EMRRequest;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// tậpthànhđiện tửHồ sơ
    /// </summary>
    internal class EMRRequestProxy : GuiAnTelemedicinePlatformProxy<EMRRequestEntity, string>
    {
        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override string Execute(EMRRequestEntity requestDto)
        {
            var requestXml = requestDto.XmlSerialize();
            var uri = new StringBuilder(IntegratedModuleUrl).Append(requestDto.resource).Append("&doctorId=")
                .Append(requestDto.doctorId).Append("&patientId=").Append(requestDto.patientId);
            return Monitoring.RequestMonitor(_ => HttpMethods.HttpGet(uri.ToString()), requestXml, "EMRRequestProxy-Execute");
        }
    }
}