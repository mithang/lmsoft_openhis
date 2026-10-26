using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Hồ sơ điều trị
    /// </summary>
    [NotMapped]
    public class SyncTreatmentServiceRecordVO : SyncTreatmentServiceRecordEntity
    {
        /// <summary>
        /// Từ điển
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StaffId { get; set; }
        public string StaffName { get; set; }

        /// <summary>
        /// KhoaCode
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Loại lớn thu phíCode
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// Tên loại lớn thu phí
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>z
        public decimal? dj { get; set; }

        /// <summary>
        /// calc
        /// </summary>
        public int sl
        {
            get
            {
                if (units > 0)
                {
                    return (int)units;
                }
                else if (durationPerUnit > 0 && minutes > 0)
                {
                    return (int)(minutes / durationPerUnit);
                }
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// durationPerUnit
        /// </summary>
        public int? duration
        {
            get
            {
                if (durationPerUnit > 0)
                {
                    return durationPerUnit;
                }
                else if (units > 0 && minutes > 0)
                {
                    return (int)(minutes / units);
                }
                return 0;
            }
        }
    }
}
