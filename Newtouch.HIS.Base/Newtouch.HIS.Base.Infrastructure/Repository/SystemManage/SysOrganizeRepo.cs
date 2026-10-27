using System.Collections.Generic;
using Newtouch.HIS.Repository;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Data.SqlClient;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysOrganizeRepo : RepositoryBase<SysOrganizeEntity>, ISysOrganizeRepository
    {
        public SysOrganizeRepo()
            
        {

        }

        /// <summary>
        /// Lấy tất cả tổ chức thuộc tổ chức hiện tại
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeEntity> GetListByTopOrg(string topOrganizeId)
        {
            return this.IQueryable().Where(p => p.TopOrganizeId == topOrganizeId).ToList();
        }

        /// <summary>
        /// Lấy tất cả tổ chức thuộc tổ chức hiện tại
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeEntity> GetValidListByTopOrg(string topOrganizeId)
        {
            return this.IQueryable().Where(p => p.TopOrganizeId == topOrganizeId && p.zt == "1").ToList();
        }

        /// <summary>
        /// Lấy tất cả tổ chức hiệu lực thuộc tổ chức（parent）
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeVO> GetValidListByParentOrg(string parentOrgId)
        {
            var sql = @" 
    WITH cteTree
        AS (SELECT *
              FROM Sys_Organize
              WHERE Id = @parentOrgId  and zt = '1'  --Truy vấn đầu tiên làm điểm cơ sở đệ quy(Điểm neo)
            UNION ALL
            SELECT Sys_Organize.*     --Truy vấn thứ hai làm thành viên đệ quy， Khi kết quả thành viên cấp dưới rỗng，Đệ quy này kết thúc。
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
        /// Lấy tất cả tổ chức thuộc tổ chức（parent）（Bao gồm cả vô hiệu）
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeVO> GetListByParentOrg(string parentOrgId)
        {
            var sql = @" 
    WITH cteTree
        AS (SELECT *
              FROM Sys_Organize
              WHERE Id = @parentOrgId  --Truy vấn đầu tiên làm điểm cơ sở đệ quy(Điểm neo)
            UNION ALL
            SELECT Sys_Organize.*     --Truy vấn thứ hai làm thành viên đệ quy， Khi kết quả thành viên cấp dưới rỗng，Đệ quy này kết thúc。
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
        /// Lấy danh sách tổ chức cấp cao nhất Bao gồm cả vô hiệu
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
        /// Gửi tạo mới、Cập nhật Thực thể
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysOrganizeEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (string.IsNullOrWhiteSpace(entity.TopOrganizeId))
                {
                    throw new FailedException("Dữ liệu bất thường, chưa chỉ định cơ sở cấp cao nhất");
                }
                var isCodeRepeated = string.IsNullOrWhiteSpace(entity.ParentId)
                    ? this.IQueryable().Any(p => p.ParentId == null
                        && p.Code == entity.Code && p.Id != keyValue)   //Trong tổ chức cấp cao nhất không được trùngcode
                    : this.IQueryable().Any(p => p.TopOrganizeId == entity.TopOrganizeId
                        && p.Code == entity.Code && p.Id != keyValue)   //Thuộc một tổ chức cấp cao nhất nào đó tất cả của Không được trùng
                    ;
                if (isCodeRepeated)
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysOrganizeEntity oldEntity = null;   //Trước khi thay đổiEntity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "Sys_Organize", oldEntity.Id);
                }
            }
            else
            {
                entity.Create(true);
                if (string.IsNullOrWhiteSpace(entity.ParentId) && string.IsNullOrWhiteSpace(entity.TopOrganizeId))
                {
                    entity.ParentId = null;
                    entity.TopOrganizeId = entity.Id;   //Đây là tổ chức cấp cao nhất
                }
                var isCodeRepeated = string.IsNullOrWhiteSpace(entity.ParentId)
                    ? this.IQueryable().Any(p => p.ParentId == null
                        && p.Code == entity.Code)   //Trong tổ chức cấp cao nhất không được trùngcode
                    : this.IQueryable().Any(p => p.TopOrganizeId == entity.TopOrganizeId
                        && p.Code == entity.Code)   //Thuộc một tổ chức cấp cao nhất nào đó tất cả của Không được trùng
                    ;
                if (isCodeRepeated)
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                this.Insert(entity);
            }
        }

        /// <summary>
        /// TheoOrgIdLấyCode
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


