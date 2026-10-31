using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// Kiểm kêthông tin
    /// </summary>
    public class VInventoryInfoEntity
    {
        /// <summary>
        /// vật tưTên
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// lýluậnsố(mangĐơn vị)
        /// </summary>
        public string llsl { get; set; }
        
        /// <summary>
        /// vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Khoa phòngĐơn vịSố lượng
        /// </summary>
        public int deptSjsl { get; set; }

        /// <summary>
        /// Khoa phòngĐơn vị
        /// </summary>
        public string deptdw { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtSố lượng
        /// </summary>
        public int minSjsl { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
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
        /// khôngbánĐơn giá
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// lýluậntổng bán lẻ
        /// </summary>
        public decimal lllsje { get; set; }

        /// <summary>
        /// thực tếtổng bán lẻ
        /// </summary>
        public decimal sjlsje { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Kiểm kêchi tiếtID
        /// </summary>
        public long pdmxId { get; set; }
        
        /// <summary>
        /// Kiểm kêThời gian tạo
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
