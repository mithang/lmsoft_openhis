using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// Kiểm kê thông tin
    /// </summary>
    public class VInventoryInfoEntity
    {
        /// <summary>
        /// vật tư Tên
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// lý luận số(mang Đơn vị)
        /// </summary>
        public string llsl { get; set; }
        
        /// <summary>
        /// vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vị Số lượng
        /// </summary>
        public int deptSjsl { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vị
        /// </summary>
        public string deptdw { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất Số lượng
        /// </summary>
        public int minSjsl { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// chuyển hóa vì con
        /// </summary>
        public int zhyz { get; set; }

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
        /// không bán Đơn giá
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// lý luận tổng bán lẻ
        /// </summary>
        public decimal lllsje { get; set; }

        /// <summary>
        /// thực tế tổng bán lẻ
        /// </summary>
        public decimal sjlsje { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Kiểm kê chi tiết ID
        /// </summary>
        public long pdmxId { get; set; }
        
        /// <summary>
        /// Kiểm kê Thời gian tạo
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
