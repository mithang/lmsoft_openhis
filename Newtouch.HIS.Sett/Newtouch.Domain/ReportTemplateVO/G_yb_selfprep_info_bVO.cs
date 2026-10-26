using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng thông tin chế phẩm tự chế
    /// </summary>
    public class G_yb_selfprep_info_bVO
    {
        //	Mã danh mục y tế				
        public string MED_LIST_CODG { get; set; }
        //	Tên thương mại thuốc				
        public string DRUG_PRODNAME { get; set; }
        //	Tên khác				
        public string ALIS { get; set; }
        //	Tên tiếng Anh				
        public string ENG_NAME { get; set; }
        //	Dạng bào chế				
        public string DOSFORM { get; set; }
        //	Tên dạng bào chế				
        public string DOSFORM_NAME { get; set; }
        //	Dạng bào chế đã đăng ký				
        public string REG_DOSFORM { get; set; }
        //	Thành phần				
        public string ING { get; set; }
        //	Chức năng và chỉ định				
        public string EFCC_ATD { get; set; }
        //	Tính trạng				
        public string CHRT { get; set; }
        //	Quy cách thuốc				
        public string DRUG_SPEC { get; set; }
        //	Mã quy cách thuốc				
        public string DRUG_SPEC_CODE { get; set; }
        //	Quy cách đã đăng ký				
        public string REG_SPEC { get; set; }
        //	Mã quy cách đã đăng ký				
        public string REG_SPEC_CODE { get; set; }
        //	Đường dùng thuốc				
        public string RUTE { get; set; }
        //	Bảo quản				
        public string STOG { get; set; }
        //	Tần suất sử dụng				
        public string USED_FRQU { get; set; }
        //	Liều lượng mỗi lần				
        public string EACH_DOS { get; set; }
        //	Loại thuốc				
        public string DRUG_TYPE { get; set; }
        //	Tên loại thuốc				
        public string DRUG_TYPE_NAME { get; set; }
        //	Cờ thuốc không kê đơn				
        public string OTC_FLAG { get; set; }
        //	Tên cờ thuốc không kê đơn				
        public string OTC_FLAG_NAME { get; set; }
        //	Chất liệu đóng gói				
        public string PACMATL { get; set; }
        //	Tên chất liệu đóng gói				
        public string PACMATL_NAME { get; set; }
        //	Quy cách đóng gói				
        public string PACSPEC { get; set; }
        //	Hướng dẫn sử dụng				
        public string MANL { get; set; }
        //	Số lượng đóng gói				
        public string PAC_CNT { get; set; }
        //	Đơn vị sử dụng nhỏ nhất				
        public string MIN_USEUNT { get; set; }
        //	Đơn vị bán nhỏ nhất				
        public string MIN_SALUNT { get; set; }
        //	Đơn vị đo nhỏ nhất				
        public string MIN_UNT { get; set; }
        //	Số lượng đóng gói nhỏ nhất				
        public int? MIN_PAC_CNT { get; set; }
        //	Đơn vị đóng gói nhỏ nhất				
        public string MIN_PACUNT { get; set; }
        //	Đơn vị chế phẩm nhỏ nhất				
        public string MIN_PREPUNT { get; set; }
        //	Tên đơn vị chế phẩm nhỏ nhất				
        public string MIN_PREPUNT_NAME { get; set; }
        //	Hạn dùng thuốc				
        public string DRUG_EXPY { get; set; }
        //	Đơn vị tính giá nhỏ nhất				
        public string MIN_PRCUNT { get; set; }
        //	Phản ứng có hại				
        public string DEFS { get; set; }
        //	Lưu ý				
        public string MNAN { get; set; }
        //	Chống chỉ định				
        public string TABO { get; set; }
        //	Mã doanh nghiệp sản xuất				
        public string PRODENTP_CODE { get; set; }
        //	Tên doanh nghiệp sản xuất				
        public string PRODENTP_NAME { get; set; }
        //	Địa chỉ doanh nghiệp sản xuất				
        public string PRODENTP_ADDR { get; set; }
        //	Cờ thuốc giới hạn đặc biệt				
        public string SP_LMTPRIC_DRUG_FLAG { get; set; }
        //	Số văn bản phê duyệt				
        public string APRVNO { get; set; }
        //	Ngày bắt đầu số văn bản phê duyệt				
        public DateTime? APRVNO_BEGNDATE { get; set; }
        //	Ngày kết thúc số văn bản phê duyệt				
        public DateTime? APRVNO_ENDDATE { get; set; }
        //	Số chứng nhận đăng ký thuốc				
        public string DRUG_REGNO { get; set; }
        //	Ngày bắt đầu số chứng nhận đăng ký thuốc				
        public DateTime? DRUG_REGCERT_BEGNDATE { get; set; }
        //	Ngày kết thúc số chứng nhận đăng ký thuốc				
        public DateTime? DRUG_REGCERT_ENDDATE { get; set; }
        //	Tỷ lệ chuyển đổi				
        public int? CONVRAT { get; set; }
        //	Phạm vi hạn chế sử dụng				
        public string LMT_USESCP { get; set; }
        //	Tên đơn vị đóng gói nhỏ nhất				
        public string MINPACUNT_NAME { get; set; }
        //	Tên đăng ký				
        public string REG_NAME { get; set; }
        //	Nhà sản xuất đóng gói riêng				
        public string SUBPCK_FCTY { get; set; }
        //	Trạng thái thị trường				
        public string MKT_STAS { get; set; }
        //	Hồ sơ điện tử văn bản phê duyệt đăng ký thuốc				
        public string DRUG_REG_APPVLETR_ELECACS { get; set; }
        //	Hồ sơ điện tử văn bản phê duyệt đăng ký bổ sung thuốc				
        public string SPLM_APPY_APPVLETR_FILE { get; set; }
        //	Số danh mục thuốc BHYT quốc gia				
        public string NAT_HI_DRUGLIST_NO { get; set; }
        //	Ghi chú danh mục thuốc BHYT quốc gia				
        public string NAT_HI_DRUGLIST_MEMO { get; set; }
        //	Cờ thuốc điều chỉnh thuế GTGT				
        public string ADVALTAX_ADJM_DRUG_FLAG { get; set; }
        //	Tên thuốc điều chỉnh thuế GTGT				
        public string ADVALTAX_ADJM_DRUG_NAME { get; set; }
        //	Thuốc trong danh mục thuốc đã lưu hành				
        public string LSTD_DRUGLIST_DRUG { get; set; }
        //	Mã thuốc của Ủy ban Y tế				
        public string NHC_DRUG_CODG { get; set; }
        //	Ghi chú				
        public string MEMO { get; set; }
        //	Cờ hiệu lực				
        public string VALI_FLAG { get; set; }
        //	Thời gian bắt đầu				
        public DateTime? BEGNTIME { get; set; }
        //	Thời gian kết thúc				
        public DateTime? ENDTIME { get; set; }
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
        //public DateTime OPT_TIME { get; set; }
        ////	Mã cơ sở xử lý				
        //public string OPTINS_NO { get; set; }
        //	Số phiên bản				
        public string VER { get; set; }
        //	Tên phiên bản				
        public string VER_NAME { get; set; }
        //	Số giấy phép chế phẩm tự chế				
        public string SELFPREP_PMTNO { get; set; }
        //	Thuốc dùng cho trẻ em				
        public string CHLD_MEDC { get; set; }
        //	Thuốc dùng cho bệnh nhân cao tuổi				
        public string ELD_PATN_MEDC { get; set; }
        //	Tên người liên hệ cơ sở y tế				
        public string MEDINS_CONER_NAME { get; set; }
        //	Điện thoại người liên hệ cơ sở y tế				
        public string MEDINS_CONER_TEL { get; set; }
        //	Hồ sơ điện tử giấy phép chế phẩm tự chế				
        public string SELFPREP_PMTNO_ELECACS { get; set; }
        ////	Khu vực nơi ở				
        //public string REGN { get; set; }
        ////	Cờ ban hành				
        //public string ISU_FLAG { get; set; }
        ////	Truyền dữ liệuID				
        //public string TRAM_DATA_ID { get; set; }
        ////	Thời gian có hiệu lực				
        //public DateTime? EFFT_TIME { get; set; }
        ////	Thời gian hết hiệu lực				
        //public DateTime? INVD_TIME { get; set; }
        ////	Tên đơn vị đăng ký chế phẩm bệnh viện				
        //public string HOSP_PREP_APPYER_EMP_NAME { get; set; }
        ////	Địa chỉ đơn vị đăng ký chế phẩm bệnh viện				
        //public string HOSP_PREP_APPYER_EMP_ADDR { get; set; }

    }
}
