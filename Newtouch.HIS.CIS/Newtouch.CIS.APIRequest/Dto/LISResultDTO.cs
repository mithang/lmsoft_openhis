using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.CIS.APIRequest.Dto
{
    public class LISResultDTO : RequestBase
    {

            public string type { get; set; }
            /// <summary>
            /// Chuỗi kết nối cơ sở dữ liệu LIS
            /// </summary>
            public string Ado_lis { get; set; }
            /// <summary>
            /// ID bệnh nhân LIS
            /// </summary>
            public string brxx_id { get; set; }
            public string brxx_tmh { get; set; }
            public string zydj_id { get; set; }
        
    }
}
