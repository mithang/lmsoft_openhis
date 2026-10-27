using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Text;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Phương thức xuất nhập kho thuốc
    /// </summary>
    public class SysMedicineCrkfsDmnService : DmnServiceBase, ISysMedicineCrkfsDmnService
    {
        
        /// <summary>
        /// Lấy phương thức xuất nhập kho
        /// </summary>
        /// <param name="crkbz">Cờ xuất nhập kho 0：Nhập kho  1：Xuất kho</param>
        /// <returns></returns>
        public List<SysMedicineCrkfsVEntity> GetList(string crkbz)
        {
            var sql = new StringBuilder("SELECT crkfsId, crkfsCode, crkfsmc, crkbz, zt ");
            sql.AppendLine("FROM NewtouchHIS_Base.dbo.V_S_xt_ypcrkfs ");
            sql.AppendLine("WHERE zt='1' ");
            if (!string.IsNullOrWhiteSpace(crkbz))
            {
                sql.AppendLine("AND crkbz=@crkbz ");
            }
            var param = new SugarParameter[] { new SugarParameter("crkbz", crkbz) };
            return FindList<SysMedicineCrkfsVEntity>(sql.ToString(), param);
        }
    }
}