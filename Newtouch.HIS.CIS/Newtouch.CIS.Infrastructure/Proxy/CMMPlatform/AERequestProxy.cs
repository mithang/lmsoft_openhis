using System.Net;
using Newtouch.CIS.Compat;
using System.Text;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.AERequest;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// biệnchứngluậnđiều trị
    /// </summary>
    internal class AERequestProxy : GuiAnTelemedicinePlatformProxy<AERequestEntity, string>
    {
        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override string Execute(AERequestEntity requestDto)
        {
            var requestXml = requestDto.XmlSerialize();
            var uri = new StringBuilder(IntegratedModuleUrl).Append(requestDto.resource)
                .Append("&orgCode=").Append(requestDto.orgCode)
                .Append("&chisZggh=").Append(requestDto.chisZggh)
                .Append("&chisEmpName=").Append(requestDto.chisEmpName)
                .Append("&clinicId=").Append(requestDto.clinicId)
                .Append("&brxm=").Append(requestDto.brxm)
                .Append("&sex=").Append(requestDto.sex)
                .Append("&patientid=").Append(requestDto.patientid)
                .Append("&returnHisUrl=").Append(requestDto.returnHisUrl);
            return Monitoring.RequestMonitor(_ => HttpMethods.HttpGet(uri.ToString()), requestXml, "AERequestProxy-Execute");
        }
    }
}