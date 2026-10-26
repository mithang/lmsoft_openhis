using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Bao tri ton kho
    /// </summary>
    public interface IStorageApp
    {
        /// <summary>
        /// Gui nhap kho ngoai
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns>error message</returns>
        string InStorageSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// Gui xuat kho ngoai
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns>error message</returns>
        string OutStorageSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// Gui xuat kho truc tiep
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        string DirectDeliverySubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// Gui tra lai hang giao noi bo
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        string DeliveryOfReturnSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);
    }
}