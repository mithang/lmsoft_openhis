using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// lãi lỗchi tiếtthông tin
    /// </summary>
    public class VLossAndProditEntity
    {
        /// <summary>
        /// lãi lỗCờ
        /// </summary>
        public string sybz { get; set; }

        /// <summary>
        /// nguyên nhân lãi lỗ
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// phiếusố phiếu
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// vật tưTên
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// vàogiá
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// báo cáoThời gian
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// Loại
        /// </summary>
        public string lb { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Nơi sản xuấtTên
        /// </summary>
        public string cd { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public DateTime Yxq { get; set; }

        /// <summary>
        /// Trách nhiệm người
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// lãi lỗSố lượng（mangĐơn vị）
        /// </summary>
        public string syslStr { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// tổng bán lẻ
        /// </summary>
        public decimal ljze { get; set; }
    }
}
