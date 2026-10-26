using System;

namespace Newtouch.HIS.Domain.ValueObjects.NonTreatmentItemManage
{
    public class PatientInfoVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int patId { get; set; }

        ///// <summary>
        ///// Năm tháng sinh
        ///// </summary>
        //public DateTime? csny { get; set; }

        ///// <summary>
        ///// Tuổi
        ///// </summary>
        //public int? nl
        //{
        //    get
        //    {
        //        if (csny.HasValue)
        //        {
        //            return (DateTime.Now.Year - csny.Value.Year) + 1;
        //        }
        //        return null;
        //    }
        //}
    }
}
