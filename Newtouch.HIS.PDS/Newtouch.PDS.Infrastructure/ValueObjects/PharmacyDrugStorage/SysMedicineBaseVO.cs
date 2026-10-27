
using Newtouch.Infrastructure.EF.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysMedicineBaseVO : IEntity<SysMedicineBaseVO>
    {
        /// <summary>
        /// 
        /// </summary>
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
        /// Trước nhất ví dụ：(giáp) (ất10%)……
        /// </summary>
        public string ypqz { get; set; }

        /// <summary>
        /// Sau nhất ví dụ：……
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
        /// 2018.04.18 ý Hệ số chuyển đổi liều lượng
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
        /// Chuẩn bị bãi bỏ
        /// </summary>
        public decimal? mzcls { get; set; }

        /// <summary>
        /// Chuẩn bị bãi bỏ，bằng“Đơn vị chia lẻ”Thay thế
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
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá nhập mặc định
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? pfj { get; set; }

        /// <summary>
        /// Xem chi tiết trong bảng hạng mục hệ thống“Tỷ lệ tự trả”Giải thích
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 1 Tự lo 2 Tự trả theo loại   Xem chi tiết trong bảng hạng mục hệ thống“Tính chất tự trả”Giải thích      Ngoại trú có thể dùng，Cấp cứu không thể dùng，hoặc ngược lại，Kiểm soát theo bộ phận dược khác nhau có thuốc hay không
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
        /// xt_yc  Sửa tên lưu trực tiếp
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
        /// Khi thuốc tiêu hao tại các nhà thuốc“Cấp đóng gói”hoặc“Thông tin số chia lẻ”
        /// </summary>
        public string ypbzdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }

        /// <summary>
        /// ----0：Ngừng dùng  1：Bật   ---0：Chung，1：Ngoại trú，2：Nội trú  （Hủy bỏ）      
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
        /// 1：Hiệu lực 0：Không hiệu lực
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
        /// Thông tin kháng sinh thuốcId
        /// </summary>
        public string kssId { get; set; }

        /// <summary>
        /// Cờ thuốc thiết yếu
        /// </summary>
        public string jybz { get; set; }


        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// Số tiền vượt giá giới hạn
        /// </summary>
        public decimal? cxjje { get; set; }

        /// <summary>
        /// Chủng loại thuốc
        /// </summary>
        public string tsypbz { get; set; }
    }
}