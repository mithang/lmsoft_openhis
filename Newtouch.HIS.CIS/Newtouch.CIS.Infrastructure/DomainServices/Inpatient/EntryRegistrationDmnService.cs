

using Newtouch.CIS.Compat;
using Newtouch.Infrastructure.EF;
using Newtouch.Common.Web;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtouch.Common;
using Newtouch.Domain.IDomainServices.InterfaceSync;
using Newtouch.Domain.IRepository;

namespace Newtouch.DomainServices.Inpatient
{
    public class EntryRegistrationDmnService : DmnServiceBase, IEntryRegistrationDmnService
    {
        private readonly IBaseDataDmnService _baseDataDmnService;
        private readonly IHospInterfaceSyncDmnService _hospInterfaceSyncDmnService;
        private readonly IInpatientBedUseRecordRepo _inpatientBedUseRecordRepo;
        public EntryRegistrationDmnService() : base()
        { }
        /// <summary>
        /// Lấygiường ký tựthông tin(trangmặtHiển thị)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<BedInfoViewResponseVO> GetCwInfoViewList(BedInfoViewRequestVO req)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@" SELECT a.cwCode,a.cwmc,ISNULL(a.sfzy,0) sfzy,e.sfzh,b.bfNo,a.cwlx,d.zyh,e.xm,e.sex AS xb,
    ISNULL(f.Name,'') 'ysmc',e.brxzmc  ,d.[Status] cwsystu,ISNULL(yzcnt.cnt,0) cnt
  FROM NewtouchHIS_Base.dbo.V_S_xt_cw a   
  LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_bf b ON a.bfCode=b.bfCode AND a.OrganizeId=b.OrganizeId AND b.zt='1'
  LEFT JOIN [dbo].[zy_cwsyjlk] d ON a.cwCode=d.BedCode and a.bqCode=d.WardCode AND d.OrganizeId = a.OrganizeId 
            AND exists (SELECT 1 FROM (select Id,WardCode,BedCode,ROW_NUMBER() OVER(PARTITION BY BedCode ORDER BY createTime DESC) num 
                        FROM [dbo].[zy_cwsyjlk] WHERE zt='1'  AND OrganizeId=@OrganizeId AND Status in (1,2,3)) v WHERE v.num=1 and d.Id=v.Id and d.WardCode=v.WardCode)
  LEFT JOIN dbo.zy_brxxk e ON d.zyh=e.zyh AND d.OrganizeId= e.OrganizeId AND e.zt='1' and e.zybz not in ('9') 
  LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Staff f ON e.ysgh=f.gh AND e.OrganizeId=f.OrganizeId AND f.zt='1'
 LEFT JOIN (select count(1) cnt, zyh,OrganizeId from (
  select 'cq' yzxz,zyh,yzzt,yzlx,xmmc,OrganizeId from zy_cqyz where OrganizeId=@OrganizeId and yzzt='0' and  zt='1'
  union all
  select 'ls' yzxz,zyh,yzzt,yzlx,xmmc,OrganizeId  from zy_lsyz where OrganizeId=@OrganizeId and yzzt='0' and  zt='1'
  ) as yz
  group by zyh,OrganizeId) AS yzcnt on yzcnt.OrganizeId=d.OrganizeId and yzcnt.zyh=d.zyh
  WHERE  a.zt='1' ");
            if (req != null)
            {
                if (!string.IsNullOrWhiteSpace(req.OrganizeId))
                {
                    sqlstr.Append("  AND a.OrganizeId=@OrganizeId ");
                }
                if (!string.IsNullOrWhiteSpace(req.bqCode))
                {
                    sqlstr.Append("  AND a.bqCode=@bqCode");
                }
                if (!string.IsNullOrWhiteSpace(req.sfzy) && req.sfzy == "1")
                {
                    sqlstr.Append("  AND ISNULL(a.sfzy,0)='0'");
                }
            }
            sqlstr = sqlstr.Append(" order by cwmc, cwCode");
            par.Add(new SqlParameter("@OrganizeId", req.OrganizeId));
            par.Add(new SqlParameter("@bqCode", req.bqCode));
            return this.FindList<BedInfoViewResponseVO>(sqlstr.ToString(), par.ToArray());
        }
        public void SavePatInfo(InPatientDetailQueryDto patInfoRes)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                var patOldInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(p => p.zyh == patInfoRes.zyh && p.zt == "1" && p.OrganizeId == patInfoRes.OrganizeId).ToList().FirstOrDefault();
                if (patOldInfo != null)
                {
                    patOldInfo.zt = "0";
                    patOldInfo.Modify();
                    db.Update(patOldInfo);
                }

