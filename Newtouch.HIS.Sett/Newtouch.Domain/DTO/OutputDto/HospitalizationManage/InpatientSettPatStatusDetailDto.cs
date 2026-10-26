using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage
{
    public class InpatientSettPatStatusDetailDto
    {
        /// <summary>
        /// Thông tin cơ bản bệnh nhân nội trú
        /// </summary>
        public InpatientSettPatInfoVO InpatientSettPatInfo { get; set; }

        /// <summary>
        /// Chi tiết hạng mục tính phí nội trú(Bao gồm hạng mục điều trị và hạng mục không điều trị)
        /// </summary>
        public InpatientSettleItemBO InpatientSettleItemBO { get; set; }

        /// <summary>
        /// ChargeCategoryGropuFee
        /// </summary>
        public IList<HospFeeChargeCategoryGroupVO> GroupFeeVOList { get; set; }

        public InpatIentFeeSumVo InpatIentFeeSum { get; set; }

    }
}
