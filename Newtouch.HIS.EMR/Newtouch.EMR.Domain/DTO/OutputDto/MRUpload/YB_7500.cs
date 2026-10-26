using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// Thông tin tóm tắt ra viện
    /// </summary>
    public class YB_7500
    {
        /// <summary>
        /// Số thứ tự khám bệnh 
        /// 1
        /// Định danh duy nhất ghi nội bộ của cơ sở y tế về thông tin khám lần này của bệnh nhân, bắt buộc phải liên kết với hệ thống bảo hiểm y tế
        /// </summary>
        [Required]
        public string AKC190 { get; set; }
        /// <summary>
        /// Số thẻ sức khỏe cư dân
        /// </summary>
        public string BKF636 { get; set; }
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
        [Required]
        public string AKE021 { get; set; }
        /// <summary>
        /// Số giường bệnh
        /// 1
        /// </summary>
        [Required]
        public string BKB410 { get; set; }
        /// <summary>
        /// Ngày vào viện, định dạng: yyyymmddhhmiss
        /// 1
        /// </summary>
        [Required]
        public string BKC192 { get; set; }
        /// <summary>
        /// Ngày ra viện, định dạng: yyyymmddhhmiss
        /// 1
        /// </summary>
        [Required]
        public string AKC194 { get; set; }
        /// <summary>
        /// Số ngày nằm viện thực tế
        /// 1
        /// </summary>
        [Required]
        public string BKF668 { get; set; }
        /// <summary>
        /// Tình hình lúc vào viện
        /// 1
        /// </summary>
        [Required]
        public string BKF264 { get; set; }
        /// <summary>
        /// Kết quả kiểm tra bổ trợ dương tính
        /// </summary>
        public string BKF378 { get; set; }
        /// <summary>
        /// Mô tả quá trình chẩn trị
        /// </summary>
        public string BKF420 { get; set; }
        /// <summary>
        /// Tình hình ra viện
        /// 1
        /// </summary>
        [Required]
        public string BKF758 { get; set; }
        /// <summary>
        /// Triệu chứng và dấu hiệu lúc ra viện
        /// </summary>
        public string BKF139 { get; set; }
        /// <summary>
        /// Y lệnh ra viện
        /// </summary>
        public string BKF759 { get; set; }
        /// <summary>
        /// Mã kết quả điều trị
        /// </summary>
        public string BKF439 { get; set; }
        /// <summary>
        /// Mã bác sĩ nội trú
        /// </summary>
        public string BKF050 { get; set; }
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
        /// <summary>
        /// Ngày giờ ký (bác sĩ nội trú)
        /// </summary>
        public string BKF254 { get; set; }
        /// <summary>
        /// Mô tả kết quả quan sát tứ chẩn Đông y
        /// </summary>
        public string BKF473 { get; set; }
        /// <summary>
        /// Nguyên tắc và phương pháp điều trị
        /// </summary>
        public string BKF440 { get; set; }
        /// <summary>
        /// Phương pháp sắc thuốc Đông y
        /// </summary>
        public string BKF451 { get; set; }
        /// <summary>
        /// Cách dùng thuốc Đông y
        /// </summary>
        public string BKF457 { get; set; }
    }
}
