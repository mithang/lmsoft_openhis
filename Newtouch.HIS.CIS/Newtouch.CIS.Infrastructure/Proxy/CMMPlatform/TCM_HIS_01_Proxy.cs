using System;
using Newtouch.CIS.Compat;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// đẩyThông tin bệnh nhân
    /// </summary>
    internal class TcmHis01Proxy : GuiAnTelemedicinePlatformProxy<PushPatientRequestEntity, PushPatientResponseEntity>
    {
        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override PushPatientResponseEntity Execute(PushPatientRequestEntity requestDto)
        {
            var requestXml = requestDto.XmlSerialize();
            requestXml = requestXml.Substring(requestXml.IndexOf('>') + 1);
            string responseXml;
            try
            {
                responseXml = Newtouch.CIS.Compat.CmmStub.Throw<string>("CMM SOAP endpoint is unavailable on Linux (WCF client not ported)");
if (!responseXml.IsReturnSuccess()) throw new Exception("đẩyThông tin bệnh nhângiao diệngọithất bại");
                return new PushPatientResponseEntity
                {
                    Result = new Result
                    {
                        code = "1",
                        desc = ""
                    }
                };
            }
            catch (Exception e)
            {
                return new PushPatientResponseEntity
                {
                    Result = new Result
                    {
                        code = "0",
                        desc = e.Message + (e.InnerException == null ? "" : e.InnerException.Message)
                    }
                };
            }
        }
    }
}