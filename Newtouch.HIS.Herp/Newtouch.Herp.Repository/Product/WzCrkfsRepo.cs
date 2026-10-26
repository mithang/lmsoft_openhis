using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Hình thức nhập/xuất kho
    /// </summary>
    public class WzCrkfsRepo : RepositoryBase<WzCrkfsEntity>, IWzCrkfsRepo
    {
        public WzCrkfsRepo(IBaseDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Theo ID Xóa hình thức nhập/xuất kho
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCrkfsById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var entity = FindEntity(p => p.Id == id);
            return entity == null ? 0 : Delete(entity);
        }

        /// <summary>
        /// Tra cứu hình thức nhập/xuất kho
        /// </summary>
        /// <param name="crkbz"></param>
        /// <returns></returns>
        public List<WzCrkfsEntity> SelectData(string crkbz)
        {
            const string sql = @"
SELECT * FROM NewtouchHIS_Base.dbo.wz_crkfs(NOLOCK) 
WHERE zt='1' AND crkbz=@crkbz
";
            var param = new DbParameter[]
            {
                new SqlParameter("@crkbz", crkbz)
            };
            return FindList<WzCrkfsEntity>(sql, param);
        }
    }
}
