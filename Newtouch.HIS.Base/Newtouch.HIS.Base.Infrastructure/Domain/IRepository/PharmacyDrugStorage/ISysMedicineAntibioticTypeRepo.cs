using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineAntibioticTypeRepo : IRepositoryBase<SysMedicineAntibioticTypeEntity>
    {
        /// <summary>
        /// Theo tổ chức,Cấp,Lấy danh sách phân loại kháng sinh
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Level"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeEntity> GetValidListByOrg(string OrganizeId, string Level, string parentId);
        /// <summary>
        /// Theo tổ chức,Cấp trênId,Lấy danh sách phân loại kháng sinh
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeEntity> GetListByParentId(string OrganizeId, string parentId);
        /// <summary>
        /// Gửi thông tin phân loại kháng sinh
        /// </summary>
        /// <param name="entity"></param>
        void SubmitForm(SysMedicineAntibioticTypeEntity entity);
    }
}
