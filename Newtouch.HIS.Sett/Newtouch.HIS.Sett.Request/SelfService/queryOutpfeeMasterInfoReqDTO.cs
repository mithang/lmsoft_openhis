using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.SelfService
{
	public class queryOutpfeeMasterInfoReqDTO : queryAppCardInfoReqDTO
	{
		/// <summary>
		/// Thời gian bắt đ ầu
		/// </summary>
		public string START_DATE { get; set; }
		/// <summary>
		/// Thời gian kết thúc
		/// </summary>

		public string END_DATE { get; set; }
	}
}
