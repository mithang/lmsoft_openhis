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
        // háicần:
        //     Mô tả mã trả về của cổng (gateway)
        public string msg { get; set; }
        //
        // háicần:
        //     nghiệp vụQuay lạimã
        public string sub_code { get; set; }
        //
        // háicần:
        //     nghiệp vụQuay lạimãMô tả
        public string sub_msg { get; set; }
        //
        // háicần:
        //     hưởngứngDữ liệu（ResponseResultCode.SUCCESSkhicủaQuay lạiDữ liệu）
        public object data { get; set; }
        public int rowcount { get; set; }
    }
}
