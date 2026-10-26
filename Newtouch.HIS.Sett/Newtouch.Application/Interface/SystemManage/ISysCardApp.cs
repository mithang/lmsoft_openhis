using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysCardApp
    {
        /// <summary>
        /// theo số thẻ lấy bệnh nhân mã nội bộ
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        string GetPatidByCardNo(string cardno);


        /// <summary>
        /// Lấy mới ảomô phỏng thẻ số thẻ
        /// </summary>
        /// <returns></returns>
        string GetCardSerialNo();
    }
}
