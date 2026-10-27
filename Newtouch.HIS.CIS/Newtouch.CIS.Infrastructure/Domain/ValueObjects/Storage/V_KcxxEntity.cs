using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// Tồn khothông tin kết chuyểndùng
    /// </summary>
    public class VKcxxEntity
    {
        /// <summary>
        /// vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Tồn khoSố lượng Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Khoa phòngGiá bán lẻ chuyểnhóavìcontương ứngĐơn vị
        /// </summary>
        public decimal bmlsj { get; set; }

        /// <summary>
        /// vàogiá chuyểnhóavìcontương ứngĐơn vị
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
        /// </summary>
        public int zhyz { get; set; }
    }
}
