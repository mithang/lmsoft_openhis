using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    /// <summary>
    /// Thông tin chi phí
    /// </summary>
    public class jgssCostVO
    {
        public string Id { get; set; }
        public string cblb { get; set; }
        public string kmcode { get; set; }
        public string lbmc { get; set; }
        public string kmmc { get; set; }
        public decimal je { get; set; }
    }

    /// <summary>
    /// Thực thu của cơ sởandGRSThực thu
    /// </summary>
    public class jgssandgrsssVO {
        public decimal jgss { get; set; }
        public decimal grsss { get; set; }
    }
}
