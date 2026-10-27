using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class S25ResponseDTO { }
    public class S24ResponseDTO { }
    public class CqybGjbmInfoVo { public string zjh { get; set; } public string gjybdm { get; set; } public string gh { get; set; } public string name { get; set; } public string ks { get; set; } public string ybksbm { get; set; } public string ksmc { get; set; } public string jzid { get; set; } }
    public class BespeakRegisterParamDTO { public string blh { get; set; } public string ksCode { get; set; } public int mzlx { get; set; } public string ysgh { get; set; } public string zjh { get; set; } public int? zjlx { get; set; } }
    public class PrescriptionChargeStatusUpdateRequestDTO { public string cfh { get; set; } }
    public class CheckApplicationfromDTO { public object Patient { get; set; } }

    public class BespeakRegisterResult { public bool IsSucceed { get; set; } public object Data { get; set; } public string ResultMsg { get; set; } }

    /// <summary>Bespeak (appointment) registration process: STUB — the original process
    /// calls external appointment services; it returns an explicit failure, never fake success.</summary>
    public class MzBespeakRegisterProcess
    {
        private readonly BespeakRegisterParamDTO _param;
        public MzBespeakRegisterProcess(BespeakRegisterParamDTO param) { _param = param; }
        public BespeakRegisterResult Process()
        {
            return new BespeakRegisterResult { IsSucceed = false, ResultMsg = "Đăng ký hẹn khám chưa được triển khai trong bản .NET Core này." };
        }
    }

    public static class APIRequestHelper
    {
        // SUCCESS value is a stub-phase placeholder: the original lives in the
        // binary-only Newtouch.Common.Web assembly (his-dll-common) and the
        // stub helpers below never return a non-null response, so the value has
        // no runtime effect in phase 1. Confirm against the real CIS/PDS
        // contract at merge time.
        public static class ResponseResultCode { public const string SUCCESS = "200"; }
        public class DefaultResponse { public string code { get; set; } public string sub_code { get; set; } }
    }

    /// <summary>Stubs for the CIS/PDS site API helpers (external hosts 8015/8016 absent here).
    /// Request returns default(T) (null for classes); callers null-check the response.</summary>
    public static class SiteCISAPIHelper
    {
        public static T Request<T>(string url, object req) { return default(T); }
    }

    public static class SitePDSAPIHelper
    {
        public static string GetToken() { return ""; }
        public static T Request<T>(string url, object req, bool autoAppendToken = true, object httpContext = null) { return default(T); }
    }

    public class AppLogger
    {
        public static void Info(string message) { }
        public static void Info(string format, params object[] args) { }
        public static AppLogger Instance { get; } = new AppLogger();
        public void Error(string message, Exception ex) { }
    }

    public static class LogCore
    {
        public static void Info(string message, object data = null) { }
        public static void Error(string message, Exception ex) { }
    }

    // Stub for Newtouch.Core.NLogger (binary-only in the Framework build).
    public class NLogger
    {
        public void Info(string message) { }
    }

}