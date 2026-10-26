using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuộc tính thuốc
    /// </summary>
    [Table("xt_ypsx")]
    [Obsolete("please use the view")]
    public class SysMedicinePropertyEntity : IEntity<SysMedicinePropertyEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int ypsxId { get; set; }

        /// <summary>
        /// liên kết hệ thống thuốc số hiệu thuốc
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// --0 không có cần duyệt 1 cần duyệt   －－giai đoạn 1 tạm thời không làm 2006.06.21
        /// </summary>
        public string shbz { get; set; }

        /// <summary>
        /// 0 hạng mục thường 1 đặc biệt hạng mục refer xt_yptsbz   đặc biệt thuốc mô tả có đặc thù của tính chất bệnh nhân mới có thể sử dụng của.
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// bao gồm đã ban đầu của"thử phản ứng da cờ"
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
        /// 0 không cần đấu thầu   1 thành phố đấu thầu   2 quận đấu thầu   3 đấu giá   4 cứu giá
        /// </summary>
        public string zbbz { get; set; }

        /// <summary>
        /// cách dùng mặc định từ xt_ypyf
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
        /// trường cài đặt khống chế số lượng BHYT
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// trường cài đặt khống chế số lượng BHYT
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// trường cài đặt khống chế số lượng BHYT
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// trường cài đặt khống chế số lượng BHYT
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// trường cài đặt khống chế số lượng BHYT
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// từ hệ thống thuốc đặc biệt thuộc tính xt_ypxz
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// từ xt_ypfl
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// 0: mỗi lần làm tròn thu phí 1: mỗi lần làm tròn thu phí
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
        /// xg: sửa,xz: thêm mới,ty: ngừng sử dụng,qy: kích hoạt,tj: điều chỉnh giá
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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// tổ chức Id
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
