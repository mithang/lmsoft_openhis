using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng danh mục điều trị phẫu thuật trong ngày của BHYT
    /// </summary>
    public class G_yb_daysrg_trt_list_bVO
    {
        /// <summary>
        /// Danh mục điều trị phẫu thuật trong ngàyID
        /// </summary>
        public string DAYSRG_TRT_LIST_ID { get; set; }
        /// <summary>
        /// Mã danh mục loại bệnh phẫu thuật trong ngày
        /// </summary>
        public string DAYSRG_DISE_LIST_CODE { get; set; }
        /// <summary>
        /// Tên loại bệnh phẫu thuật trong ngày
        /// </summary>
        public string DAYSRG_DISE_NAME { get; set; }
        /// <summary>
        /// Cờ hiệu lực
        /// </summary>
        public string VALI_FLAG { get; set; }
        /// <summary>
        /// Số bản ghi duy nhất của dữ liệu
        /// </summary>
        public string RID { get; set; }

        /// <summary>
        /// Thời gian tạo dữ liệu
        /// </summary>
        public DateTime CRTE_TIME { get; set; }
        /// <summary>
        ///  Thời gian cập nhật dữ liệu
        /// </summary>
        public DateTime UPDT_TIME { get; set; }
        ///// <summary>
        ///// Người tạoID
        ///// </summary>
        //public string CRTER_ID { get; set; }
        ///// <summary>
        ///// Tên người tạo
        ///// </summary>
        //public string CRTER_NAME { get; set; }
        ///// <summary>
        /////   Mã cơ sở tạo
        ///// </summary>
        //public string CRTE_OPTINS_NO { get; set; }
        ///// <summary>
        ///// Người xử lýID
        ///// </summary>
        //public string OPTER_ID { get; set; }
        ///// <summary>
        /////  Tên người xử lý
        ///// </summary>
        //public string OPTER_NAME { get; set; }
        ///// <summary>
        /////  Thời gian xử lý
        ///// </summary>
        //public DateTime OPT_TIME { get; set; }
        ///// <summary>
        /////  Mã cơ sở xử lý
        ///// </summary>
        //public string OPTINS_NO { get; set; }
        /// <summary>
        ///  Số phiên bản
        /// </summary>
        public string VER { get; set; }
        /// <summary>
        ///  Nội hàm loại bệnh
        /// </summary>
        public string DISE_CONT { get; set; }
        /// <summary>
        ///  Ghi chú
        /// </summary>
        public string MEMO { get; set; }
        /// <summary>
        ///  Tên phiên bản
        /// </summary>
        public string VER_NAME { get; set; }
        /// <summary>
        ///  Số trang hướng dẫn khám chữa bệnh
        /// </summary>
        public string TRT_GUIDE_PAGEN { get; set; }
        /// <summary>
        /// Hồ sơ điện tử hướng dẫn khám chữa bệnh
        /// </summary>
        public string TRT_GUIDE_ELECACS { get; set; }
        /// <summary>
        ///  Mã phẫu thuật giới hạn
        /// </summary>
        public string QUA_OPRN_OPRT_CODE { get; set; }
        /// <summary>
        ///  Tên phẫu thuật giới hạn
        /// </summary>
        public string QUA_OPRN_OPRT_NAME { get; set; }
        ///// <summary>
        /////  Cờ ban hành
        ///// </summary>
        //public string ISU_FLAG { get; set; }
        ///// <summary>
        /////  Truyền dữ liệuID
        ///// </summary>
        //public string TRAM_DATA_ID { get; set; }
        ///// <summary>
        /////  Thời gian có hiệu lực
        ///// </summary>
        //public DateTime? EFFT_TIME { get; set; }
        ///// <summary>
        /////  Thời gian hết hiệu lực
        ///// </summary>
        //public DateTime? INVD_TIME { get; set; }
    }
}
