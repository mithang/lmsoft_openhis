using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using Newtouch.Core.Common.Interface;
using System.Linq;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 创 建: tờ 冬林
    /// Ngày kỳ:2017-09-30 14:21
    /// 描 述:Lỗi nhắc nhở cấu hình
    /// </summary>
    public class SysFailedCodeMessageMappRepo : RepositoryBase<SysFailedCodeMessageMappEntity>, ISysFailedCodeMessageMappRepo
    {
        private readonly ICache _cache;

        /// <summary>
        /// Hàm tạo mặc đ ịnh
        /// </summary>
        /// <param name="databaseFactory">EFNhà máy ngữ cảnh</param>
        public SysFailedCodeMessageMappRepo(IDefaultDatabaseFactory databaseFactory, ICache cache)
            : base(databaseFactory)
        {
            this._cache = cache;
        }

        /// <summary>
        /// Lấy分trang thực thể danh sách
        /// </summary>
        /// <param name="pagination">分trang</param>
        /// <param name="keyword">LọcTừ khóa</param>
        public IList<SysFailedCodeMessageMappEntity> GetPagintionList(Pagination pagination, string keyword)
        {
            var sql = new StringBuilder();
            sql.Append("select * from Sys_FailedCodeMessageMapp(nolock) where 1 = 1");
            List<SqlParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or code like @keyword");
                sql.Append(" or msg like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<SysFailedCodeMessageMappEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">Đ ối tư ợng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(SysFailedCodeMessageMappEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        /// <summary>
        /// LấyTổ chứcFailMsgCấu hìnhDanh sách
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysFailedCodeMessageMappEntity> GetListByOrgId(string orgId = null)
        {
            //Tổ chức cấp cao nhất 匹配 đ ến của Mapp
            var topOrgMappList = _cache.Get<IList<SysFailedCodeMessageMappEntity>>(string.Format(Infrastructure.CacheKey.SysFailedCodeMessageMapListListSetKey, Constants.TopOrganizeId), () =>
            {
                return this.IQueryable(p => (p.TopOrganizeId == null || p.TopOrganizeId == "*" || p.TopOrganizeId == Infrastructure.Constants.TopOrganizeId))
                    .ToList();
            });

            if (!string.IsNullOrWhiteSpace(orgId))
            {
                //Organize 匹配 đ ến của Mapp
                var orgMappList = topOrgMappList.Where(p => p.OrganizeId == null || p.OrganizeId == "*" || p.OrganizeId == orgId).ToList();

                return orgMappList.GroupBy(p => p.code)
                    .Select(p => new SysFailedCodeMessageMappEntity()
                    {
                        code = p.Key,
                        msg = p.OrderByDescending(t => t.OrganizeId == orgId)
                                      .ThenByDescending(t => t.TopOrganizeId == Infrastructure.Constants.TopOrganizeId)
                                      .ThenBy(t => t.px)
                                      .Select(t => t.msg).FirstOrDefault()
                    }).ToList();
            }
            else
            {
                return topOrgMappList.GroupBy(p => p.code)
                 .Select(p => new SysFailedCodeMessageMappEntity()
                 {
                     code = p.Key,
                     msg = p.OrderByDescending(t => t.TopOrganizeId == Infrastructure.Constants.TopOrganizeId)
                                   .ThenByDescending(t => t.OrganizeId == null || t.OrganizeId == "*")
                                   .ThenBy(t => t.px)
                                   .Select(t => t.msg).FirstOrDefault()
                 }).ToList();
            }
        }

    }
}