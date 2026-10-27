using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    public class ResponseBase
    {
        /// <summary>
        /// Mã trả về của giao diện
        /// </summary>
        public string code { get; set; }
        public string message { get; set; }
        /// <summary>
        /// Dữ liệu phản hồi giao diện
        /// </summary>
        public string data { get; set; }
    }
}
