using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// dathửMụcthông tin
    /// </summary>
    public class PsItemVO
    {
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Đơn thuốcchi tiếtid
        /// </summary>
        public string cfmxId { get; set; }

        /// <summary>
        /// MụcCode
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Tên mục
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Thực hiện người
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// quamẫnthông tinId
        /// </summary>
        public string gmxxId { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Thực hiệnkếtnếu
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal? ypjl { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Nội dung y lệnh
        /// </summary>
        public string yznr { get; set; }

        /// <summary>
        /// Y lệnhId
        /// </summary>
        public string yzId { get; set; }
    }
}
