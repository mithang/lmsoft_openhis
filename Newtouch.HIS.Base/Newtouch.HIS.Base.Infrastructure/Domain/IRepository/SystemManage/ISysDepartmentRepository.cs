using System.Collections.Generic;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDepartmentRepository : IRepositoryBase<SysDepartmentEntity>
    {
        /// <summary>
        /// Lấy danh sách khoa
        /// </summary>
        /// <returns></returns>
        List<SysDepartmentEntity> GetListByTopOrg(string topOrganizeId);

        /// <summary>
        /// Lấy danh sách khoa
        /// </summary>
        /// <returns></returns>
        List<SysDepartmentEntity> GetListByOrg(string organizeId);

        /// <summary>
        /// Lấy tất cả khoa hiệu lực thuộc tổ chức hiện tại
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysDepartmentEntity> GetValidListByOrg(string organizeId);

        /// <summary>
        /// Lấy tổ chức của Khoa hiệu lực
        /// </summary>
        /// <returns></returns>
        IList<SysDepartmentEntity> GetValidListByTopOrg(string topOrganizeId);

        /// <summary>
        /// TheoCodeLấy tên
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByCode(string code, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysDepartmentEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysDepartmentEntity SysDepartmentEntity, string keyValue);
        /// <summary>
        /// Cập nhật trạng thái tải lên BHYT của khoa
        /// </summary>
        /// <param name="uploadYB"></param>
        /// <param name="id"></param>
        void UpdateYbUpload(int uploadYB, string id);
        /// <summary>
        /// Lấy danh sách khoa hiệu lực theo điều kiện tra cứu
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysDepartmentEntity> GetDeptListByKeyValue(string orgId, string keyValue);

    }
}
