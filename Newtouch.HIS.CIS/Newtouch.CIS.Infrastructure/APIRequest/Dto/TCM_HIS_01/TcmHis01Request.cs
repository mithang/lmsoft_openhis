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
        /// Khám bệnhID
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Tổ chứcid
        /// </summary>
        public string organizeId { get; set; }
    }
}