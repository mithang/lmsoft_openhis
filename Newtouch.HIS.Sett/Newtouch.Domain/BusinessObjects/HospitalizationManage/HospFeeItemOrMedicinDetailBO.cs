using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// Quyết toán xuất viện Chi phí(Hạng mục/Thuốc)Chi tiết
    /// </summary>
    public class HospFeeItemOrMedicinDetailBO
    {
        public HospFeeItemOrMedicinDetailBO()
        {
            this.sfjsfymx = new HospFeeItemOrMedicinSuanfaFeeDetailBO();
        }

        /// <summary>
        /// Có cần giao dịch BHYT hay không
        /// </summary>
        public bool isNeedYBJY { get; set; }

        /// <summary>
        /// Loại lớn thu phí
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Loại lớn thu phí Tên
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HospItemFeeDetailVO Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HospMedicinFeeDetailVO Medicin { get; set; }

        #region

        /// <summary>
        /// Số tiền sau giảm miễn
        /// </summary>
        public decimal jmhje { get; set; }

        /// <summary>
        /// Chi phí tự lo
        /// </summary>
        public decimal zlfy { get; set; }

        /// <summary>
        /// Số tiền được thanh toán
        /// </summary>
        public decimal kbje { get; set; }

        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        public decimal flzf { get; set; }

        #endregion

        /// <summary>
        /// Số tiền giao dịch BHYT(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal ybjyje { get; set; }

        /// <summary>
        /// Số tiền trong phạm vi giao dịch BHYT(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal ybjyfwje { get; set; }

        /// <summary>
        /// Tính toán theo thuật toán sau đó Chi tiết chi phí
        /// </summary>
        public HospFeeItemOrMedicinSuanfaFeeDetailBO sfjsfymx { get; set; }

        /// <summary>
        /// Vật tư dùng một lần Hạng mục thu phí Tổng hợp
        /// </summary>
        public SysChargeMaterialItemSynthesisEntity sfclxmzhEntity { get; set; }

    }

}
