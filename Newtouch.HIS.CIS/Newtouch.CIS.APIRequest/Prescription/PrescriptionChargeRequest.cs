using Newtouch.CIS.APIRequest.Dto;
using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Prescription
{
    public class PrescriptionChargeRequest : RequestBase
    {
        /// <summary>
        /// Danh sách số đơn thuốc
        /// </summary>
        public List<PrescriptionChargeDto> cfList { get; set; }

        /// <summary>
        /// Cờ thu phí
        /// </summary>
        [Required]
        public bool sfbz { get; set; }

        ///// <summary>
        ///// Thời gian thao tác
        ///// </summary>
        //[Required]
        //public DateTime OperateTime { get; set; }
        public string OrganizeId { get; set; } = null;
    }

    public class PrescriptionReturnChargeRequest : RequestBase
    {
        /// <summary>
        /// Danh sách số đơn thuốc
        /// </summary>
        public List<string> cfList { get; set; }

        /// <summary>
        /// Cờ thu phí
        /// </summary>
        [Required]
        public bool tbz { get; set; }
    }
}
