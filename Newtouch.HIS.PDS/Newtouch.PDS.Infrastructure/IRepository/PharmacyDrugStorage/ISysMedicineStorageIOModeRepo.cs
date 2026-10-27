using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineStorageIOModeRepo : IRepositoryBase<SysMedicineStorageIOModeEntity>
    {
        /// <summary>
        /// Lấy phương thức xuất nhập kho
        /// </summary>
        /// <returns></returns>
        object GetStorageIOMode();
    }
}
