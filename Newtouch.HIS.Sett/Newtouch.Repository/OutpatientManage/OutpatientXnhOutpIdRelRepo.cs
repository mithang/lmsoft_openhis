using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// BHYT nông thôn mới số thứ tự bồi thư ờng ngoại trú liên kết bảng 
    /// </summary>
    public class OutpatientXnhOutpIdRelRepo : RepositoryBase<OutpatientXnhOutpIdRelEntity>, IOutpatientXnhOutpIdRelRepo
    {
        public OutpatientXnhOutpIdRelRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// SửaTrạng tháiKhông hiệu lực
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateZtDisable(string mzh, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.mz_xnh_outpIdRel SET zt='0', LastModifyTime=GETDATE(), LastModifierCode=@usercode 
WHERE mzh=@mzh AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode", userCode),
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// SửaTrạng tháiKhông hiệu lực
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateZtDisable(string mzh, string outpId, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.mz_xnh_outpIdRel SET zt='0', LastModifyTime=GETDATE(), LastModifierCode=@usercode 
WHERE mzh=@mzh AND OrganizeId=@OrganizeId AND outpId=@outpId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@outpId", outpId),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode", userCode),
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// Sửa xử lý trạng thái
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="processingStatus">Xử lý trạng thái 0-chưa xử lý 1-chi ti ếtĐ ãtải lên 2-Đ ã quyết toán tính 3-Đ ã ghi đ ỏ hoàn trả 4-trả lại ngoại trú </param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateProcessingStatus(string mzh, int processingStatus, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.mz_xnh_outpIdRel SET processingStatus=@processingStatus, LastModifyTime=GETDATE(), LastModifierCode=@usercode 
WHERE mzh=@mzh AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@processingStatus", processingStatus),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode", userCode),
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// Sửa xử lý trạng thái
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="processingStatus">Xử lý trạng thái 0-chưa xử lý 1-chi ti ếtĐ ãtải lên 2-Đ ã quyết toán tính 3-Đ ã ghi đ ỏ hoàn trả 4-trả lại ngoại trú </param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateProcessingStatus(string mzh, int processingStatus, string outpId, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.mz_xnh_outpIdRel SET processingStatus=@processingStatus, LastModifyTime=GETDATE(), LastModifierCode=@usercode 
WHERE mzh=@mzh AND OrganizeId=@OrganizeId AND outpId=@outpId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@processingStatus", processingStatus),
                new SqlParameter("@outpId", outpId),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode", userCode),
            };
            return ExecuteSqlCommand(sql, param);
        }
    }
}


