using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    /// <summary>
    /// Đối tượng sắp xếp phẫu thuật
    /// </summary>
    public class OperatPatVO
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public Int16 nl { get; set; }
        public string py { get; set; }
    }
}
