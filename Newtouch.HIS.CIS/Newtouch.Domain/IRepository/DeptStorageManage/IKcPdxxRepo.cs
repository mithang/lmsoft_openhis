using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository.DeptStorageManage
{
    /// <summary>
    /// Kiểm kê
    /// </summary>
    public interface IKcPdxxRepo : IRepositoryBase<KcPdxxEntity>
    {
        /// <summary>
        /// Theo kiểm kêIDLấy thông tin kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        KcPdxxEntity SelectPdInfByPdId(long pdId, string organizeId);

        /// <summary>
        /// Kiểm tra có kiểm kê chưa hoàn thành hay không (Kết thúc)
        /// </summary>
        /// <returns></returns>
        int SelectUnfinishedInventoryByPdId(long pdId, string organizeId);
    }
}