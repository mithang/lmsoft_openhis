using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto.Inpatient
{
    public class WardMaintenanceRequestDto
    {
        public string OrganizeId { get; set; }
        public string bedCode { get; set; }
    }
    public class DispensingMXRequestDto
    {
        public string kssj { get; set; }
        public string jssj { get; set; }
        public string OrganizeId { get; set; }
    }
    public class patRequestDto
    {
        public string OrganizeId { get; set; }
        public string zyh { get; set; }
        public string user { get; set; }
    }
    public class ZDSelect
    {
        public string zdbh { get; set; }
        public string icd10 { get; set; }
        public string zdmc { get; set; }
        public string py { get; set; }

        public int zdnm { get; set; }
    }
    public class patBedRequestDto: patRequestDto
    {
        public string bedCode { get; set; }
    }

    public class patBedFeeRequestDto: patRequestDto
    {
        /// <summary>
        /// Ra khỏi khu/Chuyển khu/Ngày vào khu
        /// </summary>
        public DateTime rq { get; set; }
    }

    public class patInAreaRequestDto : patBedFeeRequestDto
    {
        public string wzjb { get; set; }
        public string cwCode { get; set; }
        public string ysgh { get; set; }
        public string ysmc { get; set; }
        public string bq { get; set; }
    }
    public class patChangeAreaRequestDto : patRequestDto
    {
        public DateTime rqrq { get; set; }
        public string wzjb { get; set; }
        public string cwCode { get; set; }
        public string ysgh { get; set; }
        public string ysmc { get; set; }
        /// <summary>
        /// Giường bệnh trước đó
        /// </summary>
        public string lastcwCode { get; set; }
        /// <summary>
        /// Khu bệnh hiện tại
        /// </summary>
        public string WardCode {get;set ;}
        /// <summary>
        /// Khoa hiện tại
        /// </summary>
        public string DeptCode { get; set; }
    }
}
