using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Core.Common.Interface;
using System.Linq;

namespace Newtouch.HIS.Repository
{
	/// <summary>
    /// 
    /// </summary>
    public class SysFailedCodeMessageMappRepo : RepositoryBase<SysFailedCodeMessageMappEntity>, ISysFailedCodeMessageMappRepo
    {
        private readonly ICache _cache;
        public SysFailedCodeMessageMappRepo(IBaseDatabaseFactory databaseFactory, ICache cache)
            : base(databaseFactory)
        {
            this._cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysFailedCodeMessageMappEntity> GetList(string orgId = null)
        {
            //Mapp khớp với tổ chức cấp cao nhất
            var topOrgMappList = _cache.Get<IList<SysFailedCodeMessageMappEntity>>(string.Format(CacheKey.SysFailedCodeMessageMapListListSetKey, Constants.TopOrganizeId), () =>
            {
                return this.IQueryable(p => (p.TopOrganizeId == null || p.TopOrganizeId == "*" || p.TopOrganizeId == Constants.TopOrganizeId))
                    .ToList();
            });

            if (!string.IsNullOrWhiteSpace(orgId))
            {
                //Mapp khớp với Organize
                var orgMappList = topOrgMappList.Where(p => p.OrganizeId == null || p.OrganizeId == "*" || p.OrganizeId == orgId).ToList();

                return orgMappList.GroupBy(p => p.code)
                    .Select(p => new SysFailedCodeMessageMappEntity()
                    {
                        code = p.Key,
                        msg = p.OrderByDescending(t => t.OrganizeId == orgId)
                                      .ThenByDescending(t => t.TopOrganizeId == Constants.TopOrganizeId)
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
                     msg = p.OrderByDescending(t => t.TopOrganizeId == Constants.TopOrganizeId)
                                   .ThenByDescending(t => t.OrganizeId == null || t.OrganizeId == "*")
                                   .ThenBy(t => t.px)
                                   .Select(t => t.msg).FirstOrDefault()
                 }).ToList();
            }

        }
    }
}


