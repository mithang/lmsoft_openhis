namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Điều chỉnh giá
    /// </summary>
    public interface IAdjustPriceApp
    {
        /// <summary>
        /// Thực thi chênh lệch điều chỉnh giá
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        string AdjustPriceExecute(string ids);
    }
}