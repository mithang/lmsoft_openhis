using System;
using DbConfigHelper;
using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.OR.ManageSystem.DomainServices
{
    public class SysChargeItemDmnService : DmnServiceBase, ISysChargeItemDmnService
    {

        public SysChargeItemDmnService()
        {
        }
        /// <summary>
        /// Trang mẫu thu phíindex list
        /// </summary>
        /// <returns></returns>
        public IList<SysChargeTemplateGridVO> Search(Pagination pagination, string keyword,string ks, string organizeId)
        {
            var sql = @"SELECT  SUM(ISNULL(sfxm.dj, '0.00') * ISNULL(xm.sl, 0)) zje ,
        mb.sfmb ,
        mb.sfmbbh ,
        mb.sfmbmc ,
        mb.mzzybz ,
        mb.zt ,
        mb.ks ,
        mb.CreateTime
FROM    [NewtouchHIS_Sett].dbo.xt_sfmb(NOLOCK) mb
       LEFT JOIN [NewtouchHIS_Sett].dbo.xt_sfmbxm xm ON xm.sfmbbh = mb.sfmbbh AND xm.OrganizeId = mb.OrganizeId
        LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm AND sfxm.OrganizeId = xm.OrganizeId
WHERE mb.OrganizeId = @orgId and mb.zt='1' and mb.ks=@ks
and exists(select 1 from [NewtouchHIS_Base].dbo.V_S_Sys_Department d with(nolock) where mb.ks=d.code  and mb.organizeid =d.organizeid and d.zt='1')";
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", organizeId));
            pars.Add(new SugarParameter("@ks", ks));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (sfmbmc like @searchkeyword or sfmb like @searchkeyword)";
                pars.Add(new SugarParameter("@searchkeyword", "%" + keyword + "%"));
            }
            sql += @" GROUP BY mb.sfmbbh ,
                        mb.sfmb ,
                        mb.sfmbmc ,
                        mb.mzzybz ,
                        mb.zt ,
                        mb.ks ,
                        mb.CreateTime";
            return this.QueryWithPage<SysChargeTemplateGridVO>(sql, pagination, pars.ToArray());
        }

        public void ChargeTemplate_SubmitForm(SysChargeTemplateDto entity, string xmListStr,string user)
        {
            string sql = "";
            if (!string.IsNullOrWhiteSpace(entity.sfmbbh))
            {
                sql = @"select sfmbbh
                from " + DbNames.Name("SettDb", "NewtouchHIS_Sett") + @".dbo.xt_sfmb with(nolock)
                where OrganizeId = @orgId and zt = '1' and sfmb = @sfmb and sfmbbh <> @sfmbbh ";

                var ety = this.FindList<SysChargeTemplateGridVO>(sql, new SugarParameter[]
                {
                    new SugarParameter("@orgId", entity.OrganizeId),
                    new SugarParameter("@sfmb", entity.sfmb),
                    new SugarParameter("@sfmbbh", entity.sfmbbh)
                });
                if (ety != null && ety.Count>0)
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                sql = "update a set  a.sfmb = @sfmb,a.sfmbmc=@sfmbmc,a.ks=@ks,a.LastModifyTime=getdate(),a.LastModifierCode=@user,a.zt=@zt " +
                      "from " + DbNames.Name("SettDb", "NewtouchHIS_Sett") + ".dbo.xt_sfmb a " +
                      "where a.OrganizeId = @orgId and a.zt = '1' and a.sfmbbh = @sfmbbh ";
                this.ExecuteSqlCommand(sql, new SugarParameter[]
                {
                    new SugarParameter("@sfmbbh", entity.sfmbbh),
                    new SugarParameter("@orgId", entity.OrganizeId),
                    new SugarParameter("@sfmb", entity.sfmb),
                    new SugarParameter("@sfmbmc", entity.sfmbmc),
                    new SugarParameter("@user", user),
                    new SugarParameter("@ks", entity.ks),
                    new SugarParameter("@zt", entity.zt ?? "1")
                });

                sql = @"update a
                set a.zt = '0',a.LastModifyTime=getdate(),a.LastModifierCode=@user
                from " + DbNames.Name("SettDb", "NewtouchHIS_Sett") + @".dbo.xt_sfmbxm  a
                    where a.OrganizeId = @orgId and a.sfmbbh = @sfmbbh and a.zt = '1'";
                this.ExecuteSqlCommand(sql, new SugarParameter[]
                {
                    new SugarParameter("@sfmbbh", entity.sfmbbh),
                    new SugarParameter("@orgId", entity.OrganizeId),
                    new SugarParameter("@user", user)
                });

            }
            else
            {
                string sfmbbh = Guid.NewGuid().ToString();
                entity.sfmbbh = sfmbbh;

                sql = @"select sfmbbh
                from " + DbNames.Name("SettDb", "NewtouchHIS_Sett") + @".dbo.xt_sfmb with(nolock)
                where OrganizeId = @orgId and zt = '1' and sfmb = @sfmb ";
                var mbety = this.FindList<SysChargeTemplateGridVO>(sql, new SugarParameter[]
                {
                    new SugarParameter("@orgId", entity.OrganizeId),
                    new SugarParameter("@sfmb", entity.sfmb)
                });
                if (mbety != null && mbety.Count > 0)
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                sql = @"insert into " + DbNames.Name("SettDb", "NewtouchHIS_Sett") + @".dbo.xt_sfmb([sfmbbh],[OrganizeId],[sfmb],[sfmbmc],[mzzybz],[kffw],[zt],
[CreatorCode],[CreateTime],[ks])
values(@sfmbbh,@orgId,@sfmb,@sfmbmc,2,0,'1',@user,getdate(),@ks)";
                this.ExecuteSqlCommand(sql, new SugarParameter[]
                {
                    new SugarParameter("@sfmbbh", entity.sfmbbh),
                    new SugarParameter("@orgId", entity.OrganizeId),
                    new SugarParameter("@sfmb", entity.sfmb),
                    new SugarParameter("@sfmbmc", entity.sfmbmc),
                    new SugarParameter("@user", user),
                    new SugarParameter("@ks", entity.ks)
                });
            }

            // 7-fix: tham so hoa INSERT xt_sfmbxm theo tung item (truoc day noi
            // OrganizeId/sfmbbh/sfxmCode/user/sl truc tiep vao chuoi SQL).
            if (!string.IsNullOrWhiteSpace(xmListStr))
            {
                var itemList = Json.ToObject<IList<SysChargeItemTemplateVO>>(xmListStr);
                if (itemList.Count > 0)
                {
                    foreach (var item in itemList)
                    {
                        var sqlitems = @"INSERT INTO " + DbNames.Name("SettDb", "NewtouchHIS_Sett") + @".dbo.[xt_sfmbxm]
                ([sfmbxmId],[OrganizeId],[sfmbbh],[sfxm],[CreatorCode],[CreateTime],[zt],[sl])
                values(newid(),@orgId,@sfmbbh,@sfxm,@user,getdate(),'1',@sl)";
                        this.ExecuteSqlCommand(sqlitems, new SugarParameter[]
                        {
                            new SugarParameter("@orgId", entity.OrganizeId),
                            new SugarParameter("@sfmbbh", entity.sfmbbh),
                            new SugarParameter("@sfxm", item.sfxmCode),
                            new SugarParameter("@user", user),
                            new SugarParameter("@sl", (object)item.sl ?? System.DBNull.Value)
                        });
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cnt"></param>
        /// <param name="orgId"></param>
        /// <param name="dl">1Thuốc 2Khoản 3Khoản không điều trị	Nhiều Phân tách bằng dấu phẩy</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysChargeItemTemplateVO> GetDicChargeItems(int cnt ,string orgId,string dl,string keyword)
        {
            string sql = "exec [NewtouchHIS_Sett].[dbo].[usp_SelectSfxmYp] @topCount=@topCount,@orgId=@orgId,@dllb=@dl ";
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", orgId));
            pars.Add(new SugarParameter("@topCount", cnt));
            pars.Add(new SugarParameter("@dl", dl));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += ",@keyword=@keyword ";
                pars.Add(new SugarParameter("@keyword", keyword));
            }

            return this.FindList<SysChargeItemTemplateVO>(sql, pars.ToArray());
        }

        /// <summary>
        /// Lấy danh sách khoản của mẫu thu phí
        /// </summary>
        /// <param name="sfmbbh"></param>
        /// <returns></returns>
        public IList<SysChargeItemTemplateVO> GetChargeTemplateChargeItemList(string sfmbbh, string orgId)
        {
            var sql = @"select 
b.sfxmId ,
b.sfxmCode ,
b.sfxmmc ,
b.sfdlCode ,
b.badlCode ,
b.nbdlCode ,
b.py ,
b.dw ,
b.dj ,
b.zfbl ,
b.mzzybz ,
b.CreateTime ,
b.ssbz ,
a.sl ,
a.duration ,
a.kflb ,
a.zll ,
a.zxcs ,
a.zxks, a.yzpc,
a.bw
from [NewtouchHIS_Sett].[dbo].xt_sfmbxm(nolock) a
left join NewtouchHIS_Base.[dbo].V_S_xt_sfxm b on a.sfxm = b.sfxmCode and b.OrganizeId = @orgId
                where a.OrganizeId = @orgId and a.sfmbbh = @sfmbbh
                and a.zt = '1' and b.zt = '1'";
            return this.FindList<SysChargeItemTemplateVO>(sql, new[] { new SugarParameter("@sfmbbh", sfmbbh)
                ,new SugarParameter("@orgId", orgId)});
        }

        public SysChargeTemplateInfoVM GetSysChargeTemplateInfo(string keyValue,string orgId)
        {
            //Danh sách khoản thu phí
            string sql = @"select sfmbbh,OrganizeId,sfmb,sfmbmc,ks,px,zt 
from [NewtouchHIS_Sett].[dbo].xt_sfmb with(nolock)
where sfmbbh=@Id
            ";

            var templateEntity = this.FindList<SysChargeTemplateDto>(sql, new SugarParameter[]
            {
                new SugarParameter("@Id",keyValue), 
            }).FirstOrDefault();
            
            if (templateEntity == null || string.IsNullOrWhiteSpace(keyValue) || templateEntity.OrganizeId == null)
            {
                return null;
            }
            return new SysChargeTemplateInfoVM()
            {
                TemplateEntity = templateEntity,
                SysList = GetChargeTemplateChargeItemList(keyValue, templateEntity.OrganizeId)
            };
        }

    }
}
