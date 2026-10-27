using Newtouch.Infrastructure.EF;
using DbConfigHelper;
using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Framework.Operator;
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
    public class SysMedicineAntibioticTypeDmnService : DmnServiceBase, ISysMedicineAntibioticTypeDmnService
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="databaseFactory"></param>
        

        public SysMedicineAntibioticTypeVO GetById(string orgId, string Id)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_kssType where OrganizeId=@orgId";
            var parList = new List<SugarParameter>() { };
            parList.Add(new SugarParameter("@orgId", orgId ?? ""));
            if (!string.IsNullOrEmpty(Id))
            {
                strSql += " and Id = @Id";
                parList.Add(new SugarParameter("@Id", Id));
            }
            return this.FirstOrDefault<SysMedicineAntibioticTypeVO>(strSql, parList.ToArray());
        }


        /// <summary>
        /// Theo tổ chức,Cấp,Lấy danh sách thông tin kháng sinh
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysMedicineAntibioticTypeVO> GetValidListByOrg(string OrganizeId, string Level, string parentId)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;
            }
            var sql = @"select Id ,OrganizeId ,typeName ,typelevel ,qxjb ,qxjbmc ,
                            parentId ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode ,zt 
                        from [NewtouchHIS_Base].dbo.xt_kssType where zt='1' and OrganizeId=@OrganizeId
                        and typelevel=@Level ";
            if (string.IsNullOrWhiteSpace(parentId))
            {
                sql += " and (parentId IS NULL or parentId='' )";
            }
            else
            {
                sql += " and parentId = @parentId ";
            }
            sql += " order by typeName asc";
            return this.FindList<SysMedicineAntibioticTypeVO>(sql, new SugarParameter[] {
                    new SugarParameter("@OrganizeId", OrganizeId),
                new SugarParameter("@Level", Level),
                new SugarParameter("@parentId", string.IsNullOrWhiteSpace(parentId) ? string.Empty : parentId) });
        }


        public IList<SysMedicineAntibioticTypeVO> GetListByParentId(string OrganizeId, string parentId)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;
            }
            var sql = @"select Id ,OrganizeId ,typeName ,typelevel ,qxjb ,qxjbmc ,
                            parentId ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode ,zt 
                        from [NewtouchHIS_Base].dbo.xt_kssType where zt='1' and OrganizeId=@OrganizeId ";
            if (string.IsNullOrWhiteSpace(parentId))
            {
                sql += " and parentId IS NULL ";
            }
            else
            {
                sql += " and parentId = @parentId ";
            }
            sql += " order by typeName asc";
            return this.FindList<SysMedicineAntibioticTypeVO>(sql, new SugarParameter[] {
                    new SugarParameter("@OrganizeId", OrganizeId),
                new SugarParameter("@parentId", string.IsNullOrWhiteSpace(parentId) ? string.Empty : parentId) });
        }

        /// <summary>
        /// Gửi thông tin loại kháng sinh
        /// </summary>
        /// <param name="entity"></param>
        public void SubmitForm(SysMedicineAntibioticTypeVO entity)
        {

            if (!string.IsNullOrEmpty(entity.Id))
            {
                try
                {
                    using (var db = new EFDbTransaction().BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"  UPDATE " + DbNames.Name("BaseDb", "NewtouchHIS_Base") + @".dbo.xt_kssType
SET OrganizeId=@OrganizeId,typeName=@typeName,typelevel=@typelevel,qxjb=@qxjb,qxjbmc=@qxjbmc,parentId=@parentId,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode,zt=@zt
where Id=@Id");
                        var paraList = new SugarParameter[]
                        {
                        new SugarParameter("@Id", entity.Id),
                        new SugarParameter("@OrganizeId", entity.OrganizeId),
                        new SugarParameter("@typeName", entity.typeName),
                        new SugarParameter("@typelevel", entity.typelevel),
                        new SugarParameter("@qxjb", entity.qxjb==null?"":entity.qxjb),
                        new SugarParameter("@qxjbmc", entity.qxjbmc==null?"":entity.qxjbmc),
                        new SugarParameter("@parentId", entity.parentId==null?"":entity.parentId),
                        new SugarParameter("@LastModifyTime", DateTime.Now),
                        new SugarParameter("@LastModifierCode", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@zt", entity.zt),
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
                try
                {
                    using (var db = new EFDbTransaction().BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"insert into " + DbNames.Name("BaseDb", "NewtouchHIS_Base") + @".dbo.xt_kssType
                                  (Id,OrganizeId ,typeName,typelevel,qxjb,qxjbmc,parentId,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt)
                                  values (newid(),@OrganizeId,@typeName,@typelevel,@qxjb,@qxjbmc,@parentId,@CreateTime,@CreatorCode,null,null,@zt);");
                        var paraList = new SugarParameter[]
                        {
                        new SugarParameter("@OrganizeId", entity.OrganizeId),
                        new SugarParameter("@typeName", entity.typeName),
                        new SugarParameter("@typelevel", entity.typelevel),
                        new SugarParameter("@qxjb", entity.qxjb==null?"":entity.qxjb),
                        new SugarParameter("@qxjbmc", entity.qxjbmc==null?"":entity.qxjbmc),
                        new SugarParameter("@parentId", entity.parentId==null?"":entity.parentId),
                        new SugarParameter("@CreateTime", DateTime.Now),
                        new SugarParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@zt", entity.zt),
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
    }
}
