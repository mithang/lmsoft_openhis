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
        //Loại báo cáo jy:Xét nghiệm jc:Kiểm tra
        public string reportType { get; set; }
        /// <summary>
        /// Cờ ngoại trú/nội trú  mz:Ngoại trú zy:Nội trú
        /// </summary>
        public string mzzybz { get; set; }
    }
}
