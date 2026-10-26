using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Tính phí hạng mục nội trú Hạng mục của Thông tin chi tiết
    /// </summary>
    public class HospItemFeeDetailVO
    {
        /// <summary>
        /// Số hiệu
        /// </summary>
        public int jfbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Đơn giá phí dịch vụ
        /// </summary>
        public decimal fwfdj { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Hạng mục thu phí
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// Tên hạng mục thu phí
        /// </summary>
        public string sfxmmc { get; set; }

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
        /// Số tiền giảm miễn(không nhân Số lượng)
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
        /// Mã vật tư tổng hợp
        /// </summary>
        public int? clzhxmbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybbm { get; set; }

        /// <summary>
        /// Số tiền = (Đơn giá phí dịch vụ+Đơn giá)*Số lượng
        /// </summary>
        public decimal je
        {
            get
            {
                return (this.dj + this.fwfdj) * this.sl;
            }
        }


    }

}
