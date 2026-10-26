using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
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
    public class SysDepartmentRepo : RepositoryBase<SysDepartmentEntity>, ISysDepartmentRepository
    {
        public SysDepartmentRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lấy danh sách khoa
        /// </summary>
        /// <returns></returns>
        public List<SysDepartmentEntity> GetListByTopOrg(string topOrganizeId)
        {
            return this.IQueryable().Where(p => p.TopOrganizeId == topOrganizeId).ToList();
        }

        /// <summary>
        /// Lấy danh sách khoa
        /// </summary>
        /// <returns></returns>
        public List<SysDepartmentEntity> GetListByOrg(string organizeId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == organizeId).OrderByDescending(p => p.CreateTime).ToList();
        }

        /// <summary>
        /// Lấy tất cả các khoa có hiệu lực thuộc tổ chức hiện tại
        /// </summary>
        /// <returns></returns>
        public IList<SysDepartmentEntity> GetValidListByOrg(string organizeId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == organizeId && p.zt == "1").ToList();
        }

        /// <summary>
        /// Lấy khoa có hiệu lực của tổ chức
        /// </summary>
        /// <returns></returns>
        public IList<SysDepartmentEntity> GetValidListByTopOrg(string topOrganizeId)
        {
            return this.IQueryable().Where(p => p.TopOrganizeId == topOrganizeId && p.zt == "1").ToList();
        }

        /// <summary>
        /// Lấy tên theo Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetNameByCode(string code, string orgId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            var sql = "select Name from Sys_Department with(nolock) where Code = @code and OrganizeId = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@code", code)});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysDepartmentEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysDepartmentEntity SysDepartmentEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.OrganizeId == SysDepartmentEntity.OrganizeId && p.Code == SysDepartmentEntity.Code && p.Id != keyValue))
                {
                    throw new FailedException("编号不可重复");
                }

                /*Xác định ngừng sử dụng khoa*/
                var zt = SysDepartmentEntity.zt;
                if (zt == "0")
                {
                    var orgId = SysDepartmentEntity.OrganizeId;
                    var sql = "select count(1) from Sys_DepartmentWardRelation(nolock)ksbqxxb where DepartmentId = @DepartmentId and OrganizeId = @orgId and zt = '1'";
                    var kssyqk = this.FirstOrDefault<int>(sql, new[] { new SqlParameter("@orgId", orgId)
                    ,new SqlParameter("@DepartmentId", keyValue)});

                    if (kssyqk != 0)
                    {
                        throw new FailedException("该科室有病区正在使用，无法停止！");
                    }
                }

                SysDepartmentEntity oldEntity = null;   //Entity trước khi thay đổi
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                SysDepartmentEntity.Modify(keyValue);
                this.Update(SysDepartmentEntity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, SysDepartmentEntity, SysDepartmentEntity.GetTableName(), oldEntity.Id);
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == SysDepartmentEntity.OrganizeId && p.Code == SysDepartmentEntity.Code))
                {
                    throw new FailedException("编号不可重复");
                }
                SysDepartmentEntity.Create(true);
                this.Insert(SysDepartmentEntity);
            }
        }
        /// <summary>
        /// Đã hủy bỏ, việc sửa đổi trạng thái đã chuyển sang chương trình bảo hiểm y tế
        /// </summary>
        /// <param name="uploadYB"></param>
        /// <param name="id"></param>
        public void UpdateYbUpload(int uploadYB, string id)
        {
            var sql = "select Code from Sys_Department with(nolock) where Id = @Id";
            string code = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@Id", id) });
            if (string.IsNullOrEmpty(code))
            {
                throw new FailedException("科室不存在");
            }
            var updateSql = "update Sys_Department set UploadYB = @uploadYB where Id = @Id";
            this.ExecuteSqlCommand(updateSql, new[] { new SqlParameter("@Id", id), new SqlParameter("@uploadYB", uploadYB) });
        }


        /// <summary>
        /// Lấy danh sách khoa có hiệu lực theo điều kiện truy vấn
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysDepartmentEntity> GetDeptListByKeyValue(string orgId, string keyValue)
        {
            var sql = "select * from Sys_Department with(nolock) where zt=1 and OrganizeId = @orgId";
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                sql += " and (@keyValue='%%' or Name like @keyValue or Code like @keyValue)";
            }
            return this.FindList<SysDepartmentEntity>(sql, new[] { new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@keyValue", "%"+keyValue.Trim()+"%")});
        }

    }
}


