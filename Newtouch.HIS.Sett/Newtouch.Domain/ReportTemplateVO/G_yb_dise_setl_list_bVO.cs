using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng danh mục quyết toán BHYT theo loại bệnh
    /// </summary>
    public class G_yb_dise_setl_list_bVO
    {
        //	Danh mục quyết toán loại bệnhID				
        public string DISE_SETL_LIST_ID { get; set; }
        //	Mã danh mục loại bệnh quyết toán theo loại bệnh				
        public string BYDISE_SETL_LIST_CODE { get; set; }
        //	Tên loại bệnh quyết toán theo loại bệnh				
        public string BYDISE_SETL_DISE_NAME { get; set; }
        //	Mã phẫu thuật giới hạn				
        public string QUA_OPRN_OPRT_CODE { get; set; }
        //	Tên phẫu thuật giới hạn				
        public string QUA_OPRN_OPRT_NAME { get; set; }
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
        //	Nội hàm loại bệnh				
        public string DISE_CONT { get; set; }
        //	Ghi chú				
        public string MEMO { get; set; }
        //	Tên phiên bản				
        public string VER_NAME { get; set; }
        //	Số trang hướng dẫn khám chữa bệnh				
        public string TRT_GUIDE_PAGEN { get; set; }
        //	Hồ sơ điện tử hướng dẫn khám chữa bệnh				
        public string TRT_GUIDE_ELECACS { get; set; }
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
