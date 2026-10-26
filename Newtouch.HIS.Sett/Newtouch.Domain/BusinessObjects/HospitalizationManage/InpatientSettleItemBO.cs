using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage
{
    public class InpatientSettleItemBO
    {
        /// <summary>
        /// Bảng tính phí hạng mục điều trị
        /// </summary>
        public IList<TreatmentItemFeeDetailVO> TreatmentItemList { get; set; }

        /// <summary>
        /// Bảng tính phí thuốc
        /// </summary>
        public IList<DrugFeeDetailVO> DrugList { get; set; }

        /// <summary>
        /// Bảng tính phí hạng mục không điều trị
        /// </summary>
        public IList<NonTreatmentItemFeeDetailVO> Non_treatmentItemList { get; set; }
    }
}
