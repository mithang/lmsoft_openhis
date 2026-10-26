using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// Tổ hợp hạng mục điều trị lịch trực ngoại trú
    /// </summary>
    public class MzpbZlxmzh
    {

        public string OrganizeId { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// Tên tổ hợp khám chữa bệnh
        /// </summary>
        public string zhmc { get; set; }
        /// <summary>
        /// Mã tổ hợp khám chữa bệnh
        /// </summary>
        public string zhcode { get; set; }
        /// <summary>
        /// Sắp xếp hạng mục chi tiết
        /// </summary>
        public string ord { get; set; }
        /// <summary>
        /// Hạng mục chi tiết khám chữa bệnh
        /// </summary>
        public string zlxm { get; set; }
        /// <summary>
        /// Tên hạng mục chi tiết khám chữa bệnh
        /// </summary>
        public string zlxmmc { get; set; }
        /// <summary>
        /// Đơn giá hạng mục chi tiết khám chữa bệnh
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// Pinyin hạng mục chi tiết khám chữa bệnh
        /// </summary>
        public string zlxmpy { get; set; }
        /// <summary>
        /// Số tiền tổ hợp
        /// </summary>
        public decimal zhje { get; set; }
        /// <summary>
        /// Loại lớn thu phí
        /// </summary>
        public string sfdl { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
