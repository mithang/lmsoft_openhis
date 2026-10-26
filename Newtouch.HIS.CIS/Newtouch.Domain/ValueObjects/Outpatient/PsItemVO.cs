using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Thông tin hạng mục thử da
    /// </summary>
    public class PsItemVO
    {
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốcid
        /// </summary>
        public string cfmxId { get; set; }

        /// <summary>
        /// Hạng mụcCode
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Tên hạng mục
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
        /// Người thực hiện
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// Thông tin dị ứngId
        /// </summary>
        public string gmxxId { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Kết quả thực hiện
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
