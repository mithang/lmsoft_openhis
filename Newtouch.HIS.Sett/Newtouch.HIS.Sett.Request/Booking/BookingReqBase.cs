using Newtouch.HIS.API.Common;
using Newtouch.HIS.Sett.Request.Booking;
using Newtouch.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Newtouch.HIS.Sett.Request
{
    public class BookingReqBase: BookingBase<BookingReqBase>
    {
        [APIRequired]

        public new string AppId { get; set; }
        /// <summary>
        /// ID tổ chức(Đ ã定 vị đ ến 具体 bệnh viện)
        /// </summary>
        [APIRequired]
        public string OrganizeId { get; set; }
        /// <summary>
        /// yêu cầu số hiệu
        /// </summary>
        [APIRequired]
        public string methodcode { get; set; }
        public string optype { get; set; }
        /// <summary>
        /// Khi trư ớc ngư ời dùng 
        /// </summary>
        public string user { get; set; }
        /// <summary>
        /// 入参
        /// </summary>
        public Dictionary<string, string> paradata { get; set; }


    }

    public class BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "AppID is required")]
        [RegularExpression(@"WeChat", ErrorMessage = "AppID is Error")]
        public string AppID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "HospitalID is required")]
        public string HospitalID { get; set; }
    }
}
