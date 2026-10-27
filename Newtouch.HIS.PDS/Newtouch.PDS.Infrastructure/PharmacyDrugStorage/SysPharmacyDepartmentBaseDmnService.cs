using Newtouch.Infrastructure.EF;
using DbConfigHelper;
using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Common;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class SysPharmacyDepartmentBaseDmnService : DmnServiceBase, ISysPharmacyDepartmentBaseDmnService
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="databaseFactory"></param>
        
        public SysPharmacyDepartmentVO GetFormJson(string orgId, int? yfbmId)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_yfbm where OrganizeId=@orgId";
            var parList = new List<SugarParameter>() { };
            parList.Add(new SugarParameter("@orgId", orgId ?? ""));
            if (yfbmId != 0)
            {
                strSql += " and yfbmId = @yfbmId";
                parList.Add(new SugarParameter("@yfbmId", yfbmId));
            }
            return this.FirstOrDefault<SysPharmacyDepartmentVO>(strSql, parList.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVO> GetList(string orgId, byte? yjbmjb)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_yfbm where OrganizeId=@orgId";
            var parList = new List<SugarParameter>() { };
            parList.Add(new SugarParameter("@orgId", orgId ?? ""));
            if (yjbmjb.HasValue)
            {
                strSql += " and yjbmjb = @yjbmjb";
                parList.Add(new SugarParameter("@yjbmjb", yjbmjb.Value));
            }
            return this.FindList<SysPharmacyDepartmentVO>(strSql, parList.ToArray());
        }

        /// <summary>
        /// Lấy nhà thuốc và kho thuốc hiệu lực
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVO> GetEffectiveList(string orgId, byte? yjbmjb)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_yfbm where OrganizeId=@orgId and zt='1' ";
            var parList = new List<SugarParameter>
            {
                new SugarParameter("@orgId", orgId ?? "")
            };
            if (!yjbmjb.HasValue) return FindList<SysPharmacyDepartmentVO>(strSql, parList.ToArray());
            strSql += " and yjbmjb = @yjbmjb";
            parList.Add(new SugarParameter("@yjbmjb", yjbmjb.Value));
            return FindList<SysPharmacyDepartmentVO>(strSql, parList.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVO> GetPagintionList(Pagination pagination, string organizeId, string keyword = null)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_yfbm
            where OrganizeId=@organizeId  and (isnull(@keyword, '') = '' or yfbmCode like @searchkeyword or yfbmmc like @searchkeyword or py like @searchkeyword)";
            SugarParameter[] param = new SugarParameter[] {
                            new SugarParameter("@keyword",keyword ?? ""),
                            new SugarParameter("@organizeId",organizeId),
                            new SugarParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
                        };
            return this.QueryWithPage<SysPharmacyDepartmentVO>(strSql, pagination, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysPharmacyDepartmentVO entity, int? keyValue)
        {

            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var checkSql = "select * from [NewtouchHIS_Base].dbo.xt_yfbm where zt=1 and OrganizeId=@organizeId  and yfbmId = @yfbmId and yfbmCode=@yfbmCode";
                var parList = new List<SugarParameter>
                {
                    new SugarParameter("@organizeId", entity.OrganizeId ?? ""),
                    new SugarParameter("@yfbmId", keyValue ),
                    new SugarParameter("@yfbmCode", entity.yfbmCode ?? ""),
                };
                var checkList = FindList<SysPharmacyDepartmentVO>(checkSql, parList.ToArray());

                if (checkList.Count > 0)
                {
                    throw new FailedException("Mã bộ phận không thể trùng lặp！");
                }
                try
                {
                    using (var db = new EFDbTransaction().BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"  UPDATE " + DbNames.Name("BaseDb", "NewtouchHIS_Base") + @".dbo.xt_yfbm
SET yfbmCode=@yfbmCode,yfbmmc=@yfbmmc,OrganizeId=@OrganizeId,ksCode=@ksCode,ynwbz=@ynwbz,yjbmjb=@yjbmjb,fybz=@fybz,mzzybz=@mzzybz,py=@py,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode,zt=@zt
where yfbmId=@yfbmId");
                        var paraList = new SugarParameter[]
                        {
                        new SugarParameter("@yfbmId", keyValue),
                        new SugarParameter("@yfbmCode", entity.yfbmCode),
                        new SugarParameter("@yfbmmc", entity.yfbmmc),
                        new SugarParameter("@OrganizeId", entity.OrganizeId),
                        new SugarParameter("@ksCode", entity.ksCode),
                        new SugarParameter("@ynwbz", entity.ynwbz),
                        new SugarParameter("@yjbmjb", entity.yjbmjb),
                        new SugarParameter("@fybz", entity.fybz),
                        new SugarParameter("@mzzybz", entity.mzzybz),
                        new SugarParameter("@py", entity.py),
                        new SugarParameter("@LastModifyTime", DateTime.Now),
                        new SugarParameter("@LastModifierCode", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@zt", entity.zt)
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
                var checkSql = "select * from [NewtouchHIS_Base].dbo.xt_yfbm where zt=1 and OrganizeId=@organizeId  and yfbmCode=@yfbmCode ";
                var parList = new List<SugarParameter>
                {
                    new SugarParameter("@organizeId", entity.OrganizeId ?? ""),
                    new SugarParameter("@yfbmCode", entity.yfbmCode ?? ""),
                };
                var checkList = FindList<SysPharmacyDepartmentVO>(checkSql, parList.ToArray());
                if (checkList.Count > 0)
                {
                    throw new FailedException("Mã bộ phận không thể trùng lặp！");
                }
                try
                {
                    using (var db = new EFDbTransaction().BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"insert into " + DbNames.Name("BaseDb", "NewtouchHIS_Base") + @".dbo.xt_yfbm
                                  (yfbmCode,yfbmmc,OrganizeId,ksCode,ynwbz,yjbmjb,fybz,mzzybz,jsfs,py,CreatorCode,CreateTime,LastModifyTime,LastModifierCode,zt,px)
                                  values (@yfbmCode,@yfbmmc,@OrganizeId,@ksCode,@ynwbz,@yjbmjb,@fybz,@mzzybz,null,@py,@CreatorCode,@CreateTime,null,null,@zt,null);");
                        var paraList = new SugarParameter[]
                        {
                        //new SugarParameter("@yfbmId", entity.yfbmId),
                        new SugarParameter("@yfbmCode", entity.yfbmCode),
                        new SugarParameter("@yfbmmc", entity.yfbmmc),
                        new SugarParameter("@OrganizeId", entity.OrganizeId),
                        new SugarParameter("@ksCode", entity.ksCode),
                        new SugarParameter("@ynwbz", entity.ynwbz),
                        new SugarParameter("@yjbmjb", entity.yjbmjb),
                        new SugarParameter("@fybz", entity.fybz),
                        new SugarParameter("@mzzybz", entity.mzzybz),
                        //new SugarParameter("@jsfs", entity.jsfs),
                        new SugarParameter("@py", entity.py),
                        new SugarParameter("@CreatorCode",  OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@CreateTime", DateTime.Now),
                        //new SugarParameter("@LastModifyTime", null),
                        //new SugarParameter("@LastModifierCode", null),
                        new SugarParameter("@zt", entity.zt),
                        //new SugarParameter("@px", entity.px)
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                        db.Commit();
                    }
                }
                catch (Exception ex) {
                    throw new FailedException(ex.Message);
                }
            }
        }

        /// <summary>
        /// LấyUserIdđã liên kết hiện tạiyfbmCode （Không gồm tổ chức con）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<string> GetCurPharmacyList(string userId, string orgId)
        {
            var strsql = @"select yfbmCode from [NewtouchHIS_Base].dbo.Sys_UserYfbm where UserId = @UserId and OrganizeId = @orgId";
            return this.FindList<string>(strsql, new SugarParameter[] {
                new SugarParameter("@UserId",userId),
                new SugarParameter("@orgId",orgId),
            });
        }

        /// <summary>
        /// Lấy nhà thuốc thuộc tổ chức/Kho thuốc （Không gồm tổ chức con）
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVO> GetPharmacyListByOrg(string OrganizeId)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_yfbm where OrganizeId=@OrganizeId and zt='1'";
            return this.FindList<SysPharmacyDepartmentVO>(sql, new SugarParameter[] {
                new SugarParameter("@OrganizeId",OrganizeId)
            });
        }

    }
}