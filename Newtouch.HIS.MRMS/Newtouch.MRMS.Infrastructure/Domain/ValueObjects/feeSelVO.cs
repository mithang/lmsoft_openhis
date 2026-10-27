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

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class feeSelVO
    {
        /// <summary>
        /// Mã nhóm thu phí lớn
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// Tên nhóm thu phí lớn
        /// </summary>
        public string name { get; set; }
    }
}
