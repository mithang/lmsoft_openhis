using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// nhắclấyđiện tửHồ sơ
    /// </summary>
    internal class TcmHis07Proxy : GuiAnTelemedicinePlatformProxy<ReceiveRequestEntity, PullRecordRequestEntity>
    {
        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override PullRecordRequestEntity Execute(ReceiveRequestEntity requestDto)
        {
            var requestXml = "";
            try
            {
                requestXml = requestDto.XmlSerialize();
                var responseXml = Newtouch.CIS.Compat.CmmStub.Throw<string>("CMM SOAP endpoint is unavailable on Linux (WCF client not ported)");
return responseXml.XmlDeSerialize<PullRecordRequestEntity>() as PullRecordRequestEntity;
            }
            catch (Exception e)
            {
                LogCore.Error("TcmHis07Proxy Execute error", e, string.Format("requestXml:{0}", requestXml));
                throw ;
            }
        }

    }
}