using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// Tồn kho thông tin kết chuyển dùng
    /// </summary>
    public class VKcxxEntity
    {
        /// <summary>
        /// vật tư ID
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
        /// Tồn kho Số lượng Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Khoa phòng Giá bán lẻ chuyển hóa vì con tương ứng Đơn vị
        /// </summary>
        public decimal bmlsj { get; set; }

        /// <summary>
        /// vào giá chuyển hóa vì con tương ứng Đơn vị
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// chuyển hóa vì con
        /// </summary>
        public int zhyz { get; set; }
    }
}
