using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.SelfService
{
	public class queryRegistDeptMarkReqDTO
	{
		/// <summary>
		/// 看诊thời gian
		/// </summary>
		public string VisitDate { get; set; }
		/// <summary>
		/// Mã tổ chức
		/// </summary>
		public string OrganizeId { get; set; }
    }
}
