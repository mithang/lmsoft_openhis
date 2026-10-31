using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// Thông tin phẫu thuật và gây mê trang đầu bệnh án nội trú
    /// </summary>
    public class YB_7620
    {
        /// <summary>
        /// Thông tin phẫu thuật và gây mê trang đầu bệnh án nội trú
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
        /// Số thứ tự phẫu thuật
        /// 1
        /// </summary>
        public string BKF919 { get; set; }
        /// <summary>
        /// Mã phẫu thuật và thủ thuật(ICD-9-CM-3)
        /// 1
        /// </summary>
        public string BKF720 { get; set; }
        /// <summary>
        /// Tên phẫu thuật và thủ thuật
        /// 1
        /// </summary>
        public string BKF721 { get; set; }
        /// <summary>
        /// Định dạng ngày giờ phẫu thuật và thủ thuật:yyyymmddhhmiss
        /// 1
        /// </summary>
        public string BKF723 { get; set; }
        /// <summary>
        /// Loại phẫu thuật và thủ thuật 1 Phẫu thuật 2 Thao tác
        /// 1
        /// </summary>
        public string BKF922 { get; set; }
        /// <summary>
        /// Quan hệ chính/phụ của phẫu thuật 1 Phẫu thuật chính;0 Phẫu thuật phụ
        /// </summary>
        public string BKF960 { get; set; }
        /// <summary>
        /// Tham khảo cấp độ phẫu thuật<Cấp độ phẫu thuật>
        /// 1
        /// </summary>
        public string BKF773 { get; set; }
        /// <summary>
        /// Tham khảo mã loại vết mổ phẫu thuật<Loại vết mổ phẫu thuật>
        /// 1
        /// </summary>
        public string BKF283 { get; set; }
        /// <summary>
        /// Cấp độ vết mổ
        /// </summary>
        public string BKF920 { get; set; }
        /// <summary>
        /// Mã cấp độ lành vết mổ
        /// </summary>
        public string BKF257 { get; set; }
        /// <summary>
        /// Cấp độ lành
        /// </summary>
        public string BKF921 { get; set; }
        /// <summary>
        /// Mã người thực hiện phẫu thuật
        /// </summary>
        public string BKF724 { get; set; }
        /// <summary>
        /// Tên người thực hiện phẫu thuật
        /// </summary>
        public string BKF725 { get; set; }
        /// <summary>
        /// ⅠTên phụ mổ
        /// </summary>
        public string BKC769 { get; set; }
        /// <summary>
        /// ⅡTên phụ mổ
        /// </summary>
        public string BKC770 { get; set; }
        /// <summary> 
        /// Tham khảo phương pháp gây mê<Phương pháp gây mê>
        /// </summary>
        public string BKC772 { get; set; }
        /// <summary>
        /// Bác sĩ gây mê
        /// </summary>
        public string BKF732 { get; set; }
        
    }
}
