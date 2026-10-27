using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using SqlSugar;
using NewtouchHIS.Lib.Framework.Operator;
using System.Data;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;


namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class SysModuleDmnService : DmnServiceBase, ISysModuleDmnService
    {
        private readonly ISysModuleRepo _sysModuleRepository;
        private readonly ISysModuleButtonRepo _moduleButtonRepo;
        private readonly ISysRoleAuthorizeRepo _sysRoleAuthorizeRepository;
        private readonly ISysOrganizeAuthorizeRepo _sysOrganizeAuthorizeRepo;
        
        /// <summary>
        /// Theo vai trò Lấy đã cấu hình Quyền Thực đơn
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<SysModuleEntity> GetMenuListByTopOrg(string topOrgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false)
        {
            var query = _sysModuleRepository.IQueryable().Where(p => p.zt == "1");
            if (isRoot)
            {
                //all
                //170818Đổi thành chỉ có thể thấy Thực đơn hệ thống
                query = query.Where(p => p.Name == "Quan ly he thong" || p.Name == "Menu he thong" || p.Name == "Quan ly menu" || p.Name == "Cau hinh FailMsg" || p.Name == "Menu tac vu");
            }
            else if (isAdministrator)
            {
                //170818 Hệ thống nghiệp vụ Đổi thành chỉ có thể thấy Quản lý vai trò
                query = query.Where(p => p.Name == "Quan ly he thong" || p.Name == "Quan ly vai tro" || p.Name == "Vai tro he thong");
            }
            else
            {
                if (roleIdList == null)
                {
                    return null;
                }

                //tớiorganizeđang mở
                var orgItemIdList = _sysOrganizeAuthorizeRepo.IQueryable(t => t.zt == "1" && t.ItemType == 1
&& (t.TopOrganizeId == "*" || t.TopOrganizeId == topOrgId)).Select(p => p.ItemId);
                //tớiRoleđang mở
                var idList = _sysRoleAuthorizeRepository.IQueryable(t => t.zt == "1" && t.ItemType == 1
&& roleIdList.Contains(t.RoleId)).Select(p => p.ItemId);
                query = query.Where(p => orgItemIdList.Contains(p.Id) && idList.Contains(p.Id));
            }
            return query.OrderBy(t => t.px).ToList();
        }

        /// <summary>
        /// Theo vai trò Lấy đã cấu hình Quyền Nút
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<SysModuleButtonEntity> GetButtonListByTopOrg(string topOrgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false)
        {
            var data = new List<SysModuleButtonEntity>();
            var query = _moduleButtonRepo.IQueryable().Where(p => p.zt == "1");
            if (isRoot)
            {
                //all
            }
            else if (isAdministrator)
            {
                //tớiorganizeđang mở
                var idList = _sysOrganizeAuthorizeRepo.IQueryable(t => t.zt == "1" && t.ItemType == 2
&& (t.TopOrganizeId == "*" || t.TopOrganizeId == topOrgId)).Select(p => p.ItemId);
                query = query.Where(p => idList.Contains(p.Id));
            }
            else
            {
                if (roleIdList == null)
                {
                    return null;
                }
                //tớiorganizeđang mở
                var orgIdList = _sysOrganizeAuthorizeRepo.IQueryable(t => t.zt == "1" && t.ItemType == 2
&& (t.TopOrganizeId == "*" || t.TopOrganizeId == topOrgId)).Select(p => p.ItemId);
                //tớiRoleđang mở
                var idList = _sysRoleAuthorizeRepository.IQueryable(t => t.zt == "1" && t.ItemType == 2
&& roleIdList.Contains(t.RoleId)).Select(p => p.ItemId);
                query = query.Where(p => orgIdList.Contains(p.Id) && idList.Contains(p.Id));
            }
            return query.OrderBy(t => t.px).ToList();
        }

        /// <summary>
        /// Lấy tổ chức Thực đơn hệ thống
        /// </summary>
        /// <returns></returns>
        public IList<SysModuleVO> GetMenuListByTopOrg(string topOrgId)
        {
            return this.FindList<SysModuleVO>(@"
select * from
(
    select a.*, cast(1 as bit) IsOpenToOrg
    from Sys_Module a
    left join Sys_OrganizeAuthorize b
    on a.Id = b.ItemId and b.ItemType = 1 and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)
    where 
    b.Id is not null

    union

    select a.*, cast(0 as bit) IsOpenToOrg
    from Sys_Module a
    left join Sys_OrganizeAuthorize b
    on a.Id = b.ItemId and b.ItemType = 1 and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)
    where 
    b.Id is null

) as t", new SugarParameter[] {
                new SugarParameter("@topOrganizeId", topOrgId)
            });
        }

        /// <summary>
        /// Lấy tổ chức Thực đơn hệ thống Nút
        /// </summary>
        /// <returns></returns>
        public IList<SysModuleButtonVO> GetMenuButtonListByTopOrg(string topOrgId, string moduleId)
        {
            return this.FindList<SysModuleButtonVO>(@"select * from
(
select a.*, cast(1 as bit) IsOpenToOrg
from Sys_ModuleButton a
left join Sys_OrganizeAuthorize b
on a.Id = b.ItemId
where b.ItemType = 2 and a.ModuleId = @moduleId
and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)
union
(
select a.*, cast(0 as bit) IsOpenToOrg
from Sys_ModuleButton a
where a.ModuleId = @moduleId
and Id not in
(
    select a.Id IsOpenToOrg
    from Sys_ModuleButton a
    left join Sys_OrganizeAuthorize b
    on a.Id = b.ItemId
    where b.ItemType = 2 and a.ModuleId = @moduleId
    and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)
))
) as t", new SugarParameter[] {
                new SugarParameter("@topOrganizeId", topOrgId)
                ,new SugarParameter("@moduleId", moduleId)
            });
        }

        /// <summary>
        /// Lấy tổ chức Đã mở thực đơn của
        /// </summary>
        /// <returns></returns>
        public IList<SysModuleEntity> GetOpenMenuListByTopOrg(string topOrgId)
        {
            return this.FindList<SysModuleEntity>(@"select a.*
from Sys_Module a
left join Sys_OrganizeAuthorize b
on a.Id = b.ItemId
where b.ItemType = 1
and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)", new SugarParameter[] {
                new SugarParameter("@topOrganizeId", topOrgId)
            });
        }

        /// <summary>
        /// Lấy tổ chức Đã mở nút của
        /// </summary>
        /// <returns></returns>
        public IList<SysModuleButtonEntity> GetOpenMenuButtonListByTopOrg(string topOrgId)
        {
            return this.FindList<SysModuleButtonEntity>(@"select a.*
from Sys_ModuleButton a
left join Sys_OrganizeAuthorize b
on a.Id = b.ItemId
where b.ItemType = 2
and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)", new SugarParameter[] {
                new SugarParameter("@topOrganizeId", topOrgId)
            });
        }

        /// <summary>
        /// Gửi mới、Cập nhật Thực đơn
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysModuleEntity moduleEntity, string keyValue, string topOrgId = null)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleEntity.Modify(keyValue);
                _sysModuleRepository.Update(moduleEntity);
            }
            else
            {
                //Thêm mới
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    moduleEntity.Create(true);
                    db.Insert(moduleEntity);

                    if (!string.IsNullOrWhiteSpace(topOrgId))
                    {
                        //Tự động chotopOrganizeIdPhân bổModulequyền của
                        var authEntity = new SysOrganizeAuthorizeEntity()
                        {
                            TopOrganizeId = topOrgId,
                            ItemId = moduleEntity.Id,
                            ItemType = 1
                        };
                        authEntity.Create(true);
                        db.Insert(authEntity);
                    }

                    db.Commit();
                }
            }
        }

        /// <summary>
        /// Gửi mới、Cập nhật Nút quyền
        /// </summary>
        /// <param name="moduleButtonEntity"></param>
        /// <param name="keyValue"></param>
        public void ModuleButtonSubmitForm(SysModuleButtonEntity moduleButtonEntity, string keyValue, string topOrgId = null)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleButtonEntity.Modify(keyValue);
                _moduleButtonRepo.Update(moduleButtonEntity);
            }
            else
            {
                //Thêm mới
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    moduleButtonEntity.Create(true);
                    db.Insert(moduleButtonEntity);

                    if (!string.IsNullOrWhiteSpace(topOrgId))
                    {
                        //Tự động chotopOrganizeIdPhân bổModuleButtonquyền của
                        var authEntity = new SysOrganizeAuthorizeEntity()
                        {
                            TopOrganizeId = topOrgId,
                            ItemId = moduleButtonEntity.Id,
                            ItemType = 2
                        };
                        authEntity.Create(true);
                        db.Insert(authEntity);
                    }

                    db.Commit();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="orgList"></param>
        /// <param name="itemType"></param>
        public void UpdateAuthOrganizeList(string moduleId, string orgList, int itemType)
        {
            var orgIdArr = orgList.Trim(',');
            if (string.IsNullOrWhiteSpace(orgIdArr))
            {
                var sql = @"
delete from Sys_OrganizeAuthorize where ItemId = @moduleId and ItemType = @itemType
";
                _dataContext.Database.ExecuteSqlCommand(sql, new SugarParameter[] {
                    new SugarParameter("@moduleId", moduleId),
                    new SugarParameter("@itemType", itemType),
                });
            }
            else
            {
                var sql = @"
delete from Sys_OrganizeAuthorize where ItemId = @moduleId and ItemType = @itemType
insert into Sys_OrganizeAuthorize(Id, TopOrganizeId, ItemId, ItemType, CreateTime, CreatorCode,zt)
select newid(), Id, @moduleId, @itemType, GETDATE(), @creatorCode, '1'
from [NewtouchHIS_Base]..V_S_Sys_Organize where Id in (
    select value from dbo.SplitToTable(@orgIdArr, ',')
)";
                _dataContext.Database.ExecuteSqlCommand(sql, new SugarParameter[] {
                    new SugarParameter("@moduleId", moduleId),
                    new SugarParameter("@itemType", itemType),
                    new SugarParameter("@creatorCode", OperatorProvider.GetCurrent().UserCode),
                    new SugarParameter("@orgIdArr", "") { DbType = System.Data.DbType.AnsiString, Size = -1, Value = orgIdArr }
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemType"></param>
        public void AuthAllOrganize(string moduleId, int itemType)
        {
            var sql = @"
if exists (select 1 from Sys_Module where Id = @moduleId and @itemType = 1
union select 1 from Sys_ModuleButton where Id = @moduleId and @itemType = 2)
begin
    delete from Sys_OrganizeAuthorize where ItemId = @moduleId and ItemType = @itemType
    insert into Sys_OrganizeAuthorize(Id, TopOrganizeId, ItemId, ItemType, CreateTime, CreatorCode,zt)
    values(newid(), '*', @moduleId, @itemType, GETDATE(), @creatorCode, '1')
end";
            _dataContext.Database.ExecuteSqlCommand(sql, new SugarParameter[] {
                new SugarParameter("@moduleId", moduleId),
                new SugarParameter("@itemType", itemType),
                new SugarParameter("@creatorCode", OperatorProvider.GetCurrent().UserCode)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemType"></param>
        public void AuthCancelAllOrganize(string moduleId, int itemType)
        {
            var sql = @"delete from Sys_OrganizeAuthorize where ItemId = @moduleId and ItemType = @itemType";
            this.ExecuteSqlCommand(sql, new SugarParameter[] {
                new SugarParameter("@moduleId", moduleId)
                ,new SugarParameter("@itemType", itemType)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public IList<SysOrganizeVEntity> GetAuthedOrgListByModuleId(string itemId, int itemType)
        {
            var sql = @"if exists (select 1 from Sys_OrganizeAuthorize where ItemId = @itemId and ItemType = @itemType and TopOrganizeId = '*')
begin 
    --Tất cả tổ chức cấp cao nhất
	select * from [NewtouchHIS_Base]..V_S_Sys_Organize where ParentId is null
end
else
	select distinct b.* from Sys_OrganizeAuthorize a
	left join [NewtouchHIS_Base]..V_S_Sys_Organize b
	on a.TopOrganizeId = b.Id
	where a.ItemId = @itemId and a.ItemType = @itemType";

            return this.FindList<SysOrganizeVEntity>(sql, new SugarParameter[] {
                new SugarParameter("@itemId", itemId)
                ,new SugarParameter("@itemType", itemType)
            });
        }

    }

}
