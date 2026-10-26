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
        /// Enum 1 Đơn thuốc tây 2 Đơn thuốc đông y 3 Đơn thuốc phục hồi chức năng 4 Đơn xét nghiệm 5 Đơn kiểm tra 6 Đơn truyền dịch
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
        /// Chi tiết đơn thuốc
        /// </summary>
        public List<SqtxCFYP> ypcfmxList { get; set; }

        public List<SqtxCFXM> xmcfmxList { get; set; }
    }
}
