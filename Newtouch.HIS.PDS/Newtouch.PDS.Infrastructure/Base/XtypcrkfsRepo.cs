using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 出入库方式
    /// </summary>
    public class XtypcrkfsRepo : RepositoryBase<XtypcrkfsEntity>, IXtypcrkfsRepo
    {
        
        /// <summary>
        /// get crkfs list 
        /// </summary>
        /// <param name="crkbz"></param>
        /// <returns></returns>
        public List<XtypcrkfsEntity> GetCrkfsList(string crkbz)
        {
            const string sql = @"
SELECT [crkfsId]
      ,[crkfsCode]
      ,[crkfsmc]
      ,[crkbz]
      ,[CreatorCode]
      ,[CreateTime]
      ,[LastModifyTime]
      ,[LastModifierCode]
      ,[zt]
      ,[px]
  FROM [NewtouchHIS_Base].[dbo].[xt_ypcrkfs]
  WHERE crkbz=@crkbz
  AND zt='1'
";
            return FindList<XtypcrkfsEntity>(sql, new SugarParameter[] { new SugarParameter("@crkbz", crkbz) });
        }

    }
}