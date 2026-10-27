using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Exceptions;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Local SqlSugar implementations of the FrameworkBase-only system
    /// repositories. Ported from the Sett module's SysConfigRepo /
    /// SysUserRoleRepo with identical semantics; only the data access
    /// changed from EF6 to SqlSugar over EmrDb.
    ///
    /// TABLE LOCATION (verified 2026-10-26 via sqlcmd against the live
    /// instance): Sys_Config and Sys_UserRole exist in Newtouch_EMR (this
    /// classlib's database) with the shapes these entities declare. They
    /// do NOT exist in NewtouchHIS_Base (that database only has
    /// ReportSvsConfig/Sys_GlobalConfig matching %Config%), so the
    /// three-part [NewtouchHIS_Base].[dbo].[Sys_Config] name from the
    /// original brief would fail at runtime here with "Invalid object
    /// name". These repos therefore query the tables unqualified on the
    /// EmrDb connection. If a future deployment restores Sys_Config into
    /// the Base database, switch the Sys_Config references to the
    /// three-part name.
    /// </summary>
    public class SysConfigRepo : RepositoryBase<SysConfigEntity>, ISysConfigRepo
    {
        public SysConfigEntity GetByCode(string code, string orgId)
        {
            return this.FindEntity(p => p.Code == code && p.OrganizeId == orgId && p.zt == "1");
        }

        public string GetValueByCode(string code, string orgId)
        {
            return this.IQueryable().Where(p => p.Code == code && p.OrganizeId == orgId && p.zt == "1")
                .Select(p => p.Value).Take(1).ToList().FirstOrDefault() ?? "";
        }

        public int GetIntValueByCode(string code, string orgId, int defaultValue = 0)
        {
            var val = GetValueByCode(code, orgId);
            if (val != null)
            {
                int returnVal;
                if (int.TryParse(val, out returnVal))
                {
                    return returnVal;
                }
                else
                {
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }

        public bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null)
        {
            var val = GetValueByCode(code, orgId);
            if (val == "1" || val == "0")
            {
                return val == "1";
            }
            else if (val != null)
            {
                bool returnVal;
                if (bool.TryParse(val, out returnVal))
                {
                    return returnVal;
                }
                else
                {
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }

        public IList<SysConfigEntity> GetList(string keyword, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return null;
            }
            // NOTE: ORDER BY is legal here because FindList (non-paged)
            // does not wrap the SQL in a COUNT derived table.
            var sql = @"select * from Sys_Config(nolock) where OrganizeId = @orgId and (Code like @searchKeyword or Name like @searchKeyword) order by Code asc";
            return this.FindList<SysConfigEntity>(sql, new SugarParameter[] {
                    new SugarParameter("@orgId", organizeId),
                    new SugarParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                });
        }

        public SysConfigEntity GetForm(string keyValue)
        {
            return this.FindEntity(keyValue);
        }

        public void SubmitForm(SysConfigEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                if (this.IQueryable().Where(p => p.Code == entity.Code && p.Id != keyValue && p.OrganizeId == entity.OrganizeId).Take(1).ToList().Count > 0)
                {
                    throw new FailedException("Mã không được trùng");
                }
                entity.Id = keyValue;
                this.Update(entity);
            }
            else
            {
                if (this.IQueryable().Where(p => p.Code == entity.Code && p.OrganizeId == entity.OrganizeId).Take(1).ToList().Count > 0)
                {
                    throw new FailedException("Mã không được trùng");
                }
                this.Insert(entity);
            }
        }
    }

    public class SysUserRoleRepo : RepositoryBase<SysUserRoleEntity>, ISysUserRoleRepo
    {
        public IList<string> GetRoleIdListByUserId(string userId)
        {
            var sql = "select RoleId from Sys_UserRole(nolock) where UserId = @userId and zt = '1'";
            return EmrDb.GetConnection().Ado.SqlQuery<string>(sql, new SugarParameter[] {
                new SugarParameter("@userId", userId)
            });
        }
    }
}
