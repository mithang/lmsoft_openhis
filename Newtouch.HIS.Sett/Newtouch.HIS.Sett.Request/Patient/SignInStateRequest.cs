using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 签 đ ến trạng thái
    /// </summary>
    public class SignInStateRequest : RequestBase
    {
        public string mzh { get; set; }
        public string calledstu { get; set; }
        public string yhcode { get; set; }
        public string orgId { get; set; }
    }
}