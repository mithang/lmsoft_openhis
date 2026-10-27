
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.Repository
{
    /// <summary>
    /// đặt hẹnđăng ký khám
    /// </summary>
    public class SysBespeakRegisterRepo : RepositoryBase<SysBespeakRegisterEntity>, ISysBespeakRegisterRepo
    {
        public SysBespeakRegisterRepo() : base()
        {
        }

        /// <summary>
        /// Lấykhiđoạn
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="gh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<RegTimeVO> GetRegTime(string deptCode, DateTime regDate, string gh, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT Id, regTime 
FROM dbo.xt_bespeakRegister(NOLOCK)
WHERE departmentCode=@deptCode
AND regDate=@regDate
AND ysgh=@gh
AND OrganizeId=@OrganizeId
ORDER BY regTime ";
            var param = new SqlParameter[] {
                new SqlParameter("@deptCode", deptCode),
                new SqlParameter("@regDate", regDate),
                new SqlParameter("@gh", gh??""),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<RegTimeVO>(sql, param);
        }
    }
}