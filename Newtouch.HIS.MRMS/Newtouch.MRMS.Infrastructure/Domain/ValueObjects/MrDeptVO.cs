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
    public class MrDeptVO
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string organizeId { get; set; }
        /// <summary>
        /// Khoa bệnh ánID
        /// </summary>
        public string baksId { get; set; }
        /// <summary>
        /// Tên khoa bệnh án
        /// </summary>
        public string baksName { get; set; }
        /// <summary>
        /// Số hiệu khoa HIS
        /// </summary>
        public string hisdept { get; set; }
        /// <summary>
        /// Tên khoa HIS
        /// </summary>
        public string hisdeptname { get; set; }
    }
}
