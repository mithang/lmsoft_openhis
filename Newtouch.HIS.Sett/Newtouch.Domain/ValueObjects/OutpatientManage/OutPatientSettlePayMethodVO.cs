
using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    [NotMapped]
    public class OutPatientSettlePayMethodVO: OutpatientSettlementPaymentModelEntity
    {
        /// <summary>
        /// Tên phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffsmc { get; set; }
    }
}
