namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Thao tác tồn kho
    /// </summary>
    public interface IKcxxDmnService
    {
        /// <summary>
        /// Đóng băng tồn kho  Sử dụng trừ tồn kho  Trả lại phát thuốc nội bộ
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
        /// Đóng băng tồn kho  Trừ tồn kho Sử dụng xuất kho trực tiếp
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
        /// Trừ tồn kho
        /// </summary>
        /// <param name="ypdm">Mã thuốc</param>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="pc">Lô</param>
        /// <param name="ph">Số lô</param>
        /// <param name="yfbmCode">Bộ phận nhà thuốc cần trừ tồn kho</param>
        /// <param name="organizeid">Tổ chức</param>
        /// <returns></returns>
        string SubtractStock(string ypdm, int sl, string pc, string ph, string yfbmCode, string organizeid);
    }
}