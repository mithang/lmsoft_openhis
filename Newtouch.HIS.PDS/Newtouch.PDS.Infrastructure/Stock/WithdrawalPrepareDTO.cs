using FrameworkBase.MultiOrg.DmnService;
using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// Nội dung chứng từ
    /// </summary>
    public class WithdrawalPrepareDTO : RequestBase
    {
        public string OrganizeId { get; set; }
        public string Djh { get; set; }
        public string yhgh { get; set; }
        public string shzt { get; set; }
    }
}
