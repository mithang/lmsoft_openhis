using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng hạng mục dịch vụ y tế
    /// </summary>
    public class G_yb_trt_serv_bVO
    {
        //	Mã danh mục y tế				
        public string MED_LIST_CODG { get; set; }
        //	Đơn vị tính giá				
        public string PRCUNT { get; set; }
        //	Tên đơn vị tính giá				
        public string PRCUNT_NAME { get; set; }
        
        //	Giải thích hạng mục khám chữa bệnh				
        public string TRT_ITEM_DSCR { get; set; }
        //	Nội dung loại trừ khám chữa bệnh				
        public string TRT_EXCT_CONT { get; set; }
        //	Nội hàm hạng mục khám chữa bệnh				
        public string TRT_ITEM_CONT { get; set; }
        //	Cờ hiệu lực				
        public string VALI_FLAG { get; set; }
        //	Ghi chú				
        public string MEMO { get; set; }
        //	Loại hạng mục dịch vụ				
        public string SERVITEM_TYPE { get; set; }
        //	Tên hạng mục dịch vụ y tế				
        public string SERVITEM_NAME { get; set; }
        //	Giải thích hạng mục				
        public string ITEM_DSCR { get; set; }
        //	Ngày bắt đầu				
        public DateTime? BEGNDATE { get; set; }
        //	Ngày kết thúc				
        public DateTime? ENDDATE { get; set; }
        //	Số bản ghi duy nhất của dữ liệu				
        public string RID { get; set; }
        
        //	Số phiên bản				
        public string VER { get; set; }
        //	Tên phiên bản				
        public string VER_NAME { get; set; }
       

    }
}
