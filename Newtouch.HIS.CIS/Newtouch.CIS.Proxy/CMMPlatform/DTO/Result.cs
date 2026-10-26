namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// Kết quả thân phản hồi
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 1: Thành công; 0: Thất bại
        /// Bắt buộc
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Thành công; Thất bại (bổ sung mô tả nguyên nhân thất bại)
        /// Bắt buộc
        /// </summary>
        public string desc  {get; set; }
}
}