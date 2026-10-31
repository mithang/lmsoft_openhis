using Newtouch.Infrastructure;
using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_yp_base")]
    public class SysMedicineBaseEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        
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
        /// Ví dụ trước nhất:(A) (B10%)……
        /// </summary>
        public string ypqz { get; set; }

        /// <summary>
        /// Ví dụ sau cùng:……
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
        /// 2018.04.18 Chú ý Hệ số chuyển đổi liều
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// Đơn vị liều
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
        /// Chuẩn bị hủy bỏ,bằng“Đơn vị lẻ”Thay thế
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
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá nhập mặc định
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// Xem chi tiết trong bảng hạng mục hệ thống“Tỷ lệ tự trả”Mô tả
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 1 Tự chi trả 2 Tự trả theo loại   Xem chi tiết trong bảng hạng mục hệ thống“Tính chất tự trả”Mô tả      Ngoại trú có thể dùng,Cấp cứu không dùng được,hoặc ngược lại,Kiểm soát theo việc khoa dược khác nhau có thuốc hay không
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
        /// xt_yc  Sửa rồi lưu trực tiếp tên
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
        /// khi thuốc tiêu hao tại các nhà thuốc“Cấp đóng gói”hoặc“Thông tin số lẻ”
        /// </summary>
        public string ypbzdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }

        /// <summary>
        /// ----0:Vô hiệu hóa  1:Kích hoạt   ---0:Chung,1:Ngoại trú,2:Nội trú  (Hủy bỏ)      
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
        /// 1:Hiệu lực 0:Vô hiệu
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
        /// Có phải kháng sinh không
        /// </summary>
        public string isKss { get; set; }

        /// <summary>
        /// Thông tin kháng sinh của thuốcId
        /// </summary>
        public string kssId { get; set; }

        /// <summary>
        /// Cờ thuốc cơ bản
        /// </summary>
        public string jybz { get; set; }


        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// Số tiền vượt giá trần
        /// </summary>
        public decimal?cxjje { get; set; }

		/// <summary>
		/// Chủng loại thuốc
		/// </summary>
		public string tsypbz { get; set; }
		
		/// Số phiên bản
		/// </summary>
		public string VER { get; set; }
	}
}
