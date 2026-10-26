using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Tính phí thuốc nội trú Thuốc của Thông tin chi tiết
    /// </summary>
    public class HospMedicinFeeDetailVO
    {
        /// <summary>
        /// Số hiệu
        /// </summary>
        public int jfbbh { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Loại lớn thu phí
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Loại lớn thu phí Tên
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Số tiền giảm miễn(Không cần dùng nhân Số lượng)
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        /// Tỷ lệ giảm miễn
        /// </summary>
        public decimal jmbl { get; set; }

        /// <summary>
        /// Tỷ lệ tự chi trả
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// Tính chất tự chi trả
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// Mã BHYT
        /// </summary>
        public string ybbm { get; set; }

        /// <summary>
        /// Số tiền = Đơn giá * Số lượng
        /// </summary>
        public decimal je
        {
            get
            {
                return this.dj * this.sl;
            }
        }

    }
}
