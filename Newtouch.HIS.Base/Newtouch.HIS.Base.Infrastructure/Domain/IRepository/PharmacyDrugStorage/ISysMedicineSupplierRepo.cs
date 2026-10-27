using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineSupplierRepo : IRepositoryBase<SysMedicineSupplierEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineSupplierEntity> GetPagintionList(string OrganizeId, Pagination Pagination, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysMedicineSupplierEntity entity, int? keyValue);
    }
}
