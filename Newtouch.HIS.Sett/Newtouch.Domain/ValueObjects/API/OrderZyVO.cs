
namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OrderZyVO
    {
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Loại lớn thu phí
        /// </summary>
        public string dlcode { get; set; }
        /// <summary>
        /// Tên loại lớn thu phí
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// Hạng mục thu phí đơn thuốc
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal? sl { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// Đơn vị tính phí
        /// </summary>
        public string jfdw { get; set; }
        /// <summary>
        /// Tính chất tự chi trả
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// Dấu chuyển sang tự chi trả
        /// </summary>
        public string zzfbz { get; set; }
    }

    public class OrderZyBaseVO
    {
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Loại lớn thu phí
        /// </summary>
        public string dlcode { get; set; }
        /// <summary>
        /// Tên loại lớn thu phí
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal? je { get; set; }

    }


}
