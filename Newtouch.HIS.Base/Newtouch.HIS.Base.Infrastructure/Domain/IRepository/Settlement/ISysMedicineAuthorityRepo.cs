using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity.Settlement;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository.Settlement
{
    public interface ISysMedicineAuthorityRepo : IRepositoryBase<SysMedicineAuthorityEntity>
    {

        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysMedicineAuthorityEntity GetForm(string keyValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysMedicineAuthorityEntity> GetList(string orgId, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysMedicineAuthorityEntity entity, string keyValue);
        /// <summary>
        /// Lấy quyền hiệu lực
        /// </summary>
        /// <returns></returns>
        IList<SysMedicineAuthorityEntity> GetValidList(string orgId, string keyword);
    }
}
