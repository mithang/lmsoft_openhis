using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.SelfService
{
	public class OutpatientRegistrationReqDTO
	{
		/// <summary>
		/// ID tổ chức
		/// </summary>
		public string OrganizeId { get; set; }
		/// <summary>
		/// Số thẻ
		/// </summary>
		public string kh { get; set; }
		/// <summary>
		/// cửa cấp cứu dấu hiệu 
		/// </summary>
		public string mjzbz { get; set; }
		/// <summary>
		/// Khoa
		/// </summary>
		public string ks { get; set; }
		/// <summary>
		/// KhoaTên
		/// </summary>
		public string ksmc { get; set; }
		//public string ys { get; set; }
		//public string ysmc { get; set; }
		/// <summary>
		/// Tính chất bệnh nhân
		/// </summary>
		public string brxz { get; set; }
		/// <summary>
		/// Đ ăng ký khám lịch trực id
		/// </summary>
		public string ghpbId { get; set; }
    }
}
