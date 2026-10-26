using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;

namespace Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage
{
    public class InpatientCancelSettPatStatusDetailDto
    {
        /// <summary>
        /// Thông tin cơ bản bệnh nhân nội trú
        /// </summary>
        public InpatientSettPatInfoVO InpatientSettPatInfo { get; set; }

        /// <summary>
        /// Lần cuối cùng(Chưa trả lại)Quyết toán
        /// </summary>
        public HospSettlementEntity LastUnCancelledSett { get; set; }

        /// <summary>
        /// Liên quan chi phí BHYT
        /// </summary>
        public HospSettlementGAYBFeeEntity YbFee { get; set; }
        /// <summary>
        /// Liên quan giao dịch Bảo hiểm nông thôn mới Quý An
        /// </summary>

        public HospSettlementGAXNHFeeEntity XnhFee { get; set; }
		///// <summary>
		///// Quyết toán BHYT Trùng Khánh
		///// </summary>
	 //   public CqybSett05Entity CqYbFee { get; set; }
	}
}
