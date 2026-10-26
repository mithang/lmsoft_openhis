using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.Domain.DTO.InterfaceSync
{
    public class ResponseBase
    {
        public ResponseResultCode code { get; set; }
        //
        // Tóm tắt:
        //     Mô tả mã trả về của cổng
        public string msg { get; set; }
        //
        // Tóm tắt:
        //     Mã trả về nghiệp vụ
        public string sub_code { get; set; }
        //
        // Tóm tắt:
        //     Mô tả mã trả về nghiệp vụ
        public string sub_msg { get; set; }
        //
        // Tóm tắt:
        //     Dữ liệu phản hồi(ResponseResultCode.SUCCESSDữ liệu trả về khi)
        public object data { get; set; }
        public int rowcount { get; set; }
    }
}
