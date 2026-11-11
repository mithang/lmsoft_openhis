using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.VO;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Text;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_fypz
    /// </summary>
    public class SysYpksfypzEntityRepo : RepositoryBase<SysYpksfypzEntity>, ISysYpksfypzEntityRepo
    {
        public SysYpksfypzEntityRepo(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }
        /// <summary>
        /// Lấy tên khoa
        /// </summary>
        /// <returns></returns>
        public List<CodeNameVO> GetCodeName(string organizeId, int yjbmjb, string keyword = null)
        {
            var sqlStr = new StringBuilder(@"
SELECT Distinct [Code] as code
      ,[Name] as name
  FROM [NewtouchHIS_Base].[dbo].[Sys_Department]
  where zt = '1'
  and OrganizeId=@organizeId
            ");
            var pars = new List<SugarParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                 sqlStr.AppendLine( @" and ( name like @keyword or py like @keyword)");
                pars.Add(new SugarParameter("@keyword", "%"+keyword+"%"));
            }
            
            pars.Add(new SugarParameter("@organizeId", organizeId));
            return FindList<CodeNameVO>(sqlStr.ToString(), pars.ToArray());
        }
        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysYpksfypzEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId)
        {
            var sql = new StringBuilder(@"
SELECT a.[Id]
      ,a.[OrganizeId]
      ,b.yfbmmc as yfCode
      ,c.Name as ksCode
      ,a.[CreatorCode]
      ,a.[CreateTime]
      ,a.[zt]
  FROM [NewtouchHIS_PDS].[dbo].[xt_yp_ksfypz] a 
  join [NewtouchHIS_Base].[dbo].[xt_yfbm] b on a.yfCode=b.yfbmCode and b.zt='1' and b.OrganizeId=@organizeId
  left join [NewtouchHIS_Base].[dbo].[Sys_Department](nolock) c on c.Code=a.ksCode and c.OrganizeId=@OrganizeId
  where a.OrganizeId = @organizeId
            ");
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@organizeId", organizeId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and(1 = 2");
                sql.Append(" or b.yfbmmc like @keyword");
                sql.Append(")");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return QueryWithPage<SysYpksfypzEntity>(sql.ToString(), pagination, pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FypzDTO"></param>
        /// <param name="keyValue"></param>
        // G35-F2: zt chuan hoa ("true"/"1" -> "1"); G35-F5: edit merge-skip-null + loc org khi xoa, tra so dong.
        private static string MapZt(string zt) => (zt == "true" || zt == "1") ? "1" : "0";

        public int SubmitForm(SysYpksfypzEntity FypzDTO, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var db = FindEntity(p => p.Id == keyValue);
                if (db == null) return 0;
                if (FypzDTO.ksCode != null) db.ksCode = FypzDTO.ksCode;
                if (FypzDTO.yfCode != null) db.yfCode = FypzDTO.yfCode;
                if (FypzDTO.zt != null) db.zt = MapZt(FypzDTO.zt);
                // OrganizeId KHONG copy tu DTO — controller giu nguyen org goc (G35 5h)
                db.Modify();
                return Update(db);
            }
            else
            {
                var entity = new SysYpksfypzEntity
                {
                    ksCode = FypzDTO.ksCode,
                    yfCode = FypzDTO.yfCode,
                    OrganizeId = FypzDTO.OrganizeId,
                    zt = MapZt(FypzDTO.zt)
                };
                entity.Create(true);
                entity.zt = MapZt(FypzDTO.zt); // Create(true) ghi de zt="1" — gan lai gia tri that (G35 5b)
                return Insert(entity);
            }
        }
        public int DeleteForm(string keyValue, string organizeId)
        {
            return Delete(p => p.Id == keyValue && p.OrganizeId == organizeId);
        }
    }
}
