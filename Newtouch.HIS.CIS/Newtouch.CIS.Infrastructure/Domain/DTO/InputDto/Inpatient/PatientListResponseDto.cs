using System;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Bệnh nhân nội trúmộtxem ởKhu bệnhcủabệnh nhânQuay lạiđối tượngtậphợp
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
        /// làKhôngHiển thịdựXuất việnBiểu tượng (cókssj Hiển thị:null / khôngHiển thị:none)
        /// </summary>
        public string display { get; set; }
        /// <summary>
        /// Chờ duyệtY lệnhdòngsố
        /// </summary>
        public int? cnt { get; set; }
    }
    /// <summary>
    /// Bệnh nhân nội trúmộtxem đãra khỏi khucủabệnh nhânQuay lạiđối tượngtậphợp
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
