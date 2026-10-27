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
    /// 麻醉方式
    /// </summary>
    public class SysAnesListDto
    {
        public string OrganizeId { get; set; }
        /// <summary>
        /// 麻醉编码
        /// </summary>
        public string AnesCode { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string AnesName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        public string Aneszjm { get; set; }
    }
}
