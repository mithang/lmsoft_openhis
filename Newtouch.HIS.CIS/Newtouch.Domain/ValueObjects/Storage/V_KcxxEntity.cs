using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// Thông tin tồn kho Dùng để kết chuyển
    /// </summary>
    public class VKcxxEntity
    {
        /// <summary>
        /// Vật tưID
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
        /// Hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Số lượng tồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Giá bán lẻ của khoa phòng Đơn vị tương ứng với hệ số chuyển đổi
        /// </summary>
        public decimal bmlsj { get; set; }

        /// <summary>
        /// Giá nhập Đơn vị tương ứng với hệ số chuyển đổi
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }
    }
}
