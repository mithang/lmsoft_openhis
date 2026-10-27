using CacheKey = Newtouch.Infrastructure.CacheKey;

using Newtouch.Core.Common.Utils;
using Newtouch.Core.Common.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysFailedCodeMessageMappRepo : RepositoryBase<SysFailedCodeMessageMappEntity>, ISysFailedCodeMessageMappRepo
    {
        private readonly ICache _cache;
        public SysFailedCodeMessageMappRepo(IDefaultDatabaseFactory databaseFactory, ICache cache) : base(databaseFactory)
        {
            _cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysFailedCodeMessageMappEntity> GetList(string orgId = null)
        {
            //Tổ chức cấp cao nhất đã khớp Mapp
            var topOrgMappList = _cache.Get<IList<SysFailedCodeMessageMappEntity>>(string.Format(CacheKey.SysFailedCodeMessageMapListListSetKey, Constants.TopOrganizeId), () => FindList<SysFailedCodeMessageMappEntity>(@"SELECT * FROM dbo.Sys_FailedCodeMessageMapp(NOLOCK) WHERE TopOrganizeId IS NULL OR TopOrganizeId = '*' OR TopOrganizeId=@topOrganizeId", new SugarParameter[] { new SugarParameter("@topOrganizeId", Constants.TopOrganizeId) }));

            if (string.IsNullOrWhiteSpace(orgId))
            {
                return topOrgMappList.GroupBy(p => p.code).Select(p => new SysFailedCodeMessageMappEntity
                {
                    code = p.Key,
                    msg = p.OrderByDescending(t => t.TopOrganizeId == Constants.TopOrganizeId)
                            .ThenByDescending(t => t.OrganizeId == null || t.OrganizeId == "*")
                            .ThenBy(t => t.px)
                            .Select(t => t.msg)
                            .FirstOrDefault()
                }).ToList();
            }
            //Organize đã khớp Mapp
            var orgMappList = topOrgMappList.Where(p => p.OrganizeId == null || p.OrganizeId == "*" || p.OrganizeId == orgId).ToList();

            return orgMappList.GroupBy(p => p.code).Select(p => new SysFailedCodeMessageMappEntity()
            {
                code = p.Key,
                msg = p.OrderByDescending(t => t.OrganizeId == orgId)
                        .ThenByDescending(t => t.TopOrganizeId == Constants.TopOrganizeId)
                        .ThenBy(t => t.px)
                        .Select(t => t.msg).FirstOrDefault()
            }).ToList();

        }

    }
}
