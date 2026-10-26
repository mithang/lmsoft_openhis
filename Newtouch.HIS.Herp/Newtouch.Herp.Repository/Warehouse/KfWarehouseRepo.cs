using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Thao tác kho
    /// </summary>
    public class KfWarehouseRepo : RepositoryBase<KfWarehouseEntity>, IKfWarehouseRepo
    {
        public KfWarehouseRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy kho anh em hoặc kho con đích
        /// </summary>
        /// <param name="kfId"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<KfWarehouseEntity> GetBrothersOrChildren(string kfId, string organizeId, string keyWord = "")
        {
            var result = IQueryable(p => p.parentId == kfId && p.zt == ((int)Enumzt.Enable).ToString() && p.OrganizeId == organizeId).ToList();
            var s = FindEntity(p => p.Id == kfId && p.zt == ((int)Enumzt.Enable).ToString() && p.OrganizeId == organizeId);
            if (s == null) return result;
            result.AddRange(IQueryable(p => p.parentId == s.parentId && p.Id != s.Id &&
                                            p.zt == ((int)Enumzt.Enable).ToString() && p.OrganizeId == organizeId));
            return (from d in result
                    where d.name.Contains(keyWord.Trim()) || d.py.Contains(keyWord.Trim().ToUpper())
                    select d).ToList();
        }

        /// <summary>
        /// Lấy khoa phòng(Nút cha và nút anh em)
        /// </summary>
        /// <param name="kfId"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<KfWarehouseEntity> GetParentOrBrothers(string kfId, string organizeId, string keyWord = "")
        {
            var s = FindEntity(p => p.Id == kfId && p.zt == ((int)Enumzt.Enable).ToString() && p.OrganizeId == organizeId);
            var result = IQueryable(p => p.Id == s.parentId && p.zt == ((int)Enumzt.Enable).ToString() && p.OrganizeId == organizeId).ToList();
            if (s == null) return result;
            result.AddRange(IQueryable(p => p.parentId == s.parentId && p.Id != s.Id &&
                                            p.zt == ((int)Enumzt.Enable).ToString() && p.OrganizeId == organizeId));
            return (from d in result
                    where d.name.Contains(keyWord.Trim()) || d.py.Contains(keyWord.Trim().ToUpper())
                    select d).ToList();
        }
    }
}
