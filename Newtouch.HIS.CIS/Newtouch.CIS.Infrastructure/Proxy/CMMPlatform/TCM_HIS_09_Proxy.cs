using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// nhắclấyChẩn đoánthông tin
    /// </summary>
    public class TcmHis09Proxy : GuiAnTelemedicinePlatformProxy<ReceiveRequestEntity, PullDiagInfoRequestEntity>
    {
        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override PullDiagInfoRequestEntity Execute(ReceiveRequestEntity requestDto)
        {
            var requestXml = "";
            try
            {
                requestXml = requestDto.XmlSerialize();
                var responseXml = Newtouch.CIS.Compat.CmmStub.Throw<string>("CMM SOAP endpoint is unavailable on Linux (WCF client not ported)");
return responseXml.XmlDeSerialize<PullDiagInfoRequestEntity>() as PullDiagInfoRequestEntity;
            }
            catch (Exception e)
            {
                LogCore.Error("TcmHis08Proxy Execute error", e, string.Format("requestXml:{0}", requestXml));
                throw;
            }
        }
    }
}