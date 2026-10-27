using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    /// <summary>
    /// Chuyển tài liệu bệnh án thành mẫu
    /// </summary>
    public class BlConvertToTemplateVO
    {
        public int? mbqx { get; set; }
        public string blId { get; set; }
        //public string bllxId { get; set; }
        public string blxtmc { get; set; }
        public string bllx { get; set; }

        /// <summary>
        /// Đường dẫn tệp bệnh án
        /// </summary>
        public string blxtml { get; set; }
        public int Isempty { get; set; }
    }
}
