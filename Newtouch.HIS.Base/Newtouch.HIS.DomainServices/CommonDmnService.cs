using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    class CommonDmnService : DmnServiceBase, ICommonDmnService
    {
        public CommonDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lấy số lượng bệnh nhân khám (ngoại trú và nội trú)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public VisitNumBO GetVisitNum(bool isAdministrator, string orgId = null, string topOrgId = null)
        {

            /*************************************Phòng ngoại trú, tạm thời chưa xét đến trả toàn bộ*****************************************************/
            List<OutpatientVisitNumVO> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"

--当前年份
DECLARE @currYear varchar(30)=(Convert(varchar, year(getdate()),10))
--当前年月
DECLARE @currYearMoth varchar(30)
SET @currYearMoth=@currYear+'01';
--select @currYearMoth

");
            //Nếu admin đăng nhập thì thống kê số lượng bệnh nhân khám của tất cả các bệnh viện thuộc tổ chức đó
            if (isAdministrator)
            {
                sqlStr.Append(@"

WITH cteTree AS 
    (SELECT id
    FROM [Sys_Organize]
    WHERE TopOrganizeId=@topOrgId
    UNION ALL
	SELECT a.id
    FROM [Sys_Organize] a
    INNER JOIN cteTree
        ON cteTree.id=a.ParentId AND a.TopOrganizeId=@topOrgId) 
                ");
            }
            sqlStr.Append(@"
--门诊
SELECT col, isnull(num1,0) num FROM [dbo].[f_split](''+@currYear+'01,'+@currYear+'02,'+@currYear+'03,'+@currYear+'04,'+@currYear+'05,'+@currYear+'06,'+@currYear+'07,'+@currYear+'08,'+@currYear+'09,'+@currYear+'10,'+@currYear+'11,'+@currYear+'12',',') 
LEFT JOIN
(
	select groupDate, count(1) num1 from 
	(
	select distinct CONVERT(varchar(6), groupDate, 112) groupDate, patid
	from 
	(
	select convert(varchar(6),
			CreateTime,
			112) AS groupDate , patid
	from NewtouchHIS_Sett..mz_xm
            ");
            //admin đăng nhập, liên kết tất cả các bệnh viện thuộc tổ chức đó
            if (isAdministrator)
            {
                sqlStr.Append(@"
    inner join cteTree on cteTree.id=OrganizeId
    where zt = '1'
                    ");
            }
            //Ngược lại, chỉ liên kết bệnh viện đó
            else
            {
                sqlStr.Append(@" where zt = '1' and OrganizeId=@orgId");
            }
            sqlStr.Append(@"
    and convert(varchar(6),CreateTime, 112) >=@currYearMoth
	)aaa
	) bbb
	group by bbb.groupDate
) AS a
ON a.groupDate=col
                ");
            if (isAdministrator)
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@topOrgId",topOrgId)
                };
                outpatientlist = this.FindList<OutpatientVisitNumVO>(sqlStr.ToString(), param).ToList();
            }
            else
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@orgId",orgId)
                };
                outpatientlist = this.FindList<OutpatientVisitNumVO>(sqlStr.ToString(), param).ToList();
            }


            /**********************************************Phòng nội trú, đã loại bỏ người trả toàn bộ*****************************************************/

            List<InpatientVisitNumVO> inpatientList = null;
            StringBuilder sqlStr2 = new StringBuilder();
            sqlStr2.Append(@"

--当前年份
DECLARE @currYear varchar(30)=(Convert(varchar, year(getdate()),10))
--当前年月
DECLARE @currYearMoth varchar(30)
SET @currYearMoth=@currYear+'01';
--select @currYearMoth
");
            //Nếu admin đăng nhập thì thống kê số lượng bệnh nhân khám của tất cả các bệnh viện thuộc tổ chức đó
            if (isAdministrator)
            {
                sqlStr2.Append(@"
WITH cteTree AS 
    (SELECT id
    FROM [Sys_Organize]
    WHERE TopOrganizeId=@topOrgId
    UNION ALL
	SELECT a.id
    FROM [Sys_Organize] a
    INNER JOIN cteTree
        ON cteTree.id=a.ParentId AND a.TopOrganizeId=@topOrgId) 
");
            }
            sqlStr2.Append(@"
--住院
SELECT col, isnull(num1,0) num FROM [dbo].[f_split](''+@currYear+'01,'+@currYear+'02,'+@currYear+'03,'+@currYear+'04,'+@currYear+'05,'+@currYear+'06,'+@currYear+'07,'+@currYear+'08,'+@currYear+'09,'+@currYear+'10,'+@currYear+'11,'+@currYear+'12',',') 
LEFT JOIN
(
	select ddd.tdrq groupDate, count(1) num1 from 
	(
	select distinct ccc.patid, bbb.tdrq from
	(
	select distinct CONVERT(varchar(6), min(tdrq), 112) tdrq, min(zyh) zyh
	from 
	(
	select case when isnull(cxzyjfbbh, 0) = 0 then jfbbh else cxzyjfbbh end jfbbh, tdrq, zyh, sl
	from NewtouchHIS_Sett..zy_xmjfb
            ");
            //admin đăng nhập, liên kết tất cả các bệnh viện thuộc tổ chức đó
            if (isAdministrator)
            {
                sqlStr2.Append(@"
    inner join cteTree on cteTree.id=OrganizeId
	where zt = '1'
                ");
            }
            //Ngược lại, chỉ liên kết bệnh viện đó
            else {
                sqlStr2.Append(@" where zt = '1' and OrganizeId=@orgId");
            }
            sqlStr2.Append(@"
   and convert(varchar(6),tdrq, 112) >=@currYearMoth
	)aaa
	group by jfbbh
	having SUM(sl) > 0
	) bbb
	left join NewtouchHIS_Sett..zy_brjbxx ccc
            ");
            //admin đăng nhập, liên kết tất cả các bệnh viện thuộc tổ chức đó
            if (isAdministrator)
            {
                sqlStr2.Append(@"
    on bbb.zyh = ccc.zyh 
	inner join cteTree on cteTree.id=ccc.OrganizeId
                ");
            }
            //Ngược lại, chỉ liên kết bệnh viện đó
            else
            {
                sqlStr2.Append(@" on bbb.zyh = ccc.zyh and OrganizeId=@orgId");
            }
            sqlStr2.Append(@"
    ) ddd
	group by ddd.tdrq
) AS a
ON a.groupDate=col
                ");
            if (isAdministrator)
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@topOrgId",topOrgId)
                };
                inpatientList = this.FindList<InpatientVisitNumVO>(sqlStr2.ToString(), param).ToList();
            }
            else
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@orgId",orgId)
                };
                inpatientList = this.FindList<InpatientVisitNumVO>(sqlStr2.ToString(), param).ToList();
            }

            //Đặt vào một đối tượng, trả về trang
            var visitNumBO = new VisitNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList
            };
            return visitNumBO;
        }
    }
}
