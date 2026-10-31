using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// Thông tin chẩn đoán trang đầu bệnh án nội trú
    /// </summary>
    public class YB_7610
    {
        /// <summary>
        /// Số thứ tự khám
        /// 1
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// Số thứ tự bệnh án
        /// 1
        /// </summary>
        public string BKF303 { get; set; }
        /// <summary>
        /// Số thứ tự nội trú
        /// 1
        /// </summary>
        public string BKC191 { get; set; }
        /// <summary>
        /// Dấu hiệu chẩn đoán chính/phụ 1 Chẩn đoán chính;2 Chẩn đoán khác
        /// 1
        /// </summary>
        public string BKF718 { get; set; }
        /// <summary>
        /// Thứ tự chẩn đoán
        /// 1
        /// </summary>
        public string BKF512 { get; set; }
        /// <summary>
        /// Mã chẩn đoán Tây y
        /// </summary>
        public string BKF857 { get; set; }
        /// <summary>
        /// Tên chẩn đoán Tây y
        /// </summary>
        public string BKF856 { get; set; }
        /// <summary>
        /// Mã bệnh danh Đông y
        /// </summary>
        public string BKF460 { get; set; }
        /// <summary>
        /// Bệnh danh Đông y
        /// </summary>
        public string BKF459 { get; set; }
        /// <summary>
        /// Mã chứng hậu Đông y
        /// </summary>
        public string BKF475 { get; set; }
        /// <summary>
        /// Chứng hậu Đông y
        /// </summary>
        public string BKF474 { get; set; }
        /// <summary>
        /// Mã tình trạng bệnh khi nhập viện
        /// </summary>
        public string BKF262 { get; set; }
    }
}
