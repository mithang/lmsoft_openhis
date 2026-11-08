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
        // hái cần:
        //     Mô tả mã trả về của cổng (gateway)
        public string msg { get; set; }
        //
        // hái cần:
        //     nghiệp vụ Quay lại mã
        public string sub_code { get; set; }
        //
        // hái cần:
        //     nghiệp vụ Quay lại mã Mô tả
        public string sub_msg { get; set; }
        //
        // hái cần:
        //     hưởng ứng Dữ liệu(ResponseResultCode.SUCCESS khi của Quay lại Dữ liệu)
        public object data { get; set; }
        public int rowcount { get; set; }
    }
}
