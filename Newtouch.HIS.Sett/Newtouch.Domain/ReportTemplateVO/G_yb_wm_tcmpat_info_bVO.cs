using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng thông tin thuốc Tây y và thuốc thành phẩm Trung y
    /// </summary>
    public class G_yb_wm_tcmpat_info_bVO
    {
        //	Mã danh mục y tế				
        public string MED_LIST_CODG { get; set; }
        //	Tên thương mại thuốc				
        public string DRUG_PRODNAME { get; set; }
        //	Mã tên chung				
        public string GENNAME_CODG { get; set; }
        //	Tên chung của thuốc				
        public string DRUG_GENNAME { get; set; }
        //	Tên hóa học				
        public string CHEMNAME { get; set; }
        //	Tên khác				
        public string ALIS { get; set; }
        //	Tên tiếng Anh				
        public string ENG_NAME { get; set; }
        //	Tên đăng ký				
        public string REG_NAME { get; set; }
        //	Mã chuẩn thuốc				
        public string DRUGSTDCODE { get; set; }
        //	Dạng bào chế thuốc				
        public string DRUG_DOSFORM { get; set; }
        //	Tên dạng bào chế thuốc				
        public string DRUG_DOSFORM_NAME { get; set; }
        //	Loại thuốc				
        public string DRUG_TYPE { get; set; }
        //	Tên loại thuốc				
        public string DRUG_TYPE_NAME { get; set; }
        //	Quy cách thuốc				
        public string DRUG_SPEC { get; set; }
        //	Mã quy cách thuốc				
        public string DRUG_SPEC_CODE { get; set; }
        //	Dạng bào chế đã đăng ký				
        public string REG_DOSFORM { get; set; }
        //	Quy cách đã đăng ký				
        public string REG_SPEC { get; set; }
        //	Mã quy cách đã đăng ký				
        public string REG_SPEC_CODE { get; set; }
        //	Liều lượng mỗi lần				
        public string EACH_DOS { get; set; }
        //	Tần suất sử dụng				
        public string USED_FRQU { get; set; }
        //	Gốc axit-bazơ				
        public string ACDBAS { get; set; }
        //	Mã thuốc quốc gia				
        public string NAT_DRUG_NO { get; set; }
        //	Cách dùng				
        public string USED_MTD { get; set; }
        //	Cờ thuốc thành phẩm Trung y				
        public string TCMPAT_FLAG { get; set; }
        //	Loại nơi sản xuất				
        public string PRODPLAC_TYPE { get; set; }
        //	Tên loại nơi sản xuất				
        public string PRODPLAC_TYPE_NAME { get; set; }
        //	Loại đơn vị tính giá				
        public string PRCUNT_TYPE { get; set; }
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
        //	Số lượng đóng gói				
        public string PAC_CNT { get; set; }
        //	Chức năng và chỉ định				
        public string EFCC_ATD { get; set; }
        //	Đường dùng thuốc				
        public string RUTE { get; set; }
        //	Hướng dẫn sử dụng				
        public string MANL { get; set; }
        //	Ngày bắt đầu				
        public string BEGNDATE { get; set; }
        //	Ngày kết thúc				
        public string ENDDATE { get; set; }
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
        //	Tên đơn vị đóng gói nhỏ nhất				
        public string MINPACUNT_NAME { get; set; }
        //	Tên đơn vị chế phẩm nhỏ nhất				
        public string MIN_PREPUNT_NAME { get; set; }
        //	Tỷ lệ chuyển đổi				
        public int? CONVRAT { get; set; }
        //	Hạn dùng thuốc				
        public string DRUG_EXPY { get; set; }
        //	Đơn vị tính giá nhỏ nhất				
        public string MIN_PRCUNT { get; set; }
        //	Mã ghi nhớ ngũ bút				
        public string WUBI { get; set; }
        //	Mã ghi nhớ pinyin				
        public string PINYIN { get; set; }
        //	Nhà sản xuất đóng gói riêng				
        public string SUBPCK_FCTY { get; set; }
        //	Mã doanh nghiệp sản xuất				
        public string PRODENTP_CODE { get; set; }
        //	Tên doanh nghiệp sản xuất				
        public string PRODENTP_NAME { get; set; }
        //	Cờ thuốc giới hạn đặc biệt				
        public string SP_LMTPRIC_DRUG_FLAG { get; set; }
        //	Cờ thuốc đặc biệt				
        public string SP_DRUG_FLAG { get; set; }
        //	Phạm vi hạn chế sử dụng				
        public string LMT_USESCP { get; set; }
        //	Cờ hạn chế sử dụng				
        public string LMT_USED_FLAG { get; set; }
        //	Số chứng nhận đăng ký thuốc				
        public string DRUG_REGNO { get; set; }
        //	Ngày bắt đầu số chứng nhận đăng ký thuốc				
        public DateTime? DRUG_REGCERT_BEGNDATE { get; set; }
        //	Ngày kết thúc số chứng nhận đăng ký thuốc				
        public DateTime? DRUG_REGCERT_ENDDATE { get; set; }
        //	Số văn bản phê duyệt				
        public string APRVNO { get; set; }
        //	Ngày bắt đầu số văn bản phê duyệt				
        public DateTime? APRVNO_BEGNDATE { get; set; }
        //	Ngày kết thúc số văn bản phê duyệt				
        public DateTime? APRVNO_ENDDATE { get; set; }
        //	Trạng thái thị trường				
        public string MKT_STAS { get; set; }
        //	Tên trạng thái thị trường				
        public string MKT_STAS_NAME { get; set; }
        //	Hồ sơ điện tử văn bản phê duyệt đăng ký thuốc				
        public string DRUG_REG_APPVLETR_ELECACS { get; set; }
        //	Hồ sơ điện tử văn bản phê duyệt đăng ký bổ sung thuốc				
        public string SPLM_APPY_APPVLETR_FILE { get; set; }
        //	Ghi chú danh mục thuốc BHYT quốc gia				
        public string NAT_HI_DRUGLIST_MEMO { get; set; }
        //	Tên cờ thuốc cơ bản				
        public string BAS_MEDN_FLAG_NAME { get; set; }
        //	Cờ thuốc cơ bản				
        public string BAS_MEDN_FLAG { get; set; }
        //	Cờ thuốc điều chỉnh thuế GTGT				
        public string ADVALTAX_ADJM_DRUG_FLAG { get; set; }
        //	Tên thuốc điều chỉnh thuế GTGT				
        public string ADVALTAX_ADJM_DRUG_NAME { get; set; }
        //	Thuốc trong danh mục thuốc đã lưu hành				
        public string LSTD_DRUGLIST_DRUG { get; set; }
        //	Cờ thuốc đàm phán BHYT				
        public string HI_NEGO_DRUG_FLAG { get; set; }
        //	Tên thuốc đàm phán BHYT				
        public string HI_NEGO_DRUG_NAME { get; set; }
        //	Mã thuốc của Ủy ban Y tế				
        public string NHC_DRUG_CODG { get; set; }
        //	Ghi chú				
        public string MEMO { get; set; }
        //	Cờ hiệu lực				
        public string VALI_FLAG { get; set; }
        //	Số bản ghi duy nhất của dữ liệu				
        public string RID { get; set; }
        //	Thời gian tạo dữ liệu				
        public DateTime? CRTE_TIME { get; set; }
        //	Thời gian cập nhật dữ liệu				
        public DateTime? UPDT_TIME { get; set; }
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
        //	Thuốc dùng cho trẻ em				
        public string CHLD_MEDC { get; set; }
        //	Tên công ty				
        public string CO_NAME { get; set; }
        //	Thuốc generic đánh giá tính nhất quán				
        public string CONSEVAL_DRUG { get; set; }
        //	Doanh nghiệp phân phối				
        public string DSTR { get; set; }
        //	Người liên hệ doanh nghiệp phân phối				
        public string DSTR_CONER { get; set; }
        //	Hồ sơ điện tử giấy ủy quyền doanh nghiệp phân phối				
        public string DSTR_AUTH_FILE_ELECACS { get; set; }
        //	Dạng bào chế danh mục thuốc BHYT quốc gia				
        public string NAT_HI_DRUGLIST_DOSFORM { get; set; }
        //	Dấu loại Giáp/Ất danh mục thuốc BHYT quốc gia				
        public string NAT_HI_DRUGLIST_CHRGITM_LV { get; set; }
        ////	Người giữ giấy phép lưu hành				
        //public string LSTD_LIC_HOLDER { get; set; }
        ////	Cờ ban hành				
        //public string ISU_FLAG { get; set; }
        ////	Truyền dữ liệuID				
        //public string TRAM_DATA_ID { get; set; }
        ////	Thời gian có hiệu lực				
        //public DateTime? EFFT_TIME { get; set; }
        ////	Thời gian hết hiệu lực				
        //public DateTime? INVD_TIME { get; set; }

    }
    public class G_yb_mluCommon_Info: G_yb_wm_tcmpat_info_bVO
    {
        #region 1301
        public string TCMHERB_NAME { get; set; }
        public string MED_PART { get; set; }
        public string CNVL_USED { get; set; }
        public string NATFLA { get; set; }
        public string CAT { get; set; }
        public string MLMS_NAME { get; set; }
        public string PSDG_MTD { get; set; }
        public string ECY_TYPE { get; set; }
        public string MLMS_CAT_SOUC { get; set; }
        #endregion

        #region 1302
        public string DOSFORM { get; set; }
        public string BEGNTIME { get; set; }
        public string ENDTIME { get; set; }
        #endregion

        #region 1305
        public string PRCUNT { get; set; }
        public string PRCUNT_NAME { get; set; }
        public string TRT_ITEM_DSCR { get; set; }
        public string TRT_EXCT_CONT { get; set; }
        public string SERVITEM_TYPE { get; set; }
        public string SERVITEM_NAME { get; set; }
        #endregion

        #region 1306
        public string HI_GENNAME { get; set; }
        public string SPEC { get; set; }
        public string MCS_MATL { get; set; }
        #endregion

        #region 1307
        public string DIAG_LIST_ID { get; set; }
        public string CPR { get; set; }
        public string CPR_CODE_SCP { get; set; }
        public string CPR_NAME { get; set; }
        public string SEC_CODE_SCP { get; set; }
        public string SEC_NAME { get; set; }
        public string CGY_CODE { get; set; }
        public string CGY_NAME { get; set; }
        public string SOR_CODE { get; set; }
        public string SOR_NAME { get; set; }
        public string DIAG_CODE { get; set; }
        public string DIAG_NAME { get; set; }
        public string USED_STD { get; set; }
        #endregion

        #region 1308
        public string OPRN_STD_LIST_ID { get; set; }
        public string OPRN_OPRT_CODE { get; set; }
        public string OPRN_OPRT_NAME { get; set; }
        public string DTLS_CODE { get; set; }
        public string DTLS_NAME { get; set; }
        #endregion

        #region 1309
        public string OPSP_DISE_CODE { get; set; }
        public string OPSP_DISE_MAJCLS_NAME { get; set; }
        public string OPSP_DISE_SUBD_CLSS_NAME { get; set; }
        public string ADMDVS { get; set; }
        public string OPSP_DISE_NAME { get; set; }
        public string OPSP_DISE_MAJCLS_CODE { get; set; }
        #endregion

        #region 1310
        public string DISE_SETL_LIST_ID { get; set; }
        public string BYDISE_SETL_LIST_CODE { get; set; }
        public string BYDISE_SETL_DISE_NAME { get; set; }
        public string QUA_OPRN_OPRT_CODE { get; set; }
        public string QUA_OPRN_OPRT_NAME { get; set; }

        #endregion
        #region 1311
        public string DAYSRG_TRT_LIST_ID { get; set; }
        public string DAYSRG_DISE_LIST_CODE { get; set; }

        public string DAYSRG_DISE_NAME { get; set; }
        #endregion

        #region 1313
        public string TMOR_MPY_ID { get; set; }
        public string TMOR_CELL_TYPE_CODE { get; set; }

        public string TMOR_CELL_TYPE { get; set; }
        public string MPY_TYPE_CODE { get; set; }
        public string MPY_TYPE { get; set; }
        #endregion

        #region 1314
        public string TCM_DIAG_ID { get; set; }
        public string CATY_CGY_CODE { get; set; }
        public string CATY_CGY_NAME { get; set; }
        public string SPCY_SYS_TAXA_CODE { get; set; }
        public string SPCY_SYS_TAXA_NAME { get; set; }
        public string DISE_TYPE_CODE { get; set; }
        public string DISE_TYPE_NAME { get; set; }
        #endregion

        #region 1315
        public string TCMSYMP_ID { get; set; }
        public string SDS_CGY_CODE { get; set; }
        public string SDS_CGY_NAME { get; set; }
        public string SDS_ATTR_CODE { get; set; }
        public string SDS_ATTR { get; set; }
        public string SDS_TYPE_CODE { get; set; }
        public string SDS_TYPE_NAME { get; set; }
        #endregion

        #region 1320
        public string ZYPFKL_CODE { get; set; }
        public string ZYPFKL_NAME { get; set; }
        public string DDYC { get; set; }
        public string GG { get; set; }
        public string ZYPFKLZXBZ { get; set; }
        public string BLFYJCXX { get; set; }
        public string ZYYP_CODE { get; set; }
        public string ZYYP_NAME { get; set; }
        public string YC_NAME { get; set; }
        public string GXFL { get; set; }
        public string GNZZ { get; set; }
        public string CGYF { get; set; }
        public string SJGXSJ { get; set; }
        public string CJJG_CODE { get; set; }
        public string YBQH { get; set; }

        #endregion


        #region 1321
        public string YLML_CODE { get; set; }
        public string YLFWXM_NAME { get; set; }
        public string YLFWXMSC { get; set; }
        public string JGGC { get; set; }
        public string JJDW { get; set; }
        public string JJSM { get; set; }
        public string ZLXMSM { get; set; }
        public string FYLXKJ { get; set; }
        public string EFFECTIVE_TIME { get; set; }
        public string EXPIRATION_TIME { get; set; }
        #endregion
    }
}
