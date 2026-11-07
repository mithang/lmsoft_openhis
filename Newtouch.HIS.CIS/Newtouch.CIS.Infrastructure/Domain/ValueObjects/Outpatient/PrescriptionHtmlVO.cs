using System;

namespace Newtouch.Domain.ValueObjects
{
    public class PrescriptionHtmlVO
    {
        #region Đơn thuốcchínhbảng
        /// <summary>
        /// 
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? sfbz { get; set; }

        /// <summary>
        /// Liệt kê 1 Thuốc Tây Đơn thuốc 2 Thuốc Đông y Đơn thuốc 3 Phục hồi chức năng Đơn thuốc 4 Xét nghiệm Đơn thuốc 5 Kiểm tra Đơn thuốc 6 truyền dịch Đơn thuốc
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
        /// dánsố
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
        /// Đơn thuốcnhãn JI tinhI JII tinh II MZ têsay
        /// </summary>
        public string cftag { get; set; }
        /// <summary>
        /// sắc hộphươngkiểu
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// sắc hộtrảsố
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// Dặn dò
        /// </summary>
        public string cfzt { get; set; }
        public string isdzcf { get; set; }//điện tử Đơn thuốc 1 là  0 Không
        #endregion



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
        /// Liều mỗi lần điều trị  kimđối Phục hồi chức năng Mục
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
        /// Thuốctrongcủa Số lượng=Tổng lượng   Phục hồi chức năngtrongcủa Tổng lượng(khônglà Số lượng)=Tổng lượng
        /// </summary>
        public int? zl { get; set; }
        
        /// <summary>
        ///
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// không Phục hồi chức năng Đơn thuốc tạmkhi Sử dụng
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
        /// Số nhóm(Thuốccủa Số nhóm)
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
        /// Khoa thực hiện(Thuốccủa Khoa dược,Xét nghiệm Kiểm tracủa Khoa thực hiện)(Phục hồi chức năng Đơn thuốccủa Khoa thực hiện)
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
        /// hạnchế Sử dụng Cờ
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
        /// Kháng sinhnguyên nhân
        /// </summary>
        public string kssReason { get; set; }
        /// <summary>
        /// Vị trí Phương pháp
        /// </summary>
        public string bwff { get; set; }

        public string sqlx { get; set; }
        /// <summary>
        /// Thứ tự add by huangshanshan 2019.5.17 
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// tốc độ truyền add by huangshanshan 2019.6.3
        /// </summary>
        public int? ds { get; set; }
        /// <summary>
        /// Chuyển sang tự chi trả Cờ
        /// </summary>
        public int? zzfbz { get; set; }


        public string yfztbm { get; set; }

        public string yfztmc { get; set; }

        public string sfzt { get; set; }
        /// <summary>
        /// Thuốc Đông y Đơn thuốc Mô tả
        /// </summary>
        public string zysm { get; set; }

        #endregion
        #region Chi tiết đơn thuốc2
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
        /// không Phục hồi chức năng Đơn thuốc tạmkhi Sử dụng
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
        /// Khoa thực hiện(Thuốccủa Khoa dược,Xét nghiệm Kiểm tracủa Khoa thực hiện)(Phục hồi chức năng Đơn thuốccủa Khoa thực hiện)
        /// </summary>
        public string zxks2 { get; set; }

        /// <summary>
        /// Chuyển sang tự chi trả Cờ
        /// </summary>
        public int? zzfbz2 { get; set; }
        /// <summary>
        /// Thuốc Đông y Đơn thuốc Mô tả
        /// </summary>
        public string zysm2 { get; set; }

        #endregion

        /// <summary>
        /// Nguyên nhân lâm sàng
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Đăng ký Ghi chú
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
		/// Đơn thuốc Hiệu lực Số ngày
		/// </summary>
		public int? yxts { get; set; }
        public string gjybdm { get; set; }
    }
}
