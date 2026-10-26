using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class DrugFeeDetailVO
    {
        /// <summary>
        /// Có phải là thuốc hay không 0:không phải 1:là
        /// </summary>
        public string isYP { get; set; }

        /// <summary>
        /// Số hiệu
        /// </summary>
        public int jfbbh { get; set; }
        public string yzwym { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }
        
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        public decimal ylsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tsl { get; set; }

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
        /// 
        /// </summary>
        public string ybbm { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime? tdrq { get; set; }

    }
}
