using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class SysUsageVO
    {
        //
        // Tóm tắt:
        //     Id
        public int yfId { get; set; }
        //
        // Tóm tắt:
        //     Mã cách dùng
        public string yfCode { get; set; }
        //
        // Tóm tắt:
        //     Tên
        public string yfmc { get; set; }
        //
        // Tóm tắt:
        //     Cờ hiệu lực
        public string zt { get; set; }
        public string yplx { get; set; }
    }
}
