using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineSupplierRepo : IRepositoryBase<SysMedicineSupplierEntity>
    {
        /// <summary>
        /// Trả về thực thể nhà cung cấp theo từ khóa
        /// </summary>
        /// <param name="keyword">Gysdm,Gysmc,Mcsx,</param>
        /// <returns></returns>
        SysMedicineSupplierEntity SelectMedicineSupplier(string keyword);
    }
}
