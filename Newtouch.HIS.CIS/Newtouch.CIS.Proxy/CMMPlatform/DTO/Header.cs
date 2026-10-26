namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// Tệp tiêu đề
    /// </summary>
    public class Header
    {
        /// <summary>
        /// Ứng dụng gửi, chỉ định là: PLAT
        /// Bắt buộc
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// Ứng dụng nhận, chỉ định là: HIS,EMR,AE,HEAL,CBS,RC,RT,K
        /// Bắt buộc
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// Thời gian gửi, định dạng: yyyyMMddHHmmss
        /// Bắt buộc
        /// </summary>
        public string sendTime { get; set; }

        /// <summary>
        /// Loại thông báo: TCM_ PLAT _06
        /// Bắt buộc
        /// </summary>
        public string msgType { get; set; }

        /// <summary>
        /// ID thông báo (mã ứng dụng + thời gian gửi), ví dụ: PLAT20170606020202
        /// Bắt buộc
        /// </summary>
        public string msgID { get; set; }
    }
}