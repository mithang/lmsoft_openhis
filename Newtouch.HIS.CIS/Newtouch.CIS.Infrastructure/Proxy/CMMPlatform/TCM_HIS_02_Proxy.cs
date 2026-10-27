using System;
using Newtouch.CIS.Compat;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_02;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// đẩythảo dượcthông tin
    /// </summary>
    public class TcmHis02Proxy : GuiAnTelemedicinePlatformProxy<PushMedicineInfoRequestEntity, PushMedicineInfoResponseEntity>
    {
        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override PushMedicineInfoResponseEntity Execute(PushMedicineInfoRequestEntity requestDto)
        {
            var requestXml = requestDto.XmlSerialize();
            requestXml = requestXml.Substring(requestXml.IndexOf('>') + 1);
            try
            {
                var responseXml = Newtouch.CIS.Compat.CmmStub.Throw<string>("CMM SOAP endpoint is unavailable on Linux (WCF client not ported)");
if (!responseXml.IsReturnSuccess()) throw new Exception("đẩyThông tin bệnh nhângiao diệngọithất bại");
                return new PushMedicineInfoResponseEntity
                {
                    Result = new Result
                    {
                        code = "1",
                        desc = "成功"
                    }
                };
            }
            catch (Exception e)
            {
                return new PushMedicineInfoResponseEntity
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