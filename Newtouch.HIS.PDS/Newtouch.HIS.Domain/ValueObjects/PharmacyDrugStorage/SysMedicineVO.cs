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
        /// khóa chính tổ chức
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>

        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>

        public string ypmc { get; set; }

        /// <summary>
        /// cấp cao nhất khóa chính tổ chức
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public string ypqz { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public string yphz { get; set; }

        /// <summary>
        /// tên hàng hóa
        /// </summary>

        public string spm { get; set; }

        /// <summary>
        /// chữ cái đầu phiên âm
        /// </summary>

        public string py { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public decimal cfl { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public string cfdw { get; set; }

        /// <summary>
        /// 2018.04.18 ý hệ số chuyển đổi liều dùng
        /// </summary>

        public decimal jl { get; set; }

        /// <summary>
        /// đơn vị liều dùng
        /// </summary>

        public string jldw { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public decimal bzs { get; set; }

        /// <summary>
        /// đơn vị đóng gói
        /// </summary>

        public string bzdw { get; set; }

        /// <summary>
        /// số tách lẻ ngoại trú
        /// </summary>

        public decimal mzcls { get; set; }

        /// <summary>
        /// đơn vị tách lẻ ngoại trú
        /// </summary>

        public string mzcldw { get; set; }

        /// <summary>
        /// đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// số tách lẻ nội trú
        /// </summary>

        public decimal zycls { get; set; }

        /// <summary>
        /// đơn vị tách lẻ nội trú
        /// </summary>

        public string zycldw { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public decimal cls { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public string cldw { get; set; }

        /// <summary>
        /// đơn vị định giá
        /// </summary>

        public string djdw { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public decimal lsj { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public decimal pfj { get; set; }

        /// <summary>
        /// tỷ lệ tự chi trả
        /// </summary>

        public decimal zfbl { get; set; }

        /// <summary>
        /// tính chất tự chi trả
        /// </summary>

        public string zfxz { get; set; }

        /// <summary>
        /// nhóm lớn
        /// </summary>

        public string dlCode { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public string jx { get; set; }

        /// <summary>
        /// tên nhà máy dược
        /// </summary>

        public string ycmc { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public int? medid { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public int? medextid { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public string ypbzdm { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string nbdl { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public string zt { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public int? px { get; set; }

        /// <summary>
        /// ID thuộc tính thuốc
        /// </summary>
        public int ypsxId { get; set; }

        public string shbz { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string jsbz { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string gzy { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string mzy { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string yljsy { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string zbbz { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>

        public decimal? yl { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string jzlx { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public int mrbzq { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public DateTime? zjtzsj { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string xglx { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string ghdw { get; set; }

        /// <summary>
        /// khóa chính tổ chức
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
        /// BHYT cuối cùng đồng bộ thời gian
        /// </summary>
        public DateTime? LastYBUploadTime { get; set; }

        /// <summary>
        /// có hay không đã đồng bộ BHYT
        /// </summary>
        public string isSynch { get; set; }

        /// <summary>
        /// mặc định liều dùng
        /// </summary>
        public decimal? mrjl { get; set; }

        /// <summary>
        /// mặc định tần suất
        /// </summary>
        public string mrpc { get; set; }

        /// <summary>
        /// có phải kháng sinh không
        /// </summary>
        public string isKss { get; set; }

        /// <summary>
        /// kháng sinh Id
        /// </summary>
        public string kssId { get; set; }

        /// <summary>
        /// thuốc cơ bản dấu hiệu
        /// </summary>
        public string jybz { get; set; }

        /// <summary>
        /// cờ BHYT
        /// </summary>
        public string ybbz { get; set; }

        /// <summary>
        /// mã BHYT nông thôn mới
        /// </summary>
        public string xnhybdm { get; set; }

        /// <summary>
        /// ghi chú
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
        public string gjybmc { get; set; }
        /// <summary>
        /// vượt giá giới hạn số tiền
        /// </summary>
        public decimal? cxjje { get; set; }


        /// <summary>
        /// chủng loại thuốc
        /// </summary>
        public string tsypbz { get; set; }
        /// <summary>
        /// mỗi lần giới hạn
        /// </summary>
        public decimal? dcxl { get; set; }
        /// <summary>
        /// bệnh mãn tính giới hạn
        /// </summary>
        public decimal? mbxl { get; set; }

        /// <summary>
        /// cách dùng mặc định
        /// </summary>
        public string mryf { get; set; }
        /// <summary>
        /// cách dùng mặc định tên
        /// </summary>
        public string mryfmc { get; set; }






        /// <summary>        /// quy cách BHYT        /// </summary>        public string ybgg { get; set; }
    }
}
