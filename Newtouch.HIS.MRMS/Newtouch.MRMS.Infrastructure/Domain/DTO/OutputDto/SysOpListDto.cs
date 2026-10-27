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
    /// 系统手术字典
    /// </summary>
    public class SysOpListDto
    {
        /// <summary>
        /// 手术代码
        /// </summary>
        public string ssdm { get; set; }
        /// <summary>
        /// 手术名称
        /// </summary>
        public string ssmc { get; set; }
        /// <summary>
        /// 手术助记码
        /// </summary>
        public string zjm { get; set; }
        /// <summary>
        /// 手术级别
        /// </summary>
        public string ssjb { get; set; }
    }
}
