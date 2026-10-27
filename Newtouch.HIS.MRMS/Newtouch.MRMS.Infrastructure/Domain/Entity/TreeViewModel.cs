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

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
	public class TreeViewModel
	{

	
		//
		// Tóm tắt:
		//     Trường mở rộng4
		public string Ex4 { get; set; }
		//
		// Tóm tắt:
		//     Trường mở rộng3
		public string Ex3 { get; set; }
		//
		// Tóm tắt:
		//     Trường mở rộng2
		public string Ex2 { get; set; }
		//
		// Tóm tắt:
		//     Trường mở rộng1
		public string Ex1 { get; set; }
		//
		// Tóm tắt:
		//     Mã
		public string Code { get; set; }
		//
		// Tóm tắt:
		//     title
		public string title { get; set; }
		//
		public string img { get; set; }
		//
		// Tóm tắt:
		//     Trường mở rộng5
		public string Ex5 { get; set; }
		//
		// Tóm tắt:
		//     Có cấp dưới không
		public bool hasChildren { get; set; }
		//
		// Tóm tắt:
		//     Đã tải nút con chưa
		public bool complete { get; set; }
		//
		// Tóm tắt:
		//     Có hiển thị check không
		public bool showcheck { get; set; }
		//
		// Tóm tắt:
		//     Trạng thái đã chọn
		public int? checkstate { get; set; }
		//
		// Tóm tắt:
		//     value
		public string value { get; set; }
		//
		// Tóm tắt:
		//     Văn bản hiển thị
		public string text { get; set; }
		//
		// Tóm tắt:
		//     parentId id cấp trên tương ứng của cây
		public string parentId { get; set; }
		//
		// Tóm tắt:
		//     id
		public string id { get; set; }
		//
		// Tóm tắt:
		//     Có mở rộng không(if has child)
		public bool isexpand { get; set; }
		//
		// Tóm tắt:
		//     Trường mở rộng6
		public string Ex6 { get; set; }
	}
}
