using Newtouch.Infrastructure.EF;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class SysMedicineBaseDmnService : DmnServiceBase, ISysMedicineBaseDmnService
    {

        public SysMedicineBaseDmnService(IDefaultDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }


        /// <summary>
        /// 根据关键字查询药品大分类
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysMedicineClassificationVO> GetValidList(string keyword = null)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_ypfl(nolock) where zt = '1' and (ypflCode like @searchKeyword or ypflmc like @searchKeyword or py like @searchKeyword) order by CreateTime desc";
            return this.FindList<SysMedicineClassificationVO>(sql, new SugarParameter[] {
                            new SugarParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                        });
        }

        public SysMedicineAntibioticInfoVO GetKssInfo(string Id, string OrganizeId)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_ypKss(nolock) where Id=@Id and organizeId=@organizeId  ";
            return this.FirstOrDefault<SysMedicineAntibioticInfoVO>(sql, new SugarParameter[] {
                            new SugarParameter("@Id", Id),
                            new SugarParameter("@OrganizeId", OrganizeId)
                        });
        }

        public IList<SysItemsDetailVO> GetValidListByItemCode(string code, string keyword, string orgId = null)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.Sys_ItemsDetail
        where zt = '1'
        and (isnull(@orgId,'') = '' or OrganizeId = '*' or OrganizeId = @orgId)
        and ItemId in (
        select Id from [NewtouchHIS_Base].dbo.Sys_Items where zt = '1' and Code = @code
        )
        and (Name like @keyword or Code like @keyword)
        ";
            return this.FindList<SysItemsDetailVO>(sql, new SugarParameter[] {
                        new SugarParameter("@code", code),
                        new SugarParameter("@orgId", orgId ?? ""),
                        new SugarParameter("@keyword",'%'+ (keyword==null?"":keyword) +'%')
                    });
        }

        /// <summary>
        /// 获得所有列表
        /// </summary>
        public List<SysMedicalOrderFrequencyVO> GetOrderFrequencyList(string orgId, string keyword = null)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_yzpc(nolock) where organizeId=@organizeId ";

            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " yzpcmc like @keyword or yzpcCode like @keyword";
            }
            else
            {
            }
            return this.FindList<SysMedicalOrderFrequencyVO>(sql, new SugarParameter[] {
                            new SugarParameter("@keyword", "%" + (keyword ?? "") + "%"),
                        new SugarParameter("@organizeId", orgId ?? "")
                        });
        }

        /// <summary>
        /// 收费大类
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysChargeCategoryVO> GetsfdlValidList(string orgId)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_sfdl(nolock) where zt=1 and organizeId=@organizeId order by px ";
            return this.FindList<SysChargeCategoryVO>(sql, new SugarParameter[] {
                            new SugarParameter("@OrganizeId", orgId)
                        });
        }

        /// <summary>
        /// 药品剂型
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineFormulationVO> GetypjxValidList(string keyword = null)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_ypjx(nolock) where zt = '1' and (jxCode like @searchKeyword or jxmc like @searchKeyword or py like @searchKeyword) order by py asc";
            return this.FindList<SysMedicineFormulationVO>(sql, new SugarParameter[] {
                        new SugarParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    });
        }


        /// <summary>
        /// 查询药品单位
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineUnitVO> GetypdwValidList(string keyword = null)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_ypdw where zt = '1' and ypdwmc like @searchKeyword order by ypdwmc asc";
            return this.FindList<SysMedicineUnitVO>(sql, new SugarParameter[] {
                        new SugarParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    });
        }


        public int Insertyfbmyp(SysPharmacyDepartmentOpenMedicineVO entity)
        {

            if (!string.IsNullOrEmpty(entity.Id))
            {
                try
                {
                    using (var db = new EFDbTransaction().BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"insert into [NewtouchHIS_Base].dbo.xt_yfbm_yp
                               (Id,yfbmCode ,dlCode,OrganizeId,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt,px)
                                  values (newid(),@yfbmCode ,@dlCode,@OrganizeId,@CreateTime,@CreatorCode,@LastModifyTime,@LastModifierCode,@zt,@px);");
                        var paraList = new SugarParameter[]
                        {
                        new SugarParameter("@OrganizeId", entity.Id),
                        new SugarParameter("@typeName", entity.yfbmCode),
                        new SugarParameter("@typelevel", entity.dlCode),
                        new SugarParameter("@qxjb", entity.OrganizeId),
                        new SugarParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@CreateTime", DateTime.Now),
                        new SugarParameter("@LastModifyTime", null),
                        new SugarParameter("@LastModifierCode", null),
                        new SugarParameter("@zt", entity.zt),
                        new SugarParameter("@px", entity.px),
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
            return 1;

        }
    }
}
