using System;
using Newtouch.CIS.Compat;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_02;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// đẩy thảo dược thông tin
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
if (!responseXml.IsReturnSuccess()) throw new Exception("đẩy Thông tin bệnh nhân giao diện gọi thất bại");
                return new PushMedicineInfoResponseEntity
                {
                    Result = new Result
                    {
                        code = "1",
                        desc = "Thành công"
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