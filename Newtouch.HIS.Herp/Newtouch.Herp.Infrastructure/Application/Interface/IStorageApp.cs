using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Bảo trì tồn kho
    /// </summary>
    public interface IStorageApp
    {
        /// <summary>
        /// Gửi nhập kho ngoài
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns>error message</returns>
        string InStorageSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// Gửi xuất kho ngoài
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns>error message</returns>
        string OutStorageSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// Gửi xuất kho trực tiếp
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        string DirectDeliverySubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// Gửi trả hàng phát nội bộ
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        string DeliveryOfReturnSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);
    }
}