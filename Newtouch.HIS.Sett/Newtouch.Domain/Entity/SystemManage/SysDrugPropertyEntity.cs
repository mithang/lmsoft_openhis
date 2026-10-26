using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_ypsx")]
    [Obsolete("please use the view")]
    public class SysDrugPropertyEntity : IEntity<SysDrugPropertyEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ypsxId { get; set; }

        /// <summary>
        /// yp
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// yp
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// --0 Không cần kiểm tra 1 Cần kiểm tra   －－Giai đoạn 1 tạm thời chưa làm 2006.06.21
        /// </summary>
        public string shbz { get; set; }

        /// <summary>
        /// 0 Hạng mục thông thường 1 Hạng mục đặc biệt refer xt_yptsbz   Giải thích thuốc đặc biệt là chỉ tính chất bệnh nhân đặc biệt mới được dùng.
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// Đã bao gồm bản gốc"Cờ thử da"
        /// </summary>
        public string jsbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gzy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yljsy { get; set; }

        /// <summary>
        /// 0 Không cần đấu thầu   1 Đấu thầu thành phố   2 Đấu thầu khu vực   3 Đấu giá   4 Hỏi giá
        /// </summary>
        public string zbbz { get; set; }

        /// <summary>
        /// Cách dùng mặc định đến từxt_ypyf
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// Trường cài đặt kiểm soát định mức BHYT
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Trường cài đặt kiểm soát định mức BHYT
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Trường cài đặt kiểm soát định mức BHYT
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// Trường cài đặt kiểm soát định mức BHYT
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// Trường cài đặt kiểm soát định mức BHYT
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Đến từ thuộc tính đặc biệt thuốc hệ thốngxt_ypxz
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// Đến từxt_ypfl
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// 0:Thu phí làm tròn mỗi bữa1:Thu phí làm tròn mỗi lần
        /// </summary>
        public string jzlx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? mrbzq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zjtzsj { get; set; }

        /// <summary>
        /// xg:Sửa đổi,xz:Thêm mới,ty:Ngừng sử dụng,qy:Kích hoạt,tj:Điều chỉnh giá
        /// </summary>
        public string xglx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ghdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ypcd { get; set; }

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
        public string zt { get; set; }

    }
}
