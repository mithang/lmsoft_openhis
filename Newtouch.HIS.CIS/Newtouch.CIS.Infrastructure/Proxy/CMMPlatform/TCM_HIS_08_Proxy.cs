using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using ReceiveRequestEntity = Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08.ReceiveRequestEntity;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// nhắclấyĐơn thuốc
    /// </summary>
    internal class TcmHis08Proxy : GuiAnTelemedicinePlatformProxy<ReceiveRequestEntity, PullPrescriptionRequestEntity>
    {
        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override PullPrescriptionRequestEntity Execute(ReceiveRequestEntity requestDto)
        {
            var requestXml = "";
            try
            {
                requestXml = requestDto.XmlSerialize();
                var responseXml = Newtouch.CIS.Compat.CmmStub.Throw<string>("CMM SOAP endpoint is unavailable on Linux (WCF client not ported)");
var resultobj = responseXml.XmlDeSerialize<PullPrescriptionRequestEntity>();
                if (resultobj != null) return resultobj;
                var t = responseXml.XmlDeSerialize<Result>();
                if (t != null && t.code == "0") throw new Exception(t.desc);

                return null;
            }
            catch (Exception e)
            {
                LogCore.Error("TcmHis08Proxy Execute error", e, string.Format("requestXml:{0}", requestXml));
                throw;
            }
        }
    }
}