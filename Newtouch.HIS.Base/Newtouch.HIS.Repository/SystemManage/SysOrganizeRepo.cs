using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Tools;
using System.Data.SqlClient;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysOrganizeRepo : RepositoryBase<SysOrganizeEntity>, ISysOrganizeRepository
    {
        public SysOrganizeRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lấy tất cả các tổ chức thuộc tổ chức hiện tại
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeEntity> GetListByTopOrg(string topOrganizeId)
        {
            return this.IQueryable().Where(p => p.TopOrganizeId == topOrganizeId).ToList();
        }

        /// <summary>
        /// Lấy tất cả các tổ chức thuộc tổ chức hiện tại
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeEntity> GetValidListByTopOrg(string topOrganizeId)
        {
            return this.IQueryable().Where(p => p.TopOrganizeId == topOrganizeId && p.zt == "1").ToList();
        }

        /// <summary>
        /// Lấy tất cả các tổ chức có hiệu lực thuộc tổ chức (parent)
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeVO> GetValidListByParentOrg(string parentOrgId)
        {
            var sql = @" 
    WITH cteTree
        AS (SELECT *
              FROM Sys_Organize
              WHERE Id = @parentOrgId  and zt = '1'  --第一个查询作为递归的基点(锚点)
            UNION ALL
            SELECT Sys_Organize.*     --第二个查询作为递归成员， 下属成员的结果为空时，此递归结束。
              FROM
                   cteTree INNER JOIN Sys_Organize ON cteTree.Id= Sys_Organize.ParentId and Sys_Organize.zt = '1') 
        SELECT  *
          FROM cteTree 

";
            return this.FindList<SysOrganizeVO>(sql, new SqlParameter[] {
                new SqlParameter("@parentOrgId",parentOrgId)
            });
        }

        /// <summary>
        /// Lấy tất cả các tổ chức thuộc tổ chức (parent) (bao gồm cả các mục không hiệu lực)
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeVO> GetListByParentOrg(string parentOrgId)
        {
            var sql = @" 
    WITH cteTree
        AS (SELECT *
              FROM Sys_Organize
              WHERE Id = @parentOrgId  --第一个查询作为递归的基点(锚点)
            UNION ALL
            SELECT Sys_Organize.*     --第二个查询作为递归成员， 下属成员的结果为空时，此递归结束。
              FROM
                   cteTree INNER JOIN Sys_Organize ON cteTree.Id= Sys_Organize.ParentId) 
        SELECT  *
          FROM cteTree 

";
            return this.FindList<SysOrganizeVO>(sql, new SqlParameter[] {
                new SqlParameter("@parentOrgId",parentOrgId)
            });
        }

        /// <summary>
        /// Lấy danh sách tổ chức cấp cao nhất
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysOrganizeEntity> GetPagintionListByTopOrg(Pagination pagination, string keyword = null)
        {
            var sql = @"select * from Sys_Organize
where ParentId is null and
(Name = @keyword or Name like @searchkeyword or Code = @keyword or Code like @searchkeyword)";
            return this.QueryWithPage<SysOrganizeEntity>(sql, pagination, new SqlParameter[] {
                new SqlParameter("@keyword", keyword ?? ""),
                new SqlParameter("@searchkeyword", "%" + (keyword??"") + "%")
            });
        }

        /// <summary>
        /// Lấy danh sách tổ chức cấp cao nhất, bao gồm cả các mục không hiệu lực
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeEntity> GetTopOrgList()
        {
            var sql = @"select * from Sys_Organize where ParentId is null";
            return this.FindList<SysOrganizeEntity>(sql);
        }

        /// <summary>
        /// Lấy danh sách tổ chức cấp cao nhất
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeEntity> GetValidTopOrgList()
        {
            var sql = @"select * from Sys_Organize where zt = '1' and ParentId is null";
            return this.FindList<SysOrganizeEntity>(sql);
        }

        /// <summary>
        /// Gửi tạo mới, cập nhật thực thể
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysOrganizeEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (string.IsNullOrWhiteSpace(entity.TopOrganizeId))
                {
                    throw new FailedException("数据异常，未指定顶级组织机构");
                }
                var isCodeRepeated = string.IsNullOrWhiteSpace(entity.ParentId)
                    ? this.IQueryable().Any(p => p.ParentId == null
                        && p.Code == entity.Code && p.Id != keyValue)   //Trong cơ sở cấp cao nhất không được có code trùng lặp
                    : this.IQueryable().Any(p => p.TopOrganizeId == entity.TopOrganizeId
                        && p.Code == entity.Code && p.Id != keyValue)   //Tất cả thuộc một cơ sở cấp cao nhất không được trùng lặp
                    ;
                if (isCodeRepeated)
                {
                    throw new FailedException("编码不可重复");
                }

                SysOrganizeEntity oldEntity = null;   //Entity trước khi thay đổi
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysOrganizeEntity.GetTableName(), oldEntity.Id);
                }
            }
            else
            {
                entity.Create(true);
                if (string.IsNullOrWhiteSpace(entity.ParentId) && string.IsNullOrWhiteSpace(entity.TopOrganizeId))
                {
                    entity.ParentId = null;
                    entity.TopOrganizeId = entity.Id;   //Đây là cơ sở cấp cao nhất
                }
                var isCodeRepeated = string.IsNullOrWhiteSpace(entity.ParentId)
                    ? this.IQueryable().Any(p => p.ParentId == null
                        && p.Code == entity.Code)   //Trong cơ sở cấp cao nhất không được có code trùng lặp
                    : this.IQueryable().Any(p => p.TopOrganizeId == entity.TopOrganizeId
                        && p.Code == entity.Code)   //Tất cả thuộc một cơ sở cấp cao nhất không được trùng lặp
                    ;
                if (isCodeRepeated)
                {
                    throw new FailedException("编码不可重复");
                }

                this.Insert(entity);
            }
        }

        /// <summary>
        /// Lấy Code theo OrgId
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetCodeById(string orgId)
        {
            var sql = "select Code from Sys_Organize where Id = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

    }
}


