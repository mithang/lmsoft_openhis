using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineAntibioticTypeRepo : IRepositoryBase<SysMedicineAntibioticTypeEntity>
    {
        /// <summary>
        /// Lấy danh sách phân loại kháng sinh theo tổ chức, cấp độ
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Level"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeEntity> GetValidListByOrg(string OrganizeId, string Level, string parentId);
        /// <summary>
        /// Lấy danh sách phân loại kháng sinh theo tổ chức, Id cấp trên
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
