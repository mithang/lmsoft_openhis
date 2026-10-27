using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysMedicinePropertyBaseVO : IEntity<SysMedicinePropertyBaseVO>
    {
        /// <summary>
        /// 
        /// </summary>
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
        /// --0 Không cần duyệt 1 Cần duyệt   －－Giai đoạn 1 tạm thời chưa làm 2006.06.21
        /// </summary>
        public string shbz { get; set; }

        /// <summary>
        /// 0 Hạng mục thường 1 Hạng mục đặc biệt refer xt_yptsbz   Thuốc đặc biệt chỉ dùng cho tính chất bệnh nhân đặc biệt。
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// bao gồm cái gốc“Cờ thử phản ứng da”
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
        /// 0 Không cần đấu thầu   1 Đấu thầu cấp thành phố   2 Đấu thầu cấp quận   3 Chào giá cạnh tranh   4 Hỏi giá
        /// </summary>
        public string zbbz { get; set; }

        /// <summary>
        /// Cách dùng mặc định từxt_ypyf
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
        /// Trường cài đặt khống chế số lượng BHYT
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Trường cài đặt khống chế số lượng BHYT
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Trường cài đặt khống chế số lượng BHYT
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// Trường cài đặt khống chế số lượng BHYT
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// Trường cài đặt khống chế số lượng BHYT
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Từ thuộc tính đặc biệt thuốc hệ thốngxt_ypxz
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// Từxt_ypfl
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// 0：Thu phí làm tròn mỗi bữa1：Thu phí làm tròn mỗi lần
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
        /// xg：Sửa,xz：Thêm mới,ty：Ngừng dùng,qy：Bật,tj：Điều chỉnh giá
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
        /// Thời gian đồng bộ BHYT thuốc
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
        /// Quy cách BHYT
        /// </summary>
        public string ybgg { get; set; }
    }
}