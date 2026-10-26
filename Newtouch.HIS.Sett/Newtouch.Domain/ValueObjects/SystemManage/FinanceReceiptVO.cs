using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class FinanceReceiptVO
    {
        /// <summary>
        /// Biên lai tài chínhID
        /// </summary>
        public string cwsjId { get; set; }

        /// <summary>
        /// Nhân viên
        /// </summary>
        public string ry { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Chữ cái đầu
        /// </summary>
        public string szm { get; set; }

        /// <summary>
        /// Số biên lai bắt đầu
        /// </summary>
        public long qssjh { get; set; }

        /// <summary>
        /// Số biên lai hiện tại
        /// </summary>
        public long dqsjh { get; set; }

        /// <summary>
        /// Số biên lai kết thúc
        /// </summary>
        public long? jssjh { get; set; }

        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }

    public class FinanceInvoiceVO
    {
        public string fpdm { get; set; }
        public string szm { get; set; }
        public string lyry { get; set; }
        public long dqfph { get; set; }
        public long qsfph { get; set; }
        public long jsfph { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public string ry { get; set; }
    }
}
