using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// đầu văn mục
    /// </summary>
    public class Header
    {
        /// <summary>
        /// gửi ứng dùng trình thứ tự,chỉ định là:PLAT
        /// bắt buộc
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// nhận thu ứng dùng trình thứ tự,chỉ định là:HIS,EMR,AE,HEAL,CBS,RC,RT,K
        /// bắt buộc
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// Thời gian gửi,định dạng:yyyyMMddHHmmss
        /// bắt buộc
        /// </summary>
        public string sendTime { get; set; }

        /// <summary>
        /// hủy tin Loại:TCM_ PLAT _06
        /// bắt buộc
        /// </summary>
        public string msgType { get; set; }

        /// <summary>
        /// hủy tin ID(ứng dùng trình thứ tự Mã+Thời gian gửi) ,nếu:PLAT20170606020202
        /// bắt buộc
        /// </summary>
        public string msgID { get; set; }
    }
}