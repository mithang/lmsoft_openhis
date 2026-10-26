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
    /// mẫu tin nhắn kiểm soát chất lượng bệnh án
    /// </summary>
    public class MrqcMsgNoticeModel: MsgNoticeModel
    {
        /// <summary>
        /// họ tên
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// số khám bệnh
        /// </summary>
        public string? jzh { get; set; }
        /// <summary>
        /// khoa
        /// </summary>
        public string? ks { get; set; }
        /// <summary>
        /// số giường
        /// </summary>
        public string? ch { get; set; }
        /// <summary>
        /// bác sĩ nội trú
        /// </summary>
        public string? zyys { get; set; }
        /// <summary>
        /// loại kiểm soát chất lượng
        /// </summary>
        public string? zklx { get; set; }
        /// <summary>
        /// tài liệu bệnh án bệnh nhân
        /// </summary>
        public string? hzwd { get; set; }
        /// <summary>
        /// nội dung phản hồi
        /// </summary>
        public string? fknr { get; set; }
        /// <summary>
        /// mức nghiêm trọng
        /// </summary>
        public string? wtdj { get; set; }
        /// <summary>
        /// thời hạn xử lý
        /// </summary>
        public string? qxclsj { get; set; }
        /// <summary>
        /// ngày kiểm soát chất lượng
        /// </summary>
        public string? zkrq { get; set; }

    }
}
