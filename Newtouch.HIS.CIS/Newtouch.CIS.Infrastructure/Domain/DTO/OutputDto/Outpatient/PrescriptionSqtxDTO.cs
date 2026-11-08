using Newtouch.Domain.ValueObjects;
using newtouchyibao.Models;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    public class PrescriptionSqtxDTO
    {

        public int CFLX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JZLX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MZH { get; set; }

        /// <summary>
        /// Liệt kê 1 Thuốc Tây Đơn thuốc 2 Thuốc Đông y Đơn thuốc 3 Phục hồi chức năng Đơn thuốc 4 Xét nghiệm Đơn thuốc 5 Kiểm tra Đơn thuốc 6 truyền dịch Đơn thuốc
        /// </summary>
        public string CFH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YYKSBM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YYKSMC { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string YYYSGH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YSXM { get; set; }

        /// <summary>
        /// Đơn thuốc chi tiết
        /// </summary>
        public List<SqtxCFYP> ypcfmxList { get; set; }

        public List<SqtxCFXM> xmcfmxList { get; set; }
    }
}
