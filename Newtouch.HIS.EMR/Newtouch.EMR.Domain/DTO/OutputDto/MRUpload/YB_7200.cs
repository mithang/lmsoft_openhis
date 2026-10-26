using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// Ghi chép diễn biến bệnh lần đầu
    /// </summary>
    public class YB_7200
    {
        /// <summary>
        /// Số thứ tự khám bệnh
        /// 1
        /// </summary>
        [Required]
        public string AKC190 { get; set; }
        /// <summary>
        /// Số thứ tự nhập viện
        /// 1
        /// </summary>
        [Required]
        public string BKC191 { get; set; }
        /// <summary>
        /// Mã khoa
        /// </summary>
        public string AKF001 { get; set; }
        /// <summary>
        /// Tên khoa
        /// 1
        /// </summary>
        [Required]
        public string AKF002 { get; set; }
        /// <summary>
        /// Tên khu bệnh
        /// 1
        /// </summary>
        public string AKE021 { get; set; }
        /// <summary>
        /// Số giường bệnh
        /// 1
        /// </summary>
        [Required]
        public string BKB410 { get; set; }
        /// <summary>
        /// Ngày giờ ghi chép, định dạng: yyyymmddhhmiss
        /// 1
        /// </summary>
        [Required]
        public string BKF737 { get; set; }
        /// <summary>
        /// Lý do vào viện chính
        /// 1
        /// </summary>
        [Required]
        public string BKF476 { get; set; }
        /// <summary>
        /// Đặc điểm ca bệnh
        /// 1
        /// </summary>
        [Required]
        public string BKF108 { get; set; }
        /// <summary>
        /// Kết quả quan sát “tứ chẩn” Đông y
        /// </summary>
        public string BKF458 { get; set; }
        /// <summary>
        /// Căn cứ chẩn đoán
        /// </summary>
        public string BKF419 { get; set; }
        /// <summary>
        /// Chẩn đoán sơ bộ - Mã chẩn đoán Tây y
        /// Mã loại chẩn đoán điền tham khảo thông báo của Văn phòng Bộ Y tế về việc ban hành “Phân loại và mã bệnh (bản sửa đổi)” (Vệ Ban Tổng Phát [2011] số 166); chỉ cần tải lên chẩn đoán chính
        /// </summary>
        public string BKF748 { get; set; }
        /// <summary>
        /// Chẩn đoán sơ bộ - Tên chẩn đoán Tây y
        /// </summary>
        public string BKF126 { get; set; }
        /// <summary>
        /// Chẩn đoán sơ bộ - Mã tên bệnh Đông y
        /// </summary>
        public string BKF128 { get; set; }
        /// <summary>
        /// Chẩn đoán sơ bộ - Tên bệnh Đông y
        /// </summary>
        public string BKF127 { get; set; }
        /// <summary>
        /// Chẩn đoán sơ bộ - Mã chứng hậu Đông y
        /// </summary>
        public string BKF130 { get; set; }
        /// <summary>
        /// Chẩn đoán sơ bộ - Chứng hậu Đông y
        /// </summary>
        public string BKF129 { get; set; }
        /// <summary>
        /// Chẩn đoán phân biệt - Mã chẩn đoán Tây y
        /// </summary>
        public string BKF856 { get; set; }
        /// <summary>
        /// Chẩn đoán phân biệt - Tên chẩn đoán Tây y
        /// </summary>
        public string BKF857 { get; set; }
        /// <summary>
        /// Chẩn đoán phân biệt - Mã tên bệnh Đông y
        /// </summary>
        public string BKF855 { get; set; }
        /// <summary>
        /// Chẩn đoán phân biệt - Tên bệnh Đông y
        /// </summary>
        public string BKF854 { get; set; }
        /// <summary>
        /// Chẩn đoán phân biệt - Mã chứng hậu Đông y
        /// </summary>
        public string BKF204 { get; set; }
        /// <summary>
        /// Chẩn đoán phân biệt - Chứng hậu Đông y
        /// </summary>
        public string BKF203 { get; set; }
        /// <summary>
        /// Kế hoạch chẩn trị
        /// </summary>
        public string BKF421 { get; set; }
        /// <summary>
        /// Nguyên tắc và phương pháp điều trị
        /// </summary>
        public string BKF440 { get; set; }
        /// <summary>
        /// Mã bác sĩ nội trú
        /// 1
        /// </summary>
        [Required]
        public string BKF693 { get; set; }
        /// <summary>
        /// Họ tên bác sĩ nội trú
        /// 1
        /// </summary>
        [Required]
        public string BKF692 { get; set; }
        /// <summary>
        /// Họ tên bác sĩ cấp trên
        /// 1
        /// </summary>
        [Required]
        public string BKF267 { get; set; }
    }
}
