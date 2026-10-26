using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng chẩn đoán Trung y
    /// </summary>
    public class G_yb_tcm_diag_bVO
    {
        //	Chẩn đoán Trung yID				
        public string TCM_DIAG_ID { get; set; }
        //	Tên phiên bản				
        public string VER_NAME { get; set; }
        //	Mã danh mục khoa				
        public string CATY_CGY_CODE { get; set; }
        //	Tên danh mục khoa				
        public string CATY_CGY_NAME { get; set; }
        //	Mã phân loại hệ thống chuyên khoa				
        public string SPCY_SYS_TAXA_CODE { get; set; }
        //	Tên phân loại hệ thống chuyên khoa				
        public string SPCY_SYS_TAXA_NAME { get; set; }
        //	Mã loại loại bệnh				
        public string DISE_TYPE_CODE { get; set; }
        //	Tên loại loại bệnh				
        public string DISE_TYPE_NAME { get; set; }
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
