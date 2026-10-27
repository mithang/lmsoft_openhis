using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using System.Data.SqlClient;
using System.Data;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Menu hệ thống、Nút menu DmnService
    /// </summary>
    public class SysModuleDmnService : DmnServiceBase, ISysModuleDmnService
    {
        private readonly ISysModuleRepo _sysModuleRepository;
        private readonly ISysModuleButtonRepo _moduleButtonRepo;
        private readonly ISysRoleAuthorizeRepo _sysRoleAuthorizeRepository;
        private readonly ISysOrganizeAuthorizeRepo _sysOrganizeAuthorizeRepo;
        private readonly ISysRoleRepo _sysRoleRepo;

        public SysModuleDmnService( ISysModuleRepo sysModuleRepository
            , ISysModuleButtonRepo moduleButtonRepo, ISysRoleAuthorizeRepo sysRoleAuthorizeRepository
            , ISysOrganizeAuthorizeRepo sysOrganizeAuthorizeRepo, ISysRoleRepo sysRoleRepo)
            
        {
            this._sysModuleRepository = sysModuleRepository;
            this._moduleButtonRepo = moduleButtonRepo;
            this._sysRoleAuthorizeRepository = sysRoleAuthorizeRepository;
            this._sysOrganizeAuthorizeRepo = sysOrganizeAuthorizeRepo;
            this._sysRoleRepo = sysRoleRepo;
        }

        /// <summary>
        /// Theo vai trò Lấy đã cấu hình Quyền Menu
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<SysModuleEntity> GetMenuListByTopOrg(string orgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false)
        {
            var query = _sysModuleRepository.IQueryable().Where(p => p.zt == "1" && p.Target != EnumModuleTargetType.subpage.ToString());
            if (isAdministrator)
            {
                var mIdList = new List<string>();
                var adminRoleId = _sysRoleRepo.IQueryable().Where(p => p.OrganizeId == "*" && p.Code == "Administrator").Select(p => p.Id).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(adminRoleId))
                {
                    //ChoRoleđang mở
                    mIdList = _sysRoleAuthorizeRepository.IQueryable(t => t.zt == "1"
    && t.RoleId == adminRoleId).Select(p => p.ItemId).ToList();
                }
                query = query.Where(p => p.Name == "Quan ly he thong"
                   || p.Name == "Quan ly vai tro" || p.Name == "Vai tro he thong" || p.Name == "Quan ly tu dien"
                   || p.Name == "To chuc" || p.Name == "Quan ly to chuc"
                   || p.Name == "Khoa he thong" || p.Name == "Quan ly khoa" || p.Name == "Nhan su he thong" || p.Name == "Nguoi dung he thong"
                   || (mIdList.Contains(p.Id)));
            }
            else
            {
                //mở cho vai trò
                if (roleIdList == null)
                {
                    return null;
                }

                //Choorganizeđang mở
                var orgItemIdList = _sysModuleRepository.IQueryable(t => t.zt == "1"
     && (t.OrganizeId == null || t.OrganizeId == orgId))
     .Select(p => p.Id).ToList();
                //ChoRoleđang mở
                var idList = _sysRoleAuthorizeRepository.IQueryable(t => t.zt == "1"
&& roleIdList.Contains(t.RoleId)).Select(p => p.ItemId).ToList();
                query = query.Where(p => orgItemIdList.Contains(p.Id) && idList.Contains(p.Id));
            }
            var showDescToMenuTitle = ConfigurationHelper.GetAppConfigBoolValue("IS_ShowDescToMenuTitle");
            if (showDescToMenuTitle == false)
            {
                //Không hiển thị trong menuDescription
                var list = query.ToList();
                list.ForEach(p => p.Description = null);
                return list.OrderBy(t => t.px).ToList();
            }
            return query.OrderBy(t => t.px).ToList();
        }

        /// <summary>
        /// Lấy tổ chức Menu hệ thống   grid
        /// </summary>
        /// <returns></returns>
        public IList<SysModuleVO> GetMenuListByTopOrg(string topOrgId)
        {
            return this.FindList<SysModuleVO>(@"select * from
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

) as t", new SqlParameter[] {
                new SqlParameter("@topOrganizeId", topOrgId)
            });
        }

        /// <summary>
        /// Lấy tổ chức Menu hệ thống Nút    grid
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
) as t", new SqlParameter[] {
                new SqlParameter("@topOrganizeId", topOrgId)
                ,new SqlParameter("@moduleId", moduleId)
            });
        }

        /// <summary>
        /// Lấy tổ chức Đã mở menu của
        /// </summary>
        /// <returns></returns>
        public IList<SysModuleEntity> GetOpenMenuListByTopOrg(string topOrgId)
        {
            return this.FindList<SysModuleEntity>(@"select a.*
from Sys_Module a
left join Sys_OrganizeAuthorize b
on a.Id = b.ItemId
where b.ItemType = 1
and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)", new SqlParameter[] {
                new SqlParameter("@topOrganizeId", topOrgId)
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
and (b.TopOrganizeId = '*' or b.TopOrganizeId = @topOrganizeId)", new SqlParameter[] {
                new SqlParameter("@topOrganizeId", topOrgId)
            });
        }

        /// <summary>
        /// Gửi tạo mới、Cập nhật Menu
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysModuleEntity moduleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleEntity.Modify(keyValue);
                _sysModuleRepository.Update(moduleEntity);
            }
            else
            {
                //Thêm mới
                moduleEntity.Create(true);
                _sysModuleRepository.Insert(moduleEntity);
            }
        }

        /// <summary>
        /// Gửi tạo mới、Cập nhật Nút quyền
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
                using (var db = new DbTrans())
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
                Sql.ExecuteCommand(sql, new SqlParameter[] {
                    new SqlParameter("@moduleId", moduleId),
                    new SqlParameter("@itemType", itemType),
                });
            }
            else
            {
                var sql = @"
delete from Sys_OrganizeAuthorize where ItemId = @moduleId and ItemType = @itemType
insert into Sys_OrganizeAuthorize(Id, TopOrganizeId, ItemId, ItemType, CreateTime, CreatorCode,zt)
select newid(), Id, @moduleId, @itemType, GETDATE(), @creatorCode, '1'
from Sys_Organize where zt = '1' and Id in (
    select value from dbo.SplitToTable(@orgIdArr, ',')
)";
                Sql.ExecuteCommand(sql, new SqlParameter[] {
                    new SqlParameter("@moduleId", moduleId),
                    new SqlParameter("@itemType", itemType),
                    new SqlParameter("@creatorCode", OperatorProvider.GetCurrent().UserCode),
                    new SqlParameter("@orgIdArr", SqlDbType.VarChar, -1) { Value = orgIdArr }
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
            Sql.ExecuteCommand(sql, new SqlParameter[] {
                new SqlParameter("@moduleId", moduleId),
                new SqlParameter("@itemType", itemType),
                new SqlParameter("@creatorCode", OperatorProvider.GetCurrent().UserCode)
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
            this.ExecuteSqlCommand(sql, new SqlParameter[] {
                new SqlParameter("@moduleId", moduleId)
                ,new SqlParameter("@itemType", itemType)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public IList<SysOrganizeEntity> GetAuthedOrgListByModuleId(string itemId, int itemType)
        {
            var sql = @"if exists (select 1 from Sys_OrganizeAuthorize where ItemId = @itemId and ItemType = @itemType and TopOrganizeId = '*')
begin 
    --Tất cả tổ chức cấp cao nhất
	select * from Sys_Organize where ParentId is null and zt = '1'
end
else
	select distinct b.* from Sys_OrganizeAuthorize a
	left join Sys_Organize b
	on a.TopOrganizeId = b.Id
	where a.ItemId = @itemId and a.ItemType = @itemType";

            return this.FindList<SysOrganizeEntity>(sql, new SqlParameter[] {
                new SqlParameter("@itemId", itemId)
                ,new SqlParameter("@itemType", itemType)
            });
        }

        /// <summary>
        /// Xem người dùng có thể truy cập hệ thống này không（Xét theo menu quyền đã mở cho người đó）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="topOrgId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool CheckLoginable(string topOrgId, string account)
        {
            var sql = @"
SELECT 1
FROM [dbo].[Sys_RoleAuthorize]
WHERE zt='1'
        AND ItemType=1
        AND RoleId in
    (SELECT roleId
    FROM [dbo].[Sys_UserRole]
    WHERE zt='1'
            AND userId = 
        (SELECT Id
        FROM [Sys_User]
        WHERE zt='1'
                AND Account=@account
                AND topOrganizeId=@topOrgId ))
";
            var list = this.FindList<int>(sql, new SqlParameter[] {
                new SqlParameter("@topOrgId", topOrgId)
                ,new SqlParameter("@account", account)
            });
            if (list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy menu hệ thống grid
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysModuleEntity> GetMenuListByOrg(string orgId)
        {
            var sql = @"select * from Sys_Module(nolock)
where 
(
(@orgId <> '' and (OrganizeId is null or OrganizeId = @orgId))
or
(@orgId = '' and (OrganizeId is null))
)
order by isnull(px, 99999)";
            return this.FindList<SysModuleEntity>(sql, new[] { new SqlParameter("@orgId", orgId ?? "") });
        }

    }

}
