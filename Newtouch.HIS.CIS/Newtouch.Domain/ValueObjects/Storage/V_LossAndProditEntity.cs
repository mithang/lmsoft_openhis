using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// Thông tin chi tiết chênh lệch
    /// </summary>
    public class VLossAndProditEntity
    {
        /// <summary>
        /// Cờ chênh lệch
        /// </summary>
        public string sybz { get; set; }

        /// <summary>
        /// Nguyên nhân chênh lệch
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// Vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Thời gian báo cáo
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
        /// Tên xuất xứ
        /// </summary>
        public string cd { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime Yxq { get; set; }

        /// <summary>
        /// Người chịu trách nhiệm
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// Số lượng chênh lệch(Kèm đơn vị)
        /// </summary>
        public string syslStr { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Tổng số tiền bán lẻ
        /// </summary>
        public decimal ljze { get; set; }
    }
}
