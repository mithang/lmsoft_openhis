using System;

namespace Newtouch.Domain.ValueObjects
{
    public class PrescriptionHtmlVO
    {
        #region 处方主表
        /// <summary>
        /// 
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? sfbz { get; set; }

        /// <summary>
        /// Enum 1 Đơn thuốc tây 2 Đơn thuốc đông y 3 Đơn thuốc phục hồi chức năng 4 Đơn xét nghiệm 5 Đơn kiểm tra 6 Đơn truyền dịch
        /// </summary>
        public int cflx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Số thang
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }
        /// <summary>
        /// Nhãn đơn thuốc JI Hướng thầnI JII Hướng thầnII MZ Gây mê
        /// </summary>
        public string cftag { get; set; }
        /// <summary>
        /// Cách sắc hộ
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// Số thang sắc hộ
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// Dặn dò
        /// </summary>
        public string cfzt { get; set; }
        public string isdzcf { get; set; }//Đơn thuốc điện tử 1 Có  0 Không
        #endregion



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
        /// Cách dùng
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pcCode { get; set; }

        /// <summary>
        /// Số lượng trong thuốc=Tổng lượng   Tổng lượng trong phục hồi chức năng(Không phải số lượng)=Tổng lượng
        /// </summary>
        public int? zl { get; set; }
        
        /// <summary>
        ///
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Đơn thuốc không phải phục hồi chức năng Tạm thời sử dụng
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
        /// Số nhóm(Số nhóm của thuốc)
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
        /// Khoa thực hiện(Khoa nhà thuốc của thuốc,Khoa thực hiện xét nghiệm/kiểm tra)(Khoa thực hiện đơn thuốc phục hồi chức năng)
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
        /// <summary>
        /// Số ngày
        /// </summary>
        public decimal? ts { get; set; }
        public string ztmbId { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }

        /// <summary>
        /// Lý do dùng kháng sinh
        /// </summary>
        public string kssReason { get; set; }
        /// <summary>
        /// Phương pháp vị trí
        /// </summary>
        public string bwff { get; set; }

        public string sqlx { get; set; }
        /// <summary>
        /// Sắp xếp add by huangshanshan 2019.5.17 
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// Tốc độ truyền add by huangshanshan 2019.6.3
        /// </summary>
        public int? ds { get; set; }
        /// <summary>
        /// Cờ chuyển sang tự chi trả
        /// </summary>
        public int? zzfbz { get; set; }


        public string yfztbm { get; set; }

        public string yfztmc { get; set; }

        public string sfzt { get; set; }
        /// <summary>
        /// Giải thích đơn thuốc đông y
        /// </summary>
        public string zysm { get; set; }

        #endregion
        #region 处方明细2
        /// <summary>
        /// 
        /// </summary>
        public string ypCode2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypmc2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? mcjl2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mcjldw2 { get; set; }



        /// <summary>
        ///
        /// </summary>
        public int? sl2 { get; set; }

        /// <summary>
        /// Đơn thuốc không phải phục hồi chức năng Tạm thời sử dụng
        /// </summary>
        public string dw2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? dj2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? je2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remark2 { get; set; }

        /// <summary>
        /// Khoa thực hiện(Khoa nhà thuốc của thuốc,Khoa thực hiện xét nghiệm/kiểm tra)(Khoa thực hiện đơn thuốc phục hồi chức năng)
        /// </summary>
        public string zxks2 { get; set; }

        /// <summary>
        /// Cờ chuyển sang tự chi trả
        /// </summary>
        public int? zzfbz2 { get; set; }
        /// <summary>
        /// Giải thích đơn thuốc đông y
        /// </summary>
        public string zysm2 { get; set; }

        #endregion

        /// <summary>
        /// Lý do lâm sàng
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Ghi chú yêu cầu
        /// </summary>
        public string sqbz { get; set; }
        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        public string ispsbz { get; set; }

        public string islgbz { get; set; }
        public int? ztsl { get; set; }

		/// <summary>
		/// Số ngày hiệu lực đơn thuốc
		/// </summary>
		public int? yxts { get; set; }
        public string gjybdm { get; set; }
    }
}
