
namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// Phương thức thanh toán quyết toán ngoại trú
    /// </summary>
    public class OutpatientSettlementPaymentModelVO
    {
        /// <summary>
        /// Tên phương thức thanh toán
        /// </summary>
        public string xjzffsmc { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public decimal zfje { get; set; }
    }
}
