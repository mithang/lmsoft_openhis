using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
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
        public ZyYpyzzxphRepo() { }
        public ZyYpyzzxphRepo(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) : base(databaseFactory) { }
        
        /// <summary>
        /// Lấy thông tin xếp thuốc nội trú hiệu lực chưa xếp kệ  Phương thức này không ghi nhật ký
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
            var param = new SugarParameter[]
            {
                new SugarParameter("@yzId", yzId),
                new SugarParameter("@zxId", zxId),
                new SugarParameter("@ypCode", ypCode),
                new SugarParameter("@OrganizeId",organizeId )
            };
            return _dataContext.Database.SqlQuery<ZyYpyzzxphEntity>(sql, param).ToList();
        }

        /// <summary>
        /// Lấy thông tin xếp thuốc nội trú hiệu lực chưa xếp kệ  Phương thức này không ghi nhật ký
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
            var param = new SugarParameter[]
            {
                new SugarParameter("@yzId", yzId),
                new SugarParameter("@zxId", zxId),
                new SugarParameter("@ypCode", ypCode),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph),
                new SugarParameter("@OrganizeId",organizeId )
            };
            return _dataContext.Database.SqlQuery<ZyYpyzzxphEntity>(sql, param).ToList();
        }

    }
}
