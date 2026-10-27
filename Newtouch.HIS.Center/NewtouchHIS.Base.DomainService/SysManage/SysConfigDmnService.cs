using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.EnumExtend;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.DataBaseSvr;

namespace NewtouchHIS.Base.DomainService.SysManage
{
    /// <summary>
    /// Cấu hình cơ sở hệ thống(Chỉ truy vấn)
    /// </summary>
    public class SysConfigDmnService : BaseDmnService<SysOrganizeEntity>, ISysConfigDmnService
    {
        private readonly ISysRoleDmnService _roleDmn;
        /// <summary>
        /// Chức năng dùng chung đa ứng dụng cần cấu hình cơ sở dữ liệu chính
        /// </summary>
        private string _mainDB = ConfigInitHelper.DbConfig.MainDB ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "DbConfig.MainDB");
        /// <summary>
        /// Ứng dụng cần cấu hình cơ sở dữ liệu chính
        /// </summary>
        public SysConfigDmnService(ISysRoleDmnService roleDmn)
        {
            _roleDmn = roleDmn;
            base.Context = SqlSugarDbContext.Db.GetConnection(_mainDB);
        }
        /// <summary>
        /// Danh sách menu(Chỉ quản trị viên được xem)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <param name="validLimit"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public async Task<List<SysModuleVO>?> GetMenuListbyAppId(string orgId, string appId, string userId = null, bool validLimit = true, bool? systemMenuLimit = true)
        {
            var isAdmin = await _roleDmn.IsHospAdmin(userId, orgId);
            if (!isAdmin && userId!="root")
            {
                throw new FailedException(ResponseResultCode.FAILOfAuth, "Chỉ quản trị viên mới được thao tác");
            }
            string db = GetAppDB(appId) ?? "";
            if (string.IsNullOrWhiteSpace(db))
            {
                new FailedException(ResponseResultCode.FAILOfConfigInit, "AppAPIHostName");
            }
            var query = await GetByWhere<SysModuleEntity>(db, p => (p.OrganizeId == null || p.OrganizeId == orgId) && p.Target != EnumModuleTargetType.subpage.ToString());
            if (validLimit)
            {
                query = query.Where(p => p.zt == "1").ToList();
            }
            //Loại trừ menu hệ thống
            if (systemMenuLimit == true)
            {
                var sysMenu = query.Where(p => p.Name == "Quan ly he thong").FirstOrDefault();
                if (sysMenu != null)
                {
                    query = query.Where(p => !(p.Id == sysMenu.Id || p.ParentId == sysMenu.Id)).ToList();
                }
            }
            if (query == null || query.Count == 0)
            {
                return null;
            }
            return query.OrderBy(t => t.px).ToList().Adapt<List<SysModuleVO>>();
        }
        /// <summary>
        /// Lấy danh sách menu mở cho vai trò hiện tại(Tải cây menu mở)
        /// Phân biệt hệ thống nghiệp vụ
        /// </summary>
        /// <param name="orgId">Y tếTổ chức</param>
        /// <param name="appId">Ứng dụng hệ thống nghiệp vụId</param>
        /// <param name="roleIdList"></param>
        /// <param name="isRoot"></param>
        /// <param name="isAdministrator"></param>
        /// <returns></returns>
        public async Task<List<SysModuleEntity>?> GetMenuList(string appId, string orgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false, string? userId = null, bool validLimit = true)
        {
            string db = GetAppDB(appId) ?? "";
            if (string.IsNullOrWhiteSpace(db))
            {
                throw new FailedException(ResponseResultCode.FAILOfConfigInit, "AppAPIHostName");
            }
            var query = await GetByWhere<SysModuleEntity>(db, p => (p.OrganizeId == null || p.OrganizeId == orgId) && p.Target != EnumModuleTargetType.subpage.ToString());
            if (validLimit)
            {
                query = query.Where(p => p.zt == "1").ToList();
            }
            if (query.Count() == 0)
            {
                return default;
            }
            if (isRoot || isAdministrator)
            {
                var sysMenu = query.Where(p => p.Name == "Quan ly he thong"
                    || p.Name == "Menu he thong" || p.Name == "Quan ly menu"
                    || p.Name == "Cau hinh FailMsg"    //
                    || p.Name == "Menu tac vu" //
                    || p.Name == "Nhat ky he thong"
                    || p.Name == "Nguoi dung he thong"
                    || p.Name == "Quan ly vai tro" || p.Name == "Vai tro he thong").ToList();
                query = query.Where(p => sysMenu.Select(x => x.Id).ToList().Contains(p.Id)
                    || sysMenu.Select(y => y.ParentId).Where(y => !string.IsNullOrWhiteSpace(y)).ToList().Contains(p.Id)).ToList();
            }
            else
            {
                if (roleIdList == null)
                {
                    return null;
                }

                //Tớiorganizemở
                var orgAuthedModuleId = query.Select(p => p.Id).ToList();
                //TớiRolemở                
                List<SysModuleEntity> list = new List<SysModuleEntity>();
                var idList = (await GetByWhere<SysRoleAuthorizeEntity>(db, t => t.zt == "1" && roleIdList.Contains(t.RoleId)))?.Select(p => p.ItemId);
                if (idList != null && idList.Any())
                {
                    list = query.Where(p => orgAuthedModuleId.Contains(p.Id) && idList.Contains(p.Id)).ToList();
                }
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    var dbRoleList = await _roleDmn.GetUserRoleList(userId, orgId);
                    if (dbRoleList != null && dbRoleList.Count > 0)
                    {
                        var dbIdList = (await GetByWhere<SysRoleAuthorizeEntity>(db, t => t.zt == "1" && dbRoleList.Select(p => p.Id).Contains(t.RoleId)))?.Select(p => p.ItemId);
                        if (dbIdList != null && dbIdList.Count() > 0)
                        {
                            var dbModules = query.Where(p => orgAuthedModuleId.Contains(p.Id) && dbIdList.Contains(p.Id)).ToList();
                            if (dbModules != null && dbModules.Count() > 0)
                            {
                                list.AddRange(dbModules);
                            }
                        }
                    }
                }
                if (list.Count == 0)
                {
                    return null;
                }
            }
            bool showDescToMenuTitle = AppSettings.GetAppConfigBoolValueExt("IS_ShowDescToMenuTitle");
            if (!showDescToMenuTitle)
            {
                //Không hiển thị trong menuDescription
                var list = query.ToList();
                list.ForEach(p => p.Description = null);
                return list.OrderBy(t => t.px).ToList();
            }
            return query.OrderBy(t => t.px).ToList();
        }
        /// <summary>
        /// Lấy danh sách menu mở cho vai trò hiện tại(Tải cây menu mở)
        /// </summary>
        /// <param name="orgId">Y tếTổ chức</param>
        /// <param name="roleIdList"></param>
        /// <param name="isRoot"></param>
        /// <param name="isAdministrator"></param>
        /// <returns></returns>
        public async Task<List<SysModuleEntity>?> GetMenuList(string orgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false, string? userId = null, bool validLimit = true)
        {
            var query = await GetByWhere<SysModuleEntity>(p => (p.OrganizeId == null || p.OrganizeId == orgId) && p.Target != EnumModuleTargetType.subpage.ToString());
            if (validLimit)
            {
                query = query.Where(p => p.zt == "1").ToList();
            }
            if (query.Count() == 0)
            {
                return default;
            }
            if (isRoot || isAdministrator)
            {
                var sysMenu = query.Where(p => p.Name == "Quan ly he thong"
                     || p.Name == "Menu he thong" || p.Name == "Quan ly menu"
                     || p.Name == "Cau hinh FailMsg"    //
                     || p.Name == "Menu tac vu" //
                     || p.Name == "Nhat ky he thong"
                     || p.Name == "Nguoi dung he thong"
                     || p.Name == "Quan ly vai tro" || p.Name == "Vai tro he thong").ToList();
                query = query.Where(p => sysMenu.Select(x => x.Id).ToList().Contains(p.Id)
                    || sysMenu.Select(y => y.ParentId).Where(y => !string.IsNullOrWhiteSpace(y)).ToList().Contains(p.Id)).ToList();
            }
            else
            {
                if (roleIdList == null)
                {
                    return null;
                }

                //Tớiorganizemở
                var orgAuthedModuleId = query.Select(p => p.Id).ToList();
                //TớiRolemở                
                List<SysModuleEntity> list = new List<SysModuleEntity>();
                var idList = (await GetByWhere<SysRoleAuthorizeEntity>(t => t.zt == "1" && roleIdList.Contains(t.RoleId)))?.Select(p => p.ItemId);
                if (idList != null && idList.Any())
                {
                    list = query.Where(p => orgAuthedModuleId.Contains(p.Id) && idList.Contains(p.Id)).ToList();
                }
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    var dbRoleList = await _roleDmn.GetUserRoleList(userId, orgId);
                    if (dbRoleList != null && dbRoleList.Count > 0)
                    {
                        var dbIdList = (await GetByWhere<SysRoleAuthorizeEntity>(t => t.zt == "1" && dbRoleList.Select(p => p.Id).Contains(t.RoleId)))?.Select(p => p.ItemId);
                        if (dbIdList != null && dbIdList.Count() > 0)
                        {
                            var dbModules = query.Where(p => orgAuthedModuleId.Contains(p.Id) && dbIdList.Contains(p.Id)).ToList();
                            if (dbModules != null && dbModules.Count() > 0)
                            {
                                list.AddRange(dbModules);
                            }
                        }
                    }
                }
                if (list.Count == 0)
                {
                    return null;
                }
                return list;
            }
            //bool showDescToMenuTitle = AppSettings.GetAppConfigBoolValueExt("IS_ShowDescToMenuTitle");
            //if (!showDescToMenuTitle)
            //{
            //    //Không hiển thị trong menuDescription
            //    var list = query.ToList();
            //    list.ForEach(p => p.Description = null);
            //    return list.OrderBy(t => t.px).ToList();
            //}
            return query.OrderBy(t => t.px).ToList();
        }

        /// <summary>
        /// Bảng từ điển
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysItemExtendVO>> GetItemDetailsList(string orgId)
        {
            List<string> org = new List<string> { "*", orgId };
            List<SysItemsDetailEntity> itemdetaildata = await GetByWhereWithAttr<SysItemsDetailEntity>(p => p.zt == "1" && org.Contains(p.OrganizeId));
            List<SysItemsEntity> itemdata = await GetByWhereWithAttr<SysItemsEntity>(p => p.zt == "1");
            return (from k in (from p in itemdata
                               where p.zt == "1"
                               select p).ToList()
                    select new
                    {
                        Type = k.Code,
                        Items = (from t in itemdetaildata
                                 where t.ItemId.Equals(k.Id)
                                 select t into p
                                 orderby p.zt == "1" descending, p.px
                                 select new { p.Name, p.Code, p.zt }).ToList()
                    }).ToList().Adapt<List<SysItemExtendVO>>();
        }



    }
}
