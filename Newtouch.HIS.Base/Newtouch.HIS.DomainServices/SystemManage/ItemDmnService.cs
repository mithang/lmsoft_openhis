using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Từ điển, mục từ điển DmnService
    /// </summary>
    public class ItemDmnService : DmnServiceBase, IItemDmnService
    {
        private readonly ISysItemsDetailRepository _sysItemsDetailRepository;

        public ItemDmnService(IBaseDatabaseFactory databaseFactory
            , ISysItemsDetailRepository sysItemsDetailRepository) : base(databaseFactory)
        {
            this._sysItemsDetailRepository = sysItemsDetailRepository;
        }

        /// <summary>
        /// Lấy danh sách mục từ điển có hiệu lực
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId">Cơ sở y tế, null chỉ tra cứu các mục dùng chung</param>
        /// <returns></returns>
        public IList<SysItemsDetailEntity> GetValidListByItemCode(string code,string  keyword, string orgId = null)
        {
            var sql = @"select * from Sys_ItemsDetail
where zt = '1'
and (isnull(@orgId,'') = '' or OrganizeId = '*' or OrganizeId = @orgId)
and ItemId in (
select Id from Sys_Items where zt = '1' and Code = @code
)
and (Name like @keyword or Code like @keyword)
";
            return this.FindList<SysItemsDetailEntity>(sql, new SqlParameter[] {
                new SqlParameter("@code", code),
                new SqlParameter("@orgId", orgId ?? ""),
                new SqlParameter("@keyword",'%'+ (keyword==null?"":keyword) +'%')
            });
        }

    }
}
