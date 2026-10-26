namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Yêu cầu viết lại tất cả thông tin đơn thuốc dưới số khám ngoại trú
    /// </summary>
    public class PrescriptionUpdateAllForOutNoRequest : PrescriptionAddOrUpdateRequest
    {
        /// <summary>
        /// Phân tách bằng dấu phẩy của cf (nếu chưa chỉ định, hủy bỏ tất cả đơn thuốc chưa thu phí)
        /// </summary>
        public string AheadCancelCf { get; set; }

    }
}
