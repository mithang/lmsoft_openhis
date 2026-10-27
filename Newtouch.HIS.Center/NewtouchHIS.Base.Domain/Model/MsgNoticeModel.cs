using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Model
{
    public class MsgNoticeModel
    {
         

    }
    /// <summary>
    /// Kiểm soát chất lượng bệnh ánTin nhắnMẫu
    /// </summary>
    public class MrqcMsgNoticeModel: MsgNoticeModel
    {
        /// <summary>
        /// Họ tên
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// Số khám
        /// </summary>
        public string? jzh { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string? ks { get; set; }
        /// <summary>
        /// Số giường
        /// </summary>
        public string? ch { get; set; }
        /// <summary>
        /// Bác sĩ nội trú
        /// </summary>
        public string? zyys { get; set; }
        /// <summary>
        /// Loại kiểm soát chất lượng
        /// </summary>
        public string? zklx { get; set; }
        /// <summary>
        /// Bệnh nhânTài liệu bệnh án
        /// </summary>
        public string? hzwd { get; set; }
        /// <summary>
        /// Nội dung phản hồi
        /// </summary>
        public string? fknr { get; set; }
        /// <summary>
        /// Mức độ nghiêm trọng
        /// </summary>
        public string? wtdj { get; set; }
        /// <summary>
        /// Thời hạn xử lý
        /// </summary>
        public string? qxclsj { get; set; }
        /// <summary>
        /// Ngày kiểm soát chất lượng
        /// </summary>
        public string? zkrq { get; set; }

    }
}
