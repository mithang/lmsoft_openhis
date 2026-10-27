using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    public class HljlDataRepo : RepositoryBase<HljlDataEntity>, IHljlDataRepo
    {



        public List<HljldataVO> blIdselect(string zyh, string mbbh, string orgId)
        {
            var sql = @"select top 1 b.Id from [Newtouch_EMR].[dbo].[zy_meddocs_relation] a
left join  [Newtouch_EMR].[dbo].[bl_hljl] b
on a.blId=b.Id and a.OrganizeId=b.OrganizeId and b.zt='1'
where a.mbId='"+mbbh+"' and a.zyh='"+zyh+"' and a.OrganizeId='"+orgId+"' order by b.CreateTime desc";
            return this.FindList<HljldataVO>(sql);
        }





        //护理记录数据提交
        public void BtnSubmit(OperatorModel user, IList<HljldataVO> data,string blId,string zyh)
        {
            if (data == null)
            {
                return;
            }
            foreach (var item in data)
            {
                item.blId = (string.IsNullOrWhiteSpace(item.blId)) ? blId : item.blId;
                if (!string.IsNullOrWhiteSpace(item.Id))
                {
                    var sqldelete = "update [Newtouch_EMR].[dbo].[bl_hljldata] set zt='0',LastModifierCode="+user.rygh
                        +",LastModifyTime=getdate()  where Id='" + item.Id+"'";
                    ExecuteSqlCommand(sqldelete);
                    item.Id = null;
                }
//                var sql = @"insert into [Newtouch_EMR].[dbo].[bl_hljldata] values(
//newId(),'" + item.blId + "','" + item.jlrq + "','" + item.tw + "','" + item.mb + "','" + item.hx + "','" + item.xy + "','" + item.ybhd + "','" + item.cxxdjc + "'" +
//",'" + item.xroyx + "','" + item.hljb + "','" + item.xzjs + "','" + item.pbjyxkt + "','" + item.ycyf + "','" + item.ddyf + "','" + item.qtjh + "'" +
//",'" + item.zkhl + "','" + item.dglb + "','" + item.hlzd + "','" + item.nl + "','" + item.wy + "','" + item.bqhlcontent + "','" + item.hsqm + "'" +
//",'1',getdate(),'" + user.UserCode + "',NULL,NULL,'" + user.OrganizeId + "','"+item.jlwzsj+"','"+zyh+"')";

                string sql = @"insert into [Newtouch_EMR].[dbo].[bl_hljldata]([Id],[blId],[jlrq],[tw],[mb],[hx],[xy],[ybhd],[cxxdjc],[xroyx],[hljb],[xzjs],[pbjyxkt],[ycyf],[ddyf]
,[qtjh],[zkhl],[dglb],[hlzd],[nl],[wy],[bqhlcontent],[hsqm],[zt],[CreateTime]
,[CreatorCode],[LastModifyTime],[LastModifierCode],[OrganizeId],[jlwzsj],[zyh]) 
values(
newId(),@blId,@jlrq,@tw,@mb,@hx,@xy,@ybhd,@cxxdjc" +
",@xroyx,@hljb,@xzjs,@pbjyxkt,@ycyf,@ddyf,@qtjh" +
",@zkhl,@dglb,@hlzd,@nl,@wy,@bqhlcontent,@hsqm" +
",'1',getdate(),'" + user.UserCode + "',NULL,NULL,'" + user.OrganizeId + "',@jlwzsj,'" + zyh+"')";

                ExecuteSqlCommand(sql,new SugarParameter[] {
                    new SugarParameter("blId",item.blId),
                    new SugarParameter("jlrq",item.jlrq),
                    new SugarParameter("tw",item.tw??""),
                    new SugarParameter("mb",item.mb??""),
                    new SugarParameter("hx",item.hx??""),
                    new SugarParameter("xy",item.xy??""),
                    new SugarParameter("ybhd",item.ybhd??""),
                    new SugarParameter("cxxdjc",item.cxxdjc??""),
                    new SugarParameter("xroyx",item.xroyx??""),
                    new SugarParameter("hljb",item.hljb??""),
                    new SugarParameter("xzjs",item.xzjs??""),
                    new SugarParameter("pbjyxkt",item.pbjyxkt??""),
                    new SugarParameter("ycyf",item.ycyf??""),
                    new SugarParameter("ddyf",item.ddyf??""),
                    new SugarParameter("qtjh",item.qtjh??""),
                    new SugarParameter("zkhl",item.zkhl??""),
                    new SugarParameter("dglb",item.dglb??""),
                    new SugarParameter("hlzd",item.hlzd??""),
                    new SugarParameter("nl",item.nl??""),
                    new SugarParameter("wy",item.wy??""),
                    new SugarParameter("bqhlcontent",item.bqhlcontent??""),
                    new SugarParameter("hsqm",item.hsqm??""),
                    new SugarParameter("jlwzsj",item.jlwzsj)
                });                 
            }

        }

        public PatMedRecordTreeVO HljlCtrlQx(string blId,string mbbh,string orgId, string user)
        {
            string sql = "";
            if (!string.IsNullOrWhiteSpace(blId))
            {                
                sql = @"select max(ctrlLevel) ctrlLevel,a.Id BlId,isnull(a.IsLock,0)IsLock,
(case when a.LastModifierCode=@user then '' else a.LastModifierCode end)LastModifierCode
from bl_hljl a with(nolock) ,bl_mbqxkz c with(nolock),NewtouchHIS_Base.dbo.V_C_Sys_StaffDuty d
where a.Id=@blId and  a.OrganizeId=@orgId and a.zt='1' and 
a.mbbh=c.mbId   and c.zt='1'
and c.dutyCode = d.DutyCode and d.StaffGh = @user and d.OrganizeId=c.OrganizeId
group by a.Id,a.IsLock,a.LastModifierCode ";
            }
            else if (!string.IsNullOrWhiteSpace(mbbh))
            {
                sql = @"select max(ctrlLevel) ctrlLevel,0 IsLock
from bl_mbqxkz c with(nolock),NewtouchHIS_Base.dbo.V_C_Sys_StaffDuty d
where c.OrganizeId=@orgId and mbId =@mbbh
and c.dutyCode = d.DutyCode and d.StaffGh = @user ";
            }
            return this.FirstOrDefault<PatMedRecordTreeVO>(sql, new SugarParameter[] {
                new SugarParameter("blId",blId??""),
                new SugarParameter("mbbh",mbbh??""),
                new SugarParameter("user",user),
                new SugarParameter("orgId",orgId)
            });
        }




        //护理记录页面加载
        public IList<HljldataVO> HljlLoadData(Pagination pagination, string zyh, string blId,string orgId,string kssj,string jssj)
        {
            string sql = "";
            if (!string.IsNullOrWhiteSpace(zyh)&&!string.IsNullOrWhiteSpace(blId))
            {
                sql = @"select a.Id, a.blId,a.jlrq, a.tw, a.mb, a.hx, a.xy, a.ybhd, a.cxxdjc, a.xroyx, a.hljb, a.xzjs, a.pbjyxkt, a.ycyf, 
a.ddyf, a.qtjh, a.zkhl, a.dglb, a.hlzd, a.nl, a.wy, a.bqhlcontent, a.hsqm, a.zt, a.CreateTime, 
a.CreatorCode, a.LastModifyTime, a.LastModifierCode, a.OrganizeId, CONVERT(varchar(10), a.jlwzsj, 120)jlwzsj ,b.IsLock 
from [Newtouch_EMR].dbo.bl_hljldata a with(nolock) ,bl_hljl b with(nolock) 
where a.blId=@blId and a.OrganizeId=@orgId and a.zyh=@zyh and  a.zt='1' 
and a.blId=b.Id and b.zt='1'
and a.jlwzsj between @kssj and @jssj
order by jlwzsj,jlrq ";
            }
            
            return FindList<HljldataVO>(sql, new[] {
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@zyh", zyh),
                new SugarParameter("@blId", blId),
                new SugarParameter("@kssj", kssj??DateTime.Now.ToString("yyyy-MM-dd")),
                new SugarParameter("@jssj", jssj??DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))
            });


        }

        public List<InfordivVO> Infodiv(string zyh, string orgId)
        {
            var sql = @"select xm,zyh,(case when sex='1' then 'Nam' else 'Nữ' end) sex,b.Name ks,a.bedname cwmc,a.zdmc zd
from [Newtouch_EMR].[dbo].[zy_brjbxx] a
left join NewtouchHIS_Base.[dbo].[Sys_Department] b
on a.DeptCode=b.Code and a.OrganizeId=b.OrganizeId and b.zt='1'
where zyh='" + zyh+"' and a.zt='1'and a.OrganizeId='"+orgId+"'";

            return this.FindList<InfordivVO>(sql);
           
        }

        public List<HljldataVO> mbbhselect(string zyh, string mbbh,string orgId,string kssj,string jssj)
        {
            var sql = @"select b.Id, b.blId, b.jlrq, b.tw, b.mb, b.hx, b.xy, b.ybhd, b.cxxdjc, b.xroyx, b.hljb, b.xzjs, b.pbjyxkt, b.ycyf, 
b.ddyf, b.qtjh, b.zkhl, b.dglb, b.hlzd, b.nl, b.wy, b.bqhlcontent, b.hsqm, b.zt, b.CreateTime, 
b.CreatorCode, b.LastModifyTime, b.LastModifierCode, b.OrganizeId, CONVERT(varchar(10), b.jlwzsj, 120)jlwzsj  
from [Newtouch_EMR].[dbo].[zy_meddocs_relation] a ,[Newtouch_EMR].[dbo].[bl_hljldata] b
where a.blId=b.blId and a.OrganizeId=b.OrganizeId and a.zt='1' and  b.zt='1' 
and a.zyh='" + zyh+"' and a.mbId= '"+mbbh+ " ' and a.OrganizeId='"+orgId+ "'and b.jlwzsj between '"+kssj+"' and '"+jssj+"'+' 23:59:59'";
            return this.FindList<HljldataVO>(sql);
        }
    }
}
