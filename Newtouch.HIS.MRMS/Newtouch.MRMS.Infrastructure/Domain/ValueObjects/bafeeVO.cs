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
    public class bafeeVO
    {
        public string id1 { get; set; }
        public string id2 { get; set; }
        public string id3 { get; set; }
        /// <summary>
        /// Mã nhóm lớn cấp 1
        /// </summary>
        public string code1 { get; set; }
        /// <summary>
        /// Tên nhóm lớn cấp 1
        /// </summary>
        public string name1 { get; set; }
        /// <summary>
        /// Mã nhóm lớn cấp 2
        /// </summary>
        public string code2 { get; set; }
        /// <summary>
        /// Tên nhóm lớn cấp 2
        /// </summary>
        public string name2 { get; set; }
        /// <summary>
        /// Mã nhóm lớn cấp 3
        /// </summary>
        public string code3 { get; set; }
        /// <summary>
        /// Tên nhóm lớn cấp 3
        /// </summary>
        public string name3 { get; set; }
        /// <summary>
        /// Tên nhóm lớn
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Pinyin nhóm lớn
        /// </summary>
        public string py { get; set; }

        public string ShortCode { get; set; }
        public int Lev { get; set; }
        public string ParentCode { get; set; }
        public string code { get; set; }
        public int px { get; set; }
    }
}
