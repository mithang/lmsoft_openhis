using Newtouch.CIS.Compat;

namespace Newtouch.DomainServices.BGESBSercice
{
    /// <summary>
    /// STUB for the generated BGESB SOAP client (ECG report service, used by
    /// MedicalRecordDmnService.chakanmingxidiaoyong / xindianshenqing /
    /// DTXD_UploadData). The real client (WCF/ASMX generated code) is not
    /// available on Linux. CallBGESB returns a failure envelope without
    /// Return='1', so the existing callers return null (no data) exactly
    /// as they do for a failed remote call, instead of crashing.
    /// To restore: regenerate the client from the vendor WSDL or replace
    /// with HttpClient+XML against the BGESB endpoint.
    /// </summary>
    public class BGESB
    {
        public string Url { get; set; }

        public string CallBGESB(string method, string businessRequest, string bm, string version)
        {
            return "<BusinessResult Return='0' Message='BGESB SOAP service is unavailable on Linux (client not ported)'/>";
        }
    }
}
