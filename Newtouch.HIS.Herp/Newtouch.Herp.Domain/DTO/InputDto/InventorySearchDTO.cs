using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// Điều kiện tra cứu kiểm kê
    /// </summary>
    public class InventorySearchDTO
    {
        /// <summary>
        /// Kiểm kê ID
        /// </summary>
        public long pdId { get; set; }

        /// <summary>
        /// Thời gian kiểm kê
        /// </summary>
        public string pdsj { get; set; }

        /// <summary>
        /// Từ khóa  Tên vật tư/Phiên âm
        /// </summary>
        public string keyWord { get; set; }

        /// <summary>
        /// Trạng thái vật tư 0:Không hiệu lực  1:Hiệu lực
        /// </summary>
        public string wzzt { get; set; }

        /// <summary>
        /// Loại cột ID
        /// </summary>
        public string lb { get; set; }

        /// <summary>
        /// Hiển thị tồn kho -1 Hoặc trống:Tất cả
        /// </summary>
        public int kcxs { get; set; }

    }

    /// <summary>
    /// Lưu kiểm kê
    /// </summary>
    public class SaveInventoryDTO
    {
        /// <summary>
        /// Chi tiết kiểm kê ID
        /// </summary>
        public long? pdmxId { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lượng thực tế
        /// </summary>
        public int sjsl { get; set; }
    }
}
