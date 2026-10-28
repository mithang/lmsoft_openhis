using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.EnumExtend;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService.SysManage
{
    /// <summary>
    /// Menu hệ thống
    /// Chức năng dùng chung cho các hệ thống,DBContextChọn làm cơ sở dữ liệu chính,khuyên tham chiếu
    /// </summary>
    public class SysModuleDmnService : BaseDmnService<SysModuleEntity>, ISysModuleDmnService
    {
        private readonly ISysRoleDmnService _roleDmn;
        /// <summary>
        /// Ứng dụng cần cấu hình cơ sở dữ liệu chính
        /// </summary>
        private string _mainDB = ConfigInitHelper.DbConfig.MainDB ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "DbConfig.MainDB");
        public SysModuleDmnService(ISysRoleDmnService roleDmn)
        {
            _roleDmn = roleDmn;
            base.Context = SqlSugarDbContext.Db.GetConnection(_mainDB);
        }

        public async Task<SysModuleEntity?> GetEntity(string id)
        {
            return await FindKey(id);
        }
        public async Task<List<SysModuleEntity>> GetEntitybyorgId(string orgId)
        {
            return await GetByWhere<SysModuleEntity>(DBEnum.UnionDb.ToString(), p => p.zt == "1" && p.OrganizeId == orgId);
        }
        public async Task<SysModuleEntity?> GetEntity(string id, bool valid = true)
        {
            var ety = await GetByWhere(p => p.Id == id);
            if (valid && ety != null)
            {
                return ety.Where(p => p.zt == "1").FirstOrDefault();
            }
            return ety?.FirstOrDefault();
        }

        public async Task<BusResult<string>> AddRange(List<SysModuleVO> vo, string user, string orgId, string defaultParentId)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(orgId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin người dùng không được để trống" };
            }
            if (!(await MenuEditAuthcation(user, orgId)))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Nhân viên thao tác không đủ quyền" };
            }
            //Tìm menu trùng lặp
            var menuNames = vo.Select(p => p.Name);
            var existsMenu = await GetByWhere(p => menuNames.Contains(p.Name) && p.OrganizeId == orgId && p.zt == "1");
            if (existsMenu != null && existsMenu.Count() > 0)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = $"Menu đã tồn tại:{existsMenu.Select(p => p.Name).ToList().ToJson()}" };
            }
            var parentMenu = vo.Where(p => string.IsNullOrWhiteSpace(p.ParentId)).ToList();
            // FIX-1 (A-02): map from each SOURCE menu Id to the NEW Guid assigned
            // to its copy. The old code looked the parent up in `parentMenu` (the
            // pre-copy VOs, still carrying source Ids) and matched by Name, so a
            // child copy ended up pointing at the source parent Id (dangling in
            // the Union DB), and a copied root never got defaultParentId.
            var oldToNewId = new Dictionary<string, string>();
            if (parentMenu != null && parentMenu.Count() > 0)
            {
                var pEntityList = parentMenu.Adapt<List<SysModuleEntity>>();
                for (var i = 0; i < pEntityList.Count; i++)
                {
                    var p = pEntityList[i];
                    p.NewEntity(orgId, user);
                    p.Id = Guid.NewGuid().ToString();
                    p.Target = p.Target ?? EnumModuleTargetType.expand.ToString();
                    // A copied root belongs under the caller-chosen parent
                    // (custom parent created by SubmitMenuSync, or an existing
                    // node); only when no parent was chosen does it stay root.
                    if (!string.IsNullOrWhiteSpace(defaultParentId))
                    {
                        p.ParentId = defaultParentId;
                    }
                    if (!string.IsNullOrWhiteSpace(parentMenu[i].Id))
                    {
                        oldToNewId[parentMenu[i].Id] = p.Id;
                    }
                }
                var parentSync = await AddRange(pEntityList);
                if (parentSync <= 0)
                {
                    return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Thêm menu cấp cha thất bại" };
                }
            }
            //Menu con
            var childMenu = vo.Where(p => !string.IsNullOrWhiteSpace(p.ParentId)).ToList();
            if (childMenu != null && childMenu.Count() > 0)
            {
                var cEntityList = childMenu.Adapt<List<SysModuleEntity>>();
                // Assign new Ids first so deeper levels (a child whose parent
                // is itself a copied child) can also be remapped by Id.
                for (var i = 0; i < cEntityList.Count; i++)
                {
                    cEntityList[i].Id = Guid.NewGuid().ToString();
                    if (!string.IsNullOrWhiteSpace(childMenu[i].Id))
                    {
                        oldToNewId[childMenu[i].Id] = cEntityList[i].Id;
                    }
                }
                foreach (var c in cEntityList)
                {
                    // c.ParentId still holds the SOURCE parent Id at this point
                    // (Adapt copied it from the VO); remap it to the copy's Id.
                    if (!string.IsNullOrWhiteSpace(c.ParentId) && oldToNewId.TryGetValue(c.ParentId, out var newParentId))
                    {
                        c.ParentId = newParentId;
                    }
                    else
                    {
                        // Parent was not part of the copied set: fall back to
                        // the caller-chosen parent, else keep the original value
                        // (previous behaviour for this branch).
                        c.ParentId = defaultParentId ?? c.ParentId;
                    }

                    c.NewEntity(orgId, user);
                    c.Target = c.Target ?? EnumModuleTargetType.iframe.ToString();
                }
                var childSync = await AddRange(cEntityList);
                if (childSync <= 0)
                {
                    return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Thêm menu thất bại" };
                }
            }
            return new BusResult<string> { code = ResponseResultCode.SUCCESS };
        }
        public async Task<BusResult<string>> AddEntity(SysModuleVO vo, string user, string orgId)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(orgId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Thông tin tổ chức và thông tin người dùng không được để trống" };
            }
            //Lấy quyền người dùng
            var userEty = await GetByWhereWithAttr<SysUserEntity>(p => p.Account == user && p.TopOrganizeId == sysConfig.Top_OrganizeId && p.zt == "1");
            if (userEty == null || userEty.Count != 1)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Tài khoản người dùng bất thường" };
            }
            string userId = userEty.FirstOrDefault()?.Id;
            var isAdmin = await _roleDmn.IsHospAdmin(userId, orgId);
            if (!isAdmin)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Thao tác menu chưa được cấp quyền" };
            }
            if (string.IsNullOrWhiteSpace(vo.Name) || string.IsNullOrWhiteSpace(vo.Target))
            {
                return new BusResult<string> { code = ResponseResultCode.FAILOfEmpty, msg = "Thông tin menu chưa đầy đủ:Tên và mục tiêu không được để trống" };
            }
            var exists = await GetByWhere(p => p.Name == vo.Name && p.zt == "1");
            if (exists != null && exists.Count > 0)
            {
                return new BusResult<string> { code = ResponseResultCode.FAILOfExists, msg = "Menu đã tồn tại" };
            }
            var newEty = vo.Adapt<SysModuleEntity>();
            newEty.NewEntity(orgId, user);
            newEty.Id = Guid.NewGuid().ToString();
            var result = await Add(newEty);
            if (result > 0)
            {
                return new BusResult<string> { code = ResponseResultCode.SUCCESS, Data = newEty.Id };
            }
            return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Thao tác thất bại,Vui lòng làm mới và thử lại" };
        }

        public async Task<BusResult<string>> UpdateEntity(SysModuleVO vo, string user, string orgId)
        {
            if (vo == null || string.IsNullOrEmpty(vo.Id) || string.IsNullOrEmpty(user))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Thao tác không hợp lệ,Vui lòng kiểm tra thông tin menu và nhân viên thao tác" };
            }
            if (!(await MenuEditAuthcation(user, orgId)))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Nhân viên thao tác không đủ quyền" };
            }
            if (vo.zt == "1")
            {
                var exists = await GetByWhere(p => p.Name == vo.Name && p.Id != vo.Id && p.zt == "1");
                if (exists != null && exists.Count > 0)
                {
                    return new BusResult<string> { code = ResponseResultCode.FAILOfExists, msg = "Menu đã tồn tại" };
                }
            }

            var ety = await GetFirstOrDefault(p => p.Id == vo.Id && p.OrganizeId == orgId);
            if (ety == null)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy menu liên quan" };
            }
            var newEty = vo.Adapt<SysModuleEntity>();
            newEty.Id = ety.Id;
            newEty.CreateTime = ety.CreateTime;
            newEty.CreatorCode = ety.CreatorCode;
            //newEty.zt = ety.zt;
            newEty.OrganizeId = ety.OrganizeId;
            newEty.ModifiedEntity(orgId, user);
            var result = await Update(newEty);
            return new BusResult<string> { code = result == true ? ResponseResultCode.SUCCESS : ResponseResultCode.FAIL };
        }
        public async Task<BusResult<string>> DelEntity(string keyValue, string user, string orgId)
        {
            if (string.IsNullOrEmpty(keyValue) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(orgId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Thao tác không hợp lệ,Vui lòng kiểm tra thông tin menu và nhân viên thao tác" };
            }
            if (!(await MenuEditAuthcation(user, orgId)))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Nhân viên thao tác không đủ quyền" };
            }
            var ety = await GetFirstOrDefault(p => p.Id == keyValue && p.OrganizeId == orgId);
            if (ety == null)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy menu liên quan" };
            }
            if (ety.zt != "0")
            {
                ety.ModifiedEntity(orgId, user, true);
                var result = await Update(ety);
                if (string.IsNullOrWhiteSpace(ety.ParentId))
                {
                    var child = await GetByWhere(p => p.ParentId == ety.Id);
                    if (child != null)
                    {
                        child.ForEach(p =>
                        {
                            p.ModifiedEntity(orgId, user, true);
                        });
                        if (!(await UpdateRangeAsync(child)))
                        {
                            return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Thêm menu cấp con thất bại" };
                        }
                    }

                }
                return new BusResult<string> { code = result == true ? ResponseResultCode.SUCCESS : ResponseResultCode.FAIL };
            }
            return new BusResult<string> { code = ResponseResultCode.FAILOfRequestRepeat, msg = "Menu đã bị hủy" };
        }


        private async Task<bool> MenuEditAuthcation(string user, string orgId)
        {
            // Giai doan 7-fix: da xoa mien quyen sua menu cho account "root".
            // Tai khoan DB ten "root" (neu tu tao) phai co vai tro HospAdmin nhu moi user.
            //Lấy quyền người dùng
            var userEty = await GetByWhereWithAttr<SysUserEntity>(p => p.Account == user && p.TopOrganizeId == sysConfig.Top_OrganizeId && p.zt == "1");
            if (userEty == null || userEty.Count != 1)
            {
                return false;
            }
            string userId = userEty.FirstOrDefault()?.Id;
            var isAdmin = await _roleDmn.IsHospAdmin(userId, orgId);
            if (!isAdmin)
            {
                return false;
            }
            return true;
        }

    }
}

