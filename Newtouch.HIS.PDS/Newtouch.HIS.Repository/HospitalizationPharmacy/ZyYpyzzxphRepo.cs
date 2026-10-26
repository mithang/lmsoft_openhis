using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Số lô thực hiện y lệnh thuốc nội trú
    /// </summary>
    public class ZyYpyzzxphRepo : RepositoryBase<ZyYpyzzxphEntity>, IZyYpyzzxphRepo
    {
        public ZyYpyzzxphRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy thông tin xếp thuốc nội trú hợp lệ chưa trả về kệ, phương thức này không ghi nhật ký
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="zxId">Thực hiện y lệnhID</param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<ZyYpyzzxphEntity> SelectZyphList(string yzId, string zxId, string ypCode, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_ypyzzxph(NOLOCK) 
WHERE ypCode=@ypCode AND zxId=@zxId AND yzId=@yzId AND OrganizeId=@OrganizeId AND zt='1' AND gjzt='0'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@yzId", yzId),
                new SqlParameter("@zxId", zxId),
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return _dataContext.Database.SqlQuery<ZyYpyzzxphEntity>(sql, param).ToList();
        }

        /// <summary>
        /// Lấy thông tin xếp thuốc nội trú hợp lệ chưa trả về kệ, phương thức này không ghi nhật ký
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="zxId">Thực hiện y lệnhID</param>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="organizeId"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        public List<ZyYpyzzxphEntity> SelectZyphList(string yzId, string zxId, string ypCode, string pc, string ph, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_ypyzzxph(NOLOCK) 
WHERE ypCode=@ypCode AND zxId=@zxId AND yzId=@yzId AND OrganizeId=@OrganizeId AND zt='1' AND gjzt='0' AND pc=@pc AND ph=@ph
";
            var param = new DbParameter[]
            {
                new SqlParameter("@yzId", yzId),
                new SqlParameter("@zxId", zxId),
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph", ph),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return _dataContext.Database.SqlQuery<ZyYpyzzxphEntity>(sql, param).ToList();
        }

    }
}
