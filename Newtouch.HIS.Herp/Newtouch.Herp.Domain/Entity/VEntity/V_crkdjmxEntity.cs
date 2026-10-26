using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Chi tiết chứng từ nhập/xuất kho
    /// </summary>
    public class VCrkdjmxEntity
    {
        /// <summary>
        /// Chi tiết nhập/xuất kho ID
        /// </summary>
        public long crkmxId { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Số lượng,Kèm đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Tổng tiền bán lẻ
        /// </summary>
        public decimal ljze { get; set; }

        /// <summary>
        /// Tổng tiền nhập
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// Chênh lệch giá nhập-bán
        /// </summary>
        public decimal jxcj { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }
    }
}
