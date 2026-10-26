using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng danh mục chẩn đoán
    /// </summary>
    public class G_yb_diag_list_bVO
    {
        //	Danh mục chẩn đoánID				
        public string DIAG_LIST_ID { get; set; }
        
        //	Chương				
        public string CPR { get; set; }
        //	Phạm vi mã chương				
        public string CPR_CODE_SCP { get; set; }
        //	Tên chương				
        public string CPR_NAME { get; set; }
        //	Phạm vi mã tiết				
        public string SEC_CODE_SCP { get; set; }
        //	Tên tiết				
        public string SEC_NAME { get; set; }
        //	Mã danh mục				
        public string CGY_CODE { get; set; }
        //	Tên danh mục				
        public string CGY_NAME { get; set; }
        //	Mã tiểu mục				
        public string SOR_CODE { get; set; }
        //	Tên tiểu mục				
        public string SOR_NAME { get; set; }
        //	Mã chẩn đoán				
        public string DIAG_CODE { get; set; }
        //	Tên chẩn đoán				
        public string DIAG_NAME { get; set; }
        //	Dấu sử dụng				
        public string USED_STD { get; set; }
        //	Mã chẩn đoán bản tiêu chuẩn quốc gia				
        public string NATSTD_DIAG_CODE { get; set; }
        //	Tên chẩn đoán bản tiêu chuẩn quốc gia				
        public string NATSTD_DIAG_NAME { get; set; }
        //	Mã chẩn đoán bản lâm sàng				
        public string CLNC_DIAG_CODE { get; set; }
        //	Tên chẩn đoán bản lâm sàng				
        public string CLNC_DIAG_NAME { get; set; }
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
        
        //	Số phiên bản				
        public string VER { get; set; }
        //	Tên phiên bản				
        public string VER_NAME { get; set; }
        

    }
}
