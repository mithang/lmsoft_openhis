using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Kiểm kêĐiều kiện tìm kiếm
    /// </summary>
    public class InventorySearchDTO
    {
        /// <summary>
        /// Kiểm kêID
        /// </summary>
        public long pdId { get; set; }

        /// <summary>
        /// Kiểm kêThời gian
        /// </summary>
        public string pdsj { get; set; }

        /// <summary>
        /// Từ khóa  vật tưTên/Pinyin
        /// </summary>
        public string keyWord { get; set; }

        /// <summary>
        /// vật tưTrạng thái 0:Vô hiệu  1:Hiệu lực
        /// </summary>
        public string wzzt { get; set; }

        /// <summary>
        /// cộtloạiID
        /// </summary>
        public string lb { get; set; }

        /// <summary>
        /// Tồn khoHiển thị -1hoặctrống:Tất cả
        /// </summary>
        public int kcxs { get; set; }

    }

    /// <summary>
    /// LưuKiểm kê
    /// </summary>
    public class SaveInventoryDTO
    {
        /// <summary>
        /// Kiểm kêchi tiếtID
        /// </summary>
        public long? pdmxId { get; set; }

        /// <summary>
        /// vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// thực tếSố lượng
        /// </summary>
        public int sjsl { get; set; }
    }
}
