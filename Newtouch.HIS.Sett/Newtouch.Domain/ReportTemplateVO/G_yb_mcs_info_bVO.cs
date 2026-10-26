using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng thông tin vật tư tiêu hao
    /// </summary>
    public class G_yb_mcs_info_bVO
    {
        //	Mã danh mục y tế				
        public string MED_LIST_CODG { get; set; }
        //	Tên vật tư tiêu hao				
        public string MCS_NAME { get; set; }
        //	Mã định danh duy nhất thiết bị y tế				
        public string UDI { get; set; }
        //	Mã tên chung BHYT				
        public string HI_GENNAME_CODE { get; set; }
        //	Tên chung BHYT				
        public string HI_GENNAME { get; set; }
        //	Mẫu sản phẩm				
        public string PROD_MOL { get; set; }
        //	Mã quy cách				
        public string SPEC_CODE { get; set; }
        //	Quy cách				
        public string SPEC { get; set; }
        //	Phân loại vật tư tiêu hao				
        public string MCS_TYPE { get; set; }
        //	Mẫu quy cách				
        public string SPEC_MOL { get; set; }
        //	Mã chất liệu				
        public string MATL_CODE { get; set; }
        //	Chất liệu vật tư tiêu hao				
        public string MCS_MATL { get; set; }
        //	Quy cách đóng gói				
        public string PACSPEC { get; set; }
        //	Số lượng đóng gói				
        public string PAC_CNT { get; set; }
        //	Chất liệu đóng gói sản phẩm				
        public string PROD_PACMATL { get; set; }
        //	Đơn vị đóng gói				
        public string PACUNT { get; set; }
        //	Tỷ lệ chuyển đổi sản phẩm				
        public string PROD_CONVRAT { get; set; }
        //	Đơn vị sử dụng nhỏ nhất				
        public string MIN_USEUNT { get; set; }
        //	Loại nơi sản xuất				
        public string PRODPLAC_TYPE { get; set; }
        //	Tên loại nơi sản xuất				
        public string PRODPLAC_TYPE_NAME { get; set; }
        //	Tiêu chuẩn sản phẩm				
        public string PRODSTRD { get; set; }
        //	Hạn dùng sản phẩm				
        public string PRODEXPY { get; set; }
        //	Hiệu năng, cấu trúc và thành phần				
        public string PERF_STRU_COMP { get; set; }
        //	Phạm vi áp dụng				
        public string APB_SCP { get; set; }
        //	Cách dùng sản phẩm				
        public string PROD_ISTR { get; set; }
        //	Số hình ảnh sản phẩm				
        public string PROD_IMG_NO { get; set; }
        //	Tiêu chuẩn chất lượng sản phẩm				
        public string PROD_QLT_STD { get; set; }
        //	Hướng dẫn sử dụng				
        public string MANL { get; set; }
        //	Tài liệu chứng minh khác				
        public string OTH_CERT_MATL { get; set; }
        //	Cờ dùng riêng cho máy chuyên dụng				
        public string SPED_FLAG { get; set; }
        //	Tên máy chuyên dụng				
        public string SPED_NAME { get; set; }
        //	Tên bộ				
        public string COMB_NAME { get; set; }
        //	Cờ bộ máy				
        public string CASE_FLAG { get; set; }
        //	Cờ hạn chế sử dụng				
        public string LMT_USED_FLAG { get; set; }
        //	Phạm vi giới hạn dùng BHYT				
        public string HI_LMT_SCP { get; set; }
        //	Đơn vị bán nhỏ nhất				
        public string MIN_SALUNT { get; set; }
        //	Cờ vật tư tiêu hao giá trị cao				
        public string HIGHVAL_MCS_FLAG { get; set; }
        //	Mã phân loại vật tư y tế				
        public string MATL_TYPE_CODE { get; set; }
        //	Cờ vật tư cấy ghép và cơ quan cơ thể người				
        public string IMPT_MATL_HMORGN_FLAG { get; set; }
        //	Cờ tiệt trùng				
        public string STLZ_FLAG { get; set; }
        //	Tên cờ tiệt trùng				
        public string STLZ_FLAG_NAME { get; set; }
        //	Cờ loại cấy ghép hoặc can thiệp				
        public string IMPT_ITVT_CLSS_FLAG { get; set; }
        //	Tên loại cấy ghép hoặc can thiệp				
        public string IMPT_ITVT_CLSS_NAME { get; set; }
        //	Cờ dùng một lần				
        public string DSPO_USED_FLAG { get; set; }
        //	Tên cờ dùng một lần				
        public string DSPO_USED_FLAG_NAME { get; set; }
        //	Tên người đăng ký lưu trữ				
        public string REGER_NAME { get; set; }
        //	Ngày bắt đầu				
        public DateTime? BEGNDATE { get; set; }
        //	Ngày kết thúc				
        public DateTime? ENDDATE { get; set; }
        //	Loại quản lý thiết bị y tế				
        public string MED_EQU_MGT_TYPE { get; set; }
        //	Tên loại quản lý thiết bị y tế				
        public string MED_EQU_MGT_TYPE_NAME { get; set; }
        //	Số đăng ký lưu trữ				
        public string REG_FIL_NO { get; set; }
        //	Tên sản phẩm đăng ký lưu trữ				
        public string REG_FIL_PROD_NAME { get; set; }
        //	Cấu trúc và thành phần				
        public string STRU_COMP { get; set; }
        //	Nội dung khác				
        public string OTH_CONT { get; set; }
        //	Ngày phê duyệt				
        public DateTime? APRV_DATE { get; set; }
        //	Nơi ở người đăng ký lưu trữ				
        public string REGER_ADDR { get; set; }
        //	Thời gian bắt đầu hiệu lực chứng nhận đăng ký				
        public DateTime? REGCERT_EXPY_BEGNTIME { get; set; }
        //	Thời gian kết thúc hiệu lực chứng nhận đăng ký				
        public DateTime? REGCERT_EXPY_ENDTIME { get; set; }
        //	Mã doanh nghiệp sản xuất				
        public string PRODENTP_CODE { get; set; }
        //	Tên doanh nghiệp sản xuất				
        public string PRODENTP_NAME { get; set; }
        //	Địa chỉ sản xuất				
        public string MANU_ADDR { get; set; }
        //	Doanh nghiệp đại lý				
        public string AGNT_ENTP { get; set; }
        //	Địa chỉ doanh nghiệp đại lý				
        public string AGNT_ENTP_ADDR { get; set; }
        //	Nước hoặc khu vực sản xuất				
        public string MANU_NAT_REGN { get; set; }
        //	Cơ sở dịch vụ sau bán hàng				
        public string AFTSAL_SERINS { get; set; }
        //	Hồ sơ điện tử chứng nhận đăng ký hoặc lưu trữ				
        public string REG_FIL_ELEC_FILE { get; set; }
        //	Hình ảnh sản phẩm				
        public string PROD_IMG { get; set; }
        //	Cờ hiệu lực				
        public string VALI_FLAG { get; set; }
        //	Số bản ghi duy nhất của dữ liệu				
        public string RID { get; set; }
       
        //	Số phiên bản				
        public string VER { get; set; }
        //	Tên phiên bản				
        public string VER_NAME { get; set; }
        

    }
}
