using Newtouch.Infrastructure.EF;
using DbConfigHelper;
using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class SysPharmacyWindowDmnService : DmnServiceBase, ISysPharmacyWindowDmnService
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="databaseFactory"></param>
        
        public SysPharmacyWindowVO GetFormJson(string orgId, int? yfckId)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_yfck where OrganizeId=@orgId";
            var parList = new List<SugarParameter>() { };
            parList.Add(new SugarParameter("@orgId", orgId ?? ""));
            if (yfckId != 0)
            {
                strSql += " and yfckId = @yfckId";
                parList.Add(new SugarParameter("@yfckId", yfckId));
            }
            return this.FirstOrDefault<SysPharmacyWindowVO>(strSql, parList.ToArray());
        }


        public void submitForm(SysPharmacyWindowVO entity, int? keyValue)
        {

            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var checkSql = "select * from [NewtouchHIS_Base].dbo.xt_yfck where zt=1 and OrganizeId=@organizeId  and yfckId = @yfckId and yfckCode=@yfckCode";
                var parList = new List<SugarParameter>
                {
                    new SugarParameter("@organizeId", entity.OrganizeId ?? ""),
                    new SugarParameter("@yfckId", keyValue ),
                    new SugarParameter("@yfckCode", entity.yfckCode ?? ""),
                };
                var checkList = FindList<SysPharmacyWindowVO>(checkSql, parList.ToArray());

                if (checkList.Count > 0)
                {
                    throw new FailedException("Mã cửa sổ không thể trùng lặp！");
                }
                try
                {
                    using (var db = new EFDbTransaction().BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"  UPDATE " + DbNames.Name("BaseDb", "NewtouchHIS_Base") + @".dbo.xt_yfck
SET yfckCode=@yfckCode,yfckmc=@yfckmc,OrganizeId=@OrganizeId,pfyms=@pfyms,kqbz=@kqbz,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode,zt=@zt,px=@px
where yfckId=@yfckId");
                        var paraList = new SugarParameter[]
                        {
                        new SugarParameter("@yfckId", keyValue),
                        new SugarParameter("@yfckCode", entity.yfckCode),
                        new SugarParameter("@yfckmc", entity.yfckmc),
                        new SugarParameter("@OrganizeId", entity.OrganizeId),
                        new SugarParameter("@pfyms", entity.pfyms==null?"":entity.pfyms),
                        new SugarParameter("@kqbz", entity.kqbz==null?"":entity.kqbz),
                        new SugarParameter("@LastModifyTime", DateTime.Now),
                        new SugarParameter("@LastModifierCode", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@zt", entity.zt),
                        new SugarParameter("@px", entity.px==null?0:entity.px),
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                        db.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException(ex.Message);
                }
            }
            else
            {
                var checkSql = "select * from [NewtouchHIS_Base].dbo.xt_yfck where zt=1 and OrganizeId=@organizeId  and yfckCode=@yfckCode ";
                var parList = new List<SugarParameter>
                {
                    new SugarParameter("@organizeId", entity.OrganizeId ?? ""),
                    new SugarParameter("@yfckCode", entity.yfckCode ?? ""),
                };
                var checkList = FindList<SysPharmacyWindowVO>(checkSql, parList.ToArray());
                if (checkList.Count > 0)
                {
                    throw new FailedException("Mã cửa sổ không thể trùng lặp！");
                }
                try
                {
                    using (var db = new EFDbTransaction().BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"insert into " + DbNames.Name("BaseDb", "NewtouchHIS_Base") + @".dbo.xt_yfck
                                  (yfckCode,yfckmc,OrganizeId,yfbmCode,pfyms,kqbz,CreatorCode,CreateTime,LastModifyTime,LastModifierCode,zt,px)
                                  values (@yfckCode,@yfckmc,@OrganizeId,null,@pfyms,@kqbz,@CreatorCode,@CreateTime,null,null,@zt,@px);");
                        var paraList = new SugarParameter[]
                        {
                            
                        new SugarParameter("@yfckCode", entity.yfckCode),
                        new SugarParameter("@yfckmc", entity.yfckmc),
                        new SugarParameter("@OrganizeId", entity.OrganizeId),
                        new SugarParameter("@pfyms", entity.pfyms==null?"":entity.pfyms),
                        new SugarParameter("@kqbz", entity.kqbz==null?"":entity.kqbz),
                        new SugarParameter("@CreateTime", DateTime.Now),
                        new SugarParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@zt", entity.zt),
                        new SugarParameter("@px", entity.px==null?0:entity.px),
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                        db.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException(ex.Message);
                }
            }
            
        }

        public IList<PharmacyWindowVO> GetPagintionList(Pagination pagination, string organizeId, string keyword = null)
        {
            var strSql = @"select yfck.yfckId,yfck.yfckCode,yfck.yfckmc,yfck.zt,yfck.px,yfck.CreateTime,yfck.CreatorCode
        from [NewtouchHIS_Base].dbo.xt_yfck yfck
    where yfck.OrganizeId=@OrganizeId
and (isnull(@keyword, '') = '' or yfck.yfckCode like @searchkeyword or yfck.yfckmc like @searchkeyword )";
            SugarParameter[] param = new SugarParameter[] {
                            new SugarParameter("@keyword",keyword ?? ""),
                            new SugarParameter("@OrganizeId",organizeId),
                            new SugarParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
                        };
            return this.QueryWithPage<PharmacyWindowVO>(strSql, pagination, param);
        }

    }
}
