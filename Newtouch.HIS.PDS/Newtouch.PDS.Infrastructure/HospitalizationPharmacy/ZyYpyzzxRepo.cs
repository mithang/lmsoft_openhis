using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Thực hiện y lệnh thuốc nội trú
    /// </summary>
    public class ZyYpyzzxRepo : RepositoryBase<ZyYpyzxxEntity>, IZyYpyzxxRepo
    {
        public ZyYpyzzxRepo() { }
        public ZyYpyzzxRepo(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) : base(databaseFactory) { }
        
        /// <summary>
        /// Lấy khu bệnh cần phát thuốc（Khử trùng lặp）
        /// </summary>
        /// <returns></returns>
        public List<string> GetFyBq()
        {
            return FindList<string>(@"SELECT DISTINCT bqmc FROM dbo.zy_ypyzzx(NOLOCK) WHERE fybz='" + (int)EnumFybz.Yp + "'");
        }

        /// <summary>
        /// Lấy bệnh nhân và khu bệnh cần phát thuốc（Khử trùng lặp）
        /// </summary>
        /// <returns></returns>
        public List<string> GetFyPatients()
        {
            return FindList<string>(@"SELECT DISTINCT patientName, bqmc FROM dbo.zy_ypyzzx(NOLOCK) WHERE fybz='" + (int)EnumFybz.Yp + "'");
        }

        /// <summary>
        /// Lấy thông tin y lệnh no log
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_ypyzxx(NOLOCK) 
WHERE yzId=@yzId AND OrganizeId=@OrganizeId 
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@yzId",yzId),
                new SugarParameter("@OrganizeId",organizeId)
            };
            return _dataContext.Database.SqlQuery<ZyYpyzxxEntity>(sql, param).ToList();
        }

        /// <summary>
        /// Lấy thông tin y lệnh no log
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string zxId, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_ypyzxx(NOLOCK) 
WHERE zxId=@zxId AND OrganizeId=@OrganizeId AND yzId=@yzId
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@yzId",yzId),
                new SugarParameter("@zxId",zxId),
                new SugarParameter("@OrganizeId",organizeId)
            };
            return _dataContext.Database.SqlQuery<ZyYpyzxxEntity>(sql, param).ToList();
        }

        /// <summary>
        /// Lấy thông tin y lệnh no log
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string zxId, string ypCode, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_ypyzxx(NOLOCK) 
WHERE zxId=@zxId AND OrganizeId=@OrganizeId AND yzId=@yzId AND ypCode=@ypCode 
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@yzId",yzId),
                new SugarParameter("@zxId",zxId),
                new SugarParameter("@ypCode",ypCode),
                new SugarParameter("@OrganizeId",organizeId)
            };
            return _dataContext.Database.SqlQuery<ZyYpyzxxEntity>(sql, param).ToList();
        }

        /// <summary>
        /// Sửa cờ phát thuốc thành đã trả thuốc
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="db"></param>
        public int UpdateFybzTy(string yzId, string zxId, string userCode, string organizeId, Newtouch.Infrastructure.EFDbTransaction db)
        {
            const string sql = @"
UPDATE NewtouchHIS_PDS.dbo.zy_ypyzxx
SET fybz = @fybz,
    LastModiFierCode = @userCode,
    LastModifyTime = GETDATE()
WHERE zxId=@zxId
AND OrganizeId=@OrganizeId
AND yzId=@yzId
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@yzId", yzId),
                new SugarParameter("@zxId", zxId),
                new SugarParameter("@fybz", 3),//Đã trả
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@OrganizeId", organizeId)
            };
           return db.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// Theo thực hiệnID，Xóa vật lý tất cả chi tiết của lần thực hiện này
        /// </summary>
        /// <param name="zxId"></param>
        /// <returns></returns>
        public int DeleteByZxId(string zxId)
        {
            return ExecuteSqlCommand("DELETE FROM dbo.zy_ypyzxx WHERE zxId=@zxId", new SugarParameter("@zxId", zxId));
        }
    }
}
