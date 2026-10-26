using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng phân loại và mã phẫu thuật
    /// </summary>
    public class G_yb_oprn_std_bVO
    {
        //	Danh mục tiêu chuẩn phẫu thuậtID				
        public string OPRN_STD_LIST_ID { get; set; }
        //	Tên phiên bản				
        public string VER_NAME { get; set; }
        //	Chương				
        public string CPR { get; set; }
        //	Phạm vi mã chương				
        public string CPR_CODE_SCP { get; set; }
        //	Tên chương				
        public string CPR_NAME { get; set; }
        //	Mã danh mục				
        public string CGY_CODE { get; set; }
        //	Tên danh mục				
        public string CGY_NAME { get; set; }
        //	Mã tiểu mục				
        public string SOR_CODE { get; set; }
        //	Tên tiểu mục				
        public string SOR_NAME { get; set; }
        //	Mã tiểu mục				
        public string DTLS_CODE { get; set; }
        //	Tên tiểu mục				
        public string DTLS_NAME { get; set; }
        //	Mã phẫu thuật				
        public string OPRN_OPRT_CODE { get; set; }
        //	Tên phẫu thuật				
        public string OPRN_OPRT_NAME { get; set; }
        //	Dấu sử dụng				
        public string USED_STD { get; set; }
        //	Mã phẫu thuật bản tiêu chuẩn đoàn thể				
        public string RTL_OPRN_OPRT_CODE { get; set; }
        //	Tên phẫu thuật bản tiêu chuẩn đoàn thể				
        public string RTL_OPRN_OPRT_NAME { get; set; }
        //	Mã phẫu thuật bản lâm sàng				
        public string CLNC_OPRN_OPRT_CODE { get; set; }
        //	Tên phẫu thuật bản lâm sàng				
        public string CLNC_OPRN_OPRT_NAME { get; set; }
        //	Ghi chú				
        public string MEMO { get; set; }
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
        //public DateTime OPT_TIME { get; set; }
        ////	Mã cơ sở xử lý				
        //public string OPTINS_NO { get; set; }
        //	Số phiên bản				
        public string VER { get; set; }
        //	Cờ ban hành				
        //public string ISU_FLAG { get; set; }
        ////	Truyền dữ liệuID				
        //public string TRAM_DATA_ID { get; set; }
        ////	Thời gian có hiệu lực				
        //public DateTime? EFFT_TIME { get; set; }
        ////	Thời gian hết hiệu lực				
        //public DateTime? INVD_TIME { get; set; }

    }
}
