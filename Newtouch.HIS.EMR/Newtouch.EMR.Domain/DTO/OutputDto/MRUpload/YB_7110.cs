using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    public class YB_7110
    {
        /// <summary>
        /// Số thứ tự khám bệnh
        /// </summary>
        /// <returns></returns>
        public string AKC190 { get; set; }

        /// <summary>
        /// Số thứ tự hồ sơ vào viện
        /// </summary>
        /// <returns></returns>
        public string BKF263 { get; set; }

        /// <summary>
        /// Số thứ tự nhập viện
        /// </summary>
        /// <returns></returns>
        public string BKC191 { get; set; }

        /// <summary>
        /// Phân loại chẩn đoán lúc vào viện
        /// </summary>
        /// <returns></returns>
        public string BKF260 { get; set; }

        /// <summary>
        /// Cờ chẩn đoán chính/phụ: 1 Chẩn đoán chính; 2 Chẩn đoán khác; 3 Chẩn đoán bệnh lý
        /// </summary>
        /// <returns></returns>
        public string BKF718 { get; set; }

        /// <summary>
        /// Thứ tự chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string BKF512 { get; set; }

        /// <summary>
        /// Mã chẩn đoán Tây y
        /// </summary>
        /// <returns></returns>
        public string BKF857 { get; set; }

        /// <summary>
        /// Tên chẩn đoán Tây y
        /// </summary>
        /// <returns></returns>
        public string BKF856 { get; set; }

        /// <summary>
        /// Mã tên bệnh Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF460 { get; set; }

        /// <summary>
        /// Tên bệnh Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF459 { get; set; }

        /// <summary>
        /// Mã chứng hậu Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF475 { get; set; }

        /// <summary>
        /// BKF474
        /// </summary>
        /// <returns></returns>
        public string BKF474 { get; set; }

        /// <summary>
        /// Ngày giờ chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string BKF510 { get; set; }
    }
}
