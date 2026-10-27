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

namespace Newtouch.MR.ManageSystem.Domain.DTO.OutputDto
{
    /// <summary>
    /// Phương pháp gây mê
    /// </summary>
    public class SysAnesListDto
    {
        public string OrganizeId { get; set; }
        /// <summary>
        /// Mã gây mê
        /// </summary>
        public string AnesCode { get; set; }
        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        public string AnesName { get; set; }
        /// <summary>
        /// Mã gợi nhớ
        /// </summary>
        public string Aneszjm { get; set; }
    }
}
