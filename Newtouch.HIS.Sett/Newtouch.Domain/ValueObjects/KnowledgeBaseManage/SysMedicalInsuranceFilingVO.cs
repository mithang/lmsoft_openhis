using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects.KnowledgeBaseManage
{
    [NotMapped]
    public class SysMedicalInsuranceFilingVO: SysMedicalInsuranceFilingEntity
    {
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public DateTime? csny { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }
    }
}
