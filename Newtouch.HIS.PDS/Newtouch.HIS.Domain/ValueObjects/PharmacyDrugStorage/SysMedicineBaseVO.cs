
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
        /// chung tên
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// ví dụ trước nhất: (A) (B 10%)......
        /// </summary>
        public string ypqz { get; set; }

        /// <summary>
        /// ví dụ sau nhất:......
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
        /// 2018.04.18 ý hệ số chuyển đổi liều dùng
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// đơn vị liều dùng
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
        /// chuẩn bị hủy bỏ
        /// </summary>
        public decimal? mzcls { get; set; }

        /// <summary>
        /// chuẩn bị hủy bỏ, bằng"đơn vị tách lẻ"thay thế
        /// </summary>
        public string mzcldw { get; set; }

        /// <summary>
        /// chuẩn bị hủy bỏ
        /// </summary>
        public decimal? zycls { get; set; }

        /// <summary>
        /// chuẩn bị hủy bỏ
        /// </summary>
        public string zycldw { get; set; }

        /// <summary>
        /// đơn vị nhỏ nhất
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
        /// mặc định giá nhập
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? pfj { get; set; }

        /// <summary>
        /// xem chi tiết hệ thống hạng mục bảng"tỷ lệ tự chi trả"mô tả
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 1 tự lo 2 tự chi trả theo loại   xem chi tiết hệ thống hạng mục bảng"tính chất tự chi trả"mô tả      ngoại trú khả dụng, cấp cứu không khả dụng, hoặc ngược lại, điều khiển theo việc các bộ phận dược khác nhau có thuốc hay không
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
        /// xt_yc  sửa trực tiếp lưu tên
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
        /// khi thuốc được tiêu thụ tại các nhà thuốc"cấp đóng gói"hoặc"thông tin số tách lẻ"
        /// </summary>
        public string ypbzdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }

        /// <summary>
        /// ----0: ngừng sử dụng  1: kích hoạt   ---0: chung, 1: ngoại trú, 2: nội trú   (hủy bỏ)      
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
        /// 1: còn hiệu lực 0: không hiệu lực
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
        /// cách dùng thuốc
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// có phải kháng sinh không
        /// </summary>
        public string isKss { get; set; }

        /// <summary>
        /// thuốc kháng sinh thông tin Id
        /// </summary>
        public string kssId { get; set; }

        /// <summary>
        /// thuốc cơ bản dấu hiệu
        /// </summary>
        public string jybz { get; set; }


        /// <summary>
        /// ghi chú
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// vượt giá giới hạn số tiền
        /// </summary>
        public decimal? cxjje { get; set; }

        /// <summary>
        /// chủng loại thuốc
        /// </summary>
        public string tsypbz { get; set; }
    }
}