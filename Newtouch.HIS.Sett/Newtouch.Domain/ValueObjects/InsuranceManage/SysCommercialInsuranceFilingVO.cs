using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects.InsuranceManage
{
    [NotMapped]
    public class SysCommercialInsuranceFilingVO : SysCommercialInsuranceFilingEntity
    {
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        public string zjh { get; set; }
        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public DateTime? csny { get; set; }
        public string bxName { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        public int? nl
        {
            get
            {
                if (csny.HasValue)
                {
                    return (DateTime.Now.Year - csny.Value.Year) + 1;
                }
                return null;
            }
        }
    }
}
