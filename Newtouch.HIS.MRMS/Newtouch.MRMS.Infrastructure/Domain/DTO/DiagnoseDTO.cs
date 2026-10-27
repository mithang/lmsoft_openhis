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

namespace Newtouch.MR.ManageSystem.Domain.DTO
{
    public class DiagnoseDTO
    {
        public string zdCode { get; set; }
        public string zdName { get; set; }
        public string orgId { get; set; }
        public string icd10 { get; set; }
        public string zdlx { get; set; }
    }
}
