using NewtouchHIS.Lib.Base.Model;
using System;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// yêu cầuởKhu bệnhThông tin bệnh nhântham số
    /// </summary>
    public class PatientzbqRequestDto
    {
        public  Pagination pagination { get; set; }
        public string bqcode { get; set; }

        public string ysgh { get; set; }

        public string cw { get; set; }
    }

    public class PatientycqRequestDto : PatientzbqRequestDto
    {
        public Pagination pagination { get; set; }
        public DateTime? cqksrq { get; set; }
        public DateTime? cqjsrq { get; set; }
        public string zyh { get; set; }
    }
}
