using System;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// Ngoại trú
    /// </summary>
    public class OutpatientCommitRequestDTO : RequestBase
    {
        /// <summary>
        /// Mã nội bộ thanh toán bắt buộc
        /// </summary>
        [Required]
        public long Jsnm { get; set; }

        /// <summary>
        /// Thời gian thu phí
        /// </summary>
        [Required]
        public DateTime Sfsj { get; set; }

        /// <summary>
        /// Số đơn thuốc bắt buộc
        /// </summary>
        [Required]
        public string Cfh { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc bắt buộc
        /// </summary>
        [Required]
        public long Cfnm { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// Mã nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Người gửi
        /// </summary>
        public string CreatorCode { get; set; }
    }
}