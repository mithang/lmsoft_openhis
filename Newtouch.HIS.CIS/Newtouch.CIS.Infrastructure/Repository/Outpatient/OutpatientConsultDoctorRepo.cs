
using Newtouch.CIS.Repository;
using DbConfigHelper;
using Newtouch.Domain.Entity.Outpatient;
using Newtouch.Domain.IRepository.Outpatient;
using Newtouch.Domain.ValueObjects.Outpatient;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository.Outpatient
{
    public class OutpatientConsultDoctorRepo : RepositoryBase<OutpatientConsultDoctorEntity>, IOutpatientConsultDoctorRepo
    {
        public OutpatientConsultDoctorRepo()
            : base()
        {
        }

        /// <summary>
        /// LưuPhòng khámkhingàyrakhámBác sĩ
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SubmitForm(OutpatientConsultDoctorEntity entity)
        {
            //var zsys = this.IQueryable().Where(p => p.zsCode == entity.zsCode && p.rq.ToString("yyyy-MM-dd") ==DateTime.Now.ToString("yyyy-MM-dd") && p.zt == "1" && p.OrganizeId == entity.OrganizeId).FirstOrDefault();
            //var zsys = this.IQueryable().Where(p => p.zsCode == entity.zsCode && p.zt == "1" && p.OrganizeId == entity.OrganizeId).FirstOrDefault();
            //var list = this.IQueryable().Where(p => p.zsCode == entity.zsCode && p.zt == "1" && p.OrganizeId == entity.OrganizeId);
            //var zsys = list.FirstOrDefault();
            var zsys = this.GetTodayDoctorByConsult(entity.zsCode,entity.OrganizeId);
            //Sửa
            if (zsys != null)
            //if (!string.IsNullOrEmpty(ghzs.ghnm.ToString()))
            {
                var dbEntity = this.FindEntity(zsys.Id);
                //properties
                dbEntity.gh = entity.gh;
                dbEntity.Modify(zsys.Id);
                return Update(dbEntity);
            }
            //Lưu
            entity.Create();
            return Insert(entity);
        }

		public int UpdateZS(OutpatientConsultDoctorVO zsov)
		{

			var sqlstr = @"update " + DbNames.Name("BaseDb", "NewtouchHIS_Base") + @"..xt_zs set zslc=(case when @zslc='' then null else @zslc end ),
zsfh=(case when @zsfh='' then null else @zsfh end ),
ys=(case when @ys='' then null else @ys end ),
ysmc=(case when @ysmc='' then null else @ysmc end )
where zt=1 and zsCode=@zscode";

			 return this.ExecuteSqlCommand(sqlstr, new[] { new SqlParameter("@zslc", zsov.zslc??"")
				,new SqlParameter("@zsfh", zsov.zsfh??""),new SqlParameter("@ys", zsov.gh??""),new SqlParameter("@ysmc", zsov.ysxm??"") ,new SqlParameter("@zscode", zsov.zsCode) });
		}
		/// <summary>
		/// Tra cứukhingàycủaphútkhámBác sĩ
		/// </summary>
		/// <param name="zsCode"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		public OutpatientConsultDoctorEntity GetTodayDoctorByConsult(string zsCode,string orgId)
        {
            if (string.IsNullOrWhiteSpace(zsCode))
            {
                return null;
            }
            string sql = @"select * from mz_zsys
  where organizeId=@orgId and zt=1
  and zsCode=@zsCode
  and rq between dateadd(day,-1,GETDATE()) and  GETDATE()";

            return FirstOrDefault<OutpatientConsultDoctorEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@zsCode", zsCode)

            });
        }

        
        public List<OutpatientConsultDoctorVO> GetRepeatDoctor(string orgId, string zsStr,string ghStr)
        {
            if (string.IsNullOrWhiteSpace(zsStr))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(ghStr))
            {
                return null;
            }
            string sql = @"select zsCode,zsmc,ys gh ,ysmc ysxm from [NewtouchHIS_Base].dbo.xt_zs
where organizeId=@orgId and zt=1 
and zsCode not in (select * from [dbo].[f_split](@zsStr,',') )
and ys in (select * from [dbo].[f_split](@ghStr,',') )";

            return this.FindList<OutpatientConsultDoctorVO>(sql, new[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@zsStr", zsStr),
                new SqlParameter("@ghStr", ghStr)

            });
        }
    }
}
