namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class jgszchargemoneyVO
    {
        //public string Id { get; set; }
        //public DateTime sfrq { get; set; }
        //public string dlmc { get; set; }

        /// <summary>
        /// Mã hạng mục thu phí
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// Tên hạng mục thu phí
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// Tổng số tiền quyết toán
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Tổng số tiền kế hoạch ghi sổ
        /// </summary>
        public decimal? jhzje { get; set; }

        /// <summary>
        /// Tổng số tiền hoàn phí  jhzje-zje=tzje
        /// </summary>
        public decimal? tzje { get; set; }
    }
}
