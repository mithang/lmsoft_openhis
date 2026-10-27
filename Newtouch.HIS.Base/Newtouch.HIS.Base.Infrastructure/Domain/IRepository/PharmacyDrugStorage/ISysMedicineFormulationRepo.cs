using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineFormulationRepo : IRepositoryBase<SysMedicineFormulationEntity>
    {
        /// <summary>
        /// Tra cứu dạng bào chế thuốc theo từ khóa
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
