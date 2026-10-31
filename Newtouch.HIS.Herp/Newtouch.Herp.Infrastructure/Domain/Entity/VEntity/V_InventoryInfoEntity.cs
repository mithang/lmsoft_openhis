using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thông tin kiểm kê
    /// </summary>
    public class VInventoryInfoEntity
    {
        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Số lý thuyết(Kèm đơn vị)
        /// </summary>
        public string llsl { get; set; }
        
        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lượng đơn vị khoa phòng
        /// </summary>
        public int deptSjsl { get; set; }

        /// <summary>
        /// Đơn vị khoa phòng
        /// </summary>
        public string deptdw { get; set; }

        /// <summary>
        /// Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int minSjsl { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô hàng
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Đơn giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ lý thuyết
        /// </summary>
        public decimal lllsje { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ thực tế
        /// </summary>
        public decimal sjlsje { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Chi tiết kiểm kê ID
        /// </summary>
        public long pdmxId { get; set; }
        
        /// <summary>
        /// Thời gian tạo kiểm kê
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
