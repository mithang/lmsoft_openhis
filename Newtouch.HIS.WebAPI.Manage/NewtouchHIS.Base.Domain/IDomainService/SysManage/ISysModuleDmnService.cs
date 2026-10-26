using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    /// <summary>
    /// menu hệ thống （giao diện thêm, xóa, sửa）
    /// </summary>
    public interface ISysModuleDmnService : IScopedDependency
    {
        /// <summary>
        /// truy vấn menuby id
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SysModuleEntity?> GetEntity(string id);
        /// <summary>
        /// truy vấn menuby id(chỉ hiệu lực)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="valid"></param>
        /// <returns></returns>
        Task<SysModuleEntity?> GetEntity(string id, bool valid = true);
        /// <summary>
        /// thêm mới menu
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<BusResult<string>> AddEntity(SysModuleVO vo, string user, string orgId);
        /// <summary>
        /// thêm mới hàng loạt
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<BusResult<string>> AddRange(List<SysModuleVO> vo, string user, string orgId, string defaultParentId);
        /// <summary>
        /// cập nhật thông tin menu
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<BusResult<string>> UpdateEntity(SysModuleVO vo, string user, string orgId);
        /// <summary>
        /// menu hủy
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<BusResult<string>> DelEntity(string keyValue, string user, string orgId);
        /// <summary>
        /// lấy danh sách menu hiệu lực theo tổ chức
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysModuleEntity>> GetEntitybyorgId(string orgId);
    }
}
