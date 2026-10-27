using System.Net;
using Newtouch.CIS.Compat;
using System.Text;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.RCIntegrateRequest;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// tậpthànhxatrìnhkhámtrịHệ thống
    /// </summary>
    internal class RCIntegrateRequestProxy : GuiAnTelemedicinePlatformProxy<RCIntegrateRequestEntity, string>
    {
        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override string Execute(RCIntegrateRequestEntity requestDto)
        {
            var rCIntegrateRequest = Newtouch.CIS.Compat.AppConfig.Get("RCIntegrateRequestURL");
            var requestXml = requestDto.XmlSerialize();
            var uri = new StringBuilder(rCIntegrateRequest)
                .Append("orgCode=").Append(requestDto.openid)
                .Append("&userName=").Append(requestDto.userName)
                .Append("&patientId=").Append(requestDto.patientId)
                .Append("&series=").Append(requestDto.series)
                .Append("&patientName=").Append(requestDto.patientName)
                .Append("&mobile=").Append(requestDto.mobile)
                .Append("&certid=").Append(requestDto.certid)
                .Append("&gender=").Append(requestDto.gender)
                .Append("&birthday=").Append(requestDto.birthday)
                .Append("&patientType=").Append(requestDto.patientType)
                .Append("&cardId=").Append(requestDto.cardId)
                .Append("&organCode=").Append(requestDto.organCode)
                .Append("&m=").Append(requestDto.m)
                .Append("&clz=").Append(requestDto.clz);
            return Monitoring.RequestMonitor(_ => HttpMethods.HttpGet(uri.ToString()), requestXml, "RCIntegrateRequestProxy-Execute");
        }
    }
}