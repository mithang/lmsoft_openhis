namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    public class ScheduleRequest
    {
        /// <summary>
        /// khoa
        /// </summary>
        public string? ks { get; set; }
        public string? ysgh { get; set; }
        /// <summary>
        /// ngày bắt đầu(yyyy-MM-dd)
        /// </summary>
        public DateTime? ksrq { get; set; }
        /// <summary>
        /// ngày kết thúc(yyyy-MM-dd)
        /// </summary>
        public DateTime? jsrq { get; set; }
        /// <summary>
        /// mz_ghpb_schedule lịch hẹn đã địnhId
        /// </summary>
        public string? ScheduId { get; set; }
        /// <summary>
        /// có thể hẹn qua mạng hay không 0 không thể rút gọn
        /// </summary>
        public string? IsBook { get; set; }
        /// <summary>
        /// loại ngoại trú chuyên gia thông thường, phó cao cấp, v.v. mjzbz
        /// </summary>
        public string? RegType { get; set; }
    }
    public class SchedulePageRequest: ScheduleRequest
    {
        public string pagination { get; set; }
    }

}
