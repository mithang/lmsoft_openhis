namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// tồn kho thao tác
    /// </summary>
    public interface IKcxxDmnService
    {
        /// <summary>
        /// tồn kho bị đóng băng  trừ tồn kho sử dụng  trả lại thuốc phát nội bộ
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="organizeid"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="sl"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="rkbm"></param>
        /// <returns></returns>
        string FrozenStockReduceByReturninward(string ypdm, string organizeid, string pc, string ph, int sl, string yfbmCode, string rkbm);

        /// <summary>
        /// tồn kho bị đóng băng  trừ tồn kho xuất kho trực tiếp khiến dùng
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="organizeid"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="sl"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="rkbm"></param>
        /// <returns></returns>
        string FrozenStockReduceByDeliveryDirect(string ypdm,
           string organizeid,
           string pc,
           string ph,
           int sl,
           string yfbmCode);

        /// <summary>
        /// trừ tồn kho
        /// </summary>
        /// <param name="ypdm">mã thuốc</param>
        /// <param name="sl">số lượng đơn vị nhỏ nhất</param>
        /// <param name="pc">lô</param>
        /// <param name="ph">số lô</param>
        /// <param name="yfbmCode">cần trừ tồn kho của bộ phận nhà thuốc</param>
        /// <param name="organizeid">tổ chức</param>
        /// <returns></returns>
        string SubtractStock(string ypdm, int sl, string pc, string ph, string yfbmCode, string organizeid);
    }
}