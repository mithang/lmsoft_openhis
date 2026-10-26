using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineFormulationRepo : IRepositoryBase<SysMedicineFormulationEntity>
    {
        /// <summary>
        /// Truy vấn dạng bào chế thuốc theo từ khóa
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineFormulationEntity> GetValidList(string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineFormulationEntity> GetPagintionList(Pagination pagination, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysMedicineFormulationEntity entity, int? keyValue);
    }
}
