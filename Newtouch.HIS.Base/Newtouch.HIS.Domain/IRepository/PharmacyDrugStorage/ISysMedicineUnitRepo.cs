using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineUnitRepo : IRepositoryBase<SysMedicineUnitEntity>
    {
        /// <summary>
        /// Truy vấn đơn vị thuốc theo từ khóa
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineUnitEntity> GetValidList(string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineUnitEntity> GetList(string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysMedicineUnitEntity entity, int? keyValue);
    }
}
