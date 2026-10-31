
using Newtouch.CIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả:Nhập việnbệnh nhândấu hiệu sinh tồn
    /// </summary>
    public class InpatientVitalSignsRepo : RepositoryBase<InpatientVitalSignsEntity>, IInpatientVitalSignsRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public InpatientVitalSignsRepo()
            : base()
        {

        }

        /// <summary>
        /// điều dưỡngphúttrangTra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<InpatientVitalSignsDto> GetPaginationList(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string zyh,string wardCode,string isShowDelete)
        {
			//            var sb = new StringBuilder();
			//            sb.Append(@"declare @strhljb varchar(1000),@hljb int=0
			//	select @strhljb=value 
			//	from [dbo].[Sys_Config] with(nolock)
			//	where code='NursinLevelChargeItemMapp' and organizeid=@orgId and zt=1

			//	create table #tmp( num int identity(1,1) primary key,col varchar(200))

			//	insert into #tmp
			//	select col from dbo.f_split(@strhljb,',')

			//select a.*,b.xm,j.sfxmmc hljb,itemhlys.Name hlysname,itembrfood.Name brfoodname,itempfqk.Name pfqkname,itemgdhl.Name gdhlname from zy_brsmtz(nolock) a 
			//left join zy_brxxk b on a.zyh=b.zyh and a.OrganizeId=b.OrganizeId and b.zt='1'
			//LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail itemhlys ON ( itemhlys.OrganizeId = a.OrganizeId
			//                                                              OR itemhlys.OrganizeId = '*'
			//                                                              )
			//                                                              AND itemhlys.CateCode = 'NursingCognition'
			//                                                              AND itemhlys.Code = a.hlys
			//LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail itembrfood ON ( itembrfood.OrganizeId = a.OrganizeId
			//                                                              OR itembrfood.OrganizeId = '*'
			//                                                              )
			//                                                              AND itembrfood.CateCode = 'NursingFood'
			//                                                              AND itembrfood.Code = a.brfood
			//LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail itempfqk ON ( itempfqk.OrganizeId = a.OrganizeId
			//                                                              OR itempfqk.OrganizeId = '*'
			//                                                              )
			//                                                              AND itempfqk.CateCode = 'NursingSkin'
			//                                                              AND itempfqk.Code = a.pfqk
			//LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail itemgdhl ON ( itemgdhl.OrganizeId = a.OrganizeId
			//                                                              OR itemgdhl.OrganizeId = '*'
			//                                                              )
			//                                                              AND itemgdhl.CateCode = 'PipelineNursing'
			//                                                              AND itemgdhl.Code = a.gdhl
			//left join #tmp i on i.num=b.hljb
			//left join NewtouchHIS_Base..V_S_xt_sfxm j on j.sfxmCode=i.col and j.OrganizeId=a.OrganizeId and j.zt='1'
			//where a.OrganizeId = @orgId and a.zt = '1' ");
			//            var pars = new List<SqlParameter>();
			//            pars.Add(new SqlParameter("@orgId", orgId));
			//            if (kssj.HasValue)
			//            {
			//                sb.Append(" and a.rq >= @kssj");
			//                pars.Add(new SqlParameter("@kssj", kssj.Value.Date));
			//            }
			//            if (jssj.HasValue)
			//            {
			//                sb.Append(" and a.rq < @jssj");
			//                pars.Add(new SqlParameter("@jssj", jssj.Value.Date.AddDays(1)));
			//            }
			//            if (!string.IsNullOrWhiteSpace(zyh))
			//            {
			//                sb.Append(" and a.zyh like @searchZyh");
			//                pars.Add(new SqlParameter("@searchZyh", "%" + zyh.Trim() + "%"));
			//            }
			//            sb.Append(@" order by zyh,rq,sj 
			//                drop table #tmp");
			//            return this.FindList<InpatientVitalSignsDto>(sb.ToString(), pars.ToArray());

			//2021-2-2: giải quyết việc dùng Chinese_PRC_CI_AS khi tra cứu stored procedure đã tồn tại
			var pars = new List<SqlParameter>();
			pars.Add(new SqlParameter("@orgId", orgId));
			pars.Add(new SqlParameter("@kssj", kssj.Value.Date));
			pars.Add(new SqlParameter("@jssj", jssj.Value.Date.AddDays(1)));
			pars.Add(new SqlParameter("@zyh", "%" + zyh.Trim() + "%"));
			pars.Add(new SqlParameter("@wardCode", "%" + wardCode.Trim() + "%"));
			pars.Add(new SqlParameter("@isShowDelete", isShowDelete));
			return this.FindList<InpatientVitalSignsDto>("EXEC [dbo].[InpatientVitalSigns] @orgId=@orgId,@kssj=@kssj,@jssj=@jssj,@zyh=@zyh ,@wardCode=@wardCode,@isShowDelete=@isShowDelete", pars.ToArray());

		}

		/// <summary>
		/// điều dưỡngTra cứu(khôngphúttrang)
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="orgId"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public IList<InpatientVitalSignsDto> GetList(string orgId, DateTime? kssj, DateTime? jssj, string zyh)
        {
            var sb = new StringBuilder();
            sb.Append(@"select a.*,b.xm,b.hljb from zy_brsmtz(nolock) a left join zy_brxxk b on a.zyh=b.zyh and a.OrganizeId=b.OrganizeId
where a.OrganizeId = @orgId and a.zt = '1'");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (kssj.HasValue)
            {
                sb.Append(" and a.rq >= @kssj");
                pars.Add(new SqlParameter("@kssj", kssj.Value.Date));
            }
            if (jssj.HasValue)
            {
                sb.Append(" and a.rq < @jssj");
                pars.Add(new SqlParameter("@jssj", jssj.Value.Date.AddDays(1)));
            }
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sb.Append(" and a.zyh like @searchZyh");
                pars.Add(new SqlParameter("@searchZyh", "%" + zyh.Trim() + "%"));
            }

            return this.FindList<InpatientVitalSignsDto>(sb.ToString(), pars.ToArray());
        }

        /// <summary>
        /// điều dưỡngTra cứu
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<InpatientVitalSignsEntity> GetValidList(string orgId, DateTime? kssj, DateTime? jssj, string zyh)
        {
            var sb = new StringBuilder();
            sb.Append(@"select * from zy_brsmtz(nolock) 
where OrganizeId = @orgId and zt = '1'");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (kssj.HasValue)
            {
                sb.Append(" and rq >= @kssj");
                pars.Add(new SqlParameter("@kssj", kssj.Value.Date));
            }
            if (jssj.HasValue)
            {
                sb.Append(" and rq < @jssj");
                pars.Add(new SqlParameter("@jssj", jssj.Value.Date.AddDays(1)));
            }
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sb.Append(" and zyh like @searchZyh");
                pars.Add(new SqlParameter("@searchZyh", "%" + zyh.Trim() + "%"));
            }

            //theoThời gianThứ tự
            sb.Append(" order by rq,sj");

            return this.FindList<InpatientVitalSignsEntity>(sb.ToString(), pars.ToArray());
        }

        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void SubmitForm(InpatientVitalSignsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable(p => p.zyh == entity.zyh && p.rq == entity.rq && p.sj == entity.sj && p.Id != keyValue && p.zt == "1").Any())
                {
                    throw new FailedException("InpatientVitalSigns_Repeated_Error", "trùng lặpnhập");
                }
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity.OrganizeId != entity.OrganizeId)
                {
                    throw new FailedException("Error", "Thao tácNgoại lệ");
                }
                //properties
                var ignoreProps = new List<string>() { "Id", "CreateTime", "CreatorCode" };
                entity.MapperTo(dbEntity, ignoreProps);
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                if (this.IQueryable(p => p.zyh == entity.zyh && p.rq == entity.rq && p.sj == entity.sj && p.zt == "1").Any())
                {
                    throw new FailedException("InpatientVitalSigns_Repeated_Error", "trùng lặpnhập");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void DeleteForm(string keyValue)
        {
            var dbEntity = this.FindEntity(keyValue);
            dbEntity.zt = "0";  //Vô hiệu
            dbEntity.Modify(keyValue);
            this.Update(dbEntity);
        }

        /*****************************************************************/
        /// <summary>
        /// Lấy dữ liệu cần thiết để vẽ phiếu đo ba chỉ số
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<TemperatureGraphData> GetTempratureData(string orgId, DateTime? kssj, DateTime? jssj, string zyh, int pagesize =6)
        {
            string sql = @"exec usp_tempraturedata @orgId=@orgId,@kssj=@kssj,@jssj=@jssj,@zyh=@zyh,@pagesize=@pagesize ";
            return FindList<TemperatureGraphData>(sql, new SqlParameter[] {
                new SqlParameter("orgId",orgId),
                new SqlParameter("kssj",kssj.ToDateString()),
                new SqlParameter("jssj",jssj.ToDateString()),
                new SqlParameter("zyh",zyh??""),
                new SqlParameter("pagesize",pagesize.ToString()),
            });
        }

    }
}