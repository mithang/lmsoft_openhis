using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng thông tin thảo dược Trung y
    /// </summary>
    public class G_yb_tcmherb_info_bVO
    {
        //	Mã danh mục y tế				
        public string MED_LIST_CODG { get; set; }
        //	Tên thảo dược Trung y				
        public string TCMHERB_NAME { get; set; }
        //	Cờ thuốc đơn phương/phức phương				
        public string CPND_FLAG { get; set; }
        //	Cấp chất lượng				
        public string QLT_REG { get; set; }
        //	Năm thảo dược Trung y				
        public string TCMHERB_YEAR { get; set; }
        //	Bộ phận dùng làm thuốc				
        public string MED_PART { get; set; }
        //	Đo lường an toàn				
        public string SAFE_MTR { get; set; }
        //	Cách dùng thông thường				
        public string CNVL_USED { get; set; }
        //	Tính vị				
        public string NATFLA { get; set; }
        //	Quy kinh				
        public string MERTPM { get; set; }
        //	Chủng loại				
        public string CAT { get; set; }
        //	Ngày bắt đầu				
        public DateTime? BEGNDATE { get; set; }
        //	Ngày kết thúc				
        public DateTime? ENDDATE { get; set; }
        //	Cờ hiệu lực				
        public string VALI_FLAG { get; set; }
        //	Số bản ghi duy nhất của dữ liệu				
        public string RID { get; set; }
        //	Thời gian tạo dữ liệu				
        public DateTime CRTE_TIME { get; set; }
        //	Thời gian cập nhật dữ liệu				
        public DateTime UPDT_TIME { get; set; }
        ////	Người tạoID				
        //public string CRTER_ID { get; set; }
        ////	Tên người tạo				
        //public string CRTER_NAME { get; set; }
        ////	Mã cơ sở tạo				
        //public string CRTE_OPTINS_NO { get; set; }
        ////	Người xử lýID				
        //public string OPTER_ID { get; set; }
        ////	Tên người xử lý				
        //public string OPTER_NAME { get; set; }
        ////	Thời gian xử lý				
        //public DateTime? OPT_TIME { get; set; }
        ////	Mã cơ sở xử lý				
        //public string OPTINS_NO { get; set; }
        //	Số phiên bản				
        public string VER { get; set; }
        //	Tên phiên bản				
        public string VER_NAME { get; set; }
        //	Tên dược liệu				
        public string MLMS_NAME { get; set; }
        //	Chức năng và chỉ định				
        public string EFCC_ATD { get; set; }
        //	Phương pháp bào chế				
        public string PSDG_MTD { get; set; }
        //	Phân loại công hiệu				
        public string ECY_TYPE { get; set; }
        //	Nguồn gốc dược liệu				
        public string MLMS_CAT_SOUC { get; set; }
        //	Chính sách thanh toán BHYT quốc gia				
        public string NAT_HI_PAY_POL { get; set; }
        //	Chính sách thanh toán BHYT cấp tỉnh				
        public string PROV_HI_PAY_POL { get; set; }
        //	Tên tiêu chuẩn				
        public string STD_NAME { get; set; }
        //	Số trang tiêu chuẩn				
        public string STD_PAGEN { get; set; }
        //	Hồ sơ điện tử tiêu chuẩn				
        public string STD_ELECACS { get; set; }
        ////	Cờ ban hành				
        //public string ISU_FLAG { get; set; }
        ////	Truyền dữ liệuID				
        //public string TRAM_DATA_ID { get; set; }
        ////	Thời gian có hiệu lực				
        //public DateTime? EFFT_TIME { get; set; }
        ////	Thời gian hết hiệu lực				
        //public DateTime? INVD_TIME { get; set; }

    }
}
