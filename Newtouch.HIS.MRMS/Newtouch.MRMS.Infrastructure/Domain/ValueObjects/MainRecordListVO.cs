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
    public class MainRecordListVO
    {
        public string Id { get; set; }
        public string blzt { get; set; }
        public string xm { get; set; }
        public string bah { get; set; }
        public string zyh { get; set; }
        public string nl { get; set; }
        public string xb { get; set; }
        public string ks { get; set; }
        public string bq { get; set; }
        public string ch { get; set; }
        public string zd { get; set; }
        public int cyts { get; set; }
        public int bazt { get; set; }

    }
}
