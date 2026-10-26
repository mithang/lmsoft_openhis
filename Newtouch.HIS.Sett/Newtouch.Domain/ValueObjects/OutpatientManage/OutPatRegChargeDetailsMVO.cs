using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi tiết thu phí hiển thị bao gồm hoàn phí
    /// </summary>
    public class OutPatRegChargeDetailsMVO
    {
        #region 挂号收费查询明细(含退费查询)
        public int jsmxnm { get; set; }
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
        /// Số lượng thu phí
        /// </summary>
        public decimal sl { get; set; }
        /// <summary>
        /// Số tiền thu phí
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
        #endregion
    }
}
