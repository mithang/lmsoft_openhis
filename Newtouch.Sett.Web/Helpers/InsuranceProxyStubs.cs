using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Exceptions;

namespace Newtouch.Sett.Web.Helpers
{
    /// <summary>Explicit-error stubs for the GuiAn rural-cooperative (XNH)
    /// insurance proxies (CommonProxy / HospitalizationProxy, WCF/SOAP in
    /// Framework, no contract source in this deployment). The Framework
    /// controllers call these for S03/S04/S05 submissions; the Core port
    /// throws a Vietnamese FailedException instead of 404 or fake success,
    /// exactly like the other WCF stubs in this app.</summary>
    public class Response<T>
    {
        public bool state { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }

    public class CommonProxy
    {
        public static CommonProxy GetInstance(string orgId)
        {
            return new CommonProxy();
        }

        public object S03(object request)
        {
            throw new FailedException("Giao diện bảo hiểm hợp tác nông thôn mới (S03) chưa được chuyển đổi sang .NET Core (proxy WCF gốc không có contract nguồn).");
        }
    }

    public class HospitalizationProxy
    {
        public static HospitalizationProxy GetInstance(string orgId)
        {
            return new HospitalizationProxy();
        }

        public Response<S04ResponseDTO> S04(S04RequestDTO request)
        {
            throw new FailedException("Giao diện bảo hiểm hợp tác nông thôn mới (S04) chưa được chuyển đổi sang .NET Core (proxy WCF gốc không có contract nguồn).");
        }

        public Response<string> S05(S05RequestDTO request)
        {
            throw new FailedException("Giao diện bảo hiểm hợp tác nông thôn mới (S05) chưa được chuyển đổi sang .NET Core (proxy WCF gốc không có contract nguồn).");
        }
    }
}
