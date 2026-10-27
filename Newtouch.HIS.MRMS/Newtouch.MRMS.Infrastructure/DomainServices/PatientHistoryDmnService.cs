using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;

namespace Newtouch.MR.ManageSystem.DomainServices
{
	public class PatientHistoryDmnService : DmnServiceBase, IPatientHistoryDmnService
	{
		public PatientHistoryDmnService()
			
		{

		}

		/// <summary>
		/// TheoIDLấy thông tin chẩn đoán
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="organizeId"></param>
		/// <param name="patId"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public IList<zys_zdVO> GetzdList(Pagination pagination, string organizeId, string id)
		{
			var sql = new StringBuilder();
			sql.Append("select a.id,a.zyh,b.zdorder zdtype,b.jbmc zdms,b.cyqkms zljg,a.sjzyts zlts,b.createTime zdrq from[dbo].[mr_basy] a left join[dbo].[mr_basy_zd] b on a.zyh = b.zyh  where a.zt=1 and b.zt=1 ");

			List<SugarParameter> pars = null;
			if (!string.IsNullOrWhiteSpace(organizeId))
			{
				pars = pars ?? new List<SugarParameter>();
				sql.Append(" and a.organizeid = @organizeId");
				pars.Add(new SugarParameter("@organizeId", organizeId));
			}
			if (!string.IsNullOrWhiteSpace(id))
			{
				pars = pars ?? new List<SugarParameter>();
				sql.Append(" and a.id=@id");
				pars.Add(new SugarParameter("@id", id));
			}
			return this.QueryWithPage<zys_zdVO>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
		}
		public IList<zys_zdVO> GetzdList(string organizeId, string id)
		{
			string sql="select a.id,a.zyh,b.zdorder zdtype,b.jbmc zdms,b.cyqkms zljg,a.sjzyts zlts,b.createTime zdrq from[dbo].[mr_basy] a left join[dbo].[mr_basy_zd] b on a.zyh = b.zyh  where a.zt=1 and b.zt=1 ";
			
			if (!string.IsNullOrWhiteSpace(organizeId))
			{
				sql+=" and a.organizeid = @organizeId";
			}
			if (!string.IsNullOrWhiteSpace(id))
			{
				sql+=" and a.id=@id";
			}
			return this.FindList<zys_zdVO>(sql, new SugarParameter[] {
				new SugarParameter("@organizeId", organizeId),
				new SugarParameter("@id",id),
				});
		}

		/// <summary>
		/// TheoIDLấy thông tin chẩn đoán ngoại trú
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="organizeId"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public IList<zys_zdVO> GetmzzdList(Pagination pagination, string organizeId, string id)
		{
			var sql = new StringBuilder();
			sql.Append("select id,zyh,0 zytype,mzzd zdms, null zljg,sjzyts zlts,createtime zdrq  from [dbo].[mr_basy] where zt = 1 ");

			List<SugarParameter> pars = null;
			if (!string.IsNullOrWhiteSpace(organizeId))
			{
				pars = pars ?? new List<SugarParameter>();
				sql.Append(" and organizeid = @organizeId");
				pars.Add(new SugarParameter("@organizeId", organizeId));
			}
			if (!string.IsNullOrWhiteSpace(id))
			{
				pars = pars ?? new List<SugarParameter>();
				sql.Append(" and id=@id");
				pars.Add(new SugarParameter("@id", id));
			}
			return this.QueryWithPage<zys_zdVO>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
		}
		public IList<zys_zdVO> GetmzzdList( string organizeId, string id)
		{
			string sql="select id,zyh,0 zytype,mzzd zdms, null zljg,sjzyts zlts,createtime zdrq  from [dbo].[mr_basy] where zt = 1 ";
			
			if (!string.IsNullOrWhiteSpace(organizeId))
			{
				sql+=" and organizeid = @organizeId";
			}
			if (!string.IsNullOrWhiteSpace(id))
			{
				sql+=" and id=@id";
			}
			return this.FindList<zys_zdVO>(sql, new SugarParameter[] {
				new SugarParameter("@organizeId", organizeId),
				new SugarParameter("@id",id),
				});
		}


		/// <summary>
		/// TheoIDLấy thông tin phẫu thuật
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="organizeId"></param>
		/// <param name="patId"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public IList<zys_ssVO> GetssList(Pagination pagination, string organizeId, string id)
		{
			var sql = new StringBuilder();
			sql.Append("select b.id,b.ssjczmc ssmc,c.AnesName mzfs,d.name ssys,b.ssjczrq ssrq from [dbo].[mr_basy] a  left join[dbo].[mr_basy_ss] b on a.zyh = b.zyh left join[Newtouch_OR].[dbo].[OR_Anesthesia] c on b.mzfs = c.anesCode left join[NewtouchHIS_Base].[dbo].[Sys_Staff] d on b.sz = d.gh where a.zt != 0 and b.zt != 0  ");

			List<SugarParameter> pars = null;
			if (!string.IsNullOrWhiteSpace(organizeId))
			{
				pars = pars ?? new List<SugarParameter>();
				sql.Append(" and a.organizeid = @organizeId");
				pars.Add(new SugarParameter("@organizeId", organizeId));
			}
			if (!string.IsNullOrWhiteSpace(id))
			{
				pars = pars ?? new List<SugarParameter>();
				sql.Append(" and a.id=@id");
				pars.Add(new SugarParameter("@id", id));
			}
			return this.QueryWithPage<zys_ssVO>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
		}

		public IList<zys_ssVO> GetssList(string organizeId, string id)
		{
			string sql = @"select b.id,b.ssjczmc ssmc,c.AnesName mzfs,d.name ssys,b.ssjczrq ssrq from [dbo].[mr_basy] a  left join[dbo].[mr_basy_ss] b on a.zyh = b.zyh left join[Newtouch_OR].[dbo].[OR_Anesthesia] c on b.mzfs = c.anesCode left join[NewtouchHIS_Base].[dbo].[Sys_Staff] d on b.sz = d.gh where a.zt != 0 and b.zt != 0  ";
			if (!string.IsNullOrWhiteSpace(organizeId))
			{
				sql+=" and a.organizeid = @organizeId";
			}
			if (!string.IsNullOrWhiteSpace(id))
			{
				sql+=" and a.id=@id";
			}
			return this.FindList<zys_ssVO>(sql, new SugarParameter[] {
				new SugarParameter("@organizeId", organizeId),
				new SugarParameter("@id",id),
				});
		}


	}
}