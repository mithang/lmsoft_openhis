using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Ngoại trú BHYT nông thôn mới quyết toán
    /// </summary>
    public class OutpatientXnhSettlementResultRepo : RepositoryBase<OutpatientXnhSettlementResultEntity>, IOutpatientXnhSettlementResultRepo
    {
        public OutpatientXnhSettlementResultRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// theo outpid lấy dữ liệu
        /// </summary>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<OutpatientXnhSettlementResultEntity> SelectData(string outpId, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.mz_xnh_settResult(NOLOCK) 
WHERE outpId=@outpId AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@outpId", outpId),
                new SqlParameter("@OrganizeId",organizeId ),
            };
            return FindList<OutpatientXnhSettlementResultEntity>(sql, param);
        }

        /// <summary>
        /// Sửa quyết toán dấu hiệu 
        /// </summary>
        /// <param name="jszt">Trạng thái quyết toán 1-Đ ã quyết toán 2-Đ ã hoàn mặc đ ịnhĐ ã quyết toán</param>
        /// <param name="outpId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int UpdateJszt(int jszt, string outpId, string userCode, string organizeId)
        {
            const string sql = @"
UPDATE dbo.mz_xnh_settResult SET jszt=@jszt, LastModifyTime=GETDATE(), LastModifierCode=@userCode 
WHERE outpId=@outpId AND zt='1' AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@jszt", jszt),
                new SqlParameter("@userCode", userCode),
                new SqlParameter("@outpId", outpId),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return ExecuteSqlCommand(sql, param);
        }
    }
}


