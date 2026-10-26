using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Thuộc tính thuốc hệ thống
    /// </summary>
    [Table("xt_ypsx_base")]
    public class SysMedicinePropertyBaseEntity : IEntity<SysMedicinePropertyBaseEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int ypsxId { get; set; }

        /// <summary>
        /// Số hiệu thuốc hệ thống liên kết
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// --0 Không cần xét duyệt 1 Cần xét duyệt   --Giai đoạn một tạm thời chưa làm 2006.06.21
        /// </summary>
        public string shbz { get; set; }

        /// <summary>
        /// 0 Hạng mục thông thường 1 Hạng mục đặc biệt refer xt_yptsbz   Thuốc đặc biệt chỉ được sử dụng cho tính chất bệnh nhân cụ thể.
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// Đã bao gồm “cờ thử phản ứng da” ban đầu
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
        /// 0 Không cần đấu thầu   1 Đấu thầu cấp thành phố   2 Đấu thầu cấp quận   3 Đấu giá cạnh tranh   4 Hỏi giá
        /// </summary>
        public string zbbz { get; set; }

        /// <summary>
        /// Cách dùng mặc định từ xt_ypyf
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
        /// Trường thiết lập kiểm soát số lượng bảo hiểm y tế
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Trường thiết lập kiểm soát số lượng bảo hiểm y tế
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Trường thiết lập kiểm soát số lượng bảo hiểm y tế
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// Trường thiết lập kiểm soát số lượng bảo hiểm y tế
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// Trường thiết lập kiểm soát số lượng bảo hiểm y tế
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Từ thuộc tính đặc biệt của thuốc hệ thống xt_ypxz
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// Từ xt_ypfl
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// 0: Thu phí làm tròn theo bữa 1: Thu phí làm tròn theo lần
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
        /// xg: Sửa đổi, xz: Thêm mới, ty: Ngừng sử dụng, qy: Kích hoạt, tj: Điều chỉnh giá
        /// </summary>
        public string xglx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ghdw { get; set; }

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
        /// Id tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Thời gian thuốc đồng bộ bảo hiểm y tế
        /// </summary>
        public DateTime? LastYBUploadTime { get; set; }
        /// <summary>
        /// Liều lượng mặc định
        /// </summary>
        public decimal? mrjl { get; set; }
        /// <summary>
        /// Tần suất mặc định
        /// </summary>
        public string mrpc { get; set; }
        /// <summary>
        /// Cờ bảo hiểm y tế
        /// </summary>
        public string ybbz { get; set; }
        /// <summary>
        /// Mã bảo hiểm y tế nông thôn mới
        /// </summary>
        public string xnhybdm { get; set; }
        /// <summary>
        /// Mã bảo hiểm y tế nông thôn mới
        /// </summary>
        public string gjybdm { get; set; }
        public string gjybmc { get; set; }
        /// <summary>
        /// Giới hạn mỗi lần
        /// </summary>
        public decimal? dcxl { get; set; }
        /// <summary>
        /// Giới hạn bệnh mạn tính
        /// </summary>
        public decimal? mbxl { get; set; }

        /// <summary>
        /// Cách dùng mặc định
        /// </summary>
        public string mryf { get; set; }
        /// <summary>
        /// Quy cách bảo hiểm y tế
        /// </summary>
        public string ybgg { get; set; }
        public int? kcyjz { get; set; }
    }
}