                var patInfoEntity = new InpatientPatientInfoEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    OrganizeId = patInfoRes.OrganizeId,
                    zyh = patInfoRes.zyh,
                    blh = patInfoRes.blh,
                    xm = patInfoRes.xm,
                    py = patInfoRes.py,
                    wb = patInfoRes.wb,
                    sfzh = patInfoRes.zjh,
                    sex = patInfoRes.sex,
                    birth = patInfoRes.csny,
                    zybz = patInfoRes.zybz,
                    DeptCode = patInfoRes.ks,
                    WardCode = patInfoRes.bqCode,
                    ysgh = patInfoRes.ys,
                    BedCode = patInfoRes.BedCode,
                    ryrq = patInfoRes.ryrq,
                    rqrq = patInfoRes.rqrq,
                    wzjb = patInfoRes.wzjb,
                    ryfs = patInfoRes.ryfs,
                    brxzdm = patInfoRes.brxz,
                    brxzmc = patInfoRes.brxzmc,
                    cardno = patInfoRes.idCardNo,
                    lxr = patInfoRes.contPerName,
                    lxrgx = patInfoRes.contPerRel,
                    lxrdh = patInfoRes.contPerPhoneNum,
                    zddm = patInfoRes.zzdCode,
                    zdmc = patInfoRes.zzdmc
                };
                //patInfoEntity.sfqj = false;
                //patInfoEntity.cqrq = patInfoRes.zyh;
                //patInfoEntity.hljb = patInfoRes.zyh;
                //patInfoEntity.cyfs = patInfoRes.zyh;
                //patInfoEntity.gdxmzxrq = patInfoRes.zyh;
                //patInfoEntity.cyzddm = patInfoRes.zyh;
                //patInfoEntity.cyzdmc = patInfoRes.zyh;
                patInfoEntity.Create();
                db.Insert(patInfoEntity);
                db.Commit();
            }
        }

        /// <summary>
        /// Sửanguytrọngcấploại
        /// </summary>
        /// <param name="patChangeAreaRequestDto"></param>
        /// <returns></returns>
        public string UpdatePatInfo(string zyh, string wzjb, string orgId)
        {
            try {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    var patInfoEntity = db.IQueryable<InpatientPatientInfoEntity>().Where(p => p.zyh == zyh && p.zt == "1" && p.OrganizeId == orgId).ToList().FirstOrDefault();
                    patInfoEntity.wzjb = wzjb;
                    patInfoEntity.Modify();
                    db.Update(patInfoEntity);

                    db.Commit();
                }
            }
            catch (Exception ex) {
                return "F|Thao tác thất bại：" + ex.Message;
            }
            return "T|Thao tác thành công";
        }

        public patBedInfoVO GetBedInfo(string cwCode, string OrganizeId,string bqCode = null)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@" SELECT a.OrganizeId,a.cwCode 'BedCode', a.cwmc 'BedNo',a.bqCode 'WardCode',d.bqmc 'WardName',c.bfCode 'RoomCode',c.bfNo 'RoomName' FROM NewtouchHIS_Base.dbo.V_S_xt_cw a 
                LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_bf c ON a.bfCode=c.bfCode AND a.OrganizeId=c.OrganizeId AND c.zt='1'
                LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_bq d ON a.bqCode=d.bqCode AND a.OrganizeId=c.OrganizeId AND d.zt='1'
                where a.OrganizeId=@OrganizeId AND a.zt='1' ");
            if (!string.IsNullOrWhiteSpace(cwCode))
            {
                sqlstr.Append("  AND a.cwCode=@cwCode ");
            }
            if (!string.IsNullOrWhiteSpace(bqCode))
            {
                sqlstr.Append("  AND a.bqCode=@bqCode ");
            }
            par.Add(new SqlParameter("@cwCode", cwCode));
            par.Add(new SqlParameter("@OrganizeId", OrganizeId));
            par.Add(new SqlParameter("@bqCode", bqCode??""));
            return this.FirstOrDefault<patBedInfoVO>(sqlstr.ToString(), par.ToArray());
        }
        /// <summary>
        /// LấyBệnh nhângiường ký tựSử dụngthông tin
        /// </summary>
        /// <param name="patInfoRes"></param>
        /// <returns></returns>
        public patBedSyjlInfoVO GetPatBedSyInfo(string zyh)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@" SELECT a.OrganizeId,a.blh,a.zyh,a.BedCode,b.cwmc 'BedNo',a.WardCode,d.bqmc 'WardName',c.bfCode 'RoomCode',c.bfNo 'RoomName',a.DeptCode,e.Name 'DeptName',a.rqrq 'OccBeginDate' FROM dbo.zy_brxxk a
                LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_cw b ON a.cwCode=b.cwCode AND a.OrganizeId=b.OrganizeId AND b.zt='1'
                LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_bf c ON b.bfCode=c.bfCode AND b.OrganizeId=c.OrganizeId AND c.zt='1'
                LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_bq d ON b.bqCode=d.bqCode AND b.OrganizeId=c.OrganizeId AND d.zt='1'
                LEFT JOIN NewtouchHIS_Base.[dbo].[V_S_Sys_Department] e ON a.OrganizeId=e.OrganizeId AND a.DeptCode=e.Code AND e.zt='1' where a.zt='1' ");
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sqlstr.Append("  AND a.zyh=@zyh ");
            }
            par.Add(new SqlParameter("@zyh", zyh));
            return this.FirstOrDefault<patBedSyjlInfoVO>(sqlstr.ToString(), par.ToArray());
        }
        /// <summary>
        /// 2020-6-2 chl modified chuyển khu đọclấygiường ký tựSử dụngghi chép
        /// </summary>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        public List<NewPatInfoVO> GetPatChangeArea(string bqdm)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"   SELECT a.zyh,a.blh,xm,CONVERT(VARCHAR(10),DATEDIFF(year,birth,GETDATE()))  nl,CASE WHEN sex=1 THEN 'Nam' WHEN sex=2 THEN 'Nữ' ELSE 'Khong ro' END xb,CONVERT(VARCHAR(10), ryrq,120)ryrq,zddm,brxzmc,ysgh,b.StaffName ysmc,a.BedCode lastcwCode,c.TransDeptCode DeptCode
            FROM Newtouch_CIS.dbo.zy_brxxk a 
            LEFT JOIN NewtouchHIS_Base..V_C_Sys_StaffDuty b ON a.ysgh=b.StaffGh AND a.OrganizeId=b.OrganizeId AND b.zt='1' 
            left join zy_cwsyjlk c on a.zyh=c.zyh and c.zt='1' and a.OrganizeId=c.OrganizeId
            WHERE  a.zt='1' AND a.zybz=@zybz ");
            if (!string.IsNullOrWhiteSpace(bqdm))
            {
                sqlstr.Append("  AND c.TransWardCode=@bqdm ");
            }
            par.Add(new SqlParameter("@bqdm", bqdm));
            par.Add(new SqlParameter("@zybz", Convert.ToInt32(EnumZYBZ.Zq)));
            return this.FindList<NewPatInfoVO>(sqlstr.ToString(), par.ToArray());
        }
        public void SavePatBedSYInfo(patBedSyjlInfoVO patBedSyInfo)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                InpatientPatientInfoDTO patInfo = new InpatientPatientInfoDTO()
                {
                    zybz = Convert.ToInt32(EnumZYBZ.Bqz),
                    zyh = patBedSyInfo.zyh,
                    cw = patBedSyInfo.BedCode
                };
                SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfo);

                #region Hủy hồ sơ sử dụng giường cũ của bệnh nhân này，Chỉ giữ lại một bản ghi mới nhất
                var patBedUsedList = db.IQueryable<InpatientBedUseRecordEntity>().Where(p => p.zyh == patBedSyInfo.zyh && p.OrganizeId == patBedSyInfo.OrganizeId && p.zt == "1").ToList();
                foreach (var item in patBedUsedList)
                {
                    item.zt = "0";
                    item.Modify();
                    db.Update(item);
                }
                #endregion

                InpatientBedUseRecordEntity bedEntity = new InpatientBedUseRecordEntity();
                bedEntity.Id = Guid.NewGuid().ToString();
                bedEntity.OrganizeId = patBedSyInfo.OrganizeId;
                bedEntity.blh = patBedSyInfo.blh;
                bedEntity.zyh = patBedSyInfo.zyh;
                bedEntity.BedCode = patBedSyInfo.BedCode;
                bedEntity.BedNo = patBedSyInfo.BedNo;
                bedEntity.WardCode = patBedSyInfo.WardCode;
                bedEntity.WardName = patBedSyInfo.WardName;
                bedEntity.RoomCode = patBedSyInfo.RoomCode;
                bedEntity.RoomName = patBedSyInfo.RoomName;
                bedEntity.DeptCode = patBedSyInfo.DeptCode;
                bedEntity.DeptName = patBedSyInfo.DeptName;
                bedEntity.Status = patBedSyInfo.Status;
                bedEntity.OccBeginDate = patBedSyInfo.OccBeginDate;

                bedEntity.Create();
                db.Insert(bedEntity);
                db.Commit();
            }
        }
        /// <summary>
        /// vàokhukhitínhphí
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string AddPatBedItemFee(patBedFeeRequestDto req)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@OrganizeId", req.OrganizeId));
            inParameters.Add(new SqlParameter("@zyh", req.zyh));
            inParameters.Add(new SqlParameter("@rq", req.rq));
            inParameters.Add(new SqlParameter("@rygh", req.user));
            var outParameter = new SqlParameter("@outMsg", System.Data.SqlDbType.VarChar, 300);
            outParameter.Direction = System.Data.ParameterDirection.Output;
            inParameters.Add(outParameter);

            ExecuteSqlCommand("exec usp_zy_PatInArea @zyh,@OrganizeId,@rq,@rygh,@outMsg out ", inParameters.ToArray());
            return outParameter.Value.ToString();
        }
        /// <summary>
        /// phúttrangLấythựcthểnội dungTheodòngmụcTra cứu
        /// </summary>
        /// <param name="pagination">phúttrang</param>
        /// <param name="keyword">Từ khóa</param>
        /// <param name="ksrq">Ngày bắt đầu</param>
        /// <param name="jsrq">Ngày kết thúc</param>
        /// <param name="bqdm">Khu bệnhMã</param>
        /// <returns></returns>
        public IList<OutAreapatientInfo> GetOutAreaPatlist(Pagination pagination, string keyword, DateTime? ksrq, DateTime? jsrq, string bqdm, string orgId)
        {

            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@" SELECT  zyxx.id , zyxx.zyh ,zyxx.xm ,zyxx.sex , CAST(FLOOR(DATEDIFF(DY, zyxx.birth, GETDATE()) / 365.25) AS VARCHAR(5)) nl , cw.cwmc ,zyxx.ryrq ,  zyxx.brxzmc, zyxx.zdmc zzdmc, staff.Name ysxm,zyxx.cqrq,zyxx.CreateTime,zyxx.CreatorCode 
FROM    dbo.zy_brxxk zyxx
        INNER JOIN NewtouchHIS_Base..V_S_xt_bq bq ON bq.bqCode = zyxx.WardCode
                                                     AND bq.OrganizeId = zyxx.OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_cw cw ON cw.bqCode = zyxx.WardCode
                                                    AND cw.cwCode = zyxx.BedCode
                                                    AND cw.OrganizeId = zyxx.OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = zyxx.ysgh
                                                           AND staff.OrganizeId = zyxx.OrganizeId
        WHERE 1 = 1 ");
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sqlStr.Append(" AND  zyxx.xm like @keyword");

            }
            if (ksrq.HasValue)
            {
                sqlStr.Append(" AND zyxx.cqrq >= @ksrq");
                parlist.Add(new SqlParameter("@ksrq", ksrq.Value));
            }
            if (jsrq.HasValue)
            {
                sqlStr.Append(" AND zyxx.cqrq < @jsrq");
                parlist.Add(new SqlParameter("@jsrq", jsrq.Value.AddDays(1).Date));
            }
            if (!string.IsNullOrWhiteSpace(bqdm))
            {
                sqlStr.Append(" AND  zyxx.WardCode = @bqdm");
                parlist.Add(new SqlParameter("@bqdm", bqdm.ToString()));
            }
            sqlStr.Append(" and zyxx.zt='1' and cw.zt='1'");
            sqlStr.Append(" and zyxx.zybz=@zybz");
            sqlStr.Append(" and zyxx.OrganizeId=@orgId");
            parlist.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            parlist.Add(new SqlParameter("@zybz", (int)EnumZYBZ.Djz));
            parlist.Add(new SqlParameter("@orgId", orgId));
            return this.QueryWithPage<OutAreapatientInfo>(sqlStr.ToString(), pagination, parlist.ToArray()).ToList();
            //return this.FindList<InPatientDetailQueryDto>(sqlStr.ToString(), parlist.ToArray());
        }

        /// <summary>
        /// bệnh nhânra khỏi khutriệuvề
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="bqdm">Khu bệnhMã</param>
        /// <returns></returns>
        public string SaveRecallOutArea(string zyh, string bqdm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(zyh))
                {
                    return "F|Số nhập việnkhông thểđể trống";
                }
                InpatientPatientInfoDTO patInfo = new InpatientPatientInfoDTO()
                {
                    zyh = zyh,
                    zybz = Convert.ToInt32(EnumZYBZ.Xry),
                    cyrq=null
                };
                //Cập nhậtBệnh nhânNhập việnTrạng thái
                var apiRespbr = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutRecallInfo", patInfo);

                AppLogger.Info(string.Format("ra khỏi khutriệuvềcập nhậtHISBệnh nhânNhập việnTrạng thái，Số nhập viện：{0}，kết quả：{1}、{2}", zyh, apiRespbr.code, apiRespbr.sub_code));

                if (apiRespbr.code != APIRequestHelper.ResponseResultCode.SUCCESS)
                {
                    return "F|gọihơnsửaĐang nằm việnTrạng tháigiao diệnthất bại";
                }

                SqlParameter[] para = new SqlParameter[] {
                            new SqlParameter("@zyh", zyh),
                            new SqlParameter("@bqdm", bqdm.ToString())
                };
                var sql = @" UPDATE zy_brxxk SET zybz=0,WardCode=@bqdm,cqrq=null,LastModifyTime=GETDATE() WHERE zt='1' And zybz=2 And zyh=@zyh ";

                this.ExecuteSqlCommand(sql, para);
                return "T|triệuvềthành công";
            }
            catch (Exception ex)
            {
                return "F|triệuvềthất bại：" + ex.Message;
            }

        }

        public string GetPatIsCancelInArea(patRequestDto req)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@OrganizeId", req.OrganizeId));
            inParameters.Add(new SqlParameter("@zyh", req.zyh));
            var outParameter = new SqlParameter("@outMsg", System.Data.SqlDbType.VarChar, 300);
            outParameter.Direction = System.Data.ParameterDirection.Output;
            inParameters.Add(outParameter);

            ExecuteSqlCommand("exec usp_zy_PatInAreaCancel @zyh,@OrganizeId,@outMsg out ", inParameters.ToArray());
            return outParameter.Value.ToString();
        }

        public string GetPatIsChangeArea(patRequestDto req)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@OrganizeId", req.OrganizeId));
            inParameters.Add(new SqlParameter("@zyh", req.zyh));
            inParameters.Add(new SqlParameter("@rygh", req.user));
            var outParameter = new SqlParameter("@outMsg", System.Data.SqlDbType.VarChar, 300);
            outParameter.Direction = System.Data.ParameterDirection.Output;
            inParameters.Add(outParameter);

            ExecuteSqlCommand("exec usp_zy_PatChangeArea @zyh,@OrganizeId,@rygh,@outMsg out ", inParameters.ToArray());
            return outParameter.Value.ToString();
        }
        

        /// <summary>
        /// ởKhu bệnhThông tin bệnh nhân
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        public IList<InAreapatientInfo> GetINAreaPatlist(Pagination pagination, string keyword, string bqdm, string orgId)
        {
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@" SELECT 
    zyxx.id , zyxx.zyh ,zyxx.xm ,zyxx.sex ,zyxx.brxzdm brxz
,CAST(FLOOR(DATEDIFF(DY, zyxx.birth, GETDATE()) / 365.25) AS VARCHAR(5)) nl 
,cw.cwmc ,zyxx.ryrq ,  zyxx.brxzmc, zyxx.zdmc zzdmc
,(select top 1 zdmc from zy_PatDxInfo cyzd where cyzd.zdlb=2 and cyzd.zt=1  and (cyzd.zdlx='1' or cyzd.zdlx='0') and cyzd.zyh=zyxx.zyh) cyzdmc
,staff.Name ysxm,zyxx.cqrq,zyxx.CreateTime,zyxx.CreatorCode
FROM    dbo.zy_brxxk zyxx
        INNER JOIN NewtouchHIS_Base..V_S_xt_bq bq ON bq.bqCode = zyxx.WardCode
                                                     AND bq.OrganizeId = zyxx.OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_cw cw ON cw.bqCode = zyxx.WardCode
                                                    AND cw.cwCode = zyxx.BedCode
                                                    AND cw.OrganizeId = zyxx.OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = zyxx.ysgh
                                                   AND staff.OrganizeId = zyxx.OrganizeId
        --LEFT JOIN zy_PatDxInfo cyzd on cyzd.zyh=zyxx.zyh and cyzd.zdlb=2 and cyzd.zdlx=0 and cyzd.zt=1 
        WHERE 1 = 1 and  zyxx.OrganizeId=@orgId ");
            parlist.Add(new SqlParameter("@orgId", orgId.ToString()));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sqlStr.Append(" AND  (zyxx.xm like @keyword or zyxx.zyh=@zyh or zyxx.py like @py)");
                parlist.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
                parlist.Add(new SqlParameter("@zyh", keyword.Trim()));
                parlist.Add(new SqlParameter("@py", "%" + keyword.Trim() + "%"));

            }
            if (!string.IsNullOrWhiteSpace(bqdm))
            {
                sqlStr.Append(" AND  zyxx.WardCode = @bqdm");
                parlist.Add(new SqlParameter("@bqdm", bqdm.ToString()));
            }
            sqlStr.Append(" and zyxx.zt='1' and cw.zt='1'");
            sqlStr.Append(" and (zyxx.zybz=@zybz");
            sqlStr.Append(" or zyxx.zybz=@zybz1 ) ");
            parlist.Add(new SqlParameter("@zybz", (int)EnumZYBZ.Bqz));
            parlist.Add(new SqlParameter("@zybz1", (int)EnumZYBZ.Djz));
            return this.QueryWithPage<InAreapatientInfo>(sqlStr.ToString(), pagination, parlist.ToArray()).ToList();
            //return this.FindList<InPatientDetailQueryDto>(sqlStr.ToString(), parlist.ToArray());
        }

        public bool ValidationBedIsUse(string OrganizeId, string cwCode, string bqCode)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                InpatientPatientInfoEntity patInfoEntity = db.IQueryable<InpatientPatientInfoEntity>(a => a.zybz == (int)EnumZYBZ.Bqz && a.zt == "1" && a.OrganizeId == OrganizeId && a.BedCode == cwCode && a.WardCode == bqCode).FirstOrDefault();

                if (patInfoEntity == null)
                {
                    return false;
                }
                return true;
            }

        }

        #region mượndùngthaygiao diện
        public string ChangeBed(patBedRequestDto reqDto)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                //LấyThông tin bệnh nhân,Cập nhậtgiường ký tựthông tin
                InpatientPatientInfoEntity patInfoEntity = db.IQueryable<InpatientPatientInfoEntity>(a => a.zyh == reqDto.zyh && a.zt == "1" && a.OrganizeId == reqDto.OrganizeId).FirstOrDefault();
                if (patInfoEntity == null)
                {
                    return "F|LấyThông tin bệnh nhânthất bại";
                }
                string dqcw = patInfoEntity.BedCode;
                string bq = patInfoEntity.WardCode;
                //Đăng kýchiếmdùngmớigiường ký tự
                var apiRespCwNew = _hospInterfaceSyncDmnService.UpdateOccupyByCode(reqDto.bedCode, bq, true, reqDto.OrganizeId, reqDto.user);
                if (apiRespCwNew.code == APIRequestHelper.ResponseResultCode.SUCCESS)
                {
                    var apiRespCwOld = _hospInterfaceSyncDmnService.UpdateOccupyByCode(dqcw, bq, false, reqDto.OrganizeId, reqDto.user);
                    if (apiRespCwOld.code != APIRequestHelper.ResponseResultCode.SUCCESS)
                    {
                        return "F|Hủyhiệncógiường ký tựchiếmdùngthất bại:" + apiRespCwOld.msg;
                    }
                }
                else
                {
                    return "F|Đăng kýgiường ký tựchiếmdùngthất bại:" + apiRespCwNew.msg;
                }
                
                InpatientPatientInfoDTO patInfo = new InpatientPatientInfoDTO()
                {
                    zybz = Convert.ToInt32(EnumZYBZ.Bqz),
                    zyh = reqDto.zyh,
                    cw = reqDto.bedCode
                };
                var apiRespbr = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfo);
                if (apiRespCwNew.code == APIRequestHelper.ResponseResultCode.SUCCESS && apiRespCwNew.code == APIRequestHelper.ResponseResultCode.SUCCESS)
                {
                    try
                    {
                        //Lấycái trướctờgiườngcủaSử dụngghi chépvàCập nhậtliên quanthông tin
                        InpatientBedUseRecordEntity bedUseEntity = db.IQueryable<InpatientBedUseRecordEntity>(b => b.zyh == reqDto.zyh && b.BedCode == patInfoEntity.BedCode && b.zt == "1" && b.OrganizeId == patInfoEntity.OrganizeId).FirstOrDefault();

                        //SửaThông tin bệnh nhânbảngtrongbedcode
                        patInfoEntity.BedCode = reqDto.bedCode;
                        //Sửalịch sửgiường ký tựSử dụngghi chépbảngtrongDữ liệu
                        bedUseEntity.OccEndDate = DateTime.Now;
                        bedUseEntity.TransBedCode = reqDto.bedCode;

                        patInfoEntity.Modify();
                        db.Update(patInfoEntity);
                        bedUseEntity.Modify();
                        db.Update(bedUseEntity);

                        //Chèngiường ký tựSử dụngghi chépbảng
                        patBedInfoVO patcwjl = GetBedInfo(reqDto.bedCode, reqDto.OrganizeId);
                        #region Hủy hồ sơ sử dụng giường cũ của bệnh nhân này，Chỉ giữ lại một bản ghi mới nhất
                        var patBedUsedList = db.IQueryable<InpatientBedUseRecordEntity>().Where(p => p.zyh == reqDto.zyh && p.OrganizeId == reqDto.OrganizeId && p.zt == "1").ToList();
                        foreach (var item in patBedUsedList)
                        {
                            item.zt = "0";
                            item.Modify();
                            db.Update(item);
                        }
                        #endregion

                        InpatientBedUseRecordEntity bedEntity = new InpatientBedUseRecordEntity();
                        bedEntity.Id = Guid.NewGuid().ToString();
                        bedEntity.OrganizeId = patcwjl.OrganizeId;
                        bedEntity.blh = patInfoEntity.blh;
                        bedEntity.zyh = patInfoEntity.zyh;
                        bedEntity.BedCode = patcwjl.BedCode;
                        bedEntity.BedNo = patcwjl.BedNo;
                        bedEntity.WardCode = patcwjl.WardCode;
                        bedEntity.WardName = patcwjl.WardName;
                        bedEntity.RoomCode = patcwjl.RoomCode;
                        bedEntity.RoomName = patcwjl.RoomName;
                        bedEntity.DeptCode = bedUseEntity.DeptCode;
                        bedEntity.DeptName = bedUseEntity.DeptName;
                        bedEntity.Status = Convert.ToInt32(EnumCwjlzt.Zc);//chuyểngiường
                        bedEntity.OccBeginDate = DateTime.Now;

                        bedEntity.Create();
                        db.Insert(bedEntity);

                        db.Commit();
                    }
                    catch (Exception ex)
                    {
                        _hospInterfaceSyncDmnService.UpdateOccupyByCode(reqDto.bedCode, bq, false, reqDto.OrganizeId, reqDto.user);
                        _hospInterfaceSyncDmnService.UpdateOccupyByCode(dqcw, bq, true, reqDto.OrganizeId, reqDto.user);                       
                        return "F|Thao tác thất bại：" + ex.Message;
                    }
                    return "T|Thao tác thành công";

                }
                else
                {
                    #region khôi phụccập nhậtgiường ký tựTrạng thái
                    _hospInterfaceSyncDmnService.UpdateOccupyByCode(reqDto.bedCode, bq, false, reqDto.OrganizeId, reqDto.user);
                    _hospInterfaceSyncDmnService.UpdateOccupyByCode(dqcw, bq, true, reqDto.OrganizeId, reqDto.user);

                    return "F|Gọi giao diện cập nhật thông tin giường thất bại";
                    #endregion
                }

            }
        }
        /// <summary>
        /// vàokhu
        /// </summary>
        /// <param name="inareaReq"></param>
        /// <returns></returns>
        public string SavePatInArea(patInAreaRequestDto inareaReq)
        {
            var patInfo = new InpatientPatientInfoDTO
            {
                zyh = inareaReq.zyh,
                zybz = Convert.ToInt32(EnumZYBZ.Bqz),
                cw = inareaReq.cwCode,
                doctor = inareaReq.ysgh
            };
            var reqObj = new
            {
                TimeStamp = DateTime.Now.ToString(),
                lastUpdateTime = DateTime.Now.ToString(),
                zyh = inareaReq.zyh
            };
            //kéolấyvàokhuBệnh nhânchi tiếtthông tin
            var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<InPatientDetailQueryDto>>("/api/patient/InPatientDetailQuery", reqObj);
            if (apiResp.data == null) return "F|Gọi giao diện lấy thông tin bệnh nhân thất bại";
            //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
            var apiRespCw = _hospInterfaceSyncDmnService.UpdateOccupyByCode(inareaReq.cwCode, inareaReq.bq, true, inareaReq.OrganizeId, inareaReq.user);
            if (apiRespCw.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                return "F|Đăng kýchiếmdùnggiường ký tựthất bại：" + apiRespCw.msg; 
            }

            //Cập nhậtBệnh nhânNhập việnTrạng thái
            var apiRespbr = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfo);
            if (apiRespbr != null && apiRespCw.code == APIRequestHelper.ResponseResultCode.SUCCESS && apiRespbr.code == APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                try
                {
                    var patInfoRes = apiResp.data;
                    patInfoRes.OrganizeId = inareaReq.OrganizeId;
                    patInfoRes.rqrq = Convert.ToDateTime(inareaReq.rq);
                    patInfoRes.wzjb = inareaReq.wzjb;
                    patInfoRes.BedCode = inareaReq.cwCode;
                    patInfoRes.zybz = Convert.ToInt32(EnumZYBZ.Bqz);
                    patInfoRes.bqCode = inareaReq.bq;

                    using (var db = new EFDbTransaction().BeginTrans())
                    {
                        var patOldInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(p => p.zyh == patInfoRes.zyh && p.zt == "1" && p.OrganizeId == patInfoRes.OrganizeId).ToList().FirstOrDefault();
                        var isNew = true;
                        if (patOldInfo != null)
                        {
                            if (patOldInfo.zybz != Convert.ToInt32(EnumZYBZ.Wry) && !string.IsNullOrEmpty(patOldInfo.cyzddm))//ra khỏi khutriệuvềBệnh nhân(xếptrừHủyNhập việnBệnh nhân)
                            {
                                patOldInfo.rqrq = Convert.ToDateTime(inareaReq.rq);
                                patOldInfo.wzjb = inareaReq.wzjb;
                                patOldInfo.BedCode = inareaReq.cwCode;
                                patOldInfo.WardCode = inareaReq.bq;
                                patOldInfo.zybz = Convert.ToInt32(EnumZYBZ.Bqz);
                                isNew = false;
                            }
                            else
                            {
                                patOldInfo.zt = "0";
                            }
                            patOldInfo.Modify();
                            db.Update(patOldInfo);
                        }
                        if (isNew)
                        {
                            #region ChènThông tin bệnh nhânbảng

                            var patInfoEntity = new InpatientPatientInfoEntity
                            {
                                Id = Guid.NewGuid().ToString(),
                                OrganizeId = patInfoRes.OrganizeId,
                                zyh = patInfoRes.zyh,
                                blh = patInfoRes.blh,
                                xm = patInfoRes.xm,
                                py = patInfoRes.py,
                                wb = patInfoRes.wb,
                                sfzh = patInfoRes.zjh,
                                sex = patInfoRes.sex,
                                birth = patInfoRes.csny,
                                zybz = patInfoRes.zybz,
                                DeptCode = patInfoRes.ks,
                                WardCode = patInfoRes.bqCode,
                                ysgh = inareaReq.ysgh,
                                BedCode = patInfoRes.BedCode,
                                ryrq = patInfoRes.ryrq,
                                rqrq = patInfoRes.rqrq,
                                wzjb = patInfoRes.wzjb,
                                ryfs = patInfoRes.ryfs,
                                brxzdm = patInfoRes.brxz,
                                brxzmc = patInfoRes.brxzmc,
                                cardno = patInfoRes.idCardNo,
                                lxr = patInfoRes.contPerName,
                                lxrgx = patInfoRes.contPerRel,
                                lxrdh = patInfoRes.contPerPhoneNum,
                                zddm = patInfoRes.zzdCode,
                                zdmc = patInfoRes.zzdmc,
                                grbh = patInfoRes.grbh
                            };
                            patInfoEntity.Create();
                            db.Insert(patInfoEntity);
                            #endregion
                        }
                        #region Chẩn đoán nhập việnChènChẩn đoánbảng
                        if (isNew && !string.IsNullOrWhiteSpace(patInfoRes.zzdCode))
                        {
                            var diaEntity = new InpatientPatientDiagnosisEntity
                            {
                                Id = Guid.NewGuid().ToString(),
                                OrganizeId = patInfoRes.OrganizeId,
                                zyh = patInfoRes.zyh,
                                zdlb = "1",
                                zdlx = "0",
                                zddm = patInfoRes.zzdCode,
                                zdmc = patInfoRes.zzdmc
                            };
                            diaEntity.Create();
                            db.Insert(diaEntity);
                        }

                        #endregion

                        #region Thêm thông tin bác sĩ nội trú
                        if (isNew && !string.IsNullOrWhiteSpace(inareaReq.ysgh))
                        {
                            var docEntity = new InpatientPatientDoctorEntity
                            {
                                Id = Guid.NewGuid().ToString(),
                                OrganizeId = inareaReq.OrganizeId,
                                zyh = inareaReq.zyh,
                                Type = Convert.ToInt32(EnumYslx.ZyDoc),
                                ysgh = inareaReq.ysgh,
                                ysmc = inareaReq.ysmc,
                                TypeName = "住院医生",
                                zt = "1"
                            };
                            docEntity.Create();
                            db.Insert(docEntity);
                        }
                        #endregion

                        #region Hủy hồ sơ sử dụng giường cũ của bệnh nhân này，Chỉ giữ lại một bản ghi mới nhất
                        var patBedUsedList = db.IQueryable<InpatientBedUseRecordEntity>().Where(p => p.zyh == patInfoRes.zyh && p.OrganizeId == inareaReq.OrganizeId && p.zt == "1").ToList();
                        foreach (var item in patBedUsedList)
                        {
                            item.zt = "0";
                            item.Modify();
                            db.Update(item);
                        }
                        #endregion

                        #region Chèngiường ký tựsử dụngghi chépbảng
                        var patBedSyInfo = GetBedInfo(inareaReq.cwCode, inareaReq.OrganizeId,inareaReq.bq);
                        var bedEntity = new InpatientBedUseRecordEntity
                        {
                            Id = Guid.NewGuid().ToString(),
                            OrganizeId = patBedSyInfo.OrganizeId,
                            blh = patInfoRes.blh,
                            zyh = patInfoRes.zyh,
                            BedCode = patBedSyInfo.BedCode,
                            BedNo = patBedSyInfo.BedNo,
                            WardCode = patBedSyInfo.WardCode,
                            WardName = patBedSyInfo.WardName,
                            RoomCode = patBedSyInfo.RoomCode,
                            RoomName = patBedSyInfo.RoomName,
                            DeptCode = patInfoRes.ks,
                            DeptName = patInfoRes.ksmc,
                            Status = Convert.ToInt32(EnumCwjlzt.Dq),
                            OccBeginDate = DateTime.Now
                        };
                        bedEntity.Create();
                        db.Insert(bedEntity);
                        #endregion
                        db.Commit();
                    }
                    return "T|Thao tác thành công";
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException; //lấytrongbộNgoại lệ
                    }
                    #region Giao diện khôi phục sau khi cập nhật thất bại
                    var BackpatInfo = new InpatientPatientInfoDTO
                    {
                        zyh = inareaReq.zyh,
                        zybz = Convert.ToInt32(EnumZYBZ.Xry)
                    };
                    //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                    _hospInterfaceSyncDmnService.UpdateOccupyByCode(inareaReq.cwCode, inareaReq.bq, false, inareaReq.OrganizeId, inareaReq.user);
                    //Cập nhậtBệnh nhânNhập việnTrạng thái
                    SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", BackpatInfo);
                    #endregion
                    return "F|Thao tác thất bại：" + ex.Message;
                }
            }

            {
                #region Giao diện khôi phục sau khi cập nhật thất bại
                var backpatInfo = new InpatientPatientInfoDTO
                {
                    zyh = inareaReq.zyh,
                    zybz = Convert.ToInt32(EnumZYBZ.Xry)
                };
                //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                _hospInterfaceSyncDmnService.UpdateOccupyByCode(inareaReq.cwCode, inareaReq.bq, false, inareaReq.OrganizeId, inareaReq.user);
                //Cập nhậtBệnh nhânNhập việnTrạng thái
                SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", backpatInfo);
                #endregion
                return "F|gọicập nhậtgiường hoặcĐang nằm việnTrạng tháigiao diệnthất bại";
            }

        }

        /// <summary>
        /// chuyển khuThông tin bệnh nhânSửa
        /// </summary>
        /// <param name="patChangeAreaRequestDto"></param>
        public string SaveChangeAreaPatInfo(patChangeAreaRequestDto patChangeAreaRequestDto)
        {
            var oldInfo = _inpatientBedUseRecordRepo.FindEntity(p => p.zyh == patChangeAreaRequestDto.zyh && p.OrganizeId == patChangeAreaRequestDto.OrganizeId && p.zt == "1" && p.BedCode==patChangeAreaRequestDto.lastcwCode);
            if (oldInfo == null)
            {
                return "F|Bệnh nhângiường ký tựsử dụngthông tinbất thường";
            }
            //chuẩnchuẩn bịCập nhậttrướcbrxx
            var patInfo = new InpatientPatientInfoDTO
            {
                zyh = patChangeAreaRequestDto.zyh,
                zybz = Convert.ToInt32(EnumZYBZ.Bqz),
                cw = patChangeAreaRequestDto.cwCode,
                doctor = patChangeAreaRequestDto.ysgh
            };
            var reqcwObj = new
            {
                TimeStamp = DateTime.Now.ToString(),
                cwCode = patChangeAreaRequestDto.cwCode,
                isOccupy = true
            };
            //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
            var apiRespCw = _hospInterfaceSyncDmnService.UpdateOccupyByCode(patChangeAreaRequestDto.cwCode, patChangeAreaRequestDto.WardCode, true, patChangeAreaRequestDto.OrganizeId, patChangeAreaRequestDto.user);
            if (apiRespCw.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                return "F|Đăng kýchiếmdùnggiường ký tựthất bại:" + apiRespCw.msg;
            }
            var apiRespbr = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfo);
            if (apiRespbr.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                return "F|Đồng bộHISThông tin bệnh nhângiao diệnthất bại";
            }
            #region Hủytrướcmộtbệnhgiườngchiếm ký tự
            var apiResplastCw = _hospInterfaceSyncDmnService.UpdateOccupyByCode(patChangeAreaRequestDto.lastcwCode, oldInfo.WardCode, false, patChangeAreaRequestDto.OrganizeId, patChangeAreaRequestDto.user);
            #endregion
            if (apiResplastCw.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                //nếunếuthất bạithìHủymớigiường ký tựchiếmdùng
                _hospInterfaceSyncDmnService.UpdateOccupyByCode(patChangeAreaRequestDto.cwCode, patChangeAreaRequestDto.WardCode, false, patChangeAreaRequestDto.OrganizeId, patChangeAreaRequestDto.user);
                return "F|cập nhậthiện tạigiường ký tựchiếmdùngTrạng tháithất bại";
            }
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    var patBedSyInfo = GetBedInfo(patChangeAreaRequestDto.cwCode, patChangeAreaRequestDto.OrganizeId,patChangeAreaRequestDto.WardCode);
                    var bedDept = _baseDataDmnService.GetDeptWardRel(patChangeAreaRequestDto.OrganizeId, null, patBedSyInfo.WardCode).FirstOrDefault();

                    var patInfoEntity = db.IQueryable<InpatientPatientInfoEntity>().Where(p => p.zyh == patChangeAreaRequestDto.zyh && p.zt == "1" && p.OrganizeId == patChangeAreaRequestDto.OrganizeId).ToList().FirstOrDefault();
                    patInfoEntity.zybz = Convert.ToInt32(EnumZYBZ.Bqz);
                    patInfoEntity.rqrq = patChangeAreaRequestDto.rqrq;
                    patInfoEntity.BedCode = patChangeAreaRequestDto.cwCode;
                    patInfoEntity.wzjb = patChangeAreaRequestDto.wzjb;
                    patInfoEntity.ysgh = patChangeAreaRequestDto.ysgh;
                    patInfoEntity.WardCode = patChangeAreaRequestDto.WardCode;
                    patInfoEntity.DeptCode = patChangeAreaRequestDto.DeptCode ?? bedDept.DepartmentCode;
                    patInfoEntity.Modify();
                    db.Update(patInfoEntity);

                    //Chèngiường ký tựSử dụngghi chépbảng

                    #region Hủy hồ sơ sử dụng giường cũ của bệnh nhân này，Chỉ giữ lại một bản ghi mới nhất
                    var patBedUsedList = db.IQueryable<InpatientBedUseRecordEntity>().Where(p => p.zyh == patChangeAreaRequestDto.zyh && p.OrganizeId == patBedSyInfo.OrganizeId && p.zt == "1").ToList();
                    foreach (var item in patBedUsedList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }
                    #endregion

                    #region Thêm thông tin bác sĩ nội trú
                    var patDocList = db.IQueryable<InpatientPatientDoctorEntity>().Where(p => p.zyh == patChangeAreaRequestDto.zyh && p.OrganizeId == patBedSyInfo.OrganizeId && p.zt == "1").ToList();
                    foreach (var item in patDocList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }
                    if (!string.IsNullOrWhiteSpace(patChangeAreaRequestDto.ysgh))
                    {
                        InpatientPatientDoctorEntity docEntity = new InpatientPatientDoctorEntity();
                        docEntity.Id = Guid.NewGuid().ToString();
                        docEntity.OrganizeId = patChangeAreaRequestDto.OrganizeId;
                        docEntity.zyh = patChangeAreaRequestDto.zyh;
                        docEntity.Type = Convert.ToInt32(EnumYslx.ZyDoc);
                        docEntity.ysgh = patChangeAreaRequestDto.ysgh;
                        docEntity.ysmc = patChangeAreaRequestDto.ysmc;
                        docEntity.TypeName = "住院医生";
                        docEntity.zt = "1";
                        docEntity.Create();
                        db.Insert(docEntity);
                    }
                    #endregion

                    #region Chèngiường ký tựsử dụngghi chép
                    InpatientBedUseRecordEntity bedEntity = new InpatientBedUseRecordEntity();
                    bedEntity.Id = Guid.NewGuid().ToString();
                    bedEntity.OrganizeId = patBedSyInfo.OrganizeId;
                    bedEntity.blh = patInfoEntity.blh;
                    bedEntity.zyh = patInfoEntity.zyh;
                    bedEntity.BedCode = patBedSyInfo.BedCode;
                    bedEntity.BedNo = patBedSyInfo.BedNo;
                    bedEntity.WardCode = patBedSyInfo.WardCode;
                    bedEntity.WardName = patBedSyInfo.WardName;
                    bedEntity.RoomCode = patBedSyInfo.RoomCode;
                    bedEntity.RoomName = patBedSyInfo.RoomName;
                    bedEntity.DeptCode = bedDept == null ? patInfoEntity.DeptCode : bedDept.DepartmentCode;
                    bedEntity.DeptName = bedDept == null ? null : bedDept.DepartmentName;
                    bedEntity.Status = Convert.ToInt32(EnumCwjlzt.Dq);
                    bedEntity.OccBeginDate = DateTime.Now;

                    bedEntity.Create();
                    db.Insert(bedEntity);
                    #endregion
                    db.Commit();
                }
            }
            catch (Exception ex)
            {
                var patBackInfo = new InpatientPatientInfoDTO()
                {
                    zyh = patChangeAreaRequestDto.zyh,
                    zybz = Convert.ToInt32(EnumZYBZ.Zq)
                };
                //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                _hospInterfaceSyncDmnService.UpdateOccupyByCode(patChangeAreaRequestDto.cwCode, patChangeAreaRequestDto.WardCode, false, patChangeAreaRequestDto.OrganizeId, patChangeAreaRequestDto.user);
                SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patBackInfo);
                return "F|Thao tác thất bại：" + ex.Message;
            }
            return "T|Thao tác thành công";
        }

        public string PatCancelInArea(patRequestDto req)
        {
            string cwCode = "";
            string zyh = "";
            //Lấynhấtgầnmột lầnbệnhgiườngghi chép
            InpatientBedUseRecordEntity bedUsedEntity = _inpatientBedUseRecordRepo.FindEntity(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId);

            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    //LấyThông tin bệnh nhân
                    InpatientPatientInfoEntity patInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();

                    if (patInfo == null || bedUsedEntity == null)
                    {
                        return "F|Lấy thông tin bệnh nhân hoặc hồ sơ sử dụng giường thất bại";
                    }
                    cwCode = patInfo.BedCode;
                    zyh = req.zyh;
                    #region Cập nhật trạng thái bệnh nhân của giường

                    //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                    var apiRespCw = _hospInterfaceSyncDmnService.UpdateOccupyByCode(cwCode, bedUsedEntity.WardCode, false, req.OrganizeId, req.user);
                    if (apiRespCw.code != APIRequestHelper.ResponseResultCode.SUCCESS)
                    {
                        return "F|Thu hồihiện tạigiường ký tựchiếmdùngTrạng tháithất bại：" + apiRespCw.msg;
                    }
                    InpatientPatientInfoDTO patInfoReq = new InpatientPatientInfoDTO()
                    {
                        zyh = zyh,
                        zybz = Convert.ToInt32(EnumZYBZ.Xry)
                    };
                    //Cập nhậtBệnh nhânNhập việnTrạng thái
                    var apiRespbr = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoReq);
                    #endregion
                    if (apiRespbr.code != APIRequestHelper.ResponseResultCode.SUCCESS)
                    {
                        //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                        _hospInterfaceSyncDmnService.UpdateOccupyByCode(cwCode, bedUsedEntity.WardCode, true, req.OrganizeId, req.user);

                        InpatientPatientInfoDTO patInfoBackReq = new InpatientPatientInfoDTO()
                        {
                            zyh = zyh,
                            zybz = Convert.ToInt32(EnumZYBZ.Bqz)
                        };
                        //Cập nhậtBệnh nhânNhập việnTrạng thái
                        SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoBackReq);
                        return "F|gọigiao diệncập nhậtgiường hoặcBệnh nhânTrạng tháithất bại";
                    }

                    #region Cập nhật bảng sử dụng giường bệnh
                    bedUsedEntity.OccEndDate = DateTime.Now;
                    bedUsedEntity.zt = "0";
                    bedUsedEntity.Modify();
                    db.Update(bedUsedEntity);

                    InpatientBedUseRecordEntity newbedUsedEntity = new InpatientBedUseRecordEntity();
                    newbedUsedEntity.Id = Guid.NewGuid().ToString();
                    newbedUsedEntity.OrganizeId = bedUsedEntity.OrganizeId;
                    newbedUsedEntity.blh = bedUsedEntity.blh;
                    newbedUsedEntity.zyh = bedUsedEntity.zyh;
                    newbedUsedEntity.BedCode = bedUsedEntity.BedCode;
                    newbedUsedEntity.BedNo = bedUsedEntity.BedNo;
                    newbedUsedEntity.WardCode = bedUsedEntity.WardCode;
                    newbedUsedEntity.RoomCode = bedUsedEntity.RoomCode;
                    newbedUsedEntity.zt = "1";
                    newbedUsedEntity.Status = Convert.ToInt32(EnumCwjlzt.QxRq);
                    newbedUsedEntity.Create();
                    db.Insert(newbedUsedEntity);
                    #endregion

                    #region Hủy thông tin chẩn đoán cũ
                    var patZDList = db.IQueryable<InpatientPatientDiagnosisEntity>().Where(p => p.zyh == req.zyh && p.OrganizeId == req.OrganizeId && p.zt == "1").ToList();
                    foreach (var item in patZDList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }
                    #endregion

                    #region Cập nhật bảng thông tin bệnh nhân
                    if (patInfo != null)
                    {
                        patInfo.ysgh = null;
                        patInfo.BedCode = null;
                        patInfo.rqrq = null;
                        patInfo.zybz = Convert.ToInt32(EnumZYBZ.Wry);
                        patInfo.Modify();
                        db.Update(patInfo);
                    }
                    #endregion

                    db.Commit();
                }
                return "T|Hủyvàokhuthành công";
            }
            catch (Exception ex)
            {
                InpatientPatientInfoDTO patInfoBackReq = new InpatientPatientInfoDTO()
                {
                    zyh = zyh,
                    zybz = Convert.ToInt32(EnumZYBZ.Bqz)
                };
                //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                _hospInterfaceSyncDmnService.UpdateOccupyByCode(cwCode, bedUsedEntity.WardCode, true, req.OrganizeId, req.user);

                //Cập nhậtBệnh nhânNhập việnTrạng thái
                SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoBackReq);
                return "F|Hủyvàokhuthất bại：" + ex.Message;
            }

        }

        public string SaveChangeArea(patRequestDto req, string bqdm)
        {
            string cwCode = "";
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    //LấyThông tin bệnh nhân
                    InpatientPatientInfoEntity patInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();
                    //Lấynhấtgầnmột lầnbệnhgiườngghi chép
                    InpatientBedUseRecordEntity bedUsedEntity = db.IQueryable<InpatientBedUseRecordEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();
                    if (patInfo == null || bedUsedEntity == null)
                    {
                        return "F|Lấy thông tin bệnh nhân hoặc hồ sơ sử dụng giường thất bại";
                    }
                    cwCode = patInfo.BedCode;

                    #region Hủy thông tin chẩn đoán cũ
                    var patZDList = db.IQueryable<InpatientPatientDiagnosisEntity>().Where(p => p.zyh == req.zyh && p.OrganizeId == req.OrganizeId && p.zt == "1" && p.zdlb == "2").ToList();
                    foreach (var item in patZDList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }
                    #endregion

                    #region hủy bỏlịch sửBác sĩthông tin
                    var patYSList = db.IQueryable<InpatientPatientDoctorEntity>().Where(p => p.zyh == req.zyh && p.OrganizeId == req.OrganizeId && p.zt == "1").ToList();
                    foreach (var item in patYSList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }
                    #endregion

                    #region Cập nhật bảng sử dụng giường bệnh
                    bedUsedEntity.OccEndDate = DateTime.Now;
                    bedUsedEntity.zt = "0";
                    bedUsedEntity.Modify();
                    db.Update(bedUsedEntity);

                    var relvo = _baseDataDmnService.GetDeptWardRel(bedUsedEntity.OrganizeId, "", bqdm).FirstOrDefault();

                    InpatientBedUseRecordEntity newbedUsedEntity = new InpatientBedUseRecordEntity();
                    newbedUsedEntity.Id = Guid.NewGuid().ToString();
                    newbedUsedEntity.OrganizeId = bedUsedEntity.OrganizeId;
                    newbedUsedEntity.blh = bedUsedEntity.blh;
                    newbedUsedEntity.zyh = bedUsedEntity.zyh;
                    newbedUsedEntity.BedCode = bedUsedEntity.BedCode;
                    newbedUsedEntity.BedNo = bedUsedEntity.BedNo;
                    newbedUsedEntity.WardCode = bedUsedEntity.WardCode;
                    newbedUsedEntity.WardName = bedUsedEntity.WardName;
                    newbedUsedEntity.RoomCode = bedUsedEntity.RoomCode;
                    newbedUsedEntity.RoomName = bedUsedEntity.RoomName;
                    newbedUsedEntity.DeptCode = bedUsedEntity.DeptCode;
                    newbedUsedEntity.DeptName = bedUsedEntity.DeptName;
                    newbedUsedEntity.TransWardCode = bqdm;
                    newbedUsedEntity.TransDeptCode = relvo == null ? null : relvo.DepartmentCode;
                    newbedUsedEntity.zt = "1";
                    newbedUsedEntity.Status = Convert.ToInt32(EnumCwjlzt.Zq);
                    newbedUsedEntity.Create();
                    db.Insert(newbedUsedEntity);
                    #endregion

                    #region Cập nhật bảng thông tin bệnh nhân
                    patInfo.ysgh = null;
                    //patInfo.BedCode = null;
                    patInfo.rqrq = null;
                    patInfo.zybz = Convert.ToInt32(EnumZYBZ.Zq);
                    //patInfo.WardCode = bqdm;
                    patInfo.Modify();
                    db.Update(patInfo);
                    #endregion
                    db.Commit();
                }
                return "T|chuyển khuthành công";
            }
            catch (Exception ex)
            {
                return "F|chuyển khuthất bại：" + ex.Message;
            }

        }

        #endregion

        #region bak
        public string ChangeBedV1(patBedRequestDto reqDto)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                //LấyThông tin bệnh nhân,Cập nhậtgiường ký tựthông tin
                InpatientPatientInfoEntity patInfoEntity = db.IQueryable<InpatientPatientInfoEntity>(a => a.zyh == reqDto.zyh && a.zt == "1" && a.OrganizeId == reqDto.OrganizeId).FirstOrDefault();
                if (patInfoEntity == null)
                {
                    return "F|LấyThông tin bệnh nhânthất bại";
                }
                var cwCode = patInfoEntity.BedCode;
                var reqcwNewObj = new
                {
                    TimeStamp = DateTime.Now.ToString(),
                    cwCode = reqDto.bedCode,
                    isOccupy = true
                };
                var reqcwOldObj = new
                {
                    TimeStamp = DateTime.Now.ToString(),
                    cwCode = cwCode,
                    isOccupy = false
                };
                InpatientPatientInfoDTO patInfo = new InpatientPatientInfoDTO()
                {
                    zybz = Convert.ToInt32(EnumZYBZ.Bqz),
                    zyh = reqDto.zyh,
                    cw = reqDto.bedCode
                };
                //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                var apiRespCwNew = SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                "/api/ward/UpdateBedOccupy", reqcwNewObj);
                //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                var apiRespCwOld = SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                "/api/ward/UpdateBedOccupy", reqcwOldObj);
                var apiRespbr = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfo);
                if (apiRespCwNew.code == APIRequestHelper.ResponseResultCode.SUCCESS && apiRespCwNew.code == APIRequestHelper.ResponseResultCode.SUCCESS)
                {
                    try
                    {
                        //Lấycái trướctờgiườngcủaSử dụngghi chépvàCập nhậtliên quanthông tin
                        InpatientBedUseRecordEntity bedUseEntity = db.IQueryable<InpatientBedUseRecordEntity>(b => b.zyh == reqDto.zyh && b.BedCode == patInfoEntity.BedCode && b.zt == "1" && b.OrganizeId == patInfoEntity.OrganizeId).FirstOrDefault();

                        //SửaThông tin bệnh nhânbảngtrongbedcode
                        patInfoEntity.BedCode = reqDto.bedCode;
                        //Sửalịch sửgiường ký tựSử dụngghi chépbảngtrongDữ liệu
                        bedUseEntity.OccEndDate = DateTime.Now;
                        bedUseEntity.TransBedCode = reqDto.bedCode;

                        patInfoEntity.Modify();
                        db.Update(patInfoEntity);
                        bedUseEntity.Modify();
                        db.Update(bedUseEntity);

                        //Chèngiường ký tựSử dụngghi chépbảng
                        patBedInfoVO patcwjl = GetBedInfo(reqDto.bedCode, reqDto.OrganizeId);
                        #region Hủy hồ sơ sử dụng giường cũ của bệnh nhân này，Chỉ giữ lại một bản ghi mới nhất
                        var patBedUsedList = db.IQueryable<InpatientBedUseRecordEntity>().Where(p => p.zyh == reqDto.zyh && p.OrganizeId == reqDto.OrganizeId && p.zt == "1").ToList();
                        foreach (var item in patBedUsedList)
                        {
                            item.zt = "0";
                            item.Modify();
                            db.Update(item);
                        }
                        #endregion

                        InpatientBedUseRecordEntity bedEntity = new InpatientBedUseRecordEntity();
                        bedEntity.Id = Guid.NewGuid().ToString();
                        bedEntity.OrganizeId = patcwjl.OrganizeId;
                        bedEntity.blh = patInfoEntity.blh;
                        bedEntity.zyh = patInfoEntity.zyh;
                        bedEntity.BedCode = patcwjl.BedCode;
                        bedEntity.BedNo = patcwjl.BedNo;
                        bedEntity.WardCode = patcwjl.WardCode;
                        bedEntity.WardName = patcwjl.WardName;
                        bedEntity.RoomCode = patcwjl.RoomCode;
                        bedEntity.RoomName = patcwjl.RoomName;
                        bedEntity.DeptCode = bedUseEntity.DeptCode;
                        bedEntity.DeptName = bedUseEntity.DeptName;
                        bedEntity.Status = Convert.ToInt32(EnumCwjlzt.Zc);//chuyểngiường
                        bedEntity.OccBeginDate = DateTime.Now;

                        bedEntity.Create();
                        db.Insert(bedEntity);

                        db.Commit();
                    }
                    catch (Exception ex)
                    {
                        var reqcwNewBackObj = new
                        {
                            TimeStamp = DateTime.Now.ToString(),
                            cwCode = reqDto.bedCode,
                            isOccupy = false
                        };
                        var reqcwOldBackObj = new
                        {
                            TimeStamp = DateTime.Now.ToString(),
                            cwCode = cwCode,
                            isOccupy = true
                        };
                        //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                        SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                        "/api/ward/UpdateBedOccupy", reqcwNewBackObj);
                        //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                        SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                        "/api/ward/UpdateBedOccupy", reqcwOldBackObj);
                        return "F|Thao tác thất bại：" + ex.Message;
                    }
                    return "T|Thao tác thành công";

                }
                else
                {
                    #region khôi phụccập nhậtgiường ký tựTrạng thái
                    var reqcwNewBackObj = new
                    {
                        TimeStamp = DateTime.Now.ToString(),
                        cwCode = reqDto.bedCode,
                        isOccupy = false
                    };
                    var reqcwOldBackObj = new
                    {
                        TimeStamp = DateTime.Now.ToString(),
                        cwCode = cwCode,
                        isOccupy = true
                    };
                    //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                    SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                    "/api/ward/UpdateBedOccupy", reqcwNewBackObj);
                    //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                    SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                    "/api/ward/UpdateBedOccupy", reqcwOldBackObj);
                    return "F|Gọi giao diện cập nhật thông tin giường thất bại";
                    #endregion
                }

            }
        }

        /// <summary>
        /// chuyển khuThông tin bệnh nhânSửa
        /// </summary>
        /// <param name="patChangeAreaRequestDto"></param>
        public string SaveChangeAreaPatInfoV1(patChangeAreaRequestDto patChangeAreaRequestDto)
        {
            var patInfo = new InpatientPatientInfoDTO
            {
                zyh = patChangeAreaRequestDto.zyh,
                zybz = Convert.ToInt32(EnumZYBZ.Bqz),
                cw = patChangeAreaRequestDto.cwCode,
                doctor = patChangeAreaRequestDto.ysgh
            };
            var reqcwObj = new
            {
                TimeStamp = DateTime.Now.ToString(),
                cwCode = patChangeAreaRequestDto.cwCode,
                isOccupy = true
            };
            //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
            var apiRespCw = SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/ward/UpdateBedOccupy", reqcwObj);
            var apiRespbr = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfo);
            if (apiRespCw.code != APIRequestHelper.ResponseResultCode.SUCCESS || apiRespbr.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                return "F|Gọi giao diện cập nhật thông tin giường thất bại";
            }
            #region Hủytrướcmộtbệnhgiườngchiếm ký tự
            var lastcwObj = new
            {
                TimeStamp = DateTime.Now.ToString(),
                cwCode = patChangeAreaRequestDto.lastcwCode,
                isOccupy = false
            };
            //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
            var apiResplastCw = SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
            "/api/ward/UpdateBedOccupy", lastcwObj);
            #endregion
            if (apiResplastCw.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                //nếunếuthất bạithìHủymớigiường ký tựchiếmdùng
                SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/ward/UpdateBedOccupy", new
                {
                    TimeStamp = DateTime.Now.ToString(),
                    cwCode = patChangeAreaRequestDto.cwCode,
                    isOccupy = false
                });
                return "F|gọigiao diệncập nhậttrướcchiếmdùnggiường ký tựTrạng tháithất bại";
            }
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    var patBedSyInfo = GetBedInfo(patChangeAreaRequestDto.cwCode, patChangeAreaRequestDto.OrganizeId);
                    var bedDept = _baseDataDmnService.GetDeptWardRel(patChangeAreaRequestDto.OrganizeId, null, patBedSyInfo.WardCode).FirstOrDefault();

                    var patInfoEntity = db.IQueryable<InpatientPatientInfoEntity>().Where(p => p.zyh == patChangeAreaRequestDto.zyh && p.zt == "1" && p.OrganizeId == patChangeAreaRequestDto.OrganizeId).ToList().FirstOrDefault();
                    patInfoEntity.zybz = Convert.ToInt32(EnumZYBZ.Bqz);
                    patInfoEntity.rqrq = patChangeAreaRequestDto.rqrq;
                    patInfoEntity.BedCode = patChangeAreaRequestDto.cwCode;
                    patInfoEntity.wzjb = patChangeAreaRequestDto.wzjb;
                    patInfoEntity.ysgh = patChangeAreaRequestDto.ysgh;
                    patInfoEntity.WardCode = patChangeAreaRequestDto.WardCode;
                    patInfoEntity.DeptCode = patChangeAreaRequestDto.DeptCode ?? bedDept.DepartmentCode;
                    patInfoEntity.Modify();
                    db.Update(patInfoEntity);

                    //Chèngiường ký tựSử dụngghi chépbảng

                    #region Hủy hồ sơ sử dụng giường cũ của bệnh nhân này，Chỉ giữ lại một bản ghi mới nhất
                    var patBedUsedList = db.IQueryable<InpatientBedUseRecordEntity>().Where(p => p.zyh == patChangeAreaRequestDto.zyh && p.OrganizeId == patBedSyInfo.OrganizeId && p.zt == "1").ToList();
                    foreach (var item in patBedUsedList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }
                    #endregion

                    #region Thêm thông tin bác sĩ nội trú
                    var patDocList = db.IQueryable<InpatientPatientDoctorEntity>().Where(p => p.zyh == patChangeAreaRequestDto.zyh && p.OrganizeId == patBedSyInfo.OrganizeId && p.zt == "1").ToList();
                    foreach (var item in patDocList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }
                    if (!string.IsNullOrWhiteSpace(patChangeAreaRequestDto.ysgh))
                    {
                        InpatientPatientDoctorEntity docEntity = new InpatientPatientDoctorEntity();
                        docEntity.Id = Guid.NewGuid().ToString();
                        docEntity.OrganizeId = patChangeAreaRequestDto.OrganizeId;
                        docEntity.zyh = patChangeAreaRequestDto.zyh;
                        docEntity.Type = Convert.ToInt32(EnumYslx.ZyDoc);
                        docEntity.ysgh = patChangeAreaRequestDto.ysgh;
                        docEntity.ysmc = patChangeAreaRequestDto.ysmc;
                        docEntity.TypeName = "住院医生";
                        docEntity.zt = "1";
                        docEntity.Create();
                        db.Insert(docEntity);
                    }
                    #endregion

                    #region Chèngiường ký tựsử dụngghi chép
                    InpatientBedUseRecordEntity bedEntity = new InpatientBedUseRecordEntity();
                    bedEntity.Id = Guid.NewGuid().ToString();
                    bedEntity.OrganizeId = patBedSyInfo.OrganizeId;
                    bedEntity.blh = patInfoEntity.blh;
                    bedEntity.zyh = patInfoEntity.zyh;
                    bedEntity.BedCode = patBedSyInfo.BedCode;
                    bedEntity.BedNo = patBedSyInfo.BedNo;
                    bedEntity.WardCode = patBedSyInfo.WardCode;
                    bedEntity.WardName = patBedSyInfo.WardName;
                    bedEntity.RoomCode = patBedSyInfo.RoomCode;
                    bedEntity.RoomName = patBedSyInfo.RoomName;
                    bedEntity.DeptCode = bedDept == null ? patInfoEntity.DeptCode : bedDept.DepartmentCode;
                    bedEntity.DeptName = bedDept == null ? null : bedDept.DepartmentName;
                    bedEntity.Status = Convert.ToInt32(EnumCwjlzt.Dq);
                    bedEntity.OccBeginDate = DateTime.Now;

                    bedEntity.Create();
                    db.Insert(bedEntity);
                    #endregion
                    db.Commit();
                }
            }
            catch (Exception ex)
            {
                var reqcwBackObj = new
                {
                    TimeStamp = DateTime.Now.ToString(),
                    cwCode = patChangeAreaRequestDto.cwCode,
                    isOccupy = false
                };
                var patBackInfo = new InpatientPatientInfoDTO()
                {
                    zyh = patChangeAreaRequestDto.zyh,
                    zybz = Convert.ToInt32(EnumZYBZ.Zq)
                };
                //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/ward/UpdateBedOccupy", reqcwBackObj);
                SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patBackInfo);
                return "F|Thao tác thất bại：" + ex.Message;
            }
            return "T|Thao tác thành công";
        }
        public string SavePatInAreaV1(patInAreaRequestDto inareaReq)
        {
            var patInfo = new InpatientPatientInfoDTO
            {
                zyh = inareaReq.zyh,
                zybz = Convert.ToInt32(EnumZYBZ.Bqz),
                cw = inareaReq.cwCode,
                doctor = inareaReq.ysgh
            };
            var reqObj = new
            {
                TimeStamp = DateTime.Now.ToString(),
                lastUpdateTime = DateTime.Now.ToString(),
                zyh = inareaReq.zyh
            };
            var reqcwObj = new
            {
                TimeStamp = DateTime.Now.ToString(),
                cwCode = inareaReq.cwCode,
                isOccupy = true
            };
            //var reqbrObj = new
            //{
            //    TimeStamp = DateTime.Now.ToString(),
            //    zyh = inareaReq.zyh,
            //    zybz = Convert.ToInt32(EnumZYBZ.Bqz)
            //};

            //kéolấyvàokhuBệnh nhânchi tiếtthông tin
            var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<InPatientDetailQueryDto>>("/api/patient/InPatientDetailQuery", reqObj);
            if (apiResp.data == null) return "F|Gọi giao diện lấy thông tin bệnh nhân thất bại";
            //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
            var apiRespCw = SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/ward/UpdateBedOccupy", reqcwObj);
            //Cập nhậtBệnh nhânNhập việnTrạng thái
            var apiRespbr = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfo);
            if (apiRespbr != null && apiRespCw.code == APIRequestHelper.ResponseResultCode.SUCCESS && apiRespbr.code == APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                try
                {
                    var patInfoRes = apiResp.data;
                    patInfoRes.OrganizeId = inareaReq.OrganizeId;
                    patInfoRes.rqrq = Convert.ToDateTime(inareaReq.rq);
                    patInfoRes.wzjb = inareaReq.wzjb;
                    patInfoRes.BedCode = inareaReq.cwCode;
                    patInfoRes.zybz = Convert.ToInt32(EnumZYBZ.Bqz);

                    using (var db = new EFDbTransaction().BeginTrans())
                    {
                        var patOldInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(p => p.zyh == patInfoRes.zyh && p.zt == "1" && p.OrganizeId == patInfoRes.OrganizeId).ToList().FirstOrDefault();
                        var isNew = true;
                        if (patOldInfo != null)
                        {
                            if (patOldInfo.zybz != Convert.ToInt32(EnumZYBZ.Wry) && !string.IsNullOrEmpty(patOldInfo.cyzddm))//ra khỏi khutriệuvềBệnh nhân(xếptrừHủyNhập việnBệnh nhân)
                            {
                                patOldInfo.rqrq = Convert.ToDateTime(inareaReq.rq);
                                patOldInfo.wzjb = inareaReq.wzjb;
                                patOldInfo.BedCode = inareaReq.cwCode;
                                patOldInfo.zybz = Convert.ToInt32(EnumZYBZ.Bqz);
                                isNew = false;
                            }
                            else
                            {
                                patOldInfo.zt = "0";
                            }
                            patOldInfo.Modify();
                            db.Update(patOldInfo);
                        }
                        if (isNew)
                        {
                            #region ChènThông tin bệnh nhânbảng

                            var patInfoEntity = new InpatientPatientInfoEntity
                            {
                                Id = Guid.NewGuid().ToString(),
                                OrganizeId = patInfoRes.OrganizeId,
                                zyh = patInfoRes.zyh,
                                blh = patInfoRes.blh,
                                xm = patInfoRes.xm,
                                py = patInfoRes.py,
                                wb = patInfoRes.wb,
                                sfzh = patInfoRes.zjh,
                                sex = patInfoRes.sex,
                                birth = patInfoRes.csny,
                                zybz = patInfoRes.zybz,
                                DeptCode = patInfoRes.ks,
                                WardCode = patInfoRes.bqCode,
                                ysgh = inareaReq.ysgh,
                                BedCode = patInfoRes.BedCode,
                                ryrq = patInfoRes.ryrq,
                                rqrq = patInfoRes.rqrq,
                                wzjb = patInfoRes.wzjb,
                                ryfs = patInfoRes.ryfs,
                                brxzdm = patInfoRes.brxz,
                                brxzmc = patInfoRes.brxzmc,
                                cardno = patInfoRes.idCardNo,
                                lxr = patInfoRes.contPerName,
                                lxrgx = patInfoRes.contPerRel,
                                lxrdh = patInfoRes.contPerPhoneNum,
                                zddm = patInfoRes.zzdCode,
                                zdmc = patInfoRes.zzdmc,
                                grbh = patInfoRes.grbh
                            };
                            //patInfoEntity.sfqj = false;
                            //patInfoEntity.cqrq = patInfoRes.zyh;
                            //patInfoEntity.hljb = patInfoRes.zyh;
                            //patInfoEntity.cyfs = patInfoRes.zyh;
                            //patInfoEntity.gdxmzxrq = patInfoRes.zyh;
                            //patInfoEntity.cyzddm = patInfoRes.zyh;
                            //patInfoEntity.cyzdmc = patInfoRes.zyh;
                            patInfoEntity.Create();
                            db.Insert(patInfoEntity);
                            #endregion
                        }
                        #region Chẩn đoán nhập việnChènChẩn đoánbảng
                        if (isNew && !string.IsNullOrWhiteSpace(patInfoRes.zzdCode))
                        {
                            var diaEntity = new InpatientPatientDiagnosisEntity
                            {
                                Id = Guid.NewGuid().ToString(),
                                OrganizeId = patInfoRes.OrganizeId,
                                zyh = patInfoRes.zyh,
                                zdlb = "1",
                                zdlx = "0",
                                zddm = patInfoRes.zzdCode,
                                zdmc = patInfoRes.zzdmc
                            };
                            diaEntity.Create();
                            db.Insert(diaEntity);
                        }

                        #endregion

                        #region Thêm thông tin bác sĩ nội trú
                        if (isNew && !string.IsNullOrWhiteSpace(inareaReq.ysgh))
                        {
                            var docEntity = new InpatientPatientDoctorEntity
                            {
                                Id = Guid.NewGuid().ToString(),
                                OrganizeId = inareaReq.OrganizeId,
                                zyh = inareaReq.zyh,
                                Type = Convert.ToInt32(EnumYslx.ZyDoc),
                                ysgh = inareaReq.ysgh,
                                ysmc = inareaReq.ysmc,
                                TypeName = "住院医生",
                                zt = "1"
                            };
                            docEntity.Create();
                            db.Insert(docEntity);
                        }
                        #endregion

                        #region Hủy hồ sơ sử dụng giường cũ của bệnh nhân này，Chỉ giữ lại một bản ghi mới nhất
                        var patBedUsedList = db.IQueryable<InpatientBedUseRecordEntity>().Where(p => p.zyh == patInfoRes.zyh && p.OrganizeId == inareaReq.OrganizeId && p.zt == "1").ToList();
                        foreach (var item in patBedUsedList)
                        {
                            item.zt = "0";
                            item.Modify();
                            db.Update(item);
                        }
                        #endregion

                        #region Chèngiường ký tựsử dụngghi chépbảng
                        var patBedSyInfo = GetBedInfo(inareaReq.cwCode, inareaReq.OrganizeId);
                        var bedEntity = new InpatientBedUseRecordEntity
                        {
                            Id = Guid.NewGuid().ToString(),
                            OrganizeId = patBedSyInfo.OrganizeId,
                            blh = patInfoRes.blh,
                            zyh = patInfoRes.zyh,
                            BedCode = patBedSyInfo.BedCode,
                            BedNo = patBedSyInfo.BedNo,
                            WardCode = patBedSyInfo.WardCode,
                            WardName = patBedSyInfo.WardName,
                            RoomCode = patBedSyInfo.RoomCode,
                            RoomName = patBedSyInfo.RoomName,
                            DeptCode = patInfoRes.ks,
                            DeptName = patInfoRes.ksmc,
                            Status = Convert.ToInt32(EnumCwjlzt.Dq),
                            OccBeginDate = DateTime.Now
                        };
                        bedEntity.Create();
                        db.Insert(bedEntity);
                        #endregion
                        db.Commit();
                    }
                    return "T|Thao tác thành công";
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException; //lấytrongbộNgoại lệ
                    }
                    #region Giao diện khôi phục sau khi cập nhật thất bại
                    var reqcwBackObj = new
                    {
                        TimeStamp = DateTime.Now.ToString(),
                        cwCode = inareaReq.cwCode,
                        isOccupy = false
                    };
                    var BackpatInfo = new InpatientPatientInfoDTO
                    {
                        zyh = inareaReq.zyh,
                        zybz = Convert.ToInt32(EnumZYBZ.Xry)
                    };
                    //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                    SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/ward/UpdateBedOccupy", reqcwBackObj);
                    //Cập nhậtBệnh nhânNhập việnTrạng thái
                    SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", BackpatInfo);
                    #endregion
                    return "F|Thao tác thất bại：" + ex.Message;
                }
            }

            {
                #region Giao diện khôi phục sau khi cập nhật thất bại
                var reqcwBackObj = new
                {
                    TimeStamp = DateTime.Now.ToString(),
                    cwCode = inareaReq.cwCode,
                    isOccupy = false
                };
                var backpatInfo = new InpatientPatientInfoDTO
                {
                    zyh = inareaReq.zyh,
                    zybz = Convert.ToInt32(EnumZYBZ.Xry)
                };
                //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/ward/UpdateBedOccupy", reqcwBackObj);
                //Cập nhậtBệnh nhânNhập việnTrạng thái
                SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", backpatInfo);
                #endregion
                return "F|gọicập nhậtgiường hoặcĐang nằm việnTrạng tháigiao diệnthất bại";
            }

        }
        public string PatCancelInAreaV1(patRequestDto req)
        {
            string cwCode = "";
            string zyh = "";
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    //LấyThông tin bệnh nhân
                    InpatientPatientInfoEntity patInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();
                    //Lấynhấtgầnmột lầnbệnhgiườngghi chép
                    InpatientBedUseRecordEntity bedUsedEntity = db.IQueryable<InpatientBedUseRecordEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();

                    if (patInfo == null || bedUsedEntity == null)
                    {
                        return "F|Lấy thông tin bệnh nhân hoặc hồ sơ sử dụng giường thất bại";
                    }
                    cwCode = patInfo.BedCode;
                    zyh = req.zyh;
                    #region Cập nhật trạng thái bệnh nhân của giường
                    var reqcwObj = new
                    {
                        TimeStamp = DateTime.Now.ToString(),
                        cwCode = cwCode,
                        isOccupy = false
                    };
                    InpatientPatientInfoDTO patInfoReq = new InpatientPatientInfoDTO()
                    {
                        zyh = zyh,
                        zybz = Convert.ToInt32(EnumZYBZ.Xry)
                    };
                    //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                    var apiRespCw = SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                    "/api/ward/UpdateBedOccupy", reqcwObj);
                    //Cập nhậtBệnh nhânNhập việnTrạng thái
                    var apiRespbr = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoReq);
                    #endregion

                    if (apiRespCw.code != APIRequestHelper.ResponseResultCode.SUCCESS || apiRespbr.code != APIRequestHelper.ResponseResultCode.SUCCESS)
                    {
                        var reqcwBackObj = new
                        {
                            TimeStamp = DateTime.Now.ToString(),
                            cwCode = cwCode,
                            isOccupy = true
                        };
                        InpatientPatientInfoDTO patInfoBackReq = new InpatientPatientInfoDTO()
                        {
                            zyh = zyh,
                            zybz = Convert.ToInt32(EnumZYBZ.Bqz)
                        };
                        //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                        SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                        "/api/ward/UpdateBedOccupy", reqcwBackObj);
                        //Cập nhậtBệnh nhânNhập việnTrạng thái
                        SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoBackReq);
                        return "F|gọigiao diệncập nhậtgiường hoặcBệnh nhânTrạng tháithất bại";
                    }

                    #region Cập nhật bảng sử dụng giường bệnh
                    bedUsedEntity.OccEndDate = DateTime.Now;
                    bedUsedEntity.zt = "0";
                    bedUsedEntity.Modify();
                    db.Update(bedUsedEntity);

                    InpatientBedUseRecordEntity newbedUsedEntity = new InpatientBedUseRecordEntity();
                    newbedUsedEntity.Id = Guid.NewGuid().ToString();
                    newbedUsedEntity.OrganizeId = bedUsedEntity.OrganizeId;
                    newbedUsedEntity.blh = bedUsedEntity.blh;
                    newbedUsedEntity.zyh = bedUsedEntity.zyh;
                    newbedUsedEntity.BedCode = bedUsedEntity.BedCode;
                    newbedUsedEntity.BedNo = bedUsedEntity.BedNo;
                    newbedUsedEntity.WardCode = bedUsedEntity.WardCode;
                    newbedUsedEntity.RoomCode = bedUsedEntity.RoomCode;
                    newbedUsedEntity.zt = "1";
                    newbedUsedEntity.Status = Convert.ToInt32(EnumCwjlzt.QxRq);
                    newbedUsedEntity.Create();
                    db.Insert(newbedUsedEntity);
                    #endregion

                    #region Hủy thông tin chẩn đoán cũ
                    var patZDList = db.IQueryable<InpatientPatientDiagnosisEntity>().Where(p => p.zyh == req.zyh && p.OrganizeId == req.OrganizeId && p.zt == "1").ToList();
                    foreach (var item in patZDList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }
                    #endregion

                    #region Cập nhật bảng thông tin bệnh nhân
                    if (patInfo != null)
                    {
                        patInfo.ysgh = null;
                        patInfo.BedCode = null;
                        patInfo.rqrq = null;
                        patInfo.zybz = Convert.ToInt32(EnumZYBZ.Wry);
                        patInfo.Modify();
                        db.Update(patInfo);
                    }
                    #endregion

                    db.Commit();
                }
                return "T|Hủyvàokhuthành công";
            }
            catch (Exception ex)
            {
                var reqcwBackObj = new
                {
                    TimeStamp = DateTime.Now.ToString(),
                    cwCode = cwCode,
                    isOccupy = true
                };
                InpatientPatientInfoDTO patInfoBackReq = new InpatientPatientInfoDTO()
                {
                    zyh = zyh,
                    zybz = Convert.ToInt32(EnumZYBZ.Bqz)
                };
                //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                "/api/ward/UpdateBedOccupy", reqcwBackObj);
                //Cập nhậtBệnh nhânNhập việnTrạng thái
                SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoBackReq);
                return "F|Hủyvàokhuthất bại：" + ex.Message;
            }

        }

        public string SaveChangeAreaV1(patRequestDto req, string bqdm)
        {
            string cwCode = "";
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    //LấyThông tin bệnh nhân
                    InpatientPatientInfoEntity patInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();
                    //Lấynhấtgầnmột lầnbệnhgiườngghi chép
                    InpatientBedUseRecordEntity bedUsedEntity = db.IQueryable<InpatientBedUseRecordEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();
                    if (patInfo == null || bedUsedEntity == null)
                    {
                        return "F|Lấy thông tin bệnh nhân hoặc hồ sơ sử dụng giường thất bại";
                    }
                    cwCode = patInfo.BedCode;
                    //#region Cập nhậtgiường ký tựbệnh nhânTrạng thái
                    //var reqcwObj = new
                    //{
                    //    TimeStamp = DateTime.Now.ToString(),
                    //    cwCode = cwCode,
                    //    isOccupy = false
                    //};
                    ////Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                    //var apiRespCw = SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                    //"/api/ward/UpdateBedOccupy", reqcwObj);
                    //#endregion
                    //if (apiRespCw.code != APIRequestHelper.ResponseResultCode.SUCCESS)
                    //{
                    //    return "F|gọigiao diệnCập nhậtgiường ký tựTrạng tháithất bại";
                    //}

                    #region Hủy thông tin chẩn đoán cũ
                    var patZDList = db.IQueryable<InpatientPatientDiagnosisEntity>().Where(p => p.zyh == req.zyh && p.OrganizeId == req.OrganizeId && p.zt == "1" && p.zdlb == "2").ToList();
                    foreach (var item in patZDList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }
                    #endregion

                    #region hủy bỏlịch sửBác sĩthông tin
                    var patYSList = db.IQueryable<InpatientPatientDoctorEntity>().Where(p => p.zyh == req.zyh && p.OrganizeId == req.OrganizeId && p.zt == "1").ToList();
                    foreach (var item in patYSList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }
                    #endregion

                    #region Cập nhật bảng sử dụng giường bệnh
                    bedUsedEntity.OccEndDate = DateTime.Now;
                    bedUsedEntity.zt = "0";
                    bedUsedEntity.Modify();
                    db.Update(bedUsedEntity);

                    var relvo = _baseDataDmnService.GetDeptWardRel(bedUsedEntity.OrganizeId, "", bqdm).FirstOrDefault();

                    InpatientBedUseRecordEntity newbedUsedEntity = new InpatientBedUseRecordEntity();
                    newbedUsedEntity.Id = Guid.NewGuid().ToString();
                    newbedUsedEntity.OrganizeId = bedUsedEntity.OrganizeId;
                    newbedUsedEntity.blh = bedUsedEntity.blh;
                    newbedUsedEntity.zyh = bedUsedEntity.zyh;
                    newbedUsedEntity.BedCode = bedUsedEntity.BedCode;
                    newbedUsedEntity.BedNo = bedUsedEntity.BedNo;
                    newbedUsedEntity.WardCode = bedUsedEntity.WardCode;
                    newbedUsedEntity.WardName = bedUsedEntity.WardName;
                    newbedUsedEntity.RoomCode = bedUsedEntity.RoomCode;
                    newbedUsedEntity.RoomName = bedUsedEntity.RoomName;
                    newbedUsedEntity.DeptCode = bedUsedEntity.DeptCode;
                    newbedUsedEntity.DeptName = bedUsedEntity.DeptName;
                    newbedUsedEntity.TransWardCode = bqdm;
                    newbedUsedEntity.TransDeptCode = relvo == null ? null : relvo.DepartmentCode;
                    newbedUsedEntity.zt = "1";
                    newbedUsedEntity.Status = Convert.ToInt32(EnumCwjlzt.Zq);
                    newbedUsedEntity.Create();
                    db.Insert(newbedUsedEntity);
                    #endregion

                    #region Cập nhật bảng thông tin bệnh nhân
                    patInfo.ysgh = null;
                    //patInfo.BedCode = null;
                    patInfo.rqrq = null;
                    patInfo.zybz = Convert.ToInt32(EnumZYBZ.Zq);
                    //patInfo.WardCode = bqdm;
                    patInfo.Modify();
                    db.Update(patInfo);
                    #endregion
                    db.Commit();
                }
                return "T|chuyển khuthành công";
            }
            catch (Exception ex)
            {
                var reqcwBackObj = new
                {
                    TimeStamp = DateTime.Now.ToString(),
                    cwCode = cwCode,
                    isOccupy = true
                };
                //Cập nhậtgiường ký tựTrạng thái(làKhôngchiếmdùng)
                var apiRespCw = SiteBaseAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>(
                "/api/ward/UpdateBedOccupy", reqcwBackObj);
                return "F|chuyển khuthất bại：" + ex.Message;
            }

        }

        #endregion

    }
}
