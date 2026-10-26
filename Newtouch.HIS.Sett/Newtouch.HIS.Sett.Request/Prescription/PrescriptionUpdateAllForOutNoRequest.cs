namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Số khám ngoại trú dư ới CủaĐ ơn thuốc thông tin tất cả重写
    /// </summary>
    public class PrescriptionUpdateAllForOutNoRequest : PrescriptionAddOrUpdateRequest
    {
        /// <summary>
        /// 逗 số 分割Củacf(nếu chưa chỉ đ ịnh,hủy bỏ tất cả các của chưa thu phí đơn thuốc)
        /// </summary>
        public string AheadCancelCf { get; set; }

    }
}
