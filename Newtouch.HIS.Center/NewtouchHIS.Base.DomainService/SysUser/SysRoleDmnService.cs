using NewtouchHIS.Base.Domain.Entity;
using DbConfigHelper;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService
{
    /// <summary>
    /// Vai trò hệ thống và ủy quyền
    /// Chức năng dùng chung cho các hệ thống,DBContext Chọn làm cơ sở dữ liệu chính,khuyên tham chiếu
    /// </summary>
    public class SysRoleDmnService : BaseDmnService<SysRoleEntity>, ISysRoleDmnService
    {
        /// <summary>
        /// Ứng dụng cần cấu hình cơ sở dữ liệu chính
        /// </summary>
        private string _mainDB = ConfigInitHelper.DbConfig.MainDB ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "DbConfig.MainDB");
        public SysRoleDmnService()
        {
            base.Context = SqlSugarDbContext.Db.GetConnection(_mainDB);
        }

        public async Task<List<SysRoleEntity>> GetUserRoleList(string userId, string orgId)
        {
            var baseUserRole = await GetByWhere<SysUserRoleEntity>(p => p.UserId == userId && p.zt == "1");
            var baseRoleIds = baseUserRole.Select(p => p.RoleId);
            return await GetByWhere(p => baseRoleIds.Contains(p.Id) && p.OrganizeId == orgId && p.zt == "1");

        }
        public async Task<bool> IsHospAdmin(string userId, string orgId)
        {
            var baseUserRole = await GetByWhere<SysUserRoleEntity>(p => p.UserId == userId && p.zt == "1");
            var baseRoleIds = baseUserRole.Select(p => p.RoleId);
            var result = await GetByWhere(p => baseRoleIds.Contains(p.Id) && p.OrganizeId == orgId && p.zt == "1");
            if (result.Any(p => p.Code == "HospAdministrator"))
            {
                return true;
            }
            return false;
        }
        public async Task<List<SysRoleEntity>> GetUserRoleList(string userId, string orgId, string db)
        {
            var userRole = await GetByWhere<SysUserRoleEntity>(db, p => p.UserId == userId && p.zt == "1");
            var roleIds = userRole.Select(p => p.RoleId);
            return await GetByWhere<SysRoleEntity>(db, p => roleIds.Contains(p.Id) && p.OrganizeId == orgId && p.zt == "1");
        }
        /// <summary>
        /// Xóa vai trò hệ thống
        /// </summary>
        /// <param name="keyValue"></param>
        public async Task DeleteForm(string keyValue)
        {
            var result = await GetByWhere<SysRoleEntity>(DBEnum.UnionDb.ToString(), p => p.zt == "1" && p.Id == keyValue);
            // NX8-003 (D-NX8-003): GetByWhere tra [] chu khong tra null khi
            // khong co row — guard cu (result == null) chet nen result[0] nem
            // "Index was out of range" thay vi FailedException da viet san.
            if (result == null || result.Count == 0)
            {
                throw new FailedException("Xóa thất bại,Truy vấn không có thông tin");
            }
            SysRoleEntity sysRoleEntity = result[0];
            if (sysRoleEntity.Code == "Administrator")
            {
                throw new FailedException("Mã Administrator được hệ thống giữ lại,không thể xóa");
            }
            await Delete<SysRoleEntity>(sysRoleEntity);
            var sql = "delete from " + DbNames.Name("UnionDb", "Newtouch_Union") + "..Sys_RoleAuthorize where RoleId=@RoleId";

            await ExecuteCommandSql(sql, new SugarParameter[] {
                new SugarParameter("@RoleId",sysRoleEntity.Id) });
        }

        /// <summary>
        /// Nộp mới,Cập nhật Thực thể
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        /// <param name="isAdministrator"></param>
        public async Task SubmitForm(SysRoleEntity roleEntity, List<Domain.Entity.SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue, string UserCode, string orgId)
        {
            if (roleEntity.Code == "Administrator")
            {
                if (string.IsNullOrWhiteSpace(keyValue)
                    || (!string.IsNullOrEmpty(keyValue) && roleEntity.OrganizeId != "*"))
                {
                    throw new FailedException("Mã Administrator được hệ thống giữ lại");
                }
            }
            var result = await GetByWhere<SysRoleEntity>(DBEnum.UnionDb.ToString(), p => p.zt == "1" && p.Id == keyValue);
            // NX18-001 (du thao D-NX18-001): keyValue khong rong ma khong tim
            // thay row (Id la, hoac row da xoa mem zt=0) thi bao loi nghiep vu
            // TRUOC khi cham DB. Nhanh else cu Add(roleEntity) truc tiep:
            // controller chi goi NewEntity (set CreatorCode) khi keyValue
            // rong, nen moi edit voi Id la deu roi vao INSERT thieu
            // CreatorCode -> loi SQL tho "Cannot insert NULL into CreatorCode"
            // (F-NX17-001, probe NX17). Day khong phai upsert chay duoc: Id
            // moi luon la Guid moi o controller (nhanh NewEntity), khong co
            // duong nao tao row moi voi Id do client dua. Tien le: DeleteForm
            // cung file (NX8-003, Count==0 -> FailedException) + SysModule
            // UpdateEntity "Khong tim thay menu lien quan" (NX14-001).
            // Chi doi nhanh loi nay: insert that (keyValue rong) va update
            // that (Id ton tai) khong doi.
            if (!string.IsNullOrWhiteSpace(keyValue) && (result == null || result.Count == 0))
            {
                throw new FailedException("Không tìm thấy vai trò cần sửa");
            }
            if (result != null && result.Count > 0 && keyValue != null && keyValue != "")
            {
                if (result[0] != null)
                {
                    SysRoleEntity sysRoleEntity = result[0];
                    sysRoleEntity.Name = roleEntity.Name;
                    sysRoleEntity.Code = roleEntity.Code;
                    sysRoleEntity.px = roleEntity.px;
                    sysRoleEntity.zt = "1";
                    sysRoleEntity.Description = roleEntity.Description;
                    sysRoleEntity.ModifiedEntity(orgId, UserCode, false);
                    await Update<SysRoleEntity>(sysRoleEntity);
                }
            }
            else
            {
                await Add<SysRoleEntity>(roleEntity);
            }
            //var RoleAuthorize = await GetFirstOrDefaultWithAttr<SysRoleAuthorizeEntity>(p => p.zt == "1" && p.RoleId == roleEntity.Id);
            var sql = "delete from " + DbNames.Name("UnionDb", "Newtouch_Union") + "..Sys_RoleAuthorize where RoleId=@RoleId";

            await ExecuteCommandSql(sql, new SugarParameter[] {
                new SugarParameter("@RoleId",keyValue) });
            for (int i = 0; i < roleAuthorizeEntitys.Count; i++)
            {
                await Add(roleAuthorizeEntitys[i]);
            }
        }
        public async Task<List<SysRoleEntity>> GetRoleList(string orgId)
        {
            return await GetByWhere(p => p.OrganizeId == orgId && p.zt == "1");
        }
        /// <summary>
        /// Tổ chức Danh sách vai trò kèm Phân trang
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<SysRoleEntity>> GetPagintionList(string orgId, string? keyword = null)
        {
            var data = await GetByWhere<SysRoleEntity>(p => p.zt == "1" && p.OrganizeId == orgId);
            return data;
        }

        // NX8-003 (D-NX8-003): data[0] nem "Index was out of range" khi Id
        // khong ton tai; tra null de GetFormJson (SSO + Sett) serialize thanh
        // "null" nhu tien le FA-001/SysModule khi khong tim thay thuc the.
        public async Task<SysRoleEntity?> GetForm(string keyword)
        {
            var data = await GetByWhere<SysRoleEntity>(p => p.Id == keyword);
            return data.FirstOrDefault();
        }
    }
}
