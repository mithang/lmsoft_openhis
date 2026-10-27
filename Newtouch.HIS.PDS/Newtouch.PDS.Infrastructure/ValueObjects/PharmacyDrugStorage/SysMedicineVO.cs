using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysMedicineVO : IEntity<SysMedicineVO>
    {
        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>

        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>

        public string ypmc { get; set; }

        /// <summary>
        /// Khóa chính tổ chức cấp cao nhất
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public string ypqz { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public string yphz { get; set; }

        /// <summary>
        /// Tên hàng hóa
        /// </summary>

        public string spm { get; set; }

        /// <summary>
        /// Chữ đầu phiên âm
        /// </summary>

        public string py { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public decimal cfl { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public string cfdw { get; set; }

        /// <summary>
        /// 2018.04.18 ý Hệ số chuyển đổi liều lượng
        /// </summary>

        public decimal jl { get; set; }

        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>

        public string jldw { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public decimal bzs { get; set; }

        /// <summary>
        /// Đơn vị đóng gói
        /// </summary>

        public string bzdw { get; set; }

        /// <summary>
        /// Số chia lẻ ngoại trú
        /// </summary>

        public decimal mzcls { get; set; }

        /// <summary>
        /// Đơn vị chia lẻ ngoại trú
        /// </summary>

        public string mzcldw { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// Số chia lẻ nội trú
        /// </summary>

        public decimal zycls { get; set; }

        /// <summary>
        /// Đơn vị chia lẻ nội trú
        /// </summary>

        public string zycldw { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public decimal cls { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public string cldw { get; set; }

        /// <summary>
        /// Đơn vị định giá
        /// </summary>

        public string djdw { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public decimal lsj { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public decimal pfj { get; set; }

        /// <summary>
        /// Tỷ lệ tự trả
        /// </summary>

        public decimal zfbl { get; set; }

        /// <summary>
        /// Tính chất tự trả
        /// </summary>

        public string zfxz { get; set; }

        /// <summary>
        /// Loại lớn
        /// </summary>

        public string dlCode { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public string jx { get; set; }

        /// <summary>
        /// Tên hãng thuốc
        /// </summary>

        public string ycmc { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public int? medid { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public int? medextid { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public string ypbzdm { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string nbdl { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public string zt { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public int? px { get; set; }

        /// <summary>
        /// Thuốc theoID
        /// </summary>
        public int ypsxId { get; set; }

        public string shbz { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string jsbz { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string gzy { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string mzy { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string yljsy { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string zbbz { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>

        public decimal? yl { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string jzlx { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public int mrbzq { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public DateTime? zjtzsj { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string xglx { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string ghdw { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public int ypcd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlMc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jxmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypflMc { get; set; }

        /// <summary>
        /// Thời gian đồng bộ BHYT cuối
        /// </summary>
        public DateTime? LastYBUploadTime { get; set; }

        /// <summary>
        /// Đã đồng bộ BHYT chưa
        /// </summary>
        public string isSynch { get; set; }

        /// <summary>
        /// Liều lượng mặc định
        /// </summary>
        public decimal? mrjl { get; set; }

        /// <summary>
        /// Tần suất mặc định
        /// </summary>
        public string mrpc { get; set; }

        /// <summary>
        /// Có phải kháng sinh không
        /// </summary>
        public string isKss { get; set; }

        /// <summary>
        /// Kháng sinhId
        /// </summary>
        public string kssId { get; set; }

        /// <summary>
        /// Cờ thuốc thiết yếu
        /// </summary>
        public string jybz { get; set; }

        /// <summary>
        /// Cờ BHYT
        /// </summary>
        public string ybbz { get; set; }

        /// <summary>
        /// Mã BHYT nông thôn mới
        /// </summary>
        public string xnhybdm { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// Mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
        public string gjybmc { get; set; }
        /// <summary>
        /// Số tiền vượt giá giới hạn
        /// </summary>
        public decimal? cxjje { get; set; }


        /// <summary>
        /// Chủng loại thuốc
        /// </summary>
        public string tsypbz { get; set; }
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
        /// Tên cách dùng mặc định
        /// </summary>
        public string mryfmc { get; set; }


        /// <summary>
        /// Quy cách BHYT
        /// </summary>
        public string ybgg { get; set; }
    }
}
