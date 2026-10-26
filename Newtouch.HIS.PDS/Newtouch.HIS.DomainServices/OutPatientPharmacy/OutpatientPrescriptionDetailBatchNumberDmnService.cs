using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Số lô đơn thuốc ngoại trú  mz_cfmxph
    /// </summary>
    public class OutpatientPrescriptionDetailBatchNumberDmnService : DmnServiceBase, IOutpatientPrescriptionDetailBatchNumberDmnService
    {

        public OutpatientPrescriptionDetailBatchNumberDmnService(IDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }

        /// <summary>
        /// Sửa cờ trả về kệ
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        public int UpdateGjztbyCfh(string cfh, string organizeId)
        {
            var param = new DbParameter[]
            {
                new SqlParameter("@cfh", cfh),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return ExecuteSqlCommand("UPDATE dbo.mz_cfmxph SET gjzt='1' WHERE cfh=@cfh AND OrganizeId=@OrganizeId", param.ToArray());
        }

        /// <summary>
        /// Lấy thông tin số lô chi tiết đơn thuốc ngoại trú chưa trả về kệ theo số đơn thuốc và tổ chức
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="ypCode"></param>
        /// <param name="gjzt">Trạng thái trả về kệ：0-Chưa trả về kệ  1-Đã trả về kệ</param>
        /// <returns></returns>
        public List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string organizeId, string ypCode = "", string gjzt = "0")
        {
            var sql = new StringBuilder(@"
SELECT * FROM dbo.mz_cfmxph(NOLOCK) 
WHERE cfh=@cfh
AND gjzt=@gjzt
AND OrganizeId=@OrganizeId
AND zt='1' 
");
            if (!string.IsNullOrWhiteSpace(ypCode)) sql.Append("AND yp=@ypCode ");
            var param = new DbParameter[]
            {
                new SqlParameter("@cfh", cfh),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@gjzt", gjzt),
                new SqlParameter("@ypCode", ypCode),
            };
            return FindListNoLog<OutpatientPrescriptionDetailBatchNumberEntity>(sql.ToString(), param);
        }

    }
}
