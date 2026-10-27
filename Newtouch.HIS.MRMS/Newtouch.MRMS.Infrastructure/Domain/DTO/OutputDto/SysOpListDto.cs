using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Domain.DTO;

namespace Newtouch.MR.ManageSystem.Domain.DTO.OutputDto
{
    /// <summary>
    /// Từ điển phẫu thuật hệ thống
    /// </summary>
    public class SysOpListDto
    {
        /// <summary>
        /// Phẫu thuậtMã
        /// </summary>
        public string ssdm { get; set; }
        /// <summary>
        /// Tên phẫu thuật
        /// </summary>
        public string ssmc { get; set; }
        /// <summary>
        /// Mã gợi nhớ phẫu thuật
        /// </summary>
        public string zjm { get; set; }
        /// <summary>
        /// Cấp độ phẫu thuật
        /// </summary>
        public string ssjb { get; set; }
    }
}
