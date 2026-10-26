using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class PrescriptionDetailVO
    {
        #region 处方明细

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
        /// Liều lượng mỗi lần điều trị  Đối với hạng mục phục hồi chức năng
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
        /// Mã cách dùng
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pcCode { get; set; }

        /// <summary>
        /// Số lượng trong thuốc=Tổng lượng   Tổng lượng trong phục hồi chức năng=Tổng lượng
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
        /// Mã duy nhất BHYT
        /// </summary>
        public string ybwym { get; set; }
        /// <summary>
        /// Cờ hạn chế sử dụng
        /// </summary>
        public string xzsybz { get; set; }

        public decimal? ts { get; set; }
        public string ztmbId { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
        public string ztCode { get; set; }

        /// <summary>
        /// Lý do dùng kháng sinh
        /// </summary>
        public string kssReason { get; set; }
        public string bwff { get; set; }

        public string sqlx { get; set; }
        public int? px { get; set; }
        /// <summary>
        /// Tốc độ truyền
        /// </summary>
        public int? ds { get; set; }
        /// <summary>
        /// Cờ chuyển sang tự chi trả
        /// </summary>
        public int? zzfbz { get; set; }

        public string ispsbz { get; set; }
        public string islgbz { get; set; }
        public int? ztsl { get; set; }

        public int? sfzt { get; set; }
        public string zysm { get; set; }

        /// <summary>
        /// Đơn thuốc thường được tạo kèm theo đơn thuốc
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
