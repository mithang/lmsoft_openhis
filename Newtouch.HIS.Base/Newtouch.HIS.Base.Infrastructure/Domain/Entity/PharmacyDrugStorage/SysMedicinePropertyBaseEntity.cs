using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Thuộc tính thuốc hệ thống
    /// </summary>
    [SugarTable("xt_ypsx_base")]
    public class SysMedicinePropertyBaseEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int ypsxId { get; set; }

        /// <summary>
        /// Liên kết số hiệu thuốc hệ thống
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
        /// 0 Hạng mục thông thường 1 Hạng mục đặc biệt refer xt_yptsbz   Giải thích thuốc đặc biệt chỉ nhóm bệnh nhân có tính chất nhất định mới được dùng.
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// đã bao gồm ban đầu“Cờ thử phản ứng da”
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
        /// 0 Không cần đấu thầu   1 Đấu thầu cấp thành phố   2 Đấu thầu cấp quận   3 Đấu giá   4 Hỏi giá
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
        /// Trường cài đặt kiểm soát số lượng BHYT
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Trường cài đặt kiểm soát số lượng BHYT
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Trường cài đặt kiểm soát số lượng BHYT
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// Trường cài đặt kiểm soát số lượng BHYT
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// Trường cài đặt kiểm soát số lượng BHYT
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Đến từ thuộc tính đặc biệt của thuốc hệ thốngxt_ypxz
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
        /// xg:Sửa,xz:Thêm mới,ty:Vô hiệu hóa,qy:Kích hoạt,tj:Điều chỉnh giá
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
        /// Tổ chứcId
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Thời gian đồng bộ thuốc với BHYT
        /// </summary>
        public DateTime? LastYBUploadTime { get; set; }
        /// <summary>
        /// Liều dùng mặc định
        /// </summary>
        public decimal? mrjl { get; set; }
        /// <summary>
        /// Tần suất mặc định
        /// </summary>
        public string mrpc { get; set; }
        /// <summary>
        /// Cờ BHYT
        /// </summary>
        public string ybbz { get; set; }
        /// <summary>
        /// Mã BHYT nông thôn mới
        /// </summary>
        public string xnhybdm { get; set; }
        /// <summary>
        /// Mã BHYT nông thôn mới
        /// </summary>
        public string gjybdm { get; set; }
        public string gjybmc { get; set; }
        /// <summary>
        /// Giới hạn một lần
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
        /// Quy cách BHYT
        /// </summary>
        public string ybgg { get; set; }
        public int? kcyjz { get; set; }
    }
}
