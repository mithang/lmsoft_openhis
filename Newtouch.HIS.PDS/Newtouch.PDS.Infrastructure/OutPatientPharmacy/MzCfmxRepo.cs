using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 门诊处方明细
    /// </summary>
    public class MzCfmxRepo : RepositoryBase<MzCfmxEntity>, IMzCfmxRepo
    {
        public MzCfmxRepo() { }
        public MzCfmxRepo(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) : base(databaseFactory) { }
        
        /// <summary>
        /// 根据处方号获取处方明细
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        public List<MzCfmxEntity> GetCfmxByCfh(string cfh)
        {
            return FindList<MzCfmxEntity>(@"
SELECT cfmx.[Id],cfmx.[ypCode],cfmx.[ypmc],cfmx.[gg],cfmx.[sl],cfmx.[dw],cfmx.[dj],cfmx.[ycmc],cfmx.[je],cfmx.[jl],cfmx.[jldw]
,cfmx.[yfmc],cfmx.[bz],cfmx.[czh],cfmx.[OrganizeId],cfmx.[CreatorCode],cfmx.[CreateTime],cfmx.[LastModiFierCode]
,cfmx.[LastModifyTime],cfmx.[cfh], cfmx.zhyz
FROM [dbo].[mz_cfmx](NOLOCK) cfmx
INNER JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfh=cfmx.cfh AND cf.OrganizeId=cfmx.OrganizeId
WHERE cf.cfh=@cfh
AND cf.lyyf=@YfbmCode
AND cfmx.OrganizeId=@Organizeid
AND cf.zt = @zt
            ", new SugarParameter[] {
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@YfbmCode", Constants.CurrentYfbm.yfbmCode),
                new SugarParameter("@Organizeid", OperatorProvider.GetCurrent().OrganizeId),
                new SugarParameter("@zt", "1")
            });
        }

        /// <summary>
        /// 判断是否已存在
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gg">规格</param>
        /// <returns></returns>
        public bool IsExist(string cfh, string ypCode, string organizeId, string gg = "")
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT 1 FROM dbo.mz_cfmx(NOLOCK)  ");
            sql.AppendLine("WHERE cfh = @cfh ");
            sql.AppendLine("AND ypCode = @ypCode ");
            sql.AppendLine("AND OrganizeId = @OrganizeId ");
            var param = new List<SugarParameter>
            {
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@ypCode", ypCode),
                new SugarParameter("@OrganizeId", organizeId)
            };
            if (!string.IsNullOrWhiteSpace(gg))
            {
                sql.AppendLine("AND gg=@gg ");
                param.Add(new SugarParameter("@gg", gg));
            }
            return FindList<int>(sql.ToString(), param.ToArray()).Any();
        }

        /// <summary>
        /// 删除老的处方明细
        /// </summary>
        /// <param name="chf">处方号</param>
        /// <returns></returns>
        public int DeleteCfmx(string cfh)
        {
            var param = new List<SugarParameter>
            {
                new SugarParameter("@cfh",cfh),
                new SugarParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
            };
            return (string.IsNullOrWhiteSpace(cfh))
                ? 0
                : ExecuteSqlCommand(@"DELETE from dbo.mz_cfmx WHERE cfh=@cfh AND OrganizeId=@OrganizeId ;",
                    param.ToArray());
        }

        /// <summary>
        /// 修改发药标致 已排=>未发
        /// </summary>
        /// <param name="cfhs"></param>
        /// <returns></returns>
        public int DeleteCfmx(List<string> cfhs)
        {
            if (cfhs == null || cfhs.Count <= 0) return 0;
            var strCfh = new StringBuilder();
            cfhs.ForEach(p => { strCfh.Append(p + ","); });
            string sql = " DELETE FROM dbo.mz_cf WHERE cfh IN (@cfhs) AND OrganizeId=@OrganizeId ;";
            SugarParameter[] param =
            {
                new SugarParameter("@cfhs", strCfh.ToString().Trim(',')),
                new SugarParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 获取处方明细信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<MzCfmxEntity> SelectData(string cfh, string organizeId)
        {
            const string sql = @"SELECT * FROM dbo.mz_cfmx(NOLOCK) WHERE cfh=@cfh AND OrganizeId=@OrganizeId AND zt='1'";
            var param = new SugarParameter[]
            {
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@organizeId", organizeId)
            };
            return _dataContext.Database.SqlQuery<MzCfmxEntity>(sql, param).ToList();
        }


        public int Updatebz(long cfmxId)
        {
            const string sql1 = @"UPDATE dbo.mz_cfmx SET bz=ypmc WHERE Id=@cfmxId";
            return _dataContext.Database.ExecuteSqlCommand(sql1, new SugarParameter("@cfmxId", cfmxId));
        }
        public int Updatebz(long cfmxId, Newtouch.Infrastructure.EFDbTransaction db)
        {
            const string sql1 = @"UPDATE dbo.mz_cfmx SET bz=ypmc WHERE Id=@cfmxId";
            return db.ExecuteSqlCommandNoLog(sql1, new SugarParameter("@cfmxId", cfmxId));
        }
    }
}
