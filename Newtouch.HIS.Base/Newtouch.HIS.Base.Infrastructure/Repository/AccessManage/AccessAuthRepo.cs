using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository.AccessManage
{
    /// <summary>
    /// Ported from Newtouch.HIS.Repository/AccessManage/AccessAuthRepo.cs. The original
    /// file was present on disk but NOT listed in the old csproj Compile list, so the
    /// mechanical port skipped it; the interface (and RegistorController) still needed
    /// it. EF factory ctor stripped (SqlSugar base needs none).
    /// </summary>
    public class AccessAuthRepo : RepositoryBase<AccessAuthEntity>, IAccessAuthRepo
    {
        public IList<AccessAuthEntity> FindList(Pagination pagination, string keyword, string orgId)
        {
            string sql = "select * from xt_accessregistor with(nolock) where zt='1'";
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and OrganizeId=@orgId";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (RegName like @keyword or RegCode like @keyword or py like @keyword)  ";
            }
            return QueryWithPage<AccessAuthEntity>(sql, pagination, new SqlParameter[] {
                new SqlParameter("orgId",orgId),
                new SqlParameter("keyword","%"+keyword+"%") });
        }

        public IList<AccessAuthEntity> FindList(string code, string orgId)
        {
            string sql = "select * from xt_accessregistor with(nolock) where zt='1' and OrganizeId=@orgId";
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += " and RegCode =@code ";
            }
            return FindList<AccessAuthEntity>(sql, new SqlParameter[] {
                new SqlParameter("orgId",orgId),
                new SqlParameter("code",code??"") });
        }

        public AccessAuthEntity FindEntity(string keyValue)
        {
            var ety = FindEntity(p => p.Id == keyValue);
            if (!string.IsNullOrWhiteSpace(ety.accesskey))
            {
                ety.accesskey = "";
            }
            return ety;
        }

        public AccessAuthEntity FindEntity(string keyValue, string valid)
        {
            var ety = FindEntity(p => p.Id == keyValue && p.zt == valid);
            if (!string.IsNullOrWhiteSpace(ety.accesskey))
            {
                ety.accesskey = "";
            }
            return ety;
        }

        public void RegistProcess(AccessAuthEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(entity.accesskey))
            {
                entity.accesskey = "";
            }
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                var old = FindEntity(p => p.Id == keyValue);
                if (entity.zt == "0")
                {
                    old.zt = "0";
                    old.Modify(keyValue);
                    Update(old);
                }
                else
                {
                    var ext = FindEntity(p => p.RegCode == entity.RegCode && p.OrganizeId == entity.OrganizeId && p.zt == "1" && p.Id != keyValue);
                    if (ext != null)
                    {
                        throw new FailedException("Mã đã tồn tại, vui lòng đổi mã khác");
                    }
                    else
                    {
                        old.RegCode = entity.RegCode;
                        old.RegName = entity.RegName;
                        old.AuthorizedLev = entity.AuthorizedLev;
                        old.AuthorizedPeriod = entity.AuthorizedPeriod;
                        old.accesskey = entity.accesskey;
                        if (old.Memo != entity.Memo)
                        {
                            old.Memo = old.Memo + ";" + DateTime.Now.ToString("yyyy-MM-dd") + " Cập nhật:" + entity.Memo;
                        }
                        old.Modify(keyValue);
                        Update(old);
                    }
                }
            }
            else
            {
                var ext = FindEntity(p => p.RegCode == entity.RegCode && p.OrganizeId == entity.OrganizeId && p.zt == "1");
                if (ext != null)
                {
                    throw new FailedException("Mã đã tồn tại, vui lòng đổi mã khác");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }
    }
}
