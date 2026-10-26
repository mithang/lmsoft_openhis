using System;
using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Dto.TCM_HIS_01
{
    /// <summary>
    /// TcmHis01 Request
    /// </summary>
    [Serializable]
    public class TcmHis01Request : RequestBase
    {
        /// <summary>
        /// ID khám bệnh
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// id tổ chức
        /// </summary>
        public string organizeId { get; set; }
    }
}