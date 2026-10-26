using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    public class CommonDmnService : DmnServiceBase, ICommonDmnService
    {
        public CommonDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy khám bệnh số ngư ời(ngoại trú và nhập viện)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public VisitNumBO GetVisitNum(string configmethod, string orgId = null)
        {

            /*************************************Khoa ngoại trú, tạm thời không xét hoàn toàn bộ*****************************************************/
            List<OutpatientVisitNumVO> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"--当前年份
                DECLARE @currYear varchar(30)=(Convert(varchar, year(getdate()),10))
                --当前年月
                DECLARE @currYearMoth varchar(30)
                SET @currYearMoth=@currYear+'01';
                --select @currYearMoth
                --门诊
                SELECT col, isnull(num1,0) num FROM [dbo].[f_split](''+@currYear+'01,'+@currYear+'02,'+@currYear+'03,'+@currYear+'04,'+@currYear+'05,'+@currYear+'06,'+@currYear+'07,'+@currYear+'08,'+@currYear+'09,'+@currYear+'10,'+@currYear+'11,'+@currYear+'12',',') 
                LEFT JOIN
                (
	                select groupDate, count(1) num1 from 
	                (
	                select distinct CONVERT(varchar(6), groupDate, 112) groupDate, patid
	                from 
	                (");
            if (configmethod.ToLower() == "reg")
            {
                sqlStr.Append(@"   SELECT    CONVERT(VARCHAR(6), ISNULL(gh.ghrq,
                                                              CreateTime), 112) AS groupDate,
                                                    mzh patid
                                          FROM      mz_gh gh
                                          WHERE gh.zt = '1'
                                                    AND ISNULL(gh.ghzt, '') <> '2'
                                                    AND OrganizeId = @orgId
                                                    AND CONVERT(VARCHAR(6), ISNULL(gh.ghrq,
                                                              CreateTime), 112) >= @currYearMoth");
            }
            else if (configmethod.ToLower() == "exce")
            {
                sqlStr.Append(@"  SELECT  CONVERT(VARCHAR(6), zx.zxsj, 112) AS groupDate ,
                        jh.patid
                FROM    dbo.mz_jzjhmxzx (NOLOCK) zx
                        LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                      AND mx.OrganizeId = @orgId
                        INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                     AND jh.OrganizeId = @orgId
                WHERE   zx.zt = '1'
                        AND zx.OrganizeId = @orgId
                        AND CONVERT(VARCHAR(6), zx.zxsj, 112) >= @currYearMoth");
            }
            else
            {
                sqlStr.Append(@" select convert(varchar(6),
			                isnull(ssrq, sfrq),
			                112) AS groupDate , patid
	                from mz_xm  where zt = '1' and OrganizeId=@orgId and convert(varchar(6),isnull(ssrq, sfrq), 112) >=@currYearMoth");
            }

            sqlStr.Append(@")aaa
	                ) bbb
	                group by bbb.groupDate
                    ) AS a
                    ON a.groupDate=col
                    ORDER BY col");

            SqlParameter[] param =
            {
                    new SqlParameter("@orgId",orgId)
                };
            outpatientlist = this.FindList<OutpatientVisitNumVO>(sqlStr.ToString(), param).ToList();


            /**********************************************Khoa nhập viện,Đ ã loại bỏ hoàn toàn bộ củangười*****************************************************/

            var inpatientList = new List<InpatientVisitNumVO>();

            var sqlStr3 = new StringBuilder();
            List<InpatientVisitBasicVO> zyList = null;

            sqlStr3.Append(@"select zyh, zybz, ryrq, cyrq
                                    from zy_brjbxx
                                    where zt = '1' and zybz <> 9
                                    and OrganizeId = @orgId
                                     AND ( zybz <> 3 --已出院
                                      AND ( ryrq >= @startTime
                                            AND ryrq < @entTime
                                          )
                                      OR ( cyrq >= @startTime
                                           AND cyrq < @entTime
                                         ))");
            zyList = this.FindList<InpatientVisitBasicVO>(sqlStr3.ToString(), new[] {
                    new SqlParameter("@orgId", orgId)
                    ,new SqlParameter("@startTime", new DateTime(DateTime.Now.Year, 1,1))
                    ,new SqlParameter("@entTime", new DateTime(DateTime.Now.Year + 1, 1,1))
                });

            var nowYear = DateTime.Now.Year;
            var nowMonth = DateTime.Now.Month;
            string ycy = ((int)EnumZYBZ.Ycy).ToString();
            for (var i = 0; i < 12; i++)
            {
                var num = 0;
                if (i < nowMonth)
                {
                    //1.KhiThángNhập việnHoặcKhiThángXuất viện
                    //2.Tại viện trạng thái<>Đ ãxuất viện and ngày nhập viện<khi tháng
                    num = zyList.Count(p =>
                    (p.ryrq >= new DateTime(nowYear, (i + 1), 1)
                    && p.ryrq < new DateTime(nowYear, (i + 1), 1).AddMonths(1)
                    || (p.cyrq >= new DateTime(nowYear, (i + 1), 1)
                    && p.cyrq < new DateTime(nowYear, (i + 1), 1).AddMonths(1)))
                    || (p.zybz != ycy && p.ryrq < new DateTime(nowYear, (i + 1), 1)));
                }
                inpatientList.Add(new InpatientVisitNumVO()
                {
                    num = num,
                });
            }

            //Đặttại một Đ ối tư ợng,quay lại đ ến trang
            var visitNumBO = new VisitNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList,
                zyrs = zyList.Count,
            };
            return visitNumBO;
        }

        /// <summary>
        /// Lấyhôm nay trư ớc đ ó 10 tuần khám bệnh Lư ợt ngư ờichi tiết
        /// </summary>
        /// <param name="month"></param>
        /// <param name="isAdministrator"></param>
        /// <param name="orgId"></param>
        /// <param name="topOrgId"></param>
        public MonthVisitNumBO GetWeekNum(string configmethod, string orgId)
        {
            /*************************************Khoa ngoại trú, tạm thời không xét hoàn toàn bộ*****************************************************/
            List<OutpatientWeekVisitNumVO> outpatientlist = null;
            var basic = @"WITH    cteTree
                              AS ( SELECT   DATEADD(wk, DATEDIFF(wk, 0, GETDATE()), 0) wbegin ,
                                            GETDATE() wend
                                   UNION ALL
                                   SELECT   DATEADD(d, -7, wbegin) wbegin ,
                                            DATEADD(d, -1, wbegin) wend
                                   FROM     cteTree
                                   WHERE    wbegin > DATEADD(wk, -9,
                                         DATEADD(wk,
                                                 DATEDIFF(wk, 0, GETDATE()), 0)))";
            StringBuilder sqlStr = new StringBuilder(basic);
            if (configmethod.ToLower() == "reg")
            {
                sqlStr.Append(@"    SELECT DISTINCT
                                    CONVERT(VARCHAR(10), aaa.wbegin, 25) wbegin ,
                                    CONVERT(VARCHAR(10), aaa.wend, 25) wend ,
                                    COUNT(1) num
                            FROM    ( SELECT  DISTINCT  cte.wbegin ,
                                                cte.wend ,
                                                CONVERT(VARCHAR(10), ISNULL(ghrq, CreateTime), 25) AS vDate ,
                                                mzh
                                      FROM      dbo.mz_gh
                                                INNER JOIN cteTree cte ON CONVERT(VARCHAR(10), ISNULL(ghrq,
                                                                                      CreateTime), 25) >= cte.wbegin
                                                                          AND CONVERT(VARCHAR(10), ISNULL(ghrq,
                                                                                      CreateTime), 25) < cte.wend
                                      WHERE     zt = '1' and ISNULL(ghzt, '') <> '2'
                                                AND OrganizeId =@orgId
                                    ) aaa
                            GROUP BY aaa.wbegin ,
                                    aaa.wend;");
            }
            else
            {
                sqlStr.Append(@"    SELECT DISTINCT
                                    CONVERT(VARCHAR(10), aaa.wbegin, 25) wbegin,
                                    CONVERT(VARCHAR(10), aaa.wend, 25) wend ,
                                    COUNT(1) num
                            FROM    ( SELECT  DISTINCT  cte.wbegin ,
                                                cte.wend ,
                                                CONVERT(VARCHAR(10), ISNULL(ssrq, sfrq), 25) AS vDate ,
                                                patid
                                      FROM      mz_xm
                                                INNER JOIN cteTree cte ON CONVERT(VARCHAR(10), ISNULL(ssrq,
                                                                                      CreateTime), 25) >= cte.wbegin
                                                                          AND CONVERT(VARCHAR(10), ISNULL(ssrq,
                                                                                      CreateTime), 25) < cte.wend
                                      WHERE     zt = '1'
                                                AND OrganizeId = @orgId
                                    ) aaa
                            GROUP BY aaa.wbegin ,
                                    aaa.wend;");
            }
            SqlParameter[] param =
                         {
                    new SqlParameter("@orgId",orgId)
                };
            outpatientlist = this.FindList<OutpatientWeekVisitNumVO>(sqlStr.ToString(), param).ToList();

            /****************Khoa nhập viện,Đ ã loại bỏ hoàn toàn bộ củangười**********************/
            StringBuilder sqlStr3 = new StringBuilder(basic);
            var inpatientList = new List<OutpatientWeekVisitNumVO>();
            List<InpatientVisitBasicVO> zyList = null;
            //Nếu Làadmin đ ăng nhập,thì thống kê này tổ chức dư ới tất cả các bệnh viện của khám bệnh số ngư ời
            sqlStr3.Append(@"     SELECT  CONVERT(VARCHAR(10), cte.wbegin, 25) wbegin,
                                    CONVERT(VARCHAR(10), cte.wend, 25)  wend,
                                    zyh ,
                                    zybz ,
                                    ryrq ,
                                    cyrq
                            FROM    zy_brjbxx
                                    INNER JOIN cteTree cte ON CONVERT(VARCHAR(10), ryrq, 25) >= cte.wbegin
                                                              AND CONVERT(VARCHAR(10), ryrq, 25) < cte.wend
                            WHERE   zt = '1'
                                    AND zybz <> 9
                                    AND OrganizeId = @orgId");
            zyList = this.FindList<InpatientVisitBasicVO>(sqlStr3.ToString(), new[] {
                    new SqlParameter("@orgId", orgId)});
            string ycy = ((int)EnumZYBZ.Ycy).ToString();
            var LastWeekInfo = GetLastWeek();//Lấy nhất sau 10 tuần ngày list
            if (LastWeekInfo != null && LastWeekInfo.Count() > 0)
            {
                for (int i = 0; i < LastWeekInfo.Count(); i++)
                {
                    //1. bản tuần nhập viện hoặc bản tuần xuất viện
                    //2.Tại viện trạng thái<>Đ ãxuất viện and ngày nhập viện< bản tuần
                    var Num = zyList.Count(p =>
                      (p.ryrq >= DateTime.Parse(LastWeekInfo[i].wbegin)
                      && p.ryrq < DateTime.Parse(LastWeekInfo[i].wend)
                      || (p.cyrq >= DateTime.Parse(LastWeekInfo[i].wbegin)
                      && p.cyrq < DateTime.Parse(LastWeekInfo[i].wend)))
                      || (p.zybz != ycy && p.ryrq < DateTime.Parse(LastWeekInfo[i].wbegin)));
                    inpatientList.Add(new OutpatientWeekVisitNumVO()
                    {
                        num = Num,
                        wbegin = LastWeekInfo[i].wbegin.ToString(),
                        wend = LastWeekInfo[i].wend.ToString()
                    });
                }
            }
            //Đặttại một Đ ối tư ợng,quay lại đ ến trang
            var visitNumBO = new MonthVisitNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList
            };
            return visitNumBO;
        }

        /// <summary>
        /// Lấy nhất sau 10 tuần ngày list
        /// </summary>
        /// <returns></returns>
        public List<LastWeekInfo> GetLastWeek()
        {
            var LastWeekInfo = this.FindList<LastWeekInfo>(@"WITH    cteTree
                              AS(SELECT    DATEADD(wk, DATEDIFF(wk, 0, GETDATE()), 0) wbegin ,
                                            GETDATE() wend
                                   UNION ALL
                                   SELECT   DATEADD(d, -7, wbegin) wbegin,
                                            DATEADD(d, -1, wbegin) wend
                                   FROM     cteTree
                                   WHERE    wbegin > DATEADD(wk, -9,
                                         DATEADD(wk,
                                                 DATEDIFF(wk, 0, GETDATE()), 0))) 
                                     SELECT  CONVERT(VARCHAR(10), wbegin, 25) wbegin ,
                                                CONVERT(VARCHAR(10), wend, 25) wend
                                        FROM    cteTree ORDER BY wbegin");
            return LastWeekInfo;
        }

        /// <summary>
        /// Quý ChâuBHYT, thông qua giao dịch số thứ tự lấy giao dịch xác minh mã 
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public string Gzyb_GetJyyzmByJylsh(string jylsh, string OrgId)
        {
            const string sqlStr = " SELECT TOP 1 jyyzm FROM V_Gzyb_Trancation WHERE jylsh=@jylsh AND OrganizeId=@orgId ";
            SqlParameter[] param =
            {
                new SqlParameter("@jylsh",jylsh),
                new SqlParameter("@orgId",OrgId)
            };
            List<string> results = this.FindList<string>(sqlStr, param).ToList();
            if (results != null && results.Count > 0)
                return results[0];
            else
                return string.Empty;
        }

        /// <summary>
        /// Lấy thuốc phân loạilist(không phải thu phí loại lớn)
        /// </summary>
        /// <returns></returns>
        public IList<SysMedicineClassificationVEntity> GetMedicineClassificationList()
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_xt_ypfl";
            return this.FindList<SysMedicineClassificationVEntity>(sql);
        }

        /// <summary>
        /// LấyHạng mụcPhân loạilist
        /// </summary>
        /// <returns></returns>
        public IList<SysChargeCategoryVEntity> GetChargetItemClassificationList(string orgId)
        {
            //2Hạng mục
            var sql = "select * from [NewtouchHIS_Base]..V_S_xt_sfdl where dllb = 2 and OrganizeId = @orgId and zt = '1'";
            return this.FindList<SysChargeCategoryVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }


        #region base字典等公共数据
        /// <summary>
        /// LấyKhoaLiên kếtCủaKhu bệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysDepartmentWardRelationVO> GetWardbyDept(string orgId,string ks,string keyword)
        {
            string sql = @"select a.bqCode bq,a.bqmc,c.Code ks,c.Name ksmc
from NewtouchHIS_Base.dbo.xt_bq a with(nolock)
left join NewtouchHIS_Base.dbo.Sys_DepartmentWardRelation b with(nolock) on a.OrganizeId=b.OrganizeId and a.bqCode=b.bqCode and b.zt='1' 
left join NewtouchHIS_Base.dbo.Sys_Department c  with(nolock) on b.OrganizeId=c.OrganizeId and b.DepartmentId=c.Id and c.zt='1'
where a.OrganizeId=@orgId and a.zt='1'";
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and b.bqCode=@ks ";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and a.bqmc like @keyword ";
            }
            return this.FindList<SysDepartmentWardRelationVO>(sql, new SqlParameter[] {
                new SqlParameter("orgId",orgId),
                new SqlParameter("ks",ks??""),
                new SqlParameter("keyword","%"+keyword??""+"%"),
            });

        }

        #endregion
    }
}

