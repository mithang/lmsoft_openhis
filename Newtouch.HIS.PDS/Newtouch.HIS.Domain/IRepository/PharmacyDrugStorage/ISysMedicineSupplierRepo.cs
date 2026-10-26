using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineSupplierRepo : IRepositoryBase<SysMedicineSupplierEntity>
    {
        /// <summary>
        /// theo từ khóa quay lại nhà cung cấp thực thể
        /// </summary>
        /// <param name="keyword">Gysdm、Gysmc、Mcsx、</param>
        /// <returns></returns>
        SysMedicineSupplierEntity SelectMedicineSupplier(string keyword);
    }
}
