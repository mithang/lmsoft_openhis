using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Kiểm kê Điều kiện tìm kiếm
    /// </summary>
    public class InventorySearchDTO
    {
        /// <summary>
        /// Kiểm kê ID
        /// </summary>
        public long pdId { get; set; }

        /// <summary>
        /// Kiểm kê Thời gian
        /// </summary>
        public string pdsj { get; set; }

        /// <summary>
        /// Từ khóa  vật tư Tên/Pinyin
        /// </summary>
        public string keyWord { get; set; }

        /// <summary>
        /// vật tư Trạng thái 0:Vô hiệu  1:Hiệu lực
        /// </summary>
        public string wzzt { get; set; }

        /// <summary>
        /// cột loại ID
        /// </summary>
        public string lb { get; set; }

        /// <summary>
        /// Tồn kho Hiển thị -1hoặc trống:Tất cả
        /// </summary>
        public int kcxs { get; set; }

    }

    /// <summary>
    /// Lưu Kiểm kê
    /// </summary>
    public class SaveInventoryDTO
    {
        /// <summary>
        /// Kiểm kê chi tiết ID
        /// </summary>
        public long? pdmxId { get; set; }

        /// <summary>
        /// vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// thực tế Số lượng
        /// </summary>
        public int sjsl { get; set; }
    }
}
