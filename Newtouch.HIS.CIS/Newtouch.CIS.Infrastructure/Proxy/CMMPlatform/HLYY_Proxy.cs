using Newtouch.CIS.Compat;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.HLYYRequest;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// Ported HLYY (prescription-review engine) proxy.
    /// STUB: the original used the WCF generated HisApiClient (Connected
    /// Service HisApiService, SOAP) which cannot run on Linux. Every method
    /// throws a FailedException with a clear message; callers in
    /// MedicineController / MedicalRecordController surface it as an error
    /// result instead of silently returning wrong review data.
    /// To restore: regenerate the client with dotnet-svcutil from the
    /// vendor WSDL, or replace with HttpClient+XML against the same endpoint.
    /// </summary>
    public class HlyyProxy
    {
        public HlyyProxy()
        {
        }

        /// <summary>
        /// Đơn thuốcPhê duyệtTrạng tháiTra cứu
        /// </summary>
        public string GetEngDataStatus(string presNo)
        {
            throw new FailedException("HLYY prescription-review SOAP service (HisApiService) is unavailable on Linux (WCF client not ported)");
        }

        /// <summary>
        /// Đơn thuốcPhê duyệtTrạng tháiTra cứu
        /// </summary>
        public EngineRes engine(EngineReq engineReq)
        {
            throw new FailedException("HLYY prescription-review SOAP service (HisApiService) is unavailable on Linux (WCF client not ported)");
        }

        /// <summary>
        /// Mô tảsách
        /// </summary>
        public string GetHisSmsJson(string ypmcId)
        {
            throw new FailedException("HLYY prescription-review SOAP service (HisApiService) is unavailable on Linux (WCF client not ported)");
        }
    }
}
