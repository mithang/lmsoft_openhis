using System;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Danh sách tổng quan bệnh nhân nội trú Tập hợp đối tượng trả về của bệnh nhân tại khu bệnh
    /// </summary>
   public class PatientzbqResponseDto: PatientyzxxResponseDto
    {
        public string id { get; set; }
        public string cwmc { get; set; }
        public string xm { get; set; }
        public string sex { get; set; }
        public int? age { get; set; }
        //public string zyh { get; set; }
        public DateTime ryrq { get; set; }
        public string hljb { get; set; }
        public string wzjb { get; set; }
        public string brzt { get; set; }
        public string brxzmc { get; set; }
        public string ysmc { get; set; }
        public string zdmc { get; set; }
        public string brxzdm { get; set; }
        public int zyts { get; set; }
        public string nlshow { get; set; }
        public string ps { get; set; }
        public string blh { get; set; }
        public string deptCode { get; set; }
    }

    public class PatientyzxxResponseDto
    {
        public string zyh { get; set; }
        public DateTime? kssj { get; set; }
        public string ztnr { get; set; }
        /// <summary>
        /// Có hiển thị biểu tượng dự kiến xuất viện hay không (Cókssj Hiển thị:null / Không hiển thị:none)
        /// </summary>
        public string display { get; set; }
        /// <summary>
        /// Số y lệnh chờ duyệt
        /// </summary>
        public int? cnt { get; set; }
    }
    /// <summary>
    /// Danh sách tổng quan bệnh nhân nội trú Tập hợp đối tượng trả về của bệnh nhân đã ra khỏi khu
    /// </summary>
    public class PatientycqResponseDto
    {
        public string id { get; set; }
        public string xm { get; set; }
        public string zyh { get; set; }
        public string cardno { get; set; }
        public string cwmc { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public DateTime ryrq { get; set; }
        public DateTime? cqrq { get; set; }
        public string ysmc { get; set; }
        public string zdmc { get; set; }
        public string brxzmc { get; set; }
        public string brxzdm { get; set; }
        public string  nlShow { get; set; }
    }
}
