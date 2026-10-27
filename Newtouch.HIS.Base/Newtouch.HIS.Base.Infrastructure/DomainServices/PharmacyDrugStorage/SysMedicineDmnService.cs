using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;


namespace Newtouch.HIS.DomainServices
{
	/// <summary>
	/// Thông tin thuốc
	/// </summary>
	public class SysMedicineDmnService : DmnServiceBase, ISysMedicineDmnService
	{
		private readonly ISysMedicineRepo _sysMedicineRepository;
		private readonly ISysMedicinePropertyRepo _sysMedicinePropertyRepo;
		
		private readonly ISysMedicineBaseRepo _sysMedicineBaseRepository;
		private readonly ISysMedicinePropertyBaseRepo _sysMedicinePropertyBaseRepo;

		public SysMedicineDmnService( ISysMedicineRepo sysMedicineRepository
			, ISysMedicinePropertyRepo sysMedicinePropertyRepo, ISysMedicineBaseRepo sysMedicineBaseRepository
			, ISysMedicinePropertyBaseRepo sysMedicinePropertyBaseRepo)
			
		{
			this._sysMedicineRepository = sysMedicineRepository;
			this._sysMedicinePropertyRepo = sysMedicinePropertyRepo;
			this._sysMedicineBaseRepository = sysMedicineBaseRepository;
			this._sysMedicinePropertyBaseRepo = sysMedicinePropertyBaseRepo;
		}

		/// <summary>
		/// Lấy thông tin thuốc hệ thống thuộc tổ chức hiện tại
		/// </summary>
		/// <returns></returns>
		public IList<SysMedicineVO> GetPaginationList(string organizeId, Pagination pagination, string zt, string ypflCode,string keyword = null)
		{
			if (string.IsNullOrWhiteSpace(organizeId))
			{
				return null;
			}
			var sql = @"
            SELECT x.ypId, x.zxdw,x.ypCode,x.ypmc,x.spm,x.py,x.bzdw,x.mzcls,x.mzcldw,x.zycls,x.zycldw,
            x.djdw,x.lsj,x.pfj,x.zfbl,x.zfxz,x.dlCode,x.ycmc,x.zt,0 bgbz,x.ypbzdm,x.cxjje
			,x.nbdl,x.mzzybz,x.CreateTime,d.pzwh,
            d.yptssx,d.ypflCode,d.zlff,d.jzlx,d.mrbzq,d.sjap,d.zbbz,d.yl,d.yldw,d.zjtzsj
			,d.ghdw,d.ypcd,d.ypgg,d.ybdm,d.xnhybdm, d.gjybdm,c.dlmc sfdlMc, e.jxmc jxmc, f.ypflmc ypflMc, case when d.ybdm IS NULL then '2' when d.ybdm IS NOT NULL AND d.LastYBUploadTime IS NOT NULL AND d.LastYBUploadTime >= d.LastModifyTime then '1' else '0' end isSynch,d.gjybmc
            FROM xt_yp as x  
			left join xt_ypsx as d on x.ypId=d.ypId
			left join xt_sfdl c	on x.dlCode = c.dlCode and c.OrganizeId = @organizeId
			left join xt_ypjx e	on x.jx = e.jxCode
			left join xt_ypfl f	on d.ypflCode = f.ypflCode
			where x.OrganizeId = @organizeId and d.OrganizeId = @organizeId ";
			DbParameter[] par;
            if (!string.IsNullOrEmpty(zt)) {
                sql += " and x.zt=@zt";
            }
            if (!string.IsNullOrEmpty(ypflCode))
            {
                sql += " and d.ypflCode=@ypflCode";
            }
            if (!string.IsNullOrEmpty(keyword))
			{
				sql += @"
            and (x.ypCode like @keyword or x.ypmc like @keyword or d.gjybdm like @keyword or d.ybdm like @keyword or x.py like @keyword) ";
				par = new DbParameter[]
				{
					new SqlParameter("@organizeId", organizeId),
					new SqlParameter("@keyword",  "%" + (keyword??"") + "%"),
                    new SqlParameter("@zt", zt),
                    new SqlParameter("@ypflCode", ypflCode)
                };
			}
			else
			{
				par = new DbParameter[]
				{
					new SqlParameter("@organizeId", organizeId),
                    new SqlParameter("@zt", zt),
                    new SqlParameter("@ypflCode", ypflCode)
                };
			}

			return this.QueryWithPage<SysMedicineVO>(sql, pagination, par);
		}



