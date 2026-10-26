using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.Common.Web;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    public class APIOutputDto
    {
        public int code { get; set; }

        /// <summary>Mô tả mã trả về của gateway</summary>
        public string msg { get; set; }
        public int sub_code { get; set; }
        public object data { get; set; }
    }
}
