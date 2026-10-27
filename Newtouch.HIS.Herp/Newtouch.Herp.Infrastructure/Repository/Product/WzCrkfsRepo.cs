using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 出入库方式 (Base DB: wz_crkfs; original SelectData queried
    /// NewtouchHIS_Base.dbo.wz_crkfs explicitly).
    /// </summary>
    public class WzCrkfsRepo : RepositoryBase<WzCrkfsEntity>, IWzCrkfsRepo
    {
        public WzCrkfsRepo()
        {
        }

        protected override SqlSugarProvider Db => HerpDb.GetBaseConnection();

        public int DeleteCrkfsById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var entity = FindEntity(p => p.Id == id);
            return entity == null ? 0 : Delete(entity);
        }

        public List<WzCrkfsEntity> SelectData(string crkbz)
        {
            const string sql = @"
SELECT * FROM dbo.wz_crkfs(NOLOCK)
WHERE zt='1' AND crkbz=@crkbz
";
            return FindList<WzCrkfsEntity>(sql, new SugarParameter("@crkbz", crkbz));
        }
    }
}
