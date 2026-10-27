using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    /// <summary>
    /// phiếucứnội dung
    /// </summary>
    public class BYDjInfoSubmit
    {
        /// <summary>
        /// Nhập khoKhoa phòng
        /// </summary>
        public string rkbm { get; set; }
        public string yfbm { get; set; }
        public string ksbm { get; set; }
        public string djh { get; set; }
        public string bqbm { get; set; }
        public string shzt { get; set; }
    }
}
