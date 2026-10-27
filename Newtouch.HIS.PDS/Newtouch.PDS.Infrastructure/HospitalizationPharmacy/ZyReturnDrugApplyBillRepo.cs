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
    /// Đơn xin trả thuốc nội trú
    /// </summary>
    public class ZyReturnDrugApplyBillRepo : RepositoryBase<ZyReturnDrugApplyBillEntity>, IZyReturnDrugApplyBillRepo
    {
        public ZyReturnDrugApplyBillRepo() { }
        public ZyReturnDrugApplyBillRepo(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) : base(databaseFactory) { }
        
        /// <summary>
        /// Sửa trạng thái xử lý
        /// </summary>
        /// <param name="applyNo"></param>
        /// <param name="organizeId"></param>
        /// <returns> >0:thành công  Còn lại:Thất bại </returns>
        public ZyReturnDrugApplyBillEntity SelectData(string applyNo, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_returnDrugApplyBill
WHERE applyNo=@applyNo AND OrganizeId=@OrganizeId AND zt='1' AND ProcessState='0'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@applyNo", applyNo),
                new SugarParameter("@OrganizeId", organizeId)
            };
            return _dataContext.Database.SqlQuery<ZyReturnDrugApplyBillEntity>(sql, param).FirstOrDefault();
        }

        /// <summary>
        /// Sửa trạng thái xử lý
        /// </summary>
        /// <param name="applyNo"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="db"></param>
        /// <returns> >0:thành công  Còn lại:Thất bại </returns>
        public int UpdateProcessStateDoingWithTrans(string applyNo, string userCode, string organizeId, Newtouch.Infrastructure.EFDbTransaction db)
        {
            const string sql = @"
UPDATE dbo.zy_returnDrugApplyBill 
SET ProcessState='1', LastModifierCode=@userCode, LastModifyTime=GETDATE() 
WHERE applyNo=@applyNo AND OrganizeId=@OrganizeId AND zt='1' AND ProcessState='0'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@applyNo", applyNo),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@userCode",userCode )
            };
            return db.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// Sửa trạng thái xử lý
        /// </summary>
        /// <param name="applyNo"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns> >0:thành công  Còn lại:Thất bại </returns>
        public int UpdateProcessStateCompleteWithTrans(string applyNo, string userCode, string organizeId, Newtouch.Infrastructure.EFDbTransaction db)
        {
            const string sql = @"
UPDATE dbo.zy_returnDrugApplyBill 
SET ProcessState='2', LastModifierCode=@userCode, LastModifyTime=GETDATE() 
WHERE applyNo=@applyNo AND OrganizeId=@OrganizeId AND zt='1' AND ProcessState='1'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@applyNo", applyNo),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@userCode",userCode )
            };
            return db.ExecuteSqlCommand(sql, param);
        }
    }
}