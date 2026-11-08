using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// hưởng ứng thể kết nếu
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 1: thành công; 0:thất bại
        /// bắt buộc
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// thành công;thất bại(Bổ sung thất bại nguyên nhân Mô tả)
        /// bắt buộc
        /// </summary>
        public string desc  {get; set; }
}
}