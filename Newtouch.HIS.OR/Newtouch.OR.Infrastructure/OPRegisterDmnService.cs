using NewtouchHIS.Lib.Base.Model;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.DomainServices
{
    public class OPRegisterDmnService : DmnServiceBase, IOPRegisterDmnService
    {
        public OPRegisterDmnService()
        {

        }
        public RegistrationListVO getRegistrationInfo(string OrganizeId,string ArrangeId, string sqzt)
        {
            string sql = "select b.OrganizeId,a.id,b.id as arrangeId,b.applyId,b.applyno,b.xm,c.xb,b.zyh,b.bq,b.ch,b.zd,a.sszd,a.sszdmc,a.shbq,a.memo,a.ssxh,";
            sql += "(case when datediff(yy,csrq,getdate())>=1 then convert(varchar(3),datediff(yy,csrq,getdate()))+' tuổi' else convert(varchar(2),datediff(mm,csrq,getdate()))+' tháng' end  )nl,";
            var para = new List<SugarParameter>();
            if (sqzt == "4" || sqzt=="已登记")
            { //Đã đăng ký
                sql += "d.ssjb,a.ssmc as ssmc,d.ssdm,c.sssj as sssqsj,b.sssj as ssapsj,a.sskssj,a.ssjssj,a.oproom,a.oporder,a.AnesCode,a.ssbw,a.qkdj,a.isgl,a.isjun,a.shuxl,a.shixl,a.zrxl,a.zcxl,a.ryzd,a.ryzdmc,";
                sql += "ysgh=(select rygh from [dbo].[OR_OpStaffRecord] where ssxh=a.ssxh and rylb='1' and px='1' and zt='1'),zlys1 = (select rygh from[dbo].[OR_OpStaffRecord] where ssxh = a.ssxh and rylb = '2' and px = '1' and zt='1'),zlys2=(select rygh from[dbo].[OR_OpStaffRecord] where ssxh = a.ssxh and rylb = '2' and px = '2' and zt='1'),xhhs=(select rygh from[dbo].[OR_OpStaffRecord] where ssxh = a.ssxh and rylb = '3' and px = '1' and zt='1'),xshs=(select rygh from[dbo].[OR_OpStaffRecord] where ssxh = a.ssxh and rylb = '4' and px = '1' and zt='1'),mzys=(select rygh from[dbo].[OR_OpStaffRecord] where ssxh = a.ssxh and rylb = '5' and px = '1' and zt='1')";
				sql += ",ysxm=(select ryxm from [dbo].[OR_OpStaffRecord] where ssxh=a.ssxh and rylb='1' and px='1' and zt='1'), zlys1name = (select ryxm from[dbo].[OR_OpStaffRecord] where ssxh = a.ssxh and rylb = '2' and px = '1' and zt = '1'), zlys2name = (select ryxm from[dbo].[OR_OpStaffRecord] where ssxh = a.ssxh and rylb = '2' and px = '2' and zt = '1'), xhhsname = (select ryxm from[dbo].[OR_OpStaffRecord] where ssxh = a.ssxh and rylb = '3' and px = '1' and zt = '1'), xshsname = (select ryxm from[dbo].[OR_OpStaffRecord] where ssxh = a.ssxh and rylb = '4' and px = '1' and zt = '1'), mzysname = (select ryxm from[dbo].[OR_OpStaffRecord] where ssxh = a.ssxh and rylb = '5' and px = '1' and zt = '1')";
			}
            else
            {//Đã xếp lịch
                sql += "d.ssjb,b.ssmc,d.ssdm,c.sssj as sssqsj,b.sssj as ssapsj,a.sskssj,a.ssjssj,b.oproom,b.oporder,b.AnesCode,b.ssbw,a.qkdj,c.isgl,a.isjun,a.shuxl,a.shixl,a.zrxl,a.zcxl,b.zd as ryzd,b.zd as ryzdmc,";
                sql += "b.ysgh,b.zlys1,b.zlys2,b.xhhs,b.xshs,";
                sql += "c.mzys";
				sql += ",b.ysxm,zlys1name = (select top 1 staffname from NewtouchHIS_Base..V_C_Sys_StaffWard  where staffPY = (select top 1 zlys1 from[dbo].OR_Arrangement where id = @arrangeId)),zlys2name = (select top 1 staffname from NewtouchHIS_Base..V_C_Sys_StaffWard  where staffPY = (select top 1 zlys2 from[dbo].OR_Arrangement where id = @arrangeId)),xhhsname = (select top 1 staffname from NewtouchHIS_Base..V_C_Sys_StaffWard  where staffPY = (select top 1 xhhs from[dbo].OR_Arrangement where id = @arrangeId)),xshsname = (select top 1 staffname from NewtouchHIS_Base..V_C_Sys_StaffWard  where staffPY = (select top 1 xshs from[dbo].OR_Arrangement where id = @arrangeId))";

			}
            sql += "from [OR_Arrangement] b left join [OR_Registration] a on a.ArrangeId = b.Id left join[OR_ApplyInfo] c on b.ApplyId = c.Id left join [dbo].[OR_ApplyInfo_Expand] e on e.Applyno=c.Applyno and e.px=1 left join[OR_Operation] d on e.ssmc = d.ssmc and d.organizeId=@OrganizeId where b.zt=1 ";
            if (!string.IsNullOrWhiteSpace(OrganizeId))
            {
	            sql += " and b.OrganizeId=@OrganizeId ";
				para.Add(new SugarParameter("@OrganizeId",OrganizeId));
            }
            if (!string.IsNullOrWhiteSpace(ArrangeId))
            {
	            sql += " and b.id=@arrangeId";
	            para.Add(new SugarParameter("@arrangeId", ArrangeId));
            }

            return this.FindList<RegistrationListVO>(sql, para.ToArray()).FirstOrDefault();
        }


        /// <summary>
        /// Trang đăng ký phẫu thuật,Lấy danh sách xếp lịch phẫu thuật ở trạng thái đã xếp lịch, đã đăng ký
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<ArrangeRegVO> GetPagintionList(Pagination pagination, string OrganizeId,string keyword, string bq,string djzt)
        {
            var sql = new StringBuilder();
            sql.Append("select a.[OrganizeId],a.[Id] ,[ApplyId],a.[Applyno],a.[zyh] ,[xm],[ks],[bq],[ch],[zd],a.[sqzt],c.[ssmc],c.[ssdm],[sssj],[ysgh],[ysxm] ,a.[AnesCode],a.[oproom] ,a.[oporder] ,[zlys1],[zlys2],[zlys3],[zlys4],[xhhs] ,[xshs] ,a.[ssbw],a.[zt],a.[CreateTime],a.[CreatorCode],a.[LastModifyTime],a.[LastModifierCode],b.ssxh,b.sqzt as djzt from [OR_Arrangement] a left join [OR_Registration] b on a.id = b.ArrangeId left join [OR_ApplyInfo_Expand] c on a.organizeId=c.organizeId and a.applyno=c.applyno and px=1  where a.zt != 0");
            sql.Append(" and a.sqzt in(2,4)");
            List<SugarParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SugarParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or a.xm like @keyword");
                sql.Append(" or a.zyh like @keyword");
                sql.Append(" or b.ssxh like @keyword");
                sql.Append(")");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
			}
			if (!string.IsNullOrWhiteSpace(OrganizeId))
			{
				pars = pars ?? new List<SugarParameter>();
				sql.Append(" and a.OrganizeId=@OrganizeId");
				pars.Add(new SugarParameter("@OrganizeId", OrganizeId.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(bq))
            {
                pars = pars ?? new List<SugarParameter>();
                sql.Append(" and a.bq=@bq");
                pars.Add(new SugarParameter("@bq", bq.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(djzt))
			{
				pars = pars ?? new List<SugarParameter>();
				if (djzt == "1")
				{
					sql.Append(" and (b.sqzt=@djzt or b.sqzt is null)");
				}
				else
				{
					sql.Append(" and b.sqzt=@djzt");
				}	

				pars.Add(new SugarParameter("@djzt", djzt.Trim()));
			}
			return this.QueryWithPage<ArrangeRegVO>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

        public IList<RegistrationListVO> GetPatOpRegistList(string ksrq,string jsrq,string zyh, string djzt,string orgId)
        {
            var sql = new StringBuilder();
            sql.Append(@"select [OrganizeId],[Id],[ArrangeId],[Applyno],[ssxh],[zyh],[sqzt],[ryzd]
,[ryzdmc],[sszd],[sszdmc],[shbq],[ssmc],[ssdm],[ssapsj],[sssqsj],[sskssj]
,[ssjssj],[AnesCode],[oproom],[oporder],[qkdj],[isgl],[isjun],[shuxl],[shixl]
,[zrxl],[zcxl],[ssbw]
FROM [Newtouch_OR].[dbo].[OR_Registration] with(nolock)
where zt='1' and OrganizeId=@orgId ");

            List<SugarParameter> pars = new List<SugarParameter>(); ;
            pars.Add(new SugarParameter("@orgId", orgId));

            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sql.Append(" and zyh=@zyh ");
                pars.Add(new SugarParameter("@zyh", zyh));
            }

            if (!string.IsNullOrWhiteSpace(djzt))
            {
                sql.Append(" and sqzt=@sqzt ");
                pars.Add(new SugarParameter("@sqzt", djzt));
            }

            if (!string.IsNullOrWhiteSpace(ksrq) && !string.IsNullOrWhiteSpace(jsrq))
            {
                sql.Append(" and sskssj>=@ksrq  and ssjssj<@jsrq");
                pars.Add(new SugarParameter("@ksrq", Convert.ToDateTime(ksrq).ToString("yyyy-MM-dd") ));
                pars.Add(new SugarParameter("@jsrq", Convert.ToDateTime(jsrq).AddDays(1).ToString("yyyy-MM-dd")));
            }

            return this.FindList<RegistrationListVO>(sql.ToString(), pars.ToArray());
        }
    }
}
