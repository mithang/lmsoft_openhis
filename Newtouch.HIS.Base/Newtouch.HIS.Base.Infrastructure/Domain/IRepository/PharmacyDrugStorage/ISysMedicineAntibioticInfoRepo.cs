using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineAntibioticInfoRepo : IRepositoryBase<SysMedicineAntibioticInfoEntity>
    {
        /// <summary>
        /// Gửi thông tin kháng sinh của thuốc
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Id</returns>
        string SubmitForm(SysMedicineAntibioticInfoEntity entity);
        /// <summary>
        /// Thông quaIdLấy thông tin kháng sinh của thuốc
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        SysMedicineAntibioticInfoEntity GetKssInfo(string Id, string OrganizeId);
    }
}
