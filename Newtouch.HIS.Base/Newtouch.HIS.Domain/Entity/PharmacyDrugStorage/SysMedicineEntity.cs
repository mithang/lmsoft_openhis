using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_yp")]
    public class SysMedicineEntity : IEntity<SysMedicineEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int ypId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên chung
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tiền tố, ví dụ: (A) (B10%)……
        /// </summary>
        public string ypqz { get; set; }

        /// <summary>
        /// Hậu tố, ví dụ:……
        /// </summary>
        public string yphz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string spm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? cfl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfdw { get; set; }

        /// <summary>
        /// 2018.04.18 Ý: Hệ số chuyển đổi liều lượng
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal bzs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// Chuẩn bị hủy bỏ
        /// </summary>
        public decimal? mzcls { get; set; }

        /// <summary>
        /// Chuẩn bị hủy bỏ, thay bằng “đơn vị lẻ”
        /// </summary>
        public string mzcldw { get; set; }

        /// <summary>
        /// Chuẩn bị hủy bỏ
        /// </summary>
        public decimal? zycls { get; set; }

        /// <summary>
        /// Chuẩn bị hủy bỏ
        /// </summary>
        public string zycldw { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá nhập mặc định
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? pfj { get; set; }

        /// <summary>
        /// Xem phần giải thích “tỷ lệ tự trả” trong bảng hạng mục hệ thống
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 1 Tự trả 2 Tự trả theo phân loại   Xem phần giải thích “tính chất tự trả” trong bảng hạng mục hệ thống      Ngoại trú dùng được, cấp cứu không dùng được, hoặc ngược lại, kiểm soát dựa trên việc các phòng dược khác nhau có thuốc hay không
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// xt_ypjx
        /// </summary>
        public string jx { get; set; }

        /// <summary>
        /// xt_yc  Sửa đổi rồi lưu trực tiếp tên
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? medid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? medextid { get; set; }

        /// <summary>
        /// “Cấp độ đóng gói” hoặc “thông tin số lẻ” khi thuốc được tiêu thụ tại các phòng dược
        /// </summary>
        public string ypbzdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }

        /// <summary>
        /// ----0: Ngừng sử dụng  1: Kích hoạt   ---0: Chung, 1: Ngoại trú, 2: Nội trú  (đã hủy bỏ)      
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 1: Có hiệu lực 0: Không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? lsbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? mjzbz { get; set; }

        /// <summary>
        /// Cách dùng thuốc
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// Có phải kháng sinh hay không
        /// </summary>
        public string isKss { get; set; }

        /// <summary>
        /// Id thông tin kháng sinh của thuốc
        /// </summary>
        public string kssId { get; set; }

        /// <summary>
        /// Dấu hiệu thuốc thiết yếu
        /// </summary>
        public string jybz { get; set; }


        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// Số tiền vượt giá giới hạn
        /// </summary>
        public decimal?cxjje { get; set; }

		/// <summary>
		/// Loại thuốc
		/// </summary>
		public string tsypbz { get; set; }
	}
}
