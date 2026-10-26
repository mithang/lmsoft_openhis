using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Thuốc hệ thống
    /// </summary>
    [Table("xt_yp")]
    [Obsolete("please use the view")]
    public class SysDrugEntity : IEntity<SysDrugEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        /// Ví dụ trước nhất:(Giáp) (Ất10%)……
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
        /// 
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// 
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
        /// Chuẩn bị bãi bỏ
        /// </summary>
        public decimal? mzcls { get; set; }

        /// <summary>
        /// Chuẩn bị bãi bỏ,bằng"Đơn vị tách lẻ"Thay thế
        /// </summary>
        public string mzcldw { get; set; }

        /// <summary>
        /// Chuẩn bị bãi bỏ
        /// </summary>
        public decimal? zycls { get; set; }

        /// <summary>
        /// Chuẩn bị bãi bỏ
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
        /// 
        /// </summary>
        public decimal? pfj2 { get; set; }

        /// <summary>
        /// Xem chi tiết trong bảng hạng mục hệ thống"Tỷ lệ tự chi trả"Giải thích
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 1 Tự lo 2 Tự chi trả theo phân loại   Xem chi tiết trong bảng hạng mục hệ thống"Tính chất tự chi trả"Giải thích      Ngoại trú khả dụng,Cấp cứu không khả dụng,Hoặc ngược lại,Kiểm soát theo việc các khoa dược khác nhau có thuốc hay không
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
        /// 1:Hiệu lực 0:Không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? medid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? medextid { get; set; }

        /// <summary>
        /// Khi thuốc được tiêu thụ tại các nhà thuốc"Cấp đóng gói"hoặc"Thông tin số lẻ đã tách"
        /// </summary>
        public string ypbzdm { get; set; }

        /// <summary>
        /// Mã nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }

        /// <summary>
        /// ----0:Ngừng sử dụng  1:Kích hoạt   ---0:Dùng chung,1:Khám ngoại trú,2:Nhập viện  (Hủy)      
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

    }
}
