using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Framework.Web.ServiceModels;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Services.HttpService;
using SqlSugar;

namespace NewtouchHIS.Framework.Web.Implementation
{
    /// <summary>
    /// chức năng cơ bản và dịch vụ từ điển của hệ thống
    /// </summary>
    public interface ISystemAppService : IScopedDependency
    {
        /// <summary>
        /// menu người dùng đăng nhập hiện tại
        /// </summary>
        /// <returns></returns>
        Task<List<SysModuleVO>?> GetUserMenuAsync(bool? validLimit = true);
        /// <summary>
        /// lấy menu hệ thống
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<SysModuleVO>> GetMenuListAsync(MenuAuthRequest request, string userCacheKey, OperatorModel? user = null);
        /// <summary>
        /// cây menu
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<SysModuleWithChildVO>> GetMenuExtendAsync(MenuAuthRequest request, string userCacheKey, OperatorModel? user = null);
        /// <summary>
        /// truy vấn chi tiết menu
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        Task<SysModuleEntity> GetMenubyIdAsync(string keyValue);

        /// <summary>
        /// lấy quốc tịch
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<SysNationalityVEntity>> GetsysNationalityListAsync(string request);
        /// <summary>
        /// lấy dân tộc
        /// </summary>
        /// <param name="request"></param>
        Task<List<SysNationVEntity>> GetsysNationListAsync(string request);
        /// <summary>
        /// lấy phương thức thanh toán tiền mặt
        /// </summary>
        /// <param name="request"></param>
        Task<List<SysCashPaymentModelEntity>> GetSysForCashPayAsync(string request);
        Task<List<SysItemExtendVO>> GetItemDetailsListAsync(string request);
        /// <summary>
        /// lấy những cái đã đăng kýAPPdanh sách
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<AuthAppVO>> GetRegAppListAsync();
        /// <summary>
        /// lấy cách dùng thuốc
        /// </summary>
        /// <returns></returns>
        Task<List<DrugUsageEntity>?> GetMediUsageListAsync();
        /// <summary>
        /// lấy quan hệ dạng bào chế - cách dùng
        /// </summary>
        /// <returns></returns>
        Task<List<DrugFormulationUsageVO>?> GetDrugFormulationUsageListAsync();
    }
    /// <summary>
    /// giao diện dùng chung của hệ thống
    /// </summary>
    public class SystemAppService : AppServiceBase, ISystemAppService
    {
        public SystemAppService(IHttpClientHelper httpClient, IAuthCenterAppService authCenterApp) : base(httpClient, authCenterApp)
        {
            AppId = ConfigInitHelper.SysConfig.AppAPIHostName?.HisAppBaseAPIHost ?? "HIS.BaseAPI";
            Host = ConfigInitHelper.SysConfig.AppAPIHost?.HisAppBaseAPIHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "HisAppBaseAPIHost");
        }
        /// <summary>
        /// lấy danh sách menu
        /// </summary>
        public string GetMenuListApi => $"{Host}api/System/GetMenuList";
        /// <summary>
        /// lấy danh sách menu，cơ sở dữ liệu tùy chọn
        /// </summary>
        public string GetMenuListbyDBApi => $"{Host}api/System/GetAppMenuList";

        /// <summary>
        /// bảng từ điển hệ thống
        /// </summary>
        public string GetItemDetailsListApi => $"{Host}api/System/GetItemDetailsList";
        /// <summary>
        /// khởi tạo dữ liệu người dùng
        /// </summary>
        public string BuildLoginStatusOprApi => $"{Host}api/System/BuildLoginStatusOpr";
        /// <summary>
        /// lấy thông tin quốc tịch
        /// </summary>
        public string GetsysNationalityListApi => $"{Host}api/System/GetsysNationalityList";
        /// <summary>
        /// lấy thông tin dân tộc
        /// </summary>
        public string GetsysNationListApi => $"{Host}api/System/GetsysNationList";
        /// <summary>
        /// lấy phương thức thanh toán tiền mặt
        /// </summary>
        public string GetSysForCashPayListApi => $"{Host}api/System/GetSysForCashPayList";
        /// <summary>
        /// lấy những cái đã đăng kýAPP
        /// </summary>
        public string GetRegAppListApi => $"{Host}api/System/GetRegAppList";
        /// <summary>
        /// cách dùng thuốc
        /// </summary>
        public string DrugUsageApi => $"{Host}api/System/GetMediUsageList";
        /// <summary>
        /// lấy quan hệ đối chiếu dạng bào chế - cách dùng thuốc
        /// </summary>
        public string DrugFormulationUsageListApi => $"{Host}api/System/GetDrugFormulationUsageList";


        #region Entitythao tác
        /// <summary>
        /// truy vấn thông tin menu
        /// </summary>
        public string Menu_QuerybyId => $"{Host}api/Systemed/GetMenubyId";

