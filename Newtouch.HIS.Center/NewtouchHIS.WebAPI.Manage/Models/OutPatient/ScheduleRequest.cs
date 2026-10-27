namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    public class ScheduleRequest
    {
        /// <summary>
        /// Khoa
        /// </summary>
        public string? ks { get; set; }
        public string? ysgh { get; set; }
        /// <summary>
        /// Ngày bắt đầu(yyyy-MM-dd)
        /// </summary>
        public DateTime? ksrq { get; set; }
        /// <summary>
        /// Ngày kết thúc(yyyy-MM-dd)
        /// </summary>
        public DateTime? jsrq { get; set; }
        /// <summary>
        /// mz_ghpb_schedule Lịch trình đã định của đặt hẹnId
        /// </summary>
        public string? ScheduId { get; set; }
        /// <summary>
        /// Mạng có đặt được hay không 0 Không đặt được
        /// </summary>
        public string? IsBook { get; set; }
        /// <summary>
        /// Loại ngoại trú Thông thường, chuyên gia, phó cao cấp v.v. mjzbz
        /// </summary>
        public string? RegType { get; set; }
    }
    public class SchedulePageRequest: ScheduleRequest
    {
        public string pagination { get; set; }
    }

}
