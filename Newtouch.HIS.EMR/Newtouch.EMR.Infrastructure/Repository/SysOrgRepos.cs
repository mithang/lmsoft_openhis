using Newtouch.EMR.Domain.IRepository;
using NewtouchHIS.Lib.DataBaseSvr;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Base-DB system repositories (see SysOrgInterfaces.cs for table locations).
    /// These do NOT extend the EmrDb-bound RepositoryBase; they query BaseDb
    /// directly. All methods are read-only.
    /// </summary>
    public class SysDepartmentRepo : ISysDepartmentRepo
    {
        private static SqlSugar.SqlSugarProvider Db()
        {
            return SqlSugarDbContext.Db.GetConnection("BaseDb");
        }

        public IList<SysDepartmentEntity> GetList(string organizeId, string zt, string keyword)
        {
            var q = Db().Queryable<SysDepartmentEntity>()
                .Where(p => p.OrganizeId == organizeId);
            if (!string.IsNullOrWhiteSpace(zt))
            {
                q = q.Where(p => p.zt == zt);
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                q = q.Where(p => p.Code.Contains(keyword) || p.Name.Contains(keyword));
            }
            return q.OrderBy(p => p.Code).ToList();
        }

        public SysDepartmentEntity GetEntityByCode(string code, string organizeId)
        {
            return Db().Queryable<SysDepartmentEntity>()
                .Where(p => p.Code == code && p.OrganizeId == organizeId)
                .First();
        }
    }

    public class SysStaffRepo : ISysStaffRepo
    {
        private static SqlSugar.SqlSugarProvider Db()
        {
            return SqlSugarDbContext.Db.GetConnection("BaseDb");
        }

        public IList<SysStaffEntity> GetValidStaffListByOrganizeId(string organizeId)
        {
            return Db().Queryable<SysStaffEntity>()
                .Where(p => p.OrganizeId == organizeId && p.zt == "1")
                .OrderBy(p => p.gh).ToList();
        }

        public SysStaffEntity GetValidStaffByGh(string gh, string organizeId)
        {
            return Db().Queryable<SysStaffEntity>()
                .Where(p => p.gh == gh && p.OrganizeId == organizeId && p.zt == "1")
                .First();
        }
    }

    public class SysDiagnosisRepo : ISysDiagnosisRepo
    {
        private static SqlSugar.SqlSugarProvider Db()
        {
            return SqlSugarDbContext.Db.GetConnection("BaseDb");
        }

        public IList<SysDiagnosisDto> GetList(string organizeId, string keyword, string zdlx, string ybnhlx)
        {
            var q = Db().Queryable<SysDiagnosisEntity>()
                .Where(p => p.OrganizeId == organizeId && p.zt == "1");
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                q = q.Where(p => p.zdCode.Contains(keyword) || p.zdmc.Contains(keyword) || p.py.Contains(keyword));
            }
            if (!string.IsNullOrWhiteSpace(zdlx))
            {
                q = q.Where(p => p.zdlx == zdlx);
            }
            if (!string.IsNullOrWhiteSpace(ybnhlx))
            {
                q = q.Where(p => p.ybnhlx == ybnhlx);
            }
            return q.OrderBy(p => p.zdCode)
                .Select(p => new SysDiagnosisDto
                {
                    id = p.zdCode,
                    text = p.zdmc,
                    zdCode = p.zdCode,
                    zdmc = p.zdmc,
                    icd10 = p.icd10,
                    py = p.py,
                    zdlx = p.zdlx,
                    ybnhlx = p.ybnhlx
                }).ToList();
        }
    }
}