		/// <summary>
		/// Lấy thông tin thuốc hệ thống thuộc tổ chức hiện tại
		/// </summary>
		/// <returns></returns>
		public IList<SysMedicineVO> GetPaginationListdm(string organizeId, Pagination pagination, string type, string keyword = null)
		{
			if (string.IsNullOrWhiteSpace(organizeId))
			{
				return null;
			}
			var sql = @"
            SELECT x.ypId, x.zxdw,x.ypCode,x.ypmc,x.spm,x.py,x.bzdw,x.mzcls,x.mzcldw,x.zycls,x.zycldw,
            x.djdw,x.lsj,x.pfj,x.zfbl,x.zfxz,x.dlCode,x.ycmc,x.zt,0 bgbz,x.ypbzdm,x.cxjje
			,x.nbdl,x.mzzybz,x.CreateTime,d.pzwh,
            d.yptssx,d.ypflCode,d.zlff,d.jzlx,d.mrbzq,d.sjap,d.zbbz,d.yl,d.yldw,d.zjtzsj
			,d.ghdw,d.ypcd,d.ypgg,d.ybdm,d.xnhybdm, d.gjybdm,c.dlmc sfdlMc, e.jxmc jxmc, f.ypflmc ypflMc, case when d.ybdm IS NULL then '2' when d.ybdm IS NOT NULL AND d.LastYBUploadTime IS NOT NULL AND d.LastYBUploadTime >= d.LastModifyTime then '1' else '0' end isSynch,d.gjybmc
            FROM xt_yp as x  
			left join xt_ypsx as d on x.ypId=d.ypId
			left join xt_sfdl c	on x.dlCode = c.dlCode and c.OrganizeId = @organizeId
			left join xt_ypjx e	on x.jx = e.jxCode
			left join xt_ypfl f	on d.ypflCode = f.ypflCode
			where x.OrganizeId = @organizeId and d.OrganizeId = @organizeId ";
			DbParameter[] par;
			if (!string.IsNullOrEmpty(keyword))
			{
				sql += @"
            and (x.ypCode like @keyword or x.ypmc like @keyword or d.gjybdm like @keyword or d.ybdm like @keyword or x.py like @keyword) ";
				if (type != "qb" && type == "yd")
				{
					sql += "  and d.gjybdm<>'0'";
				}
				else if (type != "qb" && type == "wd")
				{
					sql += "and d.gjybdm='0'";
				}
				par = new DbParameter[]
				{
					new SqlParameter("@organizeId", organizeId),
					new SqlParameter("@keyword",  "%" + (keyword??"") + "%")
				};
			}
			else
			{
				if (type != "qb" && type == "yd")
				{
					sql += "  and d.gjybdm<>'0'";
				}
				else if (type != "qb" && type == "wd")
				{
					sql += "and d.gjybdm='0'";
				}
				par = new DbParameter[]
				{
					new SqlParameter("@organizeId", organizeId)
				};
			}

			return this.QueryWithPage<SysMedicineVO>(sql, pagination, par);
		}
        /// Tra cứu đối chiếu danh mục
		/// </summary>
		/// <returns></returns>
		public IList<SysMedicineMldzxxVO> GetPaginationListMldzxx(string organizeId, Pagination pagination, string type, string keyword = null)
        {
            DbParameter[] par;
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return null;
            }
            var sql = @" ";
            if (type== "101")
            {
                sql = @"select 
mldz.id,
yp.ypcode code,
yp.ypmc name,
yp.ypgg gg,
yp.gjybdm gjybdm,
case isnull(mldz.isdz,'Chưa đối chiếu') when '1' then 'Đã đối chiếu' when '0' then 'Chưa đối chiếu' else 'Chưa đối chiếu' end isdz
 from V_S_xt_yp yp 
left join  xt_mldz mldz on yp.ypCode=mldz.code
 and yp.gjybdm=mldz.gjybdm and yp.OrganizeId =mldz.OrganizeId
 and  mldz.zt='1'
 where (yp.dlCode='01' or yp.dlCode='02')
 and yp.OrganizeId=@organizeId
 and yp.zt='1'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += @"
            and (yp.ypCode like @keyword or yp.ypmc like @keyword or yp.gjybdm like @keyword or yp.ybdm like @keyword or yp.py like @keyword) ";
                }
               
            }
            if (type == "102")
            {
                sql = @"select 
mldz.id,
yp.ypcode code,
yp.ypmc name,
yp.ypgg gg,
yp.gjybdm gjybdm,
case isnull(mldz.isdz,'Chưa đối chiếu') when '1' then 'Đã đối chiếu' when '0' then 'Chưa đối chiếu' else 'Chưa đối chiếu' end isdz
 from V_S_xt_yp yp 
left join  xt_mldz mldz on yp.ypCode=mldz.code
 and yp.gjybdm=mldz.gjybdm and yp.OrganizeId =mldz.OrganizeId
 and  mldz.zt='1'
 where  1=1
 and yp.dlCode='03'
 and yp.OrganizeId=@organizeId
 and yp.zt='1'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += @"
            and (yp.ypCode like @keyword or yp.ypmc like @keyword or yp.gjybdm like @keyword or yp.ybdm like @keyword or yp.py like @keyword) ";
                }

            }
            if (type == "301")
            {
                sql = @"  select 
mldz.id,
xm.sfxmCode code,
xm.sfxmmc name,
xm.gg gg,
xm.gjybdm gjybdm,
case isnull(mldz.isdz,'Chưa đối chiếu') when '1' then 'Đã đối chiếu' when '0' then 'Chưa đối chiếu' else 'Chưa đối chiếu' end isdz
 from V_S_xt_sfxm xm 
left join  xt_mldz mldz on xm.sfxmCode=mldz.code
 and xm.gjybdm=mldz.gjybdm and xm.OrganizeId =mldz.OrganizeId
 and  mldz.zt='1'
 where 1=1 
 and xm.sfdlCode='126'
 and xm.OrganizeId=@organizeId
 and xm.zt='1'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += @"
            and (xm.sfxmCode like @keyword or xm.sfxmmc like @keyword or xm.gjybdm like @keyword  or xm.py like @keyword) ";
                }

            }
            if (type == "201")
            {
                sql = @"  select 
mldz.id,
xm.sfxmCode code,
xm.sfxmmc name,
xm.gg gg,
xm.gjybdm gjybdm,
case isnull(mldz.isdz,'Chưa đối chiếu') when '1' then 'Đã đối chiếu' when '0' then 'Chưa đối chiếu' else 'Chưa đối chiếu' end isdz
 from V_S_xt_sfxm xm 
left join  xt_mldz mldz on xm.sfxmCode=mldz.code
 and xm.gjybdm=mldz.gjybdm and xm.OrganizeId =mldz.OrganizeId
 and  mldz.zt='1'
 where 1=1 
 and xm.sfdlCode!='126'
 and xm.OrganizeId=@organizeId
 and xm.zt='1'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += @"
            and (xm.sfxmCode like @keyword or xm.sfxmmc like @keyword or xm.gjybdm like @keyword  or xm.py like @keyword) ";
                }

            }
            par = new DbParameter[]
              {
                    new SqlParameter("@organizeId", organizeId),
                    new SqlParameter("@keyword",  "%" + (keyword??"") + "%")
              };
            return this.QueryWithPage<SysMedicineMldzxxVO>(sql, pagination, par);
        }
        /// <summary>
        /// Tra cứu thông tin thuốc BHYT
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        public IList<G_yb_ypxxVO> GetYpypxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh,string dataSource)
        {
	        string sql = "";
	        var pars = new List<SqlParameter>();
	        switch (dataSource)
	        {
		        case "1":
			    sql =  @"select top 200 * from (
select 
Mã thuốc ypdm
,Nguồn dữ liệu sjly
,Tên đăng ký zcmc
,Tên thương mại spmc
,注册剂型 zcjx
,实际剂型 sjjx
,注册规格 zcgg
,实际规格 sjgg
,Chất liệu bao bì bzcz
,Số lượng đóng gói nhỏ nhất zxbzsl
,Đơn vị bào chế nhỏ nhất zxzjdw
,Đơn vị đóng gói nhỏ nhất zxbzdw
,Doanh nghiệp dược ypqy
,上市药品持有人 shypcyr
,Số đăng ký pzwh
,原批准文号 ypzwh
,药品本位码 ypbwm
,分包装企业名称 fbzqymc
,Doanh nghiệp sản xuất scqy
,市场状态 sczt
,Tên thuốc BHYT ybypmc
,[2021版甲乙类] ybjyl
,Dạng bào chế BHYT ybjx
,Số hiệu bh
,Ghi chú bz
,Loại A/B jyl
,Ghi chú1 bz1
,[Ghi chú（特殊属性标识）] bztssxbs
,医保支付标准 ybzfbz
,pym
from dbo.gb_ypxx 
)a where 1=1 
";
			    if (!string.IsNullOrEmpty(gjybdm))
			    {
				    sql += "  and (ypdm like  '%'+@gjybdm+'%' or zcmc like '%'+@gjybdm+'%' or pzwh like '%'+@gjybdm+'%' or  ybypmc like '%'+@gjybdm+'%' or ypqy like '%'+@gjybdm+'%' or pym like '%'+@gjybdm+'%')";
				    pars.Add(new SqlParameter("@gjybdm", gjybdm));
			    }
			    if (!string.IsNullOrEmpty(pzwh))
			    {
				    sql += "  and pzwh like '%'+@pzwh+'%'";
				    pars.Add(new SqlParameter("@pzwh", pzwh));
			    }
			    if (!string.IsNullOrEmpty(ypmc))
			    {
				    sql += "  and scqy like '%'+@ypmc+'%'";
				    pars.Add(new SqlParameter("@ypmc", ypmc));
			    }   
		        break;
		        case "2":
			        sql =  @"select top 200 * from (
select 
MED_LIST_CODG ypdm
,null sjly
,REG_NAME zcmc
,DRUG_PRODNAME spmc
,REG_DOSFORM zcjx
,DRUG_DOSFORM sjjx
,REG_SPEC zcgg
,DRUG_SPEC sjgg
,PACMATL bzcz
, CONVERT(VARCHAR(255), MIN_PAC_CNT) AS zxbzsl
,MIN_PREPUNT zxzjdw
,MIN_PACUNT zxbzdw
,PRODENTP_NAME ypqy
,'' shypcyr
,APRVNO pzwh
,'' ypzwh
,DRUGSTDCODE ypbwm
,SUBPCK_FCTY fbzqymc
,PRODENTP_NAME scqy
,MKT_STAS sczt
,REG_NAME ybypmc
,'' ybjyl
,REG_DOSFORM ybjx
,'' bh
,MEMO bz
,'' jyl
,null bz1
,null bztssxbs
,null  ybzfbz
,null pym
from G_yb_wm_tcmpat_info_b
)a where 1=1 
";
			        if (!string.IsNullOrEmpty(gjybdm))
			        {
				        sql += "  and (ypdm like  '%'+@gjybdm+'%' or zcmc like '%'+@gjybdm+'%' or pzwh like '%'+@gjybdm+'%' or  ybypmc like '%'+@gjybdm+'%' or ypqy like '%'+@gjybdm+'%' or pym like '%'+@gjybdm+'%')";
				        pars.Add(new SqlParameter("@gjybdm", gjybdm));
			        }
			        if (!string.IsNullOrEmpty(pzwh))
			        {
				        sql += "  and pzwh like '%'+@pzwh+'%'";
				        pars.Add(new SqlParameter("@pzwh", pzwh));
			        }
			        if (!string.IsNullOrEmpty(ypmc))
			        {
				        sql += "  and scqy like '%'+@ypmc+'%'";
				        pars.Add(new SqlParameter("@ypmc", ypmc));
			        }   
			        break;
	        }
	        
				
				
			//if (!string.IsNullOrEmpty(py))
			//{
			//	sql += "  and Tên đăng ký like @ypmc";
			//	pars.Add(new SqlParameter("@ypmc", "%" + ypmc + "%"));
			//}
			return this.FindList<G_yb_ypxxVO>(sql, pars.ToArray());
		}

		/// <summary>
		/// Lưu đối mã BHYT thuốc
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		public int SaveYpYb(G_yb_ypxxVO ybxx, int? ypid, string organizeId)
		{
			int updatecount = 0;
			if (ypid.HasValue && ypid.Value > 0)
			{
				using (var db = new DbTrans())
				{
					var ypEntity = db.IQueryable<SysMedicineEntity>().FirstOrDefault(p => p.ypId == ypid && p.OrganizeId== organizeId);
					var ypsxEntity = db.IQueryable<SysMedicinePropertyEntity>().FirstOrDefault(p => p.ypId == ypid && p.OrganizeId == organizeId);
					if (!string.IsNullOrEmpty(ybxx.ypdm) && !string.IsNullOrEmpty(ybxx.zcmc))
					{
						ypsxEntity.gjybdm = ybxx.ypdm;
						ypsxEntity.gjybmc = ybxx.zcmc;
					}
					if (!string.IsNullOrEmpty(ybxx.jyl))
					{
						ypsxEntity.ybbz = "1";//Tồn tại mã BHYT quốc gia Không xem thuộc tính bản thân thuốc Trực tiếp mặc định BHYT
						ypEntity.zfxz = EnumZFXZv2.J.GetDescription().Contains(ybxx.jyl) ? "4" : EnumZFXZv2.Y.GetDescription().Contains(ybxx.jyl) ? "5" : EnumZFXZv2.B.GetDescription().Contains(ybxx.jyl) ? "6" : "1";
					}

					db.Update(ypEntity);
					db.Update(ypsxEntity);

					updatecount = db.Commit();
				}

			}

			return updatecount;
		}

		/// <summary>
		/// Tra cứu thông tin vật tư hệ thống
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="pagination"></param>
		/// <param name="type"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public IList<SysMedicineVO> GetclxxList(string organizeId, Pagination pagination, string type, string keyword = null)
		{
			if (string.IsNullOrWhiteSpace(organizeId))
			{
				return null;
			}
			var sql = @" select x.*,d.gjybdm,d.gjybmc, d.ypgg,d.ypzsm from dbo.xt_yp x
          left join xt_ypsx as d on x.ypId=d.ypId
          where x.dlcode='18' and x.zt='1'  and x.organizeid=@organizeId  ";
			DbParameter[] par;
			if (!string.IsNullOrEmpty(keyword))
			{
				sql += " and x.ypmc like @keyword ";
				if (type != "qb" && type == "yd")
				{
					sql += "  and d.gjybdm is not null";
				}
				else if (type != "qb" && type == "wd")
				{
					sql += " and d.gjybdm is  null";
				}
				par = new DbParameter[]
				{
					new SqlParameter("@organizeId", organizeId),
					new SqlParameter("@keyword",  "%" + (keyword??"") + "%")
				};
			}
			else
			{
				if (type != "qb" && type == "yd")
				{
					sql += "  and gjybdm is not null";
				}
				else if (type != "qb" && type == "wd")
				{
					sql += " and gjybdm is  null";
				}
				par = new DbParameter[]
				{
					new SqlParameter("@organizeId", organizeId)
				};
			}

			return this.QueryWithPage<SysMedicineVO>(sql, pagination, par);
		}

		/// <summary>
		/// Tra cứu thông tin vật tư BHYT Theo thông tin vật tư hệ thống
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="ypmc"></param>
		/// <param name="py"></param>
		/// <param name="gjybdm"></param>
		/// <param name="pzwh"></param>
		/// <returns></returns>
		public IList<G_yb_clxxVO> GetYbclxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh,string dataSource)
		{
			var sql = "";
			var pars = new List<SqlParameter>();
			switch (dataSource)
			{
				case "1":
					sql = @"select top 200 * from(
select 
耗材代码 hcdm
,Phân loại cấp 1 yjfl
,Phân loại cấp 2 ejfl
,Phân loại cấp 3 sjfl
,单件产品名称 ybtym
,Chất liệu cz
,Đặc điểm tz
,Số đăng ký lưu hành zcbah
,单件产品名称 djcpmc
,Doanh nghiệp vật tư hcqy
,Số quy cách mẫu ggxhs
,Người đăng ký zcbar
,ybxz
,pym
from gb_clxx )a where 1=1 ";
					if (!string.IsNullOrEmpty(gjybdm))
					{
						sql +=
							"  and (hcdm like  '%'+@gjybdm+'%' or djcpmc like '%'+@gjybdm+'%' or zcbah like '%'+@gjybdm+'%' or  hcqy like '%'+@gjybdm+'%' or pym like '%'+@gjybdm+'%')";
						pars.Add(new SqlParameter("@gjybdm", gjybdm));
					}

					if (!string.IsNullOrEmpty(pzwh))
					{
						sql += "  and zcbah like '%'+@pzwh+'%'";
						pars.Add(new SqlParameter("@pzwh", pzwh));
					}

					if (!string.IsNullOrEmpty(ypmc))
					{
						sql += "  and hcqy like '%'+@ypmc+'%'";
						pars.Add(new SqlParameter("@ypmc", ypmc));
					}
					
					break;
				case "2":
					//Thư mục tải BHYT
					sql = @"select top 200 * from(
select 
MED_LIST_CODG hcdm
,'1' yjfl
,'1' ejfl
,'1'sjfl
,MCS_NAME ybtym
,MCS_MATL cz
,'' tz
,REG_FIL_NO zcbah
,MCS_NAME djcpmc
,PRODENTP_NAME hcqy
, CONVERT(int, PAC_CNT) AS zxbzsl
,REGER_NAME zcbar
,null ybxz
,'' pym
from G_yb_mcs_info_b )a where 1=1 ";
					if (!string.IsNullOrEmpty(gjybdm))
					{
						sql +=
							"  and (hcdm like  '%'+@gjybdm+'%' or djcpmc like '%'+@gjybdm+'%' or zcbah like '%'+@gjybdm+'%' or  hcqy like '%'+@gjybdm+'%' or pym like '%'+@gjybdm+'%')";
						pars.Add(new SqlParameter("@gjybdm", gjybdm));
					}

					if (!string.IsNullOrEmpty(pzwh))
					{
						sql += "  and zcbah like '%'+@pzwh+'%'";
						pars.Add(new SqlParameter("@pzwh", pzwh));
					}

					if (!string.IsNullOrEmpty(ypmc))
					{
						sql += "  and hcqy like '%'+@ypmc+'%'";
						pars.Add(new SqlParameter("@ypmc", ypmc));
					}
					
					break;
			}
			
			return this.FindList<G_yb_clxxVO>(sql, pars.ToArray());
		}
		/// <summary>
		/// Lưu đối mã vật tư
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		public int SaveYpcl(G_yb_clxxVO ybxx, int? ypid, string organizeId)
		{
			int updatecount = 0;
			if (ypid.HasValue && ypid.Value > 0)
			{
				using (var db = new DbTrans())
				{
					var sfxmEntity = db.IQueryable<SysChargeItemEntity>().FirstOrDefault(p => p.sfxmId == ypid && p.OrganizeId == organizeId);
					
					if (!string.IsNullOrEmpty(ybxx.hcdm) && !string.IsNullOrEmpty(ybxx.djcpmc))
					{
						sfxmEntity.gjybdm = ybxx.hcdm;
						sfxmEntity.gjybmc = ybxx.djcpmc;
					}
					if (!string.IsNullOrEmpty(ybxx.ybxz))
					{
						sfxmEntity.ybbz = "1";
						sfxmEntity.zfxz= EnumZFXZv2.J.GetDescription().Contains(ybxx.ybxz) ? "4" : EnumZFXZv2.Y.GetDescription().Contains(ybxx.ybxz) ? "5" : EnumZFXZv2.B.GetDescription().Contains(ybxx.ybxz) ? "6" : "1";
					}
					if(!string.IsNullOrEmpty(ybxx.zcbah))
					{
						sfxmEntity.pzwh = ybxx.zcbah;
					}
					

					db.Update(sfxmEntity);

					updatecount = db.Commit();
				}

			}

			return updatecount;
		}


		/// <summary>
		/// Tra cứu thông tin hạng mục hệ thống
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="pagination"></param>
		/// <param name="type"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public IList<SysChargeItemEntity> GetxmxxList(string organizeId, Pagination pagination, string type, string keyword = null)
		{
			if (string.IsNullOrWhiteSpace(organizeId))
			{
				return null;
			}
			var sql = @" select * from dbo.xt_sfxm 
             where sfdlcode!='126' and zt='1'  and organizeid=@organizeId  ";
			DbParameter[] par;
			if (!string.IsNullOrEmpty(keyword))
			{
				sql += " and sfxmmc like @keyword ";
				if (type != "qb" && type == "yd")
				{
					sql += "  and gjybdm is not null";
				}
				else if (type != "qb" && type == "wd")
				{
					sql += " and gjybdm is  null";
				}
				par = new DbParameter[]
				{
					new SqlParameter("@organizeId", organizeId),
					new SqlParameter("@keyword",  "%" + (keyword??"") + "%")
				};
			}
			else
			{
				if (type != "qb" && type == "yd")
				{
					sql += "  and gjybdm is not null";
				}
				else if (type != "qb" && type == "wd")
				{
					sql += " and gjybdm is  null";
				}
				par = new DbParameter[]
				{
					new SqlParameter("@organizeId", organizeId)
				};
			}

			return this.QueryWithPage<SysChargeItemEntity>(sql, pagination, par);
		}
		/// <summary>
		/// Tra cứu thông tin hạng mục BHYT Theo thông tin hạng mục hệ thống
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="ypmc"></param>
		/// <param name="py"></param>
		/// <param name="gjybdm"></param>
		/// <param name="pzwh"></param>
		/// <returns></returns>
		public IList<G_yb_xmxxVO> GetYbxmxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh,string dataSource)
		{
			string sql = "";
			var pars = new List<SqlParameter>();
			switch (dataSource)
			{
				case "1":
					sql = @"select top 200 * from(
select
Mã truyền quyết toán jscsbm
,Mã hạng mục quốc gia gjxmdm
,Tên hạng mục quốc gia gjxmmc
,Mã bm
,Tên hạng mục xmmc
,Nội hàm hạng mục xmnh
,Nội dung loại trừ cwnr
,Đơn vị tính giá jjdw
,Mô tả sm
,ybxz
,pym
from gb_sfxm)a where 1=1 ";
					// Khung nhìn gốc
					if (!string.IsNullOrEmpty(gjybdm))
					{
						sql +=
							" and(jscsbm like '%'+@gjybdm+'%' or gjxmdm like '%'+@gjybdm+'%' or bm like '%'+@gjybdm+'%' or gjxmmc like '%'+@gjybdm+'%' or xmmc like '%'+@gjybdm+'%' or pym like '%'+@gjybdm+'%')";
						pars.Add(new SqlParameter("@gjybdm", gjybdm));
					}

					break;

				case "2":
					// Danh mục BHYT
					sql = @"select top 200 * from(
select
YLML_CODE jscsbm
,'' gjxmdm
,YLFWXM_NAME gjxmmc
,YLML_CODE bm
,YLFWXM_NAME xmmc
,'' xmnh
,'' cwnr
,JJDW jjdw
,JJSM sm
,null ybxz
,'' pym
from G_yb_ylfuxm_new_list_b)a where 1=1 ";
					if (!string.IsNullOrEmpty(gjybdm))
					{
						sql +=
							" and(jscsbm like '%'+@gjybdm+'%' or gjxmdm like '%'+@gjybdm+'%' or bm like '%'+@gjybdm+'%' or gjxmmc like '%'+@gjybdm+'%' or xmmc like '%'+@gjybdm+'%' or pym like '%'+@gjybdm+'%')";
						pars.Add(new SqlParameter("@gjybdm", gjybdm));
					}
					break;
			}

			return this.FindList<G_yb_xmxxVO>(sql, pars.ToArray());
		}

		/// <summary>
		/// Lưu đối mã hạng mục
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		public int SaveYpxm(G_yb_xmxxVO ybxx, int? ypid, string organizeId)
		{
			int updatecount = 0;
			if (ypid.HasValue && ypid.Value > 0)
			{
				using (var db = new DbTrans())
				{
					var sfxmEntity = db.IQueryable<SysChargeItemEntity>().FirstOrDefault(p => p.sfxmId == ypid && p.OrganizeId == organizeId);

					if (!string.IsNullOrEmpty(ybxx.jscsbm) && !string.IsNullOrEmpty(ybxx.gjxmmc))
					{
						sfxmEntity.gjybdm = ybxx.jscsbm;
						sfxmEntity.gjybmc = ybxx.gjxmmc;
					}
					if (!string.IsNullOrEmpty(ybxx.ybxz))
					{
						sfxmEntity.ybbz = "1";
						sfxmEntity.zfxz = EnumZFXZv2.J.GetDescription().Contains(ybxx.ybxz) ? "4" : EnumZFXZv2.Y.GetDescription().Contains(ybxx.ybxz) ? "5" : EnumZFXZv2.B.GetDescription().Contains(ybxx.ybxz) ? "6" : "1";
					}

					db.Update(sfxmEntity);

					updatecount = db.Commit();
				}

			}

			return updatecount;
		}

		/// <summary>
		/// Lấy thông tin nhân viên hệ thống
		/// </summary>
		/// <param name="keyValue"></param>
		/// <returns></returns>
		public SysMedicineVO GetFormJson(int keyValue)
        {
            const string sql = @"
SELECT TOP 1000 x.ypId, x.zxdw,x.ypCode,x.ypmc,x.spm,x.py,x.bzdw,x.mzcls,x.mzcldw,x.zycls,x.zycldw,x.cxjje,
x.djdw,Convert(decimal(18,4),x.lsj) lsj,Convert(decimal(18,4),x.pfj) pfj,x.zfbl,x.zfxz,x.dlCode,x.ycmc,x.zt,0 bgbz,x.ypbzdm,x.nbdl,x.mzzybz,x.CreateTime,d.pzwh,
d.yptssx,d.ypflCode,d.zlff,d.jzlx,d.mrbzq,d.sjap,d.zbbz,d.yl,d.yldw,d.zjtzsj,d.ghdw,d.ypcd,d.ypgg,d.ybdm,d.xnhybdm,
x.jx,x.jl,x.jldw,x.OrganizeId,x.bzs,x.CreatorCode, c.dlmc sfdlMc, e.jxmc jxmc, f.ypflmc ypflMc ,d.mrjl,d.mrpc,
x.isKss,x.kssId,x.jybz,d.ybbz,x.bz,d.gjybdm,x.tsypbz,d.gjybmc,d.dcxl,d.mbxl,d.mryf,g.yfmc mryfmc,d.ybgg ybgg,kcyjz
FROM xt_yp(NOLOCK) x  
LEFT JOIN xt_ypsx(NOLOCK) d on x.ypId=d.ypId 
LEFT JOIN xt_sfdl(NOLOCK) c ON x.dlCode = c.dlCode and c.OrganizeId = x.OrganizeId
LEFT JOIN xt_ypjx(NOLOCK) e ON x.jx = e.jxCode
LEFT JOIN xt_ypfl(NOLOCK) f ON d.ypflCode = f.ypflCode
LEFT JOIN xt_ypyf(NOLOCK) g ON g.yfCode=d.mryf
WHERE x.ypId =@ypId
";
            DbParameter[] par = {
                new SqlParameter("@ypId", keyValue)
            };
            return FirstOrDefault<SysMedicineVO>(sql, par);
        }

        /// <summary>
        /// Lấy thông tin nhân viên hệ thống
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public SysMedicineVO SelectMedicineInfo(string ypCode, string organizeId)
        {
            const string sql = @"
SELECT TOP 1000 x.ypId, x.zxdw,x.ypCode,x.ypmc,x.spm,x.py,x.bzdw,x.mzcls,x.mzcldw,x.zycls,x.zycldw,
x.djdw,x.lsj,x.pfj,x.zfbl,x.zfxz,x.dlCode,x.ycmc,x.zt,0 bgbz,x.ypbzdm,x.nbdl,x.mzzybz,x.CreateTime,d.pzwh,
d.yptssx,d.ypflCode,d.zlff,d.jzlx,d.mrbzq,d.sjap,d.zbbz,d.yl,d.yldw,d.zjtzsj,d.ghdw,d.ypcd,d.ypgg,d.ybdm,d.xnhybdm,
x.jx,x.jl,x.jldw,x.OrganizeId,x.bzs,x.CreatorCode, c.dlmc sfdlMc, e.jxmc jxmc, f.ypflmc ypflMc ,d.mrjl,d.mrpc,
x.isKss,x.kssId,x.jybz,d.ybbz 
FROM xt_yp(NOLOCK) x  
LEFT JOIN xt_ypsx(NOLOCK) d on x.ypId=d.ypId 
LEFT JOIN xt_sfdl(NOLOCK) c ON x.dlCode = c.dlCode and c.OrganizeId = x.OrganizeId AND c.zt='1'
LEFT JOIN xt_ypjx(NOLOCK) e ON x.jx = e.jxCode AND e.zt='1' 
LEFT JOIN xt_ypfl(NOLOCK) f ON d.ypflCode = f.ypflCode AND f.zt='1' 
WHERE x.ypCode =@ypCode
AND x.OrganizeId=@OrganizeId
";
            DbParameter[] par = {
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FirstOrDefault<SysMedicineVO>(sql, par);
        }

        /// <summary>
        /// Thêm thông tin thuốc và bảng thông tin quen thuộc thuốc
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ypId"></param>
        public void SubmitMedicine(SysMedicineVO model, int? ypId)
        {
            if (ypId.HasValue && ypId.Value > 0)
            {
	            if (!model.OrganizeId.Equals("*"))
	            { model.ypId = ypId.Value;
		            //Cập nhật
		            using (var db = new DbTrans())
		            {
			            var ypEntity = db.IQueryable<SysMedicineEntity>().FirstOrDefault(p => p.ypId == model.ypId);
			            var ypsxEntity = db.IQueryable<SysMedicinePropertyEntity>().FirstOrDefault(p => p.ypId == model.ypId);
			            ypEntity.pfj = model.pfj;
			            var oldypEntity = ypEntity.Clone();
			            var oldypsxEntity = ypsxEntity.Clone();

			            model.OrganizeId = ypEntity.OrganizeId;//Tổ chức Cơ sở Không thể đổi
			            UpdateEnityProperties(ypEntity, ypsxEntity, model);

			            ypEntity.Modify();
			            ypsxEntity.Modify();

			            db.Update(ypEntity);
			            db.Update(ypsxEntity);

			            db.Commit();

			            AppLogger.WriteEntityChangeRecordLog(oldypEntity, ypEntity, "xt_yp", oldypEntity.ypId.ToString());
			            AppLogger.WriteEntityChangeRecordLog(oldypsxEntity, ypsxEntity, "xt_ypsx", oldypsxEntity.ypsxId.ToString());
		            }
		            
	            }
	            else
	            {
		            model.ypId = ypId.Value;
		            using (var db = new DbTrans())
		            {
			            //Cập nhật kho cơ sở hệ thống
			            var ypEntity = db.IQueryable<SysMedicineBaseEntity>().FirstOrDefault(p => p.ypId == ypId);
			            var ypsxEntity = db.IQueryable<SysMedicinePropertyBaseEntity>().FirstOrDefault(p => p.ypId == ypId);
			            ypEntity.pfj = model.pfj;
			            var oldypEntity = ypEntity.Clone();
			            var oldypsxEntity = ypsxEntity.Clone();
			            model.OrganizeId = ypEntity.OrganizeId;//Tổ chức Cơ sở Không thể đổi
			            UpdateEnityProperties(ypEntity,ypsxEntity, model);
			            ypEntity.Modify();
			            ypsxEntity.Modify();
			            _sysMedicineBaseRepository.Update(ypEntity);
			            _sysMedicinePropertyBaseRepo.Update(ypsxEntity);
			            db.Commit();
			            AppLogger.WriteEntityChangeRecordLog(oldypEntity, ypEntity, "xt_yp_base", oldypEntity.ypId.ToString());
			            AppLogger.WriteEntityChangeRecordLog(oldypsxEntity, ypsxEntity, "xt_ypsx_base", oldypsxEntity.ypsxId.ToString());
		            }
		            
	            }

            }
            else
            {
                //Thêm mới
                if (model.OrganizeId.Equals("*"))
                {
	                model.ypId = _sysMedicineBaseRepository.GetBaseLastYpId() + 1;
	                var ypEntity = new SysMedicineBaseEntity();
	                var ypsxEntity = new SysMedicinePropertyBaseEntity();
	                //Chèn thuốc vào kho cơ sở hệ thống
	                UpdateEnityBaseProperties(ypEntity, ypsxEntity, model);
	                ypEntity.Create(); ;
	                 _sysMedicineBaseRepository.Insert(ypEntity);
	                //Chèn thuộc tính thuốc
	                ypsxEntity.ypId = ypEntity.ypId;
	                ypsxEntity.Create();
	                _sysMedicinePropertyBaseRepo.Insert(ypsxEntity);
                }
                else
                {
	                model.ypId = 0;
	                var ypEntity = new SysMedicineEntity();
	                var ypsxEntity = new SysMedicinePropertyEntity();
	                //Chèn thuốc
	                UpdateEnityProperties(ypEntity, ypsxEntity, model);
	                ypEntity.Create();
	                _sysMedicineRepository.Insert(ypEntity);
	                //Chèn thuộc tính thuốc
	                ypsxEntity.ypId = ypEntity.ypId;
	                ypsxEntity.Create();
	                _sysMedicinePropertyRepo.Insert(ypsxEntity);
                }

               
            }
        }
        /// <summary>
        /// Đồng bộ BHYT sửa thời gian tải lên cuối cùng
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        public bool YibaoUpload(int ypId, out string error)
        {
            try
            {
                using (var db = new DbTrans())
                {
                    var ypsxEntity = db.IQueryable<SysMedicinePropertyEntity>().Where(p => p.ypId == ypId).FirstOrDefault();
                    if (ypsxEntity == null)
                    {
                        error = "HIS không tìm thấy thuốc này";
                        return false;
                    }
                    ypsxEntity.LastYBUploadTime = System.DateTime.Now;
                    ypsxEntity.Modify();
                    db.Update(ypsxEntity);
                    db.Commit();
                }
                error = "";
                return true;
            }
            catch
            {
                error = "HIS cập nhật thời gian đồng bộ BHYT của thuốc thất bại";
                return false;
            }
        }

        #region private methods

        /// <summary>
        /// Giá trị ghép
        /// </summary>
        /// <param name="ypEntity"></param>
        /// <param name="ypsxEntity"></param>
        /// <param name="model"></param>
        public void UpdateEnityProperties(SysMedicineEntity modelyp, SysMedicinePropertyEntity modelypsx, SysMedicineVO model)
        {
            #region Giá trị bảng thông tin thuốc
            modelyp.bzdw = model.bzdw;
            modelyp.bzs = model.bzs;
            modelyp.cfdw = "";//Mặc định
            modelyp.cfl = decimal.Parse("0.0000");
            //modelyp.cldw = model.cldw;
            //modelyp.cls = model.cls;
            modelyp.djdw = model.djdw;
            modelyp.dlCode = model.dlCode;
            modelyp.jl = model.jl;
            modelyp.jldw = model.jldw; ;
            modelyp.jx = model.jx;//( or liek ) and bgbz='0' and zt='1' Theo mã/Họ tên/Lấy thông tin dạng bào chế theo mã đầu chữ
            modelyp.lsj = model.lsj;
            modelyp.medextid = 0;//Mặc định
            modelyp.medid = 0;//Mặc định
            modelyp.mzcldw = model.mzcldw;
            modelyp.mzcls = model.mzcls;
            modelyp.mzzybz = "1";//Cờ ngoại trú/nội trú mặc định1
            modelyp.nbdl = model.nbdl;
            modelyp.pfj = model.pfj;
            modelyp.px = model.px;
            modelyp.py = model.py;
            modelyp.spm = model.spm;
            modelyp.OrganizeId = model.OrganizeId;    //Tổ chứcId
            modelyp.ycmc = model.ycmc;
            modelyp.ypbzdm = model.ypbzdm;//Mã đóng gói thuốc mặc định3
            modelyp.bz = model.bz;
            modelyp.cxjje = model.cxjje;//Số tiền vượt giá trần
            modelyp.ypId = model.ypId;
            modelyp.ypCode = model.ypCode;
            modelyp.ypmc = model.ypmc;
            modelyp.yphz = "";//Mặc định
            modelyp.ypqz = "";//Mặc định
            modelyp.zfbl = model.zfbl;
            modelyp.zfxz = model.zfxz;
            modelyp.zt = model.zt;
            modelyp.zycldw = model.zycldw;
            modelyp.zycls = model.zycls;
            modelyp.zxdw = model.zxdw;
            modelyp.isKss = model.isKss;
            modelyp.kssId = model.kssId;
            modelyp.jybz = model.jybz;
			modelyp.tsypbz = model.tsypbz;
			#endregion

			#region ThuốcSXGiá trị
			modelypsx.OrganizeId = model.OrganizeId;    //Tổ chứcId
            modelypsx.dczdjl = model.dczdjl;
            modelypsx.dczdsl = model.dczdsl;
            modelypsx.ghdw = model.ghdw;
            modelypsx.gzy = model.gzy;
            modelypsx.jsbz = model.jsbz;
            modelypsx.jzlx = model.jzlx;
            modelypsx.ljzdjl = model.ljzdjl;
            modelypsx.ljzdsl = model.ljzdsl;
            modelypsx.mrbzq = model.mrbzq;
            modelypsx.mzy = model.mzy;
            //modelypsx.px = model.px;
            modelypsx.pzwh = model.pzwh;
            modelypsx.shbz = model.shbz;
            modelypsx.sjap = model.sjap;
            modelypsx.syts = model.syts;
            modelypsx.tsbz = model.tsbz;
            modelypsx.xglx = model.xglx;
            modelypsx.ybdm = model.ybdm;
            modelypsx.xnhybdm = model.xnhybdm;
            modelypsx.yl = model.yl;
            modelypsx.yldw = model.yldw;
            modelypsx.yljsy = model.yljsy;
            modelypsx.ypcd = model.ypcd;
            modelypsx.ypCode = model.ypCode;
            modelypsx.ypflCode = model.ypflCode;
            modelypsx.ypgg = model.ypgg;
            modelypsx.ypId = model.ypId;
            modelypsx.yptssx = model.yptssx;
            modelypsx.zbbz = model.zbbz;
            modelypsx.zjtzsj = model.zjtzsj;
            modelypsx.zlff = model.zlff;
            modelypsx.mrjl = model.mrjl;
            modelypsx.mrpc = model.mrpc;
            modelypsx.ybbz = model.ybbz;
            modelypsx.gjybdm = model.gjybdm;
            modelypsx.gjybmc = model.gjybmc;
            modelypsx.dcxl = model.dcxl;
            modelypsx.mbxl = model.mbxl;
            modelypsx.mryf = model.mryf;
            modelypsx.ybgg = model.ybgg;
            modelypsx.kcyjz = model.kcyjz;
            //modelypsx.zt = model.zt;

            #endregion
        }
        
         public void UpdateEnityBaseProperties(SysMedicineBaseEntity modelyp, SysMedicinePropertyBaseEntity modelypsx, SysMedicineVO model)
        {
            #region Giá trị bảng thông tin thuốc
            modelyp.bzdw = model.bzdw;
            modelyp.bzs = model.bzs;
            modelyp.cfdw = "";//Mặc định
            modelyp.cfl = decimal.Parse("0.0000");
            //modelyp.cldw = model.cldw;
            //modelyp.cls = model.cls;
            modelyp.djdw = model.djdw;
            modelyp.dlCode = model.dlCode;
            modelyp.jl = model.jl;
            modelyp.jldw = model.jldw; ;
            modelyp.jx = model.jx;//( or liek ) and bgbz='0' and zt='1' Theo mã/Họ tên/Lấy thông tin dạng bào chế theo mã đầu chữ
            modelyp.lsj = model.lsj;
            modelyp.medextid = 0;//Mặc định
            modelyp.medid = 0;//Mặc định
            modelyp.mzcldw = model.mzcldw;
            modelyp.mzcls = model.mzcls;
            modelyp.mzzybz = "1";//Cờ ngoại trú/nội trú mặc định1
            modelyp.nbdl = model.nbdl;
            modelyp.pfj = model.pfj;
            modelyp.px = model.px;
            modelyp.py = model.py;
            modelyp.spm = model.spm;
            modelyp.OrganizeId = model.OrganizeId;    //Tổ chứcId
            modelyp.ycmc = model.ycmc;
            modelyp.ypbzdm = model.ypbzdm;//Mã đóng gói thuốc mặc định3
            modelyp.bz = model.bz;
            modelyp.cxjje = model.cxjje;//Số tiền vượt giá trần
            modelyp.ypId = model.ypId;
            modelyp.ypCode = model.ypCode;
            modelyp.ypmc = model.ypmc;
            modelyp.yphz = "";//Mặc định
            modelyp.ypqz = "";//Mặc định
            modelyp.zfbl = model.zfbl;
            modelyp.zfxz = model.zfxz;
            modelyp.zt = model.zt;
            modelyp.zycldw = model.zycldw;
            modelyp.zycls = model.zycls;
            modelyp.zxdw = model.zxdw;
            modelyp.isKss = model.isKss;
            modelyp.kssId = model.kssId;
            modelyp.jybz = model.jybz;
			modelyp.tsypbz = model.tsypbz;
			#endregion

			#region ThuốcSXGiá trị
			modelypsx.OrganizeId = model.OrganizeId;    //Tổ chứcId
            modelypsx.dczdjl = model.dczdjl;
            modelypsx.dczdsl = model.dczdsl;
            modelypsx.ghdw = model.ghdw;
            modelypsx.gzy = model.gzy;
            modelypsx.jsbz = model.jsbz;
            modelypsx.jzlx = model.jzlx;
            modelypsx.ljzdjl = model.ljzdjl;
            modelypsx.ljzdsl = model.ljzdsl;
            modelypsx.mrbzq = model.mrbzq;
            modelypsx.mzy = model.mzy;
            //modelypsx.px = model.px;
            modelypsx.pzwh = model.pzwh;
            modelypsx.shbz = model.shbz;
            modelypsx.sjap = model.sjap;
            modelypsx.syts = model.syts;
            modelypsx.tsbz = model.tsbz;
            modelypsx.xglx = model.xglx;
            modelypsx.ybdm = model.ybdm;
            modelypsx.xnhybdm = model.xnhybdm;
            modelypsx.yl = model.yl;
            modelypsx.yldw = model.yldw;
            modelypsx.yljsy = model.yljsy;
            modelypsx.ypcd = model.ypcd;
            modelypsx.ypCode = model.ypCode;
            modelypsx.ypflCode = model.ypflCode;
            modelypsx.ypgg = model.ypgg;
            modelypsx.ypId = model.ypId;
            modelypsx.yptssx = model.yptssx;
            modelypsx.zbbz = model.zbbz;
            modelypsx.zjtzsj = model.zjtzsj;
            modelypsx.zlff = model.zlff;
            modelypsx.mrjl = model.mrjl;
            modelypsx.mrpc = model.mrpc;
            modelypsx.ybbz = model.ybbz;
            modelypsx.gjybdm = model.gjybdm;
            modelypsx.gjybmc = model.gjybmc;
            modelypsx.dcxl = model.dcxl;
            modelypsx.mbxl = model.mbxl;
            modelypsx.mryf = model.mryf;
            modelypsx.ybgg = model.ybgg;
            //modelypsx.zt = model.zt;

            #endregion
        }
        
        

		#endregion

		/// <summary>
		/// Lấy hệ số thuốc
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetTradePricePlus(string name)
		{
			string sql = @"select top 1 code from [dbo].[Sys_ItemsDetail] where name=@name ";
			DbParameter[] par = {
				new SqlParameter("@name", name)
			};
			return FirstOrDefault<string>(sql, par);
		}


        /// <summary>
        /// Theo tra cứu cách dùng thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineFormulationEntity> GetValidList(string keyword = null)
        {
            var sql = @"select * from xt_ypjx(nolock) where zt = '1' and (jxCode like @searchKeyword or jxmc like @searchKeyword or py like @searchKeyword) order by py asc";
            return this.FindList<SysMedicineFormulationEntity>(sql, new SqlParameter[] {
                new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
            });
        }

        //Cách dùng thuốc
        public IList<SysMedicineUsageEntity> GetUsageFloat(string keyword)
        {
            var sql = @"select * from xt_ypyf(nolock) where zt = '1' and (yfCode like @searchKeyword or yfmc like @searchKeyword or py like @searchKeyword) order by py asc";
            return this.FindList<SysMedicineUsageEntity>(sql, new SqlParameter[] {
                new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
            });
        }
         public void UpdateEnityProperties(SysMedicineBaseEntity modelyp, SysMedicinePropertyBaseEntity modelypsx, SysMedicineVO model)
        {
            #region Giá trị bảng thông tin thuốc
            modelyp.bzdw = model.bzdw;
            modelyp.bzs = model.bzs;
            modelyp.cfdw = "";//Mặc định
            modelyp.cfl = decimal.Parse("0.0000");
            //modelyp.cldw = model.cldw;
            //modelyp.cls = model.cls;
            modelyp.djdw = model.djdw;
            modelyp.dlCode = model.dlCode;
            modelyp.jl = model.jl;
            modelyp.jldw = model.jldw; ;
            modelyp.jx = model.jx;//( or liek ) and bgbz='0' and zt='1' Theo mã/Họ tên/Lấy thông tin dạng bào chế theo mã đầu chữ
            modelyp.lsj = model.lsj;
            modelyp.medextid = 0;//Mặc định
            modelyp.medid = 0;//Mặc định
            modelyp.mzcldw = model.mzcldw;
            modelyp.mzcls = model.mzcls;
            modelyp.mzzybz = "1";//Cờ ngoại trú/nội trú mặc định1
            modelyp.nbdl = model.nbdl;
            modelyp.pfj = model.pfj;
            modelyp.px = model.px;
            modelyp.py = model.py;
            modelyp.spm = model.spm;
            modelyp.OrganizeId = model.OrganizeId;    //Tổ chứcId
            modelyp.ycmc = model.ycmc;
            modelyp.ypbzdm = model.ypbzdm;//Mã đóng gói thuốc mặc định3
            modelyp.bz = model.bz;
            modelyp.cxjje = model.cxjje;//Số tiền vượt giá trần
            modelyp.ypId = model.ypId;
            modelyp.ypCode = model.ypCode;
            modelyp.ypmc = model.ypmc;
            modelyp.yphz = "";//Mặc định
            modelyp.ypqz = "";//Mặc định
            modelyp.zfbl = model.zfbl;
            modelyp.zfxz = model.zfxz;
            modelyp.zt = model.zt;
            modelyp.zycldw = model.zycldw;
            modelyp.zycls = model.zycls;
            modelyp.zxdw = model.zxdw;
            modelyp.isKss = model.isKss;
            modelyp.kssId = model.kssId;
            modelyp.jybz = model.jybz;
			modelyp.tsypbz = model.tsypbz;
			#endregion

			#region ThuốcSXGiá trị
			modelypsx.OrganizeId = model.OrganizeId;    //Tổ chứcId
            modelypsx.dczdjl = model.dczdjl;
            modelypsx.dczdsl = model.dczdsl;
            modelypsx.ghdw = model.ghdw;
            modelypsx.gzy = model.gzy;
            modelypsx.jsbz = model.jsbz;
            modelypsx.jzlx = model.jzlx;
            modelypsx.ljzdjl = model.ljzdjl;
            modelypsx.ljzdsl = model.ljzdsl;
            modelypsx.mrbzq = model.mrbzq;
            modelypsx.mzy = model.mzy;
            //modelypsx.px = model.px;
            modelypsx.pzwh = model.pzwh;
            modelypsx.shbz = model.shbz;
            modelypsx.sjap = model.sjap;
            modelypsx.syts = model.syts;
            modelypsx.tsbz = model.tsbz;
            modelypsx.xglx = model.xglx;
            modelypsx.ybdm = model.ybdm;
            modelypsx.xnhybdm = model.xnhybdm;
            modelypsx.yl = model.yl;
            modelypsx.yldw = model.yldw;
            modelypsx.yljsy = model.yljsy;
            modelypsx.ypcd = model.ypcd;
            modelypsx.ypCode = model.ypCode;
            modelypsx.ypflCode = model.ypflCode;
            modelypsx.ypgg = model.ypgg;
            modelypsx.ypId = model.ypId;
            modelypsx.yptssx = model.yptssx;
            modelypsx.zbbz = model.zbbz;
            modelypsx.zjtzsj = model.zjtzsj;
            modelypsx.zlff = model.zlff;
            modelypsx.mrjl = model.mrjl;
            modelypsx.mrpc = model.mrpc;
            modelypsx.ybbz = model.ybbz;
            modelypsx.gjybdm = model.gjybdm;
            modelypsx.gjybmc = model.gjybmc;
            modelypsx.dcxl = model.dcxl;
            modelypsx.mbxl = model.mbxl;
            modelypsx.mryf = model.mryf;
            modelypsx.ybgg = model.ybgg;
            modelypsx.kcyjz = model.kcyjz;
            //modelypsx.zt = model.zt;

            #endregion
        }


    }
}
