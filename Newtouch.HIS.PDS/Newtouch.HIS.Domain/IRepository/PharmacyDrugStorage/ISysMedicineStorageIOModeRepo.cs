using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineStorageIOModeRepo : IRepositoryBase<SysMedicineStorageIOModeEntity>
    {
        /// <summary>
        /// lấy phương thức nhập/xuất kho
        /// </summary>
        /// <returns></returns>
        object GetStorageIOMode();
    }
}
