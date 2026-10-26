using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.SelfService
{
	public class queryOutpfeeDetailInfoReqDTO
	{
		/// <summary>
		/// Mã tổ chức
		/// </summary>
		public string OrganizeId { get; set; }
		/// <summary>
		/// 票据 số (mã nội bộ quyết toán)
		/// </summary>
		public string RECEIPT_NO { get; set; }
	}
}
