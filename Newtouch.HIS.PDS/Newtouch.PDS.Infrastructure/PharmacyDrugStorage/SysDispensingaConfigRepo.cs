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
    public class SysDispensingaConfigRepo : RepositoryBase<SysDispensingaConfigEntity>, ISysDispensingaConfigRepo
    {
        public SysDispensingaConfigRepo(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }
        /// <summary>
        /// Lấy tên nhà thuốc kho thuốc
        /// </summary>
        /// <returns></returns>
        public List<CodeNameVO> GetCodeName(string organizeId,int yjbmjb)
        {
            var sqlStr = new StringBuilder(@"
SELECT Distinct [yfbmCode] as code
      ,[yfbmmc] as name
  FROM [NewtouchHIS_Base].[dbo].[xt_yfbm] 
  where zt = '1'
  and OrganizeId=@organizeId
  and yjbmjb=@yjbmjb
            ");
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@organizeId", organizeId));
            pars.Add(new SugarParameter("@yjbmjb", yjbmjb));
            return FindList<CodeNameVO>(sqlStr.ToString(), pars.ToArray());
        }
        /// <summary>
        /// Lấy tên nhà thuốc kho thuốc
        /// </summary>
        /// <returns></returns>
        public List<CodeNameVO> GetYFYKCodeName(string organizeId)
        {
            var sqlStr = new StringBuilder(@"
SELECT Distinct [yfbmCode] as code
      ,[yfbmmc] as name
  FROM [NewtouchHIS_Base].[dbo].[xt_yfbm] 
  where zt = '1'
  and OrganizeId=@organizeId
            ");
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@organizeId", organizeId));
            return FindList<CodeNameVO>(sqlStr.ToString(), pars.ToArray());
        }
        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysDispensingaConfigEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId)
        {
            var sql = new StringBuilder(@"
SELECT a.[Id]
      ,a.[OrganizeId]
      ,b.yfbmmc as ykCode
      ,c.yfbmmc as yfCode
      ,a.[CreatorCode]
      ,a.[CreateTime]
      ,a.[zt]
      ,a.[LastModifierCode]
      ,a.[LastModifyTime]
  FROM [NewtouchHIS_PDS].[dbo].[xt_yp_fypz] a 
  join [NewtouchHIS_Base].[dbo].[xt_yfbm] b on a.ykCode=b.yfbmCode and b.zt='1' and b.OrganizeId=@organizeId
  join [NewtouchHIS_Base].[dbo].[xt_yfbm] c on a.yfCode=c.yfbmCode and c.zt='1' and c.OrganizeId=@organizeId
  where a.OrganizeId = @organizeId
            ");
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@organizeId", organizeId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and(1 = 2");
                sql.Append(" or b.yfbmmc like @keyword");
                sql.Append(" or c.yfbmmc like @keyword");
                sql.Append(")");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return QueryWithPage<SysDispensingaConfigEntity>(sql.ToString(), pagination, pars.ToArray());
        }
        // G35-F2: zt chuan hoa ("true"/"1" -> "1"); G35-F5: edit merge-skip-null + loc org khi xoa, tra so dong.
        private static string MapZt(string zt) => (zt == "true" || zt == "1") ? "1" : "0";

        public int SubmitForm(SysDispensingaConfigEntity FypzDTO, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var db = FindEntity(p => p.Id == keyValue);
                if (db == null) return 0;
                if (FypzDTO.ykCode != null) db.ykCode = FypzDTO.ykCode;
                if (FypzDTO.yfCode != null) db.yfCode = FypzDTO.yfCode;
                if (FypzDTO.zt != null) db.zt = MapZt(FypzDTO.zt);
                // OrganizeId KHONG copy tu DTO — controller giu nguyen org goc (G35 3h)
                db.Modify();
                return Update(db);
            }
            else
            {
                var entity = new SysDispensingaConfigEntity
                {
                    ykCode = FypzDTO.ykCode,
                    yfCode = FypzDTO.yfCode,
                    OrganizeId = FypzDTO.OrganizeId,
                    zt = MapZt(FypzDTO.zt)
                };
                entity.Create(true);
                entity.zt = MapZt(FypzDTO.zt); // Create(true) ghi de zt="1" — gan lai gia tri that (G35 3b)
                return Insert(entity);
            }
        }
        public int DeleteForm(string keyValue, string organizeId)
        {
            return Delete(p => p.Id == keyValue && p.OrganizeId == organizeId);
        }
    }
}
