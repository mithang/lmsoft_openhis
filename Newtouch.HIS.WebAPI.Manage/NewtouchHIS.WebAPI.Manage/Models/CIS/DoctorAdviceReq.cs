namespace NewtouchHIS.WebAPI.Manage.Models.CIS
{
    public class DoctorAdviceReq
    {
        public string zyh { get; set; }
        public string yzlx { get; set; }
    }

    public class InspectionExaminationReq
    {
        public string jzh { get; set; }
        public DateTime ksrq { get; set; }
        public DateTime jsrq { get; set; }
        //loại báo cáo jy：xét nghiệm jc:kiểm tra
        public string reportType { get; set; }
        /// <summary>
        /// cờ ngoại trú – nội trú  mz:khám ngoại trú zy:nội trú
        /// </summary>
        public string mzzybz { get; set; }
    }
}
