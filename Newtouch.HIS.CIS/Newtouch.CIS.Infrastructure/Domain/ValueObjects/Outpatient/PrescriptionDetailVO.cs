using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class PrescriptionDetailVO
    {
        #region Chi tiết đơn thuốc

        /// <summary>
        /// 
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Liều mỗi lần điều trị  kimđốiPhục hồi chức năngMục
        /// </summary>
        public int? mczll { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? mcjl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mcjldw { get; set; }

        /// <summary>
        /// Cách dùngMã
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pcCode { get; set; }

        /// <summary>
        /// ThuốctrongcủaSố lượng=Tổng lượng   Phục hồi chức năngtrongcủaTổng lượng=Tổng lượng
        /// </summary>
        public int? zl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public string bw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string urgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string purpose { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// Bảo hiểm y tếduy nhấtmộtmã
        /// </summary>
        public string ybwym { get; set; }
        /// <summary>
        /// hạnchếSử dụngCờ
        /// </summary>
        public string xzsybz { get; set; }

        public decimal? ts { get; set; }
        public string ztmbId { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
        public string ztCode { get; set; }

        /// <summary>
        /// Kháng sinhnguyên nhân
        /// </summary>
        public string kssReason { get; set; }
        public string bwff { get; set; }

        public string sqlx { get; set; }
        public int? px { get; set; }
        /// <summary>
        /// tốc độ truyền
        /// </summary>
        public int? ds { get; set; }
        /// <summary>
        /// Chuyển sang tự chi trảCờ
        /// </summary>
        public int? zzfbz { get; set; }

        public string ispsbz { get; set; }
        public string islgbz { get; set; }
        public int? ztsl { get; set; }

        public int? sfzt { get; set; }
        public string zysm { get; set; }

        /// <summary>
        /// Đơn thuốckèmmangtạocủaThường quyĐơn thuốc
        /// </summary>
        public string syncfbz { get; set; }
		#endregion


		/// <summary>
		/// Đơn thuốcid
		/// </summary>
		public string cfId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ghlybz { get; set; }
        public string gjybdm { get; set; }

	}
}
