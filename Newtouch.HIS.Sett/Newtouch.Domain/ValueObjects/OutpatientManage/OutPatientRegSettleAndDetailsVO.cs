
using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Quyết toán ngoại trú và chi tiết quyết toán
    /// </summary>
    [NotMapped]
    public class OutPatientRegSettleAndDetailsVO: OutpatientRegistNonAttendanceEntity
    {
        /// <summary>
        /// Hạng mục thu phí
        /// </summary>
        public string sfxm { get; set; }


    }
}