        #endregion


        public async Task<List<SysModuleVO>?> GetUserMenuAsync(bool? validLimit = true)
        {
            var currentUser = OperatorProvider.GetCurrent();
            if (currentUser == null)
            {
                return null;
            }
            var request = new MenuAuthRequest
            {
                UserId = currentUser.UserId,
                IsAdministrator = currentUser.IsAdministrator,
                IsRoot = currentUser.IsRoot,
                RoleIdList = currentUser.RoleIdList,
                ValidLimit = validLimit ?? true
            };
            string userCacheKey = Cache_GetUserTokenKey(currentUser.UserCode, currentUser.OrganizeId, AppId);
            return await HttpPostWithToken<List<SysModuleVO>, MenuAuthRequest>(RequestAsync(request, currentUser?.OrganizeId), GetMenuListbyDBApi, userCacheKey, currentUser);
        }
        public async Task<List<SysModuleVO>> GetMenuListAsync(MenuAuthRequest request, string userCacheKey, OperatorModel? user = null)
        {
            return await HttpPostWithToken<List<SysModuleVO>, MenuAuthRequest>(RequestAsync(request, user?.OrganizeId), GetMenuListbyDBApi, userCacheKey, user);
        }
        public async Task<List<SysModuleWithChildVO>> GetMenuExtendAsync(MenuAuthRequest request, string userCacheKey, OperatorModel? user = null)
        {
            var apiResult = await HttpPostWithToken<List<SysModuleVO>, MenuAuthRequest>(RequestAsync(request, user?.OrganizeId ?? user?.TopOrganizeId), GetMenuListbyDBApi, userCacheKey, user);
            return ToMenuJson(apiResult, null);
        }

        public async Task<SysModuleEntity> GetMenubyIdAsync(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return default;
            }
            return await HttpPostWithToken<SysModuleEntity, string>(keyValue, Menu_QuerybyId, AppId);
        }

        public async Task<List<SysItemExtendVO>> GetItemDetailsListAsync(string request)
        {
            return await HttpPostWithToken<List<SysItemExtendVO>, string>(request, GetItemDetailsListApi);
        }
        /// <summary>
        /// lấy quốc tịch
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<SysNationalityVEntity>> GetsysNationalityListAsync(string request)
        {
            List<SysNationalityVEntity> NationalityVEntity = await HttpPostWithToken<List<SysNationalityVEntity>, string>(request, GetsysNationalityListApi);
            return NationalityVEntity;
        }
        /// <summary>
        /// lấy dân tộc
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<SysNationVEntity>> GetsysNationListAsync(string request)
        {
            return await HttpPostWithToken<List<SysNationVEntity>, string>(request, GetsysNationListApi);
        }
        /// <summary>
        /// lấy phương thức thanh toán tiền mặt
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<SysCashPaymentModelEntity>> GetSysForCashPayAsync(string request)
        {
            List<SysCashPaymentModelEntity> sysCashPaymentModelEntities = await HttpPostWithToken<List<SysCashPaymentModelEntity>, string>(request, GetSysForCashPayListApi);
            return sysCashPaymentModelEntities;
        }
        /// <summary>
        /// lấy những cái đã đăng kýAPPdanh sách
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<AuthAppVO>> GetRegAppListAsync()
        {
            return await HttpPostWithToken<List<AuthAppVO>, string>(null, GetRegAppListApi);
        }
        /// <summary>
        /// lấy cách dùng thuốc
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<DrugUsageEntity>?> GetMediUsageListAsync()
        {
            var request = new { code = "", yplx = Convert.ToInt32(null) };
            var resp = await HttpPostAnonymous<List<DrugUsageEntity>>(RequestAsync(request).ToJson(), DrugUsageApi);
            return resp.Data;
        }
        /// <summary>
        /// lấy dạng bào chế - cách dùng thuốc
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<DrugFormulationUsageVO>?> GetDrugFormulationUsageListAsync()
        {
            var resp = await HttpPostAnonymous<List<DrugFormulationUsageVO>>(RequestAsync("").ToJson(), DrugFormulationUsageListApi);
            return resp.Data;
        }


        #region private
        private List<SysModuleWithChildVO> ToMenuJson(List<SysModuleVO> data, string? parentId)
        {
            List<SysModuleWithChildVO> result = new List<SysModuleWithChildVO>();
            List<SysModuleVO> list = data?.Where((SysModuleVO t) => t.ParentId == parentId).ToList();

            if (list != null && list.Count > 0)
            {
                foreach (SysModuleVO item in list)
                {
                    SysModuleWithChildVO vo = item.Adapt<SysModuleWithChildVO>();
                    vo.ChildNodes = ToMenuJson(data, item.Id);
                    result.Add(vo);
                }
            }
            return result;
        }
        #endregion
    }
}
