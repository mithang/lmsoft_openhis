using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Services.HttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;

namespace NewtouchHIS.Framework.Web.Implementation
{
    /// <summary>
    /// dữ liệu cơ bản của tổ chức
    /// </summary>
    public interface IOrganizeAppService : ISingletonDependency
    {
        /// <summary>
        /// danh sách tổ chức (TopOrganize)
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<SysOrgVo>> OrgListAsync(string keyword);
        /// <summary>
        /// lấy tên tổ chức
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string?> GetNameByOrgId(string orgId);
        /// <summary>
        /// quan hệ liên kết nhân viên - vị trí
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysStaffDutyComplexVEntity>> GetsysStaffDutyListAsync(string orgId);
        /// <summary>
        /// lấy tính chất bệnh nhân
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysPatientNatureEntity>> GetsysatientNatureListAsync(string orgId);
        /// <summary>
        /// lấy khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysDepartmentEntity>> GetsysDepartListAsync(string orgId);
        /// <summary>
        /// lấy khu bệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysWardVEntity>> GetsysPatiAreaListAsync(string orgId);
        /// <summary>
        /// lấy khu bệnh theo khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysDepartmentWardRelationVO>> GetsysWardDeptRelationAsync(string orgId);
        /// <summary>
        /// lấy nhóm lớn
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysChargeCategoryVEntity>> GetsysMajorClassListAsync(string orgId);
        /// <summary>
        /// ghi sổ nằm viện lấy bác sĩ
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysDutyStaffVO>> GetStaffByDutyCodeAsync(string orgId);
        /// <summary>
        /// lấy cấu hình lỗi
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysFailedCodeMessageMappEntity>> GetSysFailedCodeMessageMapListAsync(string orgId);

    }
    public class OrganizeAppService : AppServiceBase, IOrganizeAppService
    {

        public OrganizeAppService(IHttpClientHelper httpClient, IAuthCenterAppService authCenterApp) : base(httpClient, authCenterApp)
        {
            AppId = ConfigInitHelper.SysConfig.AppAPIHostName?.HisAppBaseAPIHost ?? "HIS.BaseAPI";
            Host = ConfigInitHelper.SysConfig.AppAPIHost?.HisAppBaseAPIHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "HisAppBaseAPIHost");
        }

        #region Api
        public string OrgListApi => $"{Host}api/organize/OrgList";
        /// <summary>
        /// lấy bảng quan hệ người dùng
        /// </summary>
        public string sysStaffDutyListApi => $"{Host}api/organize/GetsysStaffDutyList";
        /// <summary>
        /// lấy tính chất bệnh nhân
        /// </summary>
        public string sysPatientNatureListApi => $"{Host}api/organize/GetsysPatientNatureList";
        /// <summary>
        /// lấy khoa
        /// </summary>
        public string sysDepartListApi => $"{Host}api/organize/GetsysDepartList";
        /// <summary>
        /// lấy khu bệnh
        /// </summary>
        public string sysPatiAreaListApi => $"{Host}api/organize/GetsysPatiAreaList";
        /// <summary>
        /// lấy khu bệnh theo khoa
        /// </summary>
        public string sysWardDeptRelationApi => $"{Host}api/organize/GetsysWardDeptRelation";

        /// <summary>
        /// lấy nhóm lớn
        /// </summary>
        public string sysMajorClassApi => $"{Host}api/organize/GetsysMajorClassList";
       /// <summary>
       /// lấy bác sĩ ngoại trú
       /// </summary>
        public string StaffByDutyCodeApi => $"{Host}api/organize/GetStaffByDutyCode";
        /// <summary>
        /// lấy cấu hình lỗi
        /// </summary>
        public string FailedCodeMessageMapApi => $"{Host}api/organize/GetSysFailedCodeMessageMapList";

        #endregion

        public async Task<List<SysOrgVo>> OrgListAsync(string keyword)
        {
            var cacheKey = SystemKey.AssemblyOrgListKey(TopOrganizeId);
            var keyValue = RedisHelper.GetList<SysOrgVo>(cacheKey);
            if (keyValue != null && keyValue.Count > 0)
            {
                return keyValue;
            }
            var orgList = await HttpPostWithToken<List<SysOrgVo>, string>(keyword, OrgListApi,AppId);
            if (orgList.Count > 0)
            {
                RedisHelper.SetList(cacheKey, orgList);
                return orgList;
            }
            return default;
        }
        public async Task<string?> GetNameByOrgId(string orgId)
        {
            var orgList = await HttpPostWithToken<List<SysOrgVo>, string>(orgId, OrgListApi, AppId);
            if (orgList.Count() > 0)
            {
                return orgList.Where(p => p.OrganizeId == orgId).FirstOrDefault()?.Name;
            }
            return null;
        }
        /// <summary>
        /// lấy bảng quan hệ người dùng
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysStaffDutyComplexVEntity>> GetsysStaffDutyListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysStaffDutyComplexVEntity>, string>(orgId, sysStaffDutyListApi, AppId);
        }
        /// <summary>
        /// bảng tính chất bệnh nhân
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysPatientNatureEntity>> GetsysatientNatureListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysPatientNatureEntity>, string>(orgId, sysPatientNatureListApi, AppId);
        }
        /// <summary>
        /// lấy khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysDepartmentEntity>> GetsysDepartListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysDepartmentEntity>, string>(orgId, sysDepartListApi, AppId);
        }
        /// <summary>
        /// lấy khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysWardVEntity>> GetsysPatiAreaListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysWardVEntity>, string>(orgId, sysPatiAreaListApi, AppId);
        }
        /// <summary>
        ///lấy khu bệnh theo khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysDepartmentWardRelationVO>> GetsysWardDeptRelationAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysDepartmentWardRelationVO>, string>(orgId, sysWardDeptRelationApi, AppId);
        }
        /// <summary>
        /// lấy nhóm lớn
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysChargeCategoryVEntity>> GetsysMajorClassListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysChargeCategoryVEntity>, string>(orgId, sysMajorClassApi, AppId);
        }

        /// <summary>
        /// lấy bác sĩ ngoại trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysDutyStaffVO>> GetStaffByDutyCodeAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysDutyStaffVO>, string>(orgId, StaffByDutyCodeApi, AppId);
        }
        /// <summary>
        /// lấy cấu hình lỗi
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysFailedCodeMessageMappEntity>> GetSysFailedCodeMessageMapListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysFailedCodeMessageMappEntity>, string>(orgId, FailedCodeMessageMapApi, AppId);
        }
        
    }
}
