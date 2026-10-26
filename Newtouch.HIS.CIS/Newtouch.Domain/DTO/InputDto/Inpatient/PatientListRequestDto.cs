using Newtouch.Core.Common;
using System;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Tham số thông tin bệnh nhân tại khu bệnh được yêu cầu
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
