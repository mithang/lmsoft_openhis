using System;

namespace Newtouch.HIS.API.Common
{
    /// <summary>
    /// Minimal stand-in for the binary Newtouch.HIS.API.Common RequestBase /
    /// JSONRequestBase (API gateway request envelopes). CIS DTOs only inherit
    /// from these; no base members are used by the ported code.
    /// </summary>
    public class RequestBase
    {
    }

    public class JSONRequestBase : RequestBase
    {
    }
}

namespace Newtouch.Common.Web
{
    /// <summary>
    /// Minimal stand-in for the binary Newtouch.Common.Web.APIRequestHelper.
    /// Only the ResponseResultCode enum is used by the ported DTO layer.
    /// </summary>
    public static class APIRequestHelper
    {
        public enum ResponseResultCode
        {
            SUCCESS = 0,
            FAIL = 1,
            ERROR = 2
        }

        /// <summary>
        /// Cross-module API envelope (status is the string "Success" /
        /// "Failure" in CIS call sites; errorCode carries the message).
        /// </summary>
        public class DefaultResponse
    {
        public ResponseResultCode code { get; set; }
        public string msg { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public string status { get; set; }
        public string errorCode { get; set; }
        public object data { get; set; }
    }
    public class DefaultResponse<T>
    {
        public ResponseResultCode code { get; set; }
        public string msg { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public string status { get; set; }
        public string errorCode { get; set; }
        public T data { get; set; }
    }
    }
}

namespace newtouchyibao.Models
{
    /// <summary>
    /// Minimal stand-ins for the binary newtouchyibao.dll insurance-review
    /// DTOs (Sqtx = 申请). Members are exactly those assigned by
    /// MedicalRecordController.getXmCfJson / getYpCfJson. The real yibao
    /// service contract (field meanings beyond these assignments) is not
    /// available on Linux, so the insurance-review submission path is a
    /// stub until the vendor contract is confirmed.
    /// </summary>
    public class SqtxCFXM
    {
        public string YZID { get; set; }
        public int CZLX { get; set; }
        public DateTime FSSJ { get; set; }
        public DateTime KSSYSJ { get; set; }
        public string YYXMDM { get; set; }
        public string YYXMMC { get; set; }
        public string YBXMDM { get; set; }
        public string DJ { get; set; }
        public string JJJG { get; set; }
        public string JJDW { get; set; }
        public string JJSL { get; set; }
        public string JCBWBM { get; set; }
        public string BZ_XY { get; set; }
        public string SYSM { get; set; }
        public string BZ_ZT { get; set; }
        public string BZ_ZF { get; set; }
    }

    public class SqtxCFYP
    {
        public string YZID { get; set; }
        public int CZLX { get; set; }
        public DateTime FSSJ { get; set; }
        public DateTime KSSYSJ { get; set; }
        public string YYXMDM { get; set; }
        public string YYXMMC { get; set; }
        public string YBXMDM { get; set; }
        public string YYTJ { get; set; }
        public string FYYQ { get; set; }
        public string FYYQSM { get; set; }
        public string JL { get; set; }
        public string DJ { get; set; }
        public string DW { get; set; }
        public string JJDW { get; set; }
        public string JJSL { get; set; }
        public string YYTS { get; set; }
        public string GYPC { get; set; }
        public string GYMD { get; set; }
        public string ZHB { get; set; }
        public string BZ_FF { get; set; }
        public string BZ_MJ { get; set; }
        public string BZ_PS { get; set; }
        public string BZ_XY { get; set; }
        public string BZ_ZF { get; set; }
    }
}
