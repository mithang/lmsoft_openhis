using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// đầuvănmục
    /// </summary>
    public class Header
    {
        /// <summary>
        /// gửiứngdùngtrìnhthứ tự，chỉđịnhlà：PLAT
        /// bắt buộc
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// nhậnthuứngdùngtrìnhthứ tự，chỉđịnhlà：HIS,EMR,AE,HEAL,CBS,RC,RT,K
        /// bắt buộc
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// Thời gian gửi，định dạng：yyyyMMddHHmmss
        /// bắt buộc
        /// </summary>
        public string sendTime { get; set; }

        /// <summary>
        /// hủytinLoại：TCM_ PLAT _06
        /// bắt buộc
        /// </summary>
        public string msgType { get; set; }

        /// <summary>
        /// hủytin ID(ứngdùngtrìnhthứ tựMã+Thời gian gửi) ，nếu：PLAT20170606020202
        /// bắt buộc
        /// </summary>
        public string msgID { get; set; }
    }
}