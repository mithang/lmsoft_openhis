using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// Thông tin phẫu thuật và gây mê trên trang đầu hồ sơ bệnh án nội trú
    /// </summary>
    public class YB_7620
    {
        /// <summary>
        /// Thông tin phẫu thuật và gây mê trên trang đầu hồ sơ bệnh án nội trú
        /// 1
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// Số thứ tự hồ sơ bệnh án
        /// 1
        /// </summary>
        public string BKF303 { get; set; }
        /// <summary>
        /// Số thứ tự nhập viện
        /// 1
        /// </summary>
        public string BKC191 { get; set; }
        /// <summary>
        /// Số thứ tự phẫu thuật
        /// 1
        /// </summary>
        public string BKF919 { get; set; }
        /// <summary>
        /// Mã thủ thuật phẫu thuật - Phẫu thuật và thủ thuật (ICD-9-CM-3)
        /// 1
        /// </summary>
        public string BKF720 { get; set; }
        /// <summary>
        /// Tên thủ thuật phẫu thuật
        /// 1
        /// </summary>
        public string BKF721 { get; set; }
        /// <summary>
        /// Ngày giờ thủ thuật phẫu thuật, định dạng: yyyymmddhhmiss
        /// 1
        /// </summary>
        public string BKF723 { get; set; }
        /// <summary>
        /// Loại thủ thuật phẫu thuật: 1 Phẫu thuật, 2 Thủ thuật
        /// 1
        /// </summary>
        public string BKF922 { get; set; }
        /// <summary>
        /// Quan hệ chính/phụ của phẫu thuật: 1 Phẫu thuật chính; 0 Phẫu thuật phụ
        /// </summary>
        public string BKF960 { get; set; }
        /// <summary>
        /// Cấp độ phẫu thuật, tham khảo “Cấp độ phẫu thuật”
        /// 1
        /// </summary>
        public string BKF773 { get; set; }
        /// <summary>
        /// Mã loại vết mổ phẫu thuật, tham khảo “Loại vết mổ phẫu thuật”
        /// 1
        /// </summary>
        public string BKF283 { get; set; }
        /// <summary>
        /// Cấp độ vết mổ
        /// </summary>
        public string BKF920 { get; set; }
        /// <summary>
        /// Mã cấp độ liền vết mổ
        /// </summary>
        public string BKF257 { get; set; }
        /// <summary>
        /// Cấp độ liền vết thương
        /// </summary>
        public string BKF921 { get; set; }
        /// <summary>
        /// Mã phẫu thuật viên
        /// </summary>
        public string BKF724 { get; set; }
        /// <summary>
        /// Họ tên phẫu thuật viên
        /// </summary>
        public string BKF725 { get; set; }
        /// <summary>
        /// Họ tên phụ mổ I
        /// </summary>
        public string BKC769 { get; set; }
        /// <summary>
        /// Họ tên phụ mổ II
        /// </summary>
        public string BKC770 { get; set; }
        /// <summary> 
        /// Phương pháp gây mê, tham khảo “Phương pháp gây mê”
        /// </summary>
        public string BKC772 { get; set; }
        /// <summary>
        /// Bác sĩ gây mê
        /// </summary>
        public string BKF732 { get; set; }
        
    }
}
