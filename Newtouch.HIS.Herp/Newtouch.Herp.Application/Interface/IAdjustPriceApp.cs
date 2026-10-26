namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Điều chỉnh giá
    /// </summary>
    public interface IAdjustPriceApp
    {
        /// <summary>
        /// Thực hiện lãi/lỗ điều chỉnh giá
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        string AdjustPriceExecute(string ids);
    }
}