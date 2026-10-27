using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Domain.ValueObjects;
using Newtouch.EMR.Domain.BusinessObjects;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using Newtouch.EMR.Infrastructure;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SqlSugar;
using Newtouch.EMR.Repository;

namespace Newtouch.EMR.DomainServices
{
    public class NursingRecordDmnService : RepositoryBase<bl_hljl_ybEntity>, INursingRecordDmnService
    {

        

        public NursingRecordDmnService()
        {
        }
        /// <summary>
        /// 获取全部病人树信息
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public IList<InpWardPatTreeVO> GetPatTree(string orgId, string zyzt, string keyword)
        {
            string sql = @"select  distinct b.zyh,b.xm hzxm,b.wardCode bqCode,c.bqmc,b.sex,b.zybz,
CAST(FLOOR(DATEDIFF(DY, b.birth, GETDATE()) / 365.25) AS VARCHAR(5)) nl,convert(varchar(50),isnull(b.rqrq,getdate()),120) rqrq,convert(varchar(50),isnull(b.cqrq,getdate()),120) cqrq
from [Newtouch_CIS]..zy_brxxk b with(nolock)  
left join [NewtouchHIS_Base].[dbo].[V_S_xt_bq] c with(nolock) on c.bqcode=b.wardCode and c.OrganizeId=@orgId and c.zt='1'
                where 1=1 and b.zt='1' ";
            var par = new List<SugarParameter>();
            par.Add(new SugarParameter("@orgId", orgId));
            if (!string.IsNullOrWhiteSpace(zyzt))
            {
                switch (zyzt)
                {
                    case "zy"://在院：不是已出院，不是作废记录，不是取消入院
                        sql += " and b.zybz in (SELECT * FROM dbo.f_split(@zybz, ','))";
                        par.Add(new SugarParameter("@zybz", (int)EnumZYBZ.Bqz));
                        break;
                    case "cy"://出院：已出院状态
                        sql += " and b.zybz in (SELECT * FROM dbo.f_split(@zybz, ','))";
                        par.Add(new SugarParameter("@zybz", (int)EnumZYBZ.Ycy + "," + (int)EnumZYBZ.Djz));
                        break;
                }
            }
            else
            {
                sql += " and b.zybz<>'9'";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (zyh like @keyword or xm like @keyword)";
                par.Add(new SugarParameter("@keyword", "%" + keyword + "%"));
            }
            return this.FindList<InpWardPatTreeVO>(sql, par.ToArray());
        }
        public InpWardPatTreeVO GetPatList(string orgId, string zyh)
        {
            string sql = @"select  distinct b.zyh,b.xm hzxm,b.wardCode bqCode,c.bqmc,b.sex,b.zybz,
CAST(FLOOR(DATEDIFF(DY, b.birth, GETDATE()) / 365.25) AS VARCHAR(5)) nl,convert(varchar(50),isnull(b.rqrq,getdate()),120) rqrq,convert(varchar(50),isnull(b.cqrq,getdate()),120) cqrq
from [Newtouch_CIS]..zy_brxxk b with(nolock)  
left join [NewtouchHIS_Base].[dbo].[V_S_xt_bq] c with(nolock) on c.bqcode=b.wardCode and c.OrganizeId=@orgId and c.zt='1'
                where 1=1 and b.zt='1' ";
            var par = new List<SugarParameter>();
            par.Add(new SugarParameter("@orgId", orgId));
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sql += " and (zyh = @zyh)";
                par.Add(new SugarParameter("@zyh",zyh));
            }
            return this.FirstOrDefault<InpWardPatTreeVO>(sql, par.ToArray());
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(bl_hljl_ybEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable(p => p.zyh == entity.zyh && p.rq == entity.rq && p.sj == entity.sj && p.Id != keyValue && p.zt == "1").Any())
                {
                    throw new FailedException("InpatientVitalSigns_Repeated_Error", "Nhập trùng lặp");
                }
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity.OrganizeId != entity.OrganizeId)
                {
                    throw new FailedException("Error", "Thao tác bất thường");
                }
                //properties
                var ignoreProps = new List<string>() { "Id", "CreateTime", "CreatorCode" };
                entity.MapperTo(dbEntity, ignoreProps);
                this.Update(dbEntity);
            }
            else
            {
                if (this.IQueryable(p => p.zyh == entity.zyh && p.rq == entity.rq && p.sj == entity.sj && p.zt == "1").Any())
                {
                    throw new FailedException("InpatientVitalSigns_Repeated_Error", "Nhập trùng lặp");
                }
                this.Insert(entity);
            }
        }
        /// <summary>
        /// 护理分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<bl_hljl_ybEntity> GetPaginationList(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string zyh, string wardCode, string isShowDelete)
        {
            string sql = @"select * from bl_hljl_yb where createtime>=@kssj  and createtime<=@jssj and OrganizeId=@orgId and zt='1'";
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", orgId));
            pars.Add(new SugarParameter("@kssj", kssj.Value.ToString()));
            pars.Add(new SugarParameter("@jssj", jssj.Value.AddDays(1).ToString()));
            if (zyh!="")
            {
                sql += " and zyh=@zyh";
                pars.Add(new SugarParameter("@zyh", zyh.Trim()));
            }
            
            return this.FindList<bl_hljl_ybEntity>(sql, pars.ToArray());

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string keyValue)
        {
            var dbEntity = this.FindEntity(keyValue);
            dbEntity.zt = "0";  //无效
            this.Update(dbEntity);
        }
        public int SubmitSrl(string mc, string ml, string dw, string tj, string orgId, string zyh, string rq, string sj,string bllx,string user)
        {
            using (var db = new EmrTransaction().BeginTrans())
            {
                bl_hljl_srsclEntity srscl = new bl_hljl_srsclEntity();
                srscl.mc = mc;
                srscl.ml = ml;
                srscl.dw = dw;
                srscl.tj = tj;
                srscl.zyh = zyh;
                srscl.rq = rq;
                srscl.sj = sj;
                srscl.bllx = bllx; //0 一般 1 危重 2 手术
                srscl.srsclx = "0";//摄入量 0 摄出量 1
                srscl.OrganizeId = orgId;
                db.Insert(srscl);
                db.Commit();
            }
            return 0;
        }
        public int DeleteSrl(string orgId, string zyh, string rq, string sj, string bllx, string user)
        {
            string sql = @"update bl_hljl_srscl set zt='0',LastModifyTime=@date,LastModifierCode=@user where OrganizeId=@orgId and srsclx='0' and zyh=@zyh and rq=@rq and sj=@sj and bllx=@bllx";
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", orgId));
            pars.Add(new SugarParameter("@zyh", zyh));
            pars.Add(new SugarParameter("@rq", rq));
            pars.Add(new SugarParameter("@sj", sj));
            pars.Add(new SugarParameter("@bllx", bllx));
            pars.Add(new SugarParameter("@date", DateTime.Now));
            pars.Add(new SugarParameter("@user", user));
            this.ExecuteSqlCommand(sql, pars.ToArray());
            return 0;
        }

        public int DeleteScl(string orgId, string zyh, string rq, string sj, string bllx, string user)
        {
            string sql = @"update bl_hljl_srscl set zt='0',LastModifyTime=@date,LastModifierCode=@user where OrganizeId=@orgId and srsclx='1' and zyh=@zyh and rq=@rq and sj=@sj and bllx=@bllx";
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", orgId));
            pars.Add(new SugarParameter("@zyh", zyh));
            pars.Add(new SugarParameter("@rq", rq));
            pars.Add(new SugarParameter("@sj", sj));
            pars.Add(new SugarParameter("@bllx", bllx));
            pars.Add(new SugarParameter("@date", DateTime.Now));
            pars.Add(new SugarParameter("@user", user));
            this.ExecuteSqlCommand(sql, pars.ToArray());
            return 0;
        }
        public int SubmitScl(string mc, string ml, string dw, string ysxz, string orgId, string zyh, string rq, string sj, string bllx, string user)
        {
            using (var db = new EmrTransaction().BeginTrans())
            {
                bl_hljl_srsclEntity srscl = new bl_hljl_srsclEntity();
                srscl.mc = mc;
                srscl.ml = ml;
                srscl.dw = dw;
                srscl.ysxz = ysxz;
                srscl.zyh = zyh;
                srscl.rq = rq;
                srscl.sj = sj;
                srscl.bllx = bllx; //0 一般 1 危重 2 手术
                srscl.srsclx = "1";//摄入量 0 摄出量 1
                srscl.OrganizeId = orgId;
                db.Insert(srscl);
                db.Commit();
            }
            return 0;
        }
        public IList<bl_hljl_srsclEntity> GetSrlScl(string orgId, string zyh, string rq, string sj, string bllx)
        {
            string sql = @"select * from bl_hljl_srscl where OrganizeId=@orgId and zt='1' and zyh=@zyh and rq=@rq and sj=@sj and bllx=@bllx and srsclx='0'";
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", orgId));
            pars.Add(new SugarParameter("@zyh", zyh));
            pars.Add(new SugarParameter("@rq", rq));
            pars.Add(new SugarParameter("@sj", sj));
            pars.Add(new SugarParameter("@bllx", bllx));
            return this.FindList<bl_hljl_srsclEntity>(sql, pars.ToArray());

        }
        public IList<bl_hljl_srsclEntity> GetScl(string orgId, string zyh, string rq, string sj, string bllx)
        {
            string sql = @"select * from bl_hljl_srscl where OrganizeId=@orgId and zt='1' and zyh=@zyh and rq=@rq and sj=@sj and bllx=@bllx and srsclx='1'";
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", orgId));
            pars.Add(new SugarParameter("@zyh", zyh));
            pars.Add(new SugarParameter("@rq", rq));
            pars.Add(new SugarParameter("@sj", sj));
            pars.Add(new SugarParameter("@bllx", bllx));
            return this.FindList<bl_hljl_srsclEntity>(sql, pars.ToArray());
        }
        public patientInfoDto GetInfoByZyh(string zyh, string orgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SugarParameter>();
            sqlstr = sqlstr.Append(@"SELECT  zyh ,  
                        xx.blh,
                        xm ,
                        sex ,
                        CAST(FLOOR(DATEDIFF(DY, birth, GETDATE()) / 365.25) AS INT) age ,
                        brxzmc ,
                        bq.bqmc ,
                        cw.cwmc ,
                         CONVERT(DATETIME, xx.rqrq, 23) ryrq,
                        xx.cqrq,
                        xx.rqrq,
                        xx.zybz
                FROM    [Newtouch_CIS]..zy_brxxk xx
                                LEFT JOIN NewtouchHIS_Base..V_S_xt_bq bq ON bq.bqCode = xx.WardCode
                                                    AND bq.OrganizeId = xx.OrganizeId
                                                    AND bq.zt = '1'
                LEFT JOIN NewtouchHIS_Base..V_S_xt_cw cw ON cw.cwCode = xx.BedCode
                                                    AND cw.OrganizeId = xx.OrganizeId
                                                    AND cw.zt = '1'
            where xx.OrganizeId=@orgId and xx.zyh=@zyh and xx.zt='1'");
            par.Add(new SugarParameter("@orgId", orgId));
            par.Add(new SugarParameter("@zyh", zyh));
            return this.FirstOrDefault<patientInfoDto>(sqlstr.ToString(), par.ToArray());
        }
    }
}
