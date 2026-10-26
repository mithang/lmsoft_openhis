using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDepartmentBindingRepo : IRepositoryBase<SysDepartmentBindingEntity>
    {

        /// <summary>
        /// Lấy danh sách tất cả hóa đơn
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysDepartmentBindingEntity> GetSysDepartmentBindingList(string keyValue, string organizeId);

        /// <summary>
        /// Trang sửa đổi,Lấy thực thể theo khóa chính
        /// </summary>
        /// <param name="bddm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysDepartmentBindingEntity GetSysDepartmentBindingEntity(string bddm, string orgId);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysDepartmentBindingEntity"></param>
        /// <param name="bddm"></param>
        void SubmitForm(SysDepartmentBindingEntity sysDepartmentBindingEntity, string bddm);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="bddm"></param>
        /// <param name="orgId"></param>
        void DeleteForm(string bddm, string orgId);
    }
}
