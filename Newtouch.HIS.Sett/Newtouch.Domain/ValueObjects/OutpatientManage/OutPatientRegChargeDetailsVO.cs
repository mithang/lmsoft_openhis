
namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatientRegChargeDetailsVO
    {
        #region 挂号收费查询明细
        /// <summary>
        /// from xt_sfdl Loại lớn thu phí
        /// </summary>
        public string dlCode { get; set; }
        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// Số đơn thuốc  Trong tra cứu thu phí đăng ký khám thì cho giá trị rỗng
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// Hạng mục thu phí from mz_xm
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// Tên thuốc  Trong tra cứu thu phí đăng ký khám chỉ tên hạng mục thu phí
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// Đơn giá from mz_xm
        /// </summary>
        public decimal dj { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }
        /// <summary>
        /// Số tiền: mz_jsmx.sl*mz_xm.dj=je
        /// </summary>
        public decimal je { get; set; }
        /// <summary>
        /// Số lượng đã lập
        /// </summary>
        public decimal klsl { get; set; }
        /// <summary>
        /// Số tiền đã lập
        /// </summary>
        public decimal klje { get; set; }
        /// <summary>
        /// Số lượng đã trả lại
        /// </summary>
        public decimal ytsl { get; set; }
        /// <summary>
        /// Số tiền đã trả lại
        /// </summary>
        public decimal ytje { get; set; }
        public int? jsmxnm { get; set; }
        /// <summary>
        /// Bác sĩ kê đơn
        /// </summary>
        public string ysmc { get; set; }
        public string fph { get; set; }
        public int? jszt { get; set; }
        public string pc { get; set; }
        public string gg { get; set; }
        public decimal? zfbl { get; set; }
        public string ksmc { get; set; }
        #endregion
    }
}
