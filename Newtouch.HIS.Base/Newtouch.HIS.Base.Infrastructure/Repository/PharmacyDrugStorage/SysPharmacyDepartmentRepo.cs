using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPharmacyDepartmentRepo : RepositoryBase<SysPharmacyDepartmentEntity>, ISysPharmacyDepartmentRepo
    {
        public SysPharmacyDepartmentRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentEntity> GetList(string orgId, byte? yjbmjb)
        {
            var strSql = @"select * from xt_yfbm where OrganizeId=@orgId";
            var parList = new List<SqlParameter>() { };
            parList.Add(new SqlParameter("@orgId", orgId ?? ""));
            if (yjbmjb.HasValue)
            {
                strSql += " and yjbmjb = @yjbmjb";
                parList.Add(new SqlParameter("@yjbmjb", yjbmjb.Value));
            }
            return this.FindList<SysPharmacyDepartmentEntity>(strSql, parList.ToArray());
        }

        /// <summary>
        /// Lấy nhà thuốc và kho dược hiệu lực
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentEntity> GetEffectiveList(string orgId, byte? yjbmjb)
        {
            var strSql = @"select * from xt_yfbm where OrganizeId=@orgId and zt='1' ";
            var parList = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId ?? "")
            };
            if (!yjbmjb.HasValue) return FindList<SysPharmacyDepartmentEntity>(strSql, parList.ToArray());
            strSql += " and yjbmjb = @yjbmjb";
            parList.Add(new SqlParameter("@yjbmjb", yjbmjb.Value));
            return FindList<SysPharmacyDepartmentEntity>(strSql, parList.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword = null)
        {
            var strSql = @"select * from xt_yfbm
            where OrganizeId=@organizeId  and (isnull(@keyword, '') = '' or yfbmCode like @searchkeyword or yfbmmc like @searchkeyword or py like @searchkeyword)";
            SqlParameter[] param = new SqlParameter[] {
                            new SqlParameter("@keyword",keyword ?? ""),
                            new SqlParameter("@organizeId",organizeId),
                            new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
                        };
            return this.QueryWithPage<SysPharmacyDepartmentEntity>(strSql, pagination, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysPharmacyDepartmentEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId
                && p.yfbmId != keyValue && p.yfbmCode == entity.yfbmCode
                ))
                {
                    throw new FailedException("Mã khoa không được trùng lặp!");
                }

                SysPharmacyDepartmentEntity oldEntity = null;   //Trước khi thay đổiEntity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.yfbmId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_yfbm", oldEntity.yfbmId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId &&
                p.yfbmCode == entity.yfbmCode))
                {
                    throw new FailedException("Mã khoa không được trùng lặp!");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

        /// <summary>
        /// LấyUserIdcái đã liên kết hiện tại cầnyfbmCode (Không bao gồm tổ chức con)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<string> GetCurPharmacyList(string userId, string orgId)
        {
            var strsql = @"select yfbmCode from Sys_UserYfbm where UserId = @UserId and OrganizeId = @orgId";
            return this.FindList<string>(strsql, new SqlParameter[] {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@orgId",orgId),
            });
        }

        /// <summary>
        /// Lấy nhà thuốc thuộc tổ chức/Kho thuốc (Không bao gồm tổ chức con)
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentEntity> GetPharmacyListByOrg(string OrganizeId)
        {
            var sql = @"select * from xt_yfbm where OrganizeId=@OrganizeId and zt='1'";
            return this.FindList<SysPharmacyDepartmentEntity>(sql, new SqlParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId)
            });
        }
    }
}
