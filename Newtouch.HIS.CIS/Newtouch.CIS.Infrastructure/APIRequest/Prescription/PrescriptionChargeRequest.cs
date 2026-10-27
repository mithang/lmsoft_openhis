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
        /// Số đơn thuốcList
        /// </summary>
        public List<PrescriptionChargeDto> cfList { get; set; }

        /// <summary>
        /// Cờ thu phí
        /// </summary>
        [Required]
        public bool sfbz { get; set; }

        ///// <summary>
        ///// Thao tácThời gian
        ///// </summary>
        //[Required]
        //public DateTime OperateTime { get; set; }
        public string OrganizeId { get; set; } = null;
    }

    public class PrescriptionReturnChargeRequest : RequestBase
    {
        /// <summary>
        /// Số đơn thuốcList
        /// </summary>
        public List<string> cfList { get; set; }

        /// <summary>
        /// Cờ thu phí
        /// </summary>
        [Required]
        public bool tbz { get; set; }
    }
}
